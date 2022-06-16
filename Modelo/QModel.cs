using Software_de_Producción.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.UI.DataVisualization.Charting;

namespace Software_de_Producción.Modelo
{
    public class QModel
    {

        public QModel() { }
        public double calcularPedidoOptimoSinFaltante(double demandaAnual, double CostoPedir, double politicaAlmacen, double costoCompra, double costoAlmacen)
        {
            if (costoAlmacen == 0)
            {
                politicaAlmacen = politicaAlmacen / 100;
                return (int)Math.Floor(MathUtils.detectandoDecimales(Math.Sqrt((demandaAnual * 2 * CostoPedir) / (politicaAlmacen * costoCompra))));
            }
            else
            {
                return (int)Math.Floor(MathUtils.detectandoDecimales(Math.Sqrt((demandaAnual * 2 * CostoPedir) / (costoAlmacen))));
            }
        }


        public double NumeroPedidos(double qoptimo, int demandaAnual)
        {
            double division = demandaAnual / qoptimo;
            return (int)Math.Floor(MathUtils.detectandoDecimales(division));
        }


        public double TiempoEntrePedidos(double NPedidos)
        {
            return 360 / NPedidos;
        }

        public double ROPSinZ(double demandaDiaria, int plazoEntrega)
        {
            return (double)Math.Floor(MathUtils.detectandoDecimales((demandaDiaria * plazoEntrega)));
        }

        public double RopConZ(double demandaDiaria, int plazoEntrega, double z, int desv)
        {
            Chart Chart1 = new Chart();
            double result = Chart1.DataManipulator.Statistics.InverseNormalDistribution(z/100);
            double zL = (desv * Math.Sqrt(plazoEntrega)) * result;
            return (double)Math.Floor(MathUtils.detectandoDecimales( (demandaDiaria * plazoEntrega)+zL));

        }



        public double calcularDemandadiaria(int demandaAnual, int diasLab)
        {
            return (double)demandaAnual / diasLab;
        }

        public double costoTotal(int demandaAnual, double costoPedir, double costoMtto, int qoptimo)
        {
            return ((demandaAnual * costoPedir)/qoptimo) + ((qoptimo * costoMtto) / 2);
        }

        public double calcularCostoMtto(double politica, double costoCompra)
        {

            return (double)politica * costoCompra;
        }


        public string obtenerAnalisis(double qotimo, double rop, double pedidos, double tiempoEntrePedidos)
        {
            return "La política de decisión de inventarios del sistema Q consiste en colocar un pedido de " + qotimo + " unidades siempre que la posición de\n" +
                "las existencias caiga a " + rop + "\n" +
                "En promedio se levantarán " + pedidos + " pedidos al año y un tiempo entre pedidos de " + tiempoEntrePedidos;
        }

    }
}
