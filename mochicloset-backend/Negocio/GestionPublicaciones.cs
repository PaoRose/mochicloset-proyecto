using mochi_closet.Data;
using mochi_closet.Datos;

namespace mochi_closet.Negocio;

public class GestionPublicaciones
{
    private readonly MochiClosetDbContext _context;
    private readonly GestionNotificaciones _gestionNotificaciones;

    public GestionPublicaciones(
        MochiClosetDbContext context,
        GestionNotificaciones gestionNotificaciones)
    {
        _context = context;
        _gestionNotificaciones = gestionNotificaciones;
    }

    public List<Publicacion> ListaPublicaciones()
    {
        return _context.Publicaciones.ToList();
    }
    
    public List<Publicacion> ListaPublicacionesPorUsuaria(int usuarioId)
    {
        return _context.Publicaciones
            .Where(p => p.UsuarioId == usuarioId)
            .ToList();
    }
    
    public Publicacion? ObtenerPublicacion(int id)
    {
        return _context.Publicaciones
            .FirstOrDefault(p => p.Id == id);
    }

    public string CrearPublicacion(Publicacion publicacion)
    {
        if (string.IsNullOrWhiteSpace(publicacion.Titulo))
            return "El título es obligatorio";

        if (publicacion.CategoriaId <= 0)
            return "La categoría es obligatoria";

        if (publicacion.UsuarioId <= 0)
            return "La usuaria es obligatoria";

        if (string.IsNullOrWhiteSpace(publicacion.Talla))
            return "La talla es obligatoria";

        if (string.IsNullOrWhiteSpace(publicacion.Condicion))
            return "La condición es obligatoria";

        if (publicacion.Precio <= 0)
            return "El precio debe ser mayor a cero";

        if (string.IsNullOrWhiteSpace(publicacion.ImagenUrl))
            return "Debe incluir al menos una fotografía";

        var usuario = _context.Usuarios
            .FirstOrDefault(u => u.Id == publicacion.UsuarioId);

        if (usuario == null)
            return "La usuaria no existe";

        var categoria = _context.Categorias
            .FirstOrDefault(c => c.Id == publicacion.CategoriaId);

        if (categoria == null)
            return "La categoría no existe";

        publicacion.Estado = "Disponible";
        publicacion.FechaPublicacion = DateTime.Now;

        _context.Publicaciones.Add(publicacion);

        _context.SaveChanges();

        return "ok";
    }

    public void ActualizarPublicacion(Publicacion publicacionEditada)
    {
        var publicacion = _context.Publicaciones
            .FirstOrDefault(p => p.Id == publicacionEditada.Id);

        if (publicacion != null)
        {
            publicacion.Titulo = publicacionEditada.Titulo;
            publicacion.Descripcion = publicacionEditada.Descripcion;
            publicacion.Precio = publicacionEditada.Precio;
            publicacion.Talla = publicacionEditada.Talla;
            publicacion.Condicion = publicacionEditada.Condicion;
            publicacion.ImagenUrl = publicacionEditada.ImagenUrl;
            publicacion.CategoriaId = publicacionEditada.CategoriaId;

            _context.SaveChanges();
        }
    }
    
    public List<Publicacion> FiltrarPublicaciones(
        string? busqueda,
        int? categoriaId,
        string? talla,
        string? condicion,
        decimal? precioMin,
        decimal? precioMax)
    {
        var query = _context.Publicaciones
            .Where(p => p.Estado == "Disponible")
            .Where(p => _context.Usuarios
                .Any(u => u.Id == p.UsuarioId && u.Estado == "Activo"))
            .AsQueryable();

        if (!string.IsNullOrWhiteSpace(busqueda))
            query = query.Where(p => p.Titulo!.Contains(busqueda));

        if (categoriaId.HasValue)
            query = query.Where(p => p.CategoriaId == categoriaId.Value);

        if (!string.IsNullOrWhiteSpace(talla))
            query = query.Where(p => p.Talla == talla);

        if (!string.IsNullOrWhiteSpace(condicion))
            query = query.Where(p => p.Condicion == condicion);

        if (precioMin.HasValue)
            query = query.Where(p => p.Precio >= precioMin.Value);

        if (precioMax.HasValue)
            query = query.Where(p => p.Precio <= precioMax.Value);

        return query.ToList();
    }
    
    //ADMIN
    public string EliminarPublicacion(int adminId, int id, string razon)
    {
        var admin = _context.Usuarios
            .FirstOrDefault(u => u.Id == adminId);

        if (admin == null || admin.Rol != "Admin")
            return "No tienes permisos para realizar esta accion";

        var publicacion = _context.Publicaciones
            .FirstOrDefault(p => p.Id == id);

        if (publicacion == null)
            return "Publicacion no encontrada";

        _gestionNotificaciones.CrearNotificacion(
            publicacion.UsuarioId,
            "PrendaEliminada",
            "Tu publicacion fue eliminada por el administrador. Razon: " + razon);

        _context.Publicaciones.Remove(publicacion);
        _context.SaveChanges();

        return "ok";
    }
    //USUARIA
    public string EliminarPublicacionPropia(int usuarioId, int id)
    {
        var usuario = _context.Usuarios
            .FirstOrDefault(u => u.Id == usuarioId);

        if (usuario == null || usuario.Rol != "Usuaria")
            return "No tienes permisos para realizar esta accion";

        var publicacion = _context.Publicaciones
            .FirstOrDefault(p => p.Id == id);

        if (publicacion == null)
            return "Publicacion no encontrada";

        if (publicacion.UsuarioId != usuarioId)
            return "No puedes eliminar una publicacion que no es tuya";

        _context.Publicaciones.Remove(publicacion);
        _context.SaveChanges();

        return "ok";
    }
}