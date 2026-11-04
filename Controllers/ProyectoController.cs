using Microsoft.AspNetCore.Mvc;
using ProyectoDesarrollo.B.P.Models;
using ProyectoDesarrollo.B.P.Services;

namespace ProyectoDesarrollo.B.P.Controllers
{
    public class ProyectoController : Controller
    {
        private readonly IUsuario _usuario;
        public ProyectoController(IUsuario usuario)
        {
            _usuario = usuario;
        }
        public IActionResult Index()
        {
            return View();
        }
    
        public IActionResult Registro()
        {
            return View();
        }
        public IActionResult Listar(Usuario usuario)
        {
            if (ModelState.IsValid)
            {
                _usuario.Add(usuario);
                var lst = _usuario.GellAllUser();
                return View(lst);
            }
            else
            {
                return View("Registro");
            }
        }
		[Route("usuario/cargar/{codigo}")]
		public IActionResult Cargar(string codigo)
		{
			ViewData["cod"] = codigo;
			return View();
		}
        public IActionResult RegistroProducto()
        {
            return View();
        }
        public IActionResult Pagar()
        {
            return View();
        }
        public IActionResult Login()
        {
            return View();
        }
        public IActionResult Principal()
        {
            return RedirectToAction("Catalogo", "Producto");
        }
    }

}
