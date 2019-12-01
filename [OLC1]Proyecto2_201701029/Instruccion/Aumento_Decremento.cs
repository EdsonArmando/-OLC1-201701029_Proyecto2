using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using _OLC1_Proyecto2_201701029.Entorno;

namespace _OLC1_Proyecto2_201701029.Instruccion
{
    class Aumento_Decremento : Instruccion
    {
        String id;
        String tipo;
        public Aumento_Decremento(String id, String tipo) {
            this.id = id;
            this.tipo = tipo;
        }
        public override Retornar ejectuar(Entorno.Entorno ent)
        {
            if (tipo.Equals("Aumento"))
            {
                Simbolo variable = ent.obtener(id);
                int valAument = int.Parse(variable.valor.ToString());
                valAument = valAument + 1;
                variable.setValor((object)valAument);
                ent.Insertar(id, variable);
            }
            else {
                Simbolo variable = ent.obtener(id);
                int valAument = int.Parse(variable.valor.ToString());
                valAument = valAument - 1;
                variable.setValor((object)valAument);
                ent.Insertar(id, variable);
            }
            return new Retornar();
        }
    }
}
