using mochi_closet.Datos;

namespace mochi_closet.Negocio;

public class GestionCategorias
{
    private static List<Categoria> _dbCategorias = new()
    {
        new() { Id = 1, Nombre = "Tops", Descripcion = "Blusas, camisetas y tops" },
        new() { Id = 2, Nombre = "Pantalones", Descripcion = "Jeans, leggings y pantalones" },
        new() { Id = 3, Nombre = "Vestidos", Descripcion = "Vestidos y faldas" },
        new() { Id = 4, Nombre = "Abrigos", Descripcion = "Chaquetas y abrigos" },
        new() { Id = 5, Nombre = "Accesorios", Descripcion = "Bolsos, cinturones y accesorios" }
    };
 
    public List<Categoria> ListaCategorias()
    {
        return _dbCategorias;
    }
 
    public Categoria? ObtenerCategoria(int id)
    {
        return _dbCategorias.FirstOrDefault(c => c.Id == id);
    }
 
    public void CrearCategoria(Categoria categoria)
    {
        categoria.Id = _dbCategorias.Count == 0 ? 1 : _dbCategorias.Max(c => c.Id) + 1;
        _dbCategorias.Add(categoria);
    }
 
    public void ActualizarCategoria(Categoria categoriaEditada)
    {
        var c = _dbCategorias.FirstOrDefault(c => c.Id == categoriaEditada.Id);
        if (c != null)
        {
            c.Nombre = categoriaEditada.Nombre;
            c.Descripcion = categoriaEditada.Descripcion;
        }
    }
 
    public void EliminarCategoria(int id)
    {
        var c = _dbCategorias.FirstOrDefault(c => c.Id == id);
        if (c != null)
            _dbCategorias.Remove(c);
    }
}