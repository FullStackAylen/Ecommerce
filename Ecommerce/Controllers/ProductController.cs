using Controller;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Model;
using Repositories;

namespace Ecommerce.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class ProductController : ControllerBase
    {
        [HttpGet]
        //[Authorize(Roles = "administrator,clients")]
        public async Task<ActionResult> GetProducts(int page = 1, int limit = 10)
        {
            var count = await ControladoraProductos.Instance.ListadoProductos(); // Obtén todos los productos
            var products = count.Skip((page - 1) * limit).Take(limit).ToList();
            return Ok(new
            {
                products = products,
                totalPages = (int)Math.Ceiling(count.Count / (double)limit)
            });
        }
        [HttpGet]
        public async Task<ActionResult<List<ProductosMaquillaje>>> GetProductsList()
        {
            var productosSinInventario = await ControladoraProductos.Instance.ListadoSinInventario();
            return Ok(productosSinInventario);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ProductosMaquillaje>> GetProductoID(int id)
        {

            var prod = ControladoraProductos.Instance.ProductoID(id).Result;
            if (prod == null) // Manejo de producto no encontrado
                return NotFound();

            return Ok(prod);
        }
        [HttpPost]
        //[Authorize(Roles = "administrator")]
        public async Task<ActionResult<string>> AddProduct(ProductosMaquillaje prod) => await ControladoraProductos.Instance.AgregarProducto(prod);
        [HttpPut]
        //[Authorize(Roles = "administrator")]

        public async Task<ActionResult<string>> EditProduct(ProductosMaquillaje prod) => await ControladoraProductos.Instance.EditarProducto(prod);
        [HttpDelete]
        //[Authorize(Roles = "administrator")]
        public async Task<ActionResult<string>> DeleteProduct(ProductosMaquillaje prod) => await ControladoraProductos.Instance.EliminarProducto(prod);


    }
}
