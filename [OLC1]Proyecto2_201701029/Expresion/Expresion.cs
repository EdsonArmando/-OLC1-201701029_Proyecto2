using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static _OLC1_Proyecto2_201701029.Entorno.Simbolo;

namespace _OLC1_Proyecto2_201701029.Expresion
{
    abstract class Expresion
    {
        public Object valor;
        public EnumTipoDato tipo;
        abstract public Expresion obtenerValor(Entorno.Entorno ent);
        public abstract EnumTipoDato getTipo();
    }
}
