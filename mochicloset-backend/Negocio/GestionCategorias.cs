using mochi_closet.Data;
using mochi_closet.Datos;

namespace mochi_closet.Negocio;

public class GestionCategorias
{
    private readonly MochiClosetDbContext _context;

    public GestionCategorias(MochiClosetDbContext context)
    {
        _context = context;
    }

    public List<Categoria> ListaCategorias()
    {
        return _context.Categorias.ToList();
    }

    public Categoria? ObtenerCategoria(int id)
    {
        return _context.Categorias
            .FirstOrDefault(c => c.Id == id);
    }

    public void CrearCategoria(Categoria categoria)
    {
        _context.Categorias.Add(categoria);
        _context.SaveChanges();
    }

    public void ActualizarCategoria(Categoria categoriaEditada)
    {
        var categoria = _context.Categorias
            .FirstOrDefault(c => c.Id == categoriaEditada.Id);

        if (categoria != null)
        {
            categoria.Nombre = categoriaEditada.Nombre;
            categoria.Descripcion = categoriaEditada.Descripcion;

            _context.SaveChanges();
        }
    }

    public void EliminarCategoria(int id)
    {
        var categoria = _context.Categorias
            .FirstOrDefault(c => c.Id == id);

        if (categoria != null)
        {
            _context.Categorias.Remove(categoria);
            _context.SaveChanges();
        }
    }
}