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
}
