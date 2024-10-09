using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Interfaces
{
    public interface IRepositorioProducto
    {
        Task<List<ProductosMaquillaje>> Listar();
        Task<ProductosMaquillaje> ObtenerProductoID(int id);
        Task<bool> Agregar(ProductosMaquillaje product);
        Task<bool> Editar(ProductosMaquillaje productos);
        Task<bool> Eliminar(int id);
    }
}
