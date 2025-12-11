using System;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SwimRoomWeb.Models;

namespace SwimRoomWeb.Controllers
{
    [Authorize]
    public class ClientesController : Controller
    {
        private readonly AplicacionDbContext _context;

        public ClientesController(AplicacionDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index(string riesgo = "todos", string? search = null)
        {
            var query = _context.VentasDiferidas.AsQueryable();

            if (!string.IsNullOrWhiteSpace(search))
            {
                var term = search.Trim().ToLower();
                query = query.Where(v =>
                    (v.Cedula ?? string.Empty).Contains(term) ||
                    (v.ClienteNombre ?? string.Empty).ToLower().Contains(term));
            }

            var clientes = await query
                .GroupBy(v => new { v.Cedula, v.ClienteNombre })
                .Select(g => new ClienteResumenViewModel
                {
                    Cedula = g.Key.Cedula ?? string.Empty,
                    NombreCompleto = g.Key.ClienteNombre ?? string.Empty,
                    TotalVentas = g.Count(),
                    MontoTotal = g.Sum(v => v.MontoCompra),
                    IngresoMensualPromedio = g.Average(v => v.IngresoMensual),
                    ProbMoraPromedio = g.Average(v => v.ProbabilidadMora),
                    RiesgoMaximo =
                        g.Any(v => (v.NivelRiesgo ?? "").ToLower() == "alto") ? "ALTO" :
                        g.Any(v => (v.NivelRiesgo ?? "").ToLower() == "medio") ? "MEDIO" :
                        "BAJO",
                    UltimaFechaRegistro = g.Max(v => v.FechaRegistro)
                })
                .ToListAsync();

            if (!string.IsNullOrEmpty(riesgo) && riesgo != "todos")
            {
                var r = riesgo.ToUpper();
                clientes = clientes
                    .Where(c => c.RiesgoMaximo == r)
                    .ToList();
            }

            var totalClientes = clientes.Count;
            var clientesAlto = clientes.Count(c => c.RiesgoMaximo == "ALTO");
            var clientesMedio = clientes.Count(c => c.RiesgoMaximo == "MEDIO");
            var clientesBajo = clientes.Count(c => c.RiesgoMaximo == "BAJO");
            var montoTotal = clientes.Sum(c => c.MontoTotal);
            var moraPromedioGeneral = clientes.Any() ? clientes.Average(c => c.ProbMoraPromedio) : 0.0;

            ViewBag.Riesgo = riesgo;
            ViewBag.Search = search ?? string.Empty;
            ViewBag.TotalClientes = totalClientes;
            ViewBag.ClientesAlto = clientesAlto;
            ViewBag.ClientesMedio = clientesMedio;
            ViewBag.ClientesBajo = clientesBajo;

            ViewBag.MontoTotalFormatted = montoTotal.ToString("N2");
            ViewBag.MoraPromedioFormatted = moraPromedioGeneral.ToString("P2");

            var ordenados = clientes
                .OrderByDescending(c => c.RiesgoMaximo == "ALTO")
                .ThenByDescending(c => c.RiesgoMaximo == "MEDIO")
                .ThenByDescending(c => c.ProbMoraPromedio);

            return View(ordenados);
        }

        public async Task<IActionResult> Detalle(string cedula)
        {
            if (string.IsNullOrWhiteSpace(cedula))
                return NotFound();

            var ventas = await _context.VentasDiferidas
                .Where(v => v.Cedula == cedula)
                .OrderByDescending(v => v.FechaRegistro)
                .ToListAsync();

            if (!ventas.Any())
                return NotFound();

            var nombre = ventas.First().ClienteNombre;

            var vm = new ClienteDetalleViewModel
            {
                Cedula = cedula,
                NombreCompleto = nombre,
                TotalVentas = ventas.Count,
                MontoTotal = ventas.Sum(v => v.MontoCompra),
                IngresoMensualPromedio = ventas.Average(v => v.IngresoMensual),
                ProbMoraPromedio = ventas.Average(v => v.ProbabilidadMora),
                RiesgoMaximo =
                    ventas.Any(v => v.NivelRiesgo.ToLower() == "alto") ? "ALTO" :
                    ventas.Any(v => v.NivelRiesgo.ToLower() == "medio") ? "MEDIO" :
                    "BAJO",
                UltimaFechaRegistro = ventas.Max(v => v.FechaRegistro),
                Ventas = ventas
            };

            return View(vm);
        }
    }
}
