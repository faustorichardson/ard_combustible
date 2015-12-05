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
    public partial class frmSolicitudCombustible : frmBase
    {

        string cModo = "Inicio";

        public frmSolicitudCombustible()
        {
            InitializeComponent();
        }

        private void frmSolicitudCombustible_Load(object sender, EventArgs e)
        {
            this.Limpiar();
            this.BuscarInformaciones();
            this.fillCmbTipoCombustible();
            this.cModo = "Inicio";
            this.Botones();
        }

        private void fillCmbTipoCombustible()
        {
            // Step 1 
            MySqlConnection MyConexion = new MySqlConnection(clsConexion.ConectionString);

            // Step 2
            MyConexion.Open();

            // Step 3
            MySqlCommand MyCommand = new MySqlCommand("SELECT id, combustible FROM tipo_combustible ORDER BY combustible ASC", MyConexion);

            // Step 4
            MySqlDataReader MyReader;
            MyReader = MyCommand.ExecuteReader();

            // Step 5
            DataTable MyDataTable = new DataTable();
            MyDataTable.Columns.Add("id", typeof(int));
            MyDataTable.Columns.Add("combustible", typeof(string));
            MyDataTable.Load(MyReader);

            // Step 6            
            cmbTipoCombustible.ValueMember = "id";
            cmbTipoCombustible.DisplayMember = "combustible";
            cmbTipoCombustible.DataSource = MyDataTable;
            
        }

        private void Botones()
        {
            switch (cModo)
            {
                case "Inicio":
                    this.btnNuevo.Enabled = true;
                    this.btnGrabar.Enabled = false;
                    this.btnEditar.Enabled = false;
                    this.btnBuscar.Enabled = true;
                    this.btnEliminar.Enabled = false;
                    this.btnImprimir.Enabled = false;
                    this.btnCancelar.Enabled = false;
                    this.btnSalir.Enabled = true;
                    //
                    this.dtFechaSolicitud.Enabled = false;
                    this.txtSolicitud.Enabled = true;
                    this.cmbTipoCombustible.Enabled = false;
                    this.txtCantidad.Enabled = false;
                    this.txtNota.Enabled = false;
                    //
                    this.txtSolicitud.Focus();
                    break;

                case "Nuevo":
                    this.btnNuevo.Enabled = false;
                    this.btnGrabar.Enabled = true;
                    this.btnEditar.Enabled = false;
                    this.btnBuscar.Enabled = false;
                    this.btnImprimir.Enabled = false;
                    this.btnEliminar.Enabled = false;
                    this.btnCancelar.Enabled = true;
                    this.btnSalir.Enabled = true;
                    //
                    this.dtFechaSolicitud.Enabled = true;
                    this.txtSolicitud.Enabled = false;
                    this.cmbTipoCombustible.Enabled = true;
                    this.txtCantidad.Enabled = true;
                    this.txtNota.Enabled = true;
                    //
                    this.cmbTipoCombustible.Focus();
                    break;

                case "Grabar":
                    this.btnNuevo.Enabled = true;
                    this.btnGrabar.Enabled = false;
                    this.btnEditar.Enabled = false;
                    this.btnBuscar.Enabled = true;
                    this.btnImprimir.Enabled = false;
                    this.btnEliminar.Enabled = false;
                    this.btnCancelar.Enabled = false;
                    this.btnSalir.Enabled = true;
                    //
                    this.dtFechaSolicitud.Enabled = false;
                    this.txtSolicitud.Enabled = true;
                    this.cmbTipoCombustible.Enabled = false;
                    this.txtCantidad.Enabled = false;
                    this.txtNota.Enabled = false;
                    break;

                case "Editar":
                    this.btnNuevo.Enabled = false;
                    this.btnGrabar.Enabled = true;
                    this.btnEditar.Enabled = false;
                    this.btnBuscar.Enabled = false;
                    this.btnImprimir.Enabled = false;
                    this.btnEliminar.Enabled = true;
                    this.btnCancelar.Enabled = true;
                    this.btnSalir.Enabled = true;
                    //
                    this.dtFechaSolicitud.Enabled = true;
                    this.txtSolicitud.Enabled = false;
                    this.cmbTipoCombustible.Enabled = true;
                    this.txtCantidad.Enabled = true;
                    this.txtNota.Enabled = true;
                    //
                    this.cmbTipoCombustible.Focus();
                    break;

                case "Buscar":
                    this.btnNuevo.Enabled = true;
                    this.btnGrabar.Enabled = false;
                    this.btnEditar.Enabled = true;
                    this.btnBuscar.Enabled = true;
                    this.btnImprimir.Enabled = true;
                    this.btnEliminar.Enabled = false;
                    this.btnCancelar.Enabled = false;
                    this.btnSalir.Enabled = true;
                    //
                    this.dtFechaSolicitud.Enabled = false;
                    this.txtSolicitud.Enabled = true;
                    this.cmbTipoCombustible.Enabled = false;
                    this.txtCantidad.Enabled = false;
                    this.txtNota.Enabled = false;
                    break;

                case "Eliminar":
                    break;

                case "Cancelar":
                    this.btnNuevo.Enabled = true;
                    this.btnGrabar.Enabled = false;
                    this.btnEditar.Enabled = false;
                    this.btnBuscar.Enabled = true;
                    this.btnImprimir.Enabled = false;
                    this.btnEliminar.Enabled = false;
                    this.btnCancelar.Enabled = false;
                    this.btnSalir.Enabled = true;
                    //
                    this.dtFechaSolicitud.Enabled = false;
                    this.txtSolicitud.Enabled = true;
                    this.cmbTipoCombustible.Enabled = false;
                    this.txtCantidad.Enabled = false;
                    this.txtNota.Enabled = false;
                    break;

                default:
                    break;
            }

        }

        private void BuscarInformaciones()
        {

            try
            {
                // Step 1 - Conexion
                MySqlConnection MyConexion = new MySqlConnection(clsConexion.ConectionString);

                // Step 2 - creating the command object
                MySqlCommand MyCommand = MyConexion.CreateCommand();

                // Step 3 - creating the commandtext
                MyCommand.CommandText = "SELECT cdtegral, enccomb FROM info WHERE id = 1";

                // Step 4 - connection open
                MyConexion.Open();

                // Step 5 - Creating the DataReader                    
                MySqlDataReader MyReader = MyCommand.ExecuteReader();

                // Step 6 - Verifying if Reader has rows
                if (MyReader.HasRows)
                {
                    while (MyReader.Read())
                    {
                        // Informaciones del Comandante General
                        lblCdteGral.Text = MyReader["cdtegral"].ToString();
                        lblEncComb.Text = MyReader["enccomb"].ToString();

                        //MessageBox.Show(lblCdteGral.Text);
                        //MessageBox.Show(lblEncComb.Text);
                    }
                }

            }
            catch (Exception myEx)
            {
                MessageBox.Show(myEx.Message);
                throw;
            }

        }

        private void Limpiar()
        {
            this.dtFechaSolicitud.Refresh();
            this.txtSolicitud.Clear();
            this.cmbTipoCombustible.Refresh();
            this.txtCantidad.Clear();
            this.txtNota.Clear();
        }

        private void ProximoCodigo()
        {
            try
            {
                // Step 1 - Connection stablished
                MySqlConnection MyConexion = new MySqlConnection(clsConexion.ConectionString);

                // Step 2 - Create command
                MySqlCommand MyCommand = MyConexion.CreateCommand();

                // Step 3 - Set the commanndtext property
                MyCommand.CommandText = "SELECT count(*) FROM solicitud";

                // Step 4 - Open connection
                MyConexion.Open();

                // Step 5 - Execute the SQL Statement y Asigno el valor resultante a la variable "codigo"
                int codigo;
                codigo = Convert.ToInt32(MyCommand.ExecuteScalar());
                codigo = codigo + 1;
                txtSolicitud.Text = Convert.ToString(codigo);

                // Step 5 - Close the connection
                MyConexion.Close();
            }
            catch (MySqlException MyEx)
            {
                MessageBox.Show(MyEx.Message);
            }
        }

        private void btnNuevo_Click(object sender, EventArgs e)
        {
            cModo = "Nuevo";
            this.Limpiar();
            this.Botones();
            this.ProximoCodigo();
        }

        private void btnGrabar_Click(object sender, EventArgs e)
        {
            if (txtSolicitud.Text == "")
            {
                MessageBox.Show("No se permite guardar un registro sin numero solicitud...");
                txtSolicitud.Focus();
            }
            else
            {
                if (cModo == "Nuevo")
                {
                    try
                    {
                        // Step 1 - Stablishing the connection
                        MySqlConnection MyConexion = new MySqlConnection(clsConexion.ConectionString);

                        // Step 2 - Crear el comando de ejecucion
                        MySqlCommand myCommand = MyConexion.CreateCommand();

                        // Step 3 - Comando a ejecutar                        
                        myCommand.CommandText = "INSERT INTO solicitud(fecha, tipo_combustible, cantidad, nota, cdtegral, enccomb, status) " +
                            "values(@fecha, @tipocombustible, @cantidad, @nota, @cdtegral, @enccomb, @status)";
                        myCommand.Parameters.AddWithValue("@fecha", dtFechaSolicitud.Value.ToString("yyyy-MM-dd HH:mm:ss"));
                        myCommand.Parameters.AddWithValue("@tipocombustible", cmbTipoCombustible.SelectedValue);
                        myCommand.Parameters.AddWithValue("@cantidad", txtCantidad.Text);
                        myCommand.Parameters.AddWithValue("@nota", txtNota.Text);
                        myCommand.Parameters.AddWithValue("@cdtegral", lblCdteGral.Text);
                        myCommand.Parameters.AddWithValue("@enccomb", lblEncComb.Text);
                        myCommand.Parameters.AddWithValue("@status", 1);

                        // Step 4 - Opening the connection
                        MyConexion.Open();

                        // Step 5 - Executing the query
                        myCommand.ExecuteNonQuery();

                        // Step 6 - Closing the connection
                        MyConexion.Close();

                        MessageBox.Show("Informacion guardada satisfactoriamente...");
                    }
                    catch (Exception myEx)
                    {
                        MessageBox.Show(myEx.Message);
                        throw;
                    }
                }
                else
                {
                    try
                    {
                        // Step 1 - Stablishing the connection
                        MySqlConnection MyConexion = new MySqlConnection(clsConexion.ConectionString);

                        // Step 2 - Crear el comando de ejecucion
                        MySqlCommand myCommand = MyConexion.CreateCommand();

                        // Step 3 - Comando a ejecutar
                        myCommand.CommandText = "UPDATE solicitud SET fecha = @fecha, tipo_combustible = @tipocombustible, " +
                            "cantidad = @cantidad, nota = @nota " +
                            "WHERE id = " + txtSolicitud.Text + "";
                        myCommand.Parameters.AddWithValue("@fecha", dtFechaSolicitud.Value.ToString("yyyy-MM-dd HH:mm:ss"));
                        myCommand.Parameters.AddWithValue("@tipocombustible", cmbTipoCombustible.SelectedValue);
                        myCommand.Parameters.AddWithValue("@cantidad", txtCantidad.Text);
                        myCommand.Parameters.AddWithValue("@nota", txtNota.Text);
                        //myCommand.Parameters.AddWithValue("@cdtegral", lblCdteGral.Text);
                        //myCommand.Parameters.AddWithValue("@enccomb", lblEncComb.Text);
                        //myCommand.Parameters.AddWithValue("@status", 1);

                        // Step 4 - Opening the connection
                        MyConexion.Open();

                        // Step 5 - Executing the query
                        myCommand.ExecuteNonQuery();

                        // Step 6 - Closing the connection
                        MyConexion.Close();

                        MessageBox.Show("Informacion actualizada satisfactoriamente...");
                    }
                    catch (Exception myEx)
                    {
                        MessageBox.Show(myEx.Message);
                        throw;
                    }
                }

                this.cModo = "Inicio";
                this.Botones();
                this.Limpiar();
            }
        }

        private void btnEditar_Click(object sender, EventArgs e)
        {
            this.cModo = "Editar";
            this.Botones();
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            if (txtSolicitud.Text == "")
            {
                MessageBox.Show("Debe de indicar un numero de pedido de combustible...");
                txtSolicitud.Focus();
            }
            else
            {
                // Step 1 - Conexion
                MySqlConnection MyConexion = new MySqlConnection(clsConexion.ConectionString);

                // Step 2 - creating the command object
                MySqlCommand MyCommand = MyConexion.CreateCommand();

                // Step 3 - creating the commandtext
                MyCommand.CommandText = "SELECT id, fecha, tipo_combustible, cantidad, nota, enccomb, cdtegral " +
                    "FROM solicitud WHERE id = " + txtSolicitud.Text + "";

                // Step 4 - connection open
                MyConexion.Open();

                // Step 5 - Creating the DataReader                    
                MySqlDataReader MyReader = MyCommand.ExecuteReader();

                // Step 6 - Verifying if Reader has rows
                if (MyReader.HasRows)
                {
                    while (MyReader.Read())
                    {
                        dtFechaSolicitud.Value = Convert.ToDateTime(MyReader["fecha"].ToString());
                        cmbTipoCombustible.SelectedValue = MyReader["tipo_combustible"].ToString();
                        txtCantidad.Text = MyReader["cantidad"].ToString();
                        txtNota.Text = MyReader["nota"].ToString();
                        lblCdteGral.Text = MyReader["cdtegral"].ToString();
                        lblEncComb.Text = MyReader["enccomb"].ToString();                        
                    }

                    this.cModo = "Buscar";
                    this.Botones();
                }
                else
                {
                    MessageBox.Show("No se encontraron registros...");
                    this.Limpiar();
                    this.cModo = "Inicio";
                    this.Botones();                    
                    //this.txtYear.Focus();
                }

                // Step 6 - Closing all
                MyReader.Close();
                MyCommand.Dispose();
                MyConexion.Close();
            }
        }

        private void btnImprimir_Click(object sender, EventArgs e)
        {

        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {

        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            if (txtSolicitud.Text != "")
            {
                DialogResult Result =
                MessageBox.Show("Se perderan Los cambios Realizados" + System.Environment.NewLine + "Desea Guardar los Cambios", "Sistema Gestion de Combustibles", MessageBoxButtons.YesNo,
                        MessageBoxIcon.Question);
                switch (Result)
                {
                    case DialogResult.Yes:
                        cModo = "Actualiza";
                        btnGrabar_Click(sender, e);
                        break;
                }
            }

            this.Limpiar();
            this.cModo = "Inicio";
            this.Botones();
        }

        private void btnSalir_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void txtCantidad_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!(char.IsNumber(e.KeyChar)) && (e.KeyChar != (char)Keys.Back))
            {
                MessageBox.Show("Solo se permiten numeros...", "Sistema de Gestion de Combustibles", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                e.Handled = true;
                return;
            }
        }

        private void txtSolicitud_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!(char.IsNumber(e.KeyChar)) && (e.KeyChar != (char)Keys.Back))
            {
                MessageBox.Show("Solo se permiten numeros...", "Sistema de Gestion de Combustibles", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                e.Handled = true;
                return;
            }
        }        


    }
}
