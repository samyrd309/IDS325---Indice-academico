using IDS325___Indice_academico.Data;
using IDS325___Indice_academico.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Security.Claims;

namespace IDS325___Indice_academico.Controllers
{
    public class HomeController : Controller
    {

        private readonly IDS325___Indice_academicoContext _context;

        public IActionResult Index()
        {
            return View();
        }

        public HomeController(IDS325___Indice_academicoContext context)
        {
            _context = context;
        }

        public static Persona usuario = new Persona();

        public Task<ViewResult> Login()
        {
            return Task.FromResult(View());
        }

        //POST: Usuario
        [HttpPost]
        public async Task<IActionResult> IniciarSesion(int Matricula, string Contraseña)
        {

            if (ModelState.IsValid)
            {
                usuario = await _context.Persona.FindAsync(Matricula, Contraseña);

                if (usuario != null)
                {
                    var claims = new List<Claim>
                    {
                        new Claim(ClaimTypes.Name, usuario.Nombre),
                        new Claim("Matricula", usuario.Matricula.ToString()),
                        new Claim(ClaimTypes.Role, usuario.IdRol.ToString())
                    };

                    var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

                    await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity));

                    return RedirectToAction("Index", "CalificacionesEstudiantes", usuario);
                }
                else
                {
                    return RedirectToAction("Login", "Home", usuario);
                }

            }
            return RedirectToAction("Login", "Home");


        }

        public async Task<IActionResult> CerrarSesion()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);

            return RedirectToAction("Login", "Home");
        }

        public IActionResult Privacy()
        {
            return View();
        }

  
        public IActionResult CambiarContraseña()
        {
            return View();
        }



        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }


    }
}