using Microsoft.AspNetCore.Mvc;
using Dapper;
using System.Data.SqlClient;
using System.Linq;
using System.Collections.Generic;

namespace Pictures.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;

    public HomeController(ILogger<HomeController> logger)
    {
        _logger = logger;
    }

    public IActionResult Index()
    {
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

     public IActionResult Historia()
    {
        return View();
    }

    public IActionResult Deportes()
    {
        using (SqlConnection connection = new SqlConnection(connectionString))
        {
            List<Deporte> deportes = connection.Query<Deporte>("SELECT * FROM Deportes").ToList();
            ViewBag.Deportes = deportes;
        }
        return View();
    }

    public IActionResult Paises()
    {
        using (SqlConnection connection = new SqlConnection(connectionString))
        {
            List<Pais> paises = connection.Query<Pais>("SELECT * FROM Paises").ToList();
            ViewBag.Paises = paises;
        }
        return View();
    }

    public IActionResult VerDetalleDeporte(int idDeporte)
    {
        using (SqlConnection connection = new SqlConnection(connectionString))
        {
            Deporte deporte = connection.QueryFirstOrDefault<Deporte>("SELECT * FROM Deportes WHERE IdDeporte = @IdDeporte", new { IdDeporte = idDeporte });
            List<Deportista> deportistas = connection.Query<Deportista>("SELECT * FROM Deportistas WHERE IdDeporte = @IdDeporte", new { IdDeporte = idDeporte }).ToList();
            ViewBag.Deporte = deporte;
            ViewBag.Deportistas = deportistas;
        }
        return View("DetalleDeporte");
    }

    public IActionResult VerDetallePais(int idPais)
    {
        using (SqlConnection connection = new SqlConnection(connectionString))
        {
            Pais pais = connection.QueryFirstOrDefault<Pais>("SELECT * FROM Paises WHERE IdPais = @IdPais", new { IdPais = idPais });
            List<Deportista> deportistas = connection.Query<Deportista>("SELECT * FROM Deportistas WHERE IdPais = @IdPais", new { IdPais = idPais }).ToList();
            ViewBag.Pais = pais;
            ViewBag.Deportistas = deportistas;
        }
        return View("DetallePais");
    }

    public IActionResult VerDetalleDeportista(int idDeportista)
    {
        using (SqlConnection connection = new SqlConnection(connectionString))
        {
            Deportista deportista = connection.QueryFirstOrDefault<Deportista>("SELECT * FROM Deportistas WHERE IdDeportista = @IdDeportista", new { IdDeportista = idDeportista });
            ViewBag.Deportista = deportista;
        }
        return View("DetalleDeportista");
    }

    public IActionResult AgregarDeportista()
    {
        using (SqlConnection connection = new SqlConnection(connectionString))
        {
            List<Pais> paises = connection.Query<Pais>("SELECT * FROM Paises").ToList();
            List<Deporte> deportes = connection.Query<Deporte>("SELECT * FROM Deportes").ToList();
            ViewBag.Paises = paises;
            ViewBag.Deportes = deportes;
        }
        return View("FormularioDeportista");
    }

    [HttpPost]
    public IActionResult GuardarDeportista(int idPais, int idDeporte, string apellido, string nombre, DateTime fechaNacimiento, string foto)
    {
        GuardarNuevoDeportista(idPais, idDeporte, apellido, nombre, fechaNacimiento, foto);
        return RedirectToAction("Index");
    }

    private void GuardarNuevoDeportista(int idPais, int idDeporte, string apellido, string nombre, DateTime fechaNacimiento, string foto)
    {
        string sql = "INSERT INTO Deportistas (Apellido, Nombre, FechaNacimiento, Foto, IdPais, IdDeporte) " +
                     "VALUES (@Apellido, @Nombre, @FechaNacimiento, @Foto, @IdPais, @IdDeporte)";

        using (SqlConnection db = new SqlConnection(connectionString))
        {
            int rowsAffected = db.Execute(sql, new
            {
                Apellido = apellido,
                Nombre = nombre,
                FechaNacimiento = fechaNacimiento,
                Foto = foto,
                IdPais = idPais,
                IdDeporte = idDeporte
            });

            // El valor de rowsAffected indica la cantidad de filas afectadas
            Console.WriteLine($"{rowsAffected} fila(s) insertada(s).");
        }
    }

    public IActionResult EliminarDeportista(int idDeportista)
    {
        string sql = "DELETE FROM Deportistas WHERE IdDeportista = @IdDeportista";

        using (SqlConnection db = new SqlConnection(connectionString))
        {
            int rowsAffected = db.Execute(sql, new { IdDeportista = idDeportista });
            // Similar al anterior, rowsAffected indica cuántas filas fueron eliminadas.
            Console.WriteLine($"{rowsAffected} fila(s) eliminada(s).");
        }
        return RedirectToAction("Index");
    }

    public IActionResult Creditos()
    {
        return View();
    }
}
