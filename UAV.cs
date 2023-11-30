namespace ProyectoIntegrador
{
    // Clase UAV (Drones voladores)
    class UAV : Operador
    {
        public UAV(Cuartel cuartel)
            : base(4000, Estado.Activo, 5, 100, cuartel)
        {
        }

        public override void Mover(Localizacion destino)
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

                TipoLocalizacion tipoLocalizacionX = cuartel.Mapa1.TipoTerrenoPorCoordenadas(localizacion.X1, yOp);

                if (tipoLocalizacionX == TipoLocalizacion.Vertedero)
                {
                    HayDaño();
                }

                if (tipoLocalizacionX == TipoLocalizacion.VertederoElectronico)
                {
                    bateria.SetearCargaBateria(bateria.BateriaActual * 20 / 100);
                }
            }

            // Se mueve por el eje x hasta llegar al eje x destino
            while (localizacion.Y1 < yDe)
            {
                localizacion.Y1 += y;

                TipoLocalizacion tipoLocalizacionY = cuartel.Mapa1.TipoTerrenoPorCoordenadas(localizacion.X1, localizacion.Y1);

                if (tipoLocalizacionY == TipoLocalizacion.Vertedero)
                {
                    HayDaño();
                }

                if (tipoLocalizacionY == TipoLocalizacion.VertederoElectronico)
                {
                    bateria.SetearCargaBateria(bateria.BateriaActual * 20 / 100);
                }

                if (tipoLocalizacionY == TipoLocalizacion.VertederoElectronico)
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
