using System.Collections.Generic;

namespace SwimRoomWeb.Models
{
    public class ClienteDetalleViewModel : ClienteResumenViewModel
    {
        public List<VentaDiferida> Ventas { get; set; } = new();
    }
}
