using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using _OLC1_Proyecto2_201701029.Entorno;

namespace _OLC1_Proyecto2_201701029.Instruccion
{
    class Graph : Instruccion
    {
        Expresion.Expresion nombre;
        Expresion.Expresion contenido;
        public Graph(Expresion.Expresion nombre, Expresion.Expresion contenido) {
            this.nombre = nombre;
            this.contenido = contenido;
        }
        public override Retornar ejectuar(Entorno.Entorno ent)
        {
            Object nombreImagen = nombre.obtenerValor(ent).valor;
            Object contenidoImagen = contenido.obtenerValor(ent).valor;
            StreamWriter writer = new StreamWriter("File1.txt");
            writer.Write(Convert.ToString(contenidoImagen));
            writer.Close();

            return new Retornar();
        }
    }
}
