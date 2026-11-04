using ProyectoDesarrollo.B.P.Models;
using System.Reflection.Metadata.Ecma335;

namespace ProyectoDesarrollo.B.P.Services
{
    public class ProductoRepository : iProducto
    {
        private VentasC conexion = new VentasC();
        public IEnumerable<TbProducto> MostrarTodosProductos()
        {
            return conexion.TbProductos;
        }
        public TbProducto GetaProducto(string id)
        {
            var objEncontrado = (from tpro in conexion.TbProductos
                                 where tpro.CodProducto == id
                                 select tpro).Single();

            return objEncontrado; 
        }

        
    }
}
