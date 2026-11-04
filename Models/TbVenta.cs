using System;
using System.Collections.Generic;

namespace ProyectoDesarrollo.B.P.Models;

public partial class TbVenta
{
    public string CodVenta { get; set; } = null!;

    public string CodCliente { get; set; } = null!;

    public DateTime FecVenta { get; set; }

    public decimal TotalVenta { get; set; }

    public string EstadoVenta { get; set; } = null!;

    public virtual TbCliente CodClienteNavigation { get; set; } = null!;

    public virtual ICollection<TbDetalleVenta> TbDetalleventa { get; set; } = new List<TbDetalleVenta>();
}
