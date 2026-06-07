using mochi_closet.Datos;

namespace mochi_closet.Negocio;

public class GestionUsuarios
{
        private static List<Usuario> _dbUsuarios = new();
 
    public List<Usuario> ListaUsuarios()
    {
        return _dbUsuarios;
    }
 
    public Usuario? ObtenerUsuario(int id)
    {
        return _dbUsuarios.FirstOrDefault(u => u.Id == id);
    }
 
    public string RegistrarUsuario(Usuario usuario)
    {
        if (string.IsNullOrWhiteSpace(usuario.Nombre))
            return "El nombre es obligatorio";
 
        if (string.IsNullOrWhiteSpace(usuario.Username))
            return "El username es obligatorio";
 
        if (string.IsNullOrWhiteSpace(usuario.Email))
            return "El email es obligatorio";
 
        if (string.IsNullOrWhiteSpace(usuario.Password))
            return "La contraseña es obligatoria";
 
        if (usuario.Password.Length < 8)
            return "La contraseña debe tener al menos 8 caracteres";
 
        if (_dbUsuarios.Any(u => u.Username == usuario.Username))
            return "El username ya está en uso";
 
        if (_dbUsuarios.Any(u => u.Email == usuario.Email))
            return "El email ya está registrado";
 
        usuario.Id = _dbUsuarios.Count == 0 ? 1 : _dbUsuarios.Max(u => u.Id) + 1;
        usuario.Rol = "Usuaria";
        usuario.Estado = "Activo";
        _dbUsuarios.Add(usuario);
 
        return "ok";
    }
 
    public Usuario? IniciarSesion(string? email, string? password)
    {
        var usuario = _dbUsuarios.FirstOrDefault(u => u.Email == email && u.Password == password);
 
        if (usuario == null)
            return null;
 
        if (usuario.Estado == "Suspendido")
            return null;
 
        return usuario;
    }
 
    public string ActualizarUsuario(Usuario usuarioEditado)
    {
        var u = _dbUsuarios.FirstOrDefault(u => u.Id == usuarioEditado.Id);
 
        if (u == null)
            return "Usuaria no encontrada";
 
        if (_dbUsuarios.Any(x => x.Username == usuarioEditado.Username && x.Id != usuarioEditado.Id))
            return "El username ya está en uso";
 
        u.Nombre = usuarioEditado.Nombre;
        u.Username = usuarioEditado.Username;
        u.Telefono = usuarioEditado.Telefono;
 
        return "ok";
    }
 
    public void SuspenderUsuario(int id)
    {
        var u = _dbUsuarios.FirstOrDefault(u => u.Id == id);
        if (u != null)
            u.Estado = "Suspendido";
    }
 
    public void ActivarUsuario(int id)
    {
        var u = _dbUsuarios.FirstOrDefault(u => u.Id == id);
        if (u != null)
            u.Estado = "Activo";
    }
}