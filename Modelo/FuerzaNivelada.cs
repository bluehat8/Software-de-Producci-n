using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Software_de_Producción.Modelo
{
    public class FuerzaNivelada
    {
        public string periodo { get; set; }
        public int inventario_inicial { get; set; }
        public double ProducReal { get; set; }
        public int Demanda { get; set; }
        public int InventarioFinal { get; set; }
        public int CostoFaltante { get; set; }
        public double StockSeguridad { get; set; }
        public double costoH { get; set; }
        public double costoTN { get; set; }
        public double costo_despido_total { get; set; }
        public double costo_total { get; set; }


        public FuerzaNivelada() { }




        public List<FuerzaNivelada> calcularFuerzaNivelada(List<PlanAgregado> lst, double costoMP, double costoH, double cf, double costoCont, double costoDespido, double costoTN, double tiempoReqUnidad, int invInicial, double ss, int fuerzaInicial)
        {
            double tasa = ss / 100;
            List<FuerzaNivelada> listaP = new List<FuerzaNivelada>();
            FuerzaNivelada obj = new FuerzaNivelada();

            double trabajadoresConstantes = (lst.Sum(item => item.demanda) * tiempoReqUnidad) / (lst.Sum(item => item.diasLaborales) * 8);
            int diasDisponibles = 0;
            int contador = lst.Count;
            int contadorPlan = 1;
            int demanda = 0;
            double stockS;

            while (contador > 0)
            {
                /*obj = new FuerzaNivelada();
                
                diasDisponibles = lst.ElementAt(contadorPlan - 1).diasLaborales;

                demanda = lst.ElementAt(contadorPlan - 1).demanda;

                obj.periodo = lst.ElementAt(contadorPlan - 1).periodo;

                obj.inventario_inicial=


                stockS = tasa * demanda;
                if (contador == lst.Count)
                {
                    obj.ProducRequerida = (demanda - invInicial + stockS);
                    obj.inventario_inicial = invInicial;
                }
                else if (contador != lst.Count)
                {
                    obj.inventario_inicial = (int)listaP.ElementAt(listaP.Count - 1).inventario_final;
                    obj.ProducRequerida = (demanda - obj.inventario_inicial + stockS);
                }

                obj.Horas_disponibles = obj.Dias_habiles * 8;
                obj.Horas_requeridas = (int)(obj.ProducRequerida * tiempoReqUnidad);
                double trab = (double)Math.Floor(detectandoDecimales((double)obj.Horas_requeridas / obj.Horas_disponibles));
                obj.Trabajadores = Math.Round((trab), MidpointRounding.AwayFromZero);
                obj.inventario_final = (tasa * demanda);

                obj.costo_mp_total = (obj.ProducRequerida * costoMP);
                obj.costoH = (stockS) * costoH;
                obj.costo_md_total = (obj.Horas_requeridas * costoTN);

                //Despedir

                if (contador == lst.Count)
                {
                    fuerzaInicial = fuerzaInicial + 0;
                }

                if (obj.Trabajadores < fuerzaInicial)
                {
                    obj.costo_despido_total = ((fuerzaInicial - obj.Trabajadores) * costoDespido);
                    fuerzaInicial = (int)obj.Trabajadores;
                }
                else { obj.costo_despido_total = 0; }


                if (obj.Trabajadores > fuerzaInicial)
                {
                    obj.costo_contratacion_total = ((obj.Trabajadores - fuerzaInicial) * costoCont);
                    fuerzaInicial = (int)obj.Trabajadores;
                }
                else { obj.costo_contratacion_total = 0; }

                obj.inventario_final = (tasa * demanda);
                obj.costo_total = obj.costo_md_total + obj.costoH + obj.costo_despido_total + obj.costo_mp_total + obj.costo_contratacion_total;

                listaP.Add(obj);

                contadorPlan = contadorPlan + 1;


                contador = contador - 1;*/
            }

            return listaP;

        }

       /* public static void generarColumnasPersecucion(DataGrid dg)
        {

            dg.Columns.Clear();
            string[] labels = new string[] { "inventario_inicial", "ProducRequerida", "Horas_requeridas",
                "Dias_habiles", "Horas_disponibles","Trabajadores", "inventario_final", "costo_mp_total", "costo_md_total","costoH","costo_contratacion_total", "costo_despido_total", "costo_total" };

            foreach (string label in labels)
            {
                DataGridTextColumn column = new DataGridTextColumn();
                column.Header = label;
                column.Width = 90;
                column.Binding = new Binding(label.Replace(' ', '_'));

                dg.Columns.Add(column);
            }
        }


        public static void renombrarColumnasLOTEALOTE(DataGrid dg)
        {
            string[] labels = new string[] { "Período", "Demanda", "Producción", "Inventario", "CostoMtto", "CostoPedir", "costoTotal" };

            for (int i = 0; i < dg.Columns.Count; i++)
            {
                dg.Columns[i].Header = labels[i];
            }
        }
       */
        public double detectandoDecimales(double valor)
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
