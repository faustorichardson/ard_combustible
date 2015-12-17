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
    public partial class frmSalidaCombustible : frmBase
    {

        string cModo = "Inicio";
        decimal cCombustible;

        public frmSalidaCombustible()
        {
            InitializeComponent();
        }

        private void frmSalidaCombustible_Load(object sender, EventArgs e)
        {
            this.Limpiar();
            this.cModo = "Inicio";
            this.Botones();
            this.fillCmbTipoCombustible();
            this.fillCmbTipoBeneficiario();
            this.fillCmbAutorizadoPor();
        }

        private void fillCmbTipoCombustible()
        {
            try
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

                // Step 7
                MyConexion.Close();

            }
            catch (Exception myEx)
            {
                MessageBox.Show(myEx.Message);
                throw;
            }
        }

        private void fillCmbTipoBeneficiario()
        {
            try
            {
                // Step 1 
	            MySqlConnection MyConexion = new MySqlConnection(clsConexion.ConectionString);

                // Step 2
                MyConexion.Open();

                // Step 3
                MySqlCommand MyCommand = new MySqlCommand("SELECT id, deptobeneficiario FROM deptobeneficiario ORDER BY deptobeneficiario ASC", MyConexion);

                // Step 4
                MySqlDataReader MyReader;
                MyReader = MyCommand.ExecuteReader();

                // Step 5
                DataTable MyDataTable = new DataTable();
                MyDataTable.Columns.Add("id", typeof(int));
                MyDataTable.Columns.Add("deptobeneficiario", typeof(string));
                MyDataTable.Load(MyReader);

                // Step 6
                cmbRenglonBeneficiario.ValueMember = "id";
                cmbRenglonBeneficiario.DisplayMember = "deptobeneficiario";
                cmbRenglonBeneficiario.DataSource = MyDataTable;

                // Step 7
                MyConexion.Close();
            }
            catch (Exception myEx)
            {
                MessageBox.Show(myEx.Message);
                throw;
            }
        }

        private void fillCmbAutorizadoPor()
        {
            try
            {
                // Step 1 
	            MySqlConnection MyConexion = new MySqlConnection(clsConexion.ConectionString);

                // Step 2
                MyConexion.Open();

                // Step 3
                MySqlCommand MyCommand = new MySqlCommand("SELECT id, departamento FROM departamento_autoriza ORDER BY departamento ASC", MyConexion);

                // Step 4
                MySqlDataReader MyReader;
                MyReader = MyCommand.ExecuteReader();

                // Step 5
                DataTable MyDataTable = new DataTable();
                MyDataTable.Columns.Add("id", typeof(int));
                MyDataTable.Columns.Add("departamento", typeof(string));
                MyDataTable.Load(MyReader);

                // Step 6
                cmbAutorizadoPor.ValueMember = "id";
                cmbAutorizadoPor.DisplayMember = "departamento";
                cmbAutorizadoPor.DataSource = MyDataTable;

                // Step 7
                MyConexion.Close();
            }
            catch (Exception myEx)
            {
                MessageBox.Show(myEx.Message);
                throw;
            }
        }

        private void Limpiar()
        {
            txtCodigo.Clear();
            txtBeneficiario.Clear();
            cmbRenglonBeneficiario.Refresh();
            cmbTipoCombustible.Refresh();
            txtCantidad.Clear();
            txtNota.Clear();
            cmbAutorizadoPor.Refresh();
            dtFecha.Refresh();
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
                MyCommand.CommandText = "SELECT count(*) FROM movimientocombustible";

                // Step 4 - Open connection
                MyConexion.Open();

                // Step 5 - Execute the SQL Statement y Asigno el valor resultante a la variable "codigo"
                int codigo;
                codigo = Convert.ToInt32(MyCommand.ExecuteScalar());
                codigo = codigo + 1;
                txtCodigo.Text = Convert.ToString(codigo);                

                // Step 5 - Close the connection
                MyConexion.Close();
            }
            catch (MySqlException MyEx)
            {
                MessageBox.Show(MyEx.Message);
            }

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
                    this.btnImprimir.Enabled = false;
                    this.btnEliminar.Enabled = false;
                    this.btnCancelar.Enabled = false;
                    this.btnSalir.Enabled = true;
                    //
                    txtCodigo.Enabled = true;
                    txtBeneficiario.Enabled = false;
                    cmbRenglonBeneficiario.Enabled = false;
                    cmbTipoCombustible.Enabled = false;
                    txtCantidad.Enabled = false;
                    txtNota.Enabled = false;
                    cmbAutorizadoPor.Enabled = false;
                    dtFecha.Enabled = false;
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
                    txtCodigo.Enabled = false;
                    txtBeneficiario.Enabled = true;
                    cmbRenglonBeneficiario.Enabled = true;
                    cmbTipoCombustible.Enabled = true;
                    txtCantidad.Enabled = true;
                    txtNota.Enabled = true;
                    cmbAutorizadoPor.Enabled = true;
                    dtFecha.Enabled = true;
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
                    txtCodigo.Enabled = false;
                    txtBeneficiario.Enabled = false;
                    cmbRenglonBeneficiario.Enabled = false;
                    cmbTipoCombustible.Enabled = false;
                    txtCantidad.Enabled = false;
                    txtNota.Enabled = false;
                    cmbAutorizadoPor.Enabled = false;
                    dtFecha.Enabled = false;
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
                    txtCodigo.Enabled = false;
                    txtBeneficiario.Enabled = true;
                    cmbRenglonBeneficiario.Enabled = true;
                    cmbTipoCombustible.Enabled = true;
                    txtCantidad.Enabled = true;
                    txtNota.Enabled = true;
                    cmbAutorizadoPor.Enabled = true;
                    dtFecha.Enabled = true;
                    break;

                case "Buscar":
                    this.btnNuevo.Enabled = true;
                    this.btnGrabar.Enabled = false;
                    this.btnEditar.Enabled = false;
                    this.btnBuscar.Enabled = true;
                    this.btnImprimir.Enabled = false;
                    this.btnEliminar.Enabled = false;
                    this.btnCancelar.Enabled = false;
                    this.btnSalir.Enabled = true;
                    //
                    txtCodigo.Enabled = false;
                    txtBeneficiario.Enabled = false;
                    cmbRenglonBeneficiario.Enabled = false;
                    cmbTipoCombustible.Enabled = false;
                    txtCantidad.Enabled = false;
                    txtNota.Enabled = false;
                    cmbAutorizadoPor.Enabled = false;
                    dtFecha.Enabled = false;
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
                    txtCodigo.Enabled = false;
                    txtBeneficiario.Enabled = false;
                    cmbRenglonBeneficiario.Enabled = false;
                    cmbTipoCombustible.Enabled = false;
                    txtCantidad.Enabled = false;
                    txtNota.Enabled = false;
                    cmbAutorizadoPor.Enabled = false;
                    dtFecha.Enabled = false;
                    break;

                default:
                    break;
            }

        }

        private void btnNuevo_Click(object sender, EventArgs e)
        {
            this.cModo = "Nuevo";
            this.Limpiar();
            this.Botones();
            this.ProximoCodigo();
            this.txtBeneficiario.Focus();
        }

        private void buscarCombustible()
        {
            try
            {
                // Step 1 - Conexion
                MySqlConnection MyConexion = new MySqlConnection(clsConexion.ConectionString);

                // Step 2 - creating the command object
                MySqlCommand myCommand = MyConexion.CreateCommand();

                // Step 3 - Comando a ejecutar                        
                myCommand.CommandText = "SELECT cantidad FROM existencia WHERE tipocombustible = " + cmbTipoCombustible.SelectedValue + "";

                // Step 4 - Opening the connection
                MyConexion.Open();

                // Step 5 - Executing the query
                myCommand.ExecuteNonQuery();

                // Step 5 - Creating the DataReader                    
                MySqlDataReader MyReader = myCommand.ExecuteReader();

                // Step 6 - Verifying if Reader has rows
                if (MyReader.HasRows)
                {
                    while (MyReader.Read())
                    {
                        cCombustible = Convert.ToDecimal(MyReader["cantidad"]);
                    }
                }
                else
                {
                    cCombustible = 0;
                }

                // Step 7 - Closing the connection
                MyConexion.Close();

                // Step 8 - Paso el valor de cCombustible a Entero
                //Combustible = Convert.ToInt32(cCombustible);

            }
            catch (Exception myEx)
            {
                MessageBox.Show(myEx.Message);
                throw;
            }
        }

        private void btnGrabar_Click(object sender, EventArgs e)
        {
            if (txtBeneficiario.Text == "")
            {
                MessageBox.Show("No se permiten campos vacios...");
                txtBeneficiario.Focus();
            }
            else if (txtCantidad.Text == "")
            {
                MessageBox.Show("No se permiten campos vacios...");
                txtCantidad.Focus();
            }
            else if (txtCodigo.Text == "")
            {
                MessageBox.Show("No se permiten campos vacios...");
                txtCodigo.Focus();
            }
            else if (txtNota.Text == "")
            {
                MessageBox.Show("No se permiten campos vacios...");
                txtNota.Focus();
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
                        myCommand.CommandText = "INSERT INTO movimientocombustible(tipo_movimiento, tipo_combustible, cantidad, nota, "+
                            "beneficiario, tipobeneficiario, autorizadopor, fecha, id_solicitud) values(@tipo_movimiento, @tipo_combustible, "+
                            "@cantidad, @nota, @beneficiario, @tipobeneficiario, @autorizadopor, @fecha, @id_solicitud)";
                        myCommand.Parameters.AddWithValue("@tipo_movimiento", "S");
                        myCommand.Parameters.AddWithValue("@tipo_combustible", cmbTipoCombustible.SelectedValue);
                        myCommand.Parameters.AddWithValue("@cantidad", txtCantidad.Text);
                        myCommand.Parameters.AddWithValue("@nota", txtNota.Text);
                        myCommand.Parameters.AddWithValue("@beneficiario", txtBeneficiario.Text);
                        myCommand.Parameters.AddWithValue("@tipobeneficiario", cmbRenglonBeneficiario.SelectedValue);
                        myCommand.Parameters.AddWithValue("@autorizadopor", cmbAutorizadoPor.SelectedValue);
                        myCommand.Parameters.AddWithValue("@fecha", dtFecha.Value.ToString("yyyy-MM-dd HH:mm:ss"));
                        myCommand.Parameters.AddWithValue("@id_solicitud", 0);

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

                    // ACTUALIZANDO INVENTARIO DE COMBUSTIBLE
                    try
                    {
                        // Step 1 - Stablishing the connection
                        MySqlConnection MyConexion = new MySqlConnection(clsConexion.ConectionString);

                        // Step 2 - Crear el comando de ejecucion
                        MySqlCommand myCommand = MyConexion.CreateCommand();

                        // Step 3 - Comando a ejecutar
                        buscarCombustible();
                        cCombustible = cCombustible - Convert.ToInt32(txtCantidad.Text);
                        //
                        myCommand.CommandText = "UPDATE existencia SET cantidad = @cCombustible WHERE tipocombustible = " + cmbTipoCombustible.SelectedValue + "";
                        myCommand.Parameters.AddWithValue("@cCombustible", cCombustible);

                        // Step 4 - Opening the connection
                        MyConexion.Open();

                        // Step 5 - Executing the query
                        myCommand.ExecuteNonQuery();

                        // Step 6 - Closing the connection
                        MyConexion.Close();
                    }
                    catch (Exception myEx)
                    {
                        MessageBox.Show(myEx.Message);
                        throw;
                    }


                }
                else
                {
                    //try
                    //{
                    //    // Step 1 - Stablishing the connection
                    //    MySqlConnection MyConexion = new MySqlConnection(clsConexion.ConectionString);

                    //    // Step 2 - Crear el comando de ejecucion
                    //    MySqlCommand myCommand = MyConexion.CreateCommand();

                    //    // Step 3 - Comando a ejecutar                        
                    //    myCommand.CommandText = "UPDATE movimientocombustible SET tipo_combustible = @tipo_combustible, cantidad = @cantidad, "+
                    //    "nota = @nota, beneficiario = @beneficiario, tipobeneficiario = @tipobeneficiario, autorizadopor = @autorizadopor, "+
                    //    "fecha = @fecha WHERE id = "+ txtCodigo.Text +"";
                    //    myCommand.Parameters.AddWithValue("@tipo_combustible", cmbTipoCombustible.SelectedValue);
                    //    myCommand.Parameters.AddWithValue("@cantidad", txtCantidad.Text);
                    //    myCommand.Parameters.AddWithValue("@nota", txtNota.Text);
                    //    myCommand.Parameters.AddWithValue("@beneficiario", txtBeneficiario.Text);
                    //    myCommand.Parameters.AddWithValue("@tipobeneficiario", cmbRenglonBeneficiario.SelectedValue);
                    //    myCommand.Parameters.AddWithValue("@autorizadopor", cmbAutorizadoPor.SelectedValue);
                    //    myCommand.Parameters.AddWithValue("@fecha", dtFecha.Value.ToString("yyyy-MM-dd HH:mm:ss"));
                        
                    //    // Step 4 - Opening the connection
                    //    MyConexion.Open();

                    //    // Step 5 - Executing the query
                    //    myCommand.ExecuteNonQuery();

                    //    // Step 6 - Closing the connection
                    //    MyConexion.Close();

                    //    MessageBox.Show("Informacion actualizada satisfactoriamente...");
                    //}
                    //catch (Exception myEx)
                    //{
                    //    MessageBox.Show(myEx.Message);
                    //    throw;
                    //}
                }

                this.Limpiar();
                this.cModo = "Inicio";
                this.Botones();
            }
        }

        private void btnEditar_Click(object sender, EventArgs e)
        {
            this.cModo = "Editar";
            this.Botones();
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            if (txtCodigo.Text == "")
            {
                MessageBox.Show("No se pueden hacer busquedas con No.Despacho vacio...");
                txtCodigo.Focus();
            }
            else
            {
                try
                {
                    // Step 1 - Conexion
                    MySqlConnection MyConexion = new MySqlConnection(clsConexion.ConectionString);

                    // Step 2 - creating the command object
                    MySqlCommand MyCommand = MyConexion.CreateCommand();

                    // Step 3 - creating the commandtext
                    string TipoCombustible = "S";
                    MyCommand.CommandText = "SELECT tipo_combustible, cantidad, nota, beneficiario, tipobeneficiario, autorizadopor, fecha " +
                        "FROM movimientocombustible WHERE id = " + txtCodigo.Text + " AND tipo_movimiento = "+ TipoCombustible +"";

                    // Step 4 - connection open
                    MyConexion.Open();

                    // Step 5 - Creating the DataReader                    
                    MySqlDataReader MyReader = MyCommand.ExecuteReader();

                    // Step 6 - Verifying if Reader has rows
                    if (MyReader.HasRows)
                    {
                        while (MyReader.Read())
                        {
                            cmbTipoCombustible.SelectedValue = MyReader["tipo_combustible"].ToString();
                            txtCantidad.Text = MyReader["cantidad"].ToString();
                            txtNota.Text = MyReader["nota"].ToString();
                            txtBeneficiario.Text = MyReader["beneficiario"].ToString();
                            cmbRenglonBeneficiario.SelectedValue = MyReader["tipobeneficiario"].ToString();
                            cmbAutorizadoPor.SelectedValue = MyReader["autorizadopor"].ToString();
                            dtFecha.Value = Convert.ToDateTime(MyReader["fecha"]);
                        }

                        this.cModo = "Buscar";
                        this.Botones();
                    }
                    else
                    {
                        MessageBox.Show("No se encontraron registros...");
                        this.cModo = "Inicio";
                        this.Botones();
                        this.Limpiar();
                        //this.txtYear.Focus();
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
        }

        private void btnImprimir_Click(object sender, EventArgs e)
        {

        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {

        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            if (txtBeneficiario.Text != "")
            {
                DialogResult Result =
                MessageBox.Show("Se perderan Los cambios Realizados" + System.Environment.NewLine + "Desea Guardar los Cambios", "Sistema de Gestion de Combustibles", MessageBoxButtons.YesNo,
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

        private void txtCodigo_TextChanged(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void txtBeneficiario_TextChanged(object sender, EventArgs e)
        {

        }

        private void label7_Click(object sender, EventArgs e)
        {

        }

        private void cmbRenglonBeneficiario_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void txtNota_TextChanged(object sender, EventArgs e)
        {

        }

        private void label8_Click(object sender, EventArgs e)
        {

        }
    }
}
