using System;
using System.Collections.Generic;
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
    /// Lógica de interacción para Ventana_Usuario.xaml
    /// </summary>
    public partial class Ventana_Usuario : Window
    {
        LinqToSqlDataClassesDataContext dataContext;
        string id = null;
        SqlConnection conexion = new SqlConnection("Data Source = (local)\\SQLEXPRESS; Initial Catalog = PlanillaDePagoMensual; Integrated Security = True");

        public Ventana_Usuario(string Identidad)
        {
            InitializeComponent();
            dataContext = new LinqToSqlDataClassesDataContext(conexion);
            id = Identidad;
            txtIdentidad.Text = Identidad;
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

        private void BarraBusperior_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            DragMove();
        }

        private void BtnAgregar_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                dataContext.Usuario.InsertOnSubmit(new Usuario { Usuario1  = txtUser.Text, Contraseña = txtPass.Text, IdentidadEmpleado=id,Estado=true,Fecha=DateTime.Now});

                dataContext.SubmitChanges();
                
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            finally
            {
                MessageBox.Show(String.Format("Se Agrego al usuario"));
            }
        }

      
    }
}
