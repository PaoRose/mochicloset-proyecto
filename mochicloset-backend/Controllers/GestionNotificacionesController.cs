using Microsoft.AspNetCore.Mvc;
using mochi_closet.Datos;
using mochi_closet.Negocio;

namespace mochi_closet.Controllers;

[ApiController]
[Route("[controller]")]
public class GestionNotificacionesController : ControllerBase
{
    private readonly GestionNotificaciones _gestionNotificaciones;

    public GestionNotificacionesController(
        GestionNotificaciones gestionNotificaciones)
    {
        _gestionNotificaciones = gestionNotificaciones;
    }

    [HttpGet("{usuarioId}")]
    public IEnumerable<Notificacion> ListaNotificaciones(int usuarioId)
    {
        return _gestionNotificaciones.ListaNotificacionesPorUsuaria(usuarioId);
    }

    [HttpPut("marcar-leida/{id}")]
    public IActionResult MarcarComoLeida(int id)
    {
        _gestionNotificaciones.MarcarComoLeida(id);
        return Ok("Notificación marcada como leída");
    }
}