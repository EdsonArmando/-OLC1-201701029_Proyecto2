using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using _OLC1_Proyecto2_201701029.Entorno;

namespace _OLC1_Proyecto2_201701029.Instruccion
{
    class Objeto : Instruccion
    {
        String nombreObjeto;
        String nombreClase;
        public Entorno.Entorno entObjeto;
        public Objeto(String nombre, String nombreClase) {
            this.nombreObjeto = nombre;
            this.nombreClase = nombreClase;
            this.entObjeto = new Entorno.Entorno(null);
        }
        public override Retornar ejectuar(Entorno.Entorno ent)
        {
            entObjeto.tabla = ent.tabla;
            /*foreach (Simbolo item in ent.listSimbolo)
            {
                entObjeto.listSimbolo.AddLast(item);
            }*/
            Clase clase = ent.obtenerClase(nombreClase);
            if (clase == null)
            {
                Form1.salidaConsola.AppendText("La clase no existe");
                return null;
            }
            else {
                foreach (Instruccion  item in clase.listInstrucciones) {
                    item.ejectuar(entObjeto);
                }
            }

            ent.Insertar(nombreObjeto,new Simbolo(Simbolo.EnumTipoDato.OBJETO,this, nombreObjeto));
            entObjeto.tabla = ent.tabla;
            /*foreach (Simbolo item in ent.listSimbolo)
            {
                entObjeto.listSimbolo.AddLast(item);
            }*/
            return new Retornar();
        }
    }
}
