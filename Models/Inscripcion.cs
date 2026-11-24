namespace TP_PROG_GRINBLAT.Models;

public class Inscripcion{
    public int Id {get; set;}
    public int IdClaseApoyo {get; set;}
    public int IdUsuario {get; set;}
    public DateTime FechaInscripcion {get; set;}
    public bool Cancelado {get; set;}
    public DateTime? FechaCancelacion {get; set;}
    public DateTime FechaHora {get; set;}
    public string NombreMateria {get; set;}
    public string NombreTutor {get; set;}
}
