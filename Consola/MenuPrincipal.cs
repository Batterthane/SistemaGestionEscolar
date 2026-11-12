using System;
using System.Collections.Generic;
using System.Globalization;

namespace SistemaGestionEscolar;

// MenuPrincipal: la ÚNICA parte (junto con Demostracion) que habla con el usuario.
// Lee datos del teclado, los valida y llama a los gestores. No tiene reglas del colegio.
public class MenuPrincipal
{
    private GestorEstudiantes gestorEstudiantes;
    private GestorProfesores gestorProfesores;
    private GestorCursos gestorCursos;
    private GestorCalificaciones gestorCalificaciones;
    private GeneradorFacturas generadorFacturas;
    private Administrativo cajero;
    private Demostracion demostracion;

    public MenuPrincipal(GestorEstudiantes gestorEstudiantes, GestorProfesores gestorProfesores,
        GestorCursos gestorCursos, GestorCalificaciones gestorCalificaciones,
        GeneradorFacturas generadorFacturas, Administrativo cajero, Demostracion demostracion)
    {
        this.gestorEstudiantes = gestorEstudiantes;
        this.gestorProfesores = gestorProfesores;
        this.gestorCursos = gestorCursos;
        this.gestorCalificaciones = gestorCalificaciones;
        this.generadorFacturas = generadorFacturas;
        this.cajero = cajero;
        this.demostracion = demostracion;
    }

    // ------------------------------------------------------------------
    // Ciclo principal del menú
    // ------------------------------------------------------------------
    public void Iniciar()
    {
        bool salir = false;

        while (!salir)
        {
            MostrarMenu();
            int opcion = LeerEntero("Elija una opción", 0, 10);
            Console.WriteLine();

            switch (opcion)
            {
                case 1: RegistrarEstudiante(); break;
                case 2: RegistrarProfesor(); break;
                case 3: RegistrarTutor(); break;
                case 4: AsignarProfesor(); break;
                case 5: ConfigurarCurso(); break;
                case 6: InscribirEstudiante(); break;
                case 7: RegistrarCalificacion(); break;
                case 8: ProcesarPago(); break;
                case 9: VerInformacion(); break;
                case 10: demostracion.Ejecutar(); break;
                case 0: salir = true; break;
            }

            if (!salir)
            {
                Console.WriteLine();
                Console.Write("Presione Enter para volver al menú...");
                LeerLinea();
            }
        }

        Console.WriteLine("Hasta luego. Gracias por usar el sistema.");
    }

    private void MostrarMenu()
    {
        Console.WriteLine();
        Console.WriteLine("=========== SISTEMA DE GESTIÓN ESCOLAR ===========");
        Console.WriteLine(" 1. Registrar estudiante");
        Console.WriteLine(" 2. Registrar profesor");
        Console.WriteLine(" 3. Registrar tutor y vincularlo a un estudiante");
        Console.WriteLine(" 4. Asignar profesor a una asignatura");
        Console.WriteLine(" 5. Configurar curso (aula, asignaturas, horarios)");
        Console.WriteLine(" 6. Inscribir estudiante en un curso");
        Console.WriteLine(" 7. Registrar calificación");
        Console.WriteLine(" 8. Procesar pago y generar factura");
        Console.WriteLine(" 9. Ver información (personas, cursos, boletín, facturas)");
        Console.WriteLine("10. Ejecutar demostración automática");
        Console.WriteLine(" 0. Salir");
        Console.WriteLine("==================================================");
    }

    // ------------------------------------------------------------------
    // Operaciones del menú
    // ------------------------------------------------------------------
    private void RegistrarEstudiante()
    {
        Console.WriteLine("--- Registrar estudiante ---");
        string nombre = LeerTexto("Nombre");
        string apellido = LeerTexto("Apellido");
        int edad = LeerEntero("Edad", 3, 25);

        Estudiante estudiante = gestorEstudiantes.RegistrarEstudiante(nombre, apellido, edad);
        Console.WriteLine("Estudiante registrado: " + estudiante.ObtenerResumen());
    }

