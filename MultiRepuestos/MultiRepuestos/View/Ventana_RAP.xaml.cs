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

namespace MultiRepuestos.View
{
    /// <summary>
    /// Lógica de interacción para Ventana_RAP.xaml
    /// </summary>
    public partial class Ventana_RAP : Window
    {
        public Ventana_RAP()
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
            if (txtRAP.Text == string.Empty)
            {
                MessageBox.Show("Debe ingresar el RAP");

            }
            else
            {
                var db = new ConexionLinqRAPDataContext();
                var IH = (from a in db.RAP where a.Techo == a.Techo select a).Single();

                IH.Techo = Convert.ToDecimal(txtRAP.Text);
                db.SubmitChanges();
                MessageBox.Show("Se ah actualizado con exito");
                txtRAP.Text = string.Empty;
                Mostrar();
            }
        }

        private void Mostrar()
        {
            var db = new ConexionLinqRAPDataContext();
            var IH = (from a in db.RAP where a.Techo == a.Techo select a).Single();
            lbRAP.Text = IH.Techo.ToString();
        }
    }
}
