using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using _OLC1_Proyecto2_201701029.Entorno;

namespace _OLC1_Proyecto2_201701029.Instruccion
{
    class Importar : Instruccion
    {
        public override Retornar ejectuar(Entorno.Entorno ent)
        {
            return new Retornar();
        }
    }
}
