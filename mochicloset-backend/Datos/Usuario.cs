namespace mochi_closet.Datos;

public class Usuario
{
    public int Id { get; set; }
    public string? Nombre { get; set; }
    public string? Username { get; set; }
    public string? Email { get; set; }
    public string? Telefono { get; set; }
    public string? Password { get; set; }
    public string? Rol { get; set; }
    public string? Estado { get; set; }
    public string? FotoPerfil { get; set; }

    public virtual string ObtenerRol()
    {
        return "Usuario";
    }

    public virtual bool PuedePublicar()
    {
        return false;
    }

    public virtual bool PuedeAdministrar()
    {
        return false;
    }
}