using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using TP_JJOO_2.Models;

namespace TP_JJOO_2.Controllers
{
    public class HomeController : Controller
    {

        // Acción para mostrar la página de inicio
        public IActionResult Index()
        {
            return View();
        }

        // Acción para mostrar la historia
        public IActionResult Historia()
        {
            return View();
        }

        // Acción para mostrar los deportes
        public IActionResult Deportes()
        {
            List<Deporte> deportes = BD.ListarDeportes();
            ViewBag.Deportes = deportes;
            return View();
        }

        // Acción para mostrar los países
        public IActionResult Paises()
        {
            List<Pais> paises = BD.ListarPaises();
            ViewBag.Paises = paises;
            return View();
        }

        // Acción para mostrar detalles de un deporte específico
        public IActionResult VerDetalleDeporte(int idDeporte)
        {
            Deporte deporte = BD.VerInfoDeporte(idDeporte);
            List<Deportista> deportistas = BD.ListarDeportistas(idDeporte);
            ViewBag.Deporte = deporte;
            ViewBag.Deportistas = deportistas;
            return View();
        }

        // Acción para mostrar detalles de un país específico
        public IActionResult VerDetallePais(int idPais)
        {
            Pais pais = BD.VerInfoPais(idPais);
            List<Deportista> deportistas = BD.ListarDeportistas(idPais);
            ViewBag.Pais = pais;
            ViewBag.Deportistas = deportistas;
            return View();
        }

        // Acción para agregar un nuevo deportista
        public IActionResult AgregarDeportista()
        {
            List<Deporte> deportes = BD.ListarDeportes();
            List<Pais> paises = BD.ListarPaises();
            ViewBag.Deportes = deportes;
            ViewBag.Paises = paises;
            return View();
        }

        // Acción para ver detalles de un deportista específico
        public IActionResult VerDetalleDeportista(int idDeportista)
        {
            Deportista deportista = BD.VerInfoDeportista(idDeportista);
            Deporte deporte = BD.VerInfoDeporte(deportista.IdDeporte);
            Pais pais = BD.VerInfoPais(deportista.IdPais);
            ViewBag.Deportista = deportista;
            ViewBag.Deporte = deporte;
            ViewBag.Pais = pais;
            return View();
        }
    }
}
