
using Microsoft.Data.SqlClient;
using Dapper;

namespace MiProyectoMVC.Models;

public class BD
{
    private string _connectionString =
        @"Server=localhost;Database=MiBaseMVC;Integrated Security=True;TrustServerCertificate=True;";

    public SqlConnection ObtenerConexion()
    {
        return new SqlConnection(_connectionString);
    }
}
