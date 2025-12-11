using System;
using System.Linq;
using System.Text;
using System.Globalization;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SwimRoomWeb.Models;
using System.Net.Http;
using System.Net.Http.Json;
using Microsoft.AspNetCore.Authorization;

namespace SwimRoomWeb.Controllers
{
    [Authorize]
    public class VentasDiferidasController : Controller
    {
        private readonly AplicacionDbContext _context;
        private readonly IHttpClientFactory _httpClientFactory;

        public VentasDiferidasController(AplicacionDbContext context, IHttpClientFactory httpClientFactory)
        {
            _context = context;
            _httpClientFactory = httpClientFactory;
        }

        public async Task<IActionResult> Index()
        {
            var lista = await _context.VentasDiferidas
                .OrderByDescending(v => v.FechaRegistro)
                .ToListAsync();

            return View(lista);
        }

        public IActionResult Create()
        {
            var vm = new VentaDiferidaCreateViewModel();
            return View(vm);
        }

        [HttpPost]

        public async Task<IActionResult> Create(VentaDiferidaCreateViewModel modelo)
        {
            Console.WriteLine(">>> Entró al POST Create");

            if (!ModelState.IsValid)
            {
                var errores = ModelState
                    .Where(kvp => kvp.Value != null && kvp.Value.Errors.Count > 0)
                    .SelectMany(kvp => kvp.Value!.Errors.Select(e => $"{kvp.Key}: {e.ErrorMessage}"))
                    .ToList();

                Console.WriteLine(">>> Errores de modelo: " + string.Join(" | ", errores));
                return View(modelo);
            }

            var venta = new VentaDiferida
            {
                ClienteNombre = $"{modelo.Nombres} {modelo.Apellidos}",
                Cedula = modelo.Cedula,
                MontoCompra = modelo.MontoCompra,
                NumCuotas = modelo.NumCuotas,
                IngresoMensual = modelo.IngresoMensual,
                DiasAtrasoMax = modelo.DiasAtrasoMax,
                PorcentajeCuotasPagadas = modelo.PorcentajeCuotasPagadas
            };

            await EvaluarRiesgoConIAAsync(venta);

            venta.FechaRegistro = DateTime.Now;

            _context.Add(venta);
            await _context.SaveChangesAsync();
            Console.WriteLine($">>> Registro guardado en BD. Id: {venta.Id}");

            return RedirectToAction(nameof(Index));
        }

        public IActionResult Simulador()
        {
            var vm = new VentaDiferidaCreateViewModel
            {
                DiasAtrasoMax = 0,
                PorcentajeCuotasPagadas = 100
            };
            ViewBag.ResultadoSimulacion = null;
            ViewBag.ErrorSimulacion = null;
            return View(vm);
        }

        [HttpPost]
        public async Task<IActionResult> Simulador(VentaDiferidaCreateViewModel modelo)
        {
            ViewBag.ResultadoSimulacion = null;
            ViewBag.ErrorSimulacion = null;

            if (!ModelState.IsValid)
            {
                return View(modelo);
            }

            try
            {
                var client = _httpClientFactory.CreateClient();
                client.BaseAddress = new Uri("http://127.0.0.1:8000");

                var requestBody = new DatosRiesgoRequest
                {
                    monto_compra = (double)modelo.MontoCompra,
                    num_cuotas = modelo.NumCuotas,
                    ingreso_mensual = (double)modelo.IngresoMensual,
                    dias_atraso_max = modelo.DiasAtrasoMax,
                    porcentaje_cuotas_pagadas = modelo.PorcentajeCuotasPagadas
                };

                var response = await client.PostAsJsonAsync("/predict_riesgo", requestBody);

                if (response.IsSuccessStatusCode)
                {
                    var riesgo = await response.Content.ReadFromJsonAsync<RiesgoResponse>();
                    ViewBag.ResultadoSimulacion = riesgo;
                }
                else
                {
                    ViewBag.ErrorSimulacion = $"Error al llamar a la IA: {response.StatusCode}";
                }
            }
            catch (Exception ex)
            {
                ViewBag.ErrorSimulacion = "Excepción al llamar a la IA: " + ex.Message;
            }

            return View(modelo);
        }

        public async Task<IActionResult> ExportarCsv()
        {
            var lista = await _context.VentasDiferidas
                .OrderByDescending(v => v.FechaRegistro)
                .ToListAsync();

            var sb = new StringBuilder();
            sb.AppendLine("Id,Cedula,Cliente,MontoCompra,NumCuotas,IngresoMensual,DiasAtrasoMax,PorcentajeCuotasPagadas,ProbabilidadMora,NivelRiesgo,FechaRegistro");

            foreach (var v in lista)
            {
                sb.AppendLine(string.Join(",",
                    v.Id,
                    EscapeCsv(v.Cedula),
                    EscapeCsv(v.ClienteNombre),
                    v.MontoCompra.ToString(CultureInfo.InvariantCulture),
                    v.NumCuotas,
                    v.IngresoMensual.ToString(CultureInfo.InvariantCulture),
                    v.DiasAtrasoMax,
                    v.PorcentajeCuotasPagadas.ToString(CultureInfo.InvariantCulture),
                    v.ProbabilidadMora.ToString(CultureInfo.InvariantCulture),
                    EscapeCsv(v.NivelRiesgo),
                    v.FechaRegistro.ToString("s")
                ));
            }

            var bytes = Encoding.UTF8.GetBytes(sb.ToString());
            var fileName = $"ventas_riesgo_{DateTime.Now:yyyyMMddHHmmss}.csv";
            return File(bytes, "text/csv", fileName);
        }

        private async Task EvaluarRiesgoConIAAsync(VentaDiferida venta)
        {
            try
            {
                var client = _httpClientFactory.CreateClient();
                client.BaseAddress = new Uri("http://127.0.0.1:8000");

                var requestBody = new DatosRiesgoRequest
                {
                    monto_compra = (double)venta.MontoCompra,
                    num_cuotas = venta.NumCuotas,
                    ingreso_mensual = (double)venta.IngresoMensual,
                    dias_atraso_max = venta.DiasAtrasoMax,
                    porcentaje_cuotas_pagadas = venta.PorcentajeCuotasPagadas
                };

                var response = await client.PostAsJsonAsync("/predict_riesgo", requestBody);

                if (response.IsSuccessStatusCode)
                {
                    var riesgo = await response.Content.ReadFromJsonAsync<RiesgoResponse>();
                    if (riesgo != null)
                    {
                        venta.ProbabilidadMora = riesgo.probabilidad_mora;
                        venta.NivelRiesgo = riesgo.nivel_riesgo;
                    }
                }
                else
                {
                    Console.WriteLine($">>> Error IA: StatusCode = {response.StatusCode}");
                    venta.ProbabilidadMora = 0;
                    venta.NivelRiesgo = "desconocido";
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(">>> Excepción llamando a la IA: " + ex.Message);
                venta.ProbabilidadMora = 0;
                venta.NivelRiesgo = "desconocido";
            }
        }

        private static string EscapeCsv(string? input)
        {
            if (string.IsNullOrEmpty(input)) return "";
            if (input.Contains(',') || input.Contains('"') || input.Contains('\n'))
            {
                return "\"" + input.Replace("\"", "\"\"") + "\"";
            }
            return input;
        }
    }
}
