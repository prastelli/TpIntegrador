using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProyectoIntegrador
{
    public class Localizacion
    {
        public int X;
        public int Y;

        public Localizacion() { }

        public Localizacion(int x, int y)
        {
            X = x;
            Y = y;
        }

        public void InicializarLocalizacion(int x, int y)
        {
            X = x;
            Y = y;
        }
        public int DevolverX() 
        {
            return X;
        }
        public int DevolverY()
        {
            return Y;
        }

        public string ObtenerCoordenadas()
        {
            return $"({X},{Y})";
        }
        public int CalcularDistanciaAOtroDestino(Localizacion destino)
        {
            int x = Math.Abs(destino.X - X);
            int y = Math.Abs(destino.Y - Y);
            return (int)Math.Sqrt(x * x + y * y);
        }
    }
}
