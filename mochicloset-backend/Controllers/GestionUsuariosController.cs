using Microsoft.AspNetCore.Mvc;
using mochi_closet.Datos;
using mochi_closet.Negocio;

namespace mochi_closet.Controllers;

[ApiController]
[Route("[controller]")]
public class GestionUsuariosController : ControllerBase
{
    private readonly GestionUsuarios _gestionUsuarios;

    public GestionUsuariosController(
        GestionUsuarios gestionUsuarios)
    {
        _gestionUsuarios = gestionUsuarios;
    }

    [HttpGet("lista-usuarios")]
    public IEnumerable<Usuario> ListaUsuarios()
    {
        return _gestionUsuarios.ListaUsuarios();
    }

    [HttpGet("{id}")]
    public ActionResult<Usuario> ObtenerUsuario(int id)
    {
        var usuario = _gestionUsuarios.ObtenerUsuario(id);

        if (usuario == null)
            return NotFound();

        return Ok(usuario);
    }

    [HttpPost("registrar-usuario")]
    public ActionResult<string> RegistrarUsuario(Usuario usuario)
    {
        var resultado = _gestionUsuarios.RegistrarUsuario(usuario);

        if (resultado != "ok")
            return BadRequest(resultado);

        return Ok("Usuaria registrada correctamente");
    }

    [HttpPost("iniciar-sesion")]
    public ActionResult<Usuario> IniciarSesion(Usuario credenciales)
    {
        var usuario = _gestionUsuarios
            .IniciarSesion(
                credenciales.Email,
                credenciales.Password);

        if (usuario == null)
            return Unauthorized(
                "Credenciales incorrectas o cuenta suspendida");

        return Ok(usuario);
    }

    [HttpPut("actualizar-perfil")]
    public IActionResult ActualizarPerfil(
        Usuario usuarioEditado)
    {
        var resultado =
            _gestionUsuarios.ActualizarUsuario(usuarioEditado);

        if (resultado != "ok")
            return BadRequest(resultado);

        return Ok("Perfil actualizado correctamente");
    }

    [HttpPut("suspender-usuario/{id}")]
    public IActionResult SuspenderUsuario(int id, [FromQuery] int adminId)
    {
        var resultado = _gestionUsuarios.SuspenderUsuario(adminId, id);

        if (resultado != "ok")
            return BadRequest(resultado);

        return Ok("Usuaria suspendida correctamente");
    }

    [HttpPut("activar-usuario/{id}")]
    public IActionResult ActivarUsuario(int id, [FromQuery] int adminId)
    {
        var resultado = _gestionUsuarios.ActivarUsuario(adminId, id);

        if (resultado != "ok")
            return BadRequest(resultado);

        return Ok("Usuaria activada correctamente");
    }
}