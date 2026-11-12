using System.Collections.Generic;

namespace SistemaGestionEscolar;

// PadreTutor HEREDA de Persona e IMPLEMENTA IResponsablePago.
// Telefono (exigido por IResponsablePago) es una propiedad propia de esta clase.
public class PadreTutor : Persona, IResponsablePago
{
    public string Telefono { get; }
    public string Parentesco { get; }
    public List<Estudiante> Hijos { get; } = new List<Estudiante>();

    public PadreTutor(int id, string nombre, string apellido, int edad, string telefono, string parentesco)
        : base(id, nombre, apellido, edad)
    {
        Telefono = telefono;
        Parentesco = parentesco;
    }

    public override string ObtenerRol()
    {
        return "Tutor";
    }

    public override string ObtenerResumen()
    {
        return base.ObtenerResumen() + " | " + Parentesco + " | Tel: " + Telefono + " | Estudiantes a cargo: " + Hijos.Count;
    }

    public void AgregarHijo(Estudiante estudiante)
    {
        Hijos.Add(estudiante);
    }
}
