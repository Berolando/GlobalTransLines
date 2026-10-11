using BE.Dominio;
using BE.Infra;
using Transversales;
using System;
using System.Collections.Generic;
using System.Linq;
using MPP.Infra;

namespace BLL.Infra
{
    // Servicio de auditoría: registra eventos individuales y arma bitácoras lógicas para consulta.
    public class BitacoraBLL
    {
        private EventoMPP eventoMPP = new EventoMPP();
        

        #region "Registrar evento"

        // Registra un evento individual con usuario, tipo, descripción y origen.
        public void RegistrarEvento(BEUsuario usuario, TipoOperacion tipo, string descripcion, string origen)
        {
            try
            {
                ValidadorBLL.ObjetoNoNulo(usuario, "Usuario");
                ValidadorBLL.CampoObligatorio(descripcion, "Descripción del evento");
                ValidadorBLL.CampoObligatorio(origen, "Origen del evento");

                // El evento es la unidad persistida; la bitácora se arma después agrupando eventos.
                var evento = new BEEvento(DateTime.Now, usuario, descripcion, tipo, origen);

                eventoMPP.Alta(evento);
            }
            catch (Exception ex)
            {
                throw new Exception("Error al registrar el evento en la bitácora.", ex);
            }
        }

        #endregion

        #region "Listar eventos"

        // Devuelve todos los eventos reconstruyendo el usuario cuando el XML solo trae su Id.
        public List<BEEvento> ListarTodo()
        {
            try
            {
                var lista = eventoMPP.Listar();

                foreach (var e in lista)
                {
                    // El usuario se reconstruye por Id para mostrar datos completos sin duplicarlos en Eventos.xml.
                    if ((e.Usuario == null || e.Usuario.Id == 0) && e.UsuarioId > 0)
                    {
                        e.Usuario = ReconstructorBLL.Reconstruir<BEUsuario>(e.UsuarioId);
                    }
                }

                return lista.OrderByDescending(x => x.Fecha).ToList();
            }
            catch (Exception ex)
            {
                throw new Exception("Error al listar eventos de la bitácora.", ex);
            }
        }

        #endregion

        #region "Filtros y consultas útiles"

        // Permite consultar eventos por operación crítica o informativa.
        public List<BEEvento> ListarPorTipo(TipoOperacion tipo)
        {
            try
            {
                var todos = ListarTodo();
                return todos.Where(e => e.Tipo == tipo).ToList();
            }
            catch (Exception ex)
            {
                throw new Exception("Error al filtrar eventos por tipo.", ex);
            }
        }

        // Permite reconstruir la actividad de un usuario.
        public List<BEEvento> ListarPorUsuario(BEUsuario usuario)
        {
            try
            {
                ValidadorBLL.ObjetoNoNulo(usuario, "Usuario");
                return ListarTodo().Where(e => e.Usuario != null && e.Usuario.Id == usuario.Id).ToList();
            }
            catch (Exception ex)
            {
                throw new Exception("Error al filtrar eventos por usuario.", ex);
            }
        }

        // Permite consultar la auditoría de un período.
        public List<BEEvento> ListarPorFechas(DateTime desde, DateTime hasta)
        {
            try
            {
                return ListarTodo()
                    .Where(e => e.Fecha >= desde && e.Fecha <= hasta)
                    .OrderByDescending(e => e.Fecha)
                    .ToList();
            }
            catch (Exception ex)
            {
                throw new Exception("Error al filtrar eventos por fechas.", ex);
            }
        }

        // Devuelve los últimos eventos para una vista rápida de actividad reciente.
        public List<BEEvento> ListarUltimos(int cantidad)
        {
            try
            {
                return ListarTodo()
                    .OrderByDescending(e => e.Fecha)
                    .Take(cantidad)
                    .ToList();
            }
            catch (Exception ex)
            {
                throw new Exception("Error al listar los últimos eventos.", ex);
            }
        }

        #endregion

        #region "Bitácoras agrupadas"

        // Bitácora del día: agrupa todos los eventos ocurridos dentro de una fecha calendario.
        public BEBitacora ObtenerBitacoraDelDia(DateTime fecha)
        {
            try
            {
                var desde = fecha.Date;
                var hasta = fecha.Date.AddDays(1).AddTicks(-1);

                return new BEBitacora
                {
                    FechaCreacion = DateTime.Now,
                    Eventos = ListarPorFechas(desde, hasta)
                };
            }
            catch (Exception ex)
            {
                throw new Exception("Error al armar la bitácora del día.", ex);
            }
        }

        // Bitácora de usuario: agrupa los eventos generados por una persona del sistema.
        public BEBitacora ObtenerBitacoraDeUsuario(BEUsuario usuario)
        {
            try
            {
                ValidadorBLL.ObjetoNoNulo(usuario, "Usuario");

                return new BEBitacora(usuario)
                {
                    FechaCreacion = DateTime.Now,
                    Eventos = ListarPorUsuario(usuario)
                };
            }
            catch (Exception ex)
            {
                throw new Exception("Error al armar la bitácora del usuario.", ex);
            }
        }

        // Bitácora por operación: agrupa eventos del mismo tipo, por ejemplo Backup o Restore.
        public BEBitacora ObtenerBitacoraPorOperacion(TipoOperacion tipo)
        {
            try
            {
                ValidadorBLL.ObjetoNoNulo(tipo, "Tipo de operación");

                return new BEBitacora
                {
                    FechaCreacion = DateTime.Now,
                    Eventos = ListarPorTipo(tipo)
                };
            }
            catch (Exception ex)
            {
                throw new Exception("Error al armar la bitácora por operación.", ex);
            }
        }

        #endregion
    }
}
