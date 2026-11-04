using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProyectoDesarrollo.B.P.Models;
using ProyectoDesarrollo.B.P.Services;
using System.Data;

namespace ProyectoDesarrollo.B.P.Controllers;

public class TemporalVentaController : Controller
{
    private readonly VentasC _db;
    public TemporalVentaController(VentasC db) => _db = db;

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Agregar(string codigo, int cantidad = 1)
    {
        var producto = await _db.TbProductos.AsNoTracking().FirstOrDefaultAsync(p => p.CodProducto == codigo);
        if (producto == null || producto.EstaProducto != "Activo" || producto.StkProducto <= 0)
        { TempData["Error"] = "El producto no está disponible."; return RedirectToAction("Catalogo", "Producto"); }
        if (cantidad <= 0)
        { TempData["Error"] = "Ingresa una cantidad mayor a cero."; return RedirectToAction("Catalogo", "Producto"); }
        var carrito = CarritoSesion.Obtener(HttpContext.Session);
        var item = carrito.FirstOrDefault(x => x.codigo == codigo);
        var solicitada = cantidad + (item?.cantidad ?? 0);
        if (solicitada > producto.StkProducto)
        { TempData["Error"] = $"Stock disponible: {producto.StkProducto} unidades."; return RedirectToAction("Catalogo", "Producto"); }
        if (item == null)
            carrito.Add(new TemporalVenta { codigo = producto.CodProducto, nombre = producto.NomProducto, precio = (double)producto.PreProducto, cantidad = cantidad });
        else { item.cantidad = solicitada; item.precio = (double)producto.PreProducto; }
        CarritoSesion.Guardar(HttpContext.Session, carrito);
        TempData["Exito"] = "Producto agregado al carrito.";
        return RedirectToAction(nameof(VerCarrito));
    }

    // Compatibilidad con la vista antigua que enviaba txtcodigo y txtcantidad.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public Task<IActionResult> Index(string txtcodigo, string? txtdescripcion, string? txtprecio, int txtcantidad = 1) =>
        Agregar(txtcodigo, txtcantidad);

    [HttpGet]
    public IActionResult VerCarrito() => View(CarritoSesion.Obtener(HttpContext.Session));

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Actualizar(string codigo, int cantidad)
    {
        var carrito = CarritoSesion.Obtener(HttpContext.Session);
        var item = carrito.FirstOrDefault(x => x.codigo == codigo);
        if (item == null) return RedirectToAction(nameof(VerCarrito));
        if (cantidad <= 0) { carrito.Remove(item); }
        else
        {
            var producto = await _db.TbProductos.AsNoTracking().FirstOrDefaultAsync(p => p.CodProducto == codigo);
            if (producto == null || producto.EstaProducto != "Activo" || cantidad > producto.StkProducto)
            { TempData["Error"] = "La cantidad supera el stock disponible."; return RedirectToAction(nameof(VerCarrito)); }
            item.cantidad = cantidad;
            item.precio = (double)producto.PreProducto;
        }
        CarritoSesion.Guardar(HttpContext.Session, carrito);
        return RedirectToAction(nameof(VerCarrito));
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult EliminarProducto(string codigo)
    {
        var carrito = CarritoSesion.Obtener(HttpContext.Session);
        carrito.RemoveAll(x => x.codigo == codigo);
        CarritoSesion.Guardar(HttpContext.Session, carrito);
        return RedirectToAction(nameof(VerCarrito));
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> ConfirmarPedido()
    {
        var carrito = CarritoSesion.Obtener(HttpContext.Session);
        if (carrito.Count == 0) { TempData["Error"] = "Tu carrito está vacío."; return RedirectToAction(nameof(VerCarrito)); }
        await using var tx = await _db.Database.BeginTransactionAsync(IsolationLevel.Serializable);
        try
        {
            var cliente = await _db.TbClientes.AnyAsync(x => x.CodCliente == "CL01");
            if (!cliente) throw new InvalidOperationException("No existe el cliente de prueba CL01.");
            decimal total = 0;
            var detalles = new List<TbDetalleVenta>();
            foreach (var item in carrito)
            {
                var producto = await _db.TbProductos.FirstOrDefaultAsync(x => x.CodProducto == item.codigo);
                if (producto == null || producto.EstaProducto != "Activo" || item.cantidad <= 0 || producto.StkProducto < item.cantidad)
                    throw new InvalidOperationException($"Stock insuficiente para {item.nombre}. Actualiza el carrito.");
                producto.StkProducto -= item.cantidad;
                var precio = decimal.Round(producto.PreProducto, 2);
                total += precio * item.cantidad;
                detalles.Add(new TbDetalleVenta { CodProducto = producto.CodProducto, Cantidad = item.cantidad, Precio = precio });
            }
            var codigos = await _db.TbVentas.Select(v => v.CodVenta).ToListAsync();
            var ultimo = codigos.Where(c => c.StartsWith("V") && int.TryParse(c.Substring(1), out _))
                .Select(c => int.Parse(c.Substring(1))).DefaultIfEmpty(0).Max();
            if (ultimo >= 9999) throw new InvalidOperationException("Se agotó la numeración de pedidos.");
            var codigoVenta = $"V{ultimo + 1:0000}";
            var venta = new TbVenta { CodVenta = codigoVenta, CodCliente = "CL01", FecVenta = DateTime.Today,
                TotalVenta = decimal.Round(total, 2), EstadoVenta = "Registrado" };
            foreach (var detalle in detalles) detalle.CodVenta = codigoVenta;
            _db.TbVentas.Add(venta);
            _db.TbDetalleventas.AddRange(detalles);
            await _db.SaveChangesAsync();
            await tx.CommitAsync();
            CarritoSesion.Limpiar(HttpContext.Session);
            TempData["Exito"] = $"Pedido de demostración {codigoVenta} registrado correctamente. No se procesó ningún pago.";
            return RedirectToAction(nameof(VerCarrito));
        }
        catch (Exception ex)
        {
            await tx.RollbackAsync();
            TempData["Error"] = ex is DbUpdateException ? "No se pudo guardar el pedido en SQL Server." : ex.Message;
            return RedirectToAction(nameof(VerCarrito));
        }
    }

    public IActionResult VerDetalle() => RedirectToAction(nameof(VerCarrito));
    public IActionResult VerCombo() => RedirectToAction("Catalogo", "Producto");
}
