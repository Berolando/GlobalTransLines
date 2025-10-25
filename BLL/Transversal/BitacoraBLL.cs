using System;
using System.Collections.Generic;
using Abstraccion;
using BE.Transversal;
using BE.Enums;
using MPP.Transversal;

namespace BLL.Transversal
{
    public class BitacoraBLL : IGestor<BEBitacora>
    {
        private BitacoraMPP bitacoraMPP = new BitacoraMPP();

        #region "Registrar evento"

        public void RegistrarEvento(BEUsuario usuario, TipoOperacionEnum tipo, string descripcion, string origen)
        {
            try
            {
                ValidadorBLL.ObjetoNoNulo(usuario, "Usuario");
                ValidadorBLL.CampoObligatorio(descripcion, "Descripción del evento");
                ValidadorBLL.CampoObligatorio(origen, "Origen del evento");

                // Crear evento con los datos actuales
                var evento = new BEEvento(DateTime.Now, usuario, descripcion, tipo, origen);

                // Crear bitácora y agregar el evento
                var bitacora = new BEBitacora(usuario);
                bitacora.Eventos.Add(evento);

                bitacoraMPP.Alta(bitacora);
            }
            catch (Exception ex)
            {
                throw new Exception("Error al registrar el evento en la bitácora.", ex);
            }
        }

        #endregion

        #region "Listar / Eliminar / Utilitarios"  // (igual que antes, sin cambios)

        public List<BEBitacora> ListarTodo()
        {
            try { return bitacoraMPP.Listar(); }
            catch (Exception ex) { throw new Exception("Error al listar bitácoras.", ex); }
        }

        public BEBitacora ListarObjeto(BEBitacora bitacora)
        {
            try
            {
                ValidadorBLL.ObjetoNoNulo(bitacora, "Bitácora");
                return bitacoraMPP.BuscarPorId(bitacora.Id);
            }
            catch (Exception ex)
            {
                throw new Exception("Error al buscar bitácora.", ex);
            }
        }

        public void Eliminar(BEBitacora bitacora)
        {
            try
            {
                ValidadorBLL.ObjetoNoNulo(bitacora, "Bitácora");
                bitacoraMPP.Baja(bitacora);
            }
            catch (Exception ex)
            {
                throw new Exception("Error al eliminar bitácora.", ex);
            }
        }

        public long ObtenerUltimoId()
        {
            try { return bitacoraMPP.ObtenerUltimoId(); }
            catch (Exception ex) { throw new Exception("Error al obtener último Id de bitácora.", ex); }
        }

        public void Guardar(BEBitacora bitacora)
        {
            try
            {
                ValidadorBLL.ObjetoNoNulo(bitacora, "Bitácora");
                bitacoraMPP.Alta(bitacora);
            }
            catch (Exception ex)
            {
                throw new Exception("Error al guardar bitácora.", ex);
            }
        }

        #endregion
    }
}
