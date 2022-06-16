using Microsoft.Win32;
using Software_de_Producción.Modelo;
using Software_de_Producción.Utils;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Software_de_Producción.Vista.UserControls
{
    /// <summary>
    /// Lógica de interacción para MRP.xaml
    /// </summary>
    public partial class MRP : UserControl
    {
        PlanMaestroMRP objPlan = new PlanMaestroMRP();
        List<PlanMaestroMRP> lst = new List<PlanMaestroMRP>();
        ValidaTXT validate = new ValidaTXT();
        string tipoDemanda;

        public MRP()
        {
            InitializeComponent();
        }

        private void dgButtonDelete_Click(object sender, RoutedEventArgs e)
        {

        }

        private void dgButtonEdit_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnImportarExcel_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                OpenFileDialog openFileDialog = new OpenFileDialog
                {
                    Filter = "Excel | *.xls;*.xlsx;",
                    Title = "Seleccionar Archivo"
                };
                if ((bool)openFileDialog.ShowDialog())
                {
                    List<PlanMaestroMRP> lista = objPlan.ImportarDatosExcel(openFileDialog.FileName);
                    lst = lista;
                    datagridP.ItemsSource = null;
                    datagridP.ItemsSource = lista;


                }
            }
            catch (Exception ex) { MessageBox.Show("Error: " + ex.ToString()); }
        }

        private void datagridP_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void btnCalcular_Click(object sender, RoutedEventArgs e)
        {

            try
            {
                if (rbLFL.IsChecked == true)
                {

                    generarLoteALote();

                }

                if (rbOptimo.IsChecked == true)
                {

                    generarLoteOptimo();
                }
            }
            catch ( Exception EX)
            {

                MessageBox.Show("Error: " + EX.ToString());
            }
           

        }

        private void rbLFL_Checked(object sender, RoutedEventArgs e)
        {
            try
            {
                generarLoteALote();

            }
            catch (Exception ex)
            {

                MessageBox.Show("Faltan datos");
            }
            
        }


        private void generarLoteALote()
        {
            datagridResult.Items.Clear();

            LoteALote lot = new LoteALote();
            List<LoteALote> lstLot = new List<LoteALote>();
            lstLot.Clear();
            lstLot = new List<LoteALote>();

            LoteALote.generarColumnasLOTEALOTE(datagridResult);
            LoteALote.renombrarColumnasLOTEALOTE(datagridResult);

            lstLot = lot.generarLoteALote(double.Parse(txtCostop.Text, CultureInfo.CreateSpecificCulture("en-GB")), double.Parse(txtCostoPedir.Text, CultureInfo.CreateSpecificCulture("en-GB")), double.Parse(txtTasaMtto.Text, CultureInfo.CreateSpecificCulture("en-GB")), lst);
            tbcCostoTotal.Text = "Costo total:    " + lstLot.ElementAt(lstLot.Count - 1).costoTotal.ToString();

            // datagridResult.ItemsSource = lstLot;
            for (int i = 0; i < lstLot.Count; i++)
            {
                datagridResult.Items.Add(new LoteALote(
                    lstLot[i].periodo,
                    lstLot[i].demanda,
                    lstLot[i].produccion,
                    lstLot[i].inventario,
                    lstLot[i].costoMtto,
                    lstLot[i].costoPedir,
                    lstLot[i].costoTotal)
                    );
            }
        }

        private void generarLoteOptimo()
        {
            datagridResult.ItemsSource = null;
            datagridResult.Items.Clear();

            LoteOptimo lot = new LoteOptimo();
            List<LoteOptimo> lstLot = new List<LoteOptimo>();
            lstLot.Clear();
            lstLot = new List<LoteOptimo>();
            LoteOptimo.generarColumnasLOTEALOTE(datagridResult);
            LoteOptimo.renombrarColumnasLOTEALOTE(datagridResult);

            lstLot = lot.generarLoteOptimo(double.Parse(txtCostop.Text, CultureInfo.CreateSpecificCulture("en-GB")), double.Parse(txtCostoPedir.Text, CultureInfo.CreateSpecificCulture("en-GB")), double.Parse(txtTasaMtto.Text, CultureInfo.CreateSpecificCulture("en-GB")), lst, tipoDemanda);
            tbcCostoTotal.Text = "Costo total:    " + lstLot.ElementAt(lstLot.Count - 1).costoTotal.ToString();
            // datagridResult.ItemsSource = lstLot;
            for (int i = 0; i < lstLot.Count; i++)
            {
                datagridResult.Items.Add(new LoteALote(
                    lstLot[i].periodo,
                    lstLot[i].demanda,
                    lstLot[i].produccion,
                    lstLot[i].inventario,
                    lstLot[i].costoMtto,
                    lstLot[i].costoPedir,
                    lstLot[i].costoTotal)
                    );
            }
        }

        private void rbOptimo_Checked(object sender, RoutedEventArgs e)
        {
            try
            {
                generarLoteOptimo();

            }
            catch (Exception ex)
            {

                MessageBox.Show("Faltan datos");
            }
        }

        private void btnImportaExcel_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnLimpiar_Click(object sender, RoutedEventArgs e)
        {
            txtCostop.Clear();
            txtCostoPedir.Clear();
            txtTasaMtto.Clear();
        }

        private void txtDemanda_KeyDown(object sender, KeyEventArgs e)
        {
            
            validate.INT_KeyDown(sender, e, txtDemanda);
        }

        private void txtDemanda_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            validate.SPACE_PreviewKeyDown(sender, e);
        }

        private void txtCostop_KeyDown(object sender, KeyEventArgs e)
        {
            validate.Numeric_KeyDown(sender, e, txtCostop);
        }

        private void txtCostop_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            validate.SPACE_PreviewKeyDown(sender, e);

        }

        private void txtCostoPedir_KeyDown(object sender, KeyEventArgs e)
        {
            validate.Numeric_KeyDown(sender, e, txtCostoPedir);
        }

        private void txtCostoPedir_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            validate.SPACE_PreviewKeyDown(sender, e);

        }

        private void txtTasaMtto_KeyDown(object sender, KeyEventArgs e)
        {
            validate.Numeric_KeyDown(sender,e, txtTasaMtto);
        }

        private void txtTasaMtto_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            validate.SPACE_PreviewKeyDown(sender, e);

        }

        private void btnRegistrarD_Click(object sender, RoutedEventArgs e)
        {

            if (txtDemanda.Text == "")
            {
                MessageBox.Show("Ingrese la demanda");
            }
            else
            {
                if (lst.Count == 0)
                {
                    lst = new List<PlanMaestroMRP>();

                }
                PlanMaestroMRP mp = new PlanMaestroMRP();
                mp.periodo = (lst.Count + 1).ToString();
                mp.demanda = int.Parse(txtDemanda.Text);
                lst.Add(mp);
                datagridP.ItemsSource = null;
                datagridP.ItemsSource = lst;
                txtDemanda.Clear();
            }
        }

        private void btnEliminarTodo_Click(object sender, RoutedEventArgs e)
        {
            lst.Clear();
            lst = new List<PlanMaestroMRP>();
            datagridP.ItemsSource = null;
        }

        private void rbSemanal_Checked(object sender, RoutedEventArgs e)
        {
            tipoDemanda = "SEMANAL";
        }

        private void rbDiaria_Checked(object sender, RoutedEventArgs e)
        {
            tipoDemanda = "DIARIA";
        }

        private void rbMensual_Checked(object sender, RoutedEventArgs e)
        {
            tipoDemanda = "MENSUAL";
        }
    }
}
