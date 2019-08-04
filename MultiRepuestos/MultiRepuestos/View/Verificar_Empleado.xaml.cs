using System;
using System.Collections.Generic;
using System.Configuration;
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
    /// Lógica de interacción para Verificar_Empleado.xaml
    /// </summary>
    public partial class Verificar_Empleado : Window
    {
        LinqToSqlDataClassesDataContext dataContext;


        public Verificar_Empleado()
        {
            
                InitializeComponent();

                // El string de conexión
                string connectionString = ConfigurationManager.ConnectionStrings["MultiRepuestos.Properties.Settings.PlanillaDePagoMensualConnectionString"].ConnectionString;

                // Conectar Linq con el string de conexión
                dataContext = new LinqToSqlDataClassesDataContext(connectionString);

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

                var User = (from c in dataContext.Usuario where c.Usuario1 == txtUser.Text select c).ToList();
                var contra = (from c in dataContext.Usuario where c.Contraseña == pwdContra.Password select c).ToList();


                if (contra.Count > 0 && User.Count > 0)
                {

                    Ventana_Admin_Usuario ventana = new Ventana_Admin_Usuario();
                    ventana.Show();
                    this.Close();
                }
                else
                {
                    MessageBox.Show("Los Datos Ingresados son incorrectos");
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }

           
        }
    }
}
