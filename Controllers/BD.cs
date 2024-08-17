using System.Collections.Generic;
using System.Data.SqlClient;
using Dapper;
using TP_JJOO_2.Models;

public static class BD
{
private static string _connectionString = @"Server=MATIAS_ILAN\SQLEXPRESS; Database=TP_JJOO_2; Trusted_Connection=True;";

    // Agregar un deportista
    public static void AgregarDeportista(Deportista deportista)
    {
        string sql = "INSERT INTO Deportistas (Nombre, Apellido, IdDeporte, IdPais, Imagen) VALUES (@Nombre, @Apellido, @IdDeporte, @IdPais, @Imagen)";
        using (SqlConnection db = new SqlConnection(_connectionString))
        {
            db.Execute(sql, new { deportista.Nombre, deportista.Apellido, deportista.IdDeporte, deportista.IdPais, deportista.Imagen });
        }
    }

    // Eliminar un deportista
    public static void EliminarDeportista(int idDeportista)
    {
        string sql = "DELETE FROM Deportistas WHERE IdDeportista = @IdDeportista";
        using (SqlConnection db = new SqlConnection(_connectionString))
        {
            db.Execute(sql, new { IdDeportista = idDeportista });
        }
    }

    // Obtener información de un deporte
    public static Deporte VerInfoDeporte(int idDeporte)
    {
        string sql = "SELECT * FROM Deportes WHERE IdDeporte = @IdDeporte";
        using (SqlConnection db = new SqlConnection(_connectionString))
        {
            return db.QueryFirstOrDefault<Deporte>(sql, new { IdDeporte = idDeporte });
        }
    }

    // Obtener información de un país
    public static Pais VerInfoPais(int idPais)
    {
        string sql = "SELECT * FROM Paises WHERE IdPais = @IdPais";
        using (SqlConnection db = new SqlConnection(_connectionString))
        {
            return db.QueryFirstOrDefault<Pais>(sql, new { IdPais = idPais });
        }
    }

    // Obtener información de un deportista
    public static Deportista VerInfoDeportista(int idDeportista)
    {
        string sql = "SELECT * FROM Deportistas WHERE IdDeportista = @IdDeportista";
        using (SqlConnection db = new SqlConnection(_connectionString))
        {
            return db.QueryFirstOrDefault<Deportista>(sql, new { IdDeportista = idDeportista });
        }
    }

    // Listar todos los países
    public static List<Pais> ListarPaises()
    {
        string sql = "SELECT * FROM Paises";
        using (SqlConnection db = new SqlConnection(_connectionString))
        {
            return db.Query<Pais>(sql).AsList();
        }
    }

    // Listar deportistas por deporte
    public static List<Deportista> ListarDeportistas(int idDeporte)
    {
        string sql = "SELECT * FROM Deportistas WHERE IdDeporte = @IdDeporte";
        using (SqlConnection db = new SqlConnection(_connectionString))
        {
            return db.Query<Deportista>(sql, new { IdDeporte = idDeporte }).AsList();
        }
    }

    // Listar deportistas por país
    public static List<Deportista> ListarDeportistasPorPais(int idPais)
    {
        string sql = "SELECT * FROM Deportistas WHERE IdPais = @IdPais";
        using (SqlConnection db = new SqlConnection(_connectionString))
        {
            return db.Query<Deportista>(sql, new { IdPais = idPais }).AsList();
        }
    }

    // Listar todos los deportes
    public static List<Deporte> ListarDeportes()
    {
        string sql = "SELECT * FROM Deportes";
        using (SqlConnection db = new SqlConnection(_connectionString))
        {
            return db.Query<Deporte>(sql).AsList();
        }
    }
}
