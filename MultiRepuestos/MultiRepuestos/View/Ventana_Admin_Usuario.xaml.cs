using System;
using System.Collections.Generic;
using System.Configuration;
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
    /// Lógica de interacción para Ventana_Admin_Usuario.xaml
    /// </summary>
    public partial class Ventana_Admin_Usuario : Window
    {

        LinqToSqlDataClassesDataContext dataContext;
        SqlConnection conexion = new SqlConnection("Data Source = (local)\\SQLEXPRESS; Initial Catalog = PlanillaDePagoMensual; Integrated Security = True");

        private DataTable tabla;

        public Ventana_Admin_Usuario()
        {
            InitializeComponent();
            dataContext = new LinqToSqlDataClassesDataContext(conexion);

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
            mostrarEmpleados();
            
        }

        private void BtnActualizarLista_Click(object sender, RoutedEventArgs e)
        {
            ListarEmpleados();
        }
        private void mostrarEmpleados()
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
                    dgEmpleados.DisplayMemberPath = "Identidad";
                    
                    dgEmpleados.SelectedValuePath = "Identidad";
                    dgEmpleados.ItemsSource = tabla.DefaultView;
                    conexion.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }



        }
        private void BuscarEmpleado()
        {
            string id = dgEmpleados.SelectedValue.ToString();
            MessageBox.Show(id.ToString());
            try
            {

                var User = (from u in dataContext.Usuario
                            where u.IdentidadEmpleado == id
                            select u).First();


                var Emp = (from e in dataContext.Empleado
                           where e.Identidad == id
                           select e).First();

                txtUser.Text = User.Usuario1;
                txtContra.Text = User.Contraseña;

                txtIdentidad.Text = Emp.Identidad;
                txtNombre.Text = Emp.Nombre;
                txtApellido.Text = Emp.Apellido;


            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void DgEmpleados_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            BuscarEmpleado();
        }
    }
}
