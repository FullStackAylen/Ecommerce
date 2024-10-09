using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;
using Model;
using Model.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories
{
    public class RepositorioProductos : IRepositorioProducto
    {
        #region Private attributes
        private static RepositorioProductos instance;
        private readonly EcommerceContext context;
        #endregion
        private RepositorioProductos() => context = new EcommerceContext();
        #region instance..
        public static RepositorioProductos Instance
        {
            get
            {
                if (instance is null)
                    instance = new RepositorioProductos();
                return instance;
            }
        }
        #endregion

        #region Pubic methods..
        public async Task<List<ProductosMaquillaje>> Listar() => await context.ProductosMaquillajes.Include(x=>x.Marca).Include(x=>x.Categoria).ToListAsync();
        public async Task<ProductosMaquillaje> ObtenerProductoID(int id) => await context.ProductosMaquillajes.FindAsync(id) ?? null;
        public async Task<bool> Agregar(ProductosMaquillaje product)
        {
            await context.ProductosMaquillajes.AddAsync(product);

            return await context.SaveChangesAsync() > 0;
        }
        public async Task<bool> Editar(ProductosMaquillaje productos)
        {
            context.ProductosMaquillajes.Update(productos);
            return await context.SaveChangesAsync() > 0;
        }
        public async Task<bool> Eliminar(int id)
        {
            var prod = await context.ProductosMaquillajes.FindAsync(id);
            if (prod is not null)
            {
                context.ProductosMaquillajes.Remove(prod);
                return await context.SaveChangesAsync() > 0;
            }
            return false;
        }
        public async Task<List<ProductosMaquillaje>> ListadoProductosSinInventario()
        {
            // Suponiendo que tienes un contexto de base de datos llamado _context
            return await context.ProductosMaquillajes.Include(x => x.Marca).Include(x => x.Categoria)
                .Where(producto => !producto.Inventarios.Any())
                .ToListAsync();
        }

        #endregion
    }
}