    private void RegistrarProfesor()
    {
        Console.WriteLine("--- Registrar profesor ---");
        string nombre = LeerTexto("Nombre");
        string apellido = LeerTexto("Apellido");
        int edad = LeerEntero("Edad", 21, 80);
        string especialidad = LeerTexto("Especialidad");

        Profesor profesor = gestorProfesores.RegistrarProfesor(nombre, apellido, edad, especialidad);
        Console.WriteLine("Profesor registrado: " + profesor.ObtenerResumen());
    }

    private void RegistrarTutor()
    {
        Console.WriteLine("--- Registrar tutor y vincularlo a un estudiante ---");
        Estudiante estudiante = ElegirEstudiante();
        if (estudiante == null)
        {
            return;
        }

        PadreTutor tutor = null;

        if (gestorEstudiantes.Tutores.Count > 0)
        {
            Console.WriteLine("1. Crear un tutor nuevo");
            Console.WriteLine("2. Usar un tutor que ya está registrado");
            int eleccion = LeerEntero("Elija", 1, 2);
            if (eleccion == 2)
            {
                tutor = ElegirTutor(gestorEstudiantes.Tutores);
            }
        }

        if (tutor == null)
        {
            string nombre = LeerTexto("Nombre del tutor");
            string apellido = LeerTexto("Apellido del tutor");
            int edad = LeerEntero("Edad", 18, 99);
            string telefono = LeerTexto("Teléfono");

            Console.WriteLine("Parentesco: 1. Padre   2. Madre   3. Tutor legal");
            int opcionParentesco = LeerEntero("Elija", 1, 3);
            string parentesco = "Tutor legal";
            if (opcionParentesco == 1)
            {
                parentesco = "Padre";
            }
            else if (opcionParentesco == 2)
            {
                parentesco = "Madre";
            }

            tutor = gestorEstudiantes.RegistrarTutor(nombre, apellido, edad, telefono, parentesco);
            Console.WriteLine("Tutor registrado: " + tutor.ObtenerResumen());
        }

        Resultado resultado = gestorEstudiantes.VincularTutor(estudiante, tutor);
        Console.WriteLine(resultado.ObtenerTexto());
    }

    private void AsignarProfesor()
    {
        Console.WriteLine("--- Asignar profesor a una asignatura ---");
        Profesor profesor = ElegirProfesor();
        if (profesor == null)
        {
            return;
        }

        Asignatura asignatura = ElegirAsignatura(gestorCursos.Asignaturas);
        if (asignatura == null)
        {
            return;
        }

        Resultado resultado = gestorProfesores.AsignarAsignatura(profesor, asignatura);
        Console.WriteLine(resultado.ObtenerTexto());
    }

    private void ConfigurarCurso()
    {
        Console.WriteLine("--- Configurar curso ---");
        Curso curso = ElegirCurso();
        if (curso == null)
        {
            return;
        }

        Console.WriteLine("1. Asignar aula");
        Console.WriteLine("2. Agregar una asignatura al curso");
        Console.WriteLine("3. Agregar un horario");
        int opcion = LeerEntero("Elija", 1, 3);

        if (opcion == 1)
        {
            Aula aula = ElegirAula();
            if (aula != null)
            {
                Console.WriteLine(gestorCursos.AsignarAula(curso, aula).ObtenerTexto());
            }
        }
        else if (opcion == 2)
        {
            Asignatura asignatura = ElegirAsignatura(gestorCursos.Asignaturas);
            if (asignatura != null)
            {
                Console.WriteLine(gestorCursos.AgregarAsignaturaACurso(curso, asignatura).ObtenerTexto());
            }
        }
        else
        {
            Asignatura asignatura = ElegirAsignatura(curso.Asignaturas);
            if (asignatura == null)
            {
                return;
            }

            string[] dias = { "Lunes", "Martes", "Miércoles", "Jueves", "Viernes" };
            Console.WriteLine("Día: 1. Lunes   2. Martes   3. Miércoles   4. Jueves   5. Viernes");
            int dia = LeerEntero("Elija el día", 1, 5);
            int horaInicio = LeerEntero("Hora de inicio (formato 24 h)", 6, 20);
            int horaFin = LeerEntero("Hora de fin (formato 24 h)", 6, 21);

            Horario horario = new Horario(dias[dia - 1], horaInicio, horaFin, asignatura);
            Console.WriteLine(gestorCursos.AgregarHorario(curso, horario).ObtenerTexto());
        }
    }

