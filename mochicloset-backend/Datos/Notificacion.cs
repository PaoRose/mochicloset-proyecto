namespace mochi_closet.Datos;

public class Notificacion
{
    public int Id { get; set; }
    public int UsuarioId { get; set; }
    public string? Tipo { get; set; }
    public string? Mensaje { get; set; }
    public bool Leida { get; set; }
    public DateTime FechaCreacion { get; set; }

    public virtual string ObtenerIcono()
    {
        return "🔔";
    }

    public virtual string ObtenerDescripcion()
    {
        return Mensaje ?? "Notificación";
    }
}