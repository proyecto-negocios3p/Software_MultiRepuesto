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
    /// Lógica de interacción para Ventana_Pago_Hora.xaml
    /// </summary>
    public partial class Ventana_Pago_Hora : Window
    {
        LinqToSqlDataClassesDataContext dataContext;
        SqlConnection conexion = new SqlConnection("Data Source = (local)\\SQLEXPRESS; Initial Catalog = PlanillaDePagoMensual; Integrated Security = True");

        private DataTable tabla;
        // SqlConnection = new SqlConnection(connectionString);
      
        public Ventana_Pago_Hora()
        {
            InitializeComponent();
             dataContext = new LinqToSqlDataClassesDataContext(conexion);
            mostrarCargos();
        }
        private void BtnCerrar_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void BtnMin_Click(object sender, RoutedEventArgs e)
        {
            WindowState = WindowState.Minimized;
        }

        private void BtnActualizar_Click(object sender, RoutedEventArgs e)
        {
            if (cbCargo.SelectedValue == null)
            {
                MessageBox.Show("Debes escoger un cargo antes de actualizarlo.");
                
            }
            else
            {
                try
                {
                    string query = "UPDATE Planilla.Cargo SET Codigo = @Cod,Nombre=@Nom WHERE Codigo = @CodId";

                    SqlCommand sqlCommand = new SqlCommand(query, conexion);

                    conexion.Open();

                    sqlCommand.Parameters.AddWithValue("@Nom", txtNombre.Text);
                    sqlCommand.Parameters.AddWithValue("@Cod", txtCodigo.Text);
                    sqlCommand.Parameters.AddWithValue("@CodId", cbCargo.SelectedValue.ToString());
                    sqlCommand.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }
                finally
                {
                    conexion.Close();
                    mostrarCargos();
                    txtCodigo.Text = String.Empty;
                    txtNombre.Text = String.Empty;
                }
            }
        }

        private void BtnAgregar_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                dataContext.Cargo.InsertOnSubmit(new Cargo { Codigo = txtCodigo.Text,Nombre=txtNombre.Text,Fecha= DateTime.Now });
    
                dataContext.SubmitChanges();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void mostrarCargos()
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
                    cbCargo.DisplayMemberPath = "Nombre";
                    cbCargo.SelectedValuePath = "Codigo";
                    cbCargo.ItemsSource = tabla.DefaultView;
                    conexion.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        
        

        }

        private void CbCargo_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

            if (cbCargo.SelectedItem == null)
            {

            }
            else
            {
                try
                {
                    string id = cbCargo.SelectedValue.ToString();
                    //  MessageBox.Show(id);
                    var Cargo = (from c in dataContext.Cargo
                                 where c.Codigo == id
                                 select c).Single();

                    txtCodigo.Text = Cargo.Codigo;
                    txtNombre.Text = Cargo.Nombre;



                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }
            }
        }
    }
}
