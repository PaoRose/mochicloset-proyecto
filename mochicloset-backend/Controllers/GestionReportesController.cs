using Microsoft.AspNetCore.Mvc;
using mochi_closet.Datos;
using mochi_closet.Negocio;

namespace mochi_closet.Controllers;

[ApiController]
[Route("[controller]")]
public class GestionReportesController : ControllerBase
{
    private readonly GestionReportes _gestionReportes;

    public GestionReportesController(GestionReportes gestionReportes)
    {
        _gestionReportes = gestionReportes;
    }

    [HttpGet]
    public IEnumerable<Reporte> ListaReportes()
    {
        return _gestionReportes.ListaReportes();
    }

    [HttpPost]
    public ActionResult CrearReporte(Reporte reporte)
    {
        var resultado = _gestionReportes.CrearReporte(reporte);
        if (resultado != "ok") return BadRequest(resultado);
        return Ok(new { mensaje = "Reporte enviado correctamente" });
    }

    [HttpPut("resolver/{id}")]
    public IActionResult ResolverReporte(int id)
    {
        var resultado = _gestionReportes.ResolverReporte(id);
        if (resultado != "ok") return BadRequest(resultado);
        return Ok(new { mensaje = "Reporte resuelto" });
    }
}