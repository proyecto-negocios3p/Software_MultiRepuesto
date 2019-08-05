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



namespace MultiRepuestos.View
{
    /// <summary>
    /// Lógica de interacción para WindowAgregarEmpleado.xaml
    /// </summary>
    public partial class WindowAgregarEmpleado : Window
    {
        LinqToSqlDataClassesDataContext dataContext;
        SqlConnection conexion = new SqlConnection("Data Source = (local)\\SQLEXPRESS; Initial Catalog = PlanillaDePagoMensual; Integrated Security = True");

        private DataTable tabla;
        // SqlConnection = new SqlConnection(connectionString);
       
        public WindowAgregarEmpleado()
        {
            InitializeComponent();
            dataContext = new LinqToSqlDataClassesDataContext(conexion);

            MostrarCargos();
        }

        private void MostrarCargos()
        {
            tabla = new DataTable();
            try
            {
                conexion.Open();
                string query = "SELECT * FROM Planilla.Cargo";
                SqlDataAdapter adapter = new SqlDataAdapter(query, conexion);
                using (adapter)
                {
                    adapter.Fill(tabla);
                    cmbCargo.DisplayMemberPath = "Nombre";
                    cmbCargo.SelectedValuePath = "Codigo";
                    cmbCargo.ItemsSource = tabla.DefaultView;
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

        private void BtnMin_Click(object sender, RoutedEventArgs e)
        {
            WindowState = WindowState.Minimized;
        }

        private void BtnAceptar_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                // Falta condición de campos vacios
                MessageBox.Show(Convert.ToString(cmbCargo.SelectedValue));
                DateTime now = DateTime.Now;
                dataContext.Empleado.InsertOnSubmit(new Empleado { Identidad = txtIdentidad.Text, Nombre = txtNombre.Text, Apellido = txtApellido.Text, CodigoCargo = cmbCargo.SelectedValue.ToString(), Genero = Convert.ToChar(cmbGenero.Text) , SueldoOrdinario = Convert.ToDecimal(txtSueldoBase.Text), NivelAcademico = cmbAcademico.Text, Fecha = now, Estado = true});

                dataContext.SubmitChanges();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            finally
            {
                MessageBox.Show("El empleado se agregó corretamente");
            }

        }

        private void BtnCancelar_Click(object sender, RoutedEventArgs e)
        {
            WindowAgregarEmpleado ventana = new WindowAgregarEmpleado();
            ventana.Show();
            this.Close();
        }

    }
}
