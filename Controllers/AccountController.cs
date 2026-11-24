using Microsoft.AspNetCore.Mvc;
using TP_PROG_GRINBLAT.Models;

namespace TP_PROG_GRINBLAT.Controllers;

public class AccountController : Controller
{
    public IActionResult Login()
    {
        return View();
    }

    [HttpPost]
    public IActionResult Login(string nombreUsuario, string contrasena)
    {
        BD bd = new BD();
        Usuario? usuario = bd.ObtenerUsuarioPorLogin(nombreUsuario, contrasena);

        if (usuario == null)
        {
            ViewBag.Error = "Usuario o contraseña incorrectos";
            return View();
        }

        HttpContext.Session.SetInt32("IdUsuario", usuario.Id);
        return RedirectToAction("Index", "Home");
    }

    public IActionResult Logout()
    {
        HttpContext.Session.Clear();
        return RedirectToAction("Login");
    }
        public IActionResult Registro()
    {
        return View();
    }

    [HttpPost]
    public IActionResult Registro(string nombreUsuario, string contrasena, string confirmarContrasena, string nombre, string apellido)
    {
        if (string.IsNullOrEmpty(nombreUsuario) || string.IsNullOrEmpty(contrasena) || string.IsNullOrEmpty(nombre) || string.IsNullOrEmpty(apellido))
        {
            ViewBag.Error = "Completá todos los campos";
            return View();
        }

        if (contrasena.Length < 4)
        {
            ViewBag.Error = "La contraseña tiene que tener al menos 4 caracteres";
            return View();
        }

        if (contrasena != confirmarContrasena)
        {
            ViewBag.Error = "Las contraseñas no coinciden";
            return View();
        }

        BD bd = new BD();

        if (bd.ExisteNombreUsuario(nombreUsuario))
        {
            ViewBag.Error = "El usuario " + nombreUsuario + " ya existe, elegí otro";
            return View();
        }

        Usuario usuario = new Usuario();
        usuario.NombreUsuario = nombreUsuario;
        usuario.Contrasena = contrasena;
        usuario.Nombre = nombre;
        usuario.Apellido = apellido;
        bd.AgregarUsuario(usuario);

        return RedirectToAction("Login");
    }
}
