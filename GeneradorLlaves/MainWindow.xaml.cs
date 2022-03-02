using Newtonsoft.Json;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace GeneradorLlaves
{
    /// <summary>
    /// Lógica de interacción para MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        KeyModel keyModel = new KeyModel();
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            txtTextOuput.IsReadOnly = true;

        }

        private void btnGenerar_Click(object sender, RoutedEventArgs e)
        {
            if (rbtnEncript.IsChecked==true)
            {
                string encriptar;
                keyModel.Instancia = txtInstancia.Text.EncriptarDatos();
                keyModel.Nit = txtNit.Text;
                keyModel.NombreCliente = txtNombre.Text;
                keyModel.FechaVencimiento = txtFecha.Text;
                keyModel.Sistema = txtSistema.Text;
                object modelJson = keyModel;
                var result=JsonConvert.SerializeObject(modelJson);
                encriptar = result.EncriptarDatos();
                txtTextOuput.Text = encriptar;
            }else if (rbtnDecript.IsChecked == true)
            {

                if (!String.IsNullOrEmpty(txtTextOuput.Text))
                {
                    var Result = JsonConvert.DeserializeObject<KeyModel>(txtTextOuput.Text.DesencriptarDatos());
                    txtInstancia.Text = Result.Instancia.DesencriptarDatos();
                    txtNit.Text = Result.Nit;
                    txtNombre.Text = Result.NombreCliente;
                    txtFecha.Text = Result.FechaVencimiento;
                    txtSistema.Text = Result.Sistema;
                    //
                }
                else
                {
                    MessageBox.Show("Debe Ingresar una cadena encriptada", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }

            }
            else
            {
                MessageBox.Show("Debe Seleccionar una opcion","Error",MessageBoxButton.OK,MessageBoxImage.Error);
            }
            
        }

        private void btnLimpiar_Click(object sender, RoutedEventArgs e)
        {
            txtInstancia.Clear();
            txtNit.Clear();
            txtNombre.Clear();
            txtSistema.Clear();
            txtFecha.Clear();
            txtTextOuput.Clear();
            rbtnDecript.IsChecked = false;
            rbtnEncript.IsChecked = false;

        }

        private void rbtnDecript_Checked(object sender, RoutedEventArgs e)
        {
            txtTextOuput.IsReadOnly = false;
        }

        private void Text_Click(object sender, RoutedEventArgs e)
        {
            Cadena cadena = new Cadena();
           cadena.ShowDialog();
        }
    }
}
