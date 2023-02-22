using System.Windows.Forms;

namespace Final._2021.WinFormsApp
{
    public partial class FrmAuto : Form
    {
        private Entidades.Auto auto;

        public Entidades.Auto AutoDelFormulario
        {
            get { return this.auto; }
        }

        public FrmAuto()
        {
            InitializeComponent();
        }

        public FrmAuto(Entidades.Auto a, bool esEliminar) : this()
        {
            this.txtMarca.Text = a.Marca;
            this.txtModelo.Text = a.Modelo;
            this.txtKms.Text = a.Kms.ToString();
            this.txtColor.Text = a.Color;
            this.txtPatente.Text = a.Patente;
            this.txtPatente.ReadOnly = true;

            if (esEliminar)
            {
                this.txtMarca.ReadOnly = true;
                this.txtModelo.ReadOnly = true;
                this.txtKms.ReadOnly = true;
                this.txtColor.ReadOnly = true;
                this.txtPatente.ReadOnly = true;
            }
        }

        /// Crar una instancia de tipo Auto
        /// Establecer como valor del atributo auto
        private void btnAceptar_Click(object sender, System.EventArgs e)
        {
            string marca = this.txtMarca.Text;
            string modelo = this.txtModelo.Text;
            int kms = int.Parse(this.txtKms.Text);
            string color = this.txtColor.Text;
            string patente = this.txtPatente.Text;

            auto = new Entidades.Auto(color, kms, marca, modelo, patente);
            this.DialogResult = DialogResult.OK;
        }

        private void btnCancelar_Click(object sender, System.EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
        }
    }
}
