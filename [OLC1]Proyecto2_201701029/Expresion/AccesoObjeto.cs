using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using _OLC1_Proyecto2_201701029.Entorno;
using _OLC1_Proyecto2_201701029.Expresion;

namespace _OLC1_Proyecto2_201701029.Instruccion
{
    class AccesoObjeto : Expresion.Expresion
    {
        String idObjeto;
        LinkedList<Expresion.Expresion> acceso;
        LinkedList<Expresion.Expresion> listParametros;

        public AccesoObjeto(String idObjeto, LinkedList<Expresion.Expresion> acceso) {
            this.idObjeto = idObjeto;
            this.acceso = acceso;
        }
        public AccesoObjeto(String idObjeto, LinkedList<Expresion.Expresion> acceso, LinkedList<Expresion.Expresion> listParametros)
        {
            this.listParametros = listParametros;
            this.idObjeto = idObjeto;
            this.acceso = acceso;
        }

        public override Simbolo.EnumTipoDato getTipo()
        {
            throw new NotImplementedException();
        }

        public override Expresion.Expresion obtenerValor(Entorno.Entorno ent)
        {
            Objeto obj=null;
            Simbolo sim = ent.obtener(idObjeto);
            if (sim.tipo == Simbolo.EnumTipoDato.OBJETO)
            {
                obj  = (Objeto)sim.valor;
            }
            else {
                LlamadaFuncion funcion = new LlamadaFuncion(idObjeto, listParametros);
                Retornar contenido = funcion.ejectuar(ent);
                Expresion.Expresion temp = contenido.valor;
                return new Literal(contenido.valor.tipo, temp.valor);
            }
            
            foreach (Expresion.Expresion exp in acceso) {
                if (listParametros!=null) {
                    Function nuevo = (Function) exp.obtenerValor(obj.entObjeto).valor;
                    LlamadaFuncion funcion = new LlamadaFuncion(nuevo.id,listParametros);
                    Retornar contenido = funcion.ejectuar(obj.entObjeto);
                    if (contenido !=null) {
                        Expresion.Expresion temp = contenido.valor;
                        //return new Literal(contenido.valor.tipo, temp.valor);
                        return new Literal(contenido.valor.tipo, contenido.valGuardado);
                    }
                }
                
                return new Literal(Simbolo.EnumTipoDato.OBJETO,exp.obtenerValor(obj.entObjeto).valor);
            }
            throw new NotImplementedException();
        }
    }
}
