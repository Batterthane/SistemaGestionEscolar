using System.Collections.Generic;

namespace SistemaGestionEscolar;

// [SOLID - SRP] Única responsabilidad: la estructura académica del colegio
// (aulas, asignaturas y cursos) y sus reglas: inscribir, asignar aula, agregar horarios.
public class GestorCursos
{
    public List<Aula> Aulas { get; } = new List<Aula>();
    public List<Asignatura> Asignaturas { get; } = new List<Asignatura>();
    public List<Curso> Cursos { get; } = new List<Curso>();

    public Aula CrearAula(string nombre, int capacidad)
    {
        Aula aula = new Aula(nombre, capacidad);
        Aulas.Add(aula);
        return aula;
    }

    public Asignatura CrearAsignatura(string codigo, string nombre)
    {
        Asignatura asignatura = new Asignatura(codigo, nombre);
        Asignaturas.Add(asignatura);
        return asignatura;
    }

    public Curso CrearCurso(string nombre)
    {
        Curso curso = new Curso(nombre);
        Cursos.Add(curso);
        return curso;
    }

    public Resultado AgregarAsignaturaACurso(Curso curso, Asignatura asignatura)
    {
        if (curso.Asignaturas.Contains(asignatura))
        {
            return new Resultado(false, "La asignatura " + asignatura.Nombre + " ya está en el curso " + curso.Nombre + ".");
        }

        curso.AgregarAsignatura(asignatura);
        return new Resultado(true, "Asignatura " + asignatura.Nombre + " agregada al curso " + curso.Nombre + ".");
    }

    public Resultado AsignarAula(Curso curso, Aula aula)
    {
        if (aula.Capacidad < curso.Estudiantes.Count)
        {
            return new Resultado(false, "El aula " + aula.Nombre + " es demasiado pequeña para los "
                + curso.Estudiantes.Count + " estudiantes ya inscritos.");
        }

        foreach (Curso otro in Cursos)
        {
            if (otro != curso && otro.Aula == aula)
            {
                return new Resultado(false, "El aula " + aula.Nombre + " ya está asignada al curso " + otro.Nombre + ".");
            }
        }

        curso.AsignarAula(aula);
        return new Resultado(true, "Aula " + aula.Nombre + " asignada al curso " + curso.Nombre
            + " (capacidad: " + aula.Capacidad + ").");
    }

    public Resultado AgregarHorario(Curso curso, Horario horario)
    {
        if (horario.HoraInicio >= horario.HoraFin)
        {
            return new Resultado(false, "La hora de fin debe ser mayor que la hora de inicio.");
        }

        if (!curso.Asignaturas.Contains(horario.Asignatura))
        {
            return new Resultado(false, "La asignatura " + horario.Asignatura.Nombre + " no pertenece al curso " + curso.Nombre + ".");
        }

        foreach (Horario existente in curso.Horarios)
        {
            if (existente.SeSolapaCon(horario))
            {
                return new Resultado(false, "Choque de horario con: " + existente.ObtenerResumen() + ".");
            }
        }

        curso.AgregarHorario(horario);
        return new Resultado(true, "Horario agregado a " + curso.Nombre + ": " + horario.ObtenerResumen() + ".");
    }

    public Resultado InscribirEstudiante(Estudiante estudiante, Curso curso)
    {
        if (estudiante.CursoActual != null)
        {
            return new Resultado(false, estudiante.NombreCompleto + " ya está inscrito(a) en " + estudiante.CursoActual.Nombre + ".");
        }

        if (curso.Aula == null)
        {
            return new Resultado(false, "El curso " + curso.Nombre + " todavía no tiene aula asignada.");
        }

        if (!curso.TieneCupo())
        {
            return new Resultado(false, "El curso " + curso.Nombre + " está lleno (capacidad: " + curso.Aula.Capacidad + ").");
        }

        curso.AgregarEstudiante(estudiante);
        return new Resultado(true, estudiante.NombreCompleto + " fue inscrito(a) en " + curso.Nombre + ".");
    }
}
