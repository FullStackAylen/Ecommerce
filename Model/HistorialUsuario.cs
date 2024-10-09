using System;
using System.Collections.Generic;

namespace Model;

public partial class HistorialUsuario
{
    public int HistorialUsuarioId { get; set; }

    public int UsuarioId { get; set; }

    public string Token { get; set; } = null!;

    public DateTime InicioSesion { get; set; }

    public DateTime? FinSesion { get; set; }

    public virtual Usuario Usuario { get; set; } = null!;
}
