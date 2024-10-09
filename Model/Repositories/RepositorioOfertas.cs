using Microsoft.EntityFrameworkCore;
using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories
{
    public class RepositorioOfertas
    {
        #region private atributtes
        private static RepositorioOfertas instance;
        private readonly EcommerceContext context;
        #endregion
        private RepositorioOfertas() => context = new EcommerceContext();
        #region instance..
        public static RepositorioOfertas Instance
        {
            get
            {
                if (instance is null)
                    instance = new RepositorioOfertas();
                return instance;
            }
        }
        #endregion

        #region Pubic methods..
        public async Task<List<Oferta>> Listar() => await context.Ofertas.Include(x=>x.Producto).Include(x=>x.Producto.Marca).ToListAsync();
        public async Task<Oferta> ObtenerOfertaID(int id) => await context.Ofertas.FindAsync(id) ?? null;
        public async Task<bool> Agregar(Oferta Oferta)
        {
            await context.Ofertas.AddAsync(Oferta);

            return await context.SaveChangesAsync() > 0;
        }
        public async Task<bool> Editar(Oferta Oferta)
        {
            context.Ofertas.Update(Oferta);
            return await context.SaveChangesAsync() > 0;
        }
        public async Task<bool> Eliminar(int id)
        {
            var offer = await context.Ofertas.FindAsync(id);
            if (offer is not null)
            {
                context.Ofertas.Remove(offer);
                return await context.SaveChangesAsync() > 0;
            }
            return false;
        }
        #endregion
    }
}