    private void InscribirEstudiante()
    {
        Console.WriteLine("--- Inscribir estudiante en un curso ---");
        Estudiante estudiante = ElegirEstudiante();
        if (estudiante == null)
        {
            return;
        }

        Curso curso = ElegirCurso();
        if (curso == null)
        {
            return;
        }

        Resultado resultado = gestorCursos.InscribirEstudiante(estudiante, curso);
        Console.WriteLine(resultado.ObtenerTexto());
    }

    private void RegistrarCalificacion()
    {
        Console.WriteLine("--- Registrar calificación ---");
        Estudiante estudiante = ElegirEstudiante();
        if (estudiante == null)
        {
            return;
        }

        if (estudiante.CursoActual == null)
        {
            Console.WriteLine("[ERROR] " + estudiante.NombreCompleto + " no está inscrito(a) en ningún curso. Use la opción 6.");
            return;
        }

        Asignatura asignatura = ElegirAsignatura(estudiante.CursoActual.Asignaturas);
        if (asignatura == null)
        {
            return;
        }

        double valor = LeerNumero("Nota (de 0 a 100)");
        string descripcion = LeerTexto("Descripción (ej. Examen parcial)");

        Resultado resultado = gestorCalificaciones.RegistrarCalificacion(estudiante, asignatura, valor, descripcion);
        Console.WriteLine(resultado.ObtenerTexto());
    }

    private void ProcesarPago()
    {
        Console.WriteLine("--- Procesar pago y generar factura ---");
        Estudiante estudiante = ElegirEstudiante();
        if (estudiante == null)
        {
            return;
        }

        if (estudiante.Tutores.Count == 0)
        {
            Console.WriteLine("[ERROR] " + estudiante.NombreCompleto + " no tiene tutores vinculados. Use la opción 3.");
            return;
        }

        Console.WriteLine("¿Quién paga?");
        PadreTutor tutor = ElegirTutor(estudiante.Tutores);
        if (tutor == null)
        {
            return;
        }

        string concepto = LeerTexto("Concepto (ej. Mensualidad)");
        decimal monto = LeerDecimal("Monto a pagar");

        Console.WriteLine("Método de pago: 1. Efectivo   2. Tarjeta   3. Transferencia");
        int opcionMetodo = LeerEntero("Elija", 1, 3);

        // Aquí se elige la clase concreta. Después, ProcesadorPagos la usa sin saber cuál es.
        IMetodoPago metodo;
        if (opcionMetodo == 1)
        {
            decimal entregado = LeerDecimal("Efectivo entregado");
            metodo = new PagoEfectivo(entregado);
        }
        else if (opcionMetodo == 2)
        {
            string numero = LeerTexto("Número de tarjeta (16 dígitos)");
            string titular = LeerTexto("Titular de la tarjeta");
            metodo = new PagoTarjeta(numero, titular);
        }
        else
        {
            string banco = LeerTexto("Banco de origen");
            string referencia = LeerTexto("Número de referencia");
            metodo = new PagoTransferencia(banco, referencia);
        }

        ProcesadorPagos procesador = new ProcesadorPagos(metodo);
        Pago pago = procesador.ProcesarPago(tutor, estudiante, monto, concepto, cajero);
        Console.WriteLine();
        Console.WriteLine(pago.ObtenerResumen());

        Factura factura = generadorFacturas.Generar(pago);
        if (factura != null)
        {
            Console.WriteLine();
            Console.WriteLine(factura.ObtenerTexto());
        }
        else
        {
            Console.WriteLine("No se genera factura porque el pago fue rechazado.");
        }
    }

