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
    /// Lógica de interacción para WindowDarDeBajaEmpleado.xaml
    /// </summary>
    public partial class WindowDarDeBajaEmpleado : Window
    {
        LinqToSqlDataClassesDataContext dataContext;
        SqlConnection conexion = new SqlConnection("Data Source = (local)\\SQLEXPRESS; Initial Catalog = PlanillaDePagoMensual; Integrated Security = True");
        private DataTable tabla;
        public WindowDarDeBajaEmpleado()
        {

            InitializeComponent();
            dataContext = new LinqToSqlDataClassesDataContext(conexion);
            BuscarEmpleado();
        }

        private void BarraSuperior_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            DragMove();
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

        private void BtnCancelar_Click(object sender, RoutedEventArgs e)
        {

        }

        private void BtnAceptar_Click(object sender, RoutedEventArgs e)
        {
            //   < ComboBoxItem Content = "Despido" />

            //    < ComboBoxItem Content = "Preaviso" />
            string i = ((ComboBoxItem)cbTipo.SelectedItem).Content.ToString();


            //if (cbIdentidad.SelectedValue == null || cbTipo.SelectedValue == null)
            //{
            //    MessageBox.Show("Debe seleccionar una Identidad y un tipo.");
            //}
            //else if (i == "Despido")
            //{
            //    //MessageBox.Show("Despido");
            //  //  despido();
            //}
            //else if (i == "Preaviso")
            //{
            //    //  MessageBox.Show("Preaviso");
            //}
            //else
            //{

            //}


            if (cbIdentidad.SelectedValue == null)
            {
                MessageBox.Show("Debes escoger un cargo antes de actualizarlo.");

            }
            else
            {
                try
                {
                    string query = "UPDATE Planilla.Empleado SET Estado=@Estado WHERE Identidad = @Id";

                    SqlCommand sqlCommand = new SqlCommand(query, conexion);

                    conexion.Open();

                    sqlCommand.Parameters.AddWithValue("@Estado", 0);
               
                    sqlCommand.Parameters.AddWithValue("@Id", cbIdentidad.SelectedValue.ToString());
                    sqlCommand.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }
                finally
                {
                    conexion.Close();
                    MessageBox.Show("Se dio de baja al empleado");
            
                }
                try
                {
                    string query = "UPDATE Planilla.Usuario SET Estado=@Estado WHERE IdentidadEmpleado = @Id";

                    SqlCommand sqlCommand = new SqlCommand(query, conexion);

                    conexion.Open();

                    sqlCommand.Parameters.AddWithValue("@Estado", 0);

                    sqlCommand.Parameters.AddWithValue("@Id", cbIdentidad.SelectedValue.ToString());
                    sqlCommand.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }
                finally
                {
                    conexion.Close();
                    MessageBox.Show("Se dio de baja al usuario");

                }
            }

        }

        private void despido()
        {
            decimal sueldoOrdinario = 0;
            string fechaIngCompleta = "";
        
            string fechaSalidaCompleta = DateTime.Now.ToString("dd/MM/yy");



            int Dias = 0, Meses = 0, Años = 0;
            string id = cbIdentidad.SelectedValue.ToString();

            #region obtener Sueldo
            try
            {
              
                var Empleado = (from c in dataContext.Empleado
                             where c.Identidad == id
                             select c).Single();

                sueldoOrdinario = Empleado.SueldoOrdinario;
               // fechaIngCompleta = Empleado.Fecha.ToString();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            #endregion

            #region Obtener Fecha Inicial
            
            try
            {
                string query = "SELECT Fecha FROM Planilla.Empleado WHERE Identidad=@id";

                conexion.Open();
                SqlCommand sqlCommand = new SqlCommand(query, conexion);
                // Reemplazar el parámetro con su valor respectivo
                sqlCommand.Parameters.AddWithValue("@id", id);
                sqlCommand.ExecuteNonQuery();
                SqlDataReader reader = sqlCommand.ExecuteReader();

                if (reader.Read())
                {

                    fechaIngCompleta= reader["Fecha"].ToString();


                }

            }
            catch (Exception e)
            {
                MessageBox.Show(e.ToString());
            }
            finally
            {
                conexion.Close();
            }
            #endregion

            #region Calcular dias meses y años

            MessageBox.Show(fechaIngCompleta);
            MessageBox.Show(fechaSalidaCompleta);

            #endregion

        }



        private void CbNombre_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            llenarCampos();
        }
        private void BuscarEmpleado()
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
                    cbIdentidad.DisplayMemberPath = "Identidad";

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
        private void llenarCampos()
        {
            if (cbIdentidad.SelectedValue != null) { }
            try
            {

                string id = cbIdentidad.SelectedValue.ToString();

                var Emp = (from e in dataContext.Empleado
                           where e.Identidad == id
                           select e).First();





                txtApellido.Text = Emp.Apellido;
                txtNombre.Text = Emp.Nombre;


            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        
        }

    }
}
