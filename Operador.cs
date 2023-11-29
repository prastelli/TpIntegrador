namespace ProyectoIntegrador
{
    // Clase base Operador
    public abstract class Operador
    {
        private string? iD1;
        private Estado estado1;
        private int cargaMaxima1;
        private Bateria bateria1;
        private Localizacion localizacion1;
        private Cuartel cuartel1;
        private double velocidadOptima1;
        private int cargaActual1;
        private int cantKimRecorridos;
        private Dictionary<TipoDaño, Boolean> daño = new Dictionary<TipoDaño, bool>{
                { TipoDaño.MotorComprometido,false },
                { TipoDaño.ServoAtascado,false },
                { TipoDaño.BateriaPerforada,false },
                { TipoDaño.PuertoBateriaDesconectado,false },
                { TipoDaño.PinturaRayada,false }
            };

        public string? iD { get => iD1; set => iD1 = value; }

        public Estado estado { get => estado1; set => estado1 = value; }
        public int cargaMaxima { get => cargaMaxima1; set => cargaMaxima1 = value; }
        public int cargaActual { get => cargaActual1; set => cargaActual1 = value; }
        public double velocidadOptima { get => velocidadOptima1; set => velocidadOptima1 = value; }
        public Bateria bateria { get => bateria1; set => bateria1 = value; }

        public Cuartel cuartel { get => cuartel1; set => cuartel1 = value; }

        public Localizacion localizacion { get => localizacion1; set => localizacion1 = value; }
        public int CantKimRecorridos { get => cantKimRecorridos; set => cantKimRecorridos = value; }
        public Dictionary<TipoDaño, bool> Daño { get => daño; set => daño = value; }

        public Operador(int cargaBateria, Estado estado, int cargaMaxima, int velocidadOptima, Cuartel cuartel)
        {
            iD = Helpers.GeneraId();
            bateria = new Bateria(cargaBateria);
            this.estado = estado;
            this.cargaMaxima = cargaMaxima;
            this.velocidadOptima = velocidadOptima;
            cargaActual = 0;
            this.cuartel = cuartel;
            localizacion = cuartel.Localizacion;
        }

        public virtual void Mover(Localizacion destino)
        {
            /* 
              * Rutinas para unidades que NO tienen que rodear un obstaculo
             */
            int xOp, yOp;
            int xDe, yDe;
            int distancia = localizacion.CalcularDistanciaAOtroDestino(destino);

            if (distancia <= 0)
            {
                throw new Exception("La localización destino no es correcta");
            }
            xOp = localizacion.X1;
            yOp = localizacion.Y1;
            xDe = destino.X1;
            yDe = destino.Y1;

            int x = xOp < xDe ? 1 : -1;
            int y = yOp < yDe ? 1 : -1;

            // Se mueve por el eje x hasta llegar al eje x destino
            while (localizacion.X1 < xDe)
            {
                localizacion.X1 += x;

                TipoLocalizacion tipoLocalizacion = cuartel.Mapa1.TipoTerrenoPorCoordenadas(localizacion.X1, yOp);

                if (tipoLocalizacion == TipoLocalizacion.Vertedero)
                {
                    HayDaño();
                }
            }

            // Se mueve por el eje x hasta llegar al eje x destino
            while (localizacion.Y1 < yDe)
            {
                localizacion.Y1 += y;

                TipoLocalizacion tipoLocalizacion = cuartel.Mapa1.TipoTerrenoPorCoordenadas(localizacion.X1, yOp);

                if (tipoLocalizacion == TipoLocalizacion.Vertedero)
                {
                    HayDaño();
                }

                if (tipoLocalizacion == TipoLocalizacion.VertederoElectronico)
                {
                    bateria.SetearCargaBateria(bateria.BateriaActual * 20 / 100);
                }

            }

            double velocidadAjustada = AjustarVelocidadPorCarga();
            ConsumirBateria(bateria, distancia, velocidadAjustada);
            CantKimRecorridos += distancia;
        }

        public double AjustarVelocidadPorCarga()
        {
            if (velocidadOptima <= 0) 
            {
                throw new Exception("No se puede ajustar la velocidad por error en los parametros");
            }

            double porcentajeCargaUtilizada = (double)cargaActual / cargaMaxima;
            double velocidadAjustada = velocidadOptima - ((porcentajeCargaUtilizada * 0.05/ 0.1) * velocidadOptima);
            return velocidadAjustada;
        }

        public void ConsumirBateria(Bateria bateria, int distancia, double velocidad)
        {
            double consumoBateria = (distancia / velocidad) * 100;
            bateria.ActualizarCargaBateria((int)consumoBateria * -1);

            if (bateria.BateriaActual <= 0)
            {
                Console.WriteLine("¡El operador ha quedado sin energía!");
            }
        }

        public void TransferirBateria(Operador Operadordestino, int cantidad)
        {
            // Comprueba que los operadores estén en la misma localización
            if (!MismaLocalizacion(Operadordestino.localizacion))
            {
                throw new Exception("Los operadores deben estar en la misma localización para transferir bateria.");
            }
            // Comprueba que la batería del operador tenga suficiente carga
            if (bateria.BateriaActual > cantidad )
            {
                throw new Exception("La batería del operador no tiene suficiente carga para transferir.");
            }

            // Transfiere bateria
            bateria.ActualizarCargaBateria( -1 * cantidad);
            Operadordestino.bateria.ActualizarCargaBateria(cantidad);
        }

        public void TransferirCarga(Operador Operadordestino, int cantidad)
        {
            if (!MismaLocalizacion(Operadordestino.localizacion)) throw new Exception("Los operadores deben estar en la misma localización para transferir carga.");

            if (daño[TipoDaño.ServoAtascado] && Operadordestino.daño[TipoDaño.ServoAtascado] ) throw new Exception("Operador con Servo Atascado NO puede transferir carga.");

            if (cantidad <= cargaActual && cantidad + Operadordestino.cargaActual <= Operadordestino.cargaMaxima) throw new Exception("Carga a transferir supera carga máxima.");
            
            cargaActual -= cantidad;
            Operadordestino.cargaActual += cantidad;
        }

        public void Descargar()
        { 
            cargaActual = 0; 
        }

        public void CambiarEsatado(Estado estado)
        {
            this.estado = estado;
        }

        public string DevolverDatosOperador() 
        {
            return $"ID: {iD}, Estado: {estado}, Batería: {bateria.BateriaActual} mAh, Carga Máxima: {cargaMaxima} kg, Velocidad Óptima: {velocidadOptima} km/h, Localización: {localizacion.ObtenerCoordenadas()}";
        }
        public bool MismaLocalizacion(Localizacion lugar) 
        {
            return localizacion.DevolverX() == lugar.DevolverX() && localizacion.DevolverY() == lugar.DevolverY();
        }

        public void HayDaño()
        {
            Random random = new Random();
            int danio = random.Next(0, 999);

            if (danio >= 0 && danio <= 5)
            {
                daño[(TipoDaño)danio] = true;

                if ((TipoDaño)danio == TipoDaño.MotorComprometido)
                {
                    velocidadOptima = velocidadOptima / 2;
                }
            }
        }
        public void RecogerCargaMaxima() 
        {
            cargaActual1 = cargaMaxima1;
        }
        public void BateriaNueva()
        {
            if (daño[TipoDaño.BateriaPerforada])
            {
                Mover(cuartel.Localizacion);
                bateria.BateriaNueva();
            }

        }
    }
}
