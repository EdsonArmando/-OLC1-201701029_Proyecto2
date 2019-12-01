using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using _OLC1_Proyecto2_201701029.Entorno;

namespace _OLC1_Proyecto2_201701029.Instruccion
{
    class LlamadaFuncion : Instruccion
    {
        Instruccion llamadaFuncion;
        String id;
        LinkedList<Expresion.Expresion> listParametros;
        public LlamadaFuncion(String id, LinkedList<Expresion.Expresion> listParam) {
            
            this.id = id;
            this.listParametros = listParam;
        }
        public LlamadaFuncion(String id, LinkedList<Expresion.Expresion> listParam, Instruccion llamadaFuncion)
        {
            this.llamadaFuncion = llamadaFuncion;
            this.id = id;
            this.listParametros = listParam;
        }
        public override Retornar ejectuar(Entorno.Entorno ent)
        {
            Entorno.Entorno tablaLocal = new Entorno.Entorno(null);
            try
            {
                ICollection letra = ent.tabla.Keys;
                foreach (String let in letra)
                {
                    tablaLocal.Insertar(let, ent.obtener(let));
                }
            }
            catch (Exception e) { }

            /*foreach (Simbolo item in ent.listSimbolo)
            {
                tablaLocal.listSimbolo.AddLast(item);
            }*/
            Simbolo sim = ent.obtener(id);
            Function funcion = (Function)sim.valor;
            LinkedList<String> porDeclara = funcion.ids;
            LinkedList<Instruccion> listInst = funcion.listInstrucciones;
     
            int cont=0;

            
            foreach (Expresion.Expresion item in listParametros) {
                tablaLocal.Insertar(porDeclara.ElementAt(cont), new Simbolo(Simbolo.EnumTipoDato.OBJETO, item.obtenerValor(tablaLocal).valor,porDeclara.ElementAt(cont)));
                cont++;
            }
            
            foreach (Instruccion ins in listInst) {
                /**/

                Retornar retorn = ins.ejectuar(tablaLocal);
                if (retorn.isReturn) {
                    
                    return retorn;
                }
            }
            return new Retornar();
        }
    }
}
