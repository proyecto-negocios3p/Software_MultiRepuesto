﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
// Agregando los namespaces necesarios
using MultiRepuestos.View.Reportes;
using MultiRepuestos.View.ControlHora;


namespace MultiRepuestos.View
{
    /// <summary>
    /// Lógica de interacción para WindowContenedorPrincipal.xaml
    /// </summary>
    public partial class WindowContenedorPrincipal : Window
    {
        public WindowContenedorPrincipal()
        {
            InitializeComponent();
        }

        private void PanelFondo_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            DragMove();
        }

        private void BtnMin_Click(object sender, RoutedEventArgs e)
        {
           WindowState= WindowState.Minimized;
        }

        private void BtnMax_Click(object sender, RoutedEventArgs e)
        {
            WindowState = WindowState.Maximized;
            btnMax.Visibility = Visibility.Collapsed;
            btnRest.Visibility = Visibility.Visible;

        }

        private void BtnCerrar_Click(object sender, RoutedEventArgs e)
        {
         
          Application.Current.Shutdown();
        }

        private void BtnRest_Click(object sender, RoutedEventArgs e)
        {
            WindowState = WindowState.Normal;
            btnRest.Visibility = Visibility.Collapsed;
            btnMax.Visibility = Visibility.Visible;
        }

        private void B1_MouseEnter(object sender, MouseEventArgs e)
        {
            B1_Estado1.Visibility = Visibility.Collapsed;
            B1_Estado2.Visibility = Visibility.Visible;
            Thread.Sleep(20);
        }

        private void B1_MouseLeave(object sender, MouseEventArgs e)
        {
            B1_Estado1.Visibility = Visibility.Visible;
            B1_Estado2.Visibility = Visibility.Collapsed;
            Thread.Sleep(20);
        }

        private void B2_MouseEnter(object sender, MouseEventArgs e)
        {
            B2_Estado1.Visibility = Visibility.Collapsed;
            B2_Estado2.Visibility = Visibility.Visible;
            Thread.Sleep(20);
        }

        private void B2_MouseLeave(object sender, MouseEventArgs e)
        {
            B2_Estado1.Visibility = Visibility.Visible;
            B2_Estado2.Visibility = Visibility.Collapsed;
            Thread.Sleep(20);
        }

        private void BtnCerrarSesion_Click(object sender, RoutedEventArgs e)
        {
            MainWindow ventana = new MainWindow();
            ventana.Show();
            this.Close();
        }

        private void B3_MouseEnter(object sender, MouseEventArgs e)
        {
            B3_Estado1.Visibility = Visibility.Collapsed;
            B3_Estado2.Visibility = Visibility.Visible;
            Thread.Sleep(20);
        }

        private void B3_MouseLeave(object sender, MouseEventArgs e)
        {
            B3_Estado1.Visibility = Visibility.Visible;
            B3_Estado2.Visibility = Visibility.Collapsed;
            Thread.Sleep(20);
        }

        private void B4_MouseEnter(object sender, MouseEventArgs e)
        {
            B4_Estado1.Visibility = Visibility.Collapsed;
            B4_Estado2.Visibility = Visibility.Visible;
            Thread.Sleep(20);
        }

        private void B4_MouseLeave(object sender, MouseEventArgs e)
        {
            B4_Estado1.Visibility = Visibility.Visible;
            B4_Estado2.Visibility = Visibility.Collapsed;
            Thread.Sleep(20);
        }

        private void B5_MouseEnter(object sender, MouseEventArgs e)
        {
            B5_Estado1.Visibility = Visibility.Collapsed;
            B5_Estado2.Visibility = Visibility.Visible;
            Thread.Sleep(20);
        }

        private void B5_MouseLeave(object sender, MouseEventArgs e)
        {
            B5_Estado1.Visibility = Visibility.Visible;
            B5_Estado2.Visibility = Visibility.Collapsed;
            Thread.Sleep(20);
        }

        // Módulo de empleados 
        // Agregar empleado
        private void BtnAgregarEmpleado_Click(object sender, RoutedEventArgs e)
        {
            WindowAgregarEmpleado ventana = new WindowAgregarEmpleado();
            ventana.Show();
            this.Close();


        }

        // Actualizar empleados
        private void BtnActualizar_Click(object sender, RoutedEventArgs e)
        {
            ActualizarEmpleado ventana = new ActualizarEmpleado();
            ventana.Show();
            this.Close();
        }

        // Listar empleados
        private void BtnListarEmpleados_Click(object sender, RoutedEventArgs e)
        {
            WindowListarEmpleados ventana = new WindowListarEmpleados();
            ventana.Show();
            this.Close();
        }

        // Darde baja a empleados
        private void BtnDarDeBaja_Click(object sender, RoutedEventArgs e)
        {
            WindowDarDeBajaEmpleado ventana = new WindowDarDeBajaEmpleado();
            ventana.Show();
            this.Close();


        }
        // Fin módulo de empleados 

            private void BtnPlanillaBuscar_Click(object sender, RoutedEventArgs e)
            {
                Ventana_Buscar_Planilla ventana = new Ventana_Buscar_Planilla();
                ventana.Show();
            }

            private void BtnConfIHSS_Click(object sender, RoutedEventArgs e)
            {
                Ventana_IHSS ventana = new Ventana_IHSS();
                ventana.Show();
                ventana.Owner = this;

            }

            private void BtnConfRAP_Click(object sender, RoutedEventArgs e)
            {
                Ventana_RAP ventana = new Ventana_RAP();
                ventana.Show();
                ventana.Owner = this;
            }

            private void BtnConfPagoPorHora_Click(object sender, RoutedEventArgs e)
            {
                Ventana_Pago_Hora ventana = new Ventana_Pago_Hora();
                ventana.Show();
                ventana.Owner = this;
            }

         

            private void BtnAdmiUsuario_Click(object sender, RoutedEventArgs e)
            {
                Verificar_Empleado ventana = new Verificar_Empleado();
                ventana.Show();
                this.Close();

            }

        private void BtnPlanillaCrear_Click(object sender, RoutedEventArgs e)
        {
            Crear_Planilla ventana = new Crear_Planilla();

            ventana.Show();
            ventana.Owner = this;
        }

      

        // Módulo de Control de horas
        // Agregar horas extra
        private void AgregarHorasExtra_Click(object sender, RoutedEventArgs e)
        {
            AgregarHoraExtraTrabajada ventana = new AgregarHoraExtraTrabajada();
            ventana.Show();
            ventana.Owner = this;
        }

        // Agregar horas faltadas
        private void AgregarHorasFaltadas_Click(object sender, RoutedEventArgs e)
        {
            AgregarHoraFaltada ventana = new AgregarHoraFaltada();
            ventana.Show();
            ventana.Owner = this;
        }

        // Listar horas 
        private void ListarHoras_Click(object sender, RoutedEventArgs e)
        {
            ListarHoras ventana = new ListarHoras();
            ventana.Show();
            ventana.Owner = this;
        }

        private void BtnREmpleado_Click(object sender, RoutedEventArgs e)
        {
           ReporteEmpleados ventana = new ReporteEmpleados();
            ventana.Show();
            ventana.Owner = this;
        }

        private void BtnRHorasF_Click(object sender, RoutedEventArgs e)
        {
            ReporteHorasFaltadas ventana = new ReporteHorasFaltadas();
            ventana.Show();
            ventana.Owner = this;
        }

        private void BtnRPlanillas_Click(object sender, RoutedEventArgs e)
        {
            ReportPlanillas ventana = new ReportPlanillas();
            ventana.Show();
            ventana.Owner = this;
        }
    }
}
