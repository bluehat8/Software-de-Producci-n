using Microsoft.Win32;
using Software_de_Producción.Modelo;
using Software_de_Producción.Utils;
using System;
using System.Collections.Generic;
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
    /// Lógica de interacción para PlaneacionAgregada.xaml
    /// </summary>
    public partial class PlaneacionAgregada : UserControl
    {
        PlanAgregado objPlan = new PlanAgregado();
        List<PlanAgregado> lst = new List<PlanAgregado>();
        List<Persecucion> lstLot = new List<Persecucion>();

        ValidaTXT validate = new ValidaTXT();
        public PlaneacionAgregada()
        {
            InitializeComponent();
            encender_apagar_Button();
        }

        private void dgButtonDelete_Click(object sender, RoutedEventArgs e)
        {

        }

        private void dgButtonEdit_Click(object sender, RoutedEventArgs e)
        {

        }

        private void encender_apagar_Button()
        {
            if (lst.Count==0)
            {
                btnCalcular.IsEnabled = false;

            }
            else { btnCalcular.IsEnabled = true; }
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
                    List<PlanAgregado> lista = objPlan.ImportarDatosExcel(openFileDialog.FileName);
                    lst = lista;
                    datagridP.ItemsSource = null;
                    datagridP.ItemsSource = lista;
                    encender_apagar_Button();

                }
            }
            catch (Exception ex) { MessageBox.Show("Error: " + ex.ToString()); }
        }



        private void generarPersecucion()
        {
            datagridResult.Items.Clear();

            Persecucion lot = new Persecucion();
            lstLot = new List<Persecucion>();
            lstLot.Clear();
            lstLot = new List<Persecucion>();

            Persecucion.generarColumnasPersecucion(datagridResult);
           // LoteALote.renombrarColumnasLOTEALOTE(datagridResult);

            lstLot = lot.calcularPersecucion(lst, 100,1.5,3,200,250,4,5,400,25,53);

            datagridResult.ItemsSource = lstLot;
       
        }

        private void btnImportaExcel_Click(object sender, RoutedEventArgs e)
        {
                
              try
              {
                if (txtDemanda.Text == "" || txtDiasLab.Text.Trim() == "")
                {
                    MessageBox.Show("Faltan datos");
                }
                else
                {
                    if (lst.Count == 0)
                    {
                        lst = new List<PlanAgregado>();

                    }
                    PlanAgregado mp = new PlanAgregado();
                    mp.periodo = (lst.Count + 1).ToString();
                    mp.demanda = int.Parse(txtDemanda.Text);
                    mp.diasLaborales = int.Parse(txtDiasLab.Text);

                    lst.Add(mp);
                    datagridP.ItemsSource = null;
                    datagridP.ItemsSource = lst;
                    txtDemanda.Clear();
                    txtDiasLab.Clear();
                    encender_apagar_Button();
                }

            }
            catch(Exception ex)
              {
                  MessageBox.Show("Error: " + ex.ToString());
              }
        }

        private void btnCalcular_Click(object sender, RoutedEventArgs e)
        {
            RegistrarPlan plan = new RegistrarPlan(datagridResult,lst,lstLot,tbDespido,tbContrat,tbMP,tbMD,tbTotal);
            plan.Show();
        }

        private void btnEliminarTodo_Click(object sender, RoutedEventArgs e)
        {
            lst.Clear();
            lst = new List<PlanAgregado>();
            datagridP.ItemsSource = null;
            encender_apagar_Button();
        }

        private void txtDemanda_KeyDown(object sender, KeyEventArgs e)
        {
            validate.INT_KeyDown(sender, e, txtDemanda);
        }

        private void txtDemanda_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            validate.SPACE_PreviewKeyDown(sender, e);
        }

        private void txtDiasLab_KeyDown(object sender, KeyEventArgs e)
        {
            validate.INT_KeyDown(sender, e, txtDiasLab);
        }
    }
}
