namespace mochi_closet.Datos;

public class Usuaria : Usuario
{
    public override string ObtenerRol()
    {
        return "Usuaria";
    }

    public override bool PuedePublicar()
    {
        return true;
    }

    public override bool PuedeAdministrar()
    {
        return false;
    }
}