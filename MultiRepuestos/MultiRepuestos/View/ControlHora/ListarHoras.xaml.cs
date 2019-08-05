using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
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

namespace MultiRepuestos.View.ControlHora
{
    /// <summary>
    /// Lógica de interacción para ListarHoras.xaml
    /// </summary>
    public partial class ListarHoras : Window
    {
        LinqToSqlDataClassesDataContext dataContext;
        SqlConnection conexion = new SqlConnection("Data Source = (local)\\SQLEXPRESS; Initial Catalog = PlanillaDePagoMensual; Integrated Security = True");

        private DataTable tabla;
        // SqlConnection = new SqlConnection(connectionString);
        public ListarHoras()
        {
            InitializeComponent();

            dataContext = new LinqToSqlDataClassesDataContext(conexion);

            ListarEmpleados();
            ListarTipoHora();
            
        }

        private void ListarTipoHora()
        {
            throw new NotImplementedException();
        }

        private void ListarEmpleados()
        {
            throw new NotImplementedException();
        }

        private void MostrarHoras()
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

                    dgHoras.ItemsSource = tabla.DefaultView;
                    conexion.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
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
    }
}
