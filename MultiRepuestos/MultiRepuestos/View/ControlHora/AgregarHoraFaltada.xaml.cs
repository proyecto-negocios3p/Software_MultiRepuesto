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
    /// Lógica de interacción para AgregarHoraFaltada.xaml
    /// </summary>
    public partial class AgregarHoraFaltada : Window
    {
        LinqToSqlDataClassesDataContext dataContext;
        SqlConnection conexion = new SqlConnection("Data Source = (local)\\SQLEXPRESS; Initial Catalog = PlanillaDePagoMensual; Integrated Security = True");
        private DataTable tabla;
        public AgregarHoraFaltada()
        {
            InitializeComponent();
            dataContext = new LinqToSqlDataClassesDataContext(conexion);

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
                dataContext.HoraFaltada.InsertOnSubmit(new HoraFaltada { IdentidadEmpleado = cmbIdentidad.Text, TotalHora = int.Parse(txtHorasFaltadas.Text), Fecha = DateTime.Now, Motivo = txtMotivo.Text});

                dataContext.SubmitChanges();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            finally
            {
                MessageBox.Show("Se agregaron las horas faltadas al empleado");
            }
        }
    }
}
