using SpreadsheetLight;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace Software_de_Producción.Modelo
{
    public class PlanMaestroMRP
    {
        public string periodo { get; set; }
        public int demanda { get; set; }

        public PlanMaestroMRP() { 
        }

        public List<PlanMaestroMRP> ImportarDatosExcel(string patharchivo)
        {
            SLDocument sl = new SLDocument(patharchivo);
            int irow = 2;
            List<PlanMaestroMRP> lstBG = new List<PlanMaestroMRP>();
            PlanMaestroMRP objProd;

            while (!string.IsNullOrEmpty(sl.GetCellValueAsString(irow, 1)))
            {
                objProd = new PlanMaestroMRP();

                objProd.periodo = sl.GetCellValueAsString(irow, 1);
                objProd.demanda = int.Parse(sl.GetCellValueAsString(irow, 2));

                //Validando cuentas no repetidas 
               /* if (!oBG.ValidandoCuentas(oBG, lstBG))
                {
                    MessageBox.Show("La cuentas que intenta importan se encuentran repetidas, ", "Warning", MessageBoxButton.OK, MessageBoxImage.Information);
                    break;
                }*/

                lstBG.Add(objProd);
                irow++;

            }
           

            return lstBG;
        }

    }
}
