using System;
using System.Collections.Generic;

namespace Model;

public partial class Usuario
{
    public int UsuarioId { get; set; }

    public string Nombre { get; set; } = null!;

    public string Email { get; set; } = null!;

    public string Contrasena { get; set; } = null!;

    public DateOnly? FechaRegistro { get; set; }

    public int RolId { get; set; }

    public virtual ICollection<CarritoCompra> CarritoCompras { get; set; } = new List<CarritoCompra>();

    public virtual ICollection<HistorialUsuario> HistorialUsuarios { get; set; } = new List<HistorialUsuario>();

    public virtual RolesUsuario Rol { get; set; } = null!;
}
