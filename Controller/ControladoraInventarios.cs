using Model.Repositories;
using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Repositories;

namespace Controller
{
    public class ControladoraInventarios
    {
        #region private atributtes
        private static ControladoraInventarios instance;
        #endregion
        private ControladoraInventarios() { }
        #region instance
        public static ControladoraInventarios Instance
        {
            get
            {
                if (instance == null)
                    instance = new ControladoraInventarios();
                return instance;
            }
        }
        #endregion

        #region logic methods
        public async Task<List<Inventario>> ListadoInventarios() => await RepositorioInventarios.Instance.Listar();
        public async Task<Inventario> InventarioID(int id) => await RepositorioInventarios.Instance.ObtenerInventarioID(id);

        public async Task<string> AgregarInventario(Inventario Inventario)
        {
            try
            {
                if (RepositorioInventarios.Instance.Listar().Result.FirstOrDefault(x => x.ProductoId == Inventario.ProductoId) is null)
                {
                    if (RepositorioInventarios.Instance.Agregar(Inventario).Result)
                    {
                        return $"El Inventario  se agregó correctamente!";
                    }
                    else return $"El Inventario no se ha podido agregar!";
                }
                else return $"El Inventario ya existe!";
            }
            catch (Exception)
            {
                return "Error desconocido.";
            }
        }
        public async Task<string> EditarInventario(Inventario Inventario)
        {
            try
            {
                if (RepositorioInventarios.Instance.ObtenerInventarioID(Inventario.InventarioId) is not null)
                {
                    if (RepositorioInventarios.Instance.Editar(Inventario).Result)
                        return $"El Inventario para {Inventario.Producto.Nombre} se modifico correctamente!";
                    else
                        return $"El Inventario para {Inventario.Producto.Nombre} no se ha podido modificar!";
                }
                else return $"El Inventario para {Inventario.Producto.Nombre} no existe!";
            }
            catch (Exception)
            {
                return "Error desconocido.";
            }
        }
        public async Task<string> EliminarInventario(int Inventario)
        {
            try
            {
                if (RepositorioInventarios.Instance.ObtenerInventarioID(Inventario) is not null)
                {
                    if (RepositorioInventarios.Instance.Eliminar(Inventario).Result)
                        return $"El Inventario se eliminó correctamente!";
                    else
                        return $"El Inventario no se ha podido eliminar!";
                }
                else
                    return $"El Inventario no existe!";
            }
            catch (Exception)
            {
                return "Error desconocido";
            }
        }
        #endregion
    }
}
