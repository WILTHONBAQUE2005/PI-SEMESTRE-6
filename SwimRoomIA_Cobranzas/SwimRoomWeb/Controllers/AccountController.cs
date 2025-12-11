using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SwimRoomWeb.Helpers;
using SwimRoomWeb.Models;

namespace SwimRoomWeb.Controllers
{
    public class AccountController : Controller
    {
        private readonly AplicacionDbContext _context;

        public AccountController(AplicacionDbContext context)
        {
            _context = context;
        }


        [AllowAnonymous]
        [HttpGet]
        public IActionResult Login(string? returnUrl = null)
        {
            if (User.Identity != null && User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Index", "VentasDiferidas");
            }

            ViewBag.ReturnUrl = returnUrl;
            return View(new LoginViewModel());
        }

        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel model, string? returnUrl = null)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var usuario = await _context.Usuarios
                .FirstOrDefaultAsync(u => u.Email == model.Email && u.Activo);

            if (usuario == null || !PasswordHelper.VerifyPassword(model.Password, usuario.PasswordHash))
            {
                ModelState.AddModelError(string.Empty, "Credenciales inválidas.");
                return View(model);
            }

            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, $"{usuario.Nombre} {usuario.Apellido}"),
                new Claim(ClaimTypes.Email, usuario.Email)
            };

            var identity = new ClaimsIdentity(
                claims, CookieAuthenticationDefaults.AuthenticationScheme);

            var authProperties = new AuthenticationProperties
            {
                IsPersistent = model.RememberMe
            };

            await HttpContext.SignInAsync(
                CookieAuthenticationDefaults.AuthenticationScheme,
                new ClaimsPrincipal(identity),
                authProperties);

            if (!string.IsNullOrEmpty(returnUrl) && Url.IsLocalUrl(returnUrl))
            {
                return Redirect(returnUrl);
            }

            return RedirectToAction("Index", "VentasDiferidas");
        }


        [AllowAnonymous]
        [HttpGet]
        public IActionResult Register()
        {
            if (User.Identity != null && User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Index", "VentasDiferidas");
            }

            return View(new RegisterViewModel());
        }

        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var existe = await _context.Usuarios
                .AnyAsync(u => u.Email == model.Email);

            if (existe)
            {
                ModelState.AddModelError("Email", "Ya existe una cuenta con este correo.");
                return View(model);
            }

            var usuario = new Usuario
            {
                Nombre = model.Nombre,
                Apellido = model.Apellido,
                Email = model.Email,
                PasswordHash = PasswordHelper.HashPassword(model.Password),
                FechaRegistro = DateTime.Now,
                Activo = true
            };

            _context.Usuarios.Add(usuario);
            await _context.SaveChangesAsync();

            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, $"{usuario.Nombre} {usuario.Apellido}"),
                new Claim(ClaimTypes.Email, usuario.Email)
            };

            var identity = new ClaimsIdentity(
                claims, CookieAuthenticationDefaults.AuthenticationScheme);

            await HttpContext.SignInAsync(
                CookieAuthenticationDefaults.AuthenticationScheme,
                new ClaimsPrincipal(identity));

            return RedirectToAction("Index", "VentasDiferidas");
        }


        [Authorize]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Login", "Account");
        }
    }
}
