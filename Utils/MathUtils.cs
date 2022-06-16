using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Software_de_Producción.Utils
{
    public class MathUtils
    {
        public static double detectandoDecimales(double valor)
        {
            double retornaValor = valor;
            double remainder = valor % 1;
            if (remainder != 0)
            {
                retornaValor = retornaValor + 1;

            }
            return retornaValor;
        }

    }

}
