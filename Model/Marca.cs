using System;
using System.Collections.Generic;

namespace Model;

public partial class Marca
{
    public int MarcaId { get; set; }

    public string NombreMarca { get; set; } = null!;

    public virtual ICollection<ProductosMaquillaje> ProductosMaquillajes { get; set; } = new List<ProductosMaquillaje>();
}
