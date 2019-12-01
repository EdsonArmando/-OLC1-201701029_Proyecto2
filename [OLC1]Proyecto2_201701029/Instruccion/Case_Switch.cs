using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using _OLC1_Proyecto2_201701029.Entorno;
using _OLC1_Proyecto2_201701029.Expresion;

namespace _OLC1_Proyecto2_201701029.Instruccion
{
    class Case_Switch : Instruccion
    {
        public Literal valor;
        public String dafaul="";
        private LinkedList<Instruccion> listInstrucciones;
        public Case_Switch(Literal valor, LinkedList<Instruccion> listInstrucciones) {
            this.valor = valor;
            this.listInstrucciones = listInstrucciones;
        }
        public Case_Switch(String defaul, LinkedList<Instruccion> listInstrucciones)
        {
            this.dafaul = defaul;
            this.listInstrucciones = listInstrucciones;
        }
        public override Retornar ejectuar(Entorno.Entorno ent)
        {
            Entorno.Entorno tablaLocal = new Entorno.Entorno(null);
            tablaLocal.tabla = ent.tabla;
           /* foreach (Simbolo item in ent.listSimbolo)
            {
                tablaLocal.listSimbolo.AddLast(item);
            }*/
            foreach (Instruccion ins in listInstrucciones) {
                Retornar retorn = ins.ejectuar(tablaLocal);
                if (retorn.isReturn) {
                    return retorn; 
                }
            }
            return new Retornar();
        }
    }
}
