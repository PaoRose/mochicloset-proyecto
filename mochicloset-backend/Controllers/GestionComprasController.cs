using Microsoft.AspNetCore.Mvc;
using mochi_closet.Datos;
using mochi_closet.Negocio;

namespace mochi_closet.Controllers;

[ApiController]
[Route("[controller]")]
public class GestionComprasController : ControllerBase
{
    private readonly GestionCompras _gestionCompras;

    public GestionComprasController(
        GestionCompras gestionCompras)
    {
        _gestionCompras = gestionCompras;
    }

    [HttpGet("lista-compras")]
    public IEnumerable<Compra> ListaCompras()
    {
        return _gestionCompras.ListaCompras();
    }

    [HttpGet("mis-compras/{usuarioId}")]
    public IEnumerable<Compra> ListaComprasPorUsuaria(int usuarioId)
    {
        return _gestionCompras.ListaComprasPorUsuaria(usuarioId);
    }
    
    [HttpGet("mis-ventas/{usuarioId}")]
    public IEnumerable<Compra> ListaVentasPorUsuaria(int usuarioId)
    {
        return _gestionCompras.ListaVentasPorUsuaria(usuarioId);
    }

    [HttpGet("{id}")]
    public ActionResult<Compra> ObtenerCompra(int id)
    {
        var compra = _gestionCompras.ObtenerCompra(id);

        if (compra == null)
            return NotFound();

        return Ok(compra);
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