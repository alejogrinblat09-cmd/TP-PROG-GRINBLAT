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
            public List<Tutor> VerMas(int desde)
    {
        if (HttpContext.Session.GetInt32("IdUsuario") == null)
        {
            return new List<Tutor>();
        }

        BD bd = new BD();
        List<Tutor> tutores = bd.ObtenerTutores(desde, 8);

        foreach (Tutor tutor in tutores)
        {
            tutor.Materias = bd.ObtenerMateriasDeTutor(tutor.Id);
        }

        return tutores;
    }
    public IActionResult Detalle(int id)
    {
        if (HttpContext.Session.GetInt32("IdUsuario") == null)
        {
            return RedirectToAction("Login", "Account");
        }

        BD bd = new BD();
        Tutor? tutor = bd.ObtenerTutor(id);

        if (tutor == null)
        {
            return RedirectToAction("Index");
        }

        tutor.Materias = bd.ObtenerMateriasDeTutor(id);
        ViewBag.Tutor = tutor;
        ViewBag.Clases = bd.ObtenerClasesDeTutor(id);
        return View("Detalle");
    }

    [HttpPost]
    public IActionResult Inscribirse(int idTutor, int idClase)
    {
        int? idUsuario = HttpContext.Session.GetInt32("IdUsuario");

        if (idUsuario == null)
        {
            return RedirectToAction("Login", "Account");
        }

        BD bd = new BD();
        Tutor? tutor = bd.ObtenerTutor(idTutor);

        if (tutor == null)
        {
            return RedirectToAction("Index");
        }

        ClaseApoyo? clase = bd.ObtenerClase(idClase);

        if (clase == null || clase.IdTutor != idTutor)
        {
            ViewBag.Error = "Elegí una clase para anotarte";
            return Detalle(idTutor);
        }

        string cuando = clase.NombreMateria + " el " + clase.FechaHora.ToString("dd/MM") + " a las " + clase.FechaHora.ToString("HH:mm");

        if (clase.FechaHora <= DateTime.Now)
        {
            ViewBag.Error = "La clase de " + cuando + " ya pasó";
            return Detalle(idTutor);
        }

        if (bd.EstaInscripto(idClase, idUsuario.Value))
        {
            ViewBag.Error = "Ya estás anotado en " + cuando;
            return Detalle(idTutor);
        }

        if (bd.ContarInscriptos(idClase) >= clase.Cupo)
        {
            ViewBag.Error = "No quedan lugares en " + cuando;
            return Detalle(idTutor);
        }

        bd.AgregarInscripcion(idClase, idUsuario.Value);
        return RedirectToAction("MisInscripciones");
    }
    public int LugaresDisponibles(int idClase)
    {
        if (HttpContext.Session.GetInt32("IdUsuario") == null)
        {
            return 0;
        }

        BD bd = new BD();
        ClaseApoyo? clase = bd.ObtenerClase(idClase);

        if (clase == null)
        {
            return 0;
        }

        return clase.Cupo - bd.ContarInscriptos(idClase);
    }
    public IActionResult MisInscripciones()
    {
        int? idUsuario = HttpContext.Session.GetInt32("IdUsuario");

        if (idUsuario == null)
        {
            return RedirectToAction("Login", "Account");
        }

        BD bd = new BD();
        ViewBag.Proximas = bd.ObtenerInscripcionesProximas(idUsuario.Value);
        ViewBag.Pasadas = bd.ObtenerInscripcionesPasadas(idUsuario.Value);
        return View();
    }
    [HttpPost]
    public string Cancelar(int idInscripcion)
    {
        int? idUsuario = HttpContext.Session.GetInt32("IdUsuario");

        if (idUsuario == null)
        {
            return "Tenés que iniciar sesión";
        }

        BD bd = new BD();
        Inscripcion? inscripcion = bd.ObtenerInscripcion(idInscripcion);

        if (inscripcion == null || inscripcion.IdUsuario != idUsuario.Value || inscripcion.Cancelado)
        {
            return "No se encontró la inscripción";
        }

        string cuando = inscripcion.NombreMateria + " el " + inscripcion.FechaHora.ToString("dd/MM") + " a las " + inscripcion.FechaHora.ToString("HH:mm");

        if (inscripcion.FechaHora <= DateTime.Now)
        {
            return "La clase de " + cuando + " ya pasó";
        }

        if (inscripcion.FechaHora < DateTime.Now.AddHours(24))
        {
            return "No podés cancelar la clase de " + cuando + ": solo se puede cancelar hasta 24 horas antes";
        }

        bd.CancelarInscripcion(idInscripcion);
        return "OK";
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
