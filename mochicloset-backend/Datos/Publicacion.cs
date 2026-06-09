namespace mochi_closet.Datos;

public class Publicacion
{
    public int Id { get; set; }
    public string? Titulo { get; set; }
    public string? Descripcion { get; set; }
    public decimal Precio { get; set; }
    public string? Talla { get; set; }
    public string? Condicion { get; set; }
    public string? Estado { get; set; }
    public DateTime FechaPublicacion { get; set; }
    public int UsuarioId { get; set; }
    public int CategoriaId { get; set; }
    public string? ImagenUrl { get; set; }
}