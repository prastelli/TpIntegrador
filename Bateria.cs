using System.Runtime.InteropServices;

namespace ProyectoIntegrador
{
    public class Bateria
    {
        int bateriaMaxima;
        int bateriaActual;

        public int BateriaMaxima { get => bateriaMaxima; set => bateriaMaxima = value; }
        public int BateriaActual { get => bateriaActual; set => bateriaActual = value; }

        public Bateria(int cargaMaxima)
        {
            this.BateriaMaxima = cargaMaxima;
            this.BateriaActual = cargaMaxima;
        }
         public void cargarBateria()
        {
            BateriaActual = BateriaMaxima; // Carga completa            
        }
         public void ActualizarCargaBateria(int cantidad)
        {
            BateriaActual += cantidad;
        }
        public void BateriaNueva()
        {            
            BateriaActual = BateriaMaxima; // Es lo mismo que la carga de Bateria, no se me ocurrio otra cosa          
        }

        public void SetearCargaBateria(int cantidad) 
        {
            BateriaActual = cantidad;
        }
    }
}
