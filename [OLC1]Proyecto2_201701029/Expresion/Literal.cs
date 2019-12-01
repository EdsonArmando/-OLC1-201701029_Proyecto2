using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using _OLC1_Proyecto2_201701029.Entorno;

namespace _OLC1_Proyecto2_201701029.Expresion
{
    class Literal : Expresion
    {
        public Literal(Simbolo.EnumTipoDato tipo, Object valor) {
            this.tipo = tipo;
            this.valor = valor;
        }
        public override Simbolo.EnumTipoDato getTipo()
        {
            return this.tipo;
        }

        public override Expresion obtenerValor(Entorno.Entorno ent)
        {
            return this;
        }
    }
}
