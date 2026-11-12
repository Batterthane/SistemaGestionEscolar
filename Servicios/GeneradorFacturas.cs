using System.Collections.Generic;

namespace SistemaGestionEscolar;

// [SOLID - SRP] Única responsabilidad: crear y guardar facturas a partir de pagos aprobados.
public class GeneradorFacturas
{
    private int siguienteNumero = 1;

    public List<Factura> Facturas { get; } = new List<Factura>();

    // Devuelve la factura, o null si el pago fue rechazado (un pago rechazado no se factura).
    public Factura Generar(Pago pago)
    {
        if (!pago.Aprobado)
        {
            return null;
        }

        Factura factura = new Factura(siguienteNumero, pago);
        siguienteNumero++;
        Facturas.Add(factura);
        return factura;
    }
}
