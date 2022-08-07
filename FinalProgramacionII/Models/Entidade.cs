using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace FinalProgramacionII.Models
{
    //Tabla MAPPING (Fields de la tabla con notaciones de datos para validaciones)
    public partial class Entidade
    {
        [Required]
        public int IdEntidades { get; set; }
        [Required]
        [StringLength(120)]
        public string Descripcion { get; set; } = null!;
        [Required]
        public string Direccion { get; set; } = null!;
        [Required]
        public string Localidad { get; set; } = null!;
        public string? TipoEntidad { get; set; }
        public string? TipoDocumento { get; set; }
        [Required]
        public int NumeroDocumento { get; set; }
        [Required]
        [Phone]
        public string Telefonos { get; set; } = null!;
        [Url]
        public string? UrlpaginaWeb { get; set; }
        public string? Urlfacebook { get; set; }
        public string? Urlinstagram { get; set; }
        public string? Urltwitter { get; set; }
        public string? UrltikTok { get; set; }
        public string? CodigoPostal { get; set; }
        public string? CoordenadasGps { get; set; }
        public int? LimiteCredito { get; set; }
        [Required]
        public string UserNameEntidad { get; set; } = null!;
        [Required]
        public string PasswordEntidad { get; set; } = null!;
        public string? RolUserEntidad { get; set; }
        public string? Comentario { get; set; }
        public string? Status { get; set; }
        public bool NiEliminable { get; set; }
        public DateTime? FechaRegistro { get; set; }
    }
}
