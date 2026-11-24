using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using TP_PROG_GRINBLAT.Models;

namespace TP_PROG_GRINBLAT.Controllers;

public class HomeController : Controller
{
        public IActionResult Index()
    {
        if (HttpContext.Session.GetInt32("IdUsuario") == null)
        {
            return RedirectToAction("Login", "Account");
        }

                BD bd = new BD();
        List<Tutor> tutores = bd.ObtenerTutores(0, 8);

        foreach (Tutor tutor in tutores)
        {
            tutor.Materias = bd.ObtenerMateriasDeTutor(tutor.Id);
        }

        ViewBag.Tutores = tutores;
        return View();
    }

    public IActionResult Privacy()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
