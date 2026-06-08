using mochi_closet.Data;
using mochi_closet.Datos;

namespace mochi_closet.Negocio;

public class GestionPublicaciones
{
    private readonly MochiClosetDbContext _context;

    public GestionPublicaciones(MochiClosetDbContext context)
    {
        _context = context;
    }

    public List<Publicacion> ListaPublicaciones()
    {
        return _context.Publicaciones.ToList();
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

    public void EliminarPublicacion(int id)
    {
        var publicacion = _context.Publicaciones
            .FirstOrDefault(p => p.Id == id);

        if (publicacion != null)
        {
            _context.Publicaciones.Remove(publicacion);

            _context.SaveChanges();
        }
    }
}