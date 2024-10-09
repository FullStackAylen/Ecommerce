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
    public class ControladoraOfertas
    {
        #region private atributtes
        private static ControladoraOfertas instance;
        #endregion
        private ControladoraOfertas() { }
        #region instance
        public static ControladoraOfertas Instance
        {
            get
            {
                if (instance == null)
                    instance = new ControladoraOfertas();
                return instance;
            }
        }
        #endregion

        #region logic methods
        public async Task<List<Oferta>> ListadoOfertas() => await RepositorioOfertas.Instance.Listar();
        public async Task<Oferta> OfertaID(int id) => await RepositorioOfertas.Instance.ObtenerOfertaID(id);

        public async Task<string> AgregarOferta(Oferta Oferta)
        {
            try
            {
                if (RepositorioOfertas.Instance.Listar().Result.FirstOrDefault(x => x.ProductoId == Oferta.ProductoId) is null)
                {
                    if (RepositorioOfertas.Instance.Agregar(Oferta).Result)
                    {
                        return $"La Oferta para {Oferta.Producto.Nombre} se agregó correctamente!";
                    }
                    else return $"La Oferta para {Oferta.Producto.Nombre} no se ha podido agregar!";
                }
                else return $"La Oferta de {Oferta.Producto.Nombre} ya existe!";
            }
            catch (Exception)
            {
                return "Error desconocido.";
            }
        }
        public async Task<string> EditarOferta(Oferta Oferta)
        {
            try
            {
                if (RepositorioOfertas.Instance.ObtenerOfertaID(Oferta.OfertaId) is not null)
                {
                    if (RepositorioOfertas.Instance.Editar(Oferta).Result)
                        return $"La Oferta para {Oferta.Producto.Nombre} se modifico correctamente!";
                    else
                        return $"La Oferta para {Oferta.Producto.Nombre} no se ha podido modificar!";
                }
                else return $"La Oferta de {Oferta.Producto.Nombre} no existe!";
            }
            catch (Exception)
            {
                return "Error desconocido.";
            }
        }
        public async Task<string> EliminarOferta(Oferta Oferta)
        {
            try
            {
                if (RepositorioOfertas.Instance.ObtenerOfertaID(Oferta.OfertaId) is not null)
                {
                    if (RepositorioOfertas.Instance.Eliminar(Oferta.OfertaId).Result)
                        return $"La Oferta de {Oferta.Producto.Nombre} se eliminó correctamente!";
                    else
                        return $"La Oferta de {Oferta.Producto.Nombre} no se ha podido eliminar!";
                }
                else
                    return $"La Oferta de {Oferta.Producto.Nombre} no existe!";
            }
            catch (Exception)
            {
                return "Error desconocido";
            }
        }
        #endregion
    }
}
