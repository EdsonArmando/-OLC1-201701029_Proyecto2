using Irony.Parsing;
using System;
using System.Collections.Generic;
using System.Linq;
using _OLC1_Proyecto2_201701029.Entorno;
using _OLC1_Proyecto2_201701029.Instruccion;
using System.Text;
using System.Threading.Tasks;
using _OLC1_Proyecto2_201701029.Expresion;
using System.Text.RegularExpressions;
using System.IO;

namespace _OLC1_Proyecto2_201701029.Analizadores
{
    class Sintactico
    {
        private LinkedList<Instruccion.Instruccion> listImport;
        public LinkedList<Instruccion.Instruccion> Analizar(String entrada)
        {
            Gramatica gramatica = new Gramatica();
            LanguageData lenguaje = new LanguageData(gramatica);
            Parser parser = new Parser(lenguaje);
            ParseTree arbol = parser.Parse(entrada);
            ParseTreeNode raiz = arbol.Root;
            LinkedList<Instruccion.Instruccion> AST = instrucciones(raiz.ChildNodes.ElementAt(0).ChildNodes.ElementAt(0));
            Entorno.Entorno ent = new Entorno.Entorno(null);
            if (listImport!=null) {
                foreach (Instruccion.Instruccion item in listImport) {
                    AST.AddFirst(item);
                }
            }
            foreach (Instruccion.Instruccion ins in AST)
            {
                ins.ejectuar(ent);
            }
            return AST;
        }
        public LinkedList<Instruccion.Instruccion> Importar(String entrada) {
            Gramatica gramatica = new Gramatica();
            LanguageData lenguaje = new LanguageData(gramatica);
            Parser parser = new Parser(lenguaje);
            ParseTree arbol = parser.Parse(entrada);
            ParseTreeNode raiz = arbol.Root;
            LinkedList<Instruccion.Instruccion> AST = instrucciones(raiz.ChildNodes.ElementAt(0).ChildNodes.ElementAt(0));
            
            return AST;
        }
        private LinkedList<String> listaParametros(ParseTreeNode actual) {
            LinkedList<String> parametros = new LinkedList<string>();
            for (int i=0; i < actual.ChildNodes.Count;i++) {
                ParseTreeNode temp = actual.ChildNodes.ElementAt(i);
                if (temp.ChildNodes.Count == 2) {
                    parametros.AddLast(temp.ChildNodes.ElementAt(1).ToString().Split(' ')[0]);
                }
                else {
                    parametros.AddLast(temp.ChildNodes.ElementAt(2).ToString().Split(' ')[0]);
                }
                
            }
            return parametros;
        }
        private LinkedList<Instruccion.Instruccion> instrucciones(ParseTreeNode actual)
        {
            LinkedList<Instruccion.Instruccion> instrucciones = new LinkedList<Instruccion.Instruccion>();
            for (int i = 0; i < actual.ChildNodes.Count; i++)
            {
                Instruccion.Instruccion nuevo = instruccion(actual.ChildNodes.ElementAt(i));
                if (nuevo != null)
                {
                    instrucciones.AddLast(nuevo);
                }
            }
            return instrucciones;
        }
        private LinkedList<Expresion.Expresion> acceso(ParseTreeNode actual) {
            
            if (actual.ChildNodes.Count == 3)
            {
                LinkedList<Expresion.Expresion> lista = acceso(actual.ChildNodes.ElementAt(0));
                lista.AddLast(new Expresion.Operacion(actual.ChildNodes.ElementAt(2).ChildNodes.ElementAt(0).ToString().Split(' ')[0], Operacion.Tipo_operacion.IDENTIFICADOR));
                return lista;
            }
            else 
            {
                LinkedList<Expresion.Expresion> lista = new LinkedList<Expresion.Expresion>();
                lista.AddLast(new Expresion.Operacion(actual.ChildNodes.ElementAt(0).ChildNodes.ElementAt(0).ToString().Split(' ')[0], Operacion.Tipo_operacion.IDENTIFICADOR));
                return lista;
            }

        }
        private Object accesoMetodos(ParseTreeNode actual)
        {
            LinkedList<Expresion.Expresion> listParametros = new LinkedList<Expresion.Expresion>();
            if (actual.ChildNodes.Count == 3)
            {
                LinkedList<String> lista = (LinkedList<String>)accesoMetodos(actual.ChildNodes.ElementAt(0));
                lista.AddLast(actual.ChildNodes.ElementAt(2).ChildNodes.ElementAt(0).ToString().Split(' ')[0]);
                return lista;
            } else if (actual.ChildNodes.Count == 0) {
                listParametros = devListExpresiones(actual.ChildNodes.ElementAt(2));
                return listParametros;
            }
            else
            {
                LinkedList<String> lista = new LinkedList<String>();
                lista.AddLast(actual.ChildNodes.ElementAt(0).ChildNodes.ElementAt(0).ToString().Split(' ')[0]);
                return lista;
            }

        }
        private Object accesolistParametrosObjetos(ParseTreeNode actual)
        {
            LinkedList<Expresion.Expresion> listParametros = new LinkedList<Expresion.Expresion>();
            if (actual.ChildNodes.Count == 3)
            {
                accesolistParametrosObjetos(actual.ChildNodes.ElementAt(0));
            }
            else if (actual.ChildNodes.Count == 4)
            {
                listParametros = devListExpresiones(actual.ChildNodes.ElementAt(2));
                return listParametros;
            }
            return null;
        }
        private Instruccion.Instruccion instruccion(ParseTreeNode actual)
        {
            string tokenOperador = actual.ChildNodes.ElementAt(0).ToString().Split(' ')[0];
            switch (tokenOperador)
            {
                /*
                 sumar(50,50);
                 */
                case "graph":
                    return new Graph(devExpresion(actual.ChildNodes.ElementAt(2)), devExpresion(actual.ChildNodes.ElementAt(4)));
                case "importar":
                    String Ruta = actual.ChildNodes.ElementAt(2).ChildNodes.ElementAt(0).ToString().Split(' ')[0];
                    string text = System.IO.File.ReadAllText(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"..\..\input", Ruta));
                    listImport = Importar(text);
                    return new Importar();
                case "acceso":
                    return new LlamadaFuncion(actual.ChildNodes.ElementAt(0).ChildNodes.ElementAt(0).ToString().Split(' ')[0], devListExpresiones(actual.ChildNodes.ElementAt(0).ChildNodes.ElementAt(2)));
                 
                case "class":
                    return new Clase(actual.ChildNodes.ElementAt(1).ToString().Split(' ')[0],instrucciones(actual.ChildNodes.ElementAt(3)));
                case "return":

                    return new Return(devExpresion(actual.ChildNodes.ElementAt(1)));
                case "function":
                    if (actual.ChildNodes.Count == 9)
                    {
                        return new Function(actual.ChildNodes.ElementAt(2).ToString().Split(' ')[0], listaParametros(actual.ChildNodes.ElementAt(4)), instrucciones(actual.ChildNodes.ElementAt(7)));
                    }
                    else {
                        LinkedList<Instruccion.Instruccion> nueva = instrucciones(actual.ChildNodes.ElementAt(6));
                      
                        return new Function(actual.ChildNodes.ElementAt(1).ToString().Split(' ')[0], listaParametros(actual.ChildNodes.ElementAt(3)),nueva);
                    }
           
                case "alert":
                    return new Alert(devListExpresiones(actual.ChildNodes.ElementAt(2)));
                case "for":
                    return new For(new Declaracion(Simbolo.EnumTipoDato.INT, actual.ChildNodes.ElementAt(3).ToString().Split(' ')[0], devExpresion(actual.ChildNodes.ElementAt(5))), expresion_logica(actual.ChildNodes.ElementAt(7)), 
                        new Aumento_Decremento(actual.ChildNodes.ElementAt(9).ToString().Split(' ')[0],"Aumento"), instrucciones(actual.ChildNodes.ElementAt(14)));
                case "do":
                    return new Do_While(expresion_logica(actual.ChildNodes.ElementAt(6)), instrucciones(actual.ChildNodes.ElementAt(2)));
                case "while":
                    return new Whilee(expresion_logica(actual.ChildNodes.ElementAt(2)), instrucciones(actual.ChildNodes.ElementAt(5)));
                case "break":
                    return new Breakk();
                case "default":
                    return new Case_Switch("DEFAULT", instrucciones(actual.ChildNodes.ElementAt(2)));
                case "case":
                    if (actual.ChildNodes.ElementAt(1).ChildNodes.Count == 3)
                    {
                        ParseTreeNode temp = actual.ChildNodes.ElementAt(1).ChildNodes.ElementAt(1);
                        string toekn = temp.ToString().Split(' ')[0];
                        return new Case_Switch(new Literal(Simbolo.EnumTipoDato.STRING, toekn), instrucciones(actual.ChildNodes.ElementAt(3)));
                    }
                    else {
                        ParseTreeNode temp = actual.ChildNodes.ElementAt(1).ChildNodes.ElementAt(0);
                        string toekn = temp.ToString().Split(' ')[0];
                        return new Case_Switch(new Literal(Simbolo.EnumTipoDato.STRING, toekn), instrucciones(actual.ChildNodes.ElementAt(3)));
                    }
                case "declaracion":
                    return instruccion(actual.ChildNodes.ElementAt(0));

                case "var":
                    Expresion.Expresion expresion = null;
                    Instruccion.Instruccion returnVal = null;
                    if (actual.ChildNodes.Count == 3)
                    {
                        if (actual.ChildNodes.Count == 3)
                        {
                            expresion = devExpresion(actual.ChildNodes.ElementAt(1));
                            return new Declaracion(Simbolo.EnumTipoDato.ARREGLO, actual.ChildNodes.ElementAt(1).ChildNodes.ElementAt(0).ToString().Split(' ')[0], expresion);
                        }
                        else {
                            returnVal = instruccion(actual.ChildNodes.ElementAt(3));
                        }

                    } else if (actual.ChildNodes.Count == 5) {
                        /*
                         *   4 nodos
                             ejemplo var nuevo = sumar(25,25);
                        */
                        if (actual.ChildNodes.ElementAt(3).ChildNodes.Count == 4)
                        {
                            returnVal = new LlamadaFuncion(actual.ChildNodes.ElementAt(3).ChildNodes.ElementAt(0).ToString().Split(' ')[0], devListExpresiones(actual.ChildNodes.ElementAt(3).ChildNodes.ElementAt(2)));

                        }
                        else {
                            expresion = devExpresion(actual.ChildNodes.ElementAt(3));
                        }

                    }
                    else if (actual.ChildNodes.Count == 8) {
                        if (actual.ChildNodes.ElementAt(3).Term.ErrorAlias.Equals("new"))
                        {
                            return new Objeto(nombre(actual.ChildNodes.ElementAt(1)), actual.ChildNodes.ElementAt(4).ToString().Split(' ')[0]);
                        }
                        else {
                            returnVal = new LlamadaFuncion(actual.ChildNodes.ElementAt(3).ToString().Split(' ')[0], devListExpresiones(actual.ChildNodes.ElementAt(5)));
                        }
                    } else if (actual.ChildNodes.Count == 7) {

                    }
                    else {

                        expresion = devExpresion(actual.ChildNodes.ElementAt(3));
                    }

                    if (returnVal == null)
                    {
                        LinkedList<Declaracion> listaDeclaracion = devDeclaracion(actual.ChildNodes.ElementAt(1), expresion);
                        return new Declaracion(expresion.tipo, listaDeclaracion);
                    }
                    else if (returnVal != null)
                    {
                        LinkedList<Declaracion> listaDeclaracion = devDeclaracion(actual.ChildNodes.ElementAt(1), null);
                        return new Declaracion(Simbolo.EnumTipoDato.FUNCION, listaDeclaracion, returnVal);
                    }
                    else {
                        return null;
                    }
                case "log":
                    LinkedList<Expresion.Expresion> listaExpresiones = devListExpresiones(actual.ChildNodes.ElementAt(2));
                    return new Imprimir(listaExpresiones);
                case "if":
                    if (actual.ChildNodes.Count == 7)
                    {
                        return new If((Operacion)devExpresion(actual.ChildNodes.ElementAt(2)),instrucciones(actual.ChildNodes.ElementAt(5)));
                    }
                    else {
                        return new If((Operacion)devExpresion(actual.ChildNodes.ElementAt(2)), instrucciones(actual.ChildNodes.ElementAt(5)), instrucciones(actual.ChildNodes.ElementAt(9)));
                    }
                case "switch":
                    if (actual.ChildNodes.Count==7) {
                        string tokenValor = actual.ChildNodes.ElementAt(2).ToString().Split(' ')[0];
                        return new Switch(new Id(tokenValor),instrucciones(actual.ChildNodes.ElementAt(5)));
                    }
                    break;
                default:
                    string tokenID = actual.ChildNodes.ElementAt(0).ToString().Split(' ')[0];
                    if (actual.ChildNodes.Count == 4 && actual.ChildNodes.ElementAt(2).ChildNodes.Count==4) {
                        /*
                         asignacion:
                         nuevo = sumar(50,50);
                       
                         */
                        return new Asignacion(actual.ChildNodes.ElementAt(0).ToString().Split(' ')[0], new LlamadaFuncion(actual.ChildNodes.ElementAt(2).ChildNodes.ElementAt(0).ToString().Split(' ')[0], devListExpresiones(actual.ChildNodes.ElementAt(2).ChildNodes.ElementAt(2))));
                    } else if (actual.ChildNodes.Count == 4) {
                        if (actual.ChildNodes.ElementAt(1).Term.ErrorAlias.Equals("+"))
                        {

                            return new Aumento_Decremento(tokenID, "Aumento");
                        }
                        else if (actual.ChildNodes.ElementAt(1).Term.ErrorAlias.Equals("-"))
                        {

                            return new Aumento_Decremento(tokenID, "Decremento");
                        }else if (actual.ChildNodes.ElementAt(2).Term.Name.Equals("listaAcceso"))
                        {
                            return new AccesoObj_MetodosFunciones(actual.ChildNodes.ElementAt(0).ToString().Split(' ')[0], (LinkedList<String>)accesoMetodos(actual.ChildNodes.ElementAt(2)), (LinkedList<Expresion.Expresion>)accesolistParametrosObjetos(actual.ChildNodes.ElementAt(2).ChildNodes.ElementAt(0)));

                        }
                        else
                        {
                            return new Asignacion(tokenID, expresion_numerica(actual.ChildNodes.ElementAt(2)));
                            //return new Asignacion(devExpresion(actual.ChildNodes.ElementAt(0)), expresion_numerica(actual.ChildNodes.ElementAt(2)));
                        }
                        
                    }
                    else if (actual.ChildNodes.Count == 5)
                    {
                        return new LlamadaFuncion(actual.ChildNodes.ElementAt(0).ToString().Split(' ')[0], devListExpresiones(actual.ChildNodes.ElementAt(2)));
                    }

                    if (actual.ChildNodes.Count == 6) {
                        return new Asignacion(tokenID, expresion_numerica(actual.ChildNodes.ElementAt(4)), (LinkedList<String>)accesoMetodos(actual.ChildNodes.ElementAt(2)));
                    } 
                    
                   
                    else {
                        return new Asignacion(tokenID, expresion_numerica(actual.ChildNodes.ElementAt(2)));
                    }
            }
            return null;
        }
        private String nombre(ParseTreeNode actual) {
            ParseTreeNode temp;
            for (int i = 0; i<actual.ChildNodes.Count;i++) {
                temp = actual.ChildNodes.ElementAt(i);
                return temp.ChildNodes.ElementAt(0).ChildNodes.ElementAt(0).ToString().Split(' ')[0];
            }

            return null;
        }
        private LinkedList<Expresion.Expresion> devListExpresiones(ParseTreeNode actual)
        {
            LinkedList<Expresion.Expresion> nueva = new LinkedList<Expresion.Expresion>();
            ParseTreeNode temp;
            if (actual.Term.Name.Equals("listaParametros")) {
                for (int i = 0; i < actual.ChildNodes.Count; i++) {
                    if (actual.ChildNodes.ElementAt(i).ChildNodes.Count == 1)
                    {
                        nueva.AddLast(devExpresion(actual.ChildNodes.ElementAt(i).ChildNodes.ElementAt(0)));
                    }
                    else {
                        nueva.AddLast(devExpresion(actual.ChildNodes.ElementAt(i).ChildNodes.ElementAt(1)));
                    }

                }
                return nueva;
            }
            for (int i = 0; i < actual.ChildNodes.Count; i++)
            {
                temp = actual.ChildNodes.ElementAt(i);
                BnfTerm tipo = temp.ChildNodes.ElementAt(0).ChildNodes.ElementAt(0).Term;
                if (tipo.ErrorAlias == "cadena")
                {
                    String tokenValor = temp.ChildNodes.ElementAt(0).ChildNodes.ElementAt(0).ToString();
                    nueva.AddLast(new Expresion.Literal(Simbolo.EnumTipoDato.STRING, tokenValor.Remove(tokenValor.ToCharArray().Length - 8, 8)));
                }
                else if (tipo.ErrorAlias == "ID")
                {
                    if (temp.ChildNodes.ElementAt(0).ChildNodes.Count != 3)
                    {
                        nueva.AddLast(new Expresion.Id(temp.ChildNodes.ElementAt(0).ChildNodes.ElementAt(0).ToString().Split(' ')[0]));
                    }
                    else {
                        String nombre = temp.ChildNodes.ElementAt(0).ChildNodes.ElementAt(0).ToString().Split(' ')[0];
                        nueva.AddLast(new AccesoObjeto(temp.ChildNodes.ElementAt(0).ChildNodes.ElementAt(0).ToString().Split(' ')[0], acceso(temp.ChildNodes.ElementAt(0).ChildNodes.ElementAt(2))));
                    }
                }
                else
                {

                    nueva.AddLast(expresion_numerica(temp.ChildNodes.ElementAt(0)));
                }
            }
            return nueva;
        }

