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
            .ToList();
    }

    public string CrearReporte(Reporte reporte)
    {
        reporte.Estado = "Pendiente";
        reporte.FechaReporte = DateTime.Now;
        _context.Reportes.Add(reporte);
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