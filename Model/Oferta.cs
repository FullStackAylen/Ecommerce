using System;
using System.Collections.Generic;

namespace Model;

public partial class Oferta
{
    public int OfertaId { get; set; }

    public int? ProductoId { get; set; }

    public decimal? DescuentoPorcentaje { get; set; }

    public DateOnly? FechaInicio { get; set; }

    public DateOnly? FechaFin { get; set; }

    public string? DescripcionOferta { get; set; }

    public virtual ProductosMaquillaje? Producto { get; set; }
}
