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
            conexion.Desconectar();
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

        private void btnGrabar_Click(object sender, EventArgs e)
        {
            long Id = 0;
            string Nombre = "";
            if (ValidarDatos())
            {
                long.TryParse(txtCantante.Text, out Id);
                Nombre = txtNombre.Text;
                if(cantante.BuscarCantante(Id) == false)
                {
                    if (cantante.NuevoCantante(Id, Nombre))
                    {
                        txtCantante.Text = "";
                        txtNombre.Text = "";
                    }
                    else
                    {
                        MessageBox.Show("Error al agregar el cantante: " + cantante.ObtenerError());
                    }
                }
                else
                {
                                       MessageBox.Show("El Id del cantante ya existe. Por favor, ingrese un Id diferente.");
                }
            }
            else
            {
                MessageBox.Show("Por favor, complete todos los campos correctamente. El Id debe ser un número positivo.");
            }
        }
    }
}
