using Interfaces;
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
    public class RepositorioCategorias 
    {
        #region private atributtes
        private static RepositorioCategorias instance;
        private readonly EcommerceContext context;
        #endregion
        private RepositorioCategorias() => context = new EcommerceContext();
        #region instance..
        public static RepositorioCategorias Instance
        {
            get
            {
                if (instance is null)
                    instance = new RepositorioCategorias();
                return instance;
            }
        }
        #endregion

        #region Pubic methods..
        public async Task<List<Categoria>> Listar() => await context.Categorias.ToListAsync();
        public async Task<Categoria> ObtenerCategoriaID(int id) => await context.Categorias.FindAsync(id) ?? null;
        public async Task<bool> Agregar(Categoria categoria)
        {
            await context.Categorias.AddAsync(categoria);

            return await context.SaveChangesAsync() > 0;
        }
        public async Task<bool> Editar(Categoria categoria)
        {
            context.Categorias.Update(categoria);
            return await context.SaveChangesAsync() > 0;
        }
        public async Task<bool> Eliminar(int id)
        {
            var cat = await context.Categorias.FindAsync(id);
            if (cat is not null)
            {
                var productos = context.ProductosMaquillajes.Where(x => x.CategoriaId == id).ToListAsync();

                productos.Result.ForEach(x => {
                    context.ProductosMaquillajes.Remove(x);
                    context.SaveChangesAsync();
                    });

                context.Categorias.Remove(cat);
                return await context.SaveChangesAsync() > 0;
            }
            return false;
        }
        #endregion
    }
}
