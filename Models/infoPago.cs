using System.ComponentModel.DataAnnotations;

namespace ProyectoDesarrollo.B.P.Models
{
    public class infoPago
    {
        [Required(ErrorMessage = "El campo Número de Tarjeta es obligatorio")]
        public string NumTarjeta { get; set; }


        [Required(ErrorMessage = "El campo FECHA es obligatorio")]
        public string Fecha { get; set; }


        [Required(ErrorMessage = "El campo CVV es obligatorio")]
        public string CVV { get; set; }


        [Required(ErrorMessage = "El campo NOMBRES es obligatorio")]
        public string Nombres { get; set; }


        [Required(ErrorMessage = "El campo APELLIDOS es obligatorio")]
        public string Apellidos { get; set; }


        [Required(ErrorMessage = "El campo EMAIL es obligatorio")]
        public string Email { get; set; }


        [Required(ErrorMessage = "El campo TELEFONO es obligatorio")]
        public string Telefono { get; set; }
    }
}
