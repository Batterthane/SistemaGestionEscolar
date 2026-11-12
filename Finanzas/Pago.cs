using System;

namespace SistemaGestionEscolar;

// Pago: registro de un cobro. Guarda quién paga, por quién, cuánto, por qué
// concepto, quién lo recibió y si fue aprobado o rechazado.
// El responsable se guarda como IResponsablePago (interfaz). [SOLID - DIP]
public class Pago
{
    public DateTime Fecha { get; }
    public IResponsablePago Responsable { get; }
    public Estudiante Estudiante { get; }
    public decimal Monto { get; }
    public string Concepto { get; }
    public Administrativo RecibidoPor { get; }
    public string MetodoUsado { get; private set; }
    public bool Aprobado { get; private set; }
    public string Detalle { get; private set; }

    public Pago(IResponsablePago responsable, Estudiante estudiante, decimal monto, string concepto, Administrativo recibidoPor)
    {
        Fecha = DateTime.Now;
        Responsable = responsable;
        Estudiante = estudiante;
        Monto = monto;
        Concepto = concepto;
        RecibidoPor = recibidoPor;
        MetodoUsado = "Sin definir";
        Aprobado = false;
        Detalle = "Pendiente";
    }

    public void MarcarAprobado(string metodo, string detalle)
    {
        MetodoUsado = metodo;
        Aprobado = true;
        Detalle = detalle;
    }

    public void MarcarRechazado(string metodo, string detalle)
    {
        MetodoUsado = metodo;
        Aprobado = false;
        Detalle = detalle;
    }

    public string ObtenerResumen()
    {
        string estado = "RECHAZADO";
        if (Aprobado)
        {
            estado = "APROBADO";
        }
        return "Pago " + estado + " | " + Concepto + " | Monto: " + Monto.ToString("N2")
            + " | Método: " + MetodoUsado + " | " + Detalle;
    }
}
