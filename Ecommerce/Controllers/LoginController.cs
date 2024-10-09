using Controller;
using Ecommerce.Services;
using Microsoft.AspNetCore.Mvc;
using Model;

namespace Ecommerce.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class LoginController : ControllerBase
    {
        private readonly JwtService _jwtService;
        private readonly PasswordService _passwordService;

        //Inyección de dependencias del servicio JwtService
        public LoginController(JwtService jwtService, PasswordService passwordService)
        {
            _jwtService = jwtService;
            _passwordService = passwordService;
        }
        [HttpPost]
        public async Task<IActionResult> Login([FromBody] LoginDTO login)
        {
            try
            {
                Usuario? usuario = (await ControladoraUsuarios.Instance.ListadoUsuarios()).FirstOrDefault(x => x.Email == login.Email && x.Contrasena == _passwordService.HashPassword(login.Contrasena));

                if (usuario != null)
                {

                    if (ControladoraHistorialUsuarios.Instance.ListadoHistorial().Result.Any(x => x.UsuarioId == usuario.UsuarioId && x.FinSesion == null))
                        return Unauthorized("El usuario ya tiene una sesion abierta");

                    string token = _jwtService.GenerateToken(usuario.Email, usuario.Rol.Nombre);

                    string resp = await ControladoraHistorialUsuarios.Instance.AgregarHistorial(new HistorialUsuario
                    {
                        UsuarioId = usuario.UsuarioId,
                        InicioSesion = DateTime.Now,
                        Token = token,
                        FinSesion = null
                    });

                    return Ok(new { Token = token, Rol = usuario.Rol.Nombre, Mensaje = resp, UsuarioID = usuario.UsuarioId });
                }
                else
                {
                    return Unauthorized("Credenciales inválidas");
                }
            }
            catch (Exception ex)
            {
                return BadRequest("Ha ocurrido un error");
            }
          
        }
        [HttpPost]
        public IActionResult Logout([FromBody] int UsuarioID)
        {
            var historial = ControladoraHistorialUsuarios.Instance.ListadoHistorial().Result.FirstOrDefault(x => x.UsuarioId == UsuarioID && x.FinSesion == null);

            if (historial != null)
            {
                historial.FinSesion = DateTime.Now;
                ControladoraHistorialUsuarios.Instance.EditarHistorial(historial);
                return Ok("Sesión cerrada con éxito.");
            }

            return Ok("La sesión ha expirado o no existe.");
        }
    }
}
