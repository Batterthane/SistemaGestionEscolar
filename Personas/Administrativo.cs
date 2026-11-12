namespace SistemaGestionEscolar;

// Administrativo: personal del colegio. Recibe los pagos (ver clase Pago).
// No implementa ninguna interfaz porque no la necesita (ISP).
public class Administrativo : Persona
{
    public string Cargo { get; }

    public Administrativo(int id, string nombre, string apellido, int edad, string cargo)
        : base(id, nombre, apellido, edad)
    {
        Cargo = cargo;
    }

    public override string ObtenerRol()
    {
        return "Administrativo";
    }

    public override string ObtenerResumen()
    {
        return base.ObtenerResumen() + " | Cargo: " + Cargo;
    }
}
