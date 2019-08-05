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

namespace MultiRepuestos.View.Reportes
{
    /// <summary>
    /// Lógica de interacción para ReporteEmpleados.xaml
    /// </summary>
    public partial class ReporteEmpleados : Window
    {
        LinqToSqlDataClassesDataContext dataContext;
        SqlConnection conexion = new SqlConnection("Data Source = (local)\\SQLEXPRESS; Initial Catalog = PlanillaDePagoMensual; Integrated Security = True");

        private DataTable tabla;
        public ReporteEmpleados()
        {
            InitializeComponent();
            MostrarEmpleados();
        }
        private void BtnCerrar_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void BtnMin_Click(object sender, RoutedEventArgs e)
        {
            WindowState = WindowState.Minimized;
        }

        private void MostrarEmpleados()
        {
            tabla = new DataTable();
            try
            {
                conexion.Open();
                string query = "SELECT E.Identidad, E.Nombre, E.Apellido, C.Nombre AS Cargo, E.Genero, E.SueldoOrdinario AS Sueldo, E.NivelAcademico,E.Estado FROM Planilla.Empleado AS E INNER JOIN Planilla.Cargo AS C ON E.CodigoCargo=C.Codigo";
                SqlDataAdapter adapter = new SqlDataAdapter(query, conexion);
                using (adapter)
                {
                    adapter.Fill(tabla);

                    dgEmpleados.ItemsSource = tabla.DefaultView;
                    conexion.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

      

        private void BtnActualizarLista_Click(object sender, RoutedEventArgs e)
        {

        }

        private void CbIdentidad_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void BarraBusperior_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            DragMove();
        }
    }
}
