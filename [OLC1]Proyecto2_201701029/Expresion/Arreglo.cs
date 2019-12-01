using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using _OLC1_Proyecto2_201701029.Entorno;

namespace _OLC1_Proyecto2_201701029.Expresion
{
    class Arreglo : Expresion
    {

        public Expresion[] valores;
        public Expresion tamanioArreglo;
        public EnumTamanioArreglo tipo;
        public String nombreVariable;
        public Arreglo(Expresion tamanio, EnumTamanioArreglo tipo, String nombre) {
            this.tamanioArreglo = tamanio;
            this.nombreVariable = nombre;
            this.tipo = tipo;
        }
        public override Simbolo.EnumTipoDato getTipo()
        {
            throw new NotImplementedException();
        }

        public override Expresion obtenerValor(Entorno.Entorno ent)
        {
            
            valores = new Expresion[Int32.Parse(tamanioArreglo.obtenerValor(ent).valor.ToString())];
            return this;
        }
        public Expresion returnValor(int posicion) {
            return valores[posicion];
        }
    }
    public enum EnumTamanioArreglo
    {
       UNA_DIMENSION,
       DOS_DIMENSION,
       TRES_DIMENSION,
    }
}
