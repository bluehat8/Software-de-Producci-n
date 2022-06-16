using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Software_de_Producción.Vista
{
    /// <summary>
    /// Lógica de interacción para MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            btnHome.RaiseEvent(new RoutedEventArgs(Button.ClickEvent));
            WindowState = WindowState.Maximized;

        }

        private void BG_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Tg_Btn.IsChecked = false;
        }

        // Start: MenuLeft PopupButton //
        private void btnHome_MouseEnter(object sender, MouseEventArgs e)
        {
            if (Tg_Btn.IsChecked == false)
            {
                Popup.PlacementTarget = btnHome;
                Popup.Placement = PlacementMode.Right;
                Popup.IsOpen = true;
                Header.PopupText.Text = "Home";
            }
        }

        private void btnHome_MouseLeave(object sender, MouseEventArgs e)
        {
            Popup.Visibility = Visibility.Collapsed;
            Popup.IsOpen = false;
        }


        private void btnModeloQ_MouseLeave(object sender, MouseEventArgs e)
        {

        }

        private void btnProduct_MouseEnter(object sender, MouseEventArgs e)
        {
            if (Tg_Btn.IsChecked == false)
            {
                Popup.PlacementTarget = btnProduct;
                Popup.Placement = PlacementMode.Right;
                Popup.IsOpen = true;
                Header.PopupText.Text = "Productos";
            }
        }

        private void btnProduct_MouseLeave(object sender, MouseEventArgs e)
        {
            Popup.Visibility = Visibility.Collapsed;
            Popup.IsOpen = false;
        }

        private void btnModeloQ_MouseEnter(object sender, MouseEventArgs e)
        {
            if (Tg_Btn.IsChecked == false)
            {
                Popup.PlacementTarget = btnModeloQ;
                Popup.Placement = PlacementMode.Right;
                Popup.IsOpen = true;
                Header.PopupText.Text = "Modelo Q";
            }

        }

        private void btnModeloQ_MouseLeave_2(object sender, MouseEventArgs e)
        {
            Popup.Visibility = Visibility.Collapsed;
            Popup.IsOpen = false;
        }

        private void btnMRP_MouseEnter(object sender, MouseEventArgs e)
        {
            if (Tg_Btn.IsChecked == false)
            {
                Popup.PlacementTarget = btnMRP;
                Popup.Placement = PlacementMode.Right;
                Popup.IsOpen = true;
                Header.PopupText.Text = "MRP";
            }
        }

        private void btnMRP_MouseLeave(object sender, MouseEventArgs e)
        {
            Popup.Visibility = Visibility.Collapsed;
            Popup.IsOpen = false;
        }

        private void btnPlanAgregado_MouseEnter(object sender, MouseEventArgs e)
        {
            if (Tg_Btn.IsChecked == false)
            {
                Popup.PlacementTarget = btnPlanAgregado;
                Popup.Placement = PlacementMode.Right;
                Popup.IsOpen = true;
                Header.PopupText.Text = "Planeación agregada";
            }
        }

        private void btnPlanAgregado_MouseLeave(object sender, MouseEventArgs e)
        {
            Popup.Visibility = Visibility.Collapsed;
            Popup.IsOpen = false;
        }

        #region NEW GRID NAV BUTTONS
        /*
              private void btnProducts_MouseEnter(object sender, MouseEventArgs e)
              {
                  if (Tg_Btn.IsChecked == false)
                  {
                      Popup.PlacementTarget = btnModeloQ;
                      Popup.Placement = PlacementMode.Right;
                      Popup.IsOpen = true;
                      Header.PopupText.Text = "Modelo Q";
                  }
              }

              private void btnProducts_MouseLeave(object sender, MouseEventArgs e)
              {
                  Popup.Visibility = Visibility.Collapsed;
                  Popup.IsOpen = false;
              }

              private void btnProductStock_MouseEnter(object sender, MouseEventArgs e)
              {
                  if (Tg_Btn.IsChecked == false)
                  {
                     // Popup.PlacementTarget = btnProductStock;
                      Popup.Placement = PlacementMode.Right;
                      Popup.IsOpen = true;
                      Header.PopupText.Text = "Modelo P";
                  }
              }

              private void btnProductStock_MouseLeave(object sender, MouseEventArgs e)
              {
                  Popup.Visibility = Visibility.Collapsed;
                  Popup.IsOpen = false;
              }

              private void btnOrderList_MouseEnter(object sender, MouseEventArgs e)
              {
                  if (Tg_Btn.IsChecked == false)
                  {
                     // Popup.PlacementTarget = btnOrderList;
                      Popup.Placement = PlacementMode.Right;
                      Popup.IsOpen = true;
                      Header.PopupText.Text = "Pedido único";
                  }
              }

              private void btnOrderList_MouseLeave(object sender, MouseEventArgs e)
              {
                  Popup.Visibility = Visibility.Collapsed;
                  Popup.IsOpen = false;
              }



              private void btnPointOfSale_MouseEnter(object sender, MouseEventArgs e)
              {
                  if (Tg_Btn.IsChecked == false)
                  {
                      Popup.PlacementTarget = btnPlanAgregado;
                      Popup.Placement = PlacementMode.Right;
                      Popup.IsOpen = true;
                      Header.PopupText.Text = "Planeación agregada";
                  }
              }

              private void btnPointOfSale_MouseLeave(object sender, MouseEventArgs e)
              {
                  Popup.Visibility = Visibility.Collapsed;
                  Popup.IsOpen = false;
              }

              private void btnSecurity_MouseEnter(object sender, MouseEventArgs e)
              {
                  if (Tg_Btn.IsChecked == false)
                  {
                    //  Popup.PlacementTarget = btnSecurity;
                      Popup.Placement = PlacementMode.Right;
                      Popup.IsOpen = true;
                      Header.PopupText.Text = "Security";
                  }
              }

              private void btnSecurity_MouseLeave(object sender, MouseEventArgs e)
              {
                  Popup.Visibility = Visibility.Collapsed;
                  Popup.IsOpen = false;
              }
              private void btnSetting_MouseEnter(object sender, MouseEventArgs e)
              {
                  if (Tg_Btn.IsChecked == false)
                  {
                      //Popup.PlacementTarget = btnSetting;
                      Popup.Placement = PlacementMode.Right;
                      Popup.IsOpen = true;
                      Header.PopupText.Text = "Setting";
                  }
              }

              private void btnSetting_MouseLeave(object sender, MouseEventArgs e)
              {
                  Popup.Visibility = Visibility.Collapsed;
                  Popup.IsOpen = false;
              }
        */

        #endregion
        // End: MenuLeft PopupButton //



        // Start: Button Close | Restore | Minimize 
        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            // Close();
            Application.Current.Shutdown();
        }

        private void btnRestore_Click(object sender, RoutedEventArgs e)
        {
            if (WindowState == WindowState.Normal)
                WindowState = WindowState.Maximized;
            else
                WindowState = WindowState.Normal;
        }

        private void btnMinimize_Click(object sender, RoutedEventArgs e)
        {
            WindowState = WindowState.Minimized;
        }
        // End: Button Close | Restore | Minimize


        #region NAVIGATE TO PAGE OR USERCONTROLS

        private void btnHome_Click(object sender, RoutedEventArgs e)
        {
           
            fContainer.Navigate(new System.Uri("Vista.UserControls/Home.xaml", UriKind.RelativeOrAbsolute));
        }


        private void btnProduct_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                fContainer.Navigate(new System.Uri("Vista.UserControls/Productos.xaml", UriKind.RelativeOrAbsolute));

            }
            catch (Exception EX)
            {

                MessageBox.Show("Error: " + EX.ToString());
            }
        }


        private void btnModeloQ_Click(object sender, RoutedEventArgs e)
        {
            fContainer.Navigate(new System.Uri("Vista.UserControls/ModeloQ.xaml", UriKind.RelativeOrAbsolute));

        }

        private void btnMRP_Click(object sender, RoutedEventArgs e)
        {
            fContainer.Navigate(new System.Uri("Vista.UserControls/MRP.xaml", UriKind.RelativeOrAbsolute));

        }

        private void btnPlanAgregado_Click(object sender, RoutedEventArgs e)
        {
            fContainer.Navigate(new System.Uri("Vista.UserControls/PlaneacionAgregada.xaml", UriKind.RelativeOrAbsolute));

        }

        #endregion

        private void Grid_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            //this.DragMove();
        }

        private void GridNav_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            this.DragMove();

        }

    }
}
