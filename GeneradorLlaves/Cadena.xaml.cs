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

namespace GeneradorLlaves
{
    /// <summary>
    /// Interaction logic for Candea.xaml
    /// </summary>
    public partial class Cadena : Window
    {
        public Cadena()
        {
            InitializeComponent();
        }

        private void btnDesencriptar_Click(object sender, RoutedEventArgs e)
        {
            txtResultado.Text = txtIngreso.Text.DesencriptarDatos();
        }

        private void btnEncriptar_Click(object sender, RoutedEventArgs e)
        {
            txtResultado.Text = txtIngreso.Text.EncriptarDatos();
        }
    }
}
