using System;

namespace SwimRoomWeb.Models
{
    public class ClienteResumenViewModel
    {
        public string Cedula { get; set; } = string.Empty;
        public string NombreCompleto { get; set; } = string.Empty;

        public int TotalVentas { get; set; }
        public decimal MontoTotal { get; set; }
        public decimal IngresoMensualPromedio { get; set; }

        public double ProbMoraPromedio { get; set; }
        public string RiesgoMaximo { get; set; } = string.Empty;

        public DateTime UltimaFechaRegistro { get; set; }
    }
}
