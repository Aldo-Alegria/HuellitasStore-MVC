using System;
using System.Collections.Generic;

namespace ProyectoDesarrollo.B.P.Models;

public partial class TbCategoria
{
    public string CodCategoria { get; set; } = null!;

    public string NomCat { get; set; } = null!;

    public virtual ICollection<TbProducto> TbProductos { get; set; } = new List<TbProducto>();
}
