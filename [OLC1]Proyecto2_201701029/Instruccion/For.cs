using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using _OLC1_Proyecto2_201701029.Entorno;
using _OLC1_Proyecto2_201701029.Expresion;

namespace _OLC1_Proyecto2_201701029.Instruccion
{
    class For : Instruccion
    {
        Instruccion asignacion;
        Operacion condicion;
        Instruccion aumento_decremento;
        LinkedList<Instruccion> listIntr;

        public For(Instruccion asignacion, Operacion condicion, Instruccion aumento_decremento, LinkedList<Instruccion> listIntr) {
            this.asignacion = asignacion;
            this.condicion = condicion;
            this.aumento_decremento = aumento_decremento;
            this.listIntr = listIntr;
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
            asignacion.ejectuar(tablaLocal);
            while ((Boolean)condicion.obtenerValor(tablaLocal).valor && seguirWhile) {
                
                foreach (Instruccion ins in listIntr)
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
                aumento_decremento.ejectuar(tablaLocal);
            }
            return new Retornar();
        }
    }
}
