namespace ProyectoIntegrador
{
    public class Menu
    {
        private Cuartel Cuartel;

        public Cuartel Cuartel1 { get => Cuartel; set => Cuartel = value; }

        Menu(Cuartel c)
        {
            Cuartel1 = c;
        }
    }
}
