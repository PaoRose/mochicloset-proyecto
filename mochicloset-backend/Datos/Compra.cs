namespace mochi_closet.Datos;

public class Compra
{
    public int Id { get; set; }
    public DateTime FechaCompra { get; set; }
    public string? MetodoPago { get; set; }
    public string? DireccionEntrega { get; set; }
    public string? Estado { get; set; }
    public int UsuarioId { get; set; }
    public int PublicacionId { get; set; }
    public decimal MontoTotal { get; set; }
}