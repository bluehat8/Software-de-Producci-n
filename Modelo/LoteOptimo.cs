using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Data;

namespace Software_de_Producción.Modelo
{
    public class LoteOptimo
    {

        public string periodo { get; set; }
        public int demanda { get; set; }
        public int produccion { get; set; }
        public int inventario { get; set; }
        public double costoMtto { get; set; }

        public double costoPedir { get; set; }

        public double costoTotal { get; set; }




        public LoteOptimo() { }
        public LoteOptimo(string periodo, int demanda, int produccion, int inventario, double costoMtto, double costoPedir, double costoTotal)
        {
            this.periodo = periodo;
            this.demanda = demanda;
            this.produccion = produccion;
            this.inventario = inventario;
            this.costoMtto = costoMtto;
            this.costoPedir = costoPedir;
            this.costoTotal = costoTotal;
        }

        public int calcularPedidoOptimo(List<PlanMaestroMRP> lstPlan, double costoPedir, string TipoDemanda, double costoProd, double tasaMtto)
        {

            double demandaTotal=lstPlan.Sum(item => item.demanda);
            int periodosTotales = lstPlan.Count;
            int result=0;

            if (TipoDemanda.ToUpper().Equals("SEMANAL"))
            {
                result= (int)Math.Round((Math.Sqrt((((demandaTotal/periodosTotales)*52) * 2 * costoPedir) / (tasaMtto * costoProd*52))));
            }


            if (TipoDemanda.ToUpper().Equals("MENSUAL"))
            {
                result = (int)Math.Round((Math.Sqrt((((demandaTotal / periodosTotales) * 12) * 2 * costoPedir) / (tasaMtto * costoProd * 12))));

            }


            if (TipoDemanda.ToUpper().Equals("DIARIA"))
            {
                result = (int)Math.Round((Math.Sqrt((((demandaTotal / periodosTotales) * 360) * 2 * costoPedir) / (tasaMtto * costoProd * 360))));

            }
            

            return result;
        }

        /*public double detectandoDecimales(double valor)
        {
            string conversion = Convert.ToString(valor);
            double retornaValor = valor;
            double remainder = valor % 1;

            if (remainder != 0)
            {
                retornaValor = valor + 1;

            }
            return retornaValor;
        }*/



        public List<LoteOptimo> generarLoteOptimo(double costoProducto, double costoPedir, double tasaMtto, List<PlanMaestroMRP> lstPlan, string tipoDemanda)
        {
            double tasa = tasaMtto / 100;
            int qoptimo=calcularPedidoOptimo(lstPlan, costoPedir, tipoDemanda, costoProducto, tasa);
            int contadorInv = qoptimo;
            LoteOptimo objLote = new LoteOptimo();
            List<LoteOptimo> lstLFL = new List<LoteOptimo>();
            int contadorSiguientePedido = 0;


            for (int i = 0; i <lstPlan.Count; i++)
            {
                objLote = new LoteOptimo();

                objLote.periodo = lstPlan.ElementAt(i).periodo;
                objLote.demanda = lstPlan.ElementAt(i).demanda;

                if (i == 0)
                {
                    objLote.produccion = qoptimo;

                }

                contadorInv = contadorInv - lstPlan.ElementAt(i).demanda;
                objLote.inventario = contadorInv;
                objLote.costoMtto = objLote.inventario * tasa*costoProducto;
                objLote.costoPedir = costoPedir;


                if (i > 0)
                {
                    if (contadorSiguientePedido == 1)
                    {
                        //objLote.produccion = lstLFL.ElementAt(i - 1).inventario + sum;
                        objLote.produccion = qoptimo;
                        contadorSiguientePedido--;
                    }


                    if (lstLFL.ElementAt(i - 1).inventario >= objLote.demanda)
                    {
                        /*for (int e =i+1 ; e <lstPlan.Count; e++)
                        {
                            sum = sum + lstPlan.ElementAt(e).demanda;
                        }*/
                       
                        if (contadorInv < lstPlan.ElementAt(i).demanda)
                        {
                                /*if (lstLFL.ElementAt(i-1).inventario + sum < qoptimo  )
                                {
                                    contadorInv = lstLFL.ElementAt(i-1).inventario + sum;
                                //objLote.produccion = lstLFL.ElementAt(i-1).inventario + sum;
                                //contadorSiguientePedido++;
                                
                                }*/
                               /* else
                                {*/
                                    contadorInv = qoptimo+objLote.inventario;
                                    contadorSiguientePedido++;

                                    //objLote.produccion = qoptimo;

                          //  }
                        }
                    }

                    
                    if (objLote.produccion == 0) { objLote.costoPedir = 0; }

                    objLote.costoTotal = objLote.costoMtto + objLote.costoPedir + lstLFL.ElementAt(i - 1).costoTotal;
                }

                else
                {
                    objLote.costoTotal = objLote.costoMtto + objLote.costoPedir;

                }

                lstLFL.Add(objLote);

            }

            return lstLFL;

        }



        public static void generarColumnasLOTEALOTE(DataGrid dg)
        {

            dg.Columns.Clear();
            string[] labels = new string[] { "periodo", "demanda", "produccion", "inventario", "costoMtto", "costoPedir", "costoTotal" };

            foreach (string label in labels)
            {
                DataGridTextColumn column = new DataGridTextColumn();
                column.Header = label;
                column.Width = 150;
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
    }
}
