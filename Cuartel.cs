namespace ProyectoIntegrador
{
    // Clase CuartelGeneral
    public class Cuartel
    {
        private string idCuartel;
        private List<Operador> operadores = new List<Operador>();
        private List<Operador> reserva = new List<Operador>();
        private Localizacion localizacion;
        private Terreno Mapa;

        public string IdCuartel { get => idCuartel; set => idCuartel = value; }
        public List<Operador> Operadores { get => operadores; set => operadores = value; }
        public List<Operador> Reserva { get => reserva; set => reserva = value; }
        public Localizacion Localizacion { get => localizacion; set => localizacion = value; }
        public Terreno Mapa1 { get => Mapa; set => Mapa = value; }

        public Cuartel(Terreno terreno)
        {
            IdCuartel = Helpers.GeneraId();
            Mapa1 = terreno;
            // Le asigno la primera Localizacion de los cuarteles que se generaron aleatoriamente
            // esto se podria mejorar para que sea mas dinamico.
            Localizacion = new Localizacion(terreno.cuartels.First().X1, terreno.cuartels.First().Y1);
            
        }

        /// <summary> Este metodo muestra genera un cantida n de operadores mediante parametros</summary>      
        public void CrearOperadores(int Cantidad, Cuartel cuartel)
        {
            Random random = new Random();
            int i = 0;
            IOperadorFactory Op = new OperadorFactory();
            while ( i <= Cantidad )
            {
                int tipoOperador = random.Next(Enum.GetValues(typeof(TipoOperador)).Length);
                Operador Operador = Op.CrearOperador((TipoOperador)tipoOperador, cuartel);
                Operadores.Add(Operador);
                i++;
            }
 
        }

        public void AgregarOperador(Operador operador)
        {
            Operadores.Add(operador);
        }

        public void RemoverOperador(Operador operador)
        {
            Operadores.Remove(operador);
            Console.WriteLine($"{operador.iD} fue removido de la lista de operadores.");
        }

        public void AgregarReserva(Operador operador)
        {
            Reserva.Add(operador);
            Console.WriteLine($"{operador.iD} fue agregado a la lista de reserva.");
        }

        public void RemoverReserva(Operador operador)
        {
            Reserva.Remove(operador);
            Console.WriteLine($"{operador.iD} fue removido de la lista de reserva.");
        }

        public void ListarOperadores()
        {
            Console.WriteLine("Estado de todos los operadores:");
            operadores.ForEach(o => Console.WriteLine(o.DevolverDatosOperador()));
        }

        public void DevolverIdOperadores() 
        {
            operadores.ForEach(o => Console.WriteLine($"ID Operador: {o.iD}"));
        }
        public void ListarOperadoresEnLocalizacion(Localizacion localizacion)
        {
            Console.WriteLine($"Operadores en la localización {localizacion.ObtenerCoordenadas()}:");
            operadores.ForEach(o => { if (o.MismaLocalizacion(localizacion)) { Console.WriteLine(o.DevolverDatosOperador()); } });
        }

        public void TotalRecall()
        {
            operadores.ForEach(o => o.Mover(localizacion));
            Console.WriteLine("Se realizó un Total Recall.");
        }

        public Operador SeleccionarOperador(string id)
        {
            return Operadores.Find(o => o.iD == id);
        }

        public void RegresoAlCuartelOperadoresDañados()
        {
            List<Operador> Ope = Operadores.Where(o => o.Daño.ContainsValue(true)).ToList();
            Ope.ForEach(o => o.Mover(localizacion));
        }
        public void OperadoresACargarAVertederos(List<Localizacion> Vertederos)
        {
            // Busco el vertedero mas cercano de la lista de Vertederos del Mapa 
            // y muevo el operador a esa ubicacion
            operadores.ForEach(o => { o.Mover(DistanciaMinimaAVertedero(o, Vertederos));
                                      o.RecogerCargaMaxima();
            });
        }

        private Localizacion DistanciaMinimaAVertedero(Operador operador, List<Localizacion> Vertederos)
        {
            Localizacion lugar = new Localizacion(0,0);
            bool primeraVez = true;
            int distanciaMinima = 0;

            foreach (Localizacion item in Vertederos)
            {
                int distancia = operador.localizacion.CalcularDistanciaAOtroDestino(item);
                if (distancia < distanciaMinima || primeraVez)
                {
                    primeraVez = false;
                    lugar = item;
                }
            }

            return lugar;
        }
    }
}
