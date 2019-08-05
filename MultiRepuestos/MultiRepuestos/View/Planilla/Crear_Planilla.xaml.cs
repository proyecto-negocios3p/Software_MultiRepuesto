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
    /// Lógica de interacción para Crear_Planilla.xaml
    /// </summary>
    public partial class Crear_Planilla : Window
    {
        LinqToSqlDataClassesDataContext dataContext;
        SqlConnection conexion = new SqlConnection("Data Source = (local)\\SQLEXPRESS; Initial Catalog = PlanillaDePagoMensual; Integrated Security = True");
        private DataTable tabla;
        public Crear_Planilla()
        {
            InitializeComponent();

            dataContext = new LinqToSqlDataClassesDataContext(conexion);

            MostrarIdentidades();

            txtFecha.Text = DateTime.Now.ToString();
        }

        private void MostrarIdentidades()
        {
            tabla = new DataTable();
            try
            {
                conexion.Open();
                string query = "SELECT * FROM Planilla.Empleado WHERE Estado = 1";
                SqlDataAdapter adapter = new SqlDataAdapter(query, conexion);
                using (adapter)
                {
                    adapter.Fill(tabla);
                    cmbIdentidad.DisplayMemberPath = "Identidad";
                    cmbIdentidad.SelectedValuePath = "Identidad";
                    cmbIdentidad.ItemsSource = tabla.DefaultView;
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
            WindowState = WindowState.Minimized;
        }

        private void BtnMin_Click(object sender, RoutedEventArgs e)
        {
            WindowState = WindowState.Minimized;
        }

        private void BtnCerrar_Click(object sender, RoutedEventArgs e)
        {
            WindowContenedorPrincipal ventana = new WindowContenedorPrincipal();
            ventana.Show();
            this.Close();
        }

 

        private void BtnCancelar_Click(object sender, RoutedEventArgs e)
        {
            Crear_Planilla ventana = new Crear_Planilla();
            ventana.Show();
            this.Close();
        }

        private void BtnAceptar_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Se generó la planilla del empleado seleccionado correctamente");
        }
    }
}
