using ProyectoDesarrollo.B.P.Models;

namespace ProyectoDesarrollo.B.P.Services
{
    public interface iProducto
    {
        IEnumerable<TbProducto> MostrarTodosProductos();
        TbProducto GetaProducto(string id);
    }
}
