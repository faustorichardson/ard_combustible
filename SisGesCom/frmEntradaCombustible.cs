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
    public partial class frmEntradaCombustible : frmBase
    {

        string cModo = "Inicio";
        decimal cCombustible = 0;
        //Int32 Combustible = 0;
        bool status;

        public frmEntradaCombustible()
        {
            InitializeComponent();
        }

        private void frmEntradaCombustible_Load(object sender, EventArgs e)
        {
            this.fillComboAutorizado();
            this.fillComboTipoCombustible();
            this.cModo = "Inicio";
            this.Limpiar();
            this.Botones();
        }

        private void fillComboAutorizado(){
            // Step 1 
            MySqlConnection MyConexion = new MySqlConnection(clsConexion.ConectionString);

            // Step 2
            MyConexion.Open();

            // Step 3
            MySqlCommand MyCommand = new MySqlCommand("SELECT id, departamento FROM departamento_autoriza", MyConexion);

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

        private void fillComboTipoCombustible(){
            // Step 1 
            MySqlConnection MyConexion = new MySqlConnection(clsConexion.ConectionString);

            // Step 2
            MyConexion.Open();

            // Step 3
            MySqlCommand MyCommand = new MySqlCommand("SELECT id, combustible FROM tipo_combustible", MyConexion);

            // Step 4
            MySqlDataReader MyReader;
            MyReader = MyCommand.ExecuteReader();

            // Step 5
            DataTable MyDataTable = new DataTable();
            MyDataTable.Columns.Add("id", typeof(int));
            MyDataTable.Columns.Add("combustible", typeof(string));
            MyDataTable.Load(MyReader);

            // Step 6
            cmbCombustible.ValueMember = "id";
            cmbCombustible.DisplayMember = "combustible";
            cmbCombustible.DataSource = MyDataTable;

            // Step 7
            MyConexion.Close();
        }

        private void Limpiar()
        {
            this.txtCodigo.Clear();
            this.cmbCombustible.Refresh();
            this.txtCantidad.Clear();
            this.txtNota.Clear();
            this.dtFecha.Refresh();
            this.cmbAutorizadoPor.Refresh();
        }

        private void Botones()
        {
            switch (cModo)
            {
                case "Inicio":
                    this.btnNuevo.Enabled = true;
                    this.btnGrabar.Enabled = false;
                    this.btnEditar.Enabled = false;
                    this.btnBuscar.Enabled = false;
                    this.btnBuscarSolicitud.Enabled = false;
                    this.btnImprimir.Enabled = false;
                    this.btnEliminar.Enabled = false;
                    this.btnCancelar.Enabled = false;
                    this.btnSalir.Enabled = true;
                    //
                    this.txtCodigo.Enabled = false;
                    this.cmbAutorizadoPor.Enabled = false;
                    break;

                case "Nuevo":
                    this.btnNuevo.Enabled = false;
                    this.btnGrabar.Enabled = true;
                    this.btnEditar.Enabled = false;
                    this.btnBuscar.Enabled = false;
                    this.btnBuscarSolicitud.Enabled = true;
                    this.btnImprimir.Enabled = false;
                    this.btnEliminar.Enabled = false;
                    this.btnCancelar.Enabled = true;
                    this.btnSalir.Enabled = true;
                    //
                    this.txtCodigo.Enabled = true;
                    this.cmbAutorizadoPor.Enabled = true;                    
                    break;

                case "Grabar":
                    this.btnNuevo.Enabled = true;
                    this.btnGrabar.Enabled = false;
                    this.btnEditar.Enabled = false;
                    this.btnBuscar.Enabled = true;
                    this.btnBuscarSolicitud.Enabled = false;
                    this.btnImprimir.Enabled = false;
                    this.btnEliminar.Enabled = false;
                    this.btnCancelar.Enabled = false;
                    this.btnSalir.Enabled = true;
                    //
                    this.txtCodigo.Enabled = true;
                    this.cmbAutorizadoPor.Enabled = false;
                    break;

                case "Editar":
                    this.btnNuevo.Enabled = false;
                    this.btnGrabar.Enabled = true;
                    this.btnEditar.Enabled = false;
                    this.btnBuscar.Enabled = true;
                    this.btnBuscarSolicitud.Enabled = false;
                    this.btnImprimir.Enabled = false;
                    this.btnEliminar.Enabled = true;
                    this.btnCancelar.Enabled = true;
                    this.btnSalir.Enabled = true;
                    //
                    this.txtCodigo.Enabled = true;
                    this.cmbAutorizadoPor.Enabled = true;
                    break;

                case "Buscar":
                    this.btnNuevo.Enabled = true;
                    this.btnGrabar.Enabled = false;
                    this.btnEditar.Enabled = true;
                    this.btnBuscar.Enabled = true;
                    this.btnBuscarSolicitud.Enabled = false;
                    this.btnImprimir.Enabled = false;
                    this.btnEliminar.Enabled = false;
                    this.btnCancelar.Enabled = false;
                    this.btnSalir.Enabled = true;
                    //
                    this.txtCodigo.Enabled = false;
                    this.cmbAutorizadoPor.Enabled = false;
                    break;

                case "Eliminar":
                    break;

                case "Cancelar":
                    this.btnNuevo.Enabled = true;
                    this.btnGrabar.Enabled = false;
                    this.btnEditar.Enabled = false;
                    this.btnBuscar.Enabled = true;
                    this.btnBuscarSolicitud.Enabled = false;
                    this.btnImprimir.Enabled = false;
                    this.btnEliminar.Enabled = false;
                    this.btnCancelar.Enabled = false;
                    this.btnSalir.Enabled = true;
                    //
                    this.txtCodigo.Enabled = false;
                    this.cmbAutorizadoPor.Enabled = false;
                    break;

                default:
                    break;
            }

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
                myCommand.CommandText = "SELECT cantidad FROM existencia WHERE tipocombustible = " + cmbCombustible.SelectedValue + "";

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

        private void buscarData()
        {
            try
                {
                    // Step 1 - Conexion
                    MySqlConnection MyConexion = new MySqlConnection(clsConexion.ConectionString);

                    // Step 2 - creating the command object
                    MySqlCommand MyCommand = MyConexion.CreateCommand();

                    // Step 3 - creating the commandtext
                    MyCommand.CommandText = "SELECT tipo_combustible, cantidad, nota, status " +
                        "FROM solicitud WHERE id = "+ txtCodigo.Text +"";

                    // Step 4 - connection open
                    MyConexion.Open();

                    // Step 5 - Creating the DataReader                    
                    MySqlDataReader MyReader = MyCommand.ExecuteReader();

                    // Step 6 - Verifying if Reader has rows
                    if (MyReader.HasRows)
                    {
                        while (MyReader.Read())
                        {
                            cmbCombustible.SelectedValue = MyReader["tipo_combustible"].ToString();
                            txtCantidad.Text = MyReader["cantidad"].ToString();
                            txtNota.Text = MyReader["nota"].ToString();
                            status = Convert.ToBoolean(MyReader["status"]);
                            //cmbAutorizadoPor.SelectedValue = MyReader["autorizadopor"].ToString();
                            //dtFecha.Value = Convert.ToDateTime(MyReader["fecha"].ToString());                            
                        }

                        //this.cModo = "Nuevo";
                        //this.Botones();
                    }
                    else
                    {
                        MessageBox.Show("No se encontraron registros...");
                        //this.cModo = "Inicio";
                        //this.Botones();
                        //this.Limpiar();
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
        

        private void btnNuevo_Click(object sender, EventArgs e)
        {
            cModo = "Nuevo";
            //this.Limpiar();
            this.Botones();
        }

        private void btnGrabar_Click(object sender, EventArgs e)
        {
            if (txtCodigo.Text == "")
            {
                MessageBox.Show("No se permite procesar una entrada con numero solicitud en blanco...");
                txtCodigo.Focus();
            }
            else if (txtCantidad.Text == "")
            {
                MessageBox.Show("No se permite procesar una entrada con la cantidad combustible en blanco...");
                txtCodigo.Focus();
            }
            else
            {
                if (cModo == "Nuevo")
                {
                    // INSERTANDO EL REGISTRO EN LA TABLA DE MOVIMIENTO DE COMBUSTIBLE
                    try
                    {
                        // Step 1 - Stablishing the connection
                        MySqlConnection MyConexion = new MySqlConnection(clsConexion.ConectionString);

                        // Step 2 - Crear el comando de ejecucion
                        MySqlCommand myCommand = MyConexion.CreateCommand();

                        // Step 3 - Comando a ejecutar                        
                        myCommand.CommandText = "INSERT INTO movimientocombustible(tipo_movimiento, tipo_combustible, cantidad, nota, " +
                        "autorizadopor, fecha, id_solicitud) values(@tipomovimiento, @tipocombustible, @cantidad, @nota,  " +
                            "@autorizadopor, @fecha, @id_pedido)";
                        myCommand.Parameters.AddWithValue("@tipomovimiento", "E");
                        myCommand.Parameters.AddWithValue("@tipocombustible", cmbCombustible.SelectedValue);
                        myCommand.Parameters.AddWithValue("@cantidad", txtCantidad.Text);
                        myCommand.Parameters.AddWithValue("@nota", txtNota.Text);
                        myCommand.Parameters.AddWithValue("@autorizadopor", cmbAutorizadoPor.SelectedValue);
                        myCommand.Parameters.AddWithValue("@fecha", dtFecha.Value.ToString("yyyy-MM-dd HH:mm:ss"));
                        myCommand.Parameters.AddWithValue("@id_pedido", txtCodigo.Text);

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

                    // ACTUALIZANDO EL ESTATUS DE LA SOLICITUD DE COMBUSTIBLE
                    try
                    {
                        // Step 1 - Stablishing the connection
                        MySqlConnection MyConexion = new MySqlConnection(clsConexion.ConectionString);

                        // Step 2 - Crear el comando de ejecucion
                        MySqlCommand myCommand = MyConexion.CreateCommand();

                        // Step 3 - Comando a ejecutar                        
                        myCommand.CommandText = "UPDATE solicitud SET status = @status WHERE id = " + txtCodigo.Text + "";
                        myCommand.Parameters.AddWithValue("@status", 0);

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

                    // ACTUALIZANDO INVENTARIO DE COMBUSTIBLE
                    try
                    {
                        // Step 1 - Stablishing the connection
                        MySqlConnection MyConexion = new MySqlConnection(clsConexion.ConectionString);

                        // Step 2 - Crear el comando de ejecucion
                        MySqlCommand myCommand = MyConexion.CreateCommand();

                        // Step 3 - Comando a ejecutar
                        buscarCombustible();
                        cCombustible = cCombustible + Convert.ToInt32(txtCantidad.Text);
                        //
                        myCommand.CommandText = "UPDATE existencia SET cantidad = @cCombustible WHERE tipocombustible = " + cmbCombustible.SelectedValue + "";
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
                    try
                    {
                        // Step 1 - Stablishing the connection
                        MySqlConnection MyConexion = new MySqlConnection(clsConexion.ConectionString);

                        // Step 2 - Crear el comando de ejecucion
                        MySqlCommand myCommand = MyConexion.CreateCommand();

                        // Step 3 - Comando a ejecutar                        
                        myCommand.CommandText = "UPDATE movimientocombustible SET autorizadopor = @autorizadopor, fecha = @fecha " +
                            "WHERE id_solicitud = " + txtCodigo.Text + "";
                        myCommand.Parameters.AddWithValue("@autorizadopor", cmbAutorizadoPor.SelectedValue);
                        myCommand.Parameters.AddWithValue("@fecha", dtFecha.Value.ToString("yyyy-MM-dd HH:mm:ss"));

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
                MessageBox.Show("No se puede hacer una busqueda con el numero de solicitud vacia...");
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
                    MyCommand.CommandText = "SELECT tipo_combustible, cantidad, nota, autorizadopor, fecha " +
                        "FROM movimientocombustible WHERE id_solicitud = " + txtCodigo.Text + "";

                    // Step 4 - connection open
                    MyConexion.Open();

                    // Step 5 - Creating the DataReader                    
                    MySqlDataReader MyReader = MyCommand.ExecuteReader();

                    // Step 6 - Verifying if Reader has rows
                    if (MyReader.HasRows)
                    {
                        while (MyReader.Read())
                        {
                            cmbCombustible.SelectedValue = MyReader["tipo_combustible"].ToString();
                            txtCantidad.Text = MyReader["cantidad"].ToString();
                            txtNota.Text = MyReader["nota"].ToString();
                            cmbAutorizadoPor.SelectedValue = MyReader["autorizadopor"].ToString();
                            dtFecha.Value = Convert.ToDateTime(MyReader["fecha"].ToString());
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
            if (txtCodigo.Text != "")
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

        private void txtCodigo_Leave(object sender, EventArgs e)
        {            
            this.buscarData();
            if (status == false)
            {
                MessageBox.Show("Este numero de solicitud ya ha sido procesado...");
                this.cModo = "Inicio";
                this.Botones();
            }
        }

        private void btnBuscarSolicitud_Click(object sender, EventArgs e)
        {
            this.buscarData();
            if (status == false)
            {
                MessageBox.Show("Este numero de solicitud ya ha sido procesado...");
                this.Limpiar();
                this.cModo = "Inicio";
                this.Botones();
            }
            
        }
    }
}
