﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProyectoIntegrador
{
    public interface IOperadorFactory
    {
        Operador CrearOperador(string tipo);
    }
}
