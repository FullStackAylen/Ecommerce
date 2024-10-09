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
    public class ControladoraCategorias
    {
        #region private atributtes
        private static ControladoraCategorias instance;
        #endregion
        private ControladoraCategorias() { }
        #region instance
        public static ControladoraCategorias Instance
        {
            get
            {
                if (instance == null)
                    instance = new ControladoraCategorias();
                return instance;
            }
        }
        #endregion

        #region logic methods
        public async Task<List<Categoria>> ListadoCategorias() => await RepositorioCategorias.Instance.Listar();
        public async Task<Categoria> CategoriaID(int id) => await RepositorioCategorias.Instance.ObtenerCategoriaID(id);

        public async Task<string> AgregarCategoria(Categoria categoria)
        {
            try
            {
                if (RepositorioCategorias.Instance.Listar().Result.FirstOrDefault(x => x.NombreCategoria == categoria.NombreCategoria) is null)
                {
                    if (RepositorioCategorias.Instance.Agregar(categoria).Result)
                    {
                        return $"La categoria {categoria.NombreCategoria} se agregó correctamente!";
                    }
                    else return $"La categoria {categoria.NombreCategoria} no se ha podido agregar!";
                }
                else return $"La categoria {categoria.NombreCategoria} ya existe!";
            }
            catch (Exception)
            {
                return "Error desconocido.";
            }
        }
        public async Task<string> EditarCategoria(Categoria categoria)
        {
            try
            {
                if (RepositorioCategorias.Instance.ObtenerCategoriaID(categoria.CategoriaId) is not null)
                {
                    if (RepositorioCategorias.Instance.Editar(categoria).Result)
                        return $"La categoria {categoria.NombreCategoria} se modifico correctamente!";
                    else
                        return $"La categoria {categoria.NombreCategoria} no se ha podido modificar!";
                }
                else return $"La categoria {categoria.NombreCategoria} no existe!";
            }
            catch (Exception)
            {
                return "Error desconocido.";
            }
        }
        public async Task<string> EliminarCategoria(int categoria)
        {
            try
            {
                if (RepositorioCategorias.Instance.ObtenerCategoriaID(categoria) is not null)
                {
                    if (RepositorioCategorias.Instance.Eliminar(categoria).Result)
                        return $"La categoria se eliminó correctamente!";
                    else
                        return $"La categoria no se ha podido eliminar!";
                }
                else
                    return $"La categoria no existe!";
            }
            catch (Exception)
            {
                return "Error desconocido";
            }
        }
        #endregion
    }
}