    private void VerInformacion()
    {
        Console.WriteLine("--- Ver información ---");
        Console.WriteLine("1. Todas las personas");
        Console.WriteLine("2. Cursos");
        Console.WriteLine("3. Boletín de un estudiante");
        Console.WriteLine("4. Facturas emitidas");
        int opcion = LeerEntero("Elija", 1, 4);
        Console.WriteLine();

        if (opcion == 1)
        {
            MostrarPersonas();
        }
        else if (opcion == 2)
        {
            MostrarCursos();
        }
        else if (opcion == 3)
        {
            Estudiante estudiante = ElegirEstudiante();
            if (estudiante != null)
            {
                Console.WriteLine(gestorCalificaciones.GenerarBoletin(estudiante));
            }
        }
        else
        {
            MostrarFacturas();
        }
    }

    private void MostrarPersonas()
    {
        // Una sola lista de Persona con objetos de distintas clases hijas. [SOLID - LSP]
        List<Persona> personas = new List<Persona>();
        personas.AddRange(gestorEstudiantes.Estudiantes);
        personas.AddRange(gestorEstudiantes.Tutores);
        personas.AddRange(gestorProfesores.Profesores);
        personas.Add(cajero);

        Console.WriteLine("Personas registradas en el colegio (" + personas.Count + "):");
        foreach (Persona persona in personas)
        {
            Console.WriteLine("  " + persona.ObtenerResumen());
        }
    }

    private void MostrarCursos()
    {
        foreach (Curso curso in gestorCursos.Cursos)
        {
            Console.WriteLine(curso.ObtenerResumen());

            foreach (Estudiante alumno in curso.Estudiantes)
            {
                Console.WriteLine("    Estudiante: " + alumno.NombreCompleto);
            }
            foreach (Asignatura materia in curso.Asignaturas)
            {
                Console.WriteLine("    Asignatura: " + materia.ObtenerResumen());
            }
            foreach (Horario bloque in curso.Horarios)
            {
                Console.WriteLine("    Horario:    " + bloque.ObtenerResumen());
            }
        }
    }

    private void MostrarFacturas()
    {
        if (generadorFacturas.Facturas.Count == 0)
        {
            Console.WriteLine("Todavía no se ha emitido ninguna factura.");
            return;
        }

        foreach (Factura factura in generadorFacturas.Facturas)
        {
            Console.WriteLine(factura.ObtenerTexto());
            Console.WriteLine();
        }
    }

    // ------------------------------------------------------------------
    // Elegir un elemento de una lista numerada (devuelve null si no hay elementos)
    // ------------------------------------------------------------------
    private Estudiante ElegirEstudiante()
    {
        List<Estudiante> lista = gestorEstudiantes.Estudiantes;
        if (lista.Count == 0)
        {
            Console.WriteLine("[ERROR] No hay estudiantes registrados. Use la opción 1.");
            return null;
        }

        Console.WriteLine("Estudiantes:");
        for (int i = 0; i < lista.Count; i++)
        {
            Console.WriteLine("  " + (i + 1) + ". " + lista[i].NombreCompleto + " (" + lista[i].Matricula + ")");
        }
        int opcion = LeerEntero("Elija un estudiante", 1, lista.Count);
        return lista[opcion - 1];
    }

    private Profesor ElegirProfesor()
    {
        List<Profesor> lista = gestorProfesores.Profesores;
        if (lista.Count == 0)
        {
            Console.WriteLine("[ERROR] No hay profesores registrados. Use la opción 2.");
            return null;
        }

        Console.WriteLine("Profesores:");
        for (int i = 0; i < lista.Count; i++)
        {
            Console.WriteLine("  " + (i + 1) + ". " + lista[i].NombreCompleto + " (" + lista[i].Especialidad + ")");
        }
        int opcion = LeerEntero("Elija un profesor", 1, lista.Count);
        return lista[opcion - 1];
    }

    private PadreTutor ElegirTutor(List<PadreTutor> lista)
    {
        if (lista.Count == 0)
        {
            Console.WriteLine("[ERROR] No hay tutores disponibles.");
            return null;
        }

        Console.WriteLine("Tutores:");
        for (int i = 0; i < lista.Count; i++)
        {
            Console.WriteLine("  " + (i + 1) + ". " + lista[i].NombreCompleto + " (" + lista[i].Parentesco + ")");
        }
        int opcion = LeerEntero("Elija un tutor", 1, lista.Count);
        return lista[opcion - 1];
    }

