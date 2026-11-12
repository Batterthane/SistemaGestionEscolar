namespace SistemaGestionEscolar;

// [SOLID - ISP] Interfaz pequeña: solo lo que necesita alguien a quien se le ponen notas.
// Un Profesor no tiene por qué implementar estos métodos.
public interface ICalificable
{
    string NombreCompleto { get; }
    void AgregarCalificacion(Calificacion calificacion);
    double CalcularPromedio();
}
