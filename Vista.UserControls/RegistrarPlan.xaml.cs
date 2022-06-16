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
using System.Windows.Shapes;

namespace Software_de_Producción.Vista.UserControls
{
    /// <summary>
    /// Lógica de interacción para RegistrarPlan.xaml
    /// </summary>
    public partial class RegistrarPlan : Window
    {
        DataGrid dgv;
        PlanAgregado objPlan = new PlanAgregado();
        List<PlanAgregado> lst = new List<PlanAgregado>();
        List<Persecucion> lstLot = new List<Persecucion>();
        TextBlock t1;
        TextBlock t2;
        TextBlock t3;
        TextBlock t4;
        TextBlock t5;


        List<Persecucion> lstPer;
        ValidaTXT validate = new ValidaTXT();
        public RegistrarPlan(DataGrid dg, List<PlanAgregado> lst, List<Persecucion> lstPer, TextBlock costoDespido, TextBlock costoContratac, TextBlock costoMP, TextBlock costoMD, TextBlock costoTotal)
        {
            InitializeComponent();
            dgv = dg;
            this.lst = lst;
            lstLot = lstPer;
            t1 = costoDespido;
            t2 = costoContratac;
            t3 = costoMP;
            t4 = costoMD;
            t5 = costoTotal;

        }

        private void Border_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            this.DragMove();
        }

        private void btnRegistrar_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                double mp = double.Parse(txtCostoMP.Text, CultureInfo.CreateSpecificCulture("en-GB"));
                double h = double.Parse(txtCostoH.Text, CultureInfo.CreateSpecificCulture("en-GB"));
                double contr = double.Parse(txtContrat.Text, CultureInfo.CreateSpecificCulture("en-GB"));
                double despido = double.Parse(txtCostoDespido.Text, CultureInfo.CreateSpecificCulture("en-GB"));
                double costotn = double.Parse(txtCostotn.Text, CultureInfo.CreateSpecificCulture("en-GB"));
                double tiempoRU = double.Parse(txtTiempoXUnidad.Text, CultureInfo.CreateSpecificCulture("en-GB"));
                int invIn = int.Parse(txtInventarioInicial.Text, CultureInfo.CreateSpecificCulture("en-GB"));
                double ss = double.Parse(txtSS.Text, CultureInfo.CreateSpecificCulture("en-GB"));
                int fuerzaLab = int.Parse(txtFuerzaLaboralInicial.Text, CultureInfo.CreateSpecificCulture("en-GB"));


               // dgv.Items.Clear();
                dgv.ItemsSource = null;
                Persecucion lot = new Persecucion();
                lstLot = new List<Persecucion>();
                lstLot.Clear();
                lstLot = new List<Persecucion>();
                Persecucion.generarColumnasPersecucion(dgv);
                lstLot = lot.calcularPersecucion(lst, mp, h, 0, contr, despido, costotn, tiempoRU, invIn, ss, fuerzaLab);
                dgv.ItemsSource = lstLot;
                establecerCostos();

            }
            catch (Exception ex)
            {

                MessageBox.Show("Error"+ex.ToString());
            }
            



        }


        private void establecerCostos()
        {
            t1.Text = "Costo despido: "+  lstLot.Sum(item => item.costo_despido_total).ToString();
            t2.Text = "Costo contratacion: " + lstLot.Sum(item => item.costo_contratacion_total).ToString();
            t3.Text = "Costo MP: " + lstLot.Sum(item => item.costo_mp_total).ToString();
            t4.Text = "Costo MD: " + lstLot.Sum(item => item.costo_md_total).ToString();
            t5.Text = "Costo total: " + lstLot.Sum(item => item.costo_total).ToString();



        }

        private void generarPersecucion()
        {
            dgv.Items.Clear();
            Persecucion lot = new Persecucion();
            lstLot = new List<Persecucion>();
            lstLot.Clear();
            lstLot = new List<Persecucion>();
            Persecucion.generarColumnasPersecucion(dgv);
            lstLot = lot.calcularPersecucion(lst, 100, 1.5, 3, 200, 250, 4, 5, 400, 25, 53);
            dgv.ItemsSource = lstLot;

        }

        private void Cerrar_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void Minimizar_Click(object sender, RoutedEventArgs e)
        {
            WindowState = WindowState.Minimized;
        }

        private void txtCostoMP_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            validate.SPACE_PreviewKeyDown(sender, e);
        }

        private void txtCostoMP_KeyDown(object sender, KeyEventArgs e)
        {
            validate.Numeric_KeyDown(sender, e, txtCostoMP);
        }

        private void txtCostoH_KeyDown(object sender, KeyEventArgs e)
        {
            validate.Numeric_KeyDown(sender, e, txtCostoH);
        }

        private void txtContrat_KeyDown(object sender, KeyEventArgs e)
        {
            validate.Numeric_KeyDown(sender, e, txtContrat);
        }

        private void txtCostoDespido_KeyDown(object sender, KeyEventArgs e)
        {
            validate.Numeric_KeyDown(sender, e, txtCostoDespido);
        }

        private void txtCostotn_KeyDown(object sender, KeyEventArgs e)
        {
            validate.Numeric_KeyDown(sender, e, txtCostotn);
        }

        private void txtCostotn_KeyDown_1(object sender, KeyEventArgs e)
        {
            validate.Numeric_KeyDown(sender, e, txtCostotn);

        }

        private void txtTiempoXUnidad_KeyDown(object sender, KeyEventArgs e)
        {
            validate.Numeric_KeyDown(sender, e, txtTiempoXUnidad);
        }

        private void txtInventarioInicial_KeyDown(object sender, KeyEventArgs e)
        {
            validate.INT_KeyDown(sender, e, txtInventarioInicial);
        }

        private void txtPrecioVenta_KeyDown(object sender, KeyEventArgs e)
        {
            //validate.Numeric_KeyDown(sender, e,txtPrecioVenta_KeyDown);
        }

        private void txtFuerzaLaboralInicial_KeyDown(object sender, KeyEventArgs e)
        {
            validate.INT_KeyDown(sender, e, txtFuerzaLaboralInicial);
        }

        private void txtSS_KeyDown(object sender, KeyEventArgs e)
        {
            validate.Numeric_KeyDown(sender, e, txtSS);
        }
    }
}
