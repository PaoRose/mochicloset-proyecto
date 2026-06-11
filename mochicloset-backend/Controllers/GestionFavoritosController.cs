using Microsoft.AspNetCore.Mvc;
using mochi_closet.Datos;
using mochi_closet.Negocio;

namespace mochi_closet.Controllers;

[ApiController]
[Route("[controller]")]
public class GestionFavoritosController : ControllerBase
{
    private readonly GestionFavoritos _gestionFavoritos;

    public GestionFavoritosController(
        GestionFavoritos gestionFavoritos)
    {
        _gestionFavoritos = gestionFavoritos;
    }

    [HttpGet("{usuarioId}")]
    public IEnumerable<Favorito> ListaFavoritosPorUsuaria(int usuarioId)
    {
        return _gestionFavoritos.ListaFavoritosPorUsuaria(usuarioId);
    }

    [HttpPost]
    public ActionResult AgregarFavorito(Favorito favorito)
    {
        var resultado = _gestionFavoritos.AgregarFavorito(favorito);

        if (resultado != "ok")
            return BadRequest(new { mensaje = resultado });

        return Ok(new { mensaje = "Agregado a favoritos correctamente" });
    }

    [HttpDelete("{usuarioId}/{publicacionId}")]
    public IActionResult EliminarFavorito(int usuarioId, int publicacionId)
    {
        _gestionFavoritos.EliminarFavorito(usuarioId, publicacionId);
        return Ok(new { mensaje = "Eliminado de favoritos correctamente" });
    }
}