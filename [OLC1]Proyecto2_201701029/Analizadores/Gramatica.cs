using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Irony.Ast;
using Irony.Parsing;

namespace _OLC1_Proyecto2_201701029.Analizadores
{
    class Gramatica : Grammar
    {
        public Gramatica() {
            #region ER
            var numero = new NumberLiteral("Numero");
            StringLiteral tCadena = new StringLiteral("cadena", "\"");
            var tDecimal = new RegexBasedTerminal("Decimal", "[0-9]+'.'[0-9]+");
            IdentifierTerminal tId = new IdentifierTerminal("ID");
            CommentTerminal comentarioLinea = new CommentTerminal("comentarioLinea", "//", "\n", "\r\n"); //si viene una nueva linea se termina de reconocer el comentario.
            CommentTerminal comentarioBloque = new CommentTerminal("comentarioBloque", "/*", "*/");
            #endregion
            NonGrammarTerminals.Add(comentarioLinea);
            NonGrammarTerminals.Add(comentarioBloque);
            #region terminales
            var tEvaluar = ToTerm("Evaluar");
            var tVar = ToTerm("var");
            var tLog = ToTerm("log");
            var tPow = ToTerm("pow");
            var tAnd = ToTerm("&&");
            var tOr = ToTerm("||");
            var tXor = ToTerm("^");
            var tDifQ = ToTerm("!=");
            var tPuntoComa = ToTerm(";");
            var tParA = ToTerm("(");
            var tMayorQ = ToTerm(">");
            var tMenorQ = ToTerm("<");
            var tParC = ToTerm(")");
            var tComa = ToTerm(",");
            var tComillaSimple = ToTerm("'");
            var tIgual = ToTerm("=");
            var tNull = ToTerm("null");
            var tCorchA = ToTerm("[");
            var tCorchC = ToTerm("]");
            var tLlaveA = ToTerm("{");
            var tLlaveC = ToTerm("}");
            var tDosPuntos = ToTerm(":");
            var tDobleIgual = ToTerm("==");
            var tMas = ToTerm("+");
            var tMenos = ToTerm("-");
            var tPunto = ToTerm(".");
            var tPor = ToTerm("*");
            var tFor = ToTerm("for");
            var tDiv = ToTerm("/");
            var tIf = ToTerm("if");
            var tElse = ToTerm("else");
            var tSwitch = ToTerm("switch");
            var tCase = ToTerm("case");
            var tDefault = ToTerm("default");
            var tBreak = ToTerm("break");
            var tWhile = ToTerm("while");
            var tDo = ToTerm("do");
            var tAlert = ToTerm("alert");
            var tFunction = ToTerm("function");
            var tVoid = ToTerm("void");
            var tReturn = ToTerm("return");
            var tClass = ToTerm("class");
            var tNew = ToTerm("new");
            var tImportar = ToTerm("importar");
            var tGraph = ToTerm("graph");

            RegisterOperators(1, tMas, tMenos);
            RegisterOperators(2, tPor, tDiv, tPow);
            RegisterOperators(3, tMayorQ, tMenorQ);
            RegisterOperators(4, tOr, tAnd, tXor, tDifQ, tPunto);

            #endregion

            #region No Terminales
            NonTerminal ini = new NonTerminal("ini");
            NonTerminal instruccion = new NonTerminal("instruccion");
            NonTerminal casee = new NonTerminal("case");
            NonTerminal instrucciones = new NonTerminal("instrucciones");
            NonTerminal expresion = new NonTerminal("expresion");
            NonTerminal listInstr = new NonTerminal("listInstr");
            NonTerminal listVar = new NonTerminal("listVar");
            NonTerminal variable = new NonTerminal("variable");
            NonTerminal parametro = new NonTerminal("parametro");
            NonTerminal listExpr = new NonTerminal("listExpr");
            NonTerminal listCase = new NonTerminal("listCase");
            NonTerminal listFor = new NonTerminal("listFor");
            NonTerminal declaracion = new NonTerminal("declaracion");
            NonTerminal nuevo = new NonTerminal("nuevo");
            NonTerminal forr = new NonTerminal("for");
            NonTerminal listParam = new NonTerminal("listaParametros");
            NonTerminal List_acceso = new NonTerminal("listaAcceso");
            NonTerminal acceso = new NonTerminal("acceso");
            NonTerminal arreglo = new NonTerminal("arreglo");

            #endregion

            #region Gramatica
            ini.Rule = instrucciones;

            instrucciones.Rule = listInstr;

            listInstr.Rule = MakeStarRule(listInstr, instruccion);

            instruccion.Rule = tEvaluar + tCorchA + expresion + tCorchC + tPuntoComa
                    | declaracion
                    | tLog + tParA + listExpr + tParC + tPuntoComa
                    | tIf + tParA + expresion + tParC + tLlaveA + listInstr + tLlaveC
                    | tIf + tParA + expresion + tParC + tLlaveA + listInstr + tLlaveC + tElse + tLlaveA + listInstr + tLlaveC
                    | tSwitch + tParA + tId + tParC + tLlaveA + listCase + tLlaveC
                    | tBreak + tPuntoComa
                    | tGraph + tParA + expresion + tComa + expresion + tParC + tPuntoComa
                    | tId + tIgual + expresion + tPuntoComa
                    | tImportar + tParA + expresion + tParC + tPuntoComa
                    //| expresion + tIgual + expresion + tPuntoComa
                    | tWhile + tParA + expresion + tParC + tLlaveA + listInstr + tLlaveC
                    | tDo + tLlaveA + listInstr + tLlaveC + tWhile + tParA + expresion + tParC + tPuntoComa
                    | tId + tMas + tMas + tPuntoComa
                    | tId + tMenos + tMenos + tPuntoComa
                    | tFor + tParA + tVar + tId + tIgual + expresion + tPuntoComa + expresion + tPuntoComa + tId + tMas + tMas + tParC + tLlaveA + listInstr + tLlaveC
                    | tAlert + tParA + listExpr + tParC + tPuntoComa
                    | tFunction + tVoid + tId + tParA + listParam + tParC + tLlaveA + listInstr + tLlaveC
                    | tId + tParA + listParam + tParC + tPuntoComa
                    //| tId + tIgual + acceso
                    | tReturn + expresion + tPuntoComa
                    | tFunction + tId + tParA + listParam + tParC + tLlaveA + listInstr + tLlaveC
                    | tClass + tId + tLlaveA + listInstr + tLlaveC
                    | tId + tPunto + List_acceso + tPuntoComa 
                    | tId + tPunto + List_acceso + tIgual + expresion + tPuntoComa
                    ;
            //acceso.Rule = tId + tParA + listParam + tParC + tPuntoComa
                ;
            List_acceso.Rule = List_acceso + tPunto + expresion
                        | expresion
                        //| expresion + tParA + listParam + tParC + tPuntoComa;
                        ;

            declaracion.Rule = tVar + listVar + tIgual + expresion + tPuntoComa
                    | tVar + listVar + tIgual + tNew + tId + tParA + tParC + tPuntoComa
                    | tVar + arreglo + tPuntoComa
                    //| tVar + listVar + tIgual + tId + tPunto + expresion +  tPuntoComa
                    ;
            instruccion.ErrorRule = SyntaxError + ";"
                        ;
            listCase.Rule = MakeStarRule(listCase,casee);
            casee.Rule = tCase + expresion + tDosPuntos + listInstr
                    | tDefault + tDosPuntos + listInstr 
                ;
            listExpr.Rule = MakeStarRule(listExpr,variable);

            listVar.Rule = MakeStarRule(listVar, variable);
            variable.Rule = tComa + expresion
                    | expresion;

            listParam.Rule = MakeStarRule(listParam, parametro);
            parametro.Rule = tComa + tVar + tId
                    | tVar + tId
                    | tComa + expresion
                    | expresion
                    ;

            arreglo.Rule = tId + tCorchA + expresion + tCorchC
                    | tId + tCorchA + expresion + tCorchC + tCorchA + expresion + tCorchC
                    | tId + tCorchA + expresion + tCorchC + tCorchA + expresion + tCorchC + tCorchA + expresion + tCorchC
                    ;

            expresion.Rule = tMenos + expresion
                    | expresion + tMas + expresion
                    | expresion + tMenos + expresion
                    | expresion + tPor + expresion
                    | expresion + tPow + expresion
                    | expresion + tDiv + expresion
                    | expresion + tMenorQ + expresion
                    | expresion + tMayorQ + expresion
                    | expresion + tDobleIgual + expresion
                    | expresion + tOr + expresion
                    | expresion + tAnd + expresion
                    | expresion + tXor + expresion
                    | expresion + tDifQ + expresion
                    | tId + tParA + listParam + tParC
                    | numero
                    //| tId + tCorchA + expresion + tCorchC
                    //| tId + tCorchA + expresion + tCorchC + tCorchA + expresion + tCorchC
                    //| tId + tCorchA + expresion + tCorchC + tCorchA + expresion + tCorchC + tCorchA + expresion + tCorchC
                    | tComillaSimple + tId + tComillaSimple   
                    | tCadena
                    | tId + tPunto + List_acceso
                    | tId
                    | tParA + expresion + tParC;

            #endregion

            #region Preferencias
            this.Root = ini;
            #endregion
        }
    }
}
