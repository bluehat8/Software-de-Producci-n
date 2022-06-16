using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;


namespace Software_de_Producción.Utils
{
    public class CultureInfoDecimals
    {
        public static double convertCultureDecimals(TextBox Peso)
        {
            double num = double.Parse(Peso.Text.ToString(), CultureInfo.CreateSpecificCulture("en-GB"));
           // return num;
            return Truncate(num, 10);
        }

        public static double convertCultureDecimalsDouble(double numero)
        {
            double num = double.Parse(numero.ToString(), CultureInfo.CreateSpecificCulture("en-GB"));
            return num;
        }

        public static double Truncate(double value, int decimales)
        {
            double aux_value = Math.Pow(10, decimales);
            return (Math.Truncate(value * aux_value) / aux_value);
        }
    }
}
