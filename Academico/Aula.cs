namespace SistemaGestionEscolar;

// Aula: espacio físico con una capacidad máxima de estudiantes.
public class Aula
{
    public string Nombre { get; }
    public int Capacidad { get; }

    public Aula(string nombre, int capacidad)
    {
        Nombre = nombre;
        Capacidad = capacidad;
    }

    public string ObtenerResumen()
    {
        return "Aula " + Nombre + " (capacidad: " + Capacidad + ")";
    }
}
