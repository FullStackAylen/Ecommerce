using Model;
using Model.Repositories;
using Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Controller
{
    public class ControladoraUsuarios
    {
        #region private atributtes
        private static ControladoraUsuarios instance;
        #endregion
        private ControladoraUsuarios() { }
        #region instance
        public static ControladoraUsuarios Instance
        {
            get
            {
                if (instance == null)
                    instance = new ControladoraUsuarios();
                return instance;
            }
        }
        #endregion

        #region logic methods
        public async Task<List<Usuario>> ListadoUsuarios() => await RepositorioUsuarios.Instance.Listar();
        public async Task<Usuario> UsuarioID(int id) => await RepositorioUsuarios.Instance.ObtenerUsuarioID(id);

        public async Task<string> AgregarUsuario(Usuario usuario)
        {
            try
            {
                if (RepositorioUsuarios.Instance.Listar().Result.FirstOrDefault(x => x.Nombre == usuario.Nombre) is null)
                {
                    if (RepositorioUsuarios.Instance.Agregar(usuario).Result)
                    {
                        return $"El usuario {usuario.Nombre} se agregó correctamente!";
                    }
                    else return $"El usuario {usuario.Nombre} no se ha podido agregar!";
                }
                else return $"El usuario {usuario.Nombre} ya existe!";
            }
            catch (Exception)
            {
                return "Error desconocido.";
            }
        }
        public async Task<string> EditarUsuario(Usuario usuario)
        {
            try
            {
                if (RepositorioUsuarios.Instance.ObtenerUsuarioID(usuario.UsuarioId) is not null)
                {
                    if (RepositorioUsuarios.Instance.Editar(usuario).Result)
                        return $"El usuario {usuario.Nombre} se modifico correctamente!";
                    else
                        return $"El usuario {usuario.Nombre} no se ha podido modificar!";
                }
                else return $"El usuario {usuario.Nombre} no existe!";
            }
            catch (Exception)
            {
                return "Error desconocido.";
            }
        }
        public async Task<string> EliminarUsuario(Usuario usuario)
        {
            try
            {
                if (RepositorioUsuarios.Instance.ObtenerUsuarioID(usuario.UsuarioId) is not null)
                {
                    if (RepositorioUsuarios.Instance.Eliminar(usuario.UsuarioId).Result)
                        return $"El usuario {usuario.Nombre} se eliminó correctamente!";
                    else
                        return $"El usuario {usuario.Nombre} no se ha podido eliminar!";
                }
                else
                    return $"El usuario {usuario.Nombre} no existe!";
            }
            catch (Exception)
            {
                return "Error desconocido";
            }
        }
        #endregion
    }
}
