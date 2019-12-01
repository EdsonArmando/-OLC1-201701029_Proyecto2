using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using _OLC1_Proyecto2_201701029.Instruccion;

namespace _OLC1_Proyecto2_201701029.Entorno
{
    class Clase : Instruccion.Instruccion
    {
        public String nombre;
        public LinkedList<Instruccion.Instruccion> listInstrucciones;
        public Clase(String nombre, LinkedList<Instruccion.Instruccion> lista) {
            this.nombre = nombre;
            this.listInstrucciones = lista;
        }
        public override Retornar ejectuar(Entorno ent)
        {

            ent.insertClase(nombre,this);
            return new Retornar();
        }
    }
}
