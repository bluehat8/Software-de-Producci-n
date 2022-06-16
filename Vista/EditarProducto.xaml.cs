using Software_de_Producción.Controlador;
using Software_de_Producción.Modelo;
using System.Globalization;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Markup;

namespace Software_de_Producción.Vista
{
    /// <summary>
    /// Lógica de interacción para EditarProducto.xaml
    /// </summary>
    public partial class EditarProducto : Window
    {

        Producto entidad;
        DataGrid dataGridP;
        RepositorioProductos objRep = new RepositorioProductos();

        TextBlock prod;
        TextBlock totales;
        TextBlock costototal;
        TextBlock masExistencias;

        public EditarProducto(Producto entidad, DataGrid dg, TextBlock prod, TextBlock totales, TextBlock costototal, TextBlock masExistencias)
        {
            InitializeComponent();
           // this.Language = XmlLanguage.GetLanguage("en-us");

            this.entidad = entidad;
            entidad.precioDeCompra = double.Parse(entidad.precioDeCompra.ToString());
            entidad.precioDeVenta = double.Parse(entidad.precioDeVenta.ToString());

            dataGridP = dg;
            txtNombre.Text = (this.entidad.nombre).ToString();
            txtID.Text = (this.entidad.id);
            txtInventario.Text = (this.entidad.inventario).ToString();
            txtPrecioCompra.Text = this.entidad.precioDeCompra.ToString();
            txtPrecioVenta.Text = this.entidad.precioDeVenta.ToString();
            dataGridP = dg;

            this.prod = prod;
            this.totales = totales;
            this.costototal = costototal;
            this.masExistencias = masExistencias;

        }

        private void btnRegistrar_Click(object sender, RoutedEventArgs e)
        {

            if (validateNullText())
            {
                entidad.nombre = txtNombre.Text.Trim();
                entidad.precioDeCompra = double.Parse(txtPrecioCompra.Text, CultureInfo.CreateSpecificCulture("en-GB"));
                entidad.precioDeVenta = double.Parse(txtPrecioVenta.Text, CultureInfo.CreateSpecificCulture("en-GB"));
                entidad.inventario = int.Parse(txtInventario.Text);
                entidad.TotalPrecioCompra = entidad.calcularTotalCompra(entidad.inventario, entidad.precioDeCompra);
                objRep.Editar(entidad);
                dataGridP.ItemsSource = objRep.Read;

                objRep.calcularDatosRelevantes(prod, totales, costototal, masExistencias);
                MessageBox.Show("Producto actualizado");
                return;

            }
            MessageBox.Show("No se permiten campos vacios");
        }


        private bool validateNullText()
        {
            if (txtInventario.Text.Trim() == "" || txtPrecioCompra.Text.Trim() == "" ||
                txtPrecioVenta.Text.Trim() == "" || txtNombre.Text.Trim() == "")
            {
                return false;
            }
            return true;

        }

        private void Minimizar_Click(object sender, RoutedEventArgs e)
        {
            WindowState = WindowState.Minimized;
        }

        private void Cerrar_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void GridTop_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {

        }

        private void Border_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            this.DragMove();
        }
    }
}
