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

namespace MultiRepuestos.View
{
    /// <summary>
    /// Lógica de interacción para Verificar_Empleado.xaml
    /// </summary>
    public partial class Verificar_Empleado : Window
    {
        public Verificar_Empleado()
        {
            InitializeComponent();
        }
        private void BtnCerrar_Click(object sender, RoutedEventArgs e)
        {
           
            WindowContenedorPrincipal ventana = new WindowContenedorPrincipal();
            ventana.Show();
            this.Close();
        }

        private void BtnMin_Click(object sender, RoutedEventArgs e)
        {
            WindowState = WindowState.Minimized;
        }

        private void BtnAceptar_Click(object sender, RoutedEventArgs e)
        {
           
            Ventana_Admin_Usuario ventana = new Ventana_Admin_Usuario();
            ventana.Show();
            this.Close();
        }
    }
}
