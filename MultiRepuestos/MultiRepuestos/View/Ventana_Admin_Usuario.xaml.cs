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
using System.Windows.Shapes;

namespace MultiRepuestos.View
{
    /// <summary>
    /// Lógica de interacción para Ventana_Admin_Usuario.xaml
    /// </summary>
    public partial class Ventana_Admin_Usuario : Window
    {
        LinqToSqlDataClassesDataContext dataContext;

        public Ventana_Admin_Usuario()
        {
            InitializeComponent();
            // El string de conexión
            string connectionString = ConfigurationManager.ConnectionStrings["MultiRepuestos.Properties.Settings.PlanillaDePagoMensualConnectionString"].ConnectionString;

            // Conectar Linq con el string de conexión
            dataContext = new LinqToSqlDataClassesDataContext(connectionString);
        }

        private void BtnCerrar_Click(object sender, RoutedEventArgs e)
        {
            WindowContenedorPrincipal ventana =new WindowContenedorPrincipal();
            ventana.Show();
            this.Close();
        }

        private void BtnMin_Click(object sender, RoutedEventArgs e)
        {
            WindowState = WindowState.Minimized;
        }

        private void BarraBusperior_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            DragMove();
        }

        public void ListarEmpleados()
        {
            //var Lista = (from E in dataContext.Empleado  select E).ToList();
            var Lista = from e in dataContext.Empleado
                                 // where client.direccion == txtBuscar.Text
                                  select new { e.Nombre ,e.Identidad };
            dgEmpleados.ItemsSource = Lista.ToList();
            //MessageBox.Show(Lista.ToString());



            dgEmpleados.ItemsSource = Lista;
            
        }

        private void BtnActualizarLista_Click(object sender, RoutedEventArgs e)
        {
            ListarEmpleados();
        }
    }
}
