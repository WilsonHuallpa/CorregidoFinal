using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using Entidades;

namespace Final._2021.WinFormsApp
{
    ///Agregar manejo de excepciones en TODOS los lugares críticos!!!


    public partial class FrmPrincipal : Form
    {
        protected Task hilo;

        public FrmPrincipal()
        {
            InitializeComponent();
            this.StartPosition = FormStartPosition.CenterScreen;
        }

        ///
        /// Punto 10 - Iniciar hilo
        ///
        private void FrmPrincipal_Load(object sender, EventArgs e)
        {
            this.Text = "Caretti Nicolas";
            MessageBox.Show(this.Text);

            ///Se inicia el hilo
            this.hilo = Task.Run(() => ActualizarListadoAutosBD(sender));

        }

        ///
        /// Punto 3 - FrmListado
        /// 
        private void listadoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FrmListado frm = new FrmListado();
            frm.StartPosition = FormStartPosition.CenterScreen;

            frm.Show(this);
        }

        ///
        /// Punto 9
        ///
        private void verLogAutosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.openFileDialog1.Title = "Abrir archivo de autos";
            this.openFileDialog1.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            this.openFileDialog1.Filter = "LOG files|*.log";
            this.openFileDialog1.DefaultExt = "log";
            this.openFileDialog1.FileName = "autos";

            DialogResult rta = openFileDialog1.ShowDialog();

            if (rta == DialogResult.OK)
            {
                StreamReader archivo = new StreamReader(openFileDialog1.FileName);
                txtAutosLog.Text = archivo.ReadToEnd();
            }
            else
            {
                MessageBox.Show("No se muestra .log");
            }
        }

        ///PARA ACTUALIZAR LISTADO DESDE BD EN HILO
        ///NOTA: propiedades BackColor (fondo) y ForeColor (fuente)
        ///colores: 
        ///System.Drawing.Color.Black (negro)
        ///System.Drawing.Color.White (blanco)
        public void ActualizarListadoAutosBD(object param)
        {
            try
            {
                while (true)
                {
                    if (this.lstAutos.InvokeRequired)
                    {
                        lstAutos.DataSource = ADO.ObtenerTodos();
                        this.lstAutos.BeginInvoke((MethodInvoker)delegate ()
                        {
                            lstAutos.DataSource = ADO.ObtenerTodos();
                            this.lstAutos.BackColor = System.Drawing.Color.Black;
                            this.lstAutos.ForeColor = System.Drawing.Color.White;
                        });
                        Thread.Sleep(1500);
                        this.lstAutos.BackColor = System.Drawing.Color.White;
                        this.lstAutos.ForeColor = System.Drawing.Color.Black;
                        Thread.Sleep(1500);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
