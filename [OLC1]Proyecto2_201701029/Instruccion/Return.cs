using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using _OLC1_Proyecto2_201701029.Entorno;

namespace _OLC1_Proyecto2_201701029.Instruccion
{
    class Return : Instruccion
    {
        public Expresion.Expresion valorReturn;
        public Object valGuardado;
        public Return(Expresion.Expresion valor){
            this.valorReturn = valor;
        }
        public override Retornar ejectuar(Entorno.Entorno ent)
        {

            Retornar r = new Retornar();
            
            r.isReturn = true;
            r.valor = valorReturn;
            r.valGuardado = valorReturn.obtenerValor(ent).valor;
            return r;
        }
    }
}
