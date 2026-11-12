using System.Collections.Generic;

namespace SistemaGestionEscolar;

// [SOLID - SRP] Única responsabilidad: administrar profesores y asignarlos a asignaturas.
public class GestorProfesores
{
    private int siguienteId = 1;

    public List<Profesor> Profesores { get; } = new List<Profesor>();

    public Profesor RegistrarProfesor(string nombre, string apellido, int edad, string especialidad)
    {
        Profesor profesor = new Profesor(siguienteId, nombre, apellido, edad, especialidad);
        siguienteId++;
        Profesores.Add(profesor);
        return profesor;
    }

    // [SOLID - DIP] Recibe un IDocente (interfaz), no un Profesor concreto.
    public Resultado AsignarAsignatura(IDocente docente, Asignatura asignatura)
    {
        if (asignatura.Docente != null)
        {
            return new Resultado(false, "La asignatura " + asignatura.Nombre + " ya tiene profesor: "
                + asignatura.Docente.NombreCompleto + ".");
        }

        asignatura.AsignarDocente(docente);
        docente.AsignarAsignatura(asignatura);
        return new Resultado(true, docente.NombreCompleto + " fue asignado(a) a la asignatura " + asignatura.Nombre + ".");
    }
}
