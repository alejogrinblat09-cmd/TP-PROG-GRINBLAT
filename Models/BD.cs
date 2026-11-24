
using Microsoft.Data.SqlClient;
using Dapper;

namespace TP_PROG_GRINBLAT.Models;

public class BD
{
    private string _connectionString =
        @"Server=localhost;Database=ApoyoEscolar;Integrated Security=True;TrustServerCertificate=True;";

    public SqlConnection ObtenerConexion()
    {
        return new SqlConnection(_connectionString);
    }
}
