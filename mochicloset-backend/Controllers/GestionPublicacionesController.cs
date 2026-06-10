using Microsoft.AspNetCore.Mvc;
using mochi_closet.Datos;
using mochi_closet.Negocio;

namespace mochi_closet.Controllers;

[ApiController]
[Route("[controller]")]
public class GestionPublicacionesController : ControllerBase
{
    private readonly GestionPublicaciones _gestionPublicaciones;

    public GestionPublicacionesController(
        GestionPublicaciones gestionPublicaciones)
    {
        _gestionPublicaciones = gestionPublicaciones;
    }

    [HttpGet("lista-publicaciones")]
    public IEnumerable<Publicacion> ListaPublicaciones()
    {
        return _gestionPublicaciones.ListaPublicaciones();
    }
    
    [HttpGet("filtrar")]
    public IEnumerable<Publicacion> FiltrarPublicaciones(
        [FromQuery] string? busqueda,
        [FromQuery] int? categoriaId,
        [FromQuery] string? talla,
        [FromQuery] string? condicion,
        [FromQuery] decimal? precioMin,
        [FromQuery] decimal? precioMax)
    {
        return _gestionPublicaciones.FiltrarPublicaciones(
            busqueda, categoriaId, talla, condicion, precioMin, precioMax);
    }
    
    [HttpGet("mis-publicaciones/{usuarioId}")]
    public IEnumerable<Publicacion> ListaPublicacionesPorUsuaria(int usuarioId)
    {
        return _gestionPublicaciones.ListaPublicacionesPorUsuaria(usuarioId);
    }

    [HttpGet("{id}")]
    public ActionResult<Publicacion> ObtenerPublicacion(int id)
    {
        var publicacion = _gestionPublicaciones.ObtenerPublicacion(id);

        if (publicacion == null)
            return NotFound();

        return Ok(publicacion);
    }

    [HttpPost]
    public ActionResult<string> CrearPublicacion(Publicacion publicacion)
    {
        var resultado = _gestionPublicaciones.CrearPublicacion(publicacion);

        if (resultado != "ok")
            return BadRequest(resultado);

        return Ok("Publicación creada correctamente");
    }

    [HttpPut("{id}")]
    public IActionResult ActualizarPublicacion(int id, Publicacion publicacionEditada)
    {
        if (id != publicacionEditada.Id)
            return BadRequest();

        var publicacion = _gestionPublicaciones.ObtenerPublicacion(id);

        if (publicacion == null)
            return NotFound();

        _gestionPublicaciones.ActualizarPublicacion(publicacionEditada);

        return Ok("Publicación actualizada correctamente");
    }
    
    //ADMIN
    [HttpDelete("{id}")]
    public IActionResult EliminarPublicacion(int id, [FromQuery] int adminId, [FromQuery] string? razon)
    {
        if (string.IsNullOrWhiteSpace(razon))
            return BadRequest("La razon de eliminacion es obligatoria");

        var resultado = _gestionPublicaciones.EliminarPublicacion(adminId, id, razon);

        if (resultado != "ok")
            return BadRequest(resultado);

        return NoContent();
    }
    
    //USUARIA
    [HttpDelete("eliminar-propia/{id}")]
    public IActionResult EliminarPublicacionPropia(int id, [FromQuery] int usuarioId)
    {
        var resultado = _gestionPublicaciones.EliminarPublicacionPropia(usuarioId, id);

        if (resultado != "ok")
            return BadRequest(resultado);

        return NoContent();
    }
}