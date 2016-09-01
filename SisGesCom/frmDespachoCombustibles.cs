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
    public partial class frmDespachoCombustibles : frmBase
    {

        string cModo = "Inicio";
        int i;
        DataTable dt = new DataTable();
        int idComb = 0;
        int cantComb = 0;

        public frmDespachoCombustibles()
        {
            InitializeComponent();
        }

        private void frmDespachoCombustibles_Load(object sender, EventArgs e)
        {
            // Creando el Datatable
            this.dtGenerating();

            // Llenando el combo de tipo de combustibles
            this.fillCmbComb();

            // Llenando el combo del departamento beneficiario
            this.fillCmbDepartamento();

            // Llenando el combo del Departamento Autoriza
            this.fillCmbAutorizado();

            // Llenando el combo de la embarcacion
            this.fillCmbEmbarcacion();
            this.cmbEmbarcacion.Enabled = false;

            // Modificando el valor del LblDescripcionCombustible
            this.updatelbl();

            // Funcion Limpiar
            this.Limpiar();

            // Funcion Limpia Combustibles
            this.LimpiaCampo();

            // Inhabilita el combo embarcaciones

            // Funcion Botones
            this.cModo = "Inicio";
            this.Botones();
        }

        private void fillCmbEmbarcacion()
        {
            try
            {
                // Step 1 
                MySqlConnection MyConexion = new MySqlConnection(clsConexion.ConectionString);

                // Step 2
                MyConexion.Open();

                // Step 3
                MySqlCommand MyCommand = new MySqlCommand("SELECT id, unidad FROM unidadesnavales ORDER BY unidad ASC", MyConexion);

                // Step 4
                MySqlDataReader MyReader;
                MyReader = MyCommand.ExecuteReader();

                // Step 5
                DataTable MyDataTable = new DataTable();
                MyDataTable.Columns.Add("id", typeof(int));
                MyDataTable.Columns.Add("unidad", typeof(string));
                MyDataTable.Load(MyReader);

                // Step 6
                cmbEmbarcacion.ValueMember = "id";
                cmbEmbarcacion.DisplayMember = "unidad";
                cmbEmbarcacion.DataSource = MyDataTable;

                // Step 7
                MyConexion.Close();
            }
            catch (Exception myEx)
            {
                MessageBox.Show(myEx.Message);
                throw;
            }
        }

        private void fillCmbAutorizado()
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

        private void dtGenerating()
        {
            try
            {
                // Creando el Datatable
                dt.Columns.Add("Id", typeof(int));
                dt.Columns.Add("Combustible", typeof(string));
                dt.Columns.Add("Cantidad", typeof(Int32));
            }
            catch (Exception myEx)
            {
                MessageBox.Show(myEx.Message);
                throw;
            }
        }

        private void fillCmbDepartamento()
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

        private void fillCmbComb()
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
                cmbCombustible.ValueMember = "id";
                cmbCombustible.DisplayMember = "combustible";
                cmbCombustible.DataSource = MyDataTable;

                // Step 7
                MyConexion.Close();
            }
            catch (Exception myEx)
            {
                MessageBox.Show(myEx.Message);
                throw;
            }
        }        

        private void updatelbl()
        {
            try
            {
                // Step 1 - Conexion
                MySqlConnection MyConexion = new MySqlConnection(clsConexion.ConectionString);

                // Step 2 - creating the command object
                MySqlCommand MyCommand = MyConexion.CreateCommand();

                // Step 3 - creating the commandtext
                MyCommand.CommandText = "SELECT id, combustible FROM tipo_combustible WHERE id = " + cmbCombustible.SelectedValue + "";

                // Step 4 - connection open
                MyConexion.Open();

                // Step 5 - Creating the DataReader                    
                MySqlDataReader MyReader = MyCommand.ExecuteReader();

                // Step 6 - Verifying if Reader has rows
                if (MyReader.HasRows)
                {
                    while (MyReader.Read())
                    {
                        lblDescripcionCombustible.Text = MyReader["combustible"].ToString();
                    }

                }
                else
                {
                    MessageBox.Show("No se encontraron registros para cambiar el LABEL...");
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

        private void LimpiaCampo()
        {
            this.txtCantidad.Clear();
            this.cmbCombustible.Focus();
        }

        private void Limpiar()
        {
            this.txtBeneficiario.Clear();
            this.txtCantidad.Clear();            
            this.txtNota.Clear();
            this.txtCantidad.Clear();
            this.txtCodigo.Clear();            
            //this.dgview.Rows.Clear();
            //this.dgview.Refresh();
            //this.dgview.Rows.Clear();
            this.dt.Clear();
            //this.dtGenerating();
            this.cantComb = 0;
            this.idComb = 0;
            this.cmbEmbarcacion.Enabled = false;
            this.rbTerrestres.Checked = true;
            //this.dgview.Rows.Clear();
            //this.dgview.Refresh();
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
                MyCommand.CommandText = "SELECT count(*) FROM combustible_salida";

                // Step 4 - Open connection
                MyConexion.Open();

                // Step 5 - Execute the SQL Statement y Asigno el valor resultante a la variable "codigo"
                int codigo;
                codigo = Convert.ToInt32(MyCommand.ExecuteScalar());
                codigo = codigo + 1;
                txtCodigo.Text = Convert.ToString(codigo);
                cmbCombustible.Focus();

                // Step 5 - Close the connection
                MyConexion.Close();
            }
            catch (Exception myEx)
            {
                MessageBox.Show(myEx.Message);
                throw;
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
                    this.btnAdiciona.Enabled = false;
                    //this.btnUpdate.Enabled = false;
                    //
                    this.txtCodigo.Enabled = true;
                    this.txtBeneficiario.Enabled = false;
                    this.cmbRenglonBeneficiario.Enabled = false;
                    this.txtCantidad.Enabled = false;
                    this.txtNota.Enabled = false;
                    this.cmbAutorizadoPor.Enabled = false;
                    this.dtFecha.Enabled = false;
                    this.cmbCombustible.Enabled = false;
                    break;

                case "Nuevo":
                    this.btnNuevo.Enabled = false;
                    this.btnGrabar.Enabled = true;
                    this.btnEditar.Enabled = false;
                    this.btnBuscar.Enabled = false;
                    this.btnImprimir.Enabled = false;
                    this.btnEliminar.Enabled = true;
                    this.btnCancelar.Enabled = true;
                    this.btnSalir.Enabled = true;
                    this.btnAdiciona.Enabled = true;
                    //this.btnUpdate.Enabled = true;
                    //
                    this.txtCodigo.Enabled = false;
                    this.txtBeneficiario.Enabled = true;
                    this.cmbRenglonBeneficiario.Enabled = true;
                    this.txtCantidad.Enabled = true;
                    this.txtNota.Enabled = true;
                    this.cmbAutorizadoPor.Enabled = true;
                    this.dtFecha.Enabled = true;
                    this.cmbCombustible.Enabled = true;
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
                    this.btnAdiciona.Enabled = false;
                    //this.btnUpdate.Enabled = false;
                    //
                    this.txtCodigo.Enabled = true;
                    this.txtBeneficiario.Enabled = false;
                    this.cmbRenglonBeneficiario.Enabled = false;
                    this.txtCantidad.Enabled = false;
                    this.txtNota.Enabled = false;
                    this.cmbAutorizadoPor.Enabled = false;
                    this.dtFecha.Enabled = false;
                    this.cmbCombustible.Enabled = false;
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
                    this.btnAdiciona.Enabled = true;
                    //this.btnUpdate.Enabled = true;
                    //
                    this.txtCodigo.Enabled = false;
                    this.txtBeneficiario.Enabled = true;
                    this.cmbRenglonBeneficiario.Enabled = true;
                    this.txtCantidad.Enabled = true;
                    this.txtNota.Enabled = true;
                    this.cmbAutorizadoPor.Enabled = true;
                    this.dtFecha.Enabled = true;
                    this.cmbCombustible.Enabled = true;                    
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
                    this.btnAdiciona.Enabled = false;
                    //this.btnUpdate.Enabled = false;
                    //
                    this.txtCodigo.Enabled = true;
                    this.txtBeneficiario.Enabled = false;
                    this.cmbRenglonBeneficiario.Enabled = false;
                    this.txtCantidad.Enabled = false;
                    this.txtNota.Enabled = false;
                    this.cmbAutorizadoPor.Enabled = false;
                    this.dtFecha.Enabled = false;
                    this.cmbCombustible.Enabled = false;
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

        private void updateExistencia()
        {
            // PASO 1 - Busco la cantidad actual
            try
            {
                // Step 1 - Conexion
                MySqlConnection MyConexion = new MySqlConnection(clsConexion.ConectionString);

                // Step 2 
                MySqlCommand MyCommand = MyConexion.CreateCommand();

                // Step 3
                MyCommand.CommandText = "SELECT cantidad FROM existencia WHERE tipocombustible = " + this.idComb + "";

                // Step 4
                MyConexion.Open();

                // Step 5
                Int32 MyCant = Convert.ToInt32(MyCommand.ExecuteScalar());

                // Step 6
                this.cantComb = MyCant - this.cantComb;

                // Step 7
                MyConexion.Close();
            }
            catch (Exception myEx)
            {
                MessageBox.Show(myEx.Message);
                throw;
            }

            // PASO 2 - Actualizo
            try
            {
                // Step 1 - Stablishing the connection
                MySqlConnection MyConexion = new MySqlConnection(clsConexion.ConectionString);

                // Step 2 - Crear el comando de ejecucion
                MySqlCommand myCommand = MyConexion.CreateCommand();

                // Step 3 - Comando a ejecutar
                myCommand.CommandText = "UPDATE existencia SET cantidad = @cantidad WHERE tipocombustible = @tipocombustible";
                myCommand.Parameters.AddWithValue("@tipocombustible", idComb);
                myCommand.Parameters.AddWithValue("@cantidad", cantComb);

                // Step 4 - Opening the connection
                MyConexion.Open();

                // Step 5 - Executing the query
                myCommand.ExecuteNonQuery();

                // Step 6 - Closing the connection
                MyConexion.Close();

                // MessageBox.Show("Informacion EXISTENCIA actualizada satisfactoriamente...");
            }
            catch (Exception myEx)
            {
                MessageBox.Show(myEx.Message);
                throw;
            }
        }

        private void btnGrabar_Click(object sender, EventArgs e)
        {
            if (txtCodigo.Text == "")
            {
                MessageBox.Show("No se puede grabar sin un numero de solicitud ...");
                txtCodigo.Focus();
            }
            else if (dgview.Rows.Count < 1)
            {
                MessageBox.Show("No se puede grabar un registro sin productos agregados...");
                cmbCombustible.Focus();
            }
            else if (txtNota.Text == "")
            {
                MessageBox.Show("Debe de agregar una nota a esta solicitud...");
                txtNota.Focus();
            }
            else if (txtBeneficiario.Text == "")
            {
                MessageBox.Show("Debe de agregar un beneficiario...");
                this.txtBeneficiario.Focus();
            }
            else
            {                
                // Si el modo esta en "nuevo"
                if (cModo == "Nuevo")
                {
                    // verifico el codigo nuevamente
                    this.ProximoCodigo();                   

                    // PASO 1 - Agrego la data a la tabla combustible_salida
                    try
                    {
                        // Step 1 - Stablishing the connection
                        MySqlConnection MyConexion = new MySqlConnection(clsConexion.ConectionString);

                        // Step 2 - Crear el comando de ejecucion
                        MySqlCommand myCommand = MyConexion.CreateCommand();

                        // Step 3 - Comando a ejecutar                        
                        myCommand.CommandText = "INSERT INTO combustible_salida(fecha, nota, beneficiario, beneficiario_depto, "+
                        "autorizadopor) values(@fecha, @nota, @beneficiario, @beneficiario_depto, @autorizadopor)";
                        myCommand.Parameters.AddWithValue("@fecha", dtFecha.Value.ToString("yyyy-MM-dd"));
                        myCommand.Parameters.AddWithValue("@nota", txtNota.Text);
                        myCommand.Parameters.AddWithValue("@beneficiario", txtBeneficiario.Text);
                        myCommand.Parameters.AddWithValue("@beneficiario_depto", cmbRenglonBeneficiario.SelectedValue);
                        myCommand.Parameters.AddWithValue("@autorizadopor", cmbAutorizadoPor.SelectedValue);


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

                    // PASO 2 - Agrego la data a la tabla Movimiento Combustible
                    try
                    {
                        foreach (DataGridViewRow row in dgview.Rows)
                        {
                            MySqlConnection myConexion = new MySqlConnection(clsConexion.ConectionString);

                            {
                                using (MySqlCommand myCommand = new MySqlCommand("INSERT INTO movimientocombustible(id, fecha, " +
                                    "tipo_combustible, descripcion_combustible, cantidad, tipo_movimiento, operaciones, embarcacion) " +
                                    "VALUES(@id, @fecha, @tipo_combustible, @descripcion_combustible, @cantidad, @tipo_movimiento, @operaciones, @embarcacion)", myConexion))
                                {
                                    myCommand.Parameters.AddWithValue("@id", txtCodigo.Text);
                                    myCommand.Parameters.AddWithValue("@fecha", dtFecha.Value.ToString("yyyy-MM-dd"));
                                    myCommand.Parameters.AddWithValue("@tipo_combustible", row.Cells["Id"].Value);
                                    myCommand.Parameters.AddWithValue("@descripcion_combustible", row.Cells["Combustible"].Value);
                                    myCommand.Parameters.AddWithValue("@cantidad", row.Cells["Cantidad"].Value);
                                    myCommand.Parameters.AddWithValue("@tipo_movimiento", "S");
                                    if (rbTerrestres.Checked == true)
                                    {
                                        myCommand.Parameters.AddWithValue("@operaciones", "T");
                                        myCommand.Parameters.AddWithValue("@embarcacion", 0);
                                    }
                                    else
                                    {
                                        myCommand.Parameters.AddWithValue("@operaciones", "M");
                                        myCommand.Parameters.AddWithValue("@embarcacion", cmbEmbarcacion.SelectedValue);
                                    }

                                    // Abro Conexion
                                    myConexion.Open();
                                    // Ejecuto Valores
                                    myCommand.ExecuteNonQuery();
                                    // Actualizo inventario
                                    this.idComb = Convert.ToInt32(row.Cells["Id"].Value);
                                    this.cantComb = Convert.ToInt32(row.Cells["Cantidad"].Value);
                                    this.updateExistencia();
                                    // Cierro Conexion
                                    myConexion.Close();

                                    //this.idComb = 0;
                                    //this.cantComb = 0;
                                }
                            }
                        }
                        //MessageBox.Show("Records inserted.");
                    }
                    catch (Exception myEx)
                    {
                        MessageBox.Show(myEx.Message);
                        throw;
                    }

                    // LIMPIO LOS CAMPOS Y CAMBIO EL MODO LUEGO DE HABER REGISTRADO O ACTUALIZADO EL RECORD
                    this.cModo = "Inicio";
                    this.Botones();
                    this.Limpiar();

                }
                else
                {
                    // Actualizo la data a la tabla salida de combustible
                    try
                    {
                        // Step 1 - Stablishing the connection
                        MySqlConnection MyConexion = new MySqlConnection(clsConexion.ConectionString);

                        // Step 2 - Crear el comando de ejecucion
                        MySqlCommand myCommand = MyConexion.CreateCommand();

                        // Step 3 - Comando a ejecutar                        
                        myCommand.CommandText = "UPDATE combustible_salida SET fecha = @fecha, nota = @nota, "+
                            "beneficiario = @beneficiario, beneficiario_depto = @beneficiario_depto, autorizadopor = @autorizadopor "+
                            " WHERE id = "+ txtCodigo.Text +"";
                        myCommand.Parameters.AddWithValue("@fecha", dtFecha.Value.ToString("yyyy-MM-dd"));
                        myCommand.Parameters.AddWithValue("@nota", txtNota.Text);
                        myCommand.Parameters.AddWithValue("@beneficiario", txtBeneficiario.Text);
                        myCommand.Parameters.AddWithValue("@beneficiario_depto", cmbRenglonBeneficiario.SelectedValue);
                        myCommand.Parameters.AddWithValue("@autorizadopor", cmbAutorizadoPor.SelectedValue);

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

                    // Actualizo la data a la tabla movimientocombustible
                    try
                    {
                        // Step 1 - Stablishing the connection
                        MySqlConnection MyConexion = new MySqlConnection(clsConexion.ConectionString);

                        // Step 2 - Crear el comando de ejecucion
                        MySqlCommand myCommand = MyConexion.CreateCommand();

                        // Step 3 - Comando a ejecutar                        
                        myCommand.CommandText = "UPDATE movimientocombustible SET fecha = @fecha, operaciones = @operaciones, " +
                            " embarcacion = @embarcacion WHERE id = " + txtCodigo.Text + "";
                        myCommand.Parameters.AddWithValue("@fecha", dtFecha.Value.ToString("yyyy-MM-dd"));
                        if (rbTerrestres.Checked == true)
                        {
                            myCommand.Parameters.AddWithValue("@operaciones", "T");
                            myCommand.Parameters.AddWithValue("@embarcacion", 0);
                        }
                        else
                        {
                            myCommand.Parameters.AddWithValue("@operaciones", "M");
                            myCommand.Parameters.AddWithValue("@embarcacion", cmbEmbarcacion.SelectedValue);
                        }                        

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


            }

            // Pregunto si deseo imprimir
            //this.ImprimeSolicitud();
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
                MessageBox.Show("Debe de introducir el numero de referencia de entrada...");
                this.txtCodigo.Focus();
            }
            else
            {
                // BUSCANDO EN LA TABLA COMBUSTIBLE_SALIDA
                try
                {
                    // Step 1 - Conexion
                    MySqlConnection MyConexion = new MySqlConnection(clsConexion.ConectionString);

                    // Step 2 - creating the command object
                    MySqlCommand MyCommand = MyConexion.CreateCommand();

                    // Step 3 - creating the commandtext
                    MyCommand.CommandText = "SELECT id, fecha, nota, beneficiario, beneficiario_depto, autorizadopor "+
                        "FROM combustible_salida WHERE id = " + txtCodigo.Text + "";

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
                            dtFecha.Value = Convert.ToDateTime(MyReader["fecha"]);
                            txtNota.Text = MyReader["nota"].ToString();
                            txtBeneficiario.Text = MyReader["beneficiario"].ToString();
                            cmbRenglonBeneficiario.SelectedValue = MyReader["beneficiario_depto"].ToString();
                            cmbAutorizadoPor.SelectedValue = MyReader["autorizadopor"].ToString();
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


                // BUSCANDO EN LA TABLA COMBUSTIBLE_SALIDA
                try
                {
                    // Step 1 - Conexion
                    MySqlConnection MyConexion = new MySqlConnection(clsConexion.ConectionString);

                    // Step 2 - creating the command object
                    MySqlCommand MyCommand = MyConexion.CreateCommand();

                    // Step 3 - creating the commandtext
                    MyCommand.CommandText = "SELECT operaciones, embarcacion FROM combustible_salida WHERE id = " + txtCodigo.Text + "";

                    // Step 4 - connection open
                    MyConexion.Open();

                    // Step 5 - Creating the DataReader                    
                    MySqlDataReader MyReader = MyCommand.ExecuteReader();

                    // Step 6 - Verifying if Reader has rows
                    if (MyReader.HasRows)
                    {
                        //string operaciones = MyReader["operaciones"].ToString();

                        while (MyReader.Read())
                        {
                            if (MyReader["operaciones"].ToString() == "M")
                            {
                                cmbEmbarcacion.Enabled = true;                                
                                cmbEmbarcacion.SelectedValue = MyReader["operaciones"].ToString();
                                rbMaritimas.Checked = true;
                            }
                            else
                            {
                                rbTerrestres.Checked = true;
                            }
                            
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

                // BUSCANDO LA INFORMACION EN LA TABLA: MOVIMIENTOCOMBUSTIBLE. PARA LUEGO LLENAR EL GRID
                if (txtCodigo.Text != "")
                {
                    try
                    {
                        // Establishing the MySQL Connection
                        MySqlConnection conn = new MySqlConnection(clsConexion.ConectionString);

                        // Open the connection to db
                        conn.Open();

                        // Creating the DataReader
                        MySqlDataAdapter myAdapter = new MySqlDataAdapter("SELECT tipo_combustible as Id, descripcion_combustible as Combustible," +
                            " cantidad as Cantidad FROM movimientocombustible WHERE id = " + txtCodigo.Text + " AND tipo_movimiento = 'S'", conn);

                        // Creating the Dataset
                        DataSet myDs = new System.Data.DataSet();

                        // Filling the data adapter
                        myAdapter.Fill(myDs, "Solicitud");

                        // Fill the Gridview
                        dgview.DataSource = myDs.Tables[0];

                        //this.cModo = "Buscar";
                        //this.Botones();

                    }
                    catch (Exception myEx)
                    {
                        MessageBox.Show(myEx.Message);
                        throw;
                    }
                }

            }
        }

        private void btnImprimir_Click(object sender, EventArgs e)
        {

        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            if (txtCodigo.Text != "" || txtNota.Text != "")
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

        private void btnAdiciona_Click(object sender, EventArgs e)
        {
            if (txtCantidad.Text == "")
            {
                MessageBox.Show("No se puede agregar informacion sin cantidad");
                txtCantidad.Focus();
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
                    MyCommand.CommandText = "SELECT tipocombustible, cantidad FROM existencia WHERE tipocombustible = " + Convert.ToString(cmbCombustible.SelectedValue) + "";

                    // Step 4 - connection open
                    MyConexion.Open();

                    // Step 5 - Creating the DataReader                    
                    MySqlDataReader MyReader = MyCommand.ExecuteReader();

                    // Step 6 - Verifying if Reader has rows
                    if (MyReader.HasRows)
                    {
                        if (MyReader.Read())
                        {
                            // Si encuentra registros, verifico que la cantidad en existencia sea mayor que 0.
                            Int32 Cant = Convert.ToInt32(MyReader["cantidad"]);
                            if (Cant > 0)
                            {
                                if (Cant > Convert.ToInt32(txtCantidad.Text))
                                {
                                    // Agrego la informacion al Grid
                                    dt.Rows.Add(cmbCombustible.SelectedValue, lblDescripcionCombustible.Text, Convert.ToInt32(txtCantidad.Text));
                                    dgview.DataSource = dt;
                                    this.LimpiaCampo();
                                }
                                else
                                {
                                    MessageBox.Show("La cantidad en existencia es menor a la que se quiere despachar...");
                                    this.txtCantidad.Focus();
                                }
                            }
                            else
                            {
                                MessageBox.Show("Este combustible no tiene existencia...");
                                this.LimpiaCampo();
                            }
                        }                        
                    }
                    else
                    {
                        MessageBox.Show("No se encontraron registros de este tipo de combustible...");
                        this.LimpiaCampo();
                    }

                    // Step 6 - Closing all
                    MyReader.Close();
                    MyCommand.Dispose();
                    MyConexion.Close();

                }
                catch (Exception MyEx)
                {
                    MessageBox.Show(MyEx.Message);
                }
                
            }
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            try
            {
                dt.Rows.RemoveAt(dgview.CurrentCell.RowIndex);
                dgview.DataSource = dt;
            }
            catch (Exception myEx)
            {
                MessageBox.Show(myEx.Message);
                throw;
            }
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            DataGridViewRow row = dgview.Rows[i];
            row.Cells[0].Value = cmbCombustible.SelectedValue;
            row.Cells[1].Value = lblDescripcionCombustible.Text;
            row.Cells[2].Value = txtCantidad.Text;
        }

        private void txtCodigo_KeyPress(object sender, KeyPressEventArgs e)
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

        private void cmbCombustible_Leave(object sender, EventArgs e)
        {
            this.updatelbl();
        }

        private void rbMaritimas_CheckedChanged(object sender, EventArgs e)
        {
            if (this.rbMaritimas.Checked == true)
            {
                this.cmbEmbarcacion.Enabled = true;
            }
            else
            {
                this.cmbEmbarcacion.Enabled = false;
            }
        }

    }
}
