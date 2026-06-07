using Microsoft.AspNetCore.Mvc;
using mochi_closet.Datos;
using mochi_closet.Negocio;

namespace mochi_closet.Controllers;

[ApiController]
[Route("[controller]")]
public class GestionPublicacionesController : ControllerBase
{
    private GestionPublicaciones _gestionPublicaciones;

    public GestionPublicacionesController()
    {
        _gestionPublicaciones = new GestionPublicaciones();
    }

    [HttpGet("lista-publicaciones")]
    public IEnumerable<Publicacion> ListaPublicaciones()
    {
        return _gestionPublicaciones.ListaPublicaciones();
    }

    [HttpGet("{id}")]
    public ActionResult<Publicacion> ObtenerPublicacion(int id)
    {
        var publicacion = _gestionPublicaciones.ObtenerPublicacion(id);

        if (publicacion == null)
            return NotFound();

        return publicacion;
    }

    [HttpPost]
    public ActionResult<string> CrearPublicacion(Publicacion publicacion)
    {
        var resultado = _gestionPublicaciones.CrearPublicacion(publicacion);

        if (resultado != "ok")
            return BadRequest(resultado);

        return Ok(publicacion);
    }

    [HttpPut("{id}")]
    public IActionResult ActualizarPublicacion(int id, Publicacion publicacionEditada)
    {
        var publicacion = _gestionPublicaciones.ObtenerPublicacion(id);

        if (publicacion == null)
            return NotFound();

        _gestionPublicaciones.ActualizarPublicacion(publicacionEditada);

        return Ok(publicacion);
    }

    [HttpDelete("{id}")]
    public IActionResult EliminarPublicacion(int id)
    {
        var publicacion = _gestionPublicaciones.ObtenerPublicacion(id);

        if (publicacion == null)
            return NotFound();

        _gestionPublicaciones.EliminarPublicacion(id);

        return NoContent();
    }
}