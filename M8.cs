namespace ProyectoIntegrador
{
    // Clase M8 (Entidades semi-humanoides)
    class M8 : Operador
    {
        public M8(Cuartel cuartel)
            : base(12250, Estado.Activo, 250, 30, cuartel)
        {
        }
    }
}
