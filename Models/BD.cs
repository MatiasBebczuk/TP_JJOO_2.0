using System.Collections.Generic;
using System.Data.SqlClient;
using Dapper;

public static class BD
{
    private static string _connectionString = @"Server=MATIAS_ILAN\SQLEXPRESS; DataBase=JJOO; Trusted_Connection=True;";

    // Listas estáticas para almacenar los datos de deportistas, deportes y países
    public static List<Deportista> Deportistas = new List<Deportista>();
    public static List<Deporte> Deportes = new List<Deporte>();
    public static List<Pais> Paises = new List<Pais>();

    // Método para agregar un deportista a la base de datos
    public static void AgregarDeportista(Deportista dep)
    {
        using (SqlConnection db = new SqlConnection(_connectionString))
        {
            string sqlQuery = "INSERT INTO Deportistas (Nombre, Apellido, IdDeporte, IdPais, Imagen) VALUES (@Nombre, @Apellido, @IdDeporte, @IdPais, @Imagen)";
            db.Execute(sqlQuery, new { dep.Nombre, dep.Apellido, dep.IdDeporte, dep.IdPais, dep.Imagen });

            // Agregar el deportista a la lista estática
            Deportistas.Add(dep);
        }
    }

    // Método para eliminar un deportista de la base de datos
    public static void EliminarDeportista(int idDeportista)
    {
        using (SqlConnection db = new SqlConnection(_connectionString))
        {
            string sqlQuery = "DELETE FROM Deportistas WHERE IdDeportista = @IdDeportista";
            db.Execute(sqlQuery, new { IdDeportista = idDeportista });

            // Eliminar el deportista de la lista estática
            Deportistas.RemoveAll(d => d.IdDeportista == idDeportista);
        }
    }

    // Método para obtener información de un deporte específico
    public static Deporte VerInfoDeporte(int idDeporte)
    {
        using (SqlConnection connection = new SqlConnection(_connectionString))
        {
            string sql = "SELECT * FROM Deportes WHERE IdDeporte = @IdDeporte";
            return connection.QueryFirstOrDefault<Deporte>(sql, new { IdDeporte = idDeporte });
        }
    }

    // Método para obtener información de un país específico
    public static Pais VerInfoPais(int idPais)
    {
        using (SqlConnection connection = new SqlConnection(_connectionString))
        {
            string sql = "SELECT * FROM Paises WHERE IdPais = @IdPais";
            return connection.QueryFirstOrDefault<Pais>(sql, new { IdPais = idPais });
        }
    }

    // Método para obtener información de un deportista específico
    public static Deportista VerInfoDeportista(int idDeportista)
    {
        using (SqlConnection connection = new SqlConnection(_connectionString))
        {
            string sql = "SELECT * FROM Deportistas WHERE IdDeportista = @IdDeportista";
            return connection.QueryFirstOrDefault<Deportista>(sql, new { IdDeportista = idDeportista });
        }
    }

    // Método para listar todos los países
    public static List<Pais> ListarPaises()
    {
        using (SqlConnection connection = new SqlConnection(_connectionString))
        {
            string sql = "SELECT * FROM Paises";
            Paises = connection.Query<Pais>(sql).AsList();
            return Paises;
        }
    }

    // Método para listar todos los deportes
    public static List<Deporte> ListarDeportes()
    {
        using (SqlConnection con = new SqlConnection(_connectionString))
        {
            string query = "SELECT * FROM Deportes";
            Deportes = con.Query<Deporte>(query).AsList();
            return Deportes;
        }
    }

    // Método para listar deportistas en base a un deporte específico
    public static List<Deportista> ListarDeportistas(int idDeporte)
    {
        using (SqlConnection connection = new SqlConnection(_connectionString))
        {
            string sql = "SELECT * FROM Deportistas WHERE IdDeporte = @IdDeporte";
            Deportistas = connection.Query<Deportista>(sql, new { IdDeporte = idDeporte }).AsList();
            return Deportistas;
        }
    }

    // Método para listar deportistas en base a un país específico
    public static List<Deportista> ListarDeportistasPorPais(int idPais)
    {
        using (SqlConnection connection = new SqlConnection(_connectionString))
        {
            string sql = "SELECT * FROM Deportistas WHERE IdPais = @IdPais";
            Deportistas = connection.Query<Deportista>(sql, new { IdPais = idPais }).AsList();
            return Deportistas;
        }
    }
}
