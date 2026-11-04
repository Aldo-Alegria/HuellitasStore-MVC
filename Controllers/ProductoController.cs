using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProyectoDesarrollo.B.P.Models;

namespace ProyectoDesarrollo.B.P.Controllers;

public class ProductoController : Controller
{
    private readonly VentasC _db;
    public ProductoController(VentasC db) => _db = db;
    public IActionResult Index() => RedirectToAction(nameof(Catalogo));
    public async Task<IActionResult> Catalogo(string? buscar, string? categoria)
    {
        var productos = _db.TbProductos.AsNoTracking().Where(p => p.EstaProducto == "Activo");
        if (!string.IsNullOrWhiteSpace(buscar)) productos = productos.Where(p => p.NomProducto.Contains(buscar));
        if (!string.IsNullOrWhiteSpace(categoria)) productos = productos.Where(p => p.CodCategoria == categoria);
        ViewBag.Buscar = buscar;
        ViewBag.Categoria = categoria;
        ViewBag.Categorias = await _db.TbCategorias.AsNoTracking().OrderBy(c => c.NomCat).ToListAsync();
        return View(await productos.OrderBy(p => p.NomProducto).ToListAsync());
    }
    [HttpGet("Producto/Comprar/{codigo}")]
    public async Task<IActionResult> Comprar(string codigo)
    {
        var producto = await _db.TbProductos.AsNoTracking().FirstOrDefaultAsync(p => p.CodProducto == codigo);
        if (producto == null) return NotFound();
        return View(producto);
    }
}
