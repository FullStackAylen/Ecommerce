using Controller;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Model;

namespace Ecommerce.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class CategoryController : ControllerBase
    {
        [HttpGet]
        //[Authorize(Roles = "administrator,clients")]
        public async Task<ActionResult> GetCategories(int page = 1, int limit = 10)
        {
            var count = await ControladoraCategorias.Instance.ListadoCategorias(); // Obtén todos los usuarios
            var categories = count.Skip((page - 1) * limit).Take(limit).ToList();
            return Ok(new
            {
                categories = categories,
                totalPages = (int)Math.Ceiling(count.Count / (double)limit)
            });
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<Categoria>> GetCategoryID(int id)
        {

            var prod = ControladoraCategorias.Instance.CategoriaID(id).Result;
            if (prod == null) // Manejo de producto no encontrado
                return NotFound();

            return Ok(prod);
        }
        [HttpPost]
        //[Authorize(Roles = "administrator")]
        public async Task<ActionResult<string>> AddCategory(Categoria user) => await ControladoraCategorias.Instance.AgregarCategoria(user);
        [HttpPut]
        //[Authorize(Roles = "administrator")]
        public async Task<ActionResult<string>> EditCategory(Categoria user) => await ControladoraCategorias.Instance.EditarCategoria(user);
        [HttpDelete("{id}")]
        //[Authorize(Roles = "administrator")]
        public async Task<ActionResult<string>> DeleteCategory(int id) => await ControladoraCategorias.Instance.EliminarCategoria(id);
    }
}
