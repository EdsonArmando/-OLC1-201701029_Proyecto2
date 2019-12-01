using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using _OLC1_Proyecto2_201701029.Entorno;

namespace _OLC1_Proyecto2_201701029.Instruccion
{
    class Function : Instruccion
    {
        public String id;
        public LinkedList<String> ids;
        public LinkedList<Instruccion> listInstrucciones;
        public Function(String id, LinkedList<String> ids, LinkedList<Instruccion> listInstrucciones) {
            this.id = id;
            this.ids = ids;
            this.listInstrucciones = listInstrucciones;
        }
        public override Retornar ejectuar(Entorno.Entorno ent)
        {
            ent.Insertar(id,new Simbolo(Simbolo.EnumTipoDato.FUNCION,this,id));
            return new Retornar();
        }
    }
}
