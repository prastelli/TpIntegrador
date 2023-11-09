using ProyectoIntegrador;

CuartelGeneral cuartel = new CuartelGeneral();

// Crear operadores
UAV drone1 = new UAV();
K9 k91 = new K9();
M8 m81 = new M8();

IOperadorFactory Op = new OperadorFactory();

// Prueba de implementacion de Patron Factory
Operador Operador1 = Op.CrearOperador("UAV");
Operador Operador2 = Op.CrearOperador("K9");
Operador Operador3= Op.CrearOperador("M8");


// Agregar operadores al cuartel
cuartel.AgregarOperador(drone1);
cuartel.AgregarOperador(k91);
cuartel.AgregarOperador(m81);
cuartel.AgregarOperador(Operador1);
cuartel.AgregarOperador(Operador2);
cuartel.AgregarOperador(Operador3);


// Menú de opciones
int opcion;
do
{
    Console.WriteLine("\nMenú de opciones:");
    Console.WriteLine("1. Listar operadores");
    Console.WriteLine("2. Listar operadores en una localización");
    Console.WriteLine("3. Realizar un Total Recall");
    Console.WriteLine("4. Seleccionar un operador en específico");
    Console.WriteLine("5. Agregar operador a la reserva");
    Console.WriteLine("6. Remover operador de la reserva");
    Console.WriteLine("7. Salir");
    Console.Write("Selecciona una opción: ");
    opcion = int.Parse(Console.ReadLine());

    switch (opcion)
    {
        case 1:
            cuartel.ListarOperadores();
            break;
        case 2:
            Console.Write("Ingresa una localización: ");
            string localizacion = Console.ReadLine();
            //cuartel.ListarOperadoresEnLocalizacion(localizacion);
            break;
        case 3:
            cuartel.TotalRecall();
            break;
        case 4:
            cuartel.DevolverIdOperadores();
            Console.Write("\n");
            Console.Write("Ingresa el ID del operador: ");
            string idOperador = Console.ReadLine();
            Operador operadorSeleccionado = cuartel.SeleccionarOperador(idOperador);
            if (operadorSeleccionado != null)
            {
                Console.WriteLine($"Operador seleccionado: ID: {operadorSeleccionado.DevolverId()}");
                Console.WriteLine("a. Enviar a una localización en especial");
                Console.WriteLine("b. Indicar retorno al cuartel");
                Console.WriteLine("c. Cambiar estado a STANDBY");
                Console.Write("Selecciona una opción: ");
                char opcionOperador = char.Parse(Console.ReadLine().ToLower());
                switch (opcionOperador)
                {
                    case 'a':
                        
                        Console.Write("Ingresa la coordinada X: ");
                        string CoordinadaX = Console.ReadLine();
                        int coordx;
                        bool bandX = int.TryParse(CoordinadaX, out coordx);

                        Console.Write("Ingresa la coordinada Y: ");
                        string CoordinadaY = Console.ReadLine();
                        int coordy;
                        bool bandY = int.TryParse(CoordinadaX, out coordy);

                        if (bandY && bandX)
                        {
                            Localizacion destino = new Localizacion(coordx,coordy);
                            operadorSeleccionado.Mover(destino);
                        }
                        else
                        {
                            Console.Write("Las coordinadas ingresadas son incorrectas: ");
                        }
                        break;
                    case 'b':
                        operadorSeleccionado.VolverAlCuartel(cuartel.DevolverLocalizacion());
                        break;
                    case 'c':
                        operadorSeleccionado.CambiarEsatado(Estado.Standby);
                        Console.WriteLine($"{operadorSeleccionado.DevolverId()} está ahora en estado STANDBY.");
                        break;
                    default:
                        Console.WriteLine("Opción no válida.");
                        break;
                }
            }
            else
            {
                Console.WriteLine("Operador no encontrado.");
            }
            break;
        case 5:
            Console.Write("Ingresa el ID del operador a agregar a la reserva: ");
            string idReserva = Console.ReadLine();
            Operador operadorReserva = cuartel.SeleccionarOperador(idReserva);
            if (operadorReserva != null)
            {
                cuartel.AgregarReserva(operadorReserva);
            }
            else
            {
                Console.WriteLine("Operador no encontrado.");
            }
            break;
        case 6:
            Console.Write("Ingresa el ID del operador a remover de la reserva: ");
            string idRemoverReserva = Console.ReadLine();
            Operador operadorRemoverReserva = cuartel.SeleccionarOperador(idRemoverReserva);
            if (operadorRemoverReserva != null)
            {
                cuartel.RemoverReserva(operadorRemoverReserva);
            }
            else
            {
                Console.WriteLine("Operador no encontrado.");
            }
            break;
        case 7:
            Console.WriteLine("Saliendo del programa.");
            break;
        default:
            Console.WriteLine("Opción no válida.");
            break;
    }
} while (opcion != 7);