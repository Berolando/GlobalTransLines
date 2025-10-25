using System;
using System.Collections.Generic;
using System.Linq;
using Abstraccion;
using BE;
using BE.Transversal;
using MPP.Personas;
using BLL.Transversal;

namespace BLL.Personas
{
    public class EmpleadoBLL : IGestor<BEEmpleado>
    {
        private EmpleadoMPP empleadoMPP = new EmpleadoMPP();

        #region "Constructor estático"

        // Registra cómo reconstruir un empleado por Id
        static EmpleadoBLL()
        {
            ReconstructorBLL.Registrar<BEEmpleado>(
                id => new EmpleadoBLL().ListarObjeto(new BEEmpleado { Id = id })
            );
        }

        #endregion

        #region "Guardar"

        // Valida y guarda (alta o modificación)
        public void Guardar(BEEmpleado empleado)
        {
            try
            {
                ValidadorBLL.ObjetoNoNulo(empleado, "Empleado");
                ValidadorBLL.CampoObligatorio(empleado.Nombre, "Nombre");
                ValidadorBLL.CampoObligatorio(empleado.Apellido, "Apellido");
                ValidadorBLL.CampoObligatorio(empleado.Email, "Email");

                if (empleado.Id == 0)
                    empleadoMPP.Alta(empleado);
                else
                    empleadoMPP.Modificar(empleado);

                // Limpia cache de empleados
                ReconstructorBLL.LimpiarCache<BEEmpleado>();
            }
            catch (Exception ex)
            {
                throw new Exception("Error al guardar empleado.", ex);
            }
        }

        #endregion

        #region "Eliminar"

        // Elimina empleado
        public void Eliminar(BEEmpleado empleado)
        {
            try
            {
                ValidadorBLL.ObjetoNoNulo(empleado, "Empleado");
                empleadoMPP.Baja(empleado);

                // Limpia cache
                ReconstructorBLL.LimpiarCache<BEEmpleado>();
            }
            catch (Exception ex)
            {
                throw new Exception("Error al eliminar empleado.", ex);
            }
        }

        #endregion

        #region "Listar / Buscar"

        // Lista todos los empleados y reconstruye direcciones
        public List<BEEmpleado> ListarTodo()
        {
            try
            {
                var lista = empleadoMPP.Listar();

                foreach (var emp in lista)
                {
                    if (emp.Direccion != null && emp.Direccion.Id > 0)
                        emp.Direccion = ReconstructorBLL.Reconstruir<BEDireccion>(emp.Direccion.Id);
                }

                return lista;
            }
            catch (Exception ex)
            {
                throw new Exception("Error al listar empleados.", ex);
            }
        }

        // Busca un empleado por Id
        public BEEmpleado ListarObjeto(BEEmpleado empleado)
        {
            try
            {
                ValidadorBLL.ObjetoNoNulo(empleado, "Empleado");

                var encontrado = empleadoMPP.BuscarPorId(empleado.Id);

                if (encontrado != null && encontrado.Direccion != null)
                    encontrado.Direccion = ReconstructorBLL.Reconstruir<BEDireccion>(encontrado.Direccion.Id);

                return encontrado;
            }
            catch (Exception ex)
            {
                throw new Exception("Error al buscar empleado.", ex);
            }
        }

        #endregion

        #region "Utilitarios"

        // Devuelve el último Id registrado
        public long ObtenerUltimoId()
        {
            try
            {
                return empleadoMPP.ObtenerUltimoId();
            }
            catch (Exception ex)
            {
                throw new Exception("Error al obtener último Id de empleado.", ex);
            }
        }

        #endregion
    }
}
