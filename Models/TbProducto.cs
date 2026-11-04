using System;
using System.Collections.Generic;

namespace ProyectoDesarrollo.B.P.Models;

public partial class TbProducto
{
    public string CodProducto { get; set; } = null!;

    public string NomProducto { get; set; } = null!;

    public decimal PreProducto { get; set; }

    public int StkProducto { get; set; }

    public string EstaProducto { get; set; } = null!;

    public string CodCategoria { get; set; } = null!;

    public string? Imagenes { get; set; }

    public virtual TbCategoria CodCategoriaNavigation { get; set; } = null!;

    public virtual ICollection<TbDetalleVenta> TbDetalleventa { get; set; } = new List<TbDetalleVenta>();
}
