namespace mochi_closet.Datos;

public class Reporte
{
    public int Id { get; set; }
    public int ReportanteId { get; set; }
    public int? PublicacionId { get; set; }
    public int? ReportadaId { get; set; }
    public string? Razon { get; set; }
    public string? Estado { get; set; }
    public DateTime FechaReporte { get; set; }

    public Usuario? Reportante { get; set; }
    public Publicacion? Publicacion { get; set; }
    public Usuario? Reportada { get; set; }

    public virtual string ObtenerTipo()
    {
        return "Reporte";
    }
}