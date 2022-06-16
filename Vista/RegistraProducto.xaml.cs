using Software_de_Producción.Controlador;
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
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Software_de_Producción.Vista
{
    /// <summary>
    /// Lógica de interacción para RegistraProducto.xaml
    /// </summary>
    public partial class RegistraProducto : Window
    {

        DataGrid dgp;
        RepositorioProductos objRep;
        ValidaTXT validar= new ValidaTXT();

        Producto entidad = new Producto();
        TextBlock prod;
        TextBlock totales;
        TextBlock costototal;
        TextBlock masExistencias;

        public RegistraProducto(DataGrid dg, RepositorioProductos objRepo, TextBlock prod, TextBlock totales, TextBlock costototal, TextBlock masExistencias)
        {
            InitializeComponent();
            this.Language = XmlLanguage.GetLanguage("en-us");

            objRep = objRepo;
            dgp = dg;

            this.prod = prod;
            this.totales = totales;
            this.costototal = costototal;
            this.masExistencias = masExistencias;
        }

        private bool validateNullText()
        {
            if (txtInventario.Text.Trim()=="" || txtPrecioCompra.Text.Trim() == "" ||
                txtPrecioVenta.Text.Trim() == "" || txtNombre.Text.Trim() == "")
            {
                return false;
            }
            return true;

        }

        private void btnRegistrar_Click(object sender, RoutedEventArgs e)
        {
            if (validateNullText())
            {
                entidad.nombre = txtNombre.Text;
                entidad.precioDeCompra = double.Parse(txtPrecioCompra.Text, CultureInfo.CreateSpecificCulture("en-GB"));
                entidad.precioDeVenta = double.Parse(txtPrecioVenta.Text, CultureInfo.CreateSpecificCulture("en-GB"));
                entidad.inventario = int.Parse(txtInventario.Text);
                objRep.Crear(entidad);
                dgp.ItemsSource = objRep.Read;

                objRep.calcularDatosRelevantes(prod, totales, costototal, masExistencias);
                MessageBox.Show("Producto insertado");


                return;
            }
            MessageBox.Show("No se permiten campos vacios");

           


        }

        private void Cerrar_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void Minimizar_Click(object sender, RoutedEventArgs e)
        {
            this.WindowState = WindowState.Minimized;
        }

      

        private void Border_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            this.DragMove();
        }

        private void TextBox_PreviewKeyDown(object sender, KeyEventArgs e)
        {

        }

        private void TextBox_KeyDown(object sender, KeyEventArgs e)
        {

        }

        private void TextBox_KeyDown_1(object sender, KeyEventArgs e)
        {

        }

        private void txtInventario_KeyDown(object sender, KeyEventArgs e)
        {
            validar.INT_KeyDown(sender, e, txtInventario);
        }

        private void txtPrecioVenta_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            validar.SPACE_PreviewKeyDown(sender, e);

        }

        private void txtPrecioVenta_KeyDown(object sender, KeyEventArgs e)
        {
            validar.Numeric_KeyDown(sender, e, txtPrecioVenta);

        }

        private void TextBox_KeyDown_2(object sender, KeyEventArgs e)
        {

        }

        private void txtPrecioCompra_KeyDown(object sender, KeyEventArgs e)
        {
            validar.Numeric_KeyDown(sender, e, txtPrecioCompra);
        }

        private void txtPrecioCompra_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            validar.SPACE_PreviewKeyDown(sender, e);
        }

        private void txtInventario_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            validar.SPACE_PreviewKeyDown(sender, e);

        }
    }
}
