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
    public partial class frmInformaciones : frmBase
    {

        string cModo = "Inicio";

        public frmInformaciones()
        {
            InitializeComponent();
        }

        private void frmInformaciones_Load(object sender, EventArgs e)
        {
            cModo = "Inicio";
            this.Botones();
            this.FillComboJefe();
            this.FillComboEncargado();
            this.BuscarInformaciones();
        }

        private void FillComboEncargado()
        {
            // Step 1 
            MySqlConnection MyConexion = new MySqlConnection(clsConexion.ConectionString);

            // Step 2
            MyConexion.Open();

            // Step 3
            MySqlCommand MyCommand = new MySqlCommand("SELECT rango_id, rango_descripcion FROM rangos ORDER BY rango_id ASC", MyConexion);

            // Step 4
            MySqlDataReader MyReader;
            MyReader = MyCommand.ExecuteReader();

            // Step 5
            DataTable MyDataTable = new DataTable();
            MyDataTable.Columns.Add("rango_id", typeof(int));
            MyDataTable.Columns.Add("rango_descripcion", typeof(string));
            MyDataTable.Load(MyReader);

            // Step 6            
            cmbRangoEncargado.ValueMember = "rango_id";
            cmbRangoEncargado.DisplayMember = "rango_descripcion";
            cmbRangoEncargado.DataSource = MyDataTable;

            // Step 7
            MyConexion.Close();
        }

        private void FillComboJefe()
        {
            // Step 1 
            MySqlConnection MyConexion = new MySqlConnection(clsConexion.ConectionString);

            // Step 2
            MyConexion.Open();

            // Step 3
            MySqlCommand MyCommand = new MySqlCommand("SELECT rango_id, rango_descripcion FROM rangos ORDER BY rango_id ASC", MyConexion);

            // Step 4
            MySqlDataReader MyReader;
            MyReader = MyCommand.ExecuteReader();

            // Step 5
            DataTable MyDataTable = new DataTable();
            MyDataTable.Columns.Add("rango_id", typeof(int));
            MyDataTable.Columns.Add("rango_descripcion", typeof(string));
            MyDataTable.Load(MyReader);

            // Step 6            
            cmbRangoJefe.ValueMember = "rango_id";
            cmbRangoJefe.DisplayMember = "rango_descripcion";
            cmbRangoJefe.DataSource = MyDataTable;            

            // Step 7
            MyConexion.Close();
        }

        private void Botones()
        {
            switch (cModo)
            {
                case "Inicio":                    
                    this.btnGrabar.Enabled = false;
                    this.btnEditar.Enabled = true;                    
                    this.btnCancelar.Enabled = false;
                    this.btnSalir.Enabled = true;
                    this.btnBuscarEncargado.Enabled = false;
                    this.btnBuscarJefe.Enabled = false;                    
                    break;

                //case "Nuevo":
                //    this.btnNuevo.Enabled = false;
                //    this.btnGrabar.Enabled = true;
                //    this.btnEditar.Enabled = false;
                //    this.btnBuscar.Enabled = false;
                //    this.btnEliminar.Enabled = false;
                //    this.btnCancelar.Enabled = true;
                //    this.btnSalir.Enabled = true;
                //    break;

                case "Grabar":
                    this.btnGrabar.Enabled = false;
                    this.btnEditar.Enabled = false;                    
                    this.btnCancelar.Enabled = false;
                    this.btnSalir.Enabled = true;
                    this.btnBuscarEncargado.Enabled = false;
                    this.btnBuscarJefe.Enabled = false;
                    break;

                case "Editar":
                    this.btnGrabar.Enabled = true;
                    this.btnEditar.Enabled = false;                    
                    this.btnCancelar.Enabled = false;
                    this.btnSalir.Enabled = true;
                    this.btnBuscarEncargado.Enabled = true;
                    this.btnBuscarJefe.Enabled = true;
                    break;

                //case "Buscar":
                //    this.btnNuevo.Enabled = true;
                //    this.btnGrabar.Enabled = false;
                //    this.btnEditar.Enabled = true;
                //    this.btnBuscar.Enabled = true;
                //    this.btnEliminar.Enabled = false;
                //    this.btnCancelar.Enabled = false;
                //    this.btnSalir.Enabled = true;
                //    break;

                //case "Eliminar":
                //    break;

                case "Cancelar":
                    this.btnGrabar.Enabled = false;
                    this.btnEditar.Enabled = false;                    
                    this.btnCancelar.Enabled = false;
                    this.btnSalir.Enabled = true;
                    this.btnBuscarEncargado.Enabled = false;
                    this.btnBuscarJefe.Enabled = false;
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
                MyCommand.CommandText = "SELECT cdtegral, cdtegral_rango, cdtegral_nombre, cdtegral_apellido, " +
                    "enccomb, enccomb_rango, enccomb_nombre, enccomb_apellido FROM info WHERE id = 1";

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
                        
                        txtCedulaJefe.Text = MyReader["cdtegral"].ToString();
                        if (txtCedulaJefe.Text == "")
                        {
                            break;
                        }

                        cmbRangoJefe.SelectedValue = MyReader["cdtegral_rango"].ToString();
                        if (cmbRangoJefe.SelectedValue.ToString() == "")
                        {
                            break;
                        }

                        txtNombreJefe.Text = MyReader["cdtegral_nombre"].ToString();
                        if (txtNombreJefe.Text == "")
                        {
                            break;
                        }

                        txtApellidoJefe.Text = MyReader["cdtegral_apellido"].ToString();
                        if (txtApellidoJefe.Text == "")
                        {
                            break;
                        }

                        // Informaciones del Encargado Combustible

                        txtCedulaEncargado.Text = MyReader["enccomb"].ToString();
                        if (txtCedulaEncargado.Text == "")
                        {
                            break;
                        }
                        
                        cmbRangoEncargado.SelectedValue = MyReader["enccomb_rango"].ToString();
                        if (cmbRangoEncargado.SelectedValue.ToString() == "")
                        {
                            break;
                        }

                        txtNombreEncargado.Text = MyReader["enccomb_nombre"].ToString();
                        if (txtNombreEncargado.Text == "")
                        {
                            break;
                        }

                        txtApellidoEncargado.Text = MyReader["enccomb_apellido"].ToString();
                        if (txtApellidoEncargado.Text == "")
                        {
                            break;
                        }

                    }                   
                }

            }
            catch (Exception myEx)
            {
                MessageBox.Show(myEx.Message);
                throw;
            }            

        }

        private void btnGrabar_Click(object sender, EventArgs e)
        {
            try
            {
                // Step 1 - Stablishing the connection
                MySqlConnection MyConexion = new MySqlConnection(clsConexion.ConectionString);

                // Step 2 - Crear el comando de ejecucion
                MySqlCommand myCommand = MyConexion.CreateCommand();

                // Step 3 - Comando a ejecutar
                myCommand.CommandText = "UPDATE info SET cdtegral = @cdtegral, cdtegral_rango = @cdtegral_rango, cdtegral_nombre = @cdtegral_nombre, "+
                    "cdtegral_apellido = @cdtegral_apellido, enccomb = @enccomb, enccomb_rango = @enccomb_rango, enccomb_nombre = @enccomb_nombre, "+
                    "enccomb_apellido = @enccomb_apellido WHERE id = 1";
                myCommand.Parameters.AddWithValue("@cdtegral", txtCedulaJefe.Text);
                myCommand.Parameters.AddWithValue("@cdtegral_rango", cmbRangoJefe.SelectedValue);
                myCommand.Parameters.AddWithValue("@cdtegral_nombre", txtNombreJefe.Text);
                myCommand.Parameters.AddWithValue("@cdtegral_apellido", txtApellidoJefe.Text);
                myCommand.Parameters.AddWithValue("@enccomb", txtCedulaEncargado.Text);
                myCommand.Parameters.AddWithValue("@enccomb_rango", cmbRangoEncargado.SelectedValue);
                myCommand.Parameters.AddWithValue("@enccomb_nombre", txtNombreEncargado.Text);
                myCommand.Parameters.AddWithValue("@enccomb_apellido", txtApellidoEncargado.Text);

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

        private void btnEditar_Click(object sender, EventArgs e)
        {
            this.cModo = "Editar";
            this.Botones();
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            DialogResult Result =
            MessageBox.Show("Se perderan Los cambios Realizados" + System.Environment.NewLine + "Desea Guardar los Cambios", "Sistema de Gestion de Combustibles", MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question);
            switch (Result)
            {
                case DialogResult.Yes:
                    //cModo = "Actualiza";
                    btnGrabar_Click(sender, e);
                    break;
            }            
            
            this.cModo = "Inicio";
            this.Botones();

        }

        private void btnSalir_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnBuscarJefe_Click(object sender, EventArgs e)
        {
            frmBuscarMilitar ofrmBuscarMilitar = new frmBuscarMilitar();
            ofrmBuscarMilitar.ShowDialog();            
            string cCodigo = ofrmBuscarMilitar.cCodigo;

            // Si selecciono un registro
            if (cCodigo != "" && cCodigo != null)
            {
                // Mostrar el codigo                      
                txtCedulaJefe.Text = Convert.ToString(cCodigo).Trim();
                try
                {
                    // Step 1 - clsConexion
                    MySqlConnection MyclsConexion = new MySqlConnection(clsConexion.ConectionString);

                    // Step 2 - creating the command object
                    MySqlCommand MyCommand = MyclsConexion.CreateCommand();

                    // Step 3 - creating the commandtext
                    //MyCommand.CommandText = "SELECT *  FROM paciente WHERE cedula = ' " + txtCedula.Text.Trim() + "'  " ;
                    MyCommand.CommandText = "SELECT * from militar WHERE cedula = '" + txtCedulaJefe.Text.Trim() + "'";

                    // Step 4 - connection open
                    MyclsConexion.Open();

                    // Step 5 - Creating the DataReader                    
                    MySqlDataReader MyReader = MyCommand.ExecuteReader();

                    // Step 6 - Verifying if Reader has rows
                    if (MyReader.HasRows)
                    {
                        while (MyReader.Read())
                        {
                            cmbRangoJefe.SelectedValue = MyReader["rango"].ToString();
                            txtNombreJefe.Text = MyReader["nombre"].ToString();
                            txtApellidoJefe.Text = MyReader["apellido"].ToString();                            
                        }
                        this.cModo = "Buscar";
                        this.Botones();
                    }
                    else
                    {
                        MessageBox.Show("No se encontraron registros con esta cedula...", "SisGesCom", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        //this.txtCedula.Focus();
                        this.cModo = "Inicio";
                        this.Botones();
                        //this.Limpiar();
                        //this.txtID.Focus();
                    }
                    // Step 6 - Closing all
                    MyReader.Close();
                    MyCommand.Dispose();
                    MyclsConexion.Close();
                }
                catch (Exception MyEx)
                {
                    MessageBox.Show(MyEx.Message);
                }
            }
        }

        private void btnBuscarEncargado_Click(object sender, EventArgs e)
        {
            frmBuscarMilitar ofrmBuscarMilitar = new frmBuscarMilitar();
            ofrmBuscarMilitar.ShowDialog();
            string cCodigo = ofrmBuscarMilitar.cCodigo;

            // Si selecciono un registro
            if (cCodigo != "" && cCodigo != null)
            {
                // Mostrar el codigo                      
                txtCedulaEncargado.Text = Convert.ToString(cCodigo).Trim();
                try
                {
                    // Step 1 - clsConexion
                    MySqlConnection MyclsConexion = new MySqlConnection(clsConexion.ConectionString);

                    // Step 2 - creating the command object
                    MySqlCommand MyCommand = MyclsConexion.CreateCommand();

                    // Step 3 - creating the commandtext
                    //MyCommand.CommandText = "SELECT *  FROM paciente WHERE cedula = ' " + txtCedula.Text.Trim() + "'  " ;
                    MyCommand.CommandText = "SELECT * from militar WHERE cedula = '" + txtCedulaEncargado.Text.Trim() + "'";

                    // Step 4 - connection open
                    MyclsConexion.Open();

                    // Step 5 - Creating the DataReader                    
                    MySqlDataReader MyReader = MyCommand.ExecuteReader();

                    // Step 6 - Verifying if Reader has rows
                    if (MyReader.HasRows)
                    {
                        while (MyReader.Read())
                        {
                            cmbRangoEncargado.SelectedValue = MyReader["rango"].ToString();
                            txtNombreEncargado.Text = MyReader["nombre"].ToString();
                            txtApellidoEncargado.Text = MyReader["apellido"].ToString();
                        }
                        this.cModo = "Buscar";
                        this.Botones();
                    }
                    else
                    {
                        MessageBox.Show("No se encontraron registros con esta cedula...", "SisGesCom", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        //this.txtCedula.Focus();
                        this.cModo = "Inicio";
                        this.Botones();
                        //this.Limpiar();
                        //this.txtID.Focus();
                    }
                    // Step 6 - Closing all
                    MyReader.Close();
                    MyCommand.Dispose();
                    MyclsConexion.Close();
                }
                catch (Exception MyEx)
                {
                    MessageBox.Show(MyEx.Message);
                }
            }
        }


    }
}
