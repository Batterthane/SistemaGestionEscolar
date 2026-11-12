using System.Collections.Generic;

namespace SistemaGestionEscolar;

// Estudiante HEREDA de Persona (": Persona") e IMPLEMENTA la interfaz ICalificable.
public class Estudiante : Persona, ICalificable
{
    public string Matricula { get; }
    public Curso CursoActual { get; set; }
    public List<PadreTutor> Tutores { get; } = new List<PadreTutor>();
    public List<Calificacion> Calificaciones { get; } = new List<Calificacion>();

    public Estudiante(int id, string nombre, string apellido, int edad)
        : base(id, nombre, apellido, edad)
    {
        Matricula = "EST-" + id.ToString("000");
    }

    public override string ObtenerRol()
    {
        return "Estudiante";
    }

    // override: amplía el resumen de Persona agregando datos propios del estudiante.
    public override string ObtenerResumen()
    {
        string nombreCurso = "sin curso";
        if (CursoActual != null)
        {
            nombreCurso = CursoActual.Nombre;
        }
        return base.ObtenerResumen() + " | Matrícula: " + Matricula + " | Curso: " + nombreCurso;
    }

    public void AgregarTutor(PadreTutor tutor)
    {
        Tutores.Add(tutor);
    }

    // Método exigido por ICalificable.
    public void AgregarCalificacion(Calificacion calificacion)
    {
        Calificaciones.Add(calificacion);
    }

    // Método exigido por ICalificable.
    public double CalcularPromedio()
    {
        if (Calificaciones.Count == 0)
        {
            return 0;
        }

        double suma = 0;
        foreach (Calificacion calificacion in Calificaciones)
        {
            suma = suma + calificacion.Valor;
        }
        return suma / Calificaciones.Count;
    }
}
