using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using MySql.Data;
using MySql.Data.MySqlClient;
using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.ReportSource;
using CrystalDecisions.Shared;
using CrystalDecisions.Windows.Forms;
using CrystalDecisions.Web;
using CrystalDecisions.Windows;
using System.Drawing.Imaging;
using System.IO;

namespace SisGesCom
{
    public partial class frmAnularSolicitud : frmBase
    {
        public frmAnularSolicitud()
        {
            InitializeComponent();
        }

        private void frmAnularSolicitud_Load(object sender, EventArgs e)
        {
            // Limpio los campos
            this.Limpiar();
        }

        private void Limpiar()
        {
            this.txtSolicitud.Clear();
            this.txtCausa.Clear();
            this.txtSolicitud.Focus();
        }

        private void btnGrabar_Click(object sender, EventArgs e)
        {
            if (txtSolicitud.Text != "")
            {
                DialogResult Result =
                MessageBox.Show("Esta procediendo a anular la Solicitud ..." + System.Environment.NewLine + "Esta seguro de proceder con la anulacion", "Sistema de Gestion de Combustibles", MessageBoxButtons.YesNo,
                        MessageBoxIcon.Question);
                switch (Result)
                {
                    case DialogResult.Yes:
                        this.Buscar();
                        break;
                }
            }
        }

        private void Buscar(){

            // BUSCANDO LA SOLICITUD PARA VER EXISTENCIA
            if (txtSolicitud.Text == "")
            {
                MessageBox.Show("Debe de introducir un numero de solicitud");
                txtSolicitud.Focus();
            }
            else
            {
                //Buscando la solicitud
                try
                {
                    // Step 1 - Conexion
                    MySqlConnection MyConexion = new MySqlConnection(clsConexion.ConectionString);

                    // Step 2 - creating the command object
                    MySqlCommand MyCommand = MyConexion.CreateCommand();

                    // Step 3 - creating the commandtext
                    MyCommand.CommandText = "SELECT id FROM secuencia_solicitudcombustible WHERE id = " + txtSolicitud.Text + " AND anulada = 0";

                    // Step 4 - connection open
                    MyConexion.Open();

                    // Step 5 - Creating the DataReader                    
                    MySqlDataReader MyReader = MyCommand.ExecuteReader();                    

                    // Step 6 - Verifying if Reader has rows
                    if (MyReader.HasRows)
                    {
                        this.Anular();
                    }
                    else
                    {
                        MessageBox.Show("No se encontraron registros o ya fue anulada este numero de solicitud...");
                        this.txtSolicitud.Focus();
                    }
                    
                    // Step 6 - Closing all
                    MyReader.Close();
                    MyCommand.Dispose();
                    MyConexion.Close();
                }
                catch (Exception myEx)
                {
                    MessageBox.Show(myEx.Message);
                    throw;
                }
            }     
       
            // LIMPIANDO EL FORMULARIO
            this.Limpiar();            
        }

        private void Anular(){
            
            // ANULANDO EN LA TABLA SECUENCIA_SOLICITUDCOMBUSTIBLE
            try
            {
                // Step 1 - Stablishing the connection
                MySqlConnection MyConexion = new MySqlConnection(clsConexion.ConectionString);

                // Step 2 - Crear el comando de ejecucion
                MySqlCommand myCommand = MyConexion.CreateCommand();

                // Step 3 - Comando a ejecutar
                myCommand.CommandText = "UPDATE secuencia_solicitudcombustible SET anulada = @status, anulada_comentario = @causa WHERE id = " + txtSolicitud.Text + "";
                myCommand.Parameters.AddWithValue("@status", 1);
                myCommand.Parameters.AddWithValue("@causa", txtCausa.Text);

                // Step 4 - Opening the connection
                MyConexion.Open();

                // Step 5 - Executing the query
                myCommand.ExecuteNonQuery();

                // Step 6 - Closing the connection
                MyConexion.Close();

                //MessageBox.Show("Informacion actualizada satisfactoriamente...");
            }
            catch (Exception myEx)
            {
                MessageBox.Show(myEx.Message);
                throw;
            }

            // ANULANDO EN LA TABLA SOLICITUD
            try
            {
                // Step 1 - Stablishing the connection
                MySqlConnection MyConexion = new MySqlConnection(clsConexion.ConectionString);

                // Step 2 - Crear el comando de ejecucion
                MySqlCommand myCommand = MyConexion.CreateCommand();

                // Step 3 - Comando a ejecutar
                myCommand.CommandText = "UPDATE solicitud SET anulada = @status WHERE id = " + txtSolicitud.Text + "";
                myCommand.Parameters.AddWithValue("@status", 1);

                // Step 4 - Opening the connection
                MyConexion.Open();

                // Step 5 - Executing the query
                myCommand.ExecuteNonQuery();

                // Step 6 - Closing the connection
                MyConexion.Close();

                MessageBox.Show("Solicitud anulada exitosamente...");
            }
            catch (Exception myEx)
            {
                MessageBox.Show(myEx.Message);
                throw;
            }
        }

        private void btnSalir_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void txtSolicitud_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar >= '0' && e.KeyChar <= '9')
            {
                e.Handled = false;
            }
            else
            {
                e.Handled = true;
            }
        }
    }
}
