using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using _OLC1_Proyecto2_201701029.Entorno;

namespace _OLC1_Proyecto2_201701029.Instruccion
{
    class Alert : Instruccion
    {
        LinkedList<Expresion.Expresion> listExpr;
        public Alert(LinkedList<Expresion.Expresion> listExpr) {
            this.listExpr = listExpr;
        }
        public override Retornar ejectuar(Entorno.Entorno ent)
        {
            foreach (Expresion.Expresion item in listExpr) {
                Expresion.Expresion resultado = item.obtenerValor(ent);
                if (resultado.valor!=null) {
                    MessageBox.Show(resultado.valor.ToString());
                }
                
            }
            return new Retornar();
        }
    }
}
