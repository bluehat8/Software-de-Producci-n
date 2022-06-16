using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Data;

namespace Software_de_Producción.Modelo
{
    public class LoteALote
    { 
        public string periodo { get; set; }
        public int demanda { get; set; }
        public int produccion { get; set; }
        public int inventario { get; set; }
        public double costoMtto { get; set; }

        public double costoPedir { get; set; }

        public double costoTotal { get; set; }

        


        public LoteALote() { }
        public LoteALote(string periodo, int demanda, int produccion, int inventario, double costoMtto, double costoPedir, double costoTotal) {
            this.periodo = periodo;
            this.demanda = demanda;
            this.produccion = produccion;
            this.inventario = inventario;
            this.costoMtto = costoMtto;
            this.costoPedir = costoPedir;
            this.costoTotal = costoTotal;
        }



        public List<LoteALote> generarLoteALote(double costoProducto, double costoPedir, double tasaMtto, List<PlanMaestroMRP> lstPlan)
        {
            double tasa = tasaMtto / 100;

            LoteALote objLote = new LoteALote();

            List<LoteALote> lstLFL = new List<LoteALote>();

            for (int i = 0; i < lstPlan.Count; i++)
            {
                objLote = new LoteALote();

                objLote.periodo = lstPlan.ElementAt(i).periodo;
                objLote.demanda= lstPlan.ElementAt(i).demanda;
                objLote.produccion = lstPlan.ElementAt(i).demanda;
                objLote.inventario = lstPlan.ElementAt(i).demanda-objLote.produccion;
                objLote.costoMtto = objLote.inventario * tasa;
                objLote.costoPedir = costoPedir;

                if (objLote.produccion == 0) { objLote.costoPedir = 0; }

             
                if (i > 0)
                {
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
