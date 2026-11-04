using ProyectoDesarrollo.B.P.Models;

namespace ProyectoDesarrollo.B.P.Services
{
    public interface iUsuarioRegistro
    {
        void agregar(TbUsuario obj);
        IEnumerable<TbUsuario> GetAllUsers();
        void eliminar(string id);
        TbUsuario editar(string id);
        void editarDetalles(TbUsuario obj);
    }
}
