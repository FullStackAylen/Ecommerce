using Model.Repositories;
using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Repositories
{
    public class RepositorioMarcas
    {
        #region private atributtes
        private static RepositorioMarcas instance;
        private readonly EcommerceContext context;
        #endregion
        private RepositorioMarcas() => context = new EcommerceContext();
        #region instance..
        public static RepositorioMarcas Instance
        {
            get
            {
                if (instance is null)
                    instance = new RepositorioMarcas();
                return instance;
            }
        }
        #endregion

        #region Pubic methods..
        public async Task<List<Marca>> Listar() => await context.Marcas.ToListAsync();
        public async Task<Marca> ObtenerMarcaID(int id) => await context.Marcas.FindAsync(id) ?? null;
        public async Task<bool> Agregar(Marca marca)
        {
            await context.Marcas.AddAsync(marca);

            return await context.SaveChangesAsync() > 0;
        }
        public async Task<bool> Editar(Marca marca)
        {
            context.Marcas.Update(marca);
            return await context.SaveChangesAsync() > 0;
        }
        public async Task<bool> Eliminar(int id)
        {
            var brand = await context.Marcas.FindAsync(id);
            if (brand is not null)
            {
                await context.ProductosMaquillajes.Where(x => x.MarcaId == id).ForEachAsync(prod =>
                {
                    context.ProductosMaquillajes.Remove(prod);
                    context.SaveChangesAsync();
                });
                context.Marcas.Remove(brand);
                return await context.SaveChangesAsync() > 0;
            }
            return false;
        }
        #endregion
    }
}
