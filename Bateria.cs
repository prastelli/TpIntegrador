namespace ProyectoIntegrador
{
    public class Bateria
    {
        int bateriaMaxima;
        int bateriaActual;

        public Bateria(int cargaMaxima)
        {
            this.bateriaMaxima = cargaMaxima;
            this.bateriaActual = cargaMaxima;
        }

        public int getBateriaMaxima()
        {
            return bateriaMaxima;
        }
        public int getBateriaActuall()
        {
            return bateriaActual;
        }
        public void cargarBateria()
        {
            bateriaActual = bateriaMaxima; // Carga completa            
        }
        public void inicializarCarga(int cargaMaxima) 
        {
            bateriaMaxima = cargaMaxima;
            bateriaActual = cargaMaxima;
        }
    }
}
