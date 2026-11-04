using Microsoft.Data.SqlClient;
using ProyectoDesarrollo.B.P.Models;
using static System.Net.Mime.MediaTypeNames;
using System;
using Microsoft.EntityFrameworkCore;
using ProyectoDesarrollo.B.P.Services;

public class DVentaRepository : IDVenta
    {
        private VentasC conexion = new VentasC();

    public void addBb(TbVenta objV)
    {
        conexion.TbVentas.Add(objV);
        conexion.SaveChanges();

    }
    public void addBa(TbDetalleVenta obj)
    {
        conexion.TbDetalleventas.Add(obj);
        conexion.SaveChanges();
    }

    public IEnumerable<TbDetalleVenta> GetDetalleVentas()
        {
            return conexion.TbDetalleventas;
        }

        public string GetUltimoCodigoVenta()
        {
            using (var context = new VentasC()) // Reemplaza "TuContextoDeBaseDeDatos" con el nombre de tu contexto de base de datos
            {
                var ultimoDetalleVenta = context.TbDetalleventas
                    .OrderByDescending(d => d.CodVenta)
                    .FirstOrDefault();

                if (ultimoDetalleVenta != null)
                {
                    return ultimoDetalleVenta.CodVenta;
                }

                // Si no hay detalles de venta en la base de datos, puedes retornar un código predeterminado o lanzar una excepción, según tus necesidades.
                return "V01"; // Código predeterminado si no hay detalles de venta en la base de datos
            }
        }
}



