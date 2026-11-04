using Microsoft.AspNetCore.Mvc;
using ProyectoDesarrollo.B.P.Models;
using ProyectoDesarrollo.B.P.Services;

namespace ProyectoDesarrollo.B.P.Controllers
{
    public class UsuarioRegistroController : Controller
    {
        private readonly iUsuarioRegistro _usuario;
        public UsuarioRegistroController(iUsuarioRegistro registro)
        {
            _usuario = registro;
        }

        public IActionResult RegistroUsuarios()
        {
            return View(_usuario.GetAllUsers());
        }

        [Route("UsuarioRegistro/Eliminar/{id}")]
        public IActionResult Eliminar(string id) 
        {
            _usuario.eliminar(id);
            return RedirectToAction("RegistroUsuarios");
        }

        public IActionResult Nuevo() 
        { 
            return View(); 
        }
        public IActionResult Grabar(TbUsuario obj)
        {
            _usuario.agregar(obj);
            return RedirectToAction("RegistroUsuarios");
        }


        [Route("UsuarioRegistro/Modificar/{id}")]
        public IActionResult Modificar(string id)
        {
            var obj = _usuario.editar(id);
            return View(obj);
        }

        public IActionResult ModificarUsuario(TbUsuario obj)
        {
            _usuario.editarDetalles(obj);
            return RedirectToAction("RegistroUsuarios");
        }

        /*public IActionResult CargarDatos()
        {
            return View();
        }*/
    }
}
