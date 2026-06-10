using Microsoft.AspNetCore.Mvc;
using mochi_closet.Datos;
using mochi_closet.Negocio;

namespace mochi_closet.Controllers;

[ApiController]
[Route("[controller]")]
public class GestionMensajesController : ControllerBase
{
    private readonly GestionMensajes _gestionMensajes;

    public GestionMensajesController(
        GestionMensajes gestionMensajes)
    {
        _gestionMensajes = gestionMensajes;
    }

    [HttpGet("conversaciones/{usuarioId}")]
    public IEnumerable<Conversacion> ListaConversaciones(int usuarioId)
    {
        return _gestionMensajes.ListaConversacionesPorUsuaria(usuarioId);
    }

    [HttpPost("conversaciones")]
    public ActionResult<string> IniciarConversacion(Conversacion conversacion)
    {
        var resultado = _gestionMensajes.IniciarConversacion(conversacion);

        if (resultado != "ok")
            return BadRequest(resultado);

        return Ok("Conversación iniciada correctamente");
    }

    [HttpGet("mensajes/{conversacionId}")]
    public IEnumerable<Mensaje> ListaMensajes(int conversacionId)
    {
        return _gestionMensajes.ListaMensajesPorConversacion(conversacionId);
    }

    [HttpPost("mensajes")]
    public ActionResult<string> EnviarMensaje(Mensaje mensaje)
    {
        var resultado = _gestionMensajes.EnviarMensaje(mensaje);

        if (resultado != "ok")
            return BadRequest(resultado);

        return Ok("Mensaje enviado correctamente");
    }
}