using Model;
using Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Controller
{
    public class ControladoraProductos
    {
        #region private atributtes
        private static ControladoraProductos instance;
        #endregion
        private ControladoraProductos() { }
        #region instance
        public static ControladoraProductos Instance
        {
            get
            {
                if (instance == null)
                    instance = new ControladoraProductos();
                return instance;
            }
        }
        #endregion

        #region logic methods
        public async Task<List<ProductosMaquillaje>> ListadoProductos() => await RepositorioProductos.Instance.Listar();
        public async Task<ProductosMaquillaje> ProductoID(int id) => await RepositorioProductos.Instance.ObtenerProductoID(id);
        public async Task<List<ProductosMaquillaje>> ListadoSinInventario() => await RepositorioProductos.Instance.ListadoProductosSinInventario();
           
        

        public async Task<string> AgregarProducto(ProductosMaquillaje producto)
        {
            try
            {
                if (RepositorioProductos.Instance.Listar().Result.FirstOrDefault(x => x.Nombre == producto.Nombre) is null)
                {
                    if (RepositorioProductos.Instance.Agregar(producto).Result)
                    {
                        return $"El producto {producto.Nombre} de {producto.Marca.NombreMarca} se agregó correctamente!";
                    }
                    else return $"El producto {producto.Nombre} de {producto.Marca.NombreMarca} no se ha podido agregar!";
                }
                else return $"El producto {producto.Nombre} de {producto.Marca.NombreMarca} ya existe!";
            }
            catch (Exception)
            {
                return "Error desconocido.";
            }
        }
        public async Task<string> EditarProducto(ProductosMaquillaje producto)
        {
            try
            {
                if (RepositorioProductos.Instance.ObtenerProductoID(producto.ProductoId) is not null)
                {
                    if (RepositorioProductos.Instance.Editar(producto).Result)
                        return $"El producto {producto.Nombre} de {producto.Marca.NombreMarca} se modifico correctamente!";
                    else
                        return $"El producto {producto.Nombre} de {producto.Marca.NombreMarca} no se ha podido modificar!";
                }
                else return $"El producto {producto.Nombre} de {producto.Marca.NombreMarca} no existe!";
            }
            catch (Exception)
            {
                return "Error desconocido.";
            }
        }
        public async Task<string> EliminarProducto(ProductosMaquillaje producto)
        {
            try
            {
                if (RepositorioProductos.Instance.ObtenerProductoID(producto.ProductoId) is not null)
                {
                    if (RepositorioProductos.Instance.Eliminar(producto.ProductoId).Result)
                        return $"El producto {producto.Nombre} de {producto.Marca.NombreMarca} se eliminó correctamente!";
                    else
                        return $"El producto {producto.Nombre} de {producto.Marca.NombreMarca} no se ha podido eliminar!";
                }
                else
                    return $"El producto {producto.Nombre} de {producto.Marca.NombreMarca} no existe!";
            }
            catch (Exception)
            {
                return "Error desconocido";
            }
        }
        #endregion
    }
}
