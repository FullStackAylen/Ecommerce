using Microsoft.EntityFrameworkCore;
using Model;
using Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories
{
    public class RepositorioInventarios
    {
        #region private atributtes
        private static RepositorioInventarios instance;
        private readonly EcommerceContext context;
        #endregion
        private RepositorioInventarios() => context = new EcommerceContext();
        #region instance..
        public static RepositorioInventarios Instance
        {
            get
            {
                if (instance is null)
                    instance = new RepositorioInventarios();
                return instance;
            }
        }
        #endregion

        #region Pubic methods..
        public async Task<List<Inventario>> Listar() => await context.Inventarios.Include(x=>x.Producto).Include(x=>x.Producto.Marca).ToListAsync();
        public async Task<Inventario> ObtenerInventarioID(int id) => await context.Inventarios.FindAsync(id) ?? null;
        public async Task<bool> Agregar(Inventario inventario)
        {
            await context.Inventarios.AddAsync(inventario);

            return await context.SaveChangesAsync() > 0;
        }
        public async Task<bool> Editar(Inventario inventario)
        {
            context.Inventarios.Update(inventario);
            return await context.SaveChangesAsync() > 0;
        }
        public async Task<bool> Eliminar(int id)
        {
            var inventory = await context.Inventarios.FindAsync(id);
            if (inventory is not null)
            {
                context.Inventarios.Remove(inventory);
                return await context.SaveChangesAsync() > 0;
            }
            return false;
        }
        #endregion
    }
}
