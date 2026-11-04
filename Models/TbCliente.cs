using System;
using System.Collections.Generic;

namespace ProyectoDesarrollo.B.P.Models;

public partial class TbCliente
{
    public string CodCliente { get; set; } = null!;

    public string NomCli { get; set; } = null!;

    public string ApeCli { get; set; } = null!;

    public string DirCli { get; set; } = null!;

    public string? Dni { get; set; }

    public virtual ICollection<TbVenta> TbVenta { get; set; } = new List<TbVenta>();
}
