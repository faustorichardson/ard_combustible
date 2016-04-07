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
    public partial class frmAgregarUsuario : frmBase
    {
        string cModo;

        public frmAgregarUsuario()
        {
            InitializeComponent();
        }

        private void frmAgregarUsuario_Load(object sender, EventArgs e)
        {
            this.Limpiar();
            this.cModo = "Inicio";
            this.Botones();
        }

        private void Limpiar()
        {
            this.txtID.Clear();
            this.txtUsuario.Clear();
            this.txtPassword.Clear();
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
                    this.txtUsuario.Enabled = false;
                    this.txtPassword.Enabled = false;
                    this.statusA.Enabled = false;
                    this.statusB.Enabled = false;
                    this.npAdmin.Enabled = false;
                    this.npAyudante.Enabled = false;
                    this.npDigitador.Enabled = false;
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
                    this.txtUsuario.Enabled = true;
                    this.txtPassword.Enabled = true;
                    this.statusA.Enabled = true;
                    this.statusB.Enabled = true;
                    this.npAdmin.Enabled = true;
                    this.npAyudante.Enabled = true;
                    this.npDigitador.Enabled = true;
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
                    this.txtUsuario.Enabled = false;
                    this.txtPassword.Enabled = false;
                    this.statusA.Enabled = false;
                    this.statusB.Enabled = false;
                    this.npAdmin.Enabled = false;
                    this.npAyudante.Enabled = false;
                    this.npDigitador.Enabled = false;
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
                    this.txtUsuario.Enabled = true;
                    this.txtPassword.Enabled = true;
                    this.statusA.Enabled = true;
                    this.statusB.Enabled = true;
                    this.npAdmin.Enabled = true;
                    this.npAyudante.Enabled = true;
                    this.npDigitador.Enabled = true;
                    break;

                case "Buscar":
                    this.btnNuevo.Enabled = true;
                    this.btnGrabar.Enabled = false;
                    this.btnEditar.Enabled = true;
                    this.btnBuscar.Enabled = true;
                    this.btnImprimir.Enabled = false;
                    this.btnEliminar.Enabled = false;
                    this.btnCancelar.Enabled = false;
                    this.btnSalir.Enabled = true;
                    //
                    this.txtUsuario.Enabled = false;
                    this.txtPassword.Enabled = false;
                    this.statusA.Enabled = false;
                    this.statusB.Enabled = false;
                    this.npAdmin.Enabled = false;
                    this.npAyudante.Enabled = false;
                    this.npDigitador.Enabled = false;
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
                    this.txtUsuario.Enabled = false;
                    this.txtPassword.Enabled = false;
                    this.statusA.Enabled = false;
                    this.statusB.Enabled = false;
                    this.npAdmin.Enabled = false;
                    this.npAyudante.Enabled = false;
                    this.npDigitador.Enabled = false;
                    break;

                default:
                    break;
            }

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
                MyCommand.CommandText = "SELECT count(*) FROM usuarios";

                // Step 4 - Open connection
                MyConexion.Open();

                // Step 5 - Execute the SQL Statement y Asigno el valor resultante a la variable "codigo"
                int codigo;
                codigo = Convert.ToInt32(MyCommand.ExecuteScalar());
                codigo = codigo + 1;
                txtID.Text = Convert.ToString(codigo);
                txtUsuario.Focus();

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
            this.Limpiar();
            this.cModo = "Nuevo";
            this.Botones();
            this.ProximoCodigo();
        }

        private void btnGrabar_Click(object sender, EventArgs e)
        {

            if (txtUsuario.Text == "")
            {
                MessageBox.Show("No se permiten campos vacios...");
                txtUsuario.Focus();
            }
            else if (txtPassword.Text == "") {
                MessageBox.Show("No se permiten campos vacios...");
                txtPassword.Focus();
            }
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
                        myCommand.CommandText = "INSERT INTO usuarios(usuario, clave, status, nivelpermiso)" +
                            " values(@usuario, @clave, @status, @nivelpermiso)";
                        myCommand.Parameters.AddWithValue("@usuario", txtUsuario.Text);
                        myCommand.Parameters.AddWithValue("@clave", txtPassword.Text);
                        if(statusA.Checked == true){
                            myCommand.Parameters.AddWithValue("@status", 1);
                        } else{
                            myCommand.Parameters.AddWithValue("@status", 0);
                        }
                        if (npAdmin.Checked == true)
                        {
                            myCommand.Parameters.AddWithValue("@nivelpermiso", 1);
                        }
                        else if (npAyudante.Checked == true)
                        {
                            myCommand.Parameters.AddWithValue("@nivelpermiso", 2);
                        }
                        else
                        {
                            myCommand.Parameters.AddWithValue("@nivelpermiso", 3);
                        }                        

                        // Step 4 - Opening the connection
                        MyConexion.Open();

                        // Step 5 - Executing the query
                        myCommand.ExecuteNonQuery();

                        // Step 6 - Closing the connection
                        MyConexion.Close();

                        MessageBox.Show("Informacion guardada satisfactoriamente...");
                    }
                    catch (Exception MyEx)
                    {
                        MessageBox.Show(MyEx.Message);
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
                        myCommand.CommandText = "UPDATE usuarios SET usuario = @usuario, clave = @clave, " +
                            "status = @status, nivelpermiso = @nivelpermiso WHERE idusuarios = " + txtID.Text + "";
                        myCommand.Parameters.AddWithValue("@usuario", txtUsuario.Text);
                        myCommand.Parameters.AddWithValue("@clave", txtPassword.Text);
                        if (statusA.Checked == true)
                        {
                            myCommand.Parameters.AddWithValue("@status", 1);
                        }
                        else
                        {
                            myCommand.Parameters.AddWithValue("@status", 0);
                        }
                        if (npAdmin.Checked == true)
                        {
                            myCommand.Parameters.AddWithValue("@nivelpermiso", 1);
                        }
                        else if (npAyudante.Checked == true)
                        {
                            myCommand.Parameters.AddWithValue("@nivelpermiso", 2);
                        }
                        else
                        {
                            myCommand.Parameters.AddWithValue("@nivelpermiso", 3);
                        }

                        // Step 4 - Opening the connection
                        MyConexion.Open();

                        // Step 5 - Executing the query
                        myCommand.ExecuteNonQuery();

                        // Step 6 - Closing the connection
                        MyConexion.Close();

                        MessageBox.Show("Informacion actualizada satisfactoriamente...");
                    }
                    catch (Exception MyEx)
                    {
                        MessageBox.Show(MyEx.Message);
                    }

                }

                this.Limpiar();
                cModo = "Inicio";
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
            frmBuscarUsuario ofrmBuscarUsuario = new frmBuscarUsuario();
            ofrmBuscarUsuario.ShowDialog();
            string cCodigo = ofrmBuscarUsuario.cCodigo;

            // Si selecciono un registro
            if (cCodigo != "" && cCodigo != null)
            {
                // Mostrar el codigo                      
                txtID.Text = Convert.ToString(cCodigo).Trim();
                try
                {
                    // Step 1 - clsConexion
                    MySqlConnection MyclsConexion = new MySqlConnection(clsConexion.ConectionString);

                    // Step 2 - creating the command object
                    MySqlCommand MyCommand = MyclsConexion.CreateCommand();

                    // Step 3 - creating the commandtext
                    //MyCommand.CommandText = "SELECT *  FROM paciente WHERE cedula = ' " + txtCedula.Text.Trim() + "'  " ;
                    MyCommand.CommandText = "SELECT * from usuarios WHERE idusuarios = '" + txtID.Text.Trim() + "'";

                    // Step 4 - connection open
                    MyclsConexion.Open();

                    // Step 5 - Creating the DataReader                    
                    MySqlDataReader MyReader = MyCommand.ExecuteReader();

                    // Step 6 - Verifying if Reader has rows
                    if (MyReader.HasRows)
                    {
                        while (MyReader.Read())
                        {
                            txtUsuario.Text = MyReader["usuario"].ToString();
                            txtPassword.Text = MyReader["clave"].ToString();
                            if (MyReader["status"].ToString() == "1")
                            {
                                statusA.Checked = true;
                            }
                            else
                            {
                                statusB.Checked = true;
                            }
                            if (MyReader["nivelpermiso"].ToString() == "1")
                            {
                                npAdmin.Checked = true;
                            }
                            else if (MyReader["nivelpermiso"].ToString() == "2")
                            {
                                npAyudante.Checked = true;
                            }
                            else
                            {
                                npDigitador.Checked = true;
                            }
                        }
                        //this.cModo = "Buscar";
                        //this.Botones();
                    }
                    else
                    {
                        MessageBox.Show("No se encontraron registros con este ID...", "SisGesCom", MessageBoxButtons.OK, MessageBoxIcon.Information);
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

        private void btnImprimir_Click(object sender, EventArgs e)
        {

        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {

        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            if (txtUsuario.Text != "")
            {
                DialogResult Result =
                MessageBox.Show("Se perderan Los cambios Realizados" + System.Environment.NewLine + "Desea Guardar los Cambios", "Sistema de Gestion de Combustibles v1.0", MessageBoxButtons.YesNo,
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
    }
}
