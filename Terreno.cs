namespace ProyectoIntegrador
{
    public sealed class Terreno
    {

        private static Terreno _instance;

        private Terreno()
        {
        }

        public static Terreno GetInstance()
        {
            if (_instance == null)
            {
                _instance = new Terreno();
            }
            return _instance;
        }


        private TipoLocalizacion[,] mapa;
        private List<Localizacion> sitiosReciclaje1;
        private List<Localizacion> cuartels1;
        private List<Localizacion> vertedero1;

        public TipoLocalizacion[,] Mapa { get => mapa; private set => mapa = value; }

        public List<Localizacion> sitiosReciclaje { get => sitiosReciclaje1; private set => sitiosReciclaje1 = value; }
        public List<Localizacion> cuartels { get => cuartels1; private set => cuartels1 = value; }
        public List<Localizacion> vertederos { get => vertedero1; private set => vertedero1 = value; }


        /// <summary> Este metodo muestra el mapa con las distribuciones aleatorias del terreno
        /// Parametros TamanioTerreno, Cant. Sitios Reciclaje, Cant. Cuarteles, Cant. Vertederos.
        /// </summary>      
        public void InicializarMapa(int TamanioTerreno, int MaxSitiosReciclaje, int MaxCuartels, int MaxVertederos)
        {
            Random random = new Random();
            Mapa = new TipoLocalizacion[TamanioTerreno, TamanioTerreno];
            sitiosReciclaje = GenerarCoordenadasAleatorias(MaxSitiosReciclaje, TamanioTerreno);
            cuartels = GenerarCoordenadasAleatorias(MaxCuartels, TamanioTerreno);
            vertederos = GenerarCoordenadasAleatorias(MaxVertederos, TamanioTerreno);

            // Establecer ubicaciones específicas para Sitios de Reciclaje, Cuarteles, Vertederos
            foreach (Localizacion sitioReciclaje in sitiosReciclaje)
            {
                Mapa[(int)sitioReciclaje.X1, (int)sitioReciclaje.Y1] = TipoLocalizacion.SitioReciclaje;
            }

            foreach (Localizacion cuartel in cuartels)
            {
                Mapa[(int)cuartel.X1, (int)cuartel.Y1] = TipoLocalizacion.Cuartel;
            }

            foreach (Localizacion vertedero in vertederos)
            {
                Mapa[(int)vertedero.X1, (int)vertedero.Y1] = TipoLocalizacion.Vertedero;
            }

            for (int i = 0; i < TamanioTerreno; i++)
            {
                for (int j = 0; j < TamanioTerreno; j++)
                {
                    int tipoLocalizacion = random.Next(1, Enum.GetNames(typeof(TipoLocalizacion)).Length - 3);

                    if (Mapa[i, j] == TipoLocalizacion.SinGenerar)
                    {
                        Mapa[i, j] = (TipoLocalizacion)tipoLocalizacion;

                    }
                }
            }
        }

        private List<Localizacion> GenerarCoordenadasAleatorias(int cantidad, int TamanioTerreno)
        {
            List<Localizacion> coordenadas = new List<Localizacion>();
            Random random = new Random();

            for (int i = 0; i < cantidad; i++)
            {
                int x = random.Next(TamanioTerreno);
                int y = random.Next(TamanioTerreno);
                coordenadas.Add(new Localizacion(x, y));
            }

            return coordenadas;
        }

        /// <summary> Recupero el tipo de Terreno en Base a una Coordenada</summary>
        public TipoLocalizacion TipoTerrenoPorCoordenadas(int x, int y) 
        {
            TipoLocalizacion tipo = Mapa[x, y];

            return tipo;
        }


        /// <summary> Este metodo muestra el mapa con las distribuciones aleatorias del terreno</summary>        
        public void MostrarMapa()
        {
            foreach (TipoLocalizacion tipo in Enum.GetValues(typeof(TipoLocalizacion)))
            {
                Console.WriteLine($" {(int)tipo} - {tipo} ");
            }
            Console.WriteLine("\n");
            Console.WriteLine("Mapa generado aleatoriamente\n");

            for (int i = 0; i < Mapa.GetLength(0); i++)
            {
                for (int j = 0; j < Mapa.GetLength(1); j++)
                {
                    if (Mapa[i, j] == TipoLocalizacion.Cuartel)
                    {
                        Console.BackgroundColor = ConsoleColor.Red;
                        Console.Write((int)Mapa[i, j]);
                    }
                    if (Mapa[i, j] == TipoLocalizacion.SitioReciclaje)
                    {
                        Console.BackgroundColor = ConsoleColor.Yellow;
                        Console.Write((int)Mapa[i, j]);
                    }
                    if (Mapa[i, j] != TipoLocalizacion.SitioReciclaje && Mapa[i, j] != TipoLocalizacion.Cuartel)
                        Console.Write((int)Mapa[i, j]);
                    Console.ResetColor();
                }
                Console.WriteLine();
            }
            
        }
    } 
}

