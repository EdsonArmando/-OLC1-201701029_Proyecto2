using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using _OLC1_Proyecto2_201701029.Entorno;

namespace _OLC1_Proyecto2_201701029.Instruccion
{

    class AccesoObj_MetodosFunciones : Instruccion
    {
        String idObjeto;
        LinkedList<String> acceso;
        LinkedList<Expresion.Expresion> listParametros;
        public AccesoObj_MetodosFunciones(String idObjeto, LinkedList<String> acceso, LinkedList<Expresion.Expresion> listParametros)
        {
            this.listParametros = listParametros;
            this.idObjeto = idObjeto;
            this.acceso = acceso;
        }

        public override Retornar ejectuar(Entorno.Entorno ent)
        {
            Simbolo sim = ent.obtener(idObjeto);
            Objeto obj = (Objeto)sim.valor;

            foreach (String exp in acceso)
            {
                Simbolo simb = obj.entObjeto.obtener(exp);
                LlamadaFuncion llamada = new LlamadaFuncion(exp,listParametros);
                llamada.ejectuar(obj.entObjeto);
            }
            return new Retornar();
        }
    }
}
