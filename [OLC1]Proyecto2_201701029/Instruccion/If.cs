using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using _OLC1_Proyecto2_201701029.Entorno;
using _OLC1_Proyecto2_201701029.Expresion;

namespace _OLC1_Proyecto2_201701029.Instruccion
{
    class If : Instruccion
    {
        private Operacion condicion;
        private LinkedList<Instruccion> listaInstrucciones;
        private LinkedList<Instruccion> listaInsElse;
        public If(Operacion condicion, LinkedList<Instruccion> listInstr)
        {
            this.condicion = condicion;
            this.listaInstrucciones = listInstr;
        }
        public If(Operacion condicion, LinkedList<Instruccion> listIf, LinkedList<Instruccion> listElse)
        {
            this.condicion = condicion;
            this.listaInstrucciones = listIf;
            this.listaInsElse = listElse;
        }
        public override Retornar ejectuar(Entorno.Entorno ent)
        {
            Entorno.Entorno tablaLocal = new Entorno.Entorno(null);
            tablaLocal.tabla = ent.tabla;
            if ((Boolean)condicion.obtenerValor(tablaLocal).valor)
            {
                
                /*foreach (Simbolo item in ent.listSimbolo)
                {
                    tablaLocal.listSimbolo.AddLast(item);
                }*/
                foreach (Instruccion ins in listaInstrucciones)
                {
                    Retornar contenido = ins.ejectuar(tablaLocal);
                    if (contenido.isBreak) {
                        return contenido;
                    } else if (contenido.isReturn) {
                        Expresion.Expresion exp = contenido.valor;

                        exp.valor = exp.obtenerValor(tablaLocal).valor;
                        return contenido;
                    }
                }
            }
            else {
                if (listaInsElse!=null) {
                   
                    /*foreach (Simbolo item in ent.listSimbolo)
                    {
                        tablaLocal.listSimbolo.AddLast(item);
                    }*/
                    foreach (Instruccion ins in listaInsElse)
                    {
                        Retornar contenido = ins.ejectuar(tablaLocal);
                        if (contenido.isBreak)
                        {
                            return contenido;
                        }
                        else if (contenido.isReturn)
                        {
                            Expresion.Expresion exp = contenido.valor;
                            
                            exp.valor = exp.obtenerValor(tablaLocal).valor;
                            
                            return contenido;
                        }
                    }
                }
            }
            return new Retornar();
        }
    }
}
