using System.ComponentModel.DataAnnotations;

namespace FinalProgramacionII.Models
{
    public class Registrar
    {
        [Required]
        public string Username { get; set; } = null!;
        [Required]
        public string Password { get; set; } = null!;
    }
}
