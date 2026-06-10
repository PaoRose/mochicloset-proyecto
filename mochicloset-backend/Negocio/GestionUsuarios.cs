using mochi_closet.Data;
using mochi_closet.Datos;

namespace mochi_closet.Negocio;

public class GestionUsuarios
{
    private readonly MochiClosetDbContext _context;

    public GestionUsuarios(MochiClosetDbContext context)
    {
        _context = context;
    }

    public List<Usuario> ListaUsuarios()
    {
        return _context.Usuarios.ToList();
    }

    public Usuario? ObtenerUsuario(int id)
    {
        return _context.Usuarios
            .FirstOrDefault(u => u.Id == id);
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

        if (_context.Usuarios.Any(u => u.Username == usuario.Username))
            return "El username ya está en uso";

        if (_context.Usuarios.Any(u => u.Email == usuario.Email))
            return "El email ya está registrado";

        usuario.Rol = "Usuaria";
        usuario.Estado = "Activo";

        _context.Usuarios.Add(usuario);

        _context.SaveChanges();

        return "ok";
    }

    public Usuario? IniciarSesion(string? email, string? password)
    {
        var usuario = _context.Usuarios
            .FirstOrDefault(u =>
                u.Email == email &&
                u.Password == password);

        if (usuario == null)
            return null;

        if (usuario.Estado == "Suspendido")
            return null;

        return usuario;
    }

    public string ActualizarUsuario(Usuario usuarioEditado)
    {
        var usuario = _context.Usuarios
            .FirstOrDefault(u => u.Id == usuarioEditado.Id);

        if (usuario == null)
            return "Usuaria no encontrada";

        if (_context.Usuarios.Any(u =>
                u.Username == usuarioEditado.Username &&
                u.Id != usuarioEditado.Id))
            return "El username ya está en uso";

        usuario.Nombre = usuarioEditado.Nombre;
        usuario.Username = usuarioEditado.Username;
        usuario.Telefono = usuarioEditado.Telefono;

        _context.SaveChanges();

        return "ok";
    }

    public string SuspenderUsuario(int adminId, int id)
    {
        var admin = _context.Usuarios
            .FirstOrDefault(u => u.Id == adminId);

        if (admin == null || admin.Rol != "Admin")
            return "No tienes permisos para realizar esta accion";

        var usuario = _context.Usuarios
            .FirstOrDefault(u => u.Id == id);

        if (usuario == null)
            return "Usuaria no encontrada";

        usuario.Estado = "Suspendido";
        _context.SaveChanges();

        return "ok";
    }

    public string ActivarUsuario(int adminId, int id)
    {
        var admin = _context.Usuarios
            .FirstOrDefault(u => u.Id == adminId);

        if (admin == null || admin.Rol != "Admin")
            return "No tienes permisos para realizar esta accion";

        var usuario = _context.Usuarios
            .FirstOrDefault(u => u.Id == id);

        if (usuario == null)
            return "Usuaria no encontrada";

        usuario.Estado = "Activo";
        _context.SaveChanges();

        return "ok";
    }
}