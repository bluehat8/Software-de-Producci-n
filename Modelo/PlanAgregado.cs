using SpreadsheetLight;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Software_de_Producción.Modelo
{
    public class PlanAgregado
    {
        public string periodo { get; set; }
        public int demanda { get; set; }
        public int diasLaborales { get; set; }

        public PlanAgregado()
        {
        }

        public List<PlanAgregado> ImportarDatosExcel(string patharchivo)
        {
            SLDocument sl = new SLDocument(patharchivo);
            int irow = 2;
            List<PlanAgregado> lstBG = new List<PlanAgregado>();
            PlanAgregado objProd;

            while (!string.IsNullOrEmpty(sl.GetCellValueAsString(irow, 1)))
            {
                objProd = new PlanAgregado();

                objProd.periodo = sl.GetCellValueAsString(irow, 1);
                objProd.demanda = int.Parse(sl.GetCellValueAsString(irow, 2));
                objProd.diasLaborales = int.Parse(sl.GetCellValueAsString(irow, 3));
                lstBG.Add(objProd);
                irow++;

            }

            return lstBG;
        }

        /////////////////////////
    }
}
