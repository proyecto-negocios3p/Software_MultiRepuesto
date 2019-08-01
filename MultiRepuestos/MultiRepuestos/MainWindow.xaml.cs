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
using MultiRepuestos.View;

namespace MultiRepuestos
{
    /// <summary>
    /// Lógica de interacción para MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

      

        private void LoginBorder_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            DragMove();
        }

        private void BtnMin_Click(object sender, RoutedEventArgs e)
        {
            WindowState = WindowState.Minimized;
        }

        private void BtnCerrar_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void BtnIniciar_Click(object sender, RoutedEventArgs e)
        {
            WindowContenedorPrincipal ventana = new WindowContenedorPrincipal();
            ventana.Show();
            this.Close();
        }

        private void BtnMostrarPwd_Click(object sender, RoutedEventArgs e)
        {
            pwbMostrarContraseña.Text = pwbContraseña.Password;

            pwbContraseña.Visibility = Visibility.Collapsed;
            pwbMostrarContraseña.Visibility = Visibility.Visible;
            

            btnMostrarPwd.Visibility = Visibility.Hidden;
            btnOcultarPwd.Visibility = Visibility.Visible;
        }

        private void BtnOcultarPwd_Click(object sender, RoutedEventArgs e)
        {
           
            pwbContraseña.Visibility = Visibility.Visible;
            pwbMostrarContraseña.Visibility = Visibility.Collapsed;
            

            btnMostrarPwd.Visibility = Visibility.Visible;
            btnOcultarPwd.Visibility = Visibility.Hidden;
           
        }

      
    }
}
