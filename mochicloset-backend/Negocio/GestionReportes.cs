using Microsoft.EntityFrameworkCore;
using mochi_closet.Data;
using mochi_closet.Datos;

namespace mochi_closet.Negocio;

public class GestionReportes
{
    private readonly MochiClosetDbContext _context;

    public GestionReportes(MochiClosetDbContext context)
    {
        _context = context;
    }

    public List<Reporte> ListaReportes()
    {
        return _context.Reportes
            .Include(r => r.Reportante)
            .Include(r => r.Publicacion)
            .Include(r => r.Reportada)
            .OrderBy(r => r.Estado == "Resuelto" ? 1 : 0)
            .ThenByDescending(r => r.FechaReporte)
            .ToList();
    }

    public string CrearReporte(Reporte reporte)
    {
        Reporte nuevoReporte;

        if (reporte.PublicacionId.HasValue)
            nuevoReporte = new ReportePrenda();
        else
            nuevoReporte = new ReporteUsuaria();

        nuevoReporte.ReportanteId = reporte.ReportanteId;
        nuevoReporte.PublicacionId = reporte.PublicacionId;
        nuevoReporte.ReportadaId = reporte.ReportadaId;
        nuevoReporte.Razon = reporte.Razon;
        nuevoReporte.TituloPublicacion = reporte.TituloPublicacion;
        nuevoReporte.Estado = "Pendiente";
        nuevoReporte.FechaReporte = DateTime.Now;

        _context.Reportes.Add(nuevoReporte);
        _context.SaveChanges();
        return "ok";
    }

    public string ResolverReporte(int id)
    {
        var reporte = _context.Reportes.FirstOrDefault(r => r.Id == id);
        if (reporte == null) return "Reporte no encontrado";
        reporte.Estado = "Resuelto";
        _context.SaveChanges();
        return "ok";
    }
}