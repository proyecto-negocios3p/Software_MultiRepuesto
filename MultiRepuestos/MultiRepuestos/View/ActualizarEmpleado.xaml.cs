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
using System.Data;
using System.Data.SqlClient;


namespace MultiRepuestos.View
{
    /// <summary>
    /// Lógica de interacción para ActualizarEmpleado.xaml
    /// </summary>
    public partial class ActualizarEmpleado : Window
    {
        LinqToSqlDataClassesDataContext dataContext;
        SqlConnection conexion = new SqlConnection("Data Source = (local)\\SQLEXPRESS; Initial Catalog = PlanillaDePagoMensual; Integrated Security = True");

        private DataTable tabla;
        // SqlConnection = new SqlConnection(connectionString);


        public ActualizarEmpleado()
        {
            InitializeComponent();

            dataContext = new LinqToSqlDataClassesDataContext(conexion);

            MostrarCargos();
            MostrarIdentidades();
        }

        private void MostrarIdentidades()
        {
            tabla = new DataTable();
            try
            {
                conexion.Open();
                string query = "SELECT * FROM Planilla.Empleado";
                SqlDataAdapter adapter = new SqlDataAdapter(query, conexion);
                using (adapter)
                {
                    adapter.Fill(tabla);
                    cmbNombre.DisplayMemberPath = "Nombre";
                    cmbNombre.SelectedValuePath = "Identidad";
                    cmbNombre.ItemsSource = tabla.DefaultView;
                    conexion.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }

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

        private void BtnAceptar_Click(object sender, RoutedEventArgs e)
        {
            ActEmpleado();
        }

        private void BtnCancelar_Click(object sender, RoutedEventArgs e)
        {

        }

        private void CmbNombre_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            string id = cmbNombre.SelectedValue.ToString();

            var Empleado = (from c in dataContext.Empleado
                         where c.Identidad == id
                         select c).Single();

            txtApellido.Text = Empleado.Apellido;
            cmbCargo.Text = Empleado.CodigoCargo.ToString();
            txtIdentidad.Text = Empleado.Identidad;
            txtSueldoBase.Text = Empleado.SueldoOrdinario.ToString();
            cmbAcademico.Text = Empleado.NivelAcademico.ToString();
            cmbGenero.Text = Empleado.Genero.ToString();
 
        }

        string idCarg = " ";
        private void ActEmpleado()
        {
            
            // Totales
            try
            {

                SqlCommand Comando = new SqlCommand(@"Select Codigo from Planilla.Cargo Where Nombre = @nomb", conexion);
                Comando.Parameters.Add("@nomb", SqlDbType.NVarChar);
                Comando.Parameters["@nomb"].Value = cmbCargo.Text;
                

                conexion.Open();
                idCarg = Convert.ToString(Comando.ExecuteScalar());



            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            } 
            finally
            {
                conexion.Close();
            }


            ///
            try
            {
                string query = "UPDATE Planilla.Empleado SET Nombre = @Nom, Apellido=@Ape, CodigoCargo = @codCarg, Genero = @gene, sueldoOrdinario = @sueld, NivelAcademico = @NivAc  WHERE Identidad = @Ident";

                SqlCommand sqlCommand = new SqlCommand(query, conexion);

                conexion.Open();

                sqlCommand.Parameters.AddWithValue("@Nom", cmbNombre.Text);
                sqlCommand.Parameters.AddWithValue("@Ape", txtApellido.Text);
                sqlCommand.Parameters.AddWithValue("@gene", cmbGenero.Text);
                sqlCommand.Parameters.AddWithValue("@sueld", txtSueldoBase.Text);
                sqlCommand.Parameters.AddWithValue("@NivAc", cmbAcademico.Text);
                sqlCommand.Parameters.AddWithValue("@Ident", txtIdentidad.Text);
                sqlCommand.Parameters.AddWithValue("@codCarg", idCarg);
                sqlCommand.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            finally
            {
                conexion.Close();
                
            }
            
        }
    }
}
