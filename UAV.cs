namespace ProyectoIntegrador
{
    // Clase UAV (Drones voladores)
    class UAV : Operador
    {
        public UAV()
            : base(4000, Estado.Activo, 5, 100)
        {
        }
    }
}
