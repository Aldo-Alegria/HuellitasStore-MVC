using ProyectoDesarrollo.B.P.Models;

namespace ProyectoDesarrollo.B.P.Services
{
    public interface IDVenta
    {
        void addBb(TbVenta nuevaVenta);
        void addBa(TbDetalleVenta tbDetalle);
        IEnumerable<TbDetalleVenta> GetDetalleVentas();
        public string GetUltimoCodigoVenta();
    }
}
