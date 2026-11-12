namespace SistemaGestionEscolar;

// PagoTarjeta: otra forma de pagar. IMPLEMENTA IMetodoPago.
// [SOLID - OCP] Se agregó SIN modificar ProcesadorPagos.
// [SOLID - LSP] Cumple el contrato: nunca lanza errores, devuelve un Resultado.
public class PagoTarjeta : IMetodoPago
{
    private string numeroTarjeta;
    private string titular;

    public string Nombre
    {
        get { return "Tarjeta"; }
    }

    public PagoTarjeta(string numeroTarjeta, string titular)
    {
        // Se quitan los espacios para aceptar "1234 5678 1234 5678".
        this.numeroTarjeta = numeroTarjeta.Replace(" ", "");
        this.titular = titular;
    }

    public Resultado Pagar(decimal monto)
    {
        if (numeroTarjeta.Length != 16)
        {
            return new Resultado(false, "Número de tarjeta inválido: debe tener 16 dígitos.");
        }

        foreach (char caracter in numeroTarjeta)
        {
            if (!char.IsDigit(caracter))
            {
                return new Resultado(false, "Número de tarjeta inválido: solo se permiten dígitos.");
            }
        }

        string ultimosCuatro = numeroTarjeta.Substring(12);
        return new Resultado(true, "Pago con tarjeta terminada en " + ultimosCuatro + " aprobado. Titular: " + titular + ".");
    }
}
