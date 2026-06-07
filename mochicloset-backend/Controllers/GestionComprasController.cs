using Microsoft.AspNetCore.Mvc;
using mochi_closet.Datos;
using mochi_closet.Negocio;

namespace mochi_closet.Controllers;

[ApiController]
[Route("[controller]")]
public class GestionComprasController : ControllerBase
{
    private GestionCompras _gestionCompras;

    public GestionComprasController()
    {
        _gestionCompras = new GestionCompras();
    }

    [HttpGet("lista-compras")]
    public IEnumerable<Compra> ListaCompras()
    {
        return _gestionCompras.ListaCompras();
    }

    [HttpGet("{id}")]
    public ActionResult<Compra> ObtenerCompra(int id)
    {
        var compra = _gestionCompras.ObtenerCompra(id);

        if (compra == null)
            return NotFound();

        return compra;
    }

    [HttpPost]
    public ActionResult<string> RegistrarCompra(Compra compra)
    {
        var resultado = _gestionCompras.RegistrarCompra(compra);

        if (resultado != "ok")
            return BadRequest(resultado);

        return Ok("Compra registrada correctamente");
    }
}