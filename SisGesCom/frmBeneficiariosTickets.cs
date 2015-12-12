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
    public partial class frmBeneficiariosTickets : frmBase
    {

        string status = "0";
        MySqlDataAdapter mySDA;
        MySqlCommandBuilder mySCB;
        DataTable myDT;

        public frmBeneficiariosTickets()
        {
            InitializeComponent();
        }

        private void frmBeneficiariosTickets_Load(object sender, EventArgs e)
        {
            this.Limpiar();
            this.fillGrid();
            this.fillCmbRango();
        }

        private void fillCmbRango()
        {
            // Step 1 
            MySqlConnection MyConexion = new MySqlConnection(clsConexion.ConectionString);

            // Step 2
            MyConexion.Open();

            // Step 3
            MySqlCommand MyCommand = new MySqlCommand("SELECT rango_id, rango_descripcion FROM rangos", MyConexion);

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

        private void Limpiar()
        {
            this.txtCedulaJefe.Clear();
            this.txtNombreJefe.Clear();
            this.txtApellidoJefe.Clear();
            this.cmbRangoJefe.Refresh();
            this.status = "0";
        }

        private void fillGrid()
        {
            // Conexion
            MySqlConnection myConexion = new MySqlConnection(clsConexion.ConectionString);

            // Creando el DataAdapter
            mySDA = new MySqlDataAdapter("SELECT cedula, rango, nombre, apellido FROM tickets", myConexion);

            // 
            myDT = new DataTable();

            // Llenando el Data Adapter
            mySDA.Fill(myDT);

            // Pasando la informacion al GRID
            dgview.DataSource = myDT;
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
                        //this.cModo = "Buscar";
                        //this.Botones();
                    }
                    else
                    {
                        MessageBox.Show("No se encontraron registros con esta cedula...", "SisGesCom", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        //this.txtCedula.Focus();
                        //this.cModo = "Inicio";
                        //this.Botones();
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

        private void BuscarDato()
        {
            try
            {
                // Step 1 - Conexion
                MySqlConnection MyConexion = new MySqlConnection(clsConexion.ConectionString);

                // Step 2 - creating the command object
                MySqlCommand MyCommand = MyConexion.CreateCommand();

                // Step 3 - creating the commandtext
                MyCommand.CommandText = "SELECT cedula, rango, nombre, apellido FROM tickets WHERE cedula = " + txtCedulaJefe.Text + "";

                // Step 4 - connection open
                MyConexion.Open();

                // Step 5 - Creating the DataReader                    
                MySqlDataReader MyReader = MyCommand.ExecuteReader();                    

                // Step 6 - Verifying if Reader has rows
                if (MyReader.HasRows)
                {
                    MessageBox.Show("Este militar ya ha sido agregado al listado...");
                    this.status = "1";
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
            //this.BuscarDato();
            
            //MessageBox.Show(this.status);
            //if (status == "1")
            //{
            //    // No agrega la informacion
            //    MessageBox.Show("Este militar ya ha sido agregado al listado...");
            //}
            //else
            //{
            try
            {
                // Step 1 - Stablishing the connection
                MySqlConnection MyConexion = new MySqlConnection(clsConexion.ConectionString);

                // Step 2 - Crear el comando de ejecucion
                MySqlCommand myCommand = MyConexion.CreateCommand();

                // Step 3 - Comando a ejecutar
                myCommand.CommandText = "INSERT INTO tickets(cedula, rango, nombre, apellido)" +
                    " values(@cedula, @rango, @nombre, @apellido)";
                myCommand.Parameters.AddWithValue("@cedula", txtCedulaJefe.Text);
                myCommand.Parameters.AddWithValue("@rango", cmbRangoJefe.SelectedValue);
                myCommand.Parameters.AddWithValue("@nombre", txtNombreJefe.Text);
                myCommand.Parameters.AddWithValue("@apellido", txtApellidoJefe.Text);

                // Step 4 - Opening the connection
                MyConexion.Open();

                // Step 5 - Executing the query
                myCommand.ExecuteNonQuery();

                // Step 6 - Closing the connection
                MyConexion.Close();

                //MessageBox.Show("Informacion guardada satisfactoriamente...");

                // Limpio Campos
                this.Limpiar();

                // actualizo el Grid
                this.fillGrid();

            }
            catch (Exception myEx)
            {
                MessageBox.Show(myEx.Message);
                throw;
            }
            //}
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            try
            {
                // Step 1 - Stablishing the connection
                MySqlConnection MyConexion = new MySqlConnection(clsConexion.ConectionString);

                // Step 2 - Crear el comando de ejecucion
                MySqlCommand myCommand = MyConexion.CreateCommand();

                // Step 3 - Comando a ejecutar
                myCommand.CommandText = "DELETE from tickets WHERE cedula = @cedula";
                myCommand.Parameters.AddWithValue("@cedula", txtCedulaJefe.Text);
                
                // Step 4 - Opening the connection
                MyConexion.Open();

                // Step 5 - Executing the query
                myCommand.ExecuteNonQuery();

                // Step 6 - Closing the connection
                MyConexion.Close();

                //MessageBox.Show("Informacion guardada satisfactoriamente...");

                // Limpio campos
                this.Limpiar();

                // actualizo el Grid
                this.fillGrid();
            }
            catch (Exception myEx)
            {
                MessageBox.Show(myEx.Message);
                throw;
            }
        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }
    }
}
