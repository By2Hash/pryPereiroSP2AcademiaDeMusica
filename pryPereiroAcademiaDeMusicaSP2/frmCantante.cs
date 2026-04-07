using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace pryPereiroAcademiaDeMusicaSP2
{
    public partial class frmCantante : Form
    {
        private CConexion conexion;
        private CCantantes cantante;
        public frmCantante()
        {
            InitializeComponent();
            
        }

        private void btnSalir_Click(object sender, EventArgs e)
        {
            Close();
        }
       

        private void frmCantante_Load(object sender, EventArgs e)
        {
            conexion = new CConexion(); 
                                       
            if (!conexion.Conectar("Provider=Microsoft.Jet.OLEDB.4.0; Data Source=" +
            Application.StartupPath + "\\Academia.mdb"))
            {
                MessageBox.Show(conexion.ObtenerError());
                Close(); 
            }
            
            cantante = new CCantantes(conexion.CNN, conexion.DS);
        }
        private bool ValidarDatos()
        {
            bool resultado = false;
            long Id = 0;

            if (txtCantante.Text != "" && txtNombre.Text != "")
            {
                if (long.TryParse(txtCantante.Text, out Id))
            {

                    if (Id > 0)
                    {
                         resultado = true;
                    }
            }
            
        }
            return resultado;
        }

    }
}
