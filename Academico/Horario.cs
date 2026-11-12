using System;

namespace SistemaGestionEscolar;

// Horario: un bloque de clase (día, hora de inicio, hora de fin y asignatura).
// Las horas se guardan como números enteros en formato 24 h (ej. 8 = 08:00).
public class Horario
{
    public string Dia { get; }
    public int HoraInicio { get; }
    public int HoraFin { get; }
    public Asignatura Asignatura { get; }

    public Horario(string dia, int horaInicio, int horaFin, Asignatura asignatura)
    {
        Dia = dia;
        HoraInicio = horaInicio;
        HoraFin = horaFin;
        Asignatura = asignatura;
    }

    // Dos horarios chocan si son el mismo día y sus horas se cruzan.
    public bool SeSolapaCon(Horario otro)
    {
        if (!string.Equals(Dia, otro.Dia, StringComparison.OrdinalIgnoreCase))
        {
            return false;
        }
        return HoraInicio < otro.HoraFin && otro.HoraInicio < HoraFin;
    }

    public string ObtenerResumen()
    {
        return Dia + " " + HoraInicio.ToString("00") + ":00-" + HoraFin.ToString("00") + ":00 " + Asignatura.Nombre;
    }
}
