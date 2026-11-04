using ProyectoDesarrollo.B.P.Models;

namespace ProyectoDesarrollo.B.P.Services
{
    public class VentaRepository: IVenta
    {
        private VentasC conexion = new VentasC();
        public void addB(TbVenta obj)
        {
            conexion.TbVentas.Add(obj);
            conexion.SaveChanges();
        }

        public IEnumerable<TbVenta> GetDetalleVentas()
        {
            return conexion.TbVentas;
        }
    }
}
