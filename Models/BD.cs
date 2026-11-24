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
        public bool ExisteNombreUsuario(string nombreUsuario)
    {
        using (SqlConnection conexion = ObtenerConexion())
        {
            string sql = "SELECT * FROM Usuarios WHERE NombreUsuario = @pNombreUsuario";
            Usuario? usuario = conexion.QueryFirstOrDefault<Usuario>(sql, new { pNombreUsuario = nombreUsuario });
            return usuario != null;
        }
    }
        public void AgregarUsuario(Usuario usuario)
    {
        using (SqlConnection conexion = ObtenerConexion())
        {
            string sql = "INSERT INTO Usuarios (NombreUsuario, Contrasena, Nombre, Apellido) VALUES (@pNombreUsuario, @pContrasena, @pNombre, @pApellido)";
            conexion.Execute(sql, new {
                pNombreUsuario = usuario.NombreUsuario,
                pContrasena = usuario.Contrasena,
                pNombre = usuario.Nombre,
                pApellido = usuario.Apellido
            });
        }
    }
}
