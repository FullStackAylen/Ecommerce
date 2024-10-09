using System;
using System.Collections.Generic;

namespace Model;

public partial class CarritoCompra
{
    public int CarritoId { get; set; }

    public int? UsuarioId { get; set; }

    public int? ProductoId { get; set; }

    public int Cantidad { get; set; }

    public DateOnly? FechaAgregado { get; set; }

    public virtual ProductosMaquillaje? Producto { get; set; }

    public virtual Usuario? Usuario { get; set; }
}
