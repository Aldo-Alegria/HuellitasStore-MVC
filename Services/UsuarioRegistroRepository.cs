using ProyectoDesarrollo.B.P.Models;

namespace ProyectoDesarrollo.B.P.Services
{
    public class UsuarioRegistroRepository : iUsuarioRegistro
    {
        private VentasC conexion = new VentasC();
        public void agregar(TbUsuario obj)
        {
            conexion.TbUsuarios.Add(obj);
            conexion.SaveChanges(); 
        }

        public TbUsuario editar(string id)
        {
            var objEncontrado = (from tUsu in conexion.TbUsuarios
                                 where tUsu.Id == int.Parse(id) 
                                 select tUsu).Single();
            return objEncontrado;
        }

        // PERMISO PARA MODIFICAR TODOS LOS CAMPOS

        public void editarDetalles(TbUsuario obj)
        {
            var objModificar = (from tUsu in conexion.TbUsuarios
                                where tUsu.Id == obj.Id
                                select tUsu).FirstOrDefault();
            if (objModificar != null)
            {
                objModificar.UsuClave = obj.UsuClave;
            }
            conexion.SaveChanges();
        }

        public void eliminar(string id)
        {
            var objEncontrado = (from tUsu in conexion.TbUsuarios
                                 where tUsu.Id == int.Parse(id)
                                 select tUsu).Single();
            conexion.TbUsuarios.Remove(objEncontrado);
            conexion.SaveChanges();
        }

        public IEnumerable<TbUsuario> GetAllUsers()
        {
            return conexion.TbUsuarios;
        }
    }
}
