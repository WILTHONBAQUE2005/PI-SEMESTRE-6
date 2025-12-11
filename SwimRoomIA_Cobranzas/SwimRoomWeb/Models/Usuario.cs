using System;
using System.ComponentModel.DataAnnotations;

namespace SwimRoomWeb.Models
{
    public class Usuario
    {
        public int Id { get; set; }

        [Required, MaxLength(100)]
        public string Nombre { get; set; } = string.Empty;

        [Required, MaxLength(100)]
        public string Apellido { get; set; } = string.Empty;

        [Required, MaxLength(200)]
        [EmailAddress]
        public string Email { get; set; } = string.Empty;

        [Required, MaxLength(256)]
        public string PasswordHash { get; set; } = string.Empty;

        public DateTime FechaRegistro { get; set; } = DateTime.Now;

        public bool Activo { get; set; } = true;
    }
}
