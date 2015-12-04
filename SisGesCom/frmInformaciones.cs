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
                    this.btnGrabar.Enabled = false;
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
                myCommand.CommandText = "UPDATE info SET cdtegral = @cdtegral, enccomb = @enccomb WHERE id = 1";
                myCommand.Parameters.AddWithValue("@cdtegral", txtCedulaJefe.Text);
                myCommand.Parameters.AddWithValue("@enccomb", txtCedulaEncargado.Text);                

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

        }

        private void btnBuscarEncargado_Click(object sender, EventArgs e)
        {

        }


    }
}
