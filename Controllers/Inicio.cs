using Microsoft.AspNetCore.Mvc;
using WebServicios.Models;
using WebServicios.Servicios.Contrato;
using WebServicios.Recursos;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;

namespace WebServicios.Controllers
{
    public class InicioController : Controller
    {
        private readonly IUsuarioService _usuarioServicio;
       
        public InicioController(IUsuarioService usuarioService)
        {
            _usuarioServicio = usuarioService;
        }
        public IActionResult Registrarse()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Registrarse(Usuario modelo)
        {
            modelo.Contraseña = Utilidades.EncriptarContraseña(modelo.Contraseña);

            Usuario usuario_creado = await _usuarioServicio.SaveUsuario(modelo);

            if (usuario_creado.IdUsuario > 0)
                return RedirectToAction("IniciarSecion", "Inicio");

            ViewData["Mensaje"] = "No se pudo crear el usuario";
            return View();
        }

        public IActionResult IniciarSesion()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> IniciarSesion(string correo, string contraseña)
        {
            Usuario usuario_encontrado = await _usuarioServicio.GetUsuario(correo, Utilidades.EncriptarContraseña(contraseña));

            if(usuario_encontrado == null)
            {
                ViewData["Mensaje"] = "No se encontrado coicidencia";
            }

            List<Claim> claims = new List<Claim>() {
                new Claim(ClaimTypes.Name, usuario_encontrado.Nombre)
                };

            ClaimsIdentity claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
            AuthenticationProperties properties = new AuthenticationProperties()
            {
                AllowRefresh = true
            };

            await HttpContext.SignInAsync(
                CookieAuthenticationDefaults.AuthenticationScheme,
                new ClaimsPrincipal(claimsIdentity),
                properties
                );
            return RedirectToAction("Index", "Home");
        }
    }
}
