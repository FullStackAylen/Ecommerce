using Model;
using Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Controller
{
    public class ControladoraMarcas
    {
        #region private atributtes
        private static ControladoraMarcas instance;
        #endregion
        private ControladoraMarcas() { }
        #region instance
        public static ControladoraMarcas Instance
        {
            get
            {
                if (instance == null)
                    instance = new ControladoraMarcas();
                return instance;
            }
        }
        #endregion

        #region logic methods
        public async Task<List<Marca>> ListadoMarcas() => await RepositorioMarcas.Instance.Listar();
        public async Task<Marca> MarcaID(int id) => await RepositorioMarcas.Instance.ObtenerMarcaID(id);

        public async Task<string> AgregarMarca(Marca Marca)
        {
            try
            {
                if (RepositorioMarcas.Instance.Listar().Result.FirstOrDefault(x => x.NombreMarca == Marca.NombreMarca) is null)
                {
                    if (RepositorioMarcas.Instance.Agregar(Marca).Result)
                    {
                        return $"La Marca {Marca.NombreMarca} se agregó correctamente!";
                    }
                    else return $"La Marca {Marca.NombreMarca} no se ha podido agregar!";
                }
                else return $"La Marca {Marca.NombreMarca} ya existe!";
            }
            catch (Exception)
            {
                return "Error desconocido.";
            }
        }
        public async Task<string> EditarMarca(Marca Marca)
        {
            try
            {
                if (RepositorioMarcas.Instance.ObtenerMarcaID(Marca.MarcaId) is not null)
                {
                    if (RepositorioMarcas.Instance.Editar(Marca).Result)
                        return $"La Marca {Marca.NombreMarca} se modifico correctamente!";
                    else
                        return $"La Marca {Marca.NombreMarca} no se ha podido modificar!";
                }
                else return $"La Marca {Marca.NombreMarca} no existe!";
            }
            catch (Exception)
            {
                return "Error desconocido.";
            }
        }
        public async Task<string> EliminarMarca(Marca Marca)
        {
            try
            {
                if (RepositorioMarcas.Instance.ObtenerMarcaID(Marca.MarcaId) is not null)
                {
                    if (RepositorioMarcas.Instance.Eliminar(Marca.MarcaId).Result)
                        return $"La Marca {Marca.NombreMarca} se eliminó correctamente!";
                    else
                        return $"La Marca {Marca.NombreMarca} no se ha podido eliminar!";
                }
                else
                    return $"La Marca {Marca.NombreMarca} no existe!";
            }
            catch (Exception)
            {
                return "Error desconocido";
            }
        }
        #endregion
    }
}
