using _OLC1_Proyecto2_201701029.Entorno;
using _OLC1_Proyecto2_201701029.Expresion;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _OLC1_Proyecto2_201701029.Instruccion
{
    class Asignacion : Instruccion
    {
        String id;
        Operacion valor;
        LinkedList<String> listAcceso;
        Operacion posicion;
        Expresion.Expresion arreglo;
        Instruccion llamadaFuncion;
        public Asignacion(string id, Operacion valor) {
            this.id = id;
            this.valor = valor;
        }
        public Asignacion(string id, Instruccion valor) {
            this.id = id;
            this.llamadaFuncion = valor;
        }
        public Asignacion(Expresion.Expresion arreglo, Operacion valor)
        {
            this.arreglo = arreglo;
            this.valor = valor;
            this.posicion = valor;
        }

        public Asignacion(string idObjeto, Operacion valorNuevo, LinkedList<String> listAcceso)
        {
            this.id = idObjeto;
            this.valor = valorNuevo;
            this.listAcceso = listAcceso;
        }

        public override Retornar ejectuar(Entorno.Entorno ent)
        {
            if (listAcceso != null)
            {
                Simbolo sim = ent.obtener(id);
                Objeto obj = (Objeto)sim.valor;
                foreach (String exp in listAcceso) {
                    
                    obj.entObjeto.Insertar(exp, new Simbolo(valor.obtenerValor(obj.entObjeto).tipo, valor.obtenerValor(obj.entObjeto).valor, exp));
                }

            }
            if (arreglo!=null) {
                if (arreglo.GetType() == typeof(Arreglo))
                {
                    Arreglo local = (Arreglo)arreglo;
                    Simbolo sim = ent.obtener(local.nombreVariable);
                    Arreglo arr = (Arreglo)sim.valor;
                    arr.valores[Int32.Parse(local.tamanioArreglo.obtenerValor(ent).valor.ToString())] = valor.obtenerValor(ent);
                    ent.Insertar(arr.nombreVariable, new Simbolo(Simbolo.EnumTipoDato.ARREGLO, arr, arr.nombreVariable));
                }
            }
            if (llamadaFuncion!=null) {
                Retornar result = llamadaFuncion.ejectuar(ent);
                Object valo = result.valGuardado;
                ent.Insertar(id, new Simbolo(result.valor.tipo, valo, id));
            }
            if(listAcceso == null && arreglo == null && llamadaFuncion == null)
            {
                ent.Insertar(id, new Simbolo(valor.obtenerValor(ent).tipo, valor.obtenerValor(ent).valor, id));
            }
            
            return new Retornar();
        }
    }
}
