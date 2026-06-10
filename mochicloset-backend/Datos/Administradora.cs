namespace mochi_closet.Datos;

public class Administradora : Usuario
{
    public override string ObtenerRol()
    {
        return "Admin";
    }

    public override bool PuedePublicar()
    {
        return false;
    }

    public override bool PuedeAdministrar()
    {
        return true;
    }
}