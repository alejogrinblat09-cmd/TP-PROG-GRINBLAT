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
        public List<Tutor> ObtenerTutores(int desde, int cantidad)
    {
        using (SqlConnection conexion = ObtenerConexion())
        {
            string sql = "SELECT * FROM Tutores WHERE Activo = 1 ORDER BY Nombre OFFSET @pDesde ROWS FETCH NEXT @pCantidad ROWS ONLY";
            return conexion.Query<Tutor>(sql, new { pDesde = desde, pCantidad = cantidad }).ToList();
        }
    }

    public List<Materia> ObtenerMateriasDeTutor(int idTutor)
    {
        using (SqlConnection conexion = ObtenerConexion())
        {
            string sql = "SELECT Materias.* FROM Materias INNER JOIN TutoresMaterias ON Materias.Id = TutoresMaterias.IdMateria WHERE TutoresMaterias.IdTutor = @pIdTutor";
            return conexion.Query<Materia>(sql, new { pIdTutor = idTutor }).ToList();
        }
    }
    
    public Tutor? ObtenerTutor(int id)
    {
        using (SqlConnection conexion = ObtenerConexion())
        {
            string sql = "SELECT * FROM Tutores WHERE Id = @pId AND Activo = 1";
            return conexion.QueryFirstOrDefault<Tutor>(sql, new { pId = id });
        }
    }

    public List<ClaseApoyo> ObtenerClasesDeTutor(int idTutor)
    {
        using (SqlConnection conexion = ObtenerConexion())
        {
            string sql = "SELECT ClasesApoyo.*, Materias.Nombre AS NombreMateria FROM ClasesApoyo INNER JOIN Materias ON ClasesApoyo.IdMateria = Materias.Id WHERE ClasesApoyo.IdTutor = @pIdTutor AND ClasesApoyo.FechaHora > GETDATE() ORDER BY ClasesApoyo.FechaHora";
            return conexion.Query<ClaseApoyo>(sql, new { pIdTutor = idTutor }).ToList();
        }
    }

    public ClaseApoyo? ObtenerClase(int idClase)
    {
        using (SqlConnection conexion = ObtenerConexion())
        {
            string sql = "SELECT ClasesApoyo.*, Materias.Nombre AS NombreMateria FROM ClasesApoyo INNER JOIN Materias ON ClasesApoyo.IdMateria = Materias.Id WHERE ClasesApoyo.Id = @pIdClase";
            return conexion.QueryFirstOrDefault<ClaseApoyo>(sql, new { pIdClase = idClase });
        }
    }

    public int ContarInscriptos(int idClase)
    {
        using (SqlConnection conexion = ObtenerConexion())
        {
            string sql = "SELECT COUNT(*) FROM Inscripciones WHERE IdClaseApoyo = @pIdClase AND Cancelado = 0";
            return conexion.QueryFirstOrDefault<int>(sql, new { pIdClase = idClase });
        }
    }

    public bool EstaInscripto(int idClase, int idUsuario)
    {
        using (SqlConnection conexion = ObtenerConexion())
        {
            string sql = "SELECT * FROM Inscripciones WHERE IdClaseApoyo = @pIdClase AND IdUsuario = @pIdUsuario AND Cancelado = 0";
            Inscripcion? inscripcion = conexion.QueryFirstOrDefault<Inscripcion>(sql, new { pIdClase = idClase, pIdUsuario = idUsuario });
            return inscripcion != null;
        }
    }

    public void AgregarInscripcion(int idClase, int idUsuario)
    {
        using (SqlConnection conexion = ObtenerConexion())
        {
            string sql = "INSERT INTO Inscripciones (IdClaseApoyo, IdUsuario, FechaInscripcion, Cancelado) VALUES (@pIdClase, @pIdUsuario, GETDATE(), 0)";
            conexion.Execute(sql, new { pIdClase = idClase, pIdUsuario = idUsuario });
        }
    }
}
