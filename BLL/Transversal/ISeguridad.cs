using BE.Transversal;

namespace BLL.Transversal
{
    public interface ISeguridad
    {
        // Inicia sesión y devuelve el usuario autenticado
        BEUsuario Login(string username, string password);

        // Cierra la sesión actual
        void Logout();

        // Verifica si el usuario autenticado tiene un permiso específico
        bool TienePermiso(string accion);

        // Indica si actualmente hay un usuario autenticado
        bool EstaAutenticado { get; }

        // Devuelve el nombre del usuario autenticado (solo lectura)
        string NombreUsuario { get; }
    }
}
