namespace SistemaGestionEscolar;

// [SOLID - SRP] Única responsabilidad: cobrar (no crea facturas, eso lo hace GeneradorFacturas).
//
// [SOLID - DIP] Depende de la INTERFAZ IMetodoPago, nunca de PagoTarjeta, PagoEfectivo, etc.
//               El método de pago llega "desde afuera" por el CONSTRUCTOR (inyección de dependencias).
//
// [SOLID - OCP] Esta clase está CERRADA a modificaciones: si mañana existe PagoCheque,
//               este archivo no cambia. El sistema se EXTIENDE creando una clase nueva.
public class ProcesadorPagos
{
    private IMetodoPago metodoPago;

    public ProcesadorPagos(IMetodoPago metodoPago)
    {
        this.metodoPago = metodoPago;
    }

    public Pago ProcesarPago(IResponsablePago responsable, Estudiante estudiante, decimal monto, string concepto, Administrativo recibidoPor)
    {
        Pago pago = new Pago(responsable, estudiante, monto, concepto, recibidoPor);

        if (monto <= 0)
        {
            pago.MarcarRechazado(metodoPago.Nombre, "El monto debe ser mayor que cero.");
            return pago;
        }

        // Polimorfismo: no sabemos (ni nos importa) cuál clase concreta es metodoPago.
        Resultado resultado = metodoPago.Pagar(monto);

        if (resultado.Exito)
        {
            pago.MarcarAprobado(metodoPago.Nombre, resultado.Mensaje);
        }
        else
        {
            pago.MarcarRechazado(metodoPago.Nombre, resultado.Mensaje);
        }

        return pago;
    }
}
