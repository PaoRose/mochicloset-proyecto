namespace mochi_closet.Datos;

public class NotificacionVenta : Notificacion
{
    public override string ObtenerIcono()
    {
        return "🎉";
    }

    public override string ObtenerDescripcion()
    {
        return "Venta: " + Mensaje;
    }
}