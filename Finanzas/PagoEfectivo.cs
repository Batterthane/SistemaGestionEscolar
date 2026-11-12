namespace SistemaGestionEscolar;

// PagoEfectivo: una forma de pagar. IMPLEMENTA IMetodoPago.
// [SOLID - OCP] Se agregó SIN modificar ProcesadorPagos.
// [SOLID - LSP] Cumple el contrato: nunca lanza errores, devuelve un Resultado.
public class PagoEfectivo : IMetodoPago
{
    private decimal entregado;

    public string Nombre
    {
        get { return "Efectivo"; }
    }

    public PagoEfectivo(decimal montoEntregado)
    {
        entregado = montoEntregado;
    }

    public Resultado Pagar(decimal monto)
    {
        if (entregado < monto)
        {
            return new Resultado(false, "Efectivo insuficiente. Entregado: " + entregado.ToString("N2")
                + ", requerido: " + monto.ToString("N2") + ".");
        }

        decimal cambio = entregado - monto;
        return new Resultado(true, "Pago en efectivo aceptado. Cambio: " + cambio.ToString("N2") + ".");
    }
}
