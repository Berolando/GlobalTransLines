using Abstraccion;
using BE.Dominio;
using BE.Infra;
using Transversales;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using MPP.Infra;

namespace BLL.Infra
{
    public class BackupBLL : IGestor<BEBackup>
    {
        private BackupMPP backupMPP = new BackupMPP();
        private BitacoraBLL bitacoraBLL = new BitacoraBLL();

        #region "Guardar (Crear respaldo completo)"

        // Genera un respaldo completo de los XML operativos y registra la operación en bitácora.
        public void Guardar(BEBackup backup)
        {
            try
            {
                ValidadorBLL.ObjetoNoNulo(backup, "Backup");
                ValidadorBLL.CampoObligatorio(backup.Usuario, "Usuario");
                ValidadorBLL.CampoObligatorio(backup.Descripcion, "Descripción del backup");

                // La BLL completa fecha y tipo para que la UI no defina datos de auditoría.
                backup.Fecha = DateTime.Now;
                backup.TipoOperacion = TipoOperacion.Backup;

                // El respaldo físico no incluye Bitácora/Eventos para conservar la auditoría posterior al restore.
                string carpetaBackup = backupMPP.CrearRespaldoCompleto(backup.Usuario);
                backup.RutaArchivo = carpetaBackup;

                backupMPP.Alta(backup);

                // La generación del backup queda registrada como evento crítico del sistema.
                bitacoraBLL.RegistrarEvento(
                    new BEUsuario { Id = backup.UsuarioId, Username = backup.Usuario },
                    TipoOperacion.Backup,
                    "Respaldo completo generado correctamente.",
                    "Transversal/Backup");

                // Se conserva un historial acotado por usuario para que la carpeta de backups no crezca indefinidamente.
                var todosDelUsuario = ListarTodo()
                    .Where(b => b.Usuario == backup.Usuario)
                    .OrderByDescending(b => b.Fecha)
                    .ToList();

                if (todosDelUsuario.Count > 10)
                {
                    foreach (var b in todosDelUsuario.Skip(10))
                        Eliminar(b);
                }
            }
            catch (IOException ex)
            {
                throw new Exception("Error de acceso a los archivos durante el backup.", ex);
            }
          
            catch (Exception ex)
            {
                throw new Exception("Error general al guardar el backup.", ex);
            }
        }

        // Genera el respaldo asociado a una sesión autenticada sin delegar datos de auditoría en la UI.
        public void GenerarAutomaticoAlIniciarSesion(BEUsuario usuario)
        {
            try
            {
                ValidadorBLL.ObjetoNoNulo(usuario, "Usuario");
                ValidadorBLL.CampoObligatorio(usuario.Username, "Nombre de usuario");

                Guardar(new BEBackup
                {
                    UsuarioId = usuario.Id,
                    Usuario = usuario.Username,
                    Descripcion = "Backup automático al inicio de sesión"
                });
            }
            catch (Exception ex)
            {
                throw new Exception("Error al generar el backup automático de inicio de sesión.", ex);
            }
        }

        #endregion

        #region "Restaurar (Aplicar respaldo completo)"

        // Restaura un respaldo existente sin perder la auditoría de lo ocurrido después.
        public void Restaurar(BEBackup backup, string carpetaBackupSeleccionada)
        {
            try
            {
                ValidadorBLL.ObjetoNoNulo(backup, "Backup");
                ValidadorBLL.CampoObligatorio(carpetaBackupSeleccionada, "Carpeta de backup seleccionada");

                // La restauración también se registra como operación crítica.
                backup.Fecha = DateTime.Now;
                backup.TipoOperacion = TipoOperacion.Restore;

                // El restore devuelve los XML operativos al punto elegido, pero mantiene Bitácora/Eventos.
                backupMPP.RestaurarRespaldoCompleto(carpetaBackupSeleccionada);

                bitacoraBLL.RegistrarEvento(
                    new BEUsuario { Id = backup.UsuarioId, Username = backup.Usuario },
                    TipoOperacion.Restore,
                    "Restauración completa realizada correctamente.",
                    "Transversal/Backup");
            }
            catch (IOException ex)
            {
                throw new Exception("Error de acceso a los archivos durante la restauración.", ex);
            }
           
            catch (Exception ex)
            {
                throw new Exception("Error general al restaurar el backup.", ex);
            }
        }

        #endregion

        #region "Listar / Eliminar / Utilitarios"

        // Devuelve los backups registrados para que la UI pueda mostrar el historial disponible.
        public List<BEBackup> ListarTodo()
        {
            try
            {
                return backupMPP.Listar();
            }
            catch (Exception ex)
            {
                throw new Exception("Error al listar backups.", ex);
            }
        }

        // Busca un backup por Id sin aplicar reglas adicionales.
        public BEBackup ListarObjeto(BEBackup backup)
        {
            try
            {
                ValidadorBLL.ObjetoNoNulo(backup, "Backup");
                return backupMPP.BuscarPorId(backup.Id);
            }
            catch (Exception ex)
            {
                throw new Exception("Error al buscar backup por Id.", ex);
            }
        }

        // Baja lógica del registro de backup; el criterio físico queda encapsulado en MPP.
        public void Eliminar(BEBackup backup)
        {
            try
            {
                ValidadorBLL.ObjetoNoNulo(backup, "Backup");
                backupMPP.Baja(backup);
            }
            catch (Exception ex)
            {
                throw new Exception("Error al eliminar backup.", ex);
            }
        }

        // Permite generar el próximo Id desde la capa de negocio.
        public long ObtenerUltimoId()
        {
            try
            {
                return backupMPP.ObtenerUltimoId();
            }
            catch (Exception ex)
            {
                throw new Exception("Error al obtener último Id de backup.", ex);
            }
        }

        #endregion
    }
}
