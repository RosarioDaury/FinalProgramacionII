using System.ComponentModel.DataAnnotations;

namespace FinalProgramacionII.Models
{
    //Clase modelo para el formulario de usuaripo y Password(clave)
    public class Login
    {
        [Required]
        [Display(Name = "UserNameEntidad")]
        public string UserNameEntidad { get; set; } = null!;
        [Required]
        [Display(Name = "PasswordEntidad")]
        public string PasswordEntidad { get; set; } = null!;
    }
}
