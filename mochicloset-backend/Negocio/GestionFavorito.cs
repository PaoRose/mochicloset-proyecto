using mochi_closet.Data;
using mochi_closet.Datos;

namespace mochi_closet.Negocio;

public class GestionFavoritos
{
    private readonly MochiClosetDbContext _context;

    public GestionFavoritos(MochiClosetDbContext context)
    {
        _context = context;
    }

    public List<Favorito> ListaFavoritosPorUsuaria(int usuarioId)
    {
        return _context.Favoritos
            .Where(f => f.UsuarioId == usuarioId)
            .ToList();
    }

    public string AgregarFavorito(Favorito favorito)
    {
        var usuario = _context.Usuarios
            .FirstOrDefault(u => u.Id == favorito.UsuarioId);

        if (usuario == null)
            return "La usuaria no existe";

        var publicacion = _context.Publicaciones
            .FirstOrDefault(p => p.Id == favorito.PublicacionId);

        if (publicacion == null)
            return "La publicación no existe";

        var yaExiste = _context.Favoritos.Any(f =>
            f.UsuarioId == favorito.UsuarioId &&
            f.PublicacionId == favorito.PublicacionId);

        if (yaExiste)
            return "Esta publicación ya está en favoritos";

        favorito.FechaAgregado = DateTime.Now;

        _context.Favoritos.Add(favorito);
        _context.SaveChanges();

        return "ok";
    }

    public void EliminarFavorito(int usuarioId, int publicacionId)
    {
        var favorito = _context.Favoritos
            .FirstOrDefault(f =>
                f.UsuarioId == usuarioId &&
                f.PublicacionId == publicacionId);

        if (favorito != null)
        {
            _context.Favoritos.Remove(favorito);
            _context.SaveChanges();
        }
    }
}