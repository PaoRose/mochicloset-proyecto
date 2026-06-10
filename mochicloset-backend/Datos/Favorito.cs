namespace mochi_closet.Datos;

public class Favorito
{
    public int Id { get; set; }
    public int UsuarioId { get; set; }
    public int PublicacionId { get; set; }
    public DateTime FechaAgregado { get; set; }
}