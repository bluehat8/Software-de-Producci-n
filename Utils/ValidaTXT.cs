using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace Software_de_Producción.Utils
{
    public class ValidaTXT
    {


        public ValidaTXT() { }

        public void Numeric_KeyDown(object sender, KeyEventArgs e, TextBox txt)
        {
            /* Aqui hago que sólo permita números*/
            if (e.Key >= Key.D0 && e.Key <= Key.D9 || e.Key >= Key.NumPad0 && e.Key <= Key.NumPad9 || e.Key == Key.OemPeriod)
            {

                if (e.Key == Key.OemPeriod && txt.Text.IndexOf('.') != -1)
                {
                    MessageBox.Show("Solo se permiten números.");

                    e.Handled = true;
                    return;
                }
                else
                {
                    e.Handled = false;
                }
            }
            else
            {
                MessageBox.Show("Solo se permiten numeros.");

                e.Handled = true;
            }

        }



        public void INT_KeyDown(object sender, KeyEventArgs e, TextBox txt)
        {
            /* Aqui hago que sólo permita números enteros*/
            ;
            if (e.Key >= Key.D0 && e.Key <= Key.D9 || e.Key >= Key.NumPad0 && e.Key <= Key.NumPad9 || e.Key == Key.OemPeriod)
            {
                if (e.Key == Key.OemPeriod)
                {
                    MessageBox.Show("Solo se permiten numeros enteros");

                    e.Handled = true;
                    return;
                }
                else
                {
                    e.Handled = false;
                }
            }
            else
            {
                MessageBox.Show("Solo se permiten numeros enteros");
                e.Handled = true;
            }

        }


        public void SPACE_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Space)
            {
                MessageBox.Show("No se permite espacios.");
                e.Handled = true;
            }
        }

        public void PreviewTextInputOnlyLetters(object sender, TextCompositionEventArgs e)
        {
            /*int character = Convert.ToInt32(Convert.ToChar(e.Text));
            if ((character >= 65 && character <= 90) || (character >= 97 && character <= 122) )
            {
                //e.Handled = false;
            }
            else
            {
                MessageBox.Show("No se permiten números o caractéres especiales", "Alert");
                e.Handled = true;
            }*/
        }
    }
}
