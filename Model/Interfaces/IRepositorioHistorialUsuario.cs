using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Interfaces
{
    public interface IRepositorioHistorialUsuario
    {
        Task<List<HistorialUsuario>> Listar();
        Task<bool> Agregar(HistorialUsuario usuario);
        Task<bool> Editar(HistorialUsuario usuario);
    }
}
