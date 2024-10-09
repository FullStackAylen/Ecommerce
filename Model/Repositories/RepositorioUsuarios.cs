using Interfaces;
using Microsoft.EntityFrameworkCore;
using Model.Interfaces;
using Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Repositories
{
    public class RepositorioUsuarios : IRepositorioUsuario
    {
        #region private atributtes
        private static RepositorioUsuarios instance;
        private readonly EcommerceContext context;
        #endregion
        private RepositorioUsuarios() => context = new EcommerceContext();
        #region instance..
        public static RepositorioUsuarios Instance
        {
            get
            {
                if (instance is null)
                    instance = new RepositorioUsuarios();
                return instance;
            }
        }
        #endregion

        #region Pubic methods..
        public async Task<List<Usuario>> Listar() => await context.Usuarios.Include(x=>x.Rol).ToListAsync();
        public async Task<Usuario> ObtenerUsuarioID(int id) => await context.Usuarios.FindAsync(id) ?? null;
        public async Task<bool> Agregar(Usuario usuario)
        {
            await context.Usuarios.AddAsync(usuario);

            return await context.SaveChangesAsync() > 0;
        }
        public async Task<bool> Editar(Usuario usuario)
        {
            context.Usuarios.Update(usuario);
            return await context.SaveChangesAsync() > 0;
        }
        public async Task<bool> Eliminar(int id)
        {
            var user = await context.Usuarios.FindAsync(id);
            if (user is not null)
            {
                await context.CarritoCompras.Where(x => x.UsuarioId == id).ForEachAsync(carrito =>
                {
                    context.CarritoCompras.Remove(carrito);
                    context.SaveChangesAsync();
                });
                await context.HistorialUsuarios.Where(x => x.UsuarioId == id).ForEachAsync(histo =>
                {
                    context.HistorialUsuarios.Remove(histo);
                    context.SaveChangesAsync();
                });
                context.Usuarios.Remove(user);
                return await context.SaveChangesAsync() > 0;
            }
            return false;
        }
        #endregion
    }
}
