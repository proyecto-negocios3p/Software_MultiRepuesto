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
            tabla = new DataTable();
            try
            {
                conexion.Open();
                string query = "SELECT * FROM Planilla.PorcentajeHoraExtra";
                SqlDataAdapter adapter = new SqlDataAdapter(query, conexion);
                using (adapter)
                {
                    adapter.Fill(tabla);
                    cbTipo.DisplayMemberPath = "TipoHora";
                    cbTipo.SelectedValuePath = "Codigo";
                    cbTipo.ItemsSource = tabla.DefaultView;
                    conexion.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void ListarEmpleados()
        {
            tabla = new DataTable();
            try
            {
                conexion.Open();
                string query = "SELECT * FROM Planilla.Empleado WHERE Estado =1";
                SqlDataAdapter adapter = new SqlDataAdapter(query, conexion);
                using (adapter)
                {
                    adapter.Fill(tabla);
                    cbIdentidad.DisplayMemberPath="Identidad";
                    cbIdentidad.SelectedValuePath = "Identidad";
                    cbIdentidad.ItemsSource = tabla.DefaultView;
                    conexion.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void MostrarHoras()
        {
            tabla = new DataTable();
            try
            {
                conexion.Open();
                string query = "SELECT E.Identidad, E.Nombre, E.Apellido,H.TotalHora AS Horas,H.Fecha FROM Planilla.Empleado AS E INNER JOIN Planilla.HoraExtra AS H ON E.Identidad =H.IdentidadEmpleado WHERE @id=H.IdentidadEmpleado AND @tipo=H.CodigoPorcentajeHoraExtra";
                SqlCommand sqlCommand = new SqlCommand(query, conexion);
                SqlDataAdapter adapter = new SqlDataAdapter(sqlCommand);
                sqlCommand.Parameters.AddWithValue("@id", cbIdentidad.SelectedValue.ToString());
                sqlCommand.Parameters.AddWithValue("@Tipo", cbTipo.SelectedValue.ToString());
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

        private void BtnListar_Click(object sender, RoutedEventArgs e)
        {
            if (cbIdentidad.SelectedValue == null && cbTipo.SelectedValue == null)
            {
                MessageBox.Show("Debe seleccionar un empleado y un tipo de hora");
            }
            else if (cbIdentidad.SelectedValue == null)
            {
                MessageBox.Show("Debe seleccionar un empleado");
            }
            else if (cbTipo.SelectedValue == null)
            {
                MessageBox.Show("Debe seleccionar un tipo de hora");
            }
            else
            {
                MostrarHoras();
            }
           
        }
    }
}
