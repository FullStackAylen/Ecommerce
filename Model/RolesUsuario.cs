using System;
using System.Collections.Generic;

namespace Model;

public partial class RolesUsuario
{
    public int RolId { get; set; }

    public string Nombre { get; set; } = null!;

    public virtual ICollection<Usuario> Usuarios { get; set; } = new List<Usuario>();
}
