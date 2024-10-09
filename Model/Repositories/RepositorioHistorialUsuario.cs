using Model.Repositories;
using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Interfaces;

namespace Repositories
{
    public class RepositorioHistorialUsuario : IRepositorioHistorialUsuario
    {
        #region private atributtes
        private static RepositorioHistorialUsuario instance;
        private readonly EcommerceContext context;
        #endregion
        private RepositorioHistorialUsuario() => context = new EcommerceContext();
        #region instance..
        public static RepositorioHistorialUsuario Instance
        {
            get
            {
                if (instance is null)
                    instance = new RepositorioHistorialUsuario();
                return instance;
            }
        }
        #endregion
        public async Task<List<HistorialUsuario>> Listar() => await context.HistorialUsuarios.Include(x=>x.Usuario).ToListAsync();

        public async Task<bool> Agregar(HistorialUsuario historial)
        {
            await context.HistorialUsuarios.AddAsync(historial);

            return await context.SaveChangesAsync() > 0;
        }
        public async Task<bool> Editar(HistorialUsuario historial)
        {
            context.HistorialUsuarios.Update(historial);
            return await context.SaveChangesAsync() > 0;
        }
    }
}