        private LinkedList<Declaracion> devDeclaracion(ParseTreeNode actual, Expresion.Expresion expresion)
        {
            
            LinkedList<Declaracion> nueva = new LinkedList<Declaracion>();
            ParseTreeNode temp;
            for (int i = 0; i < actual.ChildNodes.Count; i++)
            {
                temp = actual.ChildNodes.ElementAt(i);
                if (i == 0)
                {
                    if (expresion == null)
                    {
                        nueva.AddLast(new Declaracion(Simbolo.EnumTipoDato.FUNCION, temp.ChildNodes.ElementAt(0).ChildNodes.ElementAt(0).ToString().Split(' ')[0], null));
                    }
                    else
                    {
                        nueva.AddLast(new Declaracion(expresion.tipo, temp.ChildNodes.ElementAt(0).ChildNodes.ElementAt(0).ToString().Split(' ')[0], expresion));
                    }
                    
                }
                else
                {
                    nueva.AddLast(new Declaracion(expresion.tipo, temp.ChildNodes.ElementAt(1).ChildNodes.ElementAt(0).ToString().Split(' ')[0], expresion));
                }
            }
            return nueva;
        }
        public Expresion.Expresion devExpresion(ParseTreeNode actual)
        {
            if (actual.ChildNodes.Count == 3)
            {
                try
                {
                    string tokenOperador = actual.ChildNodes.ElementAt(1).ToString().Split(' ')[0];
                    if (tokenOperador.Equals(">") || tokenOperador.Equals("<") || tokenOperador.Equals("==")
                         || tokenOperador.Equals("&&") || tokenOperador.Equals("||") || tokenOperador.Equals("^") || tokenOperador.Equals("!="))
                    {
                        return expresion_logica(actual);
                    } else
                    {
                        return expresion_numerica(actual);
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                    return expresion_logica(actual.ChildNodes.ElementAt(1));
                }
            }
            else if (actual.ChildNodes.Count == 1)
            {
                return expresion_numerica(actual);
            } else if (actual.ChildNodes.Count == 4) {
                return new Arreglo(devExpresion(actual.ChildNodes.ElementAt(2)),EnumTamanioArreglo.UNA_DIMENSION, actual.ChildNodes.ElementAt(0).ToString().Split(' ')[0]);
            }

            return null;
        }
        public Operacion expresion_logica(ParseTreeNode actual)
        {
            if (actual.ChildNodes.Count==1) {
                string tokenValor = actual.ChildNodes.ElementAt(0).ToString().Split(' ')[0];
                return new Operacion(tokenValor, Operacion.Tipo_operacion.IDENTIFICADOR);
            }
            string tokenOperador = actual.ChildNodes.ElementAt(1).ToString().Split(' ')[0];
            if (tokenOperador.Equals("<"))
            {
                return new Operacion(expresion_numerica(actual.ChildNodes.ElementAt(0)), expresion_numerica(actual.ChildNodes.ElementAt(2)), Operacion.Tipo_operacion.MENOR_QUE);
            }
            else if (tokenOperador.Equals("=="))
            {
                return new Operacion(expresion_numerica(actual.ChildNodes.ElementAt(0)), expresion_numerica(actual.ChildNodes.ElementAt(2)), Operacion.Tipo_operacion.IGUAL_QUE);
            }
            else if (tokenOperador.Equals("&&"))
            {
                return new Operacion(expresion_logica(actual.ChildNodes.ElementAt(0)), expresion_logica(actual.ChildNodes.ElementAt(2)), Operacion.Tipo_operacion.AND);
            }
            else if (tokenOperador.Equals("||"))
            {
                return new Operacion(expresion_logica(actual.ChildNodes.ElementAt(0)), expresion_logica(actual.ChildNodes.ElementAt(2)), Operacion.Tipo_operacion.OR);
            }
            else if (tokenOperador.Equals("^"))
            {
                return new Operacion(expresion_logica(actual.ChildNodes.ElementAt(0)), expresion_logica(actual.ChildNodes.ElementAt(2)), Operacion.Tipo_operacion.XOR);
            }
            else if (tokenOperador.Equals("!="))
            {
                return new Operacion(expresion_logica(actual.ChildNodes.ElementAt(0)), expresion_logica(actual.ChildNodes.ElementAt(2)), Operacion.Tipo_operacion.DIFERENTE);
            }
            else if (tokenOperador.Equals(">"))
            {
                return new Operacion(expresion_numerica(actual.ChildNodes.ElementAt(0)), expresion_numerica(actual.ChildNodes.ElementAt(2)), Operacion.Tipo_operacion.MAYOR_QUE);
            }
            else {
                return expresion_logica(actual.ChildNodes.ElementAt(1));
            }

        }
        public Operacion expresion_numerica(ParseTreeNode actual)
        {
            if (actual.ChildNodes.Count == 3)
            {
                string tokenOperador = actual.ChildNodes.ElementAt(1).ToString().Split(' ')[0];
                switch (tokenOperador)
                {
                    case ".":
                        if (actual.ChildNodes.ElementAt(2).ChildNodes.ElementAt(0).ChildNodes.Count == 4)
                        {
                            return new Operacion(new AccesoObjeto(actual.ChildNodes.ElementAt(0).ToString().Split(' ')[0], acceso(actual.ChildNodes.ElementAt(2)), devListExpresiones(actual.ChildNodes.ElementAt(2).ChildNodes.ElementAt(0).ChildNodes.ElementAt(2))), Operacion.Tipo_operacion.OBJETO);
                        }
                        else {
                            return new Operacion(new AccesoObjeto(actual.ChildNodes.ElementAt(0).ToString().Split(' ')[0], acceso(actual.ChildNodes.ElementAt(2))), Operacion.Tipo_operacion.OBJETO);
                        }

                    case "+":
                        return new Operacion(expresion_numerica(actual.ChildNodes.ElementAt(0)), expresion_numerica(actual.ChildNodes.ElementAt(2)), Operacion.Tipo_operacion.SUMA);
                    case "pow":
                        return new Operacion(expresion_numerica(actual.ChildNodes.ElementAt(0)), expresion_numerica(actual.ChildNodes.ElementAt(2)), Operacion.Tipo_operacion.POTENCIA);
                    case "-":
                        return new Operacion(expresion_numerica(actual.ChildNodes.ElementAt(0)), expresion_numerica(actual.ChildNodes.ElementAt(2)), Operacion.Tipo_operacion.RESTA);
                    case "*":
                        return new Operacion(expresion_numerica(actual.ChildNodes.ElementAt(0)), expresion_numerica(actual.ChildNodes.ElementAt(2)), Operacion.Tipo_operacion.MULTIPLICACION);
                    case "/":
                        return new Operacion(expresion_numerica(actual.ChildNodes.ElementAt(0)), expresion_numerica(actual.ChildNodes.ElementAt(2)), Operacion.Tipo_operacion.DIVISION);
                    default:
                        if (Regex.IsMatch(tokenOperador, @"^[a-zA-Z]+$") && tokenOperador != "expresion") {
                            return new Operacion(Encoding.ASCII.GetBytes(tokenOperador)[0]);
                        } else {
                            return expresion_numerica(actual.ChildNodes.ElementAt(1));
                        }
                }

            }
            else if (actual.ChildNodes.Count == 2)
            {
                return new Operacion(expresion_numerica(actual.ChildNodes.ElementAt(1)), Operacion.Tipo_operacion.NEGATIVO);
            }
            else if (actual.ChildNodes.Count == 4) {
                return new Operacion(new AccesoObjeto(actual.ChildNodes.ElementAt(0).ToString().Split(' ')[0], acceso(actual.ChildNodes.ElementAt(2)), devListExpresiones(actual.ChildNodes.ElementAt(2))), Operacion.Tipo_operacion.OBJETO);
            }
            else
            {
                string tokenOperador = actual.ChildNodes.ElementAt(0).ToString().Split(' ')[1];
                string tokenValor = actual.ChildNodes.ElementAt(0).ToString().Split(' ')[0];
                string prueba = actual.ChildNodes.ElementAt(0).ToString();
                string token = "";
                try {
                    token = prueba.Remove(0, prueba.ToCharArray().Length - 8);
                }
                catch (Exception e) { }
               
                if (token.Equals("(cadena)"))
                {
                    return new Operacion(prueba.Remove(prueba.ToCharArray().Length - 8, 8), Operacion.Tipo_operacion.CADENA);
                }
                else {
                    if (tokenOperador.Equals("(ID)"))
                    {
                        Console.WriteLine(tokenValor);
                        return new Operacion(tokenValor, Operacion.Tipo_operacion.IDENTIFICADOR);
                    }
                    else if (tokenOperador.Equals("(cadena)"))
                    {
                        return new Operacion(tokenValor, Operacion.Tipo_operacion.CADENA);
                    }
                }
                return new Operacion(Double.Parse(actual.ChildNodes.ElementAt(0).ToString().Split(' ')[0]));
            }
        }
    }   
}
