using ProyectoDesarrollo.B.P.Models;

namespace ProyectoDesarrollo.B.P.Services
{
    public class UsuarioRepository : IUsuario
    {
        private VentasC conexion = new VentasC();
        private List<Usuario> LstUsuarios = new List<Usuario>();
        public void Add(Usuario usuario)
        {
            LstUsuarios.Add(usuario);
        }

        public IEnumerable<Usuario> GellAllUser()
        {
            return LstUsuarios;
        }

        public bool ValidateUser(Usuario objUsuario)
        {
            var objEncontrado = (from tUsu in conexion.TbUsuarios
                                 where tUsu.UsuNombre == objUsuario.UsuNombre
                                 && tUsu.UsuClave == objUsuario.UsuClave
                                 select tUsu).FirstOrDefault();

            if (objEncontrado != null)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
