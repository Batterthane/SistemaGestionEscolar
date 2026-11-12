namespace SistemaGestionEscolar;

// [SOLID - ISP] Interfaz pequeña: solo lo que necesita quien enseña asignaturas.
// Un Estudiante no tiene por qué implementar estos métodos.
public interface IDocente
{
    string NombreCompleto { get; }
    void AsignarAsignatura(Asignatura asignatura);
}
