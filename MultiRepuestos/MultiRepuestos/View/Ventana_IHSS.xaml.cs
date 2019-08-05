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
using System.Data.SqlClient;

namespace MultiRepuestos.View
{
    /// <summary>
    /// Lógica de interacción para Ventana_IHSS.xaml
    /// </summary>
    public partial class Ventana_IHSS : Window
    {
        LinqToSqlDataClassesDataContext dataContext;
        SqlConnection conexion = new SqlConnection("Data Source = (local)\\SQLEXPRESS; Initial Catalog = PlanillaDePagoMensual; Integrated Security = True");


        public Ventana_IHSS()
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


            if (txtIHSS.Text == string.Empty)
            {
                MessageBox.Show("Debe ingresar el RAP");

            }
            else
            {


                try
                {

                    var Existe = (from r in dataContext.IHSS select r).ToList();

                    if (Existe.Count > 0)
                    {

                        try
                        {
                            string query = "UPDATE Planilla.IHSS SET SalarioTecho = @T WHERE Codigo = 1";

                            SqlCommand sqlCommand = new SqlCommand(query, conexion);

                            conexion.Open();


                            sqlCommand.Parameters.AddWithValue("@T", txtIHSS.Text);
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
                        dataContext.IHSS.InsertOnSubmit(new IHSS { SalarioTecho = Convert.ToDecimal(txtIHSS.Text),GastosMedicos= Convert.ToDecimal(8882.30), Fecha = DateTime.Now });

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
                    txtIHSS.Text = string.Empty;
                    Mostrar();
                }











            }
        }

        private void Mostrar()
        {
            var IH = (from a in dataContext.IHSS select a).First();
            lbIHSS.Text = IH.SalarioTecho.ToString();

        }
        private void Exite()
        {

            try
            {

                var Existe = (from e in dataContext.IHSS

                              select e).ToList();
                if (Existe.Count > 0)
                {
                    Mostrar();
                }
                else
                {

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }
    }
}
