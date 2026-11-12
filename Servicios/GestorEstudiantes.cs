using System.Collections.Generic;

namespace SistemaGestionEscolar;

// [SOLID - SRP] Única responsabilidad: administrar estudiantes y tutores
// (registrarlos y vincularlos entre sí). No cobra, no califica, no arma cursos.
public class GestorEstudiantes
{
    private int siguienteIdEstudiante = 1;
    private int siguienteIdTutor = 1;

    public List<Estudiante> Estudiantes { get; } = new List<Estudiante>();
    public List<PadreTutor> Tutores { get; } = new List<PadreTutor>();

    public Estudiante RegistrarEstudiante(string nombre, string apellido, int edad)
    {
        Estudiante estudiante = new Estudiante(siguienteIdEstudiante, nombre, apellido, edad);
        siguienteIdEstudiante++;
        Estudiantes.Add(estudiante);
        return estudiante;
    }

    public PadreTutor RegistrarTutor(string nombre, string apellido, int edad, string telefono, string parentesco)
    {
        PadreTutor tutor = new PadreTutor(siguienteIdTutor, nombre, apellido, edad, telefono, parentesco);
        siguienteIdTutor++;
        Tutores.Add(tutor);
        return tutor;
    }

    // Relaciona un tutor con un estudiante (en ambos sentidos).
    public Resultado VincularTutor(Estudiante estudiante, PadreTutor tutor)
    {
        if (estudiante.Tutores.Contains(tutor))
        {
            return new Resultado(false, tutor.NombreCompleto + " ya es tutor de " + estudiante.NombreCompleto + ".");
        }

        estudiante.AgregarTutor(tutor);
        tutor.AgregarHijo(estudiante);
        return new Resultado(true, tutor.NombreCompleto + " (" + tutor.Parentesco + ") fue vinculado como tutor de "
            + estudiante.NombreCompleto + ".");
    }
}
