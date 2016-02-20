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
    public partial class frmRegistroTickets : frmBase
    {
        decimal cantExistTickets = 0;
        decimal cantTickets = 0;
        string cModo = "Inicio";
        decimal cCombustible = 0;

        public frmRegistroTickets()
        {
            InitializeComponent();
        }

        private void frmRegistroTickets_Load(object sender, EventArgs e)
        {
            this.Limpiar();
            this.cModo = "Inicio";
            this.Botones();
            //this.fillCmbAutorizadoPor();
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
                //cmbAutorizadoPor.ValueMember = "id";
                //cmbAutorizadoPor.DisplayMember = "departamento";
                //cmbAutorizadoPor.DataSource = MyDataTable;

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
            this.txtCantidad.Clear();
            this.txtCodigo.Clear();
            this.cantExistTickets = 0;
            this.cantTickets = 0;
            this.cCombustible = 0;
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
                myCommand.CommandText = "SELECT cantidad FROM existencia WHERE tipocombustible = 1000";

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

        private void ProximoCodigo()
        {
            try
            {
                // Step 1 - Connection stablished
                MySqlConnection MyConexion = new MySqlConnection(clsConexion.ConectionString);

                // Step 2 - Create command
                MySqlCommand MyCommand = MyConexion.CreateCommand();

                // Step 3 - Set the commanndtext property
                MyCommand.CommandText = "SELECT count(*) FROM movimientotickets";

                // Step 4 - Open connection
                MyConexion.Open();

                // Step 5 - Execute the SQL Statement y Asigno el valor resultante a la variable "codigo"
                int codigo;
                codigo = Convert.ToInt32(MyCommand.ExecuteScalar());
                codigo = codigo + 1;
                txtCodigo.Text = Convert.ToString(codigo);
                txtCantidad.Focus();               

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
                    this.dtFecha.Enabled = false;
                    this.txtCodigo.Enabled = true;
                    this.txtCantidad.Enabled = false;
                    //this.cmbAutorizadoPor.Enabled = false;
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
                    this.dtFecha.Enabled = true;
                    this.txtCodigo.Enabled = false;
                    this.txtCantidad.Enabled = true;
                    //this.cmbAutorizadoPor.Enabled = true;
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
                    this.dtFecha.Enabled = false;
                    this.txtCodigo.Enabled = true;
                    this.txtCantidad.Enabled = false;
                    //this.cmbAutorizadoPor.Enabled = false;
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
                    this.dtFecha.Enabled = true;
                    this.txtCodigo.Enabled = false;
                    this.txtCantidad.Enabled = true;
                    //this.cmbAutorizadoPor.Enabled = true;
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
                    this.dtFecha.Enabled = false;
                    this.txtCodigo.Enabled = true;
                    this.txtCantidad.Enabled = false;
                    //this.cmbAutorizadoPor.Enabled = false;
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
                    this.dtFecha.Enabled = false;
                    this.txtCodigo.Enabled = true;
                    this.txtCantidad.Enabled = false;
                    //this.cmbAutorizadoPor.Enabled = false;
                    break;

                default:
                    break;
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
            if (txtCantidad.Text == "")
            {
                MessageBox.Show("No se puede grabar con cantidad en blanco...");
                txtCantidad.Focus();
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
                        myCommand.CommandText = "INSERT INTO movimientotickets(tipo_movimiento, cantidad, fecha)"+
                            " values(@tipo_movimiento, @cantidad, @fecha)";
                        myCommand.Parameters.AddWithValue("@tipo_movimiento", 'E');
                        myCommand.Parameters.AddWithValue("@cantidad", txtCantidad.Text);
                        myCommand.Parameters.AddWithValue("@fecha", dtFecha.Value.ToString("yyyy-MM-dd"));
                        //myCommand.Parameters.AddWithValue("@autorizadopor", cmbAutorizadoPor.SelectedValue);

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
                        cCombustible = cCombustible + Convert.ToInt32(txtCantidad.Text);
                        //
                        myCommand.CommandText = "UPDATE existencia SET cantidad = @cCombustible WHERE tipocombustible = 1000";
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
                    if (txtCodigo.Text == "")
                    {
                        MessageBox.Show("No se puede actualizar datos sin un numero de entrada");
                        this.txtCodigo.Focus();
                    }
                    else if (txtCantidad.Text == "")
                    {
                        MessageBox.Show("No se puede actualizar datos sin una cantidad..");
                        this.txtCantidad.Focus();
                    }
                    else
                    {
                        try
                        {
                            // BUSCO LA ENTRADA
                            this.BuscarEntrada();

                            // BUSCO INVENTARIO
                            this.BuscarExistencia();

                            // SUMO CANTIDAD AL INVENTARIO
                            this.RestarInventario();

                            // APLICO ACTUALIZACION AL MOVIMIENTO
                            this.ActualizaMovimiento();

                            // ACTUALIZO NUEVAMENTE EL INVENTARIO
                            this.ActualizaInventario();
                        }
                        catch (Exception myEx)
                        {
                            MessageBox.Show(myEx.Message);
                            throw;
                        }

                    }
                }

                // IMPRIMIENDO EL REGISTRO
               // this.imprimeEntrada();

                // LIMPIANDO LOS CAMPOS LUEGO DE REGISTRARLOS
                this.cModo = "Inicio";
                this.Limpiar();
                this.Botones();
            }
        }

        private void ActualizaInventario()
        {
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
                myCommand.CommandText = "UPDATE existencia SET cantidad = @cCombustible WHERE tipocombustible = 1000";
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

        private void ActualizaMovimiento()
        {
            try
            {
                // PROCESO DE SUMAR VARIABLES
                //this.cantExistTickets = this.cantExistTickets + this.cantTickets;

                // Step 1 - Conexion
                MySqlConnection MyConexion = new MySqlConnection(clsConexion.ConectionString);

                // Step 2 - creating the command object
                MySqlCommand MyCommand = MyConexion.CreateCommand();

                // Step 3 - creating the commandtext
                MyCommand.CommandText = "UPDATE movimientotickets SET cantidad = " + txtCantidad.Text + " WHERE id = " + txtCodigo.Text + "";

                // Step 4 - connection open
                MyConexion.Open();

                //// Step 5 - Creating the DataReader                    
                //MySqlDataReader MyReader = MyCommand.ExecuteReader();
                MyCommand.ExecuteNonQuery();
                
                // Step 6 - Closing all                
                MyCommand.Dispose();
                MyConexion.Close();
            }
            catch (Exception myEx)
            {
                MessageBox.Show(myEx.Message);
                throw;
            }
            
        }

        private void BuscarExistencia()
        {
            try
            {
                // Step 1 - Conexion
                MySqlConnection MyConexion = new MySqlConnection(clsConexion.ConectionString);

                // Step 2 - creating the command object
                MySqlCommand MyCommand = MyConexion.CreateCommand();

                // Step 3 - creating the commandtext
                MyCommand.CommandText = "SELECT tipocombustible, cantidad FROM existencia WHERE tipocombustible = 1000";

                // Step 4 - connection open
                MyConexion.Open();

                // Step 5 - Creating the DataReader                    
                MySqlDataReader MyReader = MyCommand.ExecuteReader();

                // Step 6 - Verifying if Reader has rows
                if (MyReader.HasRows)
                {
                    while (MyReader.Read())
                    {
                        this.cantExistTickets = Convert.ToDecimal(MyReader["cantidad"].ToString());
                    }
                }
                else
                {
                    MessageBox.Show("No se encontraron registros de existencia...");
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

        private void RestarInventario()
        {
            try
            {
                // PROCESO DE SUMAR VARIABLES
                this.cantExistTickets = this.cantExistTickets - this.cantTickets;

                // Step 1 - Conexion
                MySqlConnection MyConexion = new MySqlConnection(clsConexion.ConectionString);

                // Step 2 - creating the command object
                MySqlCommand MyCommand = MyConexion.CreateCommand();

                // Step 3 - creating the commandtext
                MyCommand.CommandText = "UPDATE existencia SET cantidad = "+ this.cantExistTickets +" WHERE tipocombustible = 1000";

                // Step 4 - connection open
                MyConexion.Open();

                // Step 5 - Creating the DataReader                    
                MyCommand.ExecuteNonQuery();

                // Step 6 - Closing all
                //MyReader.Close();
                MyCommand.Dispose();
                MyConexion.Close();
            }
            catch (Exception myEx)
            {
                MessageBox.Show(myEx.Message);
                throw;
            }
        }

        private void BuscarEntrada()
        {
            try
            {
                // Step 1 - Conexion
                MySqlConnection MyConexion = new MySqlConnection(clsConexion.ConectionString);

                // Step 2 - creating the command object
                MySqlCommand MyCommand = MyConexion.CreateCommand();

                // Step 3 - creating the commandtext
                MyCommand.CommandText = "SELECT id, cantidad FROM movimientotickets WHERE id = " + txtCodigo.Text + " AND tipo_movimiento = 'E'";

                // Step 4 - connection open
                MyConexion.Open();

                // Step 5 - Creating the DataReader                    
                MySqlDataReader MyReader = MyCommand.ExecuteReader();

                // Step 6 - Verifying if Reader has rows
                if (MyReader.HasRows)
                {
                    while (MyReader.Read())
                    {
                        this.cantTickets = Convert.ToDecimal(MyReader["cantidad"].ToString());                    
                    }            
                }
                else
                {
                    MessageBox.Show("No se encontraron registros anteriores...");                
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

        private void imprimeEntrada()
        {
            DialogResult Result =
            MessageBox.Show("Imprima el Registro de la Entrada de Tickets" + System.Environment.NewLine + "Desea Imprimir la Entrada de Tickets", "Sistema de Gestion de Combustibles", MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question);
            switch (Result)
            {
                case DialogResult.Yes:
                    GenerarReporte();
                    break;
            }
        }

        private void GenerarReporte()
        {

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
                MessageBox.Show("No se puede hacer busquedas sin el numero de entrada...");
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
                    MyCommand.CommandText = "SELECT id, cantidad, fecha FROM movimientotickets WHERE tipo_movimiento = 'E' AND id = " + txtCodigo.Text + "";

                    // Step 4 - connection open
                    MyConexion.Open();

                    // Step 5 - Creating the DataReader                    
                    MySqlDataReader MyReader = MyCommand.ExecuteReader();                    

                    // Step 6 - Verifying if Reader has rows
                    if (MyReader.HasRows)
                    {
                        while (MyReader.Read())
                        {
                            txtCodigo.Text = MyReader["id"].ToString();
                            txtCantidad.Text = MyReader["cantidad"].ToString();
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
                        this.txtCodigo.Focus();
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
            if (txtCantidad.Text != "")
            {
                DialogResult Result =
                MessageBox.Show("Se perderan Los cambios Realizados" + System.Environment.NewLine + "Desea Guardar los Cambios", "Sistema de Gestion de Combustible", MessageBoxButtons.YesNo,
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
