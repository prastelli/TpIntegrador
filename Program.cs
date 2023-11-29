using ProyectoIntegrador;

/*
*  TP Integrador Pablo Rastelli - En Solitario
*  Gracias por tanto, perdón por tan poco.....
*/

Console.WriteLine("¿Desea cargar una simulación previa? (S/N)");
string respuesta = Console.ReadLine().ToUpper();

if (respuesta == "S")
{
    // La clase Helpers tiene 2 metodos uno para serializar y otro para deserializar. 
    // La deserializacion falla 
}

Terreno Mundo = Terreno.GetInstance();

//InicializarMapa: (Tamaño Terreno - Cant. Sitios Reciclaje - Cant. Cuarteles - Cant. Vertederos)  
Mundo.InicializarMapa(100, 5, 3, 1000);
Mundo.MostrarMapa();

Cuartel cuartel = new(Mundo);

cuartel.CrearOperadores(10, cuartel);


/*
 *  Se tendria que usar el patron command, pero no lo entendi bien y no lo pude aplicar.
 */

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
    Console.WriteLine("7. Backup de Mapa y Operadores");
    Console.WriteLine("8. Salir");
    Console.Write("Selecciona una opción: ");
    opcion = int.Parse(Console.ReadLine());

    switch (opcion)
    {
        case 1:
            cuartel.ListarOperadores();
            break;
        case 2:
            Console.Write("Ingresa la coordinada X: ");
            string X = Console.ReadLine();
            bool bandX = int.TryParse(X, out int coordx);

            Console.Write("Ingresa la coordinada Y: ");
            string Y = Console.ReadLine();
            bool bandY = int.TryParse(Y, out int coordy);

            if (bandY && bandX)
            {
                Localizacion lugar = new Localizacion(coordx, coordy);
                cuartel.ListarOperadoresEnLocalizacion(lugar);
            }            
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
                Console.WriteLine($"Operador seleccionado: ID: {operadorSeleccionado.iD}");
                Console.WriteLine("a. Enviar a una localización en especial");
                Console.WriteLine("b. Indicar retorno al cuartel");
                Console.WriteLine("c. Cambiar estado a STANDBY");
                Console.WriteLine("d. Cambiar estado a STANDBY");
                Console.WriteLine("e. Cambiar bateria");
                Console.Write("Selecciona una opción: ");
                char opcionOperador = char.Parse(Console.ReadLine().ToLower());
                switch (opcionOperador)
                {
                    case 'a':
                        
                        Console.Write("Ingresa la coordinada X: ");
                        X = Console.ReadLine();
                        bandX = int.TryParse(X, out coordx);

                        Console.Write("Ingresa la coordinada Y: ");
                        Y = Console.ReadLine();
                        bandY = int.TryParse(Y, out coordy);

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
                        operadorSeleccionado.Mover(operadorSeleccionado.cuartel.Localizacion);
                        break;
                    case 'c':
                        operadorSeleccionado.CambiarEsatado(Estado.Standby);
                        Console.WriteLine($"{operadorSeleccionado.iD} está ahora en estado STANDBY.");
                        break;
                    case 'd':
                        operadorSeleccionado.BateriaNueva();                        
                        break;
                    case 'e':
                        operadorSeleccionado.bateria.cargarBateria();
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
            Helpers.SerializarCuartel(cuartel);            
            Helpers.SerializarMapa(Mundo);

            break;
        case 8:
            Console.WriteLine("Saliendo del programa.");
            break;
        default:
            Console.WriteLine("Opción no válida.");
            break;
    }
} while (opcion != 8);

