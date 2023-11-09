using static System.Runtime.InteropServices.JavaScript.JSType;

namespace ProyectoIntegrador
{
    // Clase base Operador
    public abstract class Operador
    {
        string? ID { get; set; }
        Bateria Bateria{ get; set; }
        Estado Estado { get; set; }
        int CargaMaxima { get; set; }
        int CargaActual { get; set; }
        int VelocidadOptima { get; set; }
        int VelocidadActual { get; set; }

        Localizacion Localizacion { get; set; }

        public Operador(int bateria, Estado estado, int cargaMaxima, int velocidadOptima)
        {
            ID = GeneraNombreOperador();
            Bateria = new Bateria(bateria);
            Estado = estado;
            CargaMaxima = cargaMaxima;
            VelocidadOptima = velocidadOptima;
            VelocidadActual = 0;
            CargaActual = 0;
            Localizacion = new Localizacion(0,0); // Lo inicio en la misma posicion que el Cuartel
        }

        public void Mover(Localizacion localizacion)
        {
            int distancia = Localizacion.CalcularDistanciaAOtroDestino(localizacion);
            if (distancia > 0)
            {
                distancia = distancia * 100;    // 7 * 100 = 700KM

            }
            Console.WriteLine($"{ID} se movió {distancia} km. Batería restante: {Bateria.getBateriaActuall()} mAh. Velocidad actual: {VelocidadOptima} km/h");
        }

        public void TransferirBateria(Operador Operadordestino, int cantidad)
        {
            if (MismaLocalizacion(Operadordestino.Localizacion))
            {
                if (Bateria.getBateriaActuall() >= cantidad) 
                {
                    // Trnsfiere bateria
                }
            }
            else
            {
                Console.WriteLine("Operadores no están en la misma localización.");
            }
        }

        public void TransferirCarga(Operador Operadordestino, int cantidad)
        {
            if (MismaLocalizacion(Operadordestino.Localizacion))
            {
                if ((Operadordestino.Bateria.getBateriaActuall() + cantidad) <= Operadordestino.Bateria.getBateriaMaxima())
                {
                    // Trnsfiere carga
                }
            }
            else
            {
                Console.WriteLine("Operadores no están en la misma localización.");
            }
        }

        public void VolverAlCuartel(Localizacion localizacion)
        {
            Localizacion.InicializarLocalizacion(localizacion.DevolverX(), Localizacion.DevolverY()) ;
        }

        public void CambiarEsatado(Estado estado)
        {
            Estado = estado;
        }
        public string DevolverDatos() 
        {
            return $"ID: {ID}, Estado: {Estado}, Batería: {Bateria.getBateriaActuall()} mAh, Carga Máxima: {CargaMaxima} kg, Velocidad Óptima: {VelocidadOptima} km/h, Localización: {Localizacion.ObtenerCoordenadas()}";
        }
        public string DevolverId() 
        {
            return ID;
        }
        /*
        private int CalcularDistancia(Localizacion destino)
        {
            int x = Math.Abs(destino.X - Localizacion.X);
            int y = Math.Abs(destino.Y - Localizacion.Y);
            return (int)Math.Sqrt(x * x + y * y);
        }
        */
        public bool MismaLocalizacion(Localizacion lugar) 
        {
            return Localizacion.DevolverX() == lugar.DevolverX() && Localizacion.DevolverY() == lugar.DevolverY();
        }

        private string GeneraNombreOperador()
        {
            string alfabeto = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            string nombre = "";

            int i = 0, index = 0;

            while (i <= 8)
            {
                Random random = new Random();
                index = random.Next(0, 36);
                nombre += alfabeto[index];
                i++;
            }
            return nombre;
        }
    }
}
