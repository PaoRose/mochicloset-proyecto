using mochi_closet.Datos;

namespace mochi_closet.Negocio;

public class GestionPublicaciones
{
      private static List<Publicacion> _dbPublicaciones = new();
 
    public List<Publicacion> ListaPublicaciones()
    {
        return _dbPublicaciones;
    }
 
    public Publicacion? ObtenerPublicacion(int id)
    {
        return _dbPublicaciones.FirstOrDefault(p => p.Id == id);
    }
 
    public string CrearPublicacion(Publicacion publicacion)
    {
        if (string.IsNullOrWhiteSpace(publicacion.Titulo))
            return "El título es obligatorio";
 
        if (publicacion.CategoriaId <= 0)
            return "La categoría es obligatoria";
 
        if (string.IsNullOrWhiteSpace(publicacion.Talla))
            return "La talla es obligatoria";
 
        if (string.IsNullOrWhiteSpace(publicacion.Condicion))
            return "La condición es obligatoria";
 
        if (publicacion.Precio <= 0)
            return "El precio debe ser mayor a cero";
 
        if (string.IsNullOrWhiteSpace(publicacion.ImagenUrl))
            return "Debe incluir al menos una fotografía";
 
        publicacion.Id = _dbPublicaciones.Count == 0 ? 1 : _dbPublicaciones.Max(p => p.Id) + 1;
        publicacion.Estado = "Disponible";
        publicacion.FechaPublicacion = DateTime.Now;
        _dbPublicaciones.Add(publicacion);
 
        return "ok";
    }
 
    public void ActualizarPublicacion(Publicacion publicacionEditada)
    {
        var p = _dbPublicaciones.FirstOrDefault(p => p.Id == publicacionEditada.Id);
        if (p != null)
        {
            p.Titulo = publicacionEditada.Titulo;
            p.Descripcion = publicacionEditada.Descripcion;
            p.Precio = publicacionEditada.Precio;
            p.Talla = publicacionEditada.Talla;
            p.Condicion = publicacionEditada.Condicion;
            p.ImagenUrl = publicacionEditada.ImagenUrl;
            p.CategoriaId = publicacionEditada.CategoriaId;
        }
    }
 
    public void EliminarPublicacion(int id)
    {
        var p = _dbPublicaciones.FirstOrDefault(p => p.Id == id);
        if (p != null)
            _dbPublicaciones.Remove(p);
    }  
}