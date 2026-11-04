using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using ProyectoDesarrollo.B.P.Models;
using ProyectoDesarrollo.B.P.Services;

namespace ProyectoDesarrollo.B.P.Controllers
{
    public class UsuarioController : Controller
    {

        // Version Mayerly
        private readonly IUsuario _usuario;

        public UsuarioController(IUsuario usuario)
        {
            _usuario = usuario;
           
        }

        public IActionResult Login()
        {
            return View();
        }

        public IActionResult GrabarDatos(Usuario obj)
        {
            _usuario.Add(obj);
            return RedirectToAction("ListarUsuarios");
        }

        public IActionResult ValidarUsuario(Usuario obj)
        {
            if (_usuario.ValidateUser(obj) == true)
            {
                HttpContext.Session.SetString("sUsuario", JsonConvert.SerializeObject(obj)); 
                return RedirectToAction("VerDetalle", "TemporalVenta"); // carrito de compras
                
            }
            else
            {
                return View("Login");
            }

        }

        public IActionResult CerrarSesion()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Login", "Usuario");
        }

        // MOSTRANDO LA LISTA DE USUARIOS REGISTRADOS
        public IActionResult ListarUsuarios()
        {
            return View(_usuario.GellAllUser());
        }

    }
}
