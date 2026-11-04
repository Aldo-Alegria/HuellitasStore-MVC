using ProyectoDesarrollo.B.P.Models;

namespace ProyectoDesarrollo.B.P.Services
{
    public interface IVenta
    {
        void addB(TbVenta obj);
        IEnumerable<TbVenta> GetDetalleVentas();
    }
}
