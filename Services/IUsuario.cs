using ProyectoDesarrollo.B.P.Models;

namespace ProyectoDesarrollo.B.P.Services
{
    public interface IUsuario
    {
        void Add(Usuario usuario);
        IEnumerable<Usuario> GellAllUser();
        bool ValidateUser(Usuario objUsuario);
        
    }
}
