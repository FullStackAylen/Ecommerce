using Controller;
using Microsoft.AspNetCore.Mvc;
using Model;

namespace Ecommerce.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class InventoryController : ControllerBase
    {
        [HttpGet]
        //[Authorize(Roles = "administrator,clients")]
        public async Task<ActionResult> GetInventories(int page = 1, int limit = 10)
        {
            var count = await ControladoraInventarios.Instance.ListadoInventarios(); // Obtén todos los usuarios
            var inventories = count.Skip((page - 1) * limit).Take(limit).ToList();
            return Ok(new
            {
                inventories = inventories,
                totalPages = (int)Math.Ceiling(count.Count / (double)limit)
            });
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<Inventario>> GetInventoryID(int id)
        {

            var inventory = ControladoraInventarios.Instance.InventarioID(id).Result;
            if (inventory == null) // Manejo de offeructo no encontrado
                return NotFound();

            return Ok(inventory);
        }
        [HttpPost]
        //[Authorize(Roles = "administrator")]
        public async Task<ActionResult<string>> AddInventory(Inventario inventory) => await ControladoraInventarios.Instance.AgregarInventario(inventory);
        [HttpPut]
        //[Authorize(Roles = "administrator")]
        public async Task<ActionResult<string>> EditInventory(Inventario inventory) => await ControladoraInventarios.Instance.EditarInventario(inventory);
        [HttpDelete("{id}")]
        //[Authorize(Roles = "administrator")]
        public async Task<ActionResult<string>> DeleteInventory(int id) => await ControladoraInventarios.Instance.EliminarInventario(id);
    }
}
