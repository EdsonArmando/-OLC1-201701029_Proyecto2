using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using _OLC1_Proyecto2_201701029.Entorno;

namespace _OLC1_Proyecto2_201701029.Expresion
{
    class Id : Expresion
    {
        string id;
        public Id(string id) {
            this.id = id; 
        }
        public override Simbolo.EnumTipoDato getTipo()
        {
            return this.tipo;
        }

        public override Expresion obtenerValor(Entorno.Entorno ent)
        {
            Simbolo simbolo = ent.obtener(id);
            if (simbolo!=null) {
                return new Literal(simbolo.getTipo(),simbolo.getValor());
            }
            return new Literal(Simbolo.EnumTipoDato.ERROR,"@Error@");
        }
    }
}
