using System.ComponentModel.DataAnnotations;

namespace SwimRoomWeb.Models
{
    public class VentaDiferidaCreateViewModel
    {
        [Required]
        [Display(Name = "Nombres")]
        public string Nombres { get; set; } = string.Empty;

        [Required]
        [Display(Name = "Apellidos")]
        public string Apellidos { get; set; } = string.Empty;

        [Required]
        [StringLength(10, MinimumLength = 10, ErrorMessage = "La cédula debe tener 10 dígitos.")]
        [Display(Name = "Cédula")]
        public string Cedula { get; set; } = string.Empty;

        [Required]
        [Range(1, 1000000)]
        [Display(Name = "Monto de la compra")]
        public decimal MontoCompra { get; set; }

        [Required]
        [Display(Name = "Número de cuotas")]
        public int NumCuotas { get; set; }

        [Required]
        [Range(0, 1000000)]
        [Display(Name = "Ingreso mensual")]
        public decimal IngresoMensual { get; set; }

        [Range(0, 365)]
        [Display(Name = "Días de atraso máximo histórico")]
        public int DiasAtrasoMax { get; set; }

        [Range(0, 100)]
        [Display(Name = "Porcentaje de cuotas pagadas")]
        public double PorcentajeCuotasPagadas { get; set; }
    }
}
