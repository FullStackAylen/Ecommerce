using Model;
using Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Controller
{
    public class ControladoraHistorialUsuarios
    {
        #region private atributtes
        private static ControladoraHistorialUsuarios instance;
        #endregion
        private ControladoraHistorialUsuarios() { }
        #region instance
        public static ControladoraHistorialUsuarios Instance
        {
            get
            {
                if (instance == null)
                    instance = new ControladoraHistorialUsuarios();
                return instance;
            }
        }
        #endregion
        #region logic methods
        public async Task<List<HistorialUsuario>> ListadoHistorial() => await RepositorioHistorialUsuario.Instance.Listar();
        public async Task<string> AgregarHistorial(HistorialUsuario historial)
        {
            try
            {
                if (RepositorioHistorialUsuario.Instance.Listar().Result.FirstOrDefault(x => x.UsuarioId == historial.UsuarioId) is null)
                {
                    if (RepositorioHistorialUsuario.Instance.Agregar(historial).Result)
                    {
                        return $"Usuario logueado correctamente";
                    }
                    else return $"El usuario no se ha podido loguear";
                }
                else return $"El usuario ya se encuentra logueado!";
            }
            catch (Exception)
            {
                return "Error desconocido.";
            }
        }
        public async Task<string> EditarHistorial(HistorialUsuario historial)
        {
            try
            {
                if (RepositorioHistorialUsuario.Instance.Listar().Result.FirstOrDefault(x => x.UsuarioId == historial.UsuarioId) is not null)
                {
                    if (RepositorioHistorialUsuario.Instance.Editar(historial).Result)
                        return $"El historial se modifico correctamente!";
                    else
                        return $"El historial no se ha podido modificar!";
                }
                else return $"El historial no existe!";
            }
            catch (Exception)
            {
                return "Error desconocido.";
            }
        }
        #endregion
    }
}
