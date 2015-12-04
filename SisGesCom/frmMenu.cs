using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace SisGesCom
{
    public partial class frmMenu : Form
    {

        public string cUsuarioActual = frmLogin.cUsuarioActual;
        
        public frmMenu()
        {
            InitializeComponent();
        }

        private void buttonItem7_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void buttonItem15_Click(object sender, EventArgs e)
        {
            frmTipoCombustible ofrmTipoCombustible = new frmTipoCombustible();
            ofrmTipoCombustible.Show();
        }

        private void buttonItem16_Click(object sender, EventArgs e)
        {
            frmDeptoAutoriza ofrmDeptoAutoriza = new frmDeptoAutoriza();
            ofrmDeptoAutoriza.Show();
        }

        private void buttonItem3_Click(object sender, EventArgs e)
        {
            frmTipoBeneficiario ofrmTipoBeneficiario = new frmTipoBeneficiario();
            ofrmTipoBeneficiario.Show();
        }

        private void buttonItem1_Click(object sender, EventArgs e)
        {
            frmSolicitudCombustible ofrmSolicitudCombustible = new frmSolicitudCombustible();
            ofrmSolicitudCombustible.Show();
        }

        private void buttonItem11_Click(object sender, EventArgs e)
        {
            frmEntradaCombustible ofrmEntradaCombustible = new frmEntradaCombustible();
            ofrmEntradaCombustible.Show();
        }

        private void buttonItem14_Click(object sender, EventArgs e)
        {
            frmSalidaCombustible ofrmSalidaCombustible = new frmSalidaCombustible();
            ofrmSalidaCombustible.Show();
        }

        private void buttonItem10_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void buttonItem9_Click(object sender, EventArgs e)
        {
            frmAbout ofrmAbout = new frmAbout();
            ofrmAbout.Show();
        }

        private void buttonItem2_Click(object sender, EventArgs e)
        {
            frmInformaciones ofrmInformaciones = new frmInformaciones();
            ofrmInformaciones.Show();
        }
    }
}
