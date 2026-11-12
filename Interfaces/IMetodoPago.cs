namespace SistemaGestionEscolar;

// [SOLID - ISP / OCP / DIP] Contrato de CUALQUIER forma de pagar.
// ProcesadorPagos solo conoce esta interfaz. Para agregar un método nuevo
// (por ejemplo, cheque) se crea una clase nueva que la implemente.
//
// Contrato que TODA implementación debe cumplir (esto garantiza el LSP):
//   - Pagar(monto) nunca lanza errores: si algo falla, devuelve Resultado con Exito = false.
//   - Pagar(monto) devuelve Exito = true solo cuando el pago realmente se pudo hacer.
public interface IMetodoPago
{
    string Nombre { get; }
    Resultado Pagar(decimal monto);
}
