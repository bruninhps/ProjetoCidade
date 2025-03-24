using Microsoft.AspNetCore.Mvc;
using Projeto1.Models;
using Projeto1.Repositorio;

namespace Projeto1.Controllers
{
    public class LoginController : Controller
    {
        //Consultor
        private readonly UsuarioRepositorio _usuarioRepositorio;

        public LoginController (UsuarioRepositorio usuarioRepositorio)
        {
            _usuarioRepositorio = usuarioRepositorio;
        }

        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Login(string email, string senha)
        {
            var usuario = _usuarioRepositorio.ObterUsuario(email);

            if (usuario != null && usuario.Senha == senha)
            {
                return RedirectToAction("Index", "Home");
            }

            ModelState.AddModelError("", "Email e senha inválidos.");

            return View();
        }

        public IActionResult Cadastro()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Cadastro(Usuario usuario)
        {
            if (ModelState.IsValid)
            {
                _usuarioRepositorio.AdicionarUsuario(usuario);
                return RedirectToAction("Login");
            }
            return View(usuario);

        }
    }
}
