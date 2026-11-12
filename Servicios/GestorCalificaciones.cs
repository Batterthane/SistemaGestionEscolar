using System;

namespace SistemaGestionEscolar;

// [SOLID - SRP] Única responsabilidad: registrar calificaciones y armar el boletín.
public class GestorCalificaciones
{
    // [SOLID - DIP] Recibe un ICalificable (interfaz), no un Estudiante concreto.
    public Resultado RegistrarCalificacion(ICalificable alumno, Asignatura asignatura, double valor, string descripcion)
    {
        if (valor < 0 || valor > 100)
        {
            return new Resultado(false, "La nota debe estar entre 0 y 100. Se recibió: " + valor.ToString("0.0") + ".");
        }

        Calificacion calificacion = new Calificacion(asignatura, valor, descripcion);
        alumno.AgregarCalificacion(calificacion);

        return new Resultado(true, "Calificación registrada para " + alumno.NombreCompleto + ": "
            + calificacion.ObtenerResumen() + ". Promedio actual: " + alumno.CalcularPromedio().ToString("0.0") + ".");
    }

    // Arma el texto del boletín de un estudiante (no lo imprime; solo lo devuelve).
    public string GenerarBoletin(Estudiante estudiante)
    {
        string salto = Environment.NewLine;

        string nombreCurso = "sin curso";
        if (estudiante.CursoActual != null)
        {
            nombreCurso = estudiante.CursoActual.Nombre;
        }

        string texto = "  BOLETÍN DE " + estudiante.NombreCompleto + " (" + estudiante.Matricula + ") - Curso: " + nombreCurso + salto;

        if (estudiante.Calificaciones.Count == 0)
        {
            return texto + "    (sin calificaciones registradas)";
        }

        foreach (Calificacion calificacion in estudiante.Calificaciones)
        {
            texto = texto + "    " + calificacion.Asignatura.Nombre.PadRight(18)
                + calificacion.Valor.ToString("0.0").PadLeft(6) + "  " + calificacion.Descripcion + salto;
        }

        texto = texto + "    Promedio general: " + estudiante.CalcularPromedio().ToString("0.0");
        return texto;
    }
}
