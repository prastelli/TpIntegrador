namespace ProyectoIntegrador
{
    // Clase CuartelGeneral
    class CuartelGeneral
    {
        private List<Operador> operadores = new List<Operador>();
        private List<Operador> reserva = new List<Operador>();
        private Localizacion localizacion;

        public CuartelGeneral ()
        {
            localizacion = new Localizacion();
            localizacion.InicializarLocalizacion(0, 0);
        }

        public void AgregarOperador(Operador operador)
        {
            operadores.Add(operador);
        }

        public void RemoverOperador(Operador operador)
        {
            operadores.Remove(operador);
            Console.WriteLine($"{operador.DevolverId()} fue removido de la lista de operadores.");
        }

        public void AgregarReserva(Operador operador)
        {
            reserva.Add(operador);
            Console.WriteLine($"{operador.DevolverId()} fue agregado a la lista de reserva.");
        }

        public void RemoverReserva(Operador operador)
        {
            reserva.Remove(operador);
            Console.WriteLine($"{operador.DevolverId()} fue removido de la lista de reserva.");
        }

        public void ListarOperadores()
        {
            Console.WriteLine("Estado de todos los operadores:");
            foreach (var operador in operadores)
            {
                Console.WriteLine(operador.DevolverDatos());
            }
        }

        public void DevolverIdOperadores() 
        {
            foreach (var operador in operadores)
            {
                Console.WriteLine($"ID Operador: {operador.DevolverId()}");
            }
        }
        public void ListarOperadoresEnLocalizacion(Localizacion localizacion)
        {
            Console.WriteLine($"Operadores en la localización {localizacion.ObtenerCoordenadas()}:");
            foreach (var operador in operadores)
            {
                if (operador.MismaLocalizacion(localizacion))
                {
                    Console.WriteLine(operador.DevolverDatos());
                }
            }
        }

        public void TotalRecall()
        {
            foreach (var operador in operadores)
            {
                operador.VolverAlCuartel(localizacion);
            }
            Console.WriteLine("Se realizó un Total Recall.");
        }

        public Operador SeleccionarOperador(string id)
        {
            return operadores.Find(o => o.DevolverId() == id);
        }
        public Localizacion DevolverLocalizacion() 
        {
            return localizacion;
        }
    }
}
