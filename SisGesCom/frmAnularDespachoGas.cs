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
    public partial class frmAnularDespachoGas : frmBase
    {
        public frmAnularDespachoGas()
        {
            InitializeComponent();
        }

        private void frmAnularDespachoGas_Load(object sender, EventArgs e)
        {
            // Llamando la funcion de limpiar variables y campos
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
                MessageBox.Show("Esta procediendo a anular una Entrada de Combustible ..." + System.Environment.NewLine + "Esta seguro de proceder con la anulacion", "Sistema de Gestion de Combustibles", MessageBoxButtons.YesNo,
                        MessageBoxIcon.Question);
                switch (Result)
                {
                    case DialogResult.Yes:
                        this.Buscar();
                        break;
                }
            }
        }

        private void Buscar()
        {
            if (txtSolicitud.Text == "")
            {
                MessageBox.Show("Digite un numero de entrada...");
                txtSolicitud.Focus();
            }
            else
            {

                // VERIFICANDO ANTES DE PROCEDER
                try
                {
                    // Step 1 - Conexion
                    MySqlConnection MyConexion = new MySqlConnection(clsConexion.ConectionString);

                    // Step 2 - creating the command object
                    MySqlCommand MyCommand = MyConexion.CreateCommand();

                    // Step 3 - creating the commandtext
                    MyCommand.CommandText = "SELECT id, anulada FROM combustible_gas WHERE id = " + txtSolicitud.Text + " AND anulada = 1";

                    // Step 4 - connection open
                    MyCommand.Dispose();
                    MyConexion.Open();

                    // Step 5 - Creating the DataReader                    
                    MySqlDataReader MyReader = MyCommand.ExecuteReader();

                    // Step 6 - Verifying if Reader has rows
                    if (MyReader.HasRows)
                    {
                        MessageBox.Show("Este numero de despacho de gas fue anulado anteriormente...");
                        this.txtSolicitud.Focus();
                    }
                    else
                    {
                        // Funcion que actualiza la tabla combustible_salida
                        this.AnulaDespacho();
                        this.AnulaMovimiento();
                    }

                    // Step 6 - Closing all
                    MyReader.Close();
                    MyCommand.Dispose();
                    MyConexion.Close();

                    this.Limpiar();
                }
                catch (Exception myEx)
                {
                    MessageBox.Show(myEx.Message);
                }
            }

            // Limpio informacion
            this.Limpiar();
        }

        private void AnulaDespacho()
        {
            //ACTUALIZANDO LA TABLA COMBUSTIBLE_GAS CON LA ANULACION EN EL CAMPO ANULADA
            //ESTO LE PONDRA UN 1 AL CAMPO ANULADO.
            try
            {
                // Step 1 - Stablishing the connection
                MySqlConnection MyConexion = new MySqlConnection(clsConexion.ConectionString);

                // Step 2 - Crear el comando de ejecucion
                MySqlCommand myCommand = MyConexion.CreateCommand();

                // Step 3 - Comando a ejecutar
                myCommand.CommandText = "UPDATE combustible_gas SET anulada = @anulada, anulada_comentario = @causa WHERE id = " + txtSolicitud.Text + "";
                myCommand.Parameters.AddWithValue("@anulada", 1);
                myCommand.Parameters.AddWithValue("@causa", txtCausa.Text);

                // Step 4 - Opening the connection
                MyConexion.Open();

                // Step 5 - Executing the query
                myCommand.ExecuteNonQuery();

                // Step 6 - Closing the connection
                MyConexion.Close();

                MessageBox.Show("Despacho de Gas anulado satisfactoriamente...");
            }
            catch (Exception MyEx)
            {
                MessageBox.Show(MyEx.Message);
            }

            // Limpio los valores
            //this.Limpiar();
        }

        private void AnulaMovimiento()
        {
            //ACTUALIZANDO LA TABLA MOVIMIENTOGAS CON LA ANULACION EN EL CAMPO ANULADA
            //ESTO LE PONDRA UN 1 AL CAMPO ANULADO.
            try
            {
                // Step 1 - Stablishing the connection
                MySqlConnection MyConexion = new MySqlConnection(clsConexion.ConectionString);

                // Step 2 - Crear el comando de ejecucion
                MySqlCommand myCommand = MyConexion.CreateCommand();

                // Step 3 - Comando a ejecutar
                myCommand.CommandText = "UPDATE movimientogas SET anulada = @anulada WHERE id = " + txtSolicitud.Text + "";
                myCommand.Parameters.AddWithValue("@anulada", 1);

                // Step 4 - Opening the connection
                MyConexion.Open();

                // Step 5 - Executing the query
                myCommand.ExecuteNonQuery();

                // Step 6 - Closing the connection
                MyConexion.Close();

                MessageBox.Show("Despacho de Gas anulado satisfactoriamente...");
            }
            catch (Exception MyEx)
            {
                MessageBox.Show(MyEx.Message);
            }
        }

        private void btnSalir_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
