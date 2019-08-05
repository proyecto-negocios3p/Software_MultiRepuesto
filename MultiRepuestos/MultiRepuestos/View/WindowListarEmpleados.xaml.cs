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
// Agregando los namespaces necesarios para SQL Server
using System.Configuration;
using System.Data.SqlClient;
using System.Data;

namespace MultiRepuestos.View
{
    /// <summary>
    /// Lógica de interacción para WindowListarEmpleados.xaml
    /// </summary>
    public partial class WindowListarEmpleados : Window
    {
        LinqToSqlDataClassesDataContext dataContext;
        SqlConnection conexion = new SqlConnection("Data Source = (local)\\SQLEXPRESS; Initial Catalog = PlanillaDePagoMensual; Integrated Security = True");

        private DataTable tabla;
        // SqlConnection = new SqlConnection(connectionString);

        public WindowListarEmpleados()
        {

            InitializeComponent();

            dataContext = new LinqToSqlDataClassesDataContext(conexion);

            // Llenar el ListView de Zoológicos
            MostrarEmpleados();
        }


        private void BarraSuperior_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            DragMove();
        }

        private void BtnMin_Click(object sender, RoutedEventArgs e)
        {
            WindowState = WindowState.Minimized;
        }

        private void BtnMax_Click(object sender, RoutedEventArgs e)
        {
            WindowState = WindowState.Maximized;
            btnMax.Visibility = Visibility.Collapsed;
            btnRest.Visibility = Visibility.Visible;
        }

        private void BtnRest_Click(object sender, RoutedEventArgs e)
        {
            WindowState = WindowState.Normal;
            btnRest.Visibility = Visibility.Collapsed;
            btnMax.Visibility = Visibility.Visible;
        }

        private void BtnCerrar_Click(object sender, RoutedEventArgs e)
        {
            WindowContenedorPrincipal ventana = new WindowContenedorPrincipal();
            ventana.Show();
            this.Close();
        }

        // Métodos y propiedades
        private void MostrarEmpleados()
        {
            tabla = new DataTable();
            try
            {
                conexion.Open();
                string query = "SELECT E.Identidad, E.Nombre, E.Apellido, C.Nombre AS Cargo, E.Genero, E.Fecha, E.SueldoOrdinario AS Sueldo, E.NivelAcademico FROM Planilla.Empleado AS E INNER JOIN Planilla.Cargo AS C ON E.CodigoCargo=C.Codigo";
                SqlDataAdapter adapter = new SqlDataAdapter(query, conexion);
                using (adapter)
                {
                    adapter.Fill(tabla);

                    lvEmpleados.ItemsSource = tabla.DefaultView;
                    conexion.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }
    }
}
