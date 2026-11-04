using Microsoft.AspNetCore.Mvc;
using ProyectoDesarrollo.B.P.Models;

namespace ProyectoDesarrollo.B.P.Services
{
    public interface iTemporalVenta
    {
        void agregar(TemporalVenta temporalVenta);
        IEnumerable<TemporalVenta> ListarTemporalVenta();

        int  ObtenerProductoPorCodigo(string codigo);
        void AumentarCantidadProducto(string codigo, int cantidad);

        int ObtenerIndexPorCodigo(string codigo);

        void EliminarProducto(string codigo);

    }
}
