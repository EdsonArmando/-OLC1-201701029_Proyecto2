using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using _OLC1_Proyecto2_201701029.Entorno;

namespace _OLC1_Proyecto2_201701029.Expresion
{
    class Operacion : Expresion
    {
        private Operacion operadorDer;
        private Operacion operadorIzq;
        private Object valor;
        private Expresion objeto;
        private Tipo_operacion tipo;
        public Operacion(Operacion operadorIzq, Operacion operadorDer, Tipo_operacion tipo)
        {
            this.tipo = tipo;
            this.operadorIzq = operadorIzq;
            this.operadorDer = operadorDer;
        }
        public Operacion(Operacion operadorIzq, Tipo_operacion tipo)
        {
            this.tipo = tipo;
            this.operadorIzq = operadorIzq;
        }
        public Operacion(String a, Tipo_operacion tipo)
        {
            this.valor = a;
            this.tipo = tipo;
        }
        public Operacion(Expresion a, Tipo_operacion tipo)
        {
            this.objeto = a;
            this.tipo = tipo;
        }
        public Operacion(Double a)
        {
            this.valor = a;
            this.tipo = Tipo_operacion.NUMERO;
        }

        public override Simbolo.EnumTipoDato getTipo()
        {
            throw new NotImplementedException();
        }

        public override Expresion obtenerValor(Entorno.Entorno ent)
        {
            if (tipo == Tipo_operacion.DIVISION)
            {
                return new Literal(Simbolo.EnumTipoDato.DOUBLE, (Double)operadorIzq.obtenerValor(ent).valor / (Double)operadorDer.obtenerValor(ent).valor);
            }
            else if (tipo == Tipo_operacion.MULTIPLICACION)
            {
                Double izquierda = (Double)operadorIzq.obtenerValor(ent).valor;
                Double derecha = (Double)operadorDer.obtenerValor(ent).valor;
                Console.WriteLine(izquierda + "*" + derecha);
                return new Literal(Simbolo.EnumTipoDato.DOUBLE,izquierda * derecha);
            }
            else if (tipo == Tipo_operacion.RESTA)
            {
                return new Literal(Simbolo.EnumTipoDato.DOUBLE, (Double)operadorIzq.obtenerValor(ent).valor - (Double)operadorDer.obtenerValor(ent).valor);
            }
            else if (tipo == Tipo_operacion.POTENCIA)
            {
                Double izquierda = (Double)operadorIzq.obtenerValor(ent).valor;
                Double derecha = (Double)operadorDer.obtenerValor(ent).valor;
                return new Literal(Simbolo.EnumTipoDato.DOUBLE, Math.Pow(izquierda,derecha));
            }
            else if (tipo == Tipo_operacion.SUMA)
            {   
                try {
                    Double izquierda = (Double)operadorIzq.obtenerValor(ent).valor;
                    Double derecha = (Double)operadorDer.obtenerValor(ent).valor;
                    return new Literal(Simbolo.EnumTipoDato.DOUBLE, izquierda + derecha);
                }
                catch (Exception e) {
                    return new Literal(Simbolo.EnumTipoDato.STRING, operadorIzq.obtenerValor(ent).valor.ToString() + operadorDer.obtenerValor(ent).valor.ToString());
                }

            }
            else if (tipo == Tipo_operacion.NEGATIVO)
            {
                return new Literal(Simbolo.EnumTipoDato.DOUBLE, (Double)operadorIzq.obtenerValor(ent).valor * -1);
            }
            else if (tipo == Tipo_operacion.NUMERO)
            {
                return new Literal(Simbolo.EnumTipoDato.DOUBLE, Double.Parse(valor.ToString()));
            }
            else if (tipo == Tipo_operacion.IDENTIFICADOR)
            {
                Simbolo sim = ent.obtener(valor.ToString());
                if (sim.tipo == Simbolo.EnumTipoDato.FUNCION) {
                    return new Literal(sim.tipo, sim.valor);
                } else if (sim.tipo == Simbolo.EnumTipoDato.OBJETO) {
                    return new Literal(sim.tipo, sim.valor);
                }
                else {
                    try
                    {
                        return new Literal(Simbolo.EnumTipoDato.DOUBLE, Double.Parse(sim.getValor().ToString()));
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine(e);
                        return new Literal(Simbolo.EnumTipoDato.DOUBLE, sim.getValor().ToString());
                    }
                }
            } else if (tipo == Tipo_operacion.OBJETO) {

                return objeto.obtenerValor(ent);
}
            else if (tipo == Tipo_operacion.CADENA)
            {
                return new Literal(Simbolo.EnumTipoDato.DOUBLE, valor.ToString());
            }
            else if (tipo == Tipo_operacion.MAYOR_QUE)
            {

                return new Literal(Simbolo.EnumTipoDato.DOUBLE, (Double)operadorIzq.obtenerValor(ent).valor > (Double)operadorDer.obtenerValor(ent).valor);
            }
            else if (tipo == Tipo_operacion.AND)
            {
                return new Literal(Simbolo.EnumTipoDato.BOOLEAN, bool.Parse(operadorIzq.obtenerValor(ent).valor.ToString()) && bool.Parse(operadorDer.obtenerValor(ent).valor.ToString()));
            }
            else if (tipo == Tipo_operacion.OR)
            {
                return new Literal(Simbolo.EnumTipoDato.BOOLEAN, bool.Parse(operadorIzq.obtenerValor(ent).valor.ToString()) || bool.Parse(operadorDer.obtenerValor(ent).valor.ToString()));
            }
            else if (tipo == Tipo_operacion.XOR)
            {
                return new Literal(Simbolo.EnumTipoDato.BOOLEAN, bool.Parse(operadorIzq.obtenerValor(ent).valor.ToString()) ^ bool.Parse(operadorDer.obtenerValor(ent).valor.ToString()));
            }
            else if (tipo == Tipo_operacion.DIFERENTE)
            {
                Double izquierda = (Double)operadorIzq.obtenerValor(ent).valor;
                Double derecha = (Double)operadorDer.obtenerValor(ent).valor;
                return new Literal(Simbolo.EnumTipoDato.BOOLEAN, izquierda != derecha);
            }
            else if (tipo == Tipo_operacion.IGUAL_QUE)
            {
                return new Literal(Simbolo.EnumTipoDato.DOUBLE, (Double)operadorIzq.obtenerValor(ent).valor == (Double)operadorDer.obtenerValor(ent).valor);
            }
            else if (tipo == Tipo_operacion.MENOR_QUE)
            {
                return new Literal(Simbolo.EnumTipoDato.DOUBLE, (Double)operadorIzq.obtenerValor(ent).valor < (Double)operadorDer.obtenerValor(ent).valor);
            }
            else
            {
                return null;
            }
        }
        public enum Tipo_operacion
        {
            SUMA,
            RESTA,
            AND,
            OR,
            XOR,
            OBJETO,
            DIFERENTE,
            MULTIPLICACION,
            DIVISION,
            NEGATIVO,
            NUMERO,
            LETRA,
            IDENTIFICADOR,
            CADENA,
            MAYOR_QUE,
            MENOR_QUE,
            IGUAL_QUE,
            POTENCIA,
            CONCATENACION
        }
    }
}
