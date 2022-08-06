using System.ComponentModel.DataAnnotations;

namespace FinalProgramacionII.Models
{
    //Clase modelo para el formulario de usuaripo y Password(clave)
    public class Login
    {
        [Required]
        [Display(Name = "Username")]
        public string Username { get; set; } = null!;
        [Required]
        [Display(Name = "Password")]
        public string Password { get; set; } = null!;
    }
}
