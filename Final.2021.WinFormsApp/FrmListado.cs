using Entidades;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace Final._2021.WinFormsApp
{
    public partial class FrmListado : Form
    {
        List<Entidades.Auto> lista;
        Auto autoColorRepetido = new Auto();

        public FrmListado()
        {
            InitializeComponent();

            this.StartPosition = FormStartPosition.CenterScreen;
        }

        ///
        /// Punto 3 - Obtener y mostrar todos los autos de la BD
        ///
        private void FrmListado_Load(object sender, EventArgs e)
        {
            this.lista = Entidades.ADO.ObtenerTodos();
            this.lstListado.DataSource = this.lista;
        }

        ///
        /// Punto 4 - Agregar un nuevo auto a la BD. Utilizar FrmAuto
        ///
        private void btnAgregar_Click(object sender, EventArgs e)
        {
            ///
            /// Punto 8-a - Capturar excepción si está repetido.
            ///
            FrmAuto frm = new FrmAuto();
            ADO ado = new ADO();
            try
            {
                ado.ColorExistente += this.Manejador_colorExistente;

                frm.StartPosition = FormStartPosition.CenterParent;

                if (frm.ShowDialog() == DialogResult.OK)
                {
                    autoColorRepetido.Color = frm.AutoDelFormulario.Color;
                    ado.Agregar(frm.AutoDelFormulario);

                    MessageBox.Show("Se agrego el auto con exito.");
                }
                else
                {
                    MessageBox.Show("Ocurrio un error al agregar el auto");
                }
            }
            catch(PatenteExistenteException ex)
            {
                MessageBox.Show(frm.AutoDelFormulario.ToString());
                MessageBox.Show("No se agrego el auto.");
            }
        }

        ///
        /// Punto 5 - Modificar auto seleccionado en la BD. Reutilizar FrmAuto
        ///
        private void btnModificar_Click(object sender, EventArgs e)
        {
            int i = this.lstListado.SelectedIndex;

            if (i < 0) { return; }

            Auto a = this.lista[i];

            FrmAuto frm = new FrmAuto(a, false);

            frm.StartPosition = FormStartPosition.CenterParent;

            if (frm.ShowDialog() == DialogResult.OK)
            {
                ///Implementar
                ADO ado = new ADO();
                if (ado != null)
                {
                    if (ado.Modificar(frm.AutoDelFormulario))
                    {
                        this.lista[i] = frm.AutoDelFormulario;
                        MessageBox.Show("Se ha modificado el auto con exito.");
                        this.lista = ADO.ObtenerTodos();
                        this.lstListado.DataSource = this.lista;
                    }
                    else
                    {
                        MessageBox.Show("No se pudo modificar el auto.");
                    }
                }
            }
        }

        ///
        /// Punto 6 - Eliminar auto seleccionado de la BD. Reutilizar FrmAuto.
        ///
        private void btnEliminar_Click(object sender, EventArgs e)
        {
            int i = this.lstListado.SelectedIndex;

            if (i < 0) { return; }

            Auto auto = this.lista[i];

            FrmAuto frm = new FrmAuto(auto, true);
            frm.StartPosition = FormStartPosition.CenterParent;

            if (frm.ShowDialog() == DialogResult.OK)
            {
                ///Implementar
                ADO ado = new ADO();

                if (ado != null)
                {
                    if (ado.Eliminar(frm.AutoDelFormulario))
                    {
                        this.lista.Remove(auto);
                        this.lista = ADO.ObtenerTodos();
                        this.lstListado.DataSource = this.lista;

                        MessageBox.Show("Se ha eliminado el auto con exito.");
                    }
                    else
                    {
                        MessageBox.Show("No se pudo eliminar el auto.");
                    }
                }
            }
        }

        ///
        /// Punto 8-b - Capturar evento ColorExiste y escribir en log
        ///
        private void Manejador_colorExistente(object sender, EventArgs e)
        {
            bool todoOK = false;

            MessageBox.Show(autoColorRepetido.Color);
            if (autoColorRepetido.Color != null)
            {
                todoOK = ManejadoraTexto.EscribirArchivo(ADO.ObtenerTodos(autoColorRepetido.Color));//Reemplazar por la llamada al método de clase ManejadoraTexto.EscribirArchivo
            }


            MessageBox.Show("Color repetido!!!");

            if (todoOK)
            {
                MessageBox.Show("Se escribió correctamente!!!");
            }
            else
            {
                MessageBox.Show("No se pudo escribir!!!");
            }
        }
    }
}
