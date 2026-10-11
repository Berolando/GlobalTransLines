namespace Abstraccion
{
    // Contrato base para que todas las entidades tengan un identificador común.
    public interface IEntidad
    {
        long Id { get; set; }
    }
}
