using System;
using System.Collections.Generic;

namespace Model;

public partial class ProductosMaquillaje
{
    public int ProductoId { get; set; }

    public string Nombre { get; set; } = null!;

    public int? MarcaId { get; set; }

    public int? CategoriaId { get; set; }

    public decimal Precio { get; set; }

    public string? Tono { get; set; }

    public string? Descripcion { get; set; }

    public string? Ingredientes { get; set; }

    public string? TipoPiel { get; set; }

    public DateOnly? FechaCreacion { get; set; }

    public int? StockDisponible { get; set; }

    public string? Status { get; set; }

    public virtual ICollection<CarritoCompra> CarritoCompras { get; set; } = new List<CarritoCompra>();

    public virtual Categoria? Categoria { get; set; }

    public virtual ICollection<Inventario> Inventarios { get; set; } = new List<Inventario>();

    public virtual Marca? Marca { get; set; }

    public virtual ICollection<Oferta> Oferta { get; set; } = new List<Oferta>();
}
