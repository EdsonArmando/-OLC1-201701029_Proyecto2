using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _OLC1_Proyecto2_201701029.Entorno
{
    class Entorno 
    {
        public Hashtable tabla;
        public LinkedList<Clase> listClases;
        Entorno anterior;
        public Entorno(Entorno ent) : base()
        {
            this.tabla = new Hashtable();
            this.listClases = new LinkedList<Clase>();

            // llamada del constructor de la clase padre
        }


        public void insertClase(String nombre, Clase clase) {
            this.listClases.AddLast(clase);
        }
        public Clase obtenerClase(String nombre) {
            foreach (Clase item in listClases)
            {
                if (item.nombre.Equals(nombre)) {
                    return item;
                }

            }
            return null;
        }
        public bool existeVariable(String id) {
            return this.tabla.ContainsKey(id);
        }
        public Simbolo obtener(string id)
        {

            if (tabla.ContainsKey(id))
            {
                Simbolo sim = (Simbolo)tabla[id];
                return sim;
            }
            Form1.salidaConsola.AppendText("La variable '" + id + "' NO existe");
            return null;
        }
        public void Insertar(string nombre, Simbolo valor) {
            if (this.tabla.ContainsKey(nombre))
            {
                Console.WriteLine("La variable ya existe");
                this.tabla.Remove(nombre);
                this.tabla.Add(nombre, valor);
                return;
            }
            this.tabla.Add(nombre, valor);

        }
    }
}
