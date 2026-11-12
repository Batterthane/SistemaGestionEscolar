namespace SistemaGestionEscolar;

// PagoTransferencia: otra forma de pagar. IMPLEMENTA IMetodoPago.
// [SOLID - OCP] Se agregó SIN modificar ProcesadorPagos.
// [SOLID - LSP] Cumple el contrato: nunca lanza errores, devuelve un Resultado.
public class PagoTransferencia : IMetodoPago
{
    private string banco;
    private string referencia;

    public string Nombre
    {
        get { return "Transferencia"; }
    }

    public PagoTransferencia(string banco, string referencia)
    {
        this.banco = banco;
        this.referencia = referencia;
    }

    public Resultado Pagar(decimal monto)
    {
        if (string.IsNullOrWhiteSpace(banco))
        {
            return new Resultado(false, "Debe indicar el banco de origen.");
        }

        if (string.IsNullOrWhiteSpace(referencia) || referencia.Length < 6)
        {
            return new Resultado(false, "La referencia debe tener al menos 6 caracteres.");
        }

        return new Resultado(true, "Transferencia verificada. Banco: " + banco + ", referencia: " + referencia + ".");
    }
}
