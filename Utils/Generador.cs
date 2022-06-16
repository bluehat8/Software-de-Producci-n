using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Software_de_Producción.Utils
{
    public class Generador
    {
        public List<Punto> Puntos { get; set; }

        public List<Punto> GeneradorDatos(double limiteInferior, double limiteSuperior,double x1, double y1, double x2, double y2, double incremento)
         {
             Puntos = new List<Punto>();
             for (double x = limiteInferior;  x<=limiteSuperior;x+=incremento)
             {
                Puntos.Add(new Punto(x, generadorRecta(x1, y1, x2, y2, x)));
             }

            return Puntos;
         }


        public List<Punto> GeneradorDatosRectaHorizontal(double limiteInferior, double limiteSuperior, double x1, double y1, double x2, double y2, double incremento)
        {
            Puntos = new List<Punto>();
            for (double x = limiteInferior; x <= limiteSuperior; x += incremento)
            {
               Puntos.Add(new Punto(x, generadorRectaHorizontal(x1, y1, x2, y2, x)));
            }

            return Puntos;
        }


        public List<Punto> GeneradorDatosRectaVertical(double limiteInferior, double limiteSuperior, double x1, double y1, double x2, double y2, double incremento)
        {
            Puntos = new List<Punto>();
            for (double x = limiteInferior; x <= limiteSuperior; x += incremento)
            {
                Puntos.Add(new Punto(generadorRectaVertical(x1, y1, x2, y2, x),x));
            }

            return Puntos;
        }




        public double calcularPendiente(double x1, double y1, double x2, double y2)
        {
            return (y2 - y1) / (x2 - x1);
        }


        public double generadorRecta(double x1, double y1, double x2, double y2, double incremento)
        {
            double m = calcularPendiente(x1, y1, x2, y2);
            return ((m) * incremento) - (m * x1) + y1;
        }

        public double generadorRectaHorizontal(double x1, double y1, double x2, double y2, double incremento)
        {
            return x1;
        }

        public double generadorRectaVertical(double x1, double y1, double x2, double y2, double incremento)
        {
            return y1;
        }
    }

   
}
