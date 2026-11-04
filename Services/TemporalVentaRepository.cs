using Microsoft.AspNetCore.Mvc;
using ProyectoDesarrollo.B.P.Models;
using System.Linq;
namespace ProyectoDesarrollo.B.P.Services
{
    public class TemporalVentaRepository : iTemporalVenta
    {
        private List<TemporalVenta> lista = new List<TemporalVenta>();  
      
       
        public void agregar(TemporalVenta temporalVenta)
        {
         
            lista.Add(temporalVenta);
        }

        public IEnumerable<TemporalVenta> ListarTemporalVenta()
        {
            return lista;
        }

        public int ObtenerProductoPorCodigo(string codigo)
        {
            foreach (var producto in lista)
            {
                if (producto.codigo == codigo)
                {
                    return 1;
                }
            }

            return -1;
        }

        public int ObtenerIndexPorCodigo(string codigo)
        {
            
            for (int i = 0; i < lista.Count; i++)
            {
                if (lista[i].codigo == codigo)
                {
                    return i;
                }
            }
            return -1; // Product not found
        }

        public void AumentarCantidadProducto(string codigo, int cantidad)
        {
            int productoExistenteIndex = ObtenerIndexPorCodigo(codigo);

            if (productoExistenteIndex != -1)
            {
                // Increase the quantity of the existing product
                lista[productoExistenteIndex].cantidad += cantidad;
            }
        }

        public void EliminarProducto(string codigo)
        {
            // Busca el índice del producto a eliminar en el carrito
            int indiceProductoAEliminar = -1;
            for (int i = 0; i < lista.Count; i++)
            {
                if (lista[i].codigo == codigo)
                {
                    indiceProductoAEliminar = i;
                    break;
                }
            }

        
            if (indiceProductoAEliminar != -1)
            {
                
                lista.RemoveAt(indiceProductoAEliminar);
            }

        }

    }
}
    


