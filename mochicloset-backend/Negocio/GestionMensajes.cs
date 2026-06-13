using Microsoft.EntityFrameworkCore;
using mochi_closet.Data;
using mochi_closet.Datos;

namespace mochi_closet.Negocio;

public class GestionMensajes
{
    private readonly MochiClosetDbContext _context;
    private readonly GestionNotificaciones _gestionNotificaciones;

    public GestionMensajes(
        MochiClosetDbContext context,
        GestionNotificaciones gestionNotificaciones)
    {
        _context = context;
        _gestionNotificaciones = gestionNotificaciones;
    }

    public List<Conversacion> ListaConversacionesPorUsuaria(int usuarioId)
    {
        return _context.Conversaciones
        .Where(c => c.CompradoraId == usuarioId || c.VendedoraId == usuarioId)
        .Include(c => c.Publicacion)
        .ToList();
    }

    public string IniciarConversacion(Conversacion conversacion)
    {
        var compradora = _context.Usuarios
            .FirstOrDefault(u => u.Id == conversacion.CompradoraId);

        if (compradora == null)
            return "La compradora no existe";

        var vendedora = _context.Usuarios
            .FirstOrDefault(u => u.Id == conversacion.VendedoraId);

        if (vendedora == null)
            return "La vendedora no existe";

        var publicacion = _context.Publicaciones
            .FirstOrDefault(p => p.Id == conversacion.PublicacionId);

        if (publicacion == null)
            return "La publicación no existe";

        var yaExiste = _context.Conversaciones.Any(c =>
            c.CompradoraId == conversacion.CompradoraId &&
            c.VendedoraId == conversacion.VendedoraId &&
            c.PublicacionId == conversacion.PublicacionId);

        if (yaExiste)
            return "Ya existe una conversacion para esta publicacion";

        conversacion.FechaCreacion = DateTime.Now;

        _context.Conversaciones.Add(conversacion);
        _context.SaveChanges();

        return "ok";
    }

    public List<Mensaje> ListaMensajesPorConversacion(int conversacionId)
    {
        return _context.Mensajes
            .Where(m => m.ConversacionId == conversacionId)
            .OrderBy(m => m.FechaEnvio)
            .ToList();
    }

    public string EnviarMensaje(Mensaje mensaje)
    {
        if (string.IsNullOrWhiteSpace(mensaje.Texto))
            return "El mensaje no puede estar vacio";

        var conversacion = _context.Conversaciones
            .FirstOrDefault(c => c.Id == mensaje.ConversacionId);

        if (conversacion == null)
            return "La conversacion no existe";

        var remitente = _context.Usuarios
            .FirstOrDefault(u => u.Id == mensaje.RemitenteId);

        if (remitente == null)
            return "El remitente no existe";

        mensaje.FechaEnvio = DateTime.Now;
        mensaje.Leido = false;

        _context.Mensajes.Add(mensaje);
        _context.SaveChanges();

        var destinatarioId = conversacion.CompradoraId == mensaje.RemitenteId
            ? conversacion.VendedoraId
            : conversacion.CompradoraId;

        _gestionNotificaciones.CrearNotificacion(
            destinatarioId,
            "Mensaje",
            remitente.Username + " te envio un mensaje");

        return "ok";
    }
}