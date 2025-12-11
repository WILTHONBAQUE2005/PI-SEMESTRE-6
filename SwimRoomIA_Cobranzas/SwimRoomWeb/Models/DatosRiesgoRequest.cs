namespace SwimRoomWeb.Models
{
    // Debe coincidir con los nombres del modelo Pydantic en Python
    public class DatosRiesgoRequest
    {
        public double monto_compra { get; set; }
        public int num_cuotas { get; set; }
        public double ingreso_mensual { get; set; }
        public int dias_atraso_max { get; set; }
        public double porcentaje_cuotas_pagadas { get; set; }
    }
}
