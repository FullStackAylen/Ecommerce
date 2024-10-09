using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Interfaces
{
    public interface IRepositorioUsuario
    {
        Task<List<Usuario>> Listar();
        Task<Usuario> ObtenerUsuarioID(int id);
        Task<bool> Agregar(Usuario usuario);
        Task<bool> Editar(Usuario usuario);
        Task<bool> Eliminar(int id);
    }
}
