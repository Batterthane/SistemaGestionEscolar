using System.Collections.Generic;

namespace SistemaGestionEscolar;

// Profesor HEREDA de Persona e IMPLEMENTA IDocente.
// NombreCompleto (exigido por IDocente) ya existe porque lo hereda de Persona.
public class Profesor : Persona, IDocente
{
    public string Especialidad { get; }
    public List<Asignatura> Asignaturas { get; } = new List<Asignatura>();

    public Profesor(int id, string nombre, string apellido, int edad, string especialidad)
        : base(id, nombre, apellido, edad)
    {
        Especialidad = especialidad;
    }

    public override string ObtenerRol()
    {
        return "Profesor";
    }

    public override string ObtenerResumen()
    {
        return base.ObtenerResumen() + " | Especialidad: " + Especialidad + " | Asignaturas: " + Asignaturas.Count;
    }

    // Método exigido por IDocente.
    public void AsignarAsignatura(Asignatura asignatura)
    {
        Asignaturas.Add(asignatura);
    }
}
