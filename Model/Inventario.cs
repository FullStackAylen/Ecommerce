using System;
using System.Collections.Generic;

namespace Model;

public partial class Inventario
{
    public int InventarioId { get; set; }

    public int? ProductoId { get; set; }

    public int Cantidad { get; set; }

    public string? Ubicacion { get; set; }

    public DateOnly? FechaIngreso { get; set; }

    public virtual ProductosMaquillaje? Producto { get; set; }
}
