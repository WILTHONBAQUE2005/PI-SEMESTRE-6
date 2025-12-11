using System.ComponentModel.DataAnnotations;

namespace SwimRoomWeb.Models
{
    public class RegisterViewModel
    {
        [Required]
        [Display(Name = "Nombre")]
        public string Nombre { get; set; } = string.Empty;

        [Required]
        [Display(Name = "Apellido")]
        public string Apellido { get; set; } = string.Empty;

        [Required]
        [EmailAddress]
        [Display(Name = "Correo electrónico")]
        public string Email { get; set; } = string.Empty;

        [Required]
        [DataType(DataType.Password)]
        [MinLength(6, ErrorMessage = "La contraseña debe tener al menos 6 caracteres.")]
        [Display(Name = "Contraseña")]
        public string Password { get; set; } = string.Empty;

        [Required]
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "Las contraseñas no coinciden.")]
        [Display(Name = "Confirmar contraseña")]
        public string ConfirmPassword { get; set; } = string.Empty;
    }
}
