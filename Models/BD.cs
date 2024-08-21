using System.Collections.Generic;
using System.Data.SqlClient;
using Dapper;

public static class BD
{
    private static string _connectionString = @"Server=MATIAS_ILAN\SQLEXPRESS; DataBase=JJOO; Trusted_Connection=True;";

    // Método para agregar un deportista a la base de datos
    public static void AgregarDeportista(Deportista deportista)
    {
        using (SqlConnection connection = new SqlConnection(_connectionString))
        {
            string sql = @"
                INSERT INTO Deportista (Nombre, Apellido, IdDeporte, IdPais, Imagen)
                VALUES (@Nombre, @Apellido, @IdDeporte, @IdPais, @Imagen)";
            
            connection.Execute(sql, new
            {
                Nombre = deportista.Nombre,
                Apellido = deportista.Apellido,
                IdDeporte = deportista.IdDeporte,
                IdPais = deportista.IdPais,
                Imagen = deportista.Imagen
            });
        }
    }

    // Método para eliminar un deportista de la base de datos
    public static void EliminarDeportista(int idDeportista)
    {
        using (SqlConnection connection = new SqlConnection(_connectionString))
        {
            string sql = "DELETE FROM Deportista WHERE IdDeportista = @IdDeportista";
            connection.Execute(sql, new { IdDeportista = idDeportista });
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
            return connection.Query<Pais>(sql).AsList();
        }
    }

     public static List<Deporte> ListarDeportes()
    {
        using (SqlConnection con = new SqlConnection(connectionString))
        {
            string query = "SELECT * FROM Deportes";
            return con.Query<Deporte>(query).AsList();
        }
    }

    // Método para listar deportistas en base a un deporte específico
    public static List<Deportista> ListarDeportistas(int idDeporte)
    {
        using (SqlConnection connection = new SqlConnection(_connectionString))
        {
            string sql = "SELECT * FROM Deportistas WHERE IdDeporte = @IdDeporte";
            return connection.Query<Deportista>(sql, new { IdDeporte = idDeporte }).AsList();
        }
    }

    // Método para listar deportistas en base a un país específico
    public static List<Deportista> ListarDeportistasPorPais(int idPais)
    {
        using (SqlConnection connection = new SqlConnection(_connectionString))
        {
            string sql = "SELECT * FROM Deportistas WHERE IdPais = @IdPais";
            return connection.Query<Deportista>(sql, new { IdPais = idPais }).AsList();
        }
    }
}
