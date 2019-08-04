using System;
using System.Collections.Generic;
using System.Configuration;
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
    /// 
   
    public partial class MainWindow : Window
    {
        LinqToSqlDataClassesDataContext dataContext;

        public MainWindow()
        {
            InitializeComponent();

            // El string de conexión
            string connectionString = ConfigurationManager.ConnectionStrings["MultiRepuestos.Properties.Settings.PlanillaDePagoMensualConnectionString"].ConnectionString;

            // Conectar Linq con el string de conexión
            dataContext = new LinqToSqlDataClassesDataContext(connectionString);

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


            try
            {
           
                var User = (from c in dataContext.Usuario where c.Usuario1 == txtUsuario.Text  select c).ToList();
                var contra = (from c in dataContext.Usuario where c.Contraseña == pwbContraseña.Password select c).ToList();

              
                if(contra.Count>0 &&User.Count>0)
                { 
                
                    WindowContenedorPrincipal ventana = new WindowContenedorPrincipal();
                    ventana.Show();
                    this.Close();
                }
                else
                {
                    MessageBox.Show("Los Datos Ingresados son incorrectos");
                }
               
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            
           
           
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
