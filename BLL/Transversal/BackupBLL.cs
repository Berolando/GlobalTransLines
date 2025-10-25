using System;
using System.Collections.Generic;
using Abstraccion;
using BE.Transversal;
using BE.Enums;
using MPP.Transversal;

namespace BLL.Transversal
{
    public class BackupBLL : IGestor<BEBackup>
    {
        private BackupMPP backupMPP = new BackupMPP();
        private BitacoraBLL bitacoraBLL = new BitacoraBLL();

        #region "Guardar"

        public void Guardar(BEBackup backup)
        {
            try
            {
                ValidadorBLL.ObjetoNoNulo(backup, "Backup");
                ValidadorBLL.CampoObligatorio(backup.Usuario, "Usuario");
                ValidadorBLL.CampoObligatorio(backup.Descripcion, "Descripción del backup");

                backup.Fecha = DateTime.Now;
                backup.TipoOperacion = TipoOperacionEnum.Backup;

                // Registrar en XML
                backupMPP.Alta(backup);

                // Crear respaldo físico completo
                backupMPP.CrearRespaldoCompleto();

                // Registrar en bitácora
                bitacoraBLL.RegistrarEvento(
                    new BEUsuario { Username = backup.Usuario },
                    TipoOperacionEnum.Backup,
                    "Respaldo completo generado correctamente.",
                    "Transversal/Backup");
            }
            catch (Exception ex)
            {
                throw new Exception("Error al guardar el backup.", ex);
            }
        }

        #endregion

        #region "Restaurar"

        public void Restaurar(BEBackup backup, string carpetaBackupSeleccionada)
        {
            try
            {
                ValidadorBLL.ObjetoNoNulo(backup, "Backup");
                ValidadorBLL.CampoObligatorio(carpetaBackupSeleccionada, "Carpeta de backup");

                backup.Fecha = DateTime.Now;
                backup.TipoOperacion = TipoOperacionEnum.Restore;

                // Restaurar físicamente
                backupMPP.RestaurarRespaldoCompleto(carpetaBackupSeleccionada);

                // Registrar evento
                bitacoraBLL.RegistrarEvento(
                    new BEUsuario { Username = backup.Usuario },
                    TipoOperacionEnum.Restore,
                    "Restauración completa realizada correctamente.",
                    "Transversal/Backup");
            }
            catch (Exception ex)
            {
                throw new Exception("Error al restaurar el backup.", ex);
            }
        }

        #endregion

        #region "Listar / Eliminar / Utilitarios"

        public List<BEBackup> ListarTodo()
        {
            try { return backupMPP.Listar(); }
            catch (Exception ex) { throw new Exception("Error al listar backups.", ex); }
        }

        public BEBackup ListarObjeto(BEBackup backup)
        {
            try
            {
                ValidadorBLL.ObjetoNoNulo(backup, "Backup");
                return backupMPP.BuscarPorId(backup.Id);
            }
            catch (Exception ex)
            {
                throw new Exception("Error al buscar backup.", ex);
            }
        }

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

        public long ObtenerUltimoId()
        {
            try { return backupMPP.ObtenerUltimoId(); }
            catch (Exception ex) { throw new Exception("Error al obtener último Id de backup.", ex); }
        }

        #endregion
    }
}
