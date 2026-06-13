namespace mochi_closet.Datos;

public class Conversacion
{
    public int Id { get; set; }
    public int CompradoraId { get; set; }
    public int VendedoraId { get; set; }
    public int PublicacionId { get; set; }
    public DateTime FechaCreacion { get; set; }
    public Publicacion? Publicacion { get; set; }
}