namespace mochi_closet.Datos;

public class NotificacionAdmin : Notificacion
{
    public override string ObtenerIcono()
    {
        return "🛡";
    }

    public override string ObtenerDescripcion()
    {
        return "Admin: " + Mensaje;
    }
}