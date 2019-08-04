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
        
        public Ventana_IHSS()
        {
            InitializeComponent();
            Mostrar();
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
                MessageBox.Show("Debe ingresar el IHSS");

            }
            else
            {
                 var db  = new conexionlinqIHSSDataContext();
                var IH = (from a in db.IHSS where a.SalarioTecho == a.SalarioTecho select a).Single();

                IH.SalarioTecho = Convert.ToDecimal(txtIHSS.Text);
                db.SubmitChanges();
                MessageBox.Show("Se ah actualizado con exito");
                txtIHSS.Text = string.Empty;
                Mostrar();
            }
        }

        private void Mostrar()
        {
            var db = new conexionlinqIHSSDataContext();
            var IH = (from a in db.IHSS select a).First();
            txtIHSSMostrar.Text = IH.SalarioTecho.ToString();
            
        }
    }
}
