using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _OLC1_Proyecto2_201701029.Entorno
{
    class Simbolo
    {
        public EnumTipoDato tipo;
        public String id;
        public Object valor;
        public Simbolo(EnumTipoDato tipo, Object valor, String id)
        {
            this.id = id;
            this.tipo = tipo;
            this.valor = valor;
        }
        public Object getValor() {
            return this.valor;
        }
        public void setValor(Object valor) {
            this.valor = valor;
        }
        public String getId()
        {
            return id;
        }
        public EnumTipoDato getTipo()
        {
            return this.tipo;
        }
        public enum EnumTipoDato {
            CHAR,
            STRING,
            INT,
            ARREGLO,
            DOUBLE,
            BOOLEAN,
            NULL,
            ERROR,
            OBJETO,
            FUNCION
        }
    }
}
