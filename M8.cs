namespace ProyectoIntegrador
{
    // Clase M8 (Entidades semi-humanoides)
    class M8 : Operador
    {
        public M8(Cuartel cuartel)
            : base(12250, Estado.Activo, 250, 30, cuartel)
        {
        }


        public override void Mover(Localizacion destino)
        {
            /*
             * Rutinas para unidades que SI tienen que esquivar obstaculos
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
            /* Rutina de movimiento que utiliza las posiciones aleatorias generadas en el mapa
             * Lo dejo para el final porque no se si llego 
            */

            // Se mueve por el eje x hasta llegar al eje x destino
            while (localizacion.X1 < xDe)
            {
                localizacion.X1 += x;

                TipoLocalizacion tipoLocalizacionEjeX = cuartel.Mapa1.TipoTerrenoPorCoordenadas(localizacion.X1, localizacion.Y1);

                if (tipoLocalizacionEjeX == TipoLocalizacion.Lago)
                {
                    throw new Exception("El Operador no puede moverse en el aguaaaaa");
                }

                if (tipoLocalizacionEjeX == TipoLocalizacion.Vertedero)
                {
                    HayDaño();
                }

                if (tipoLocalizacionEjeX == TipoLocalizacion.VertederoElectronico)
                {
                    bateria.SetearCargaBateria(bateria.BateriaActual * 20 / 100);
                }
            }

            // Se mueve por el eje y hasta llegar al eje y destino
            while (localizacion.Y1 < yDe)
            {
                localizacion.Y1 += y;

                TipoLocalizacion tipoLocalizacionEjeY = cuartel.Mapa1.TipoTerrenoPorCoordenadas(localizacion.X1, yOp);

                if (tipoLocalizacionEjeY == TipoLocalizacion.Lago)
                {
                    throw new Exception("El Operador no puede moverse en el aguaaaaa");
                }

                if (tipoLocalizacionEjeY == TipoLocalizacion.Vertedero)
                {
                    HayDaño();
                }

                if (tipoLocalizacionEjeY == TipoLocalizacion.VertederoElectronico)
                {
                    bateria.SetearCargaBateria(bateria.BateriaActual * 20 / 100);
                }

            }

            double velocidadAjustada = AjustarVelocidadPorCarga();
            ConsumirBateria(bateria, distancia, velocidadAjustada);
            CantKimRecorridos += distancia;
        }
    }
}
