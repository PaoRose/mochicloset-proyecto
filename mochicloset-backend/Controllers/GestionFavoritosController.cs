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
    public ActionResult<string> AgregarFavorito(Favorito favorito)
    {
        var resultado = _gestionFavoritos.AgregarFavorito(favorito);

        if (resultado != "ok")
            return BadRequest(resultado);

        return Ok("Agregado a favoritos correctamente");
    }

    [HttpDelete("{usuarioId}/{publicacionId}")]
    public IActionResult EliminarFavorito(int usuarioId, int publicacionId)
    {
        _gestionFavoritos.EliminarFavorito(usuarioId, publicacionId);

        return Ok("Eliminado de favoritos correctamente");
    }
}