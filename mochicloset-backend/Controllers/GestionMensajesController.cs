using Microsoft.AspNetCore.Mvc;
using mochi_closet.Data;
using mochi_closet.Datos;
using mochi_closet.Negocio;

namespace mochi_closet.Controllers;

[ApiController]
[Route("[controller]")]
public class GestionMensajesController : ControllerBase
{
    private readonly GestionMensajes _gestionMensajes;
    private readonly MochiClosetDbContext _context;

    public GestionMensajesController(
        GestionMensajes gestionMensajes,
        MochiClosetDbContext context)
    {
        _gestionMensajes = gestionMensajes;
        _context = context;
    }

    [HttpGet("conversaciones/{usuarioId}")]
    public IEnumerable<Conversacion> ListaConversaciones(int usuarioId)
    {
        return _gestionMensajes.ListaConversacionesPorUsuaria(usuarioId);
    }

    [HttpPost("conversaciones")]
    public ActionResult<Conversacion> IniciarConversacion(Conversacion conversacion)
    {
        var existente = _context.Conversaciones.FirstOrDefault(c =>
            c.CompradoraId == conversacion.CompradoraId &&
            c.VendedoraId == conversacion.VendedoraId &&
            c.PublicacionId == conversacion.PublicacionId);

        if (existente != null)
            return Ok(existente);

        var resultado = _gestionMensajes.IniciarConversacion(conversacion);

        if (resultado != "ok")
            return BadRequest(resultado);

        var nueva = _context.Conversaciones.FirstOrDefault(c =>
            c.CompradoraId == conversacion.CompradoraId &&
            c.VendedoraId == conversacion.VendedoraId &&
            c.PublicacionId == conversacion.PublicacionId);

        return Ok(nueva);
    }

    [HttpGet("mensajes/{conversacionId}")]
    public IEnumerable<Mensaje> ListaMensajes(int conversacionId)
    {
        return _gestionMensajes.ListaMensajesPorConversacion(conversacionId);
    }

    [HttpPost("mensajes")]
    public ActionResult EnviarMensaje(Mensaje mensaje)
    {
        var resultado = _gestionMensajes.EnviarMensaje(mensaje);

        if (resultado != "ok")
            return BadRequest(new { mensaje = resultado });

        return Ok(new { mensaje = "Mensaje enviado correctamente" });
    }
}