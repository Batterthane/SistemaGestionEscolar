using System;
using System.Collections.Generic;

namespace SistemaGestionEscolar;

// Demostracion: recorre un escenario completo y muestra, paso a paso, cómo
// colaboran las clases. Cada vez que aparece un principio SOLID imprime un "PUNTO SOLID".
// Esta clase solo ORQUESTA: usa los gestores, no tiene reglas propias.
public class Demostracion
{
    private GestorEstudiantes gestorEstudiantes;
    private GestorProfesores gestorProfesores;
    private GestorCursos gestorCursos;
    private GestorCalificaciones gestorCalificaciones;
    private GeneradorFacturas generadorFacturas;
    private Administrativo cajero;

    public Demostracion(GestorEstudiantes gestorEstudiantes, GestorProfesores gestorProfesores,
        GestorCursos gestorCursos, GestorCalificaciones gestorCalificaciones,
        GeneradorFacturas generadorFacturas, Administrativo cajero)
    {
        this.gestorEstudiantes = gestorEstudiantes;
        this.gestorProfesores = gestorProfesores;
        this.gestorCursos = gestorCursos;
        this.gestorCalificaciones = gestorCalificaciones;
        this.generadorFacturas = generadorFacturas;
        this.cajero = cajero;
    }

    public void Ejecutar()
    {
        Console.WriteLine("########  DEMOSTRACIÓN AUTOMÁTICA DEL SISTEMA  ########");

        // ---------------------------------------------------------------
        Titulo("PASO 1: Registrar profesores y estudiantes");
        Profesor marta = gestorProfesores.RegistrarProfesor("Marta", "Gómez", 38, "Matemática");
        Profesor jorge = gestorProfesores.RegistrarProfesor("Jorge", "Ramírez", 45, "Lengua Española");
        Estudiante ana = gestorEstudiantes.RegistrarEstudiante("Ana", "Martínez", 10);
        Estudiante luis = gestorEstudiantes.RegistrarEstudiante("Luis", "Pérez", 11);
        Estudiante sofia = gestorEstudiantes.RegistrarEstudiante("Sofía", "Díaz", 10);
        Console.WriteLine("  " + marta.ObtenerResumen());
        Console.WriteLine("  " + jorge.ObtenerResumen());
        Console.WriteLine("  " + ana.ObtenerResumen());
        Console.WriteLine("  " + luis.ObtenerResumen());
        Console.WriteLine("  " + sofia.ObtenerResumen());
        PuntoSolid("SRP", "GestorProfesores registró a los profesores y GestorEstudiantes a los estudiantes. Cada gestor tiene UNA responsabilidad.");

        // ---------------------------------------------------------------
        Titulo("PASO 2: Registrar tutores y relacionarlos con estudiantes");
        PadreTutor carlos = gestorEstudiantes.RegistrarTutor("Carlos", "Martínez", 41, "555-0101", "Padre");
        PadreTutor rosa = gestorEstudiantes.RegistrarTutor("Rosa", "Pérez", 39, "555-0102", "Madre");
        Mostrar(gestorEstudiantes.VincularTutor(ana, carlos));
        Mostrar(gestorEstudiantes.VincularTutor(luis, rosa));
        Console.WriteLine("  Intento repetir un vínculo (debe fallar):");
        Mostrar(gestorEstudiantes.VincularTutor(ana, carlos));

        // ---------------------------------------------------------------
        Titulo("PASO 3: Crear la estructura académica y asignar profesores");
        Aula aula = gestorCursos.CrearAula("A-201", 2);
        Asignatura mat = gestorCursos.CrearAsignatura("MAT-6", "Matemática");
        Asignatura len = gestorCursos.CrearAsignatura("LEN-6", "Lengua Española");
        Curso curso = gestorCursos.CrearCurso("6to Grado A");
        Console.WriteLine("  Creados: " + aula.ObtenerResumen() + " | " + mat.ObtenerResumen() + " | " + len.ObtenerResumen());
        Mostrar(gestorCursos.AgregarAsignaturaACurso(curso, mat));
        Mostrar(gestorCursos.AgregarAsignaturaACurso(curso, len));

        // Variable de tipo interfaz: desde "docente" solo se ve lo que IDocente permite.
        IDocente docente = marta;
        Mostrar(gestorProfesores.AsignarAsignatura(docente, mat));
        Mostrar(gestorProfesores.AsignarAsignatura(jorge, len));
        Console.WriteLine("  Intento asignar otro profesor a una asignatura que ya tiene (debe fallar):");
        Mostrar(gestorProfesores.AsignarAsignatura(jorge, mat));
        PuntoSolid("ISP", "IDocente solo exige AsignarAsignatura. Un Profesor no tiene que implementar AgregarCalificacion, ni un Estudiante AsignarAsignatura.");

        // ---------------------------------------------------------------
        Titulo("PASO 4: Asignar aula e inscribir estudiantes");
        Console.WriteLine("  Intento inscribir a Ana ANTES de asignar el aula (debe fallar):");
        Mostrar(gestorCursos.InscribirEstudiante(ana, curso));
        Mostrar(gestorCursos.AsignarAula(curso, aula));
        Mostrar(gestorCursos.InscribirEstudiante(ana, curso));
        Mostrar(gestorCursos.InscribirEstudiante(luis, curso));
        Console.WriteLine("  Intento inscribir a Sofía con el aula llena (debe fallar):");
        Mostrar(gestorCursos.InscribirEstudiante(sofia, curso));

        // ---------------------------------------------------------------
        Titulo("PASO 5: Asignar horarios");
        Mostrar(gestorCursos.AgregarHorario(curso, new Horario("Lunes", 8, 9, mat)));
        Mostrar(gestorCursos.AgregarHorario(curso, new Horario("Lunes", 9, 10, len)));
        Console.WriteLine("  Intento un horario que choca con los anteriores (debe fallar):");
        Mostrar(gestorCursos.AgregarHorario(curso, new Horario("Lunes", 8, 10, len)));
        Console.WriteLine("  " + curso.ObtenerResumen());

        // ---------------------------------------------------------------
        Titulo("PASO 6: Registrar calificaciones");
        // Variable de tipo interfaz: GestorCalificaciones solo conoce ICalificable.
        ICalificable alumnoAna = ana;
        Mostrar(gestorCalificaciones.RegistrarCalificacion(alumnoAna, mat, 92, "Examen parcial"));
        Mostrar(gestorCalificaciones.RegistrarCalificacion(ana, len, 88, "Exposición oral"));
        Mostrar(gestorCalificaciones.RegistrarCalificacion(luis, mat, 75, "Examen parcial"));
        Console.WriteLine("  Intento una nota inválida (debe fallar):");
        Mostrar(gestorCalificaciones.RegistrarCalificacion(luis, mat, 120, "Examen final"));
        Console.WriteLine(gestorCalificaciones.GenerarBoletin(ana));
        PuntoSolid("DIP", "GestorCalificaciones recibe un ICalificable (abstracción), no la clase concreta Estudiante.");

        // ---------------------------------------------------------------
        Titulo("PASO 7: Procesar pagos y generar facturas");
        Console.WriteLine("  7.1 Carlos paga con TARJETA:");
        Cobrar(new PagoTarjeta("1234 5678 1234 5678", "Carlos Martínez"), carlos, ana, 1500m, "Mensualidad");
        Console.WriteLine("  7.2 Rosa paga en EFECTIVO pero entrega menos de lo necesario (debe fallar):");
        Cobrar(new PagoEfectivo(1000m), rosa, luis, 1500m, "Mensualidad");
        Console.WriteLine("  7.3 Rosa paga en EFECTIVO entregando lo suficiente:");
        Cobrar(new PagoEfectivo(2000m), rosa, luis, 1500m, "Mensualidad");
        Console.WriteLine("  7.4 Carlos paga con TRANSFERENCIA:");
        Cobrar(new PagoTransferencia("Banco Central", "TRX-784512"), carlos, ana, 800m, "Uniforme escolar");
        PuntoSolid("SRP", "ProcesadorPagos solo cobra. GeneradorFacturas solo factura. Son dos clases distintas.");
        PuntoSolid("DIP", "ProcesadorPagos recibió un IMetodoPago por su constructor. Fue la misma clase para tarjeta, efectivo y transferencia.");
        PuntoSolid("OCP", "Para agregar PagoCheque solo se crea una clase nueva que implemente IMetodoPago. ProcesadorPagos NO se modifica.");

        // ---------------------------------------------------------------
        Titulo("PASO 8: Polimorfismo con una lista de Persona");
        List<Persona> personas = new List<Persona>();
        personas.Add(marta);
        personas.Add(ana);
        personas.Add(carlos);
        personas.Add(cajero);
        foreach (Persona persona in personas)
        {
            Console.WriteLine("  " + persona.ObtenerResumen());
        }
        PuntoSolid("LSP", "Profesor, Estudiante, PadreTutor y Administrativo se usan como Persona, y cada uno responde bien a ObtenerResumen().");

        // ---------------------------------------------------------------
        Titulo("RESUMEN SOLID");
        Console.WriteLine("  SRP: un gestor por responsabilidad (GestorEstudiantes, GestorCursos, ProcesadorPagos...).");
        Console.WriteLine("  OCP: nuevos métodos de pago sin tocar ProcesadorPagos (IMetodoPago).");
        Console.WriteLine("  LSP: cualquier Persona o IMetodoPago se puede sustituir por otro sin romper nada.");
        Console.WriteLine("  ISP: interfaces pequeñas (ICalificable, IDocente, IResponsablePago, IMetodoPago).");
        Console.WriteLine("  DIP: ProcesadorPagos, GestorCalificaciones y GestorProfesores dependen de interfaces.");
        Console.WriteLine();
        Console.WriteLine("  Fin de la demostración. Use la opción 9 del menú para consultar los datos creados.");
    }

    // Cobra con el método recibido y, si fue aprobado, genera la factura.
    private void Cobrar(IMetodoPago metodo, PadreTutor tutor, Estudiante estudiante, decimal monto, string concepto)
    {
        ProcesadorPagos procesador = new ProcesadorPagos(metodo);
        Pago pago = procesador.ProcesarPago(tutor, estudiante, monto, concepto, cajero);
        Console.WriteLine("  " + pago.ObtenerResumen());

        Factura factura = generadorFacturas.Generar(pago);
        if (factura != null)
        {
            Console.WriteLine(factura.ObtenerTexto());
        }
        else
        {
            Console.WriteLine("  -> No se genera factura porque el pago fue rechazado.");
        }
    }

    private void Titulo(string texto)
    {
        Console.WriteLine();
        Console.WriteLine("=== " + texto + " ===");
    }

    private void Mostrar(Resultado resultado)
    {
        Console.WriteLine("  " + resultado.ObtenerTexto());
    }

    private void PuntoSolid(string principio, string explicacion)
    {
        Console.WriteLine("  >>> PUNTO SOLID [" + principio + "]: " + explicacion);
    }
}
