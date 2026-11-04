using System.Text.Json;
using Microsoft.AspNetCore.Http;
using ProyectoDesarrollo.B.P.Models;

namespace ProyectoDesarrollo.B.P.Services;

public static class CarritoSesion
{
    private const string Clave = "CarritoProductosV2";
    public static List<TemporalVenta> Obtener(ISession sesion)
    {
        var datos = sesion.GetString(Clave);
        return string.IsNullOrEmpty(datos) ? new List<TemporalVenta>()
            : JsonSerializer.Deserialize<List<TemporalVenta>>(datos) ?? new List<TemporalVenta>();
    }
    public static void Guardar(ISession sesion, List<TemporalVenta> productos) =>
        sesion.SetString(Clave, JsonSerializer.Serialize(productos));
    public static void Limpiar(ISession sesion) => sesion.Remove(Clave);
}
