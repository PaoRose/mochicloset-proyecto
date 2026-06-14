using mochi_closet.Data;
using mochi_closet.Datos;

namespace mochi_closet.Negocio;

public class GestionNotificaciones
{
    private readonly MochiClosetDbContext _context;

    public GestionNotificaciones(MochiClosetDbContext context)
    {
        _context = context;
    }

    public List<Notificacion> ListaNotificacionesPorUsuaria(int usuarioId)
    {
        return _context.Notificaciones
            .Where(n => n.UsuarioId == usuarioId)
            .OrderByDescending(n => n.FechaCreacion)
            .ToList();
    }

    public void CrearNotificacion(int usuarioId, string tipo, string mensaje)
    {
        Notificacion notificacion = tipo switch
        {
            "Mensaje" => new NotificacionMensaje(),
            "Venta" => new NotificacionVenta(),
            "Admin" => new NotificacionAdmin(),
            _ => new Notificacion()
        };

        notificacion.UsuarioId = usuarioId;
        notificacion.Tipo = tipo;
        notificacion.Mensaje = mensaje;
        notificacion.Leida = false;
        notificacion.FechaCreacion = DateTime.Now;

        _context.Notificaciones.Add(notificacion);
        _context.SaveChanges();
    }

    public void MarcarComoLeida(int id)
    {
        var notificacion = _context.Notificaciones
            .FirstOrDefault(n => n.Id == id);

        if (notificacion != null)
        {
            notificacion.Leida = true;
            _context.SaveChanges();
        }
    }
}