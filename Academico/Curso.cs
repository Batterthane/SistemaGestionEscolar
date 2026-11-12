using System.Collections.Generic;

namespace SistemaGestionEscolar;

// Curso (grado): agrupa estudiantes, asignaturas y horarios, y ocupa un aula.
// Esta clase guarda datos y sabe hacer cosas simples con ellos.
// Las REGLAS (¿puede inscribirse?, ¿hay choque de horario?) las decide GestorCursos. [SOLID - SRP]
public class Curso
{
    public string Nombre { get; }
    public Aula Aula { get; private set; }
    public List<Asignatura> Asignaturas { get; } = new List<Asignatura>();
    public List<Estudiante> Estudiantes { get; } = new List<Estudiante>();
    public List<Horario> Horarios { get; } = new List<Horario>();

    public Curso(string nombre)
    {
        Nombre = nombre;
    }

    public void AsignarAula(Aula aula)
    {
        Aula = aula;
    }

    // Hay cupo si el curso tiene aula y todavía no se llenó.
    public bool TieneCupo()
    {
        return Aula != null && Estudiantes.Count < Aula.Capacidad;
    }

    public void AgregarEstudiante(Estudiante estudiante)
    {
        Estudiantes.Add(estudiante);
        estudiante.CursoActual = this;
    }

    public void AgregarAsignatura(Asignatura asignatura)
    {
        Asignaturas.Add(asignatura);
    }

    public void AgregarHorario(Horario horario)
    {
        Horarios.Add(horario);
    }

    public string ObtenerResumen()
    {
        string nombreAula = "sin aula";
        if (Aula != null)
        {
            nombreAula = Aula.Nombre;
        }
        return "Curso " + Nombre + " | Aula: " + nombreAula + " | Estudiantes: " + Estudiantes.Count
            + " | Asignaturas: " + Asignaturas.Count + " | Horarios: " + Horarios.Count;
    }
}
