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

    public Usuario? ObtenerUsuarioPorLogin(string nombreUsuario, string contrasena)
    {
        using (SqlConnection conexion = ObtenerConexion())
        {
            string sql = "SELECT * FROM Usuarios WHERE NombreUsuario = @pNombreUsuario AND Contrasena = @pContrasena";
            return conexion.QueryFirstOrDefault<Usuario>(sql, new { pNombreUsuario = nombreUsuario, pContrasena = contrasena });
        }
    }
}
