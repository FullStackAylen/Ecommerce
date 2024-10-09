using Controller;
using Microsoft.AspNetCore.Mvc;
using Model;

namespace Ecommerce.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class BrandController : ControllerBase
    {
        [HttpGet]
        //[Authorize(Roles = "administrator,clients")]
        public async Task<ActionResult> GetBrands(int page = 1, int limit = 10)
        {
            var count = await ControladoraMarcas.Instance.ListadoMarcas(); // Obtén todos los usuarios
            var brands = count.Skip((page - 1) * limit).Take(limit).ToList();
            return Ok(new
            {
                brands = brands,
                totalPages = (int)Math.Ceiling(count.Count / (double)limit)
            });
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<Marca>> GetBrandID(int id)
        {

            var prod = ControladoraMarcas.Instance.MarcaID(id).Result;
            if (prod == null) // Manejo de producto no encontrado
                return NotFound();

            return Ok(prod);
        }
        [HttpPost]
        //[Authorize(Roles = "administrator")]
        public async Task<ActionResult<string>> AddBrand(Marca user) => await ControladoraMarcas.Instance.AgregarMarca(user);
        [HttpPut]
        //[Authorize(Roles = "administrator")]
        public async Task<ActionResult<string>> EditBrand(Marca user) => await ControladoraMarcas.Instance.EditarMarca(user);
        [HttpDelete]
        //[Authorize(Roles = "administrator")]
        public async Task<ActionResult<string>> DeleteBrand(Marca user) => await ControladoraMarcas.Instance.EliminarMarca(user);
    }
}
