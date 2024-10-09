using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace Model;

public partial class EcommerceContext : DbContext
{
    public EcommerceContext()
    {
    }

    public EcommerceContext(DbContextOptions<EcommerceContext> options)
        : base(options)
    {
    }

    public virtual DbSet<CarritoCompra> CarritoCompras { get; set; }

    public virtual DbSet<Categoria> Categorias { get; set; }

    public virtual DbSet<HistorialUsuario> HistorialUsuarios { get; set; }

    public virtual DbSet<Inventario> Inventarios { get; set; }

    public virtual DbSet<Marca> Marcas { get; set; }

    public virtual DbSet<Oferta> Ofertas { get; set; }

    public virtual DbSet<ProductosMaquillaje> ProductosMaquillajes { get; set; }

    public virtual DbSet<RolesUsuario> RolesUsuarios { get; set; }

    public virtual DbSet<Usuario> Usuarios { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=.\\SQLEXPRESS;Database=Ecommerce;User Id=sa;Password=Janus90;TrustServerCertificate=True");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<CarritoCompra>(entity =>
        {
            entity.HasKey(e => e.CarritoId).HasName("PK__CarritoC__778D580BCC6A79DA");

            entity.Property(e => e.CarritoId).HasColumnName("CarritoID");
            entity.Property(e => e.FechaAgregado).HasDefaultValueSql("(getdate())");
            entity.Property(e => e.ProductoId).HasColumnName("ProductoID");
            entity.Property(e => e.UsuarioId).HasColumnName("UsuarioID");

            entity.HasOne(d => d.Producto).WithMany(p => p.CarritoCompras)
                .HasForeignKey(d => d.ProductoId)
                .HasConstraintName("FK__CarritoCo__Produ__47DBAE45");

            entity.HasOne(d => d.Usuario).WithMany(p => p.CarritoCompras)
                .HasForeignKey(d => d.UsuarioId)
                .HasConstraintName("FK__CarritoCo__Usuar__48CFD27E");
        });

        modelBuilder.Entity<Categoria>(entity =>
        {
            entity.HasKey(e => e.CategoriaId).HasName("PK__Categori__F353C1C50FB39A6B");

            entity.Property(e => e.CategoriaId).HasColumnName("CategoriaID");
            entity.Property(e => e.NombreCategoria)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        modelBuilder.Entity<HistorialUsuario>(entity =>
        {
            entity.HasKey(e => e.HistorialUsuarioId).HasName("PK_SesionUsuarios");

            entity.ToTable("HistorialUsuario");

            entity.Property(e => e.HistorialUsuarioId).HasColumnName("HistorialUsuarioID");
            entity.Property(e => e.FinSesion).HasColumnType("datetime");
            entity.Property(e => e.InicioSesion).HasColumnType("datetime");
            entity.Property(e => e.UsuarioId).HasColumnName("UsuarioID");

            entity.HasOne(d => d.Usuario).WithMany(p => p.HistorialUsuarios)
                .HasForeignKey(d => d.UsuarioId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_SesionUsuarios_Usuarios");
        });

        modelBuilder.Entity<Inventario>(entity =>
        {
            entity.HasKey(e => e.InventarioId).HasName("PK__Inventar__FB8A24B71698AA11");

            entity.ToTable("Inventario");

            entity.Property(e => e.InventarioId).HasColumnName("InventarioID");
            entity.Property(e => e.FechaIngreso).HasDefaultValueSql("(getdate())");
            entity.Property(e => e.ProductoId).HasColumnName("ProductoID");
            entity.Property(e => e.Ubicacion)
                .HasMaxLength(100)
                .IsUnicode(false);

            entity.HasOne(d => d.Producto).WithMany(p => p.Inventarios)
                .HasForeignKey(d => d.ProductoId)
                .HasConstraintName("FK__Inventari__Produ__49C3F6B7");
        });

        modelBuilder.Entity<Marca>(entity =>
        {
            entity.HasKey(e => e.MarcaId).HasName("PK__Marcas__D5B1CDEBDF5DA227");

            entity.Property(e => e.MarcaId).HasColumnName("MarcaID");
            entity.Property(e => e.NombreMarca)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Oferta>(entity =>
        {
            entity.HasKey(e => e.OfertaId).HasName("PK__Ofertas__F2629BC937C3BFB7");

            entity.Property(e => e.OfertaId).HasColumnName("OfertaID");
            entity.Property(e => e.DescripcionOferta).HasColumnType("text");
            entity.Property(e => e.DescuentoPorcentaje).HasColumnType("decimal(5, 2)");
            entity.Property(e => e.ProductoId).HasColumnName("ProductoID");

            entity.HasOne(d => d.Producto).WithMany(p => p.Oferta)
                .HasForeignKey(d => d.ProductoId)
                .HasConstraintName("FK__Ofertas__Product__4AB81AF0");
        });

        modelBuilder.Entity<ProductosMaquillaje>(entity =>
        {
            entity.HasKey(e => e.ProductoId).HasName("PK__Producto__A430AE83264FE6C2");

            entity.ToTable("ProductosMaquillaje");

            entity.Property(e => e.ProductoId).HasColumnName("ProductoID");
            entity.Property(e => e.CategoriaId).HasColumnName("CategoriaID");
            entity.Property(e => e.Descripcion).HasColumnType("text");
            entity.Property(e => e.FechaCreacion).HasDefaultValueSql("(getdate())");
            entity.Property(e => e.Ingredientes).HasColumnType("text");
            entity.Property(e => e.MarcaId).HasColumnName("MarcaID");
            entity.Property(e => e.Nombre)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.Precio).HasColumnType("decimal(10, 2)");
            entity.Property(e => e.Status)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasDefaultValue("Disponible");
            entity.Property(e => e.TipoPiel)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Tono)
                .HasMaxLength(30)
                .IsUnicode(false);

            entity.HasOne(d => d.Categoria).WithMany(p => p.ProductosMaquillajes)
                .HasForeignKey(d => d.CategoriaId)
                .HasConstraintName("FK__Productos__Categ__4BAC3F29");

            entity.HasOne(d => d.Marca).WithMany(p => p.ProductosMaquillajes)
                .HasForeignKey(d => d.MarcaId)
                .HasConstraintName("FK__Productos__Marca__4CA06362");
        });

        modelBuilder.Entity<RolesUsuario>(entity =>
        {
            entity.HasKey(e => e.RolId);

            entity.ToTable("RolesUsuario");

            entity.Property(e => e.RolId).HasColumnName("RolID");
            entity.Property(e => e.Nombre).HasMaxLength(50);
        });

        modelBuilder.Entity<Usuario>(entity =>
        {
            entity.HasKey(e => e.UsuarioId).HasName("PK__Usuarios__2B3DE798E9F3FEC0");

            entity.HasIndex(e => e.Email, "UQ__Usuarios__A9D1053467F866C1").IsUnique();

            entity.Property(e => e.UsuarioId).HasColumnName("UsuarioID");
            entity.Property(e => e.Contrasena)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.Email)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.FechaRegistro).HasDefaultValueSql("(getdate())");
            entity.Property(e => e.Nombre)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.RolId)
                .HasDefaultValue(1)
                .HasColumnName("RolID");

            entity.HasOne(d => d.Rol).WithMany(p => p.Usuarios)
                .HasForeignKey(d => d.RolId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Usuarios_RolesUsuario");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
