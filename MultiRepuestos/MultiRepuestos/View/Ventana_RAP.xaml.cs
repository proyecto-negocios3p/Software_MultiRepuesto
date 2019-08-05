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
    /// Lógica de interacción para Ventana_RAP.xaml
    /// </summary>
    public partial class Ventana_RAP : Window
    {
        LinqToSqlDataClassesDataContext dataContext;
        SqlConnection conexion = new SqlConnection("Data Source = (local)\\SQLEXPRESS; Initial Catalog = PlanillaDePagoMensual; Integrated Security = True");

        public Ventana_RAP()
        {
            InitializeComponent();
            dataContext = new LinqToSqlDataClassesDataContext(conexion);
            Exite();
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
            if (txtRAP.Text == string.Empty)
            {
                MessageBox.Show("Debe ingresar el RAP");

            }
            else
            {


                try
                {

                    var Existe = (from r in dataContext.RAP select r).ToList();

                    if (Existe.Count > 0)
                    {
                       
                            try
                            {
                                string query = "UPDATE Planilla.RAP SET Techo = @T WHERE Codigo = 1";

                                SqlCommand sqlCommand = new SqlCommand(query, conexion);

                                conexion.Open();

                              
                                sqlCommand.Parameters.AddWithValue("@T", txtRAP.Text);
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
                    else
                    {
                        dataContext.RAP.InsertOnSubmit(new RAP { Techo= Convert.ToDecimal(txtRAP.Text), Fecha = DateTime.Now });

                        dataContext.SubmitChanges();
                        Mostrar();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }
                finally
                {
                    MessageBox.Show("Se actualizo con exito");
                    txtRAP.Text = string.Empty;
                    Mostrar();
                }








          
            
                
            }
        }

        private void Mostrar()
        {
          
            var IH = (from a in dataContext.RAP select a).First();
            lbRAP.Text = IH.Techo.ToString();
        }

        private void Exite()
        {
            
            try
            {
               
                var Existe = (from e in dataContext.RAP
                        
                            select e).ToList();
               if (Existe.Count > 0)
                {
                    Mostrar();
                }
                else
                {

                }
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }
    }
}
