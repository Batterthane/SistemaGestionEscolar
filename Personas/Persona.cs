namespace SistemaGestionEscolar;

// Persona: clase ABSTRACTA. No se puede crear con "new Persona(...)".
// Sirve de molde con los datos comunes de todas las personas del colegio.
// [SOLID - LSP] Estudiante, Profesor, PadreTutor y Administrativo se pueden usar
// en cualquier lugar donde se espere una Persona.
public abstract class Persona
{
    public int Id { get; }
    public string Nombre { get; }
    public string Apellido { get; }
    public int Edad { get; }

    // Propiedad calculada: se arma con otros datos, no se guarda.
    public string NombreCompleto
    {
        get { return Nombre + " " + Apellido; }
    }

    protected Persona(int id, string nombre, string apellido, int edad)
    {
        Id = id;
        Nombre = nombre;
        Apellido = apellido;
        Edad = edad;
    }

    // Método abstracto: cada clase hija DEBE escribir su propia versión.
    public abstract string ObtenerRol();

    // Método virtual: tiene una versión base, y las hijas pueden ampliarla con "override".
    public virtual string ObtenerResumen()
    {
        return "[" + ObtenerRol() + "] " + NombreCompleto + ", " + Edad + " años (ID " + Id + ")";
    }
}
