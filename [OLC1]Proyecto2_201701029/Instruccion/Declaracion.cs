using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using _OLC1_Proyecto2_201701029.Entorno;

namespace _OLC1_Proyecto2_201701029.Instruccion
{
    class Declaracion : Instruccion
    {
        string nombreVariable;
        Expresion.Expresion valor;
        Simbolo.EnumTipoDato tipoVariable;
        LinkedList<Declaracion> lista;
        Instruccion llamadaFuncion;
        public Declaracion(Simbolo.EnumTipoDato tipo, string nombre, Expresion.Expresion expresion) {
            this.tipoVariable = tipo;
            this.nombreVariable = nombre;
            this.valor = expresion;
        }
        public Declaracion(Simbolo.EnumTipoDato tipo, LinkedList<Declaracion> lista, Instruccion expresion)
        {
            this.tipoVariable = tipo;
            this.lista = lista;
            this.llamadaFuncion = expresion;
        }

        public Declaracion(Simbolo.EnumTipoDato tipo,LinkedList<Declaracion> lista)
        {
            this.tipoVariable = tipo;
            this.lista = lista;
        }
        public override Retornar ejectuar(Entorno.Entorno ent)
        {
            Expresion.Expresion resultado = null;
            Declaracion declaracion = null;
            if (lista != null)
            {
                foreach (Declaracion decla in lista)
                {
                    declaracion = decla;
                    if (decla.tipoVariable == Simbolo.EnumTipoDato.FUNCION)
                    {
                        Retornar result = llamadaFuncion.ejectuar(ent);
                        Object valo = result.valGuardado;

                        //ent.Insertar(decla.nombreVariable, new Simbolo(Simbolo.EnumTipoDato.FUNCION, valo, decla.nombreVariable));
                        ent.Insertar(decla.nombreVariable, new Simbolo(Simbolo.EnumTipoDato.DOUBLE, valo, decla.nombreVariable));
                    } else if (decla.tipoVariable == Simbolo.EnumTipoDato.ARREGLO) {
                        ent.Insertar(decla.nombreVariable, new Simbolo(Simbolo.EnumTipoDato.FUNCION, decla.valor.obtenerValor(ent).valor, decla.nombreVariable));
                    }

                    else {

                        resultado = decla.valor.obtenerValor(ent);
                        if (resultado != null)
                        {

                            ent.Insertar(decla.nombreVariable, new Simbolo(this.tipoVariable, resultado.valor, decla.nombreVariable));
                            //Form1.salidaConsola.AppendText("Variable: " + decla.nombreVariable + " ingresada correctamente\n");
                        }
                        else
                        {
                            Form1.salidaConsola.AppendText("Existio un error\n");
                        }
                    }
                    
                }
            }
            else {
                if (tipoVariable == Simbolo.EnumTipoDato.ARREGLO) {

                    ent.Insertar(nombreVariable, new Simbolo(Simbolo.EnumTipoDato.ARREGLO, valor.obtenerValor(ent), nombreVariable));
                }
                else {
                    ent.Insertar(nombreVariable, new Simbolo(Simbolo.EnumTipoDato.INT, valor.obtenerValor(ent).valor, nombreVariable));
                }
                
            }
            return new Retornar();
        }
    }
}
