using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProyectoIntegrador
{
    public class OperadorFactory : IOperadorFactory
    {
        public Operador CrearOperador(TipoOperador tipo, Cuartel cuartel)
        {
            switch (tipo)
            {
                case TipoOperador.UAV:
                    return new UAV(cuartel);
                case TipoOperador.K9:
                    return new K9(cuartel);
                case TipoOperador.M8:
                    return new M8(cuartel);
                default:
                    throw new ArgumentException($"Tipo de operador inválido: {tipo}");
            }
        }
    }
}
