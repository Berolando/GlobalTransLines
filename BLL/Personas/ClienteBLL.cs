using System;
using System.Collections.Generic;
using System.Linq;
using Abstraccion;
using BE;
using BE.Transversal;
using BE.Enums;
using MPP.Personas;
using BLL.Transversal;

namespace BLL.Personas
{
    public class ClienteBLL : IGestor<BECliente>
    {
        private ClienteMPP clienteMPP = new ClienteMPP();

        #region "Constructor estático"

        // Registra cómo reconstruir un cliente por Id
        static ClienteBLL()
        {
            ReconstructorBLL.Registrar<BECliente>(
                id => new ClienteBLL().ListarObjeto(new BECliente { Id = id })
            );
        }

        #endregion

        #region "Guardar"

        // Valida y guarda (alta o modificación)
        public void Guardar(BECliente cliente)
        {
            try
            { ValidadorBLL.ObjetoNoNulo(cliente, "Cliente");
        ValidadorBLL.CampoObligatorio(cliente.Nombre, "Nombre");
        ValidadorBLL.CampoObligatorio(cliente.RazonSocial, "Razón Social");
        ValidadorBLL.CampoObligatorio(cliente.Cuit, "CUIT");

        var lista = clienteMPP.Listar();
        bool duplicado = lista.Any(c =>
            c.Cuit.Equals(cliente.Cuit, StringComparison.OrdinalIgnoreCase)
            && c.Id != cliente.Id);

        if (duplicado)
            throw new Exception("Ya existe un cliente con el mismo CUIT.");

        // ✅ Guardar o actualizar dirección asociada
        if (cliente.Direccion != null)
        {
            var direccionBLL = new DireccionBLL();
            direccionBLL.Guardar(cliente.Direccion);
        }

        // Guardar cliente
        if (cliente.Id == 0)
            clienteMPP.Alta(cliente);
        else
            clienteMPP.Modificar(cliente);

        // Limpia cache de clientes
        ReconstructorBLL.LimpiarCache<BECliente>();
    }
    catch (Exception ex)
    {
        throw new Exception("Error al guardar el cliente.", ex);
    }
        }

        #endregion

        #region "Eliminar"

        // Elimina cliente
        public void Eliminar(BECliente cliente)
        {
            try
            {
                ValidadorBLL.ObjetoNoNulo(cliente, "Cliente");
                clienteMPP.Baja(cliente);

                // Limpia cache
                ReconstructorBLL.LimpiarCache<BECliente>();
            }
            catch (Exception ex)
            {
                throw new Exception("Error al eliminar el cliente.", ex);
            }
        }

        #endregion

        #region "Listar / Buscar"

        // Lista todos los clientes y reconstruye direcciones
        public List<BECliente> ListarTodo()
        {
            try
            {
                var lista = clienteMPP.Listar();

                foreach (var cli in lista)
                {
                    if (cli.Direccion != null && cli.Direccion.Id > 0)
                        cli.Direccion = ReconstructorBLL.Reconstruir<BEDireccion>(cli.Direccion.Id);
                }

                return lista;
            }
            catch (Exception ex)
            {
                throw new Exception("Error al listar clientes.", ex);
            }
        }

        // Busca un cliente por Id
        public BECliente ListarObjeto(BECliente cliente)
        {
            try
            {
                ValidadorBLL.ObjetoNoNulo(cliente, "Cliente");

                var encontrado = clienteMPP.BuscarPorId(cliente.Id);

                if (encontrado != null && encontrado.Direccion != null)
                    encontrado.Direccion = ReconstructorBLL.Reconstruir<BEDireccion>(encontrado.Direccion.Id);

                return encontrado;
            }
            catch (Exception ex)
            {
                throw new Exception("Error al buscar cliente.", ex);
            }
        }

        #endregion

        #region "Utilitarios"

        // Devuelve el último Id registrado
        public long ObtenerUltimoId()
        {
            try
            {
                return clienteMPP.ObtenerUltimoId();
            }
            catch (Exception ex)
            {
                throw new Exception("Error al obtener último Id de cliente.", ex);
            }
        }

        #endregion
    }
}
