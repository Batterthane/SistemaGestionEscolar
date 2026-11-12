namespace SistemaGestionEscolar;

// Calificacion: una nota (0 a 100) que un estudiante obtuvo en una asignatura.
public class Calificacion
{
    public Asignatura Asignatura { get; }
    public double Valor { get; }
    public string Descripcion { get; }

    public Calificacion(Asignatura asignatura, double valor, string descripcion)
    {
        Asignatura = asignatura;
        Valor = valor;
        Descripcion = descripcion;
    }

    public string ObtenerResumen()
    {
        return Asignatura.Nombre + ": " + Valor.ToString("0.0") + " (" + Descripcion + ")";
    }
}
