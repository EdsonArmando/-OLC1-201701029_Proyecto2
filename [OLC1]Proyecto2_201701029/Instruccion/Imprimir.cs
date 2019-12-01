using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using _OLC1_Proyecto2_201701029.Entorno;

namespace _OLC1_Proyecto2_201701029.Instruccion
{
    class Imprimir : Instruccion
    {
        LinkedList<Expresion.Expresion> listaExpresiones;
        public Imprimir(LinkedList<Expresion.Expresion> listaExpresiones) {
            this.listaExpresiones = listaExpresiones;
        }
        public override Retornar ejectuar(Entorno.Entorno ent)
        {
            Expresion.Expresion resultado = null;
            if (listaExpresiones!=null) {
                foreach (Expresion.Expresion exp in listaExpresiones) {
                    resultado = exp.obtenerValor(ent);
                    if (resultado != null)
                    {
                        if (resultado.valor == null)
                        {
                            Form1.salidaConsola.AppendText("null\n");
                        }
                        else {
                            Form1.salidaConsola.AppendText(resultado.valor.ToString() + "\n");
                        }
                    }
                    else {
                        Form1.salidaConsola.AppendText("@Error@\n");
                    }
                }
            }
            return new Retornar();
        }
    }
}
