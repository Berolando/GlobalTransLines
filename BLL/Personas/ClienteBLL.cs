using Abstraccion;
using BE.Personas;
using BE.Infra;
using BLL.Infra;
using MPP.Personas;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BLL.Personas
{
    public class ClienteBLL : IGestor<BECliente>
    {
        private ClienteMPP clienteMPP = new ClienteMPP();

        #region "Constructor estático"

        static ClienteBLL()
        {
            ReconstructorBLL.Registrar<BECliente>(
                id => new ClienteBLL().ListarObjeto(new BECliente { Id = id })
            );
        }

        #endregion

        #region "Guardar"

        public void Guardar(BECliente cliente)
        {
            try
            {
                ValidadorBLL.ObjetoNoNulo(cliente, "Cliente");
                ValidadorBLL.CampoObligatorio(cliente.Nombre, "Nombre");
                ValidadorBLL.CampoObligatorio(cliente.Apellido, "Apellido");
                ValidadorBLL.CampoObligatorio(cliente.RazonSocial, "Razón Social");
                ValidadorBLL.CampoObligatorio(cliente.Cuit, "CUIT");

                var lista = clienteMPP.Listar();
                bool duplicado = lista.Any(c =>
                    c.Cuit.Equals(cliente.Cuit, StringComparison.OrdinalIgnoreCase)
                    && c.Id != cliente.Id);

                if (duplicado)
                    throw new Exception("Ya existe un cliente con el mismo CUIT.");

                // La dirección se guarda primero porque el cliente persiste la relación por Id.
                if (cliente.Direccion != null)
                {
                    var direccionBLL = new DireccionBLL();
                    direccionBLL.Guardar(cliente.Direccion);
                }

                if (cliente.Id == 0)
                    clienteMPP.Alta(cliente);
                else
                    clienteMPP.Modificar(cliente);

                // Se limpia la cache para que futuras reconstrucciones tomen los datos actualizados.
                ReconstructorBLL.LimpiarCache<BECliente>();
            }
            catch (Exception ex)
            {
                throw new Exception("Error al guardar el cliente.", ex);
            }
        }

        #endregion

        #region "Eliminar"

        public void Eliminar(BECliente cliente)
        {
            try
            {
                ValidadorBLL.ObjetoNoNulo(cliente, "Cliente");
                clienteMPP.Baja(cliente);

                // La baja lógica debe reflejarse en las próximas consultas reconstruidas.
                ReconstructorBLL.LimpiarCache<BECliente>();
            }
            catch (Exception ex)
            {
                throw new Exception("Error al eliminar el cliente.", ex);
            }
        }

        #endregion

        #region "Listar / Buscar"

        // En XML se guarda la dirección por Id; la BLL reconstruye el cliente completo.
        public List<BECliente> ListarTodo()
        {
            try
            {
                var lista = clienteMPP.Listar();

                foreach (var cli in lista)
                {
                    if (cli.Direccion != null && cli.Direccion.Id > 0)
                    {
                        var direccionReconstruida = ReconstructorBLL.Reconstruir<BEDireccion>(cli.Direccion.Id);
                        if (direccionReconstruida != null)
                            cli.Direccion = direccionReconstruida;
                    }
                }

                return lista;
            }
            catch (Exception ex)
            {
                throw new Exception("Error al listar clientes.", ex);
            }
        }

        public List<BECliente> ListarActivos()
        {
            try
            {
                var lista = clienteMPP.Listar();

                foreach (var cli in lista)
                {
                    if (cli.Direccion != null && cli.Direccion.Id > 0)
                    {
                        var direccionReconstruida = ReconstructorBLL.Reconstruir<BEDireccion>(cli.Direccion.Id);
                        if (direccionReconstruida != null)
                            cli.Direccion = direccionReconstruida;
                    }
                }

                return lista.Where(c => c.Activo).ToList();
            }

            catch (Exception ex)
            {
                throw new Exception("Error al listar clientes activos.", ex);
            }
        }

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
