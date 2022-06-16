using Microsoft.Win32;
using Software_de_Producción.Controlador;
using Software_de_Producción.Modelo;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Software_de_Producción.Vista.UserControls
{
    /// <summary>
    /// Lógica de interacción para Productos.xaml
    /// </summary>
    public partial class Productos : UserControl
    {

        RepositorioProductos objRepo = new RepositorioProductos();
        Producto objProducto = new Producto();
        public Productos()
        {
            InitializeComponent();
            datagridP.ItemsSource = objRepo.Read;
            ajustarAnchoDeColumnas(180);
            this.Language = XmlLanguage.GetLanguage("en-us");


            try
            {
                objRepo.calcularDatosRelevantes(tbProductos, tbTotales, tbCostoTotalCompra, tbcMasExistencias);


            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.ToString());

            }

        }

    
        public void ajustarAnchoDeColumnas (int ancho)
        {
            columID.Width = ancho;
            columNombre.Width = ancho;
            columInventario.Width = ancho;
            columInventario.Width = ancho;
            columImporte.Width = ancho;
            columEliminar.Width = ancho;
            columPrecioCompra.Width = ancho;
            columPrecioVenta.Width = ancho;
            Editar.Width = ancho;

        }

        private void DatagridBG_SelectedCellsChanged(object sender, SelectedCellsChangedEventArgs e)
        {

        }

        private void DatagridBG_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }


        private void dgButtonEdit_Click_1(object sender, RoutedEventArgs e)
        {
            EditarProducto ventanaEditarp = new EditarProducto(objProducto,datagridP,tbProductos,tbTotales,tbCostoTotalCompra,tbcMasExistencias);
            ventanaEditarp.Show();
        }

        private void dgButtonDelete_Click_1(object sender, RoutedEventArgs e)
        {
            MessageBoxResult dialogResult = MessageBox.Show("¿DESEA ELIMINAR PRODUCTO?", "Eliminación de PRODUCTOS", MessageBoxButton.YesNo, MessageBoxImage.Information);
            if (dialogResult == MessageBoxResult.Yes)
            {
                //Delete item
                if (datagridP.SelectedItems.Count == 0)
                {
                    MessageBox.Show("Debe seleccionar por lo menos una cuenta.");
                    return;
                }

                Producto a = (Producto)datagridP.SelectedItem;
                objProducto.id = a.id.ToString();
                objProducto.nombre = a.nombre.ToString();
                objProducto.inventario = int.Parse(a.inventario.ToString());
                objProducto.precioDeCompra = double.Parse(a.precioDeCompra.ToString());
                objProducto.precioDeVenta = double.Parse(a.precioDeVenta.ToString());

                objRepo.EliminarPorID(objProducto);
                datagridP.ItemsSource = null;
                datagridP.ItemsSource = objRepo.Read;
                objRepo.calcularDatosRelevantes(tbProductos, tbTotales, tbCostoTotalCompra, tbcMasExistencias);


                MessageBox.Show("CUENTA ELIMINADA", "Eliminación de cuenta", MessageBoxButton.OK, MessageBoxImage.Asterisk);
            }
            else if (dialogResult == MessageBoxResult.No)
            {
                //do something else
            }
        }

        private void btnRegistrar_Click(object sender, RoutedEventArgs e)
        {
            RegistraProducto ventanaRegistrarP = new RegistraProducto(datagridP,objRepo,tbProductos,tbTotales,tbCostoTotalCompra,tbcMasExistencias);
            ventanaRegistrarP.Show();
        }

        private void btnEliminarTodo_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult dialogResult = MessageBox.Show("¿DESEA ELIMINAR TODO?", "Eliminación de PRODUCTOS", MessageBoxButton.YesNo, MessageBoxImage.Information);
            if (dialogResult == MessageBoxResult.Yes)
            {
                objRepo.Eliminar();
                datagridP.ItemsSource = objRepo.Read;

            }
            else { //do something
            }
        }



        private void datagridP_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (datagridP.SelectedItems.Count == 1)
            {
                Producto a = (Producto)datagridP.SelectedItem;
                objProducto.id = a.id.ToString();
                objProducto.nombre = a.nombre.ToString();
                objProducto.inventario = int.Parse(a.inventario.ToString());
                objProducto.precioDeCompra = double.Parse(a.precioDeCompra.ToString());
                objProducto.precioDeVenta = double.Parse(a.precioDeVenta.ToString());
            }
        }

       
        private void txtBusqueda_TextChanged(object sender, TextChangedEventArgs e)
        {
         
           Buscar(txtBusqueda.Text.ToString());

        }

        private void Buscar(string nombre)
        {
            if (txtBusqueda.Text.Trim() == "")
            {
                datagridP.ItemsSource = objRepo.Read;
            }
            else
            {
                objProducto = new Producto();
                objProducto.nombre = txtBusqueda.Text.ToString().ToUpper();
                datagridP.ItemsSource = null;
                datagridP.ItemsSource = objRepo.Buscar(objProducto);
            }
        }

        private void btnImprimirInforme_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                ReporteABC ventanaAbc = new ReporteABC();
                ventanaAbc.Show();
            }
            catch (Exception ex)
            {

                MessageBox.Show("Error: "+ ex.ToString());
            }
           
        }

        private void btnImportaExcel_Click(object sender, RoutedEventArgs e)
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
                    //obj_repositorio.Read = null;
                    List<Producto> lista = objRepo.ImportarDatos(openFileDialog.FileName);
                    MessageBox.Show("Count: " + lista[0].nombre);

                    foreach (var item in lista)
                    {
                        objRepo.Crear(item);
                    }
                    //objRepo.BulkCreate(lista);

                    datagridP.ItemsSource = null;
                    datagridP.ItemsSource = objRepo.Read;
                    objRepo.calcularDatosRelevantes(tbProductos, tbTotales, tbCostoTotalCompra, tbcMasExistencias);


                }
            }
            catch (Exception ex) { MessageBox.Show("Error: " + ex.ToString()); }
        }
    }
}