    private Curso ElegirCurso()
    {
        List<Curso> lista = gestorCursos.Cursos;
        if (lista.Count == 0)
        {
            Console.WriteLine("[ERROR] No hay cursos creados.");
            return null;
        }

        Console.WriteLine("Cursos:");
        for (int i = 0; i < lista.Count; i++)
        {
            Console.WriteLine("  " + (i + 1) + ". " + lista[i].ObtenerResumen());
        }
        int opcion = LeerEntero("Elija un curso", 1, lista.Count);
        return lista[opcion - 1];
    }

    private Asignatura ElegirAsignatura(List<Asignatura> lista)
    {
        if (lista.Count == 0)
        {
            Console.WriteLine("[ERROR] No hay asignaturas disponibles. Primero agréguelas al curso (opción 5).");
            return null;
        }

        Console.WriteLine("Asignaturas:");
        for (int i = 0; i < lista.Count; i++)
        {
            Console.WriteLine("  " + (i + 1) + ". " + lista[i].ObtenerResumen());
        }
        int opcion = LeerEntero("Elija una asignatura", 1, lista.Count);
        return lista[opcion - 1];
    }

    private Aula ElegirAula()
    {
        List<Aula> lista = gestorCursos.Aulas;
        if (lista.Count == 0)
        {
            Console.WriteLine("[ERROR] No hay aulas creadas.");
            return null;
        }

        Console.WriteLine("Aulas:");
        for (int i = 0; i < lista.Count; i++)
        {
            Console.WriteLine("  " + (i + 1) + ". " + lista[i].ObtenerResumen());
        }
        int opcion = LeerEntero("Elija un aula", 1, lista.Count);
        return lista[opcion - 1];
    }

    // ------------------------------------------------------------------
    // Lectura segura del teclado: repiten la pregunta hasta recibir un dato válido
    // ------------------------------------------------------------------
    private string LeerLinea()
    {
        string linea = Console.ReadLine();
        if (linea == null)
        {
            // La entrada se cerró (por ejemplo, Ctrl+Z / Ctrl+D): se termina el programa.
            Console.WriteLine();
            Console.WriteLine("Entrada finalizada. Cerrando el programa.");
            Environment.Exit(0);
        }
        return linea.Trim();
    }

    private string LeerTexto(string mensaje)
    {
        while (true)
        {
            Console.Write(mensaje + ": ");
            string texto = LeerLinea();
            if (texto != "")
            {
                return texto;
            }
            Console.WriteLine("  Error: este dato no puede estar vacío.");
        }
    }

    private int LeerEntero(string mensaje, int minimo, int maximo)
    {
        while (true)
        {
            Console.Write(mensaje + " (" + minimo + "-" + maximo + "): ");
            string texto = LeerLinea();

            int numero;
            if (int.TryParse(texto, out numero) && numero >= minimo && numero <= maximo)
            {
                return numero;
            }
            Console.WriteLine("  Error: escriba un número entero entre " + minimo + " y " + maximo + ".");
        }
    }

    private double LeerNumero(string mensaje)
    {
        while (true)
        {
            Console.Write(mensaje + ": ");
            string texto = LeerLinea();

            double numero;
            if (double.TryParse(texto, NumberStyles.AllowLeadingSign | NumberStyles.AllowDecimalPoint,
                CultureInfo.InvariantCulture, out numero))
            {
                return numero;
            }
            Console.WriteLine("  Error: escriba un número (use punto para los decimales, ej. 85.5).");
        }
    }

    private decimal LeerDecimal(string mensaje)
    {
        while (true)
        {
            Console.Write(mensaje + ": ");
            string texto = LeerLinea();

            decimal numero;
            if (decimal.TryParse(texto, NumberStyles.AllowLeadingSign | NumberStyles.AllowDecimalPoint,
                CultureInfo.InvariantCulture, out numero))
            {
                return numero;
            }
            Console.WriteLine("  Error: escriba un número sin separador de miles (ej. 1500 o 1500.50).");
        }
    }
}
