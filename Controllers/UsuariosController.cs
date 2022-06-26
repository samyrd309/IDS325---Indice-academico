using IDS325___Indice_academico.Data;
using IDS325___Indice_academico.Models;
using Microsoft.AspNetCore.Mvc;

namespace IDS325___Indice_academico.Controllers
{
    public class UsuariosController : Controller
    {
        private readonly IDS325___Indice_academicoContext _context;

        public UsuariosController(IDS325___Indice_academicoContext context)
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
        public async Task<IActionResult> Search(int Matricula, string Contraseña)
        {

            if (ModelState.IsValid)
            {
                usuario = await _context.Persona.FindAsync(Matricula, Contraseña);
                if (usuario == null)
                {
                    //return RedirectToAction(nameof(LoginError));
                }

                if (usuario.IdRol == 1)
                    return RedirectToAction("Index", "Administrador", usuario);
                if (usuario.IdRol == 2)
                    return RedirectToAction("Index", "Estudiante", usuario);
                if (usuario.IdRol == 3)
                    return RedirectToAction("Index", "Docente", usuario);
                
                    
            }
            return RedirectToAction(nameof(Login)); // Aquí que mande a un error
        }
    }
}

/*
 private readonly HospitalContext _context;

        public UsuarioController(HospitalContext context)
        {
            _context = context;
        }

        public static Usuarios usuario = new Usuarios();

        public async Task<ViewResult> Login()
        {
            return View();
        }

        public async Task<ViewResult> LoginError()
        {
            return View();
        }

        //GET: Usuario
        public async Task<IActionResult> Search(string NickName, string Password)
        {

            if (ModelState.IsValid)
            {
                usuario = await _context.Usuarios.FindAsync(NickName, Password);
                if (usuario == null)
                {
                    return RedirectToAction(nameof(LoginError));
                }
                
                if (usuario.Usuario_IdPerfil != 3)
                    return RedirectToAction(nameof(LoginError));
            }
            return RedirectToAction("Index", "Personas", usuario);
        }*/