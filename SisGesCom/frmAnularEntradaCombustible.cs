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
    public partial class frmAnularEntradaCombustible : frmBase
    {
        public frmAnularEntradaCombustible()
        {
            InitializeComponent();
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
                    MyCommand.CommandText = "SELECT id, anulada FROM combustible_entrada WHERE id = " + txtSolicitud.Text + " AND anulada = 1";

                    // Step 4 - connection open
                    MyCommand.Dispose();
                    MyConexion.Open();

                    // Step 5 - Creating the DataReader                    
                    MySqlDataReader MyReader = MyCommand.ExecuteReader();

                    // Step 6 - Verifying if Reader has rows
                    if (MyReader.HasRows)
                    {
                        MessageBox.Show("Este numero de entrada de combustible fue anulado anteriormente...");
                        this.txtSolicitud.Focus();
                    }
                    else
                    {
                        // Funcion que actualiza la tabla combustible_entrada
                        this.ActualizaEntrada();
                        // Funcion que actualiza los movimientos
                        this.ActualizarMovimiento();
                        // Funcion que actualiza la existencia
                        this.ActualizarExistencia();
                        MessageBox.Show("Proceso de anulacion realizado satisfactoriamente.");
                    }

                    // Step 6 - Closing all
                    MyReader.Close();
                    MyCommand.Dispose();
                    MyConexion.Close();
                } catch(Exception myEx){
                    MessageBox.Show(myEx.Message);
                }
            }

            // Limpio informacion
            this.Limpiar();
        }

        private void ActualizaEntrada()
        {
            //ACTUALIZANDO LA TABLA COMBUSTIBLE_ENTRADA CON LA ANULACION EN EL CAMPO ANULADA
            //ESTO LE PONDRA UN 1 AL CAMPO ANULADO.
            try
            {
                // Step 1 - Stablishing the connection
                MySqlConnection MyConexion = new MySqlConnection(clsConexion.ConectionString);

                // Step 2 - Crear el comando de ejecucion
                MySqlCommand myCommand = MyConexion.CreateCommand();

                // Step 3 - Comando a ejecutar
                myCommand.CommandText = "UPDATE combustible_entrada SET anulada = @anulada, anulada_comentario = @causa WHERE id = " + txtSolicitud.Text + "";
                myCommand.Parameters.AddWithValue("@anulada", 1);
                myCommand.Parameters.AddWithValue("@causa", txtCausa.Text);

                // Step 4 - Opening the connection
                MyConexion.Open();

                // Step 5 - Executing the query
                myCommand.ExecuteNonQuery();

                // Step 6 - Closing the connection
                MyConexion.Close();
                //MessageBox.Show("Informacion actualizada satisfactoriamente...");
            }
            catch (Exception MyEx)
            {
                MessageBox.Show(MyEx.Message);
            }
        }

        private void ActualizarMovimiento()
        {
            //ACTUALIZANDO LA TABLA MOVIMIENTOCOMBUSTIBLE CON LA ANULACION EN EL CAMPO ANULADA
            //ESTO LE PONDRA UN 1 AL CAMPO ANULADO.
            try
            {
                // Step 1 - Stablishing the connection
                MySqlConnection MyConexion = new MySqlConnection(clsConexion.ConectionString);

                // Step 2 - Crear el comando de ejecucion
                MySqlCommand myCommand = MyConexion.CreateCommand();

                // Step 3 - Comando a ejecutar
                myCommand.CommandText = "UPDATE movimientocombustible SET anulada = @anulada WHERE id = " + txtSolicitud.Text + " AND tipo_movimiento = 'E'";
                myCommand.Parameters.AddWithValue("@anulada", 1);

                // Step 4 - Opening the connection
                MyConexion.Open();

                // Step 5 - Executing the query
                myCommand.ExecuteNonQuery();

                // Step 6 - Closing the connection
                MyConexion.Close();
                //MessageBox.Show("Informacion actualizada satisfactoriamente...");
            }
            catch (Exception MyEx)
            {
                MessageBox.Show(MyEx.Message);
            }
        }

        private void ActualizarExistencia()
        {
            //ACTUALIZANDO LA TABLA EXISTENCIA                
            try
            {
                // Step 1 - Stablishing the connection
                MySqlConnection MyConexion = new MySqlConnection(clsConexion.ConectionString);

                // Step 2 - Crear el comando de ejecucion
                MySqlCommand myCommand = MyConexion.CreateCommand();

                // Step 3 - Comando a ejecutar
                myCommand.CommandText = "UPDATE existencia"+
                    " INNER JOIN movimientocombustible ON movimientocombustible.tipo_combustible = existencia.tipocombustible"+
                    " SET existencia.cantidad = (existencia.cantidad - movimientocombustible.cantidad)"+
                    " WHERE movimientocombustible.id = @anulada AND movimientocombustible.tipo_movimiento = 'E'";
                myCommand.Parameters.AddWithValue("@anulada", txtSolicitud.Text);

                // Step 4 - Opening the connection
                MyConexion.Open();

                // Step 5 - Executing the query
                myCommand.ExecuteNonQuery();

                // Step 6 - Closing the connection
                MyConexion.Close();
                //MessageBox.Show("Informacion actualizada satisfactoriamente...");
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

        private void frmAnularEntradaCombustible_Load(object sender, EventArgs e)
        {
            this.Limpiar();
        }

        private void Limpiar()
        {
            this.txtSolicitud.Clear();
            this.txtCausa.Clear();
            this.txtSolicitud.Focus();
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
