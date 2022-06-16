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
    /// Lógica de interacción para VentanaDevs.xaml
    /// </summary>
    public partial class VentanaDevs : Window
    {
        public VentanaDevs()
        {
            InitializeComponent();
            txtdev1.Text = "<Desarrolladores>";
            txtdev2.Text = "</Desarrolladores>";

        }

        private void Minimizar_Click(object sender, RoutedEventArgs e)
        {
            WindowState = WindowState.Minimized;

        }

        private void Cerrar_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void Border_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            this.DragMove();
        }
    }
}
