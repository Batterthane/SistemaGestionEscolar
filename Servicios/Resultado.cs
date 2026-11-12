namespace SistemaGestionEscolar;

// Resultado: respuesta sencilla de una operación.
// Los servicios NO escriben en pantalla; devuelven un Resultado y quien llama decide qué mostrar.
public class Resultado
{
    public bool Exito { get; }
    public string Mensaje { get; }

    public Resultado(bool exito, string mensaje)
    {
        Exito = exito;
        Mensaje = mensaje;
    }

    // Devuelve el mensaje con una etiqueta [OK] o [ERROR] delante.
    public string ObtenerTexto()
    {
        if (Exito)
        {
            return "[OK] " + Mensaje;
        }
        return "[ERROR] " + Mensaje;
    }
}
