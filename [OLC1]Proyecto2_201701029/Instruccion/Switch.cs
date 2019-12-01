using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using _OLC1_Proyecto2_201701029.Entorno;
using _OLC1_Proyecto2_201701029.Expresion;

namespace _OLC1_Proyecto2_201701029.Instruccion
{
    class Switch : Instruccion
    {
        private Id variable;
        private LinkedList<Instruccion> Listcase;
        private bool ejecutado;
        Case_Switch defaul;
        public Switch(Id variable, LinkedList<Instruccion> Listcase) {
            this.variable = variable;
            this.Listcase = Listcase;
        }
        public override Retornar ejectuar(Entorno.Entorno ent)
        {
            bool condicion=false;
            foreach (Case_Switch case_ in Listcase) {
                if (case_.dafaul.Equals("DEFAULT")) {
                    defaul = case_;
                }
                string dos = variable.obtenerValor(ent).valor.ToString();
                try {
                    condicion = case_.valor.obtenerValor(ent).valor.ToString().Equals(variable.obtenerValor(ent).valor.ToString());
                }
                catch (Exception ex) {
                    condicion = false;
                        }
                if (condicion==true)
                {
                    ejecutado = true;
                    Retornar reto = case_.ejectuar(ent);
                    if (reto.isReturn)
                    {
                        return reto;
                    }
                }
            }
            if (ejecutado!=true) {
                Retornar reto = defaul.ejectuar(ent);
                if (reto.isReturn)
                {
                    return reto;
                }
            }
            return new Retornar();
        }
    }
}
