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
    /// Lógica de interacción para AgregarHoraExtraTrabajada.xaml
    /// </summary>
    public partial class AgregarHoraExtraTrabajada : Window
    {
        LinqToSqlDataClassesDataContext dataContext;
        SqlConnection conexion = new SqlConnection("Data Source = (local)\\SQLEXPRESS; Initial Catalog = PlanillaDePagoMensual; Integrated Security = True");
        private DataTable tabla;

        public AgregarHoraExtraTrabajada()
        {
            InitializeComponent();

            dataContext = new LinqToSqlDataClassesDataContext(conexion);

            MostrarIdentidades();
            ListarTipoHoras();

        }

        private void ListarTipoHoras()
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
                    cmbHora.DisplayMemberPath = "TipoHora";
                    cmbHora.SelectedValuePath = "Codigo";
                    cmbHora.ItemsSource = tabla.DefaultView;
                    conexion.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
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

        private void BtnMin_Click(object sender, RoutedEventArgs e)
        {
            WindowState = WindowState.Minimized;
        }

        private void BtnCerrar_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void BtnAceptar_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                // Falta condición de campos vacios
                
                
                dataContext.HoraExtra.InsertOnSubmit(new HoraExtra { IdentidadEmpleado = cmbIdentidad.Text, TotalHora = int.Parse(txtHora.Text), Fecha = DateTime.Now, CodigoPorcentajeHoraExtra = cmbHora.SelectedValue.ToString()});

                dataContext.SubmitChanges();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            finally
            {
                MessageBox.Show("Se agregaron las horas extra al empleado");
            }
        }
    }

    
}
