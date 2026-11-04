using System;
using System.Collections.Generic;

namespace ProyectoDesarrollo.B.P.Models;

public partial class TbUsuario
{
    public int Id { get; set; }

    public string UsuNombre { get; set; } = null!;

    public string UsuClave { get; set; } = null!;
    
}
