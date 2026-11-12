namespace SistemaGestionEscolar;

// Asignatura: una materia (ej. Matemática). Conoce a su docente a través de la
// interfaz IDocente, no de la clase concreta Profesor. [SOLID - DIP]
public class Asignatura
{
    public string Codigo { get; }
    public string Nombre { get; }
    public IDocente Docente { get; private set; }

    public Asignatura(string codigo, string nombre)
    {
        Codigo = codigo;
        Nombre = nombre;
    }

    public void AsignarDocente(IDocente docente)
    {
        Docente = docente;
    }

    public string ObtenerResumen()
    {
        string nombreDocente = "sin profesor";
        if (Docente != null)
        {
            nombreDocente = Docente.NombreCompleto;
        }
        return Codigo + " - " + Nombre + " (Profesor: " + nombreDocente + ")";
    }
}
