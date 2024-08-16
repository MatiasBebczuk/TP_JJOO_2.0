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
            //rowsAffected indica cuántas filas fueron eliminadas.
            Console.WriteLine($"{rowsAffected} fila(s) eliminada(s).");
        }
        return RedirectToAction("Index");
    }

    public IActionResult Creditos()
    {
        return View();
    }

    // Agrega un nuevo deportista a la base de datos
public static void AgregarDeportista(Deportista deportista)
{
    string sql = "INSERT INTO Deportistas (Apellido, Nombre, FechaNacimiento, Foto, IdPais, IdDeporte) " +
                 "VALUES (@Apellido, @Nombre, @FechaNacimiento, @Foto, @IdPais, @IdDeporte)";

    using (SqlConnection db = new SqlConnection(connectionString))
    {
        db.Execute(sql, new
        {
            deportista.Apellido,
            deportista.Nombre,
            deportista.FechaNacimiento,
            deportista.Foto,
            deportista.IdPais,
            deportista.IdDeporte
        });
    }
}

// Elimina un deportista de la base de datos según su Id
public static void EliminarDeportista(int idDeportista)
{
    string sql = "DELETE FROM Deportistas WHERE IdDeportista = @IdDeportista";

    using (SqlConnection db = new SqlConnection(connectionString))
    {
        db.Execute(sql, new { IdDeportista = idDeportista });
    }
}

// Devuelve la información de un deporte según su Id
public static Deporte VerInfoDeporte(int idDeporte)
{
    string sql = "SELECT * FROM Deportes WHERE IdDeporte = @IdDeporte";

    using (SqlConnection db = new SqlConnection(connectionString))
    {
        return db.QueryFirstOrDefault<Deporte>(sql, new { IdDeporte = idDeporte });
    }
}

// Devuelve la información de un país según su Id
public static Pais VerInfoPais(int idPais)
{
    string sql = "SELECT * FROM Paises WHERE IdPais = @IdPais";

    using (SqlConnection db = new SqlConnection(connectionString))
    {
        return db.QueryFirstOrDefault<Pais>(sql, new { IdPais = idPais });
    }
}

// Devuelve la información de un deportista según su Id
public static Deportista VerInfoDeportista(int idDeportista)
{
    string sql = "SELECT * FROM Deportistas WHERE IdDeportista = @IdDeportista";

    using (SqlConnection db = new SqlConnection(connectionString))
    {
        return db.QueryFirstOrDefault<Deportista>(sql, new { IdDeportista = idDeportista });
    }
}

// Devuelve la lista de todos los países
public static List<Pais> ListarPaises()
{
    string sql = "SELECT * FROM Paises";

    using (SqlConnection db = new SqlConnection(connectionString))
    {
        return db.Query<Pais>(sql).ToList();
    }
}

// Devuelve la lista de deportistas en base a un deporte específico public static List<


}
