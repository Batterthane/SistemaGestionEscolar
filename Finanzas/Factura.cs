using System;

namespace SistemaGestionEscolar;

// Factura: comprobante que se genera a partir de un Pago aprobado.
// GeneradorFacturas se encarga de crearla; aquí solo se guardan y se muestran los datos.
public class Factura
{
    public string Numero { get; }
    public DateTime Fecha { get; }
    public Pago Pago { get; }

    public Factura(int consecutivo, Pago pago)
    {
        Numero = "FAC-" + consecutivo.ToString("0000");
        Fecha = DateTime.Now;
        Pago = pago;
    }

    public string ObtenerTexto()
    {
        string linea = new string('=', 46);
        string salto = Environment.NewLine;

        string texto = linea + salto;
        texto = texto + "  FACTURA " + Numero + salto;
        texto = texto + linea + salto;
        texto = texto + "  Fecha:        " + Fecha.ToString("dd/MM/yyyy HH:mm") + salto;
        texto = texto + "  Cliente:      " + Pago.Responsable.NombreCompleto + " (Tel. " + Pago.Responsable.Telefono + ")" + salto;
        texto = texto + "  Estudiante:   " + Pago.Estudiante.NombreCompleto + " (" + Pago.Estudiante.Matricula + ")" + salto;
        texto = texto + "  Concepto:     " + Pago.Concepto + salto;
        texto = texto + "  Método:       " + Pago.MetodoUsado + salto;
        texto = texto + "  Total:        " + Pago.Monto.ToString("N2") + salto;
        texto = texto + "  Atendido por: " + Pago.RecibidoPor.NombreCompleto + " (" + Pago.RecibidoPor.Cargo + ")" + salto;
        texto = texto + "  Estado:       PAGADA" + salto;
        texto = texto + linea;
        return texto;
    }
}
