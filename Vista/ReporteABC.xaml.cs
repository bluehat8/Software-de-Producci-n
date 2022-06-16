using Software_de_Producción.Controlador;
using Software_de_Producción.Modelo;
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
using System.Windows.Shapes;

namespace Software_de_Producción.Vista
{
    /// <summary>
    /// Lógica de interacción para ReporteABC.xaml
    /// </summary>
    public partial class ReporteABC : Window
    {
        RepositorioProductos objRepo = new RepositorioProductos();

        public List<ModeloABC> lstModelABC { get; set; }
        public ModeloABC abcObject;
        public ReporteABC()
        {
            InitializeComponent();
            abcObject = new ModeloABC();
            lstModelABC = abcObject.calcularABC(objRepo.Read);
            DataContext = this;
            setearDatosGenerales();
        }
    


        private void Button_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                printButton.IsEnabled = false;
                close.IsEnabled = false;
                PrintDialog printDialog = new PrintDialog();
                if (printDialog.ShowDialog() == true)
                {
                    printDialog.PrintVisual(print, "invoice");
                    this.Close();
                }
            }
            finally
            {
                printButton.IsEnabled = true;
                close.IsEnabled = true;

            }
        }

        private void Grid_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            this.DragMove();
        }


        public void setearDatosGenerales()
        {
            fechaActual.Text = DateTime.Now.ToString();
            aElements.Text = lstModelABC.Where(item => item.zona.Equals("A")).Count().ToString();
            bElements.Text = lstModelABC.Where(item => item.zona.Equals("B")).Count().ToString();
            cElements.Text = lstModelABC.Where(item => item.zona.Equals("C")).Count().ToString();
            tbcTotal.Text = (lstModelABC.Where(item => item.zona.Equals("A")).Count() +
                              lstModelABC.Where(item => item.zona.Equals("B")).Count() +
                              lstModelABC.Where(item => item.zona.Equals("C")).Count()).ToString();
            tbcInvTotal.Text = "C$ "+ lstModelABC.Sum(item => item.inversion).ToString();

        }

        private void Hyperlink_Click(object sender, RoutedEventArgs e)
        {
            System.Diagnostics.Process.Start("https://github.com/samtemporal");


        }

        private void gmailLink_Click(object sender, RoutedEventArgs e)
        {
            System.Diagnostics.Process.Start("https://mail.google.com/mail/u/0/?view=cm&fs=1&tf=1&to=agustin.amaya.g21@gmail.com&subject=Duda%20sobre%20StockMGM");
        }

        private void close_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
