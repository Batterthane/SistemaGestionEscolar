namespace SistemaGestionEscolar;

// [SOLID - ISP / DIP] Describe a "alguien que puede pagar" con solo dos datos.
// Pago y Factura trabajan con esta interfaz, no con la clase concreta PadreTutor.
public interface IResponsablePago
{
    string NombreCompleto { get; }
    string Telefono { get; }
}
