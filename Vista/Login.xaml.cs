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
    /// Lógica de interacción para Login.xaml
    /// </summary>
    public partial class Login : Window
    {
        public Login()
        {
            InitializeComponent();
        }

        private void btnExit_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void Grid_MouseDown(object sender, MouseButtonEventArgs e)
        {
           
        }

        private void Border_MouseDown(object sender, MouseButtonEventArgs e)
        {

        }

        private void btnLogin_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                MainWindow ventanaMain = new MainWindow();
                ventanaMain.Show();
                this.Close();
            }
            catch (Exception ex)
            {

                MessageBox.Show("error: " + ex.ToString());
            }
           
        }

        private void btnRegister_Click(object sender, RoutedEventArgs e)
        {
            Registrar ventanaRegistrar = new Registrar();
            ventanaRegistrar.Show();
            this.Close();
        }

        private void Grid_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            this.DragMove();
        }

        private void txtUsername_TextChanged(object sender, TextChangedEventArgs e)
        {

        }
    }
}
