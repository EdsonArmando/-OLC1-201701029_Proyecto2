using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _OLC1_Proyecto2_201701029.Instruccion
{
    abstract class Instruccion
    {
        abstract public Retornar ejectuar(Entorno.Entorno ent);
        public enum EnumTipoSentencia
        {
            DECLARACION,
            IMPRIMIR,
            REASIGNACION,
            COMPONENTE,
            LEERARCHIVO,
            GRAFICAR
        }
    }
    
}
