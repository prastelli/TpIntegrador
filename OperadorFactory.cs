using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProyectoIntegrador
{
    public class OperadorFactory : IOperadorFactory
    {
        public Operador CrearOperador(string tipo)
        {
            switch (tipo)
            {
                case "UAV":
                    return new UAV();
                case "K9":
                    return new K9();
                case "M8":
                    return new M8();
                default:
                    throw new ArgumentException($"Tipo de operador inválido: {tipo}");
            }
        }
    }
}
