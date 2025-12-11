using System;
using System.ComponentModel.DataAnnotations;

namespace SwimRoomWeb.Models
{
    public class VentaDiferida
    {
        public int Id { get; set; }

        [Display(Name = "Nombre completo")]
        public string ClienteNombre { get; set; } = string.Empty;

        [Display(Name = "Cédula")]
        public string? Cedula { get; set; }

        [Required]
        [Range(1, 1000000)]
        [Display(Name = "Monto de la compra")]
        public decimal MontoCompra { get; set; }

        [Required]
        [Range(1, 36)]
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

        [Display(Name = "Probabilidad de mora")]
        public double ProbabilidadMora { get; set; }

        [Display(Name = "Nivel de riesgo")]
        public string NivelRiesgo { get; set; } = "desconocido";

        [Display(Name = "Fecha de registro")]
        public DateTime FechaRegistro { get; set; }
    }
}
