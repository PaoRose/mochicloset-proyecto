using Microsoft.AspNetCore.Mvc;
using mochi_closet.Datos;
using mochi_closet.Negocio;

namespace mochi_closet.Controllers;

[ApiController]
[Route("[controller]")]
public class GestionCategoriasController : ControllerBase
{
    private GestionCategorias _gestionCategorias;

    public GestionCategoriasController()
    {
        _gestionCategorias = new GestionCategorias();
    }

    [HttpGet("lista-categorias")]
    public IEnumerable<Categoria> ListaCategorias()
    {
        return _gestionCategorias.ListaCategorias();
    }

    [HttpGet("{id}")]
    public ActionResult<Categoria> ObtenerCategoria(int id)
    {
        var categoria = _gestionCategorias.ObtenerCategoria(id);

        if (categoria == null)
            return NotFound();

        return categoria;
    }

    [HttpPost]
    public ActionResult<Categoria> CrearCategoria(Categoria categoria)
    {
        _gestionCategorias.CrearCategoria(categoria);

        return Ok(categoria);
    }

    [HttpPut("{id}")]
    public IActionResult ActualizarCategoria(int id, Categoria categoriaEditada)
    {
        var categoria = _gestionCategorias.ObtenerCategoria(id);

        if (categoria == null)
            return NotFound();

        _gestionCategorias.ActualizarCategoria(categoriaEditada);

        return Ok(categoria);
    }

    [HttpDelete("{id}")]
    public IActionResult EliminarCategoria(int id)
    {
        var categoria = _gestionCategorias.ObtenerCategoria(id);

        if (categoria == null)
            return NotFound();

        _gestionCategorias.EliminarCategoria(id);

        return NoContent();
    }
}