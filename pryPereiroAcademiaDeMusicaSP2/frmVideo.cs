
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.Web.WebView2.WinForms;


namespace pryPereiroAcademiaDeMusicaSP2
{
    
    public partial class frmVideo : Form
    {
        private CConexion conexion;
        private CCantantes cantante;
        private CTema tema;
        private WebView2 webView;

        public frmVideo()
        {
            InitializeComponent();
        }

        private void btnSalir_Click(object sender, EventArgs e)
        {
           
            conexion.Desconectar();
            webView?.Dispose();
            Close();
        }
        

        private void frmVideo_Load(object sender, EventArgs e)
        {

            conexion = new CConexion(); // crear la conexión
            if (!conexion.Conectar("Provider=Microsoft.Jet.OLEDB.4.0; Data Source=" +
            Application.StartupPath + "\\Academia.mdb"))
            {
                MessageBox.Show(conexion.ObtenerError());
                Close();
            }
            else
            {
                // crear el objeto tema
                tema = new CTema(conexion.CNN, conexion.DS);
                // crear el objeto cantante y cargar el combo con sus datos
                cantante = new CCantantes(conexion.CNN, conexion.DS);
                cmbCantante.DisplayMember = "Nombre";
                cmbCantante.ValueMember = "IdCantante";
                cmbCantante.DataSource = cantante.dtCantantes;
            }

        }

        private void cmbCantante_SelectedIndexChanged(object sender, EventArgs e)
        {
            long IdCantante = long.Parse(cmbCantante.SelectedValue.ToString());
            cmbTemas.DisplayMember = "Nombre";
            cmbTemas.ValueMember = "IdTema";
            cmbTemas.DataSource = tema.ObtenerTemasDeCantantes(IdCantante);

        }

        private void cmbTemas_SelectedIndexChanged(object sender, EventArgs e)
        {
            long IdCantante = long.Parse(cmbCantante.SelectedValue.ToString());
            long IdTema = long.Parse(cmbTemas.SelectedValue.ToString());
            txtLink.Text = tema.ObtenerLinkTemasCantantes(IdTema, IdCantante);

        }

        private async void btnVerVideo_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtLink.Text))
                return;

            // Crear el WebView2 si no existe
            if (webView == null)
            {
                webView = new WebView2();
                webView.Dock = DockStyle.Fill;
                grpBrowser.Controls.Add(webView);
                await webView.EnsureCoreWebView2Async(null);
            }

            webView.Source = new Uri(txtLink.Text);
        }
    }
}
