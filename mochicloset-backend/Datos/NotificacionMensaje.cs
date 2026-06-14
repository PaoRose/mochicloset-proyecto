namespace mochi_closet.Datos;

public class NotificacionMensaje : Notificacion
{
    public override string ObtenerIcono()
    {
        return "💬";
    }

    public override string ObtenerDescripcion()
    {
        return "Nuevo mensaje: " + Mensaje;
    }
}