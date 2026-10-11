using Abstraccion;

namespace BE
{
    // Clase base para entidades persistibles: todas tienen Id y estado Activo.
    public abstract class BEEntidad : IEntidad
    {
        public long Id { get; set; }
        public bool Activo { get; set; } = true;
    }
}
