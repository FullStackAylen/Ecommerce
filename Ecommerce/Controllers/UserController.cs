using Controller;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Model;

namespace Ecommerce.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class UserController : ControllerBase
    {
        [HttpGet]
        //[Authorize(Roles = "administrator,clients")]
        public async Task<ActionResult> GetUsers(int page = 1, int limit = 10)
        {
            var count = await ControladoraUsuarios.Instance.ListadoUsuarios(); // Obtén todos los usuarios
            var users = count.Skip((page - 1) * limit).Take(limit).ToList();
            return Ok(new
            {
                users = users,
                totalPages = (int)Math.Ceiling(count.Count / (double)limit)
            });
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<Usuario>> GetUserID(int id)
        {

            var prod = ControladoraUsuarios.Instance.UsuarioID(id).Result;
            if (prod == null) // Manejo de producto no encontrado
                return NotFound();

            return Ok(prod);
        }
        [HttpPost]
        //[Authorize(Roles = "administrator")]
        public async Task<ActionResult<string>> AddUser(Usuario user) => await ControladoraUsuarios.Instance.AgregarUsuario(user);
        [HttpPut]
        //[Authorize(Roles = "administrator")]
        public async Task<ActionResult<string>> EditUser(Usuario user) => await ControladoraUsuarios.Instance.EditarUsuario(user);
        [HttpDelete]
        //[Authorize(Roles = "administrator")]
        public async Task<ActionResult<string>> DeleteUser(Usuario user) => await ControladoraUsuarios.Instance.EliminarUsuario(user);

    }
}
