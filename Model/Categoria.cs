using System;
using System.Collections.Generic;

namespace Model;

public partial class Categoria
{
    public int CategoriaId { get; set; }

    public string NombreCategoria { get; set; } = null!;

    public virtual ICollection<ProductosMaquillaje> ProductosMaquillajes { get; set; } = new List<ProductosMaquillaje>();
}
