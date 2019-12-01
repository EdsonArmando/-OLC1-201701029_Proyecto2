using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using _OLC1_Proyecto2_201701029.Entorno;
using _OLC1_Proyecto2_201701029.Expresion;

namespace _OLC1_Proyecto2_201701029.Instruccion
{
    
    class Do_While : Instruccion
    {
        Operacion condicion;
        LinkedList<Instruccion> listaIntr;

        public Do_While(Operacion condicion, LinkedList<Instruccion> listaIntr) {
            this.condicion = condicion;
            this.listaIntr = listaIntr;
        }
        public override Retornar ejectuar(Entorno.Entorno ent)
        {
            bool seguirWhile = true;
            Entorno.Entorno tablaLocal = new Entorno.Entorno(null);
            tablaLocal.tabla = ent.tabla;
            /*foreach (Simbolo item in ent.listSimbolo)
            {
                tablaLocal.listSimbolo.AddLast(item);
            }*/
            foreach (Instruccion ins in listaIntr) {
                ins.ejectuar(tablaLocal);
            }
            while ((Boolean)condicion.obtenerValor(ent).valor && seguirWhile)
            {
                
                foreach (Instruccion ins in listaIntr)
                {
                    Retornar contenido = ins.ejectuar(tablaLocal);
                    if (contenido.isBreak)
                    {
                        seguirWhile = false;
                        return contenido;
                    }
                    if (contenido.isContinue)
                    {
                        break;
                    }

                    if (contenido.isReturn)
                    {
                        return contenido;
                    }
                }
            }

            return new Retornar();
        }
    }
}
