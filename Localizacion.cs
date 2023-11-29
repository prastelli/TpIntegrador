namespace ProyectoIntegrador
{
    public class Localizacion
    {
        int X;
        int Y;

        public int X1 { get => X; set => X = value; }
        public int Y1 { get => Y; set => Y = value; }

        public Localizacion(int x, int y)
        {
            X1 = x;
            Y1 = y;
         }

        public void InicializarLocalizacion(int x, int y)
        {
            X1 = x;
            Y1 = y;
        }
        public int DevolverX() 
        {
            return X1;
        }
        public int DevolverY()
        {
            return Y1;
        }

        public string ObtenerCoordenadas()
        {
            return $"({X1},{Y1})";
        }
        public int CalcularDistanciaAOtroDestino(Localizacion destino)
        {
            int x = Math.Abs(destino.X1 - X1);
            int y = Math.Abs(destino.Y1 - Y1);
            return (int)Math.Sqrt(x * x + y * y);
        }
    }
}
