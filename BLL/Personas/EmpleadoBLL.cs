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
    public class EmpleadoBLL : IGestor<BEEmpleado>
    {
        private EmpleadoMPP empleadoMPP = new EmpleadoMPP();

        #region "Constructor estático"

        static EmpleadoBLL()
        {
            ReconstructorBLL.Registrar<BEEmpleado>(
                id => new EmpleadoBLL().ListarObjeto(new BEEmpleado { Id = id })
            );
        }

        #endregion

        #region "Guardar"

        public void Guardar(BEEmpleado empleado)
        {
            try
            {
                ValidadorBLL.ObjetoNoNulo(empleado, "Empleado");
                ValidadorBLL.CampoObligatorio(empleado.Nombre, "Nombre");
                ValidadorBLL.CampoObligatorio(empleado.Apellido, "Apellido");
                ValidadorBLL.CampoObligatorio(empleado.Email, "Email");
                ValidadorBLL.ObjetoNoNulo(empleado.cargoEmpleado, "Cargo");

                if (empleado.Sueldo < 0)
                    throw new Exception("El sueldo no puede ser negativo.");

                bool dniDuplicado = empleadoMPP.Listar().Any(e =>
                    e.Activo &&
                    e.Id != empleado.Id &&
                    string.Equals(e.DNI, empleado.DNI, StringComparison.OrdinalIgnoreCase));

                if (dniDuplicado)
                    throw new Exception("Ya existe un empleado activo con el mismo DNI.");

                // La dirección se persiste antes porque el empleado conserva la referencia por Id.
                if (empleado.Direccion != null)
                {
                    var direccionBLL = new DireccionBLL();
                    direccionBLL.Guardar(empleado.Direccion);
                }

                if (empleado.Id == 0)
                    empleadoMPP.Alta(empleado);
                else
                    empleadoMPP.Modificar(empleado);

                // La cache se limpia para reconstruir el empleado con sus datos actualizados.
                ReconstructorBLL.LimpiarCache<BEEmpleado>();
            }
            catch (Exception ex)
            {
                throw new Exception("Error al guardar empleado.", ex);
            }
        }

        #endregion

        #region "Eliminar"

        public void Eliminar(BEEmpleado empleado)
        {
            try
            {
                ValidadorBLL.ObjetoNoNulo(empleado, "Empleado");
                empleadoMPP.Baja(empleado);
                ReconstructorBLL.LimpiarCache<BEEmpleado>();
            }
            catch (Exception ex)
            {
                throw new Exception("Error al eliminar empleado.", ex);
            }
        }

        #endregion

        #region "Listar / Buscar"

        public List<BEEmpleado> ListarTodo()
        {
            try
            {
                var lista = empleadoMPP.Listar();

                foreach (var emp in lista)
                {
                    if (emp.Direccion != null && emp.Direccion.Id > 0)
                    {
                        // En XML la dirección queda por Id; la BLL reconstruye el objeto completo.
                        var dir = ReconstructorBLL.Reconstruir<BEDireccion>(emp.Direccion.Id);
                        if (dir != null)
                            emp.Direccion = dir;
                    }
                }

                return lista;
            }
            catch (Exception ex)
            {
                throw new Exception("Error al listar empleados.", ex);
            }
        }

        public List<BEEmpleado> ListarActivos()
        {
            try
            {
                var lista = empleadoMPP.Listar();

                foreach (var emp in lista)
                {
                    if (emp.Direccion != null && emp.Direccion.Id > 0)
                    {
                        // En XML la dirección queda por Id; la BLL reconstruye el objeto completo.
                        var dir = ReconstructorBLL.Reconstruir<BEDireccion>(emp.Direccion.Id);
                        if (dir != null)
                            emp.Direccion = dir;
                    }
                }

                return lista.Where(e => e.Activo).ToList();
            }
            catch (Exception ex)
            {
                throw new Exception("Error al listar empleados activos.", ex);
            }
        }

        public BEEmpleado ListarObjeto(BEEmpleado empleado)
        {
            try
            {
                ValidadorBLL.ObjetoNoNulo(empleado, "Empleado");

                var encontrado = empleadoMPP.BuscarPorId(empleado.Id);

                if (encontrado != null && encontrado.Direccion != null)
                {
                    // Se reconstruye la dirección para devolver el empleado completo.
                    var dir = ReconstructorBLL.Reconstruir<BEDireccion>(encontrado.Direccion.Id);
                    if (dir != null)
                        encontrado.Direccion = dir;
                }

                return encontrado;
            }
            catch (Exception ex)
            {
                throw new Exception("Error al buscar empleado.", ex);
            }
        }

        #endregion

        #region "Utilitarios"

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
