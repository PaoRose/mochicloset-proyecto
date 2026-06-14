using Microsoft.EntityFrameworkCore;
using mochi_closet.Data;
using mochi_closet.Datos;

namespace mochi_closet.Negocio;

public class GestionCompras
{
    private readonly MochiClosetDbContext _context;

    public GestionCompras(MochiClosetDbContext context)
    {
        _context = context;
    }

    public List<Compra> ListaCompras()
    {
        return _context.Compras
            .Include(c => c.Publicacion)
            .ToList();
    }

    public List<Compra> ListaComprasPorUsuaria(int usuarioId)
    {
        return _context.Compras
            .Where(c => c.UsuarioId == usuarioId)
            .Include(c => c.Publicacion)
            .ToList();
    }

    public List<Compra> ListaVentasPorUsuaria(int usuarioId)
    {
        var publicacionesDeUsuaria = _context.Publicaciones
            .Where(p => p.UsuarioId == usuarioId)
            .Select(p => p.Id)
            .ToList();

        return _context.Compras
            .Where(c => publicacionesDeUsuaria.Contains(c.PublicacionId))
            .Include(c => c.Publicacion)
            .ToList();
    }

    public Compra? ObtenerCompra(int id)
    {
        return _context.Compras
            .Include(c => c.Publicacion)
            .FirstOrDefault(c => c.Id == id);
    }

    public string RegistrarCompra(Compra compra)
    {
        var publicacion = _context.Publicaciones
            .FirstOrDefault(p => p.Id == compra.PublicacionId);

        if (publicacion == null)
            return "La publicación no existe";

        if (publicacion.Estado != "Disponible")
            return "Esta publicación ya fue vendida";

        var usuario = _context.Usuarios
            .FirstOrDefault(u => u.Id == compra.UsuarioId);

        if (usuario == null)
            return "La usuaria no existe";

        compra.FechaCompra = DateTime.Now;
        compra.Estado = "Completada";
        compra.MontoTotal = publicacion.Precio;

        _context.Compras.Add(compra);
        publicacion.Estado = "Vendido";
        _context.SaveChanges();

        return "ok";
    }
    public Compra? ObtenerCompraReciente(int usuarioId, int publicacionId)
    {
        return _context.Compras
            .Where(c => c.UsuarioId == usuarioId && c.PublicacionId == publicacionId)
            .OrderByDescending(c => c.FechaCompra)
            .FirstOrDefault();
    }
}