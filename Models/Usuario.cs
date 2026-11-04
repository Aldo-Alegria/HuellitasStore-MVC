using System.ComponentModel.DataAnnotations;

namespace ProyectoDesarrollo.B.P.Models
{
    public class Usuario
    {
        [Required(ErrorMessage = "El campo DNI es obligatorio")]
        public string Id { get; set; }
        [Required(ErrorMessage = "El campo del NOMBRES es obligatorio")]
        public string Nombre { get; set; }
        [Required(ErrorMessage = "El campo de los APELLIDOS es obligatorio")]
        public string Apellido { get; set; }
        [Required(ErrorMessage = "El campo de EDAD es obligatorio")]
        public string Edad { get; set; }

        public int IdUsuario { get; set; }

        [Required(ErrorMessage = "Este campo es OBLIGATORIO")]
        public string UsuNombre { get; set; } = null!;

        [Required(ErrorMessage = "Este campo es OBLIGATORIO")]
        public string UsuClave { get; set; } = null!;

        [Required(ErrorMessage = "Este campo es OBLIGATORIO")]
        public string UsuNomApe { get; set; } = null!;

        [Required(ErrorMessage = "Este campo es OBLIGATORIO")]
        public string UsuDirec { get; set; } = null!;

        [Required(ErrorMessage = "Este campo es OBLIGATORIO")]
        public string UsuTelef { get; set; } = null!;

        [Required(ErrorMessage = "Este campo es OBLIGATORIO")]
        public string UsuEmail { get; set; } = null!;
    }
}
