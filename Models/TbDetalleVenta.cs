using System;
using System.Collections.Generic;

namespace ProyectoDesarrollo.B.P.Models;

public partial class TbDetalleVenta
{
    public string CodVenta { get; set; } = null!;

    public string CodProducto { get; set; } = null!;

    public int Cantidad { get; set; }

    public decimal Precio { get; set; }

    public virtual TbProducto CodProductoNavigation { get; set; } = null!;

    public virtual TbVenta CodVentaNavigation { get; set; } = null!;
}
