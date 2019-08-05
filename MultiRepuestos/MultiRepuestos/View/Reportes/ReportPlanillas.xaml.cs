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
    /// Lógica de interacción para ReportPlanillas.xaml
    /// </summary>
    public partial class ReportPlanillas : Window
    {
        LinqToSqlDataClassesDataContext dataContext;
        SqlConnection conexion = new SqlConnection("Data Source = (local)\\SQLEXPRESS; Initial Catalog = PlanillaDePagoMensual; Integrated Security = True");

        private DataTable tabla;
        public ReportPlanillas()
        {
            InitializeComponent();
            MostrarPlanillas();
        }
        private void BtnCerrar_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void BtnMin_Click(object sender, RoutedEventArgs e)
        {
            WindowState = WindowState.Minimized;
        }

        private void CbIdentidad_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            MostrarPlanillas();
        }

        private void MostrarPlanillas()
        {
          
                tabla = new DataTable();
                try
                {
                    conexion.Open();
                    string query = "SELECT E.Identidad, E.Nombre, E.Apellido, E.Genero, P.SueldoOrdinario AS Sueldo,P.IHSS,P.RAP,P.HorasFaltadas,P.HorasExtras,P.SueldoNeto,P.CodigoPlanillaFinal FROM Planilla.Empleado AS E INNER JOIN Planilla.PlanillaFinal AS P ON E.Identidad=P.IdentidadEmpleado" ;
                    SqlCommand sqlCommand = new SqlCommand(query, conexion);
                    SqlDataAdapter adapter = new SqlDataAdapter(sqlCommand);
                 
                    using (adapter)
                    {
                        adapter.Fill(tabla);
                      
                        dgPlanillas.ItemsSource = tabla.DefaultView;
                        conexion.Close();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }
            
        }

       
        private void BarraBusperior_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            DragMove();
        }
    }
}
