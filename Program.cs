using System;
using System.Globalization;

namespace SistemaGestionEscolar;

public class Program
{
    public static void Main(string[] args)
    {
        Console.OutputEncoding = System.Text.Encoding.UTF8;
        Console.InputEncoding = System.Text.Encoding.UTF8;

        CultureInfo.CurrentCulture = CultureInfo.InvariantCulture;

        GestorEstudiantes gestorEstudiantes = new GestorEstudiantes();
        GestorProfesores gestorProfesores = new GestorProfesores();
        GestorCursos gestorCursos = new GestorCursos();
        GestorCalificaciones gestorCalificaciones = new GestorCalificaciones();
        GeneradorFacturas generadorFacturas = new GeneradorFacturas();

        Administrativo cajero = new Administrativo(1, "Laura", "Santos", 34, "Cajera");

        gestorCursos.CrearAula("A-101", 30);
        gestorCursos.CrearAula("A-102", 25);
        gestorCursos.CrearAsignatura("MAT-5", "Matemática");
        gestorCursos.CrearAsignatura("LEN-5", "Lengua Española");
        gestorCursos.CrearAsignatura("CIE-5", "Ciencias Naturales");
        gestorCursos.CrearCurso("5to Grado A");
        gestorCursos.CrearCurso("5to Grado B");

        Demostracion demostracion = new Demostracion(gestorEstudiantes, gestorProfesores, gestorCursos,
            gestorCalificaciones, generadorFacturas, cajero);

        MenuPrincipal menu = new MenuPrincipal(gestorEstudiantes, gestorProfesores, gestorCursos,
            gestorCalificaciones, generadorFacturas, cajero, demostracion);

        menu.Iniciar();
    }
}
