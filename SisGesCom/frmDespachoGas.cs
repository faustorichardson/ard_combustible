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
    public partial class frmDespachoGas : frmBase
    {        
        string cModo = "Inicio";
        int i;
        DataTable dt = new DataTable();
        int idComb = 2000;
        int cantComb = 0;
        int combTemp = 0;

        public frmDespachoGas()
        {
            InitializeComponent();
        }

        private void frmDespachoGas_Load(object sender, EventArgs e)
        {
            // Creando el Datatable
            this.dtGenerating();

            // Llenando el combo de tipo de combustibles
            //this.fillCmbComb();

            // Llenando el combo del departamento beneficiario
            this.fillCmbDepartamento();

            // Llenando el combo del suplidor
            this.fillCmbSuplidor();

            // Llenando el combo del Departamento Autoriza
            // this.fillCmbAutorizado();

            // Modificando el valor del LblDescripcionCombustible
            this.updatelbl();

            // Funcion Limpiar
            this.Limpiar();

            // Funcion Limpia Combustibles
            this.LimpiaCampo();

            // Funcion Botones
            this.cModo = "Inicio";
            this.Botones();
        }

        private void fillCmbSuplidor()
        {
            try
            {
                // Step 1 
                MySqlConnection MyConexion = new MySqlConnection(clsConexion.ConectionString);

                // Step 2
                MyConexion.Open();

                // Step 3
                MySqlCommand MyCommand = new MySqlCommand("SELECT id_suplidor, suplidor FROM suplidores ORDER BY suplidor ASC", MyConexion);

                // Step 4
                MySqlDataReader MyReader;
                MyReader = MyCommand.ExecuteReader();

                // Step 5
                DataTable MyDataTable = new DataTable();
                MyDataTable.Columns.Add("id_suplidor", typeof(int));
                MyDataTable.Columns.Add("suplidor", typeof(string));
                MyDataTable.Load(MyReader);

                // Step 6
                cmbSuplidor.ValueMember = "id_suplidor";
                cmbSuplidor.DisplayMember = "suplidor";
                cmbSuplidor.DataSource = MyDataTable;

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
                dt.Columns.Add("Departamento", typeof(string));
                dt.Columns.Add("Cantidad", typeof(Int32));
            }
            catch (Exception myEx)
            {
                MessageBox.Show(myEx.Message);
                throw;
            }
        }

        //private void fillCmbComb()
        //{

        //}

        private void ProximoCodigo()
        {
            try
            {
                // Step 1 - Connection stablished
                MySqlConnection MyConexion = new MySqlConnection(clsConexion.ConectionString);

                // Step 2 - Create command
                MySqlCommand MyCommand = MyConexion.CreateCommand();

                // Step 3 - Set the commanndtext property
                MyCommand.CommandText = "SELECT count(*) FROM combustible_gas";

                // Step 4 - Open connection
                MyConexion.Open();

                // Step 5 - Execute the SQL Statement y Asigno el valor resultante a la variable "codigo"
                int codigo;
                codigo = Convert.ToInt32(MyCommand.ExecuteScalar());
                codigo = codigo + 1;
                txtCodigo.Text = Convert.ToString(codigo);
                // txtSuplidor.Focus();

                // Step 5 - Close the connection
                MyConexion.Close();
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
                MySqlCommand MyCommand = new MySqlCommand("SELECT id, departamento FROM deptobeneficiariogas ORDER BY departamento ASC", MyConexion);

                // Step 4
                MySqlDataReader MyReader;
                MyReader = MyCommand.ExecuteReader();

                // Step 5
                DataTable MyDataTable = new DataTable();
                MyDataTable.Columns.Add("id", typeof(int));
                MyDataTable.Columns.Add("departamento", typeof(string));
                MyDataTable.Load(MyReader);

                // Step 6
                cmbDepartamento.ValueMember = "id";
                cmbDepartamento.DisplayMember = "departamento";
                cmbDepartamento.DataSource = MyDataTable;

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
                MyCommand.CommandText = "SELECT id, departamento FROM deptobeneficiariogas WHERE id = " + cmbDepartamento.SelectedValue + "";

                // Step 4 - connection open
                MyConexion.Open();

                // Step 5 - Creating the DataReader                    
                MySqlDataReader MyReader = MyCommand.ExecuteReader();

                // Step 6 - Verifying if Reader has rows
                if (MyReader.HasRows)
                {
                    while (MyReader.Read())
                    {
                        lblDescripcionCombustible.Text = MyReader["departamento"].ToString();
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

        private void Limpiar()
        {
            this.txtCodigo.Clear();
            // this.txtSuplidor.Clear();
            this.txtNota.Clear();
            this.txtCantidad.Clear();
            this.dt.Clear();
            this.cantComb = 0;
            this.idComb = 2000;
        }

        private void LimpiaCampo()
        {
            this.txtCantidad.Clear();
            this.cmbDepartamento.Focus();
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
                    this.btnUpdate.Enabled = false;
                    //
                    this.txtCodigo.Enabled = true;
                    this.cmbSuplidor.Enabled = false;
                    this.txtCantidad.Enabled = false;
                    this.txtNota.Enabled = false;
                    this.cmbDepartamento.Enabled = false;
                    this.dtFecha.Enabled = false;                    
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
                    this.btnUpdate.Enabled = true;
                    //
                    this.txtCodigo.Enabled = false;
                    this.cmbSuplidor.Enabled = true;
                    this.txtCantidad.Enabled = true;
                    this.txtNota.Enabled = true;
                    this.cmbDepartamento.Enabled = true;
                    this.dtFecha.Enabled = true;                    
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
                    this.btnUpdate.Enabled = false;
                    //
                    this.txtCodigo.Enabled = true;
                    this.cmbSuplidor.Enabled = false;
                    this.txtCantidad.Enabled = false;
                    this.txtNota.Enabled = false;
                    this.cmbDepartamento.Enabled = false;
                    this.dtFecha.Enabled = false;                    
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
                    this.btnUpdate.Enabled = true;
                    //
                    this.txtCodigo.Enabled = false;
                    this.cmbSuplidor.Enabled = true;                    
                    this.txtCantidad.Enabled = true;
                    this.txtNota.Enabled = true;
                    this.cmbDepartamento.Enabled = true;
                    this.dtFecha.Enabled = true;                    
                    break;

                case "Buscar":
                    this.btnNuevo.Enabled = true;
                    this.btnGrabar.Enabled = false;
                    this.btnEditar.Enabled = false;
                    this.btnBuscar.Enabled = true;
                    this.btnImprimir.Enabled = true;
                    this.btnEliminar.Enabled = false;
                    this.btnCancelar.Enabled = false;
                    this.btnSalir.Enabled = true;
                    this.btnAdiciona.Enabled = false;
                    this.btnUpdate.Enabled = false;
                    //
                    this.txtCodigo.Enabled = true;
                    this.cmbSuplidor.Enabled = false;
                    this.txtCantidad.Enabled = false;
                    this.txtNota.Enabled = false;
                    this.cmbDepartamento.Enabled = false;
                    this.dtFecha.Enabled = false;                    
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

        private void btnAdiciona_Click(object sender, EventArgs e)
        {
            if (txtCantidad.Text == "")
            {
                MessageBox.Show("No se puede agregar informacion sin cantidad");
                txtCantidad.Focus();
            }
            else
            {
                // Agrego gas despachado
                cantComb = cantComb + Convert.ToInt32(txtCantidad.Text);

                // Agrego la informacion al Grid
                dt.Rows.Add(cmbDepartamento.SelectedValue, lblDescripcionCombustible.Text, Convert.ToInt32(txtCantidad.Text));
                dgview.DataSource = dt;
                this.LimpiaCampo();
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
            row.Cells[0].Value = cmbDepartamento.SelectedValue;
            row.Cells[1].Value = lblDescripcionCombustible.Text;
            row.Cells[2].Value = txtCantidad.Text;
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
            if (txtCodigo.Text == "")
            {
                MessageBox.Show("No se puede grabar sin un numero de solicitud ...");
                txtCodigo.Focus();
            }
            else if (dgview.Rows.Count < 1)
            {
                MessageBox.Show("No se puede grabar un registro sin productos agregados...");
                cmbDepartamento.Focus();
            }
            else if (txtNota.Text == "")
            {
                MessageBox.Show("Debe de agregar una nota a esta solicitud...");
                txtNota.Focus();
            }            
            else
            {
                if (cModo == "Nuevo")
                {
                    // Verifico nuevamente el numero de codigo antes de guardar
                    this.ProximoCodigo();                    

                    // PASO 1 - Agrego la data a la tabla combustible_gas
                    try
                    {
                        // Step 1 - Stablishing the connection
                        MySqlConnection MyConexion = new MySqlConnection(clsConexion.ConectionString);

                        // Step 2 - Crear el comando de ejecucion
                        MySqlCommand myCommand = MyConexion.CreateCommand();

                        // Step 3 - Comando a ejecutar                        
                        myCommand.CommandText = "INSERT INTO combustible_gas(suplidor, fecha, nota)"+
                            "values(@suplidor, @fecha, @nota)";
                        myCommand.Parameters.AddWithValue("@suplidor", cmbSuplidor.SelectedValue);
                        myCommand.Parameters.AddWithValue("@fecha", dtFecha.Value.ToString("yyyy-MM-dd"));
                        myCommand.Parameters.AddWithValue("@nota", txtNota.Text);
                        
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

                    // PASO 2 - Agrego la data a la tabla Movimiento Gas
                    try
                    {
                        foreach (DataGridViewRow row in dgview.Rows)
                        {
                            MySqlConnection myConexion = new MySqlConnection(clsConexion.ConectionString);

                            {
                                using (MySqlCommand myCommand = new MySqlCommand("INSERT INTO movimientogas(id, departamento, " +
                                    "departamento_descripcion, cantidad, fecha) VALUES(@id, @departamento, @departamento_descripcion, @cantidad, @fecha)", myConexion))
                                {
                                    myCommand.Parameters.AddWithValue("@id", txtCodigo.Text);                                    
                                    myCommand.Parameters.AddWithValue("@departamento", row.Cells["Id"].Value);
                                    myCommand.Parameters.AddWithValue("@departamento_descripcion", row.Cells["Departamento"].Value);
                                    myCommand.Parameters.AddWithValue("@cantidad", row.Cells["Cantidad"].Value);
                                    myCommand.Parameters.AddWithValue("@fecha", dtFecha.Value.ToString("yyyy-MM-dd"));
                                    
                                    // Abro Conexion
                                    myConexion.Open();
                                    // Ejecuto Valores
                                    myCommand.ExecuteNonQuery();
                                    // Actualizo inventario
                                    //this.idComb = Convert.ToInt32(row.Cells["Id"].Value);
                                    //this.cantComb = Convert.ToInt32(row.Cells["Cantidad"].Value);
                                    //this.updateExistencia();
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

                    // PASO 3 - Actualizo inventario reduciendo el despacho
                    
                    // Busco cantidad en existencia
                    this.buscaInventario();

                    // Actualizo Inventario
                    this.actualizaInventario();

                    // Pregunto si deseo imprimir
                    this.ImprimeSolicitud();

                    // LIMPIO LOS CAMPOS Y CAMBIO EL MODO LUEGO DE HABER REGISTRADO O ACTUALIZADO EL RECORD
                    this.cModo = "Inicio";
                    this.Botones();
                    this.Limpiar();

                }
                else
                {
                    

                }
                
            }

            
        }


        private void buscaInventario()
        {
            // BUSCANDO EN LA TABLA EXISTENCIA
            try
            {
                // Step 1 - Conexion
                MySqlConnection MyConexion = new MySqlConnection(clsConexion.ConectionString);

                // Step 2 - creating the command object
                MySqlCommand MyCommand = MyConexion.CreateCommand();

                // Step 3 - creating the commandtext
                MyCommand.CommandText = "SELECT cantidad from existencia WHERE tipocombustible =" + idComb + "";

                // Step 4 - connection open
                MyConexion.Open();

                // Step 5 - Creating the DataReader                    
                MySqlDataReader MyReader = MyCommand.ExecuteReader();

                // Step 6 - Verifying if Reader has rows
                if (MyReader.HasRows)
                {
                    while (MyReader.Read())
                    {
                        combTemp = Convert.ToInt32(MyReader["cantidad"].ToString());
                    }
                }
                else
                {
                    MessageBox.Show("No hay existencia de este tipo de combustible...");
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

        private void actualizaInventario()
        {
            if (combTemp > cantComb)
            {
                try
                {
                    // actualizo existencia combustible
                    combTemp = combTemp - cantComb;

                    // Step 1 - Conexion
                    MySqlConnection MyConexion = new MySqlConnection(clsConexion.ConectionString);

                    // Step 2 - creating the command object
                    MySqlCommand MyCommand = MyConexion.CreateCommand();

                    // Step 3 - creating the commandtext
                    MyCommand.CommandText = "UPDATE existencia SET cantidad = "+ combTemp +" WHERE tipocombustible = "+ idComb +"";

                    // Step 4 - connection open
                    MyConexion.Open();

                    // Step 5 - Creating the DataReader                    
                    MyCommand.ExecuteNonQuery();

                    // cierro variables
                    MyCommand.Dispose();
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
                MessageBox.Show("La cantidad de gas a despachar es mayor que existencia...");
            }            
        }

        private void ImprimeSolicitud()
        {
            DialogResult Result =
            MessageBox.Show("Imprima la Solicitud de Combustible" + System.Environment.NewLine + "Desea Imprimir la Solicitud de Despacho de Gas", "Sistema de Gestion de Combustibles", MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question);
            switch (Result)
            {
                case DialogResult.Yes:
                    this.GenerarReporte();
                    break;
            }
        }

        private void GenerarReporte()
        {
            if (txtCodigo.Text == "")
            {
                MessageBox.Show("No se permite generar una solicitud sin su debida numeracion...");
                txtCantidad.Focus();
            }
            else
            {

                //clsConexion a la base de datos
                MySqlConnection myclsConexion = new MySqlConnection(clsConexion.ConectionString);
                // Creando el command que ejecutare
                MySqlCommand myCommand = new MySqlCommand();
                // Creando el Data Adapter
                MySqlDataAdapter myAdapter = new MySqlDataAdapter();
                // Creando el String Builder
                StringBuilder sbQuery = new StringBuilder();
                // Otras variables del entorno
                string cWhere = " WHERE 1 = 1";
                string cUsuario = frmLogin.cUsuarioActual;
                string cTitulo = "";
                int cCodigo = Convert.ToInt32(txtCodigo.Text);

                try
                {
                    // Abro clsConexion
                    myclsConexion.Open();
                    // Creo comando
                    myCommand = myclsConexion.CreateCommand();
                    // Adhiero el comando a la clsConexion
                    myCommand.Connection = myclsConexion;
                    // Filtros de la busqueda
                    //int cCodigoImprimir = Convert.ToInt32(txtIdLicencia.Text);
                    cWhere = cWhere + " AND combustible_gas.id =" + cCodigo + "";
                    sbQuery.Clear();
                    sbQuery.Append("SELECT combustible_gas.id as id_despacho, combustible_gas.fecha, suplidores.suplidor as suplidor, combustible_gas.nota,");
                    sbQuery.Append(" movimientogas.id, movimientogas.cantidad, movimientogas.departamento_descripcion as departamento,");
                    sbQuery.Append(" deptobeneficiariogas.tarjeta");                    
                    sbQuery.Append(" FROM movimientogas");
                    sbQuery.Append(" INNER JOIN combustible_gas ON combustible_gas.id = movimientogas.id");                    
                    sbQuery.Append(" INNER JOIN suplidores ON suplidores.id_suplidor = combustible_gas.suplidor");
                    sbQuery.Append(" INNER JOIN deptobeneficiariogas ON deptobeneficiariogas.id = movimientogas.departamento");
                    sbQuery.Append(cWhere);
                    //sbQuery.Append(" ORDER BY tipo_deptogas.id ASC");

                    // Paso los valores de sbQuery al CommandText
                    myCommand.CommandText = sbQuery.ToString();
                    // Creo el objeto Data Adapter y ejecuto el command en el
                    myAdapter = new MySqlDataAdapter(myCommand);
                    // Creo el objeto Data Table
                    DataTable dtGas = new DataTable();
                    // Lleno el data adapter
                    myAdapter.Fill(dtGas);
                    // Cierro el objeto clsConexion
                    myclsConexion.Close();

                    // Verifico cantidad de datos encontrados
                    int nRegistro = dtGas.Rows.Count;
                    if (nRegistro == 0)
                    {
                        MessageBox.Show("No Hay Datos Para Mostrar, Favor Verificar", "Sistema de Gestion de Combustible", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return;
                    }
                    else
                    {
                        //1ero.HACEMOS LA COLECCION DE PARAMETROS
                        //los campos de parametros contiene un objeto para cada campo de parametro en el informe
                        ParameterFields oParametrosCR = new ParameterFields();
                        //Proporciona propiedades para la recuperacion y configuracion del tipo de los parametros
                        ParameterValues oParametrosValuesCR = new ParameterValues();

                        //2do.CREAMOS LOS PARAMETROS
                        ParameterField oUsuario = new ParameterField();
                        //parametervaluetype especifica el TIPO de valor de parametro
                        //ParameterValueKind especifica el tipo de valor de parametro en la PARAMETERVALUETYPE de la Clase PARAMETERFIELD
                        oUsuario.ParameterValueType = ParameterValueKind.StringParameter;

                        //3ero.VALORES PARA LOS PARAMETROS
                        //ParameterDiscreteValue proporciona propiedades para la recuperacion y configuracion de 
                        //parametros de valores discretos
                        ParameterDiscreteValue oUsuarioDValue = new ParameterDiscreteValue();
                        oUsuarioDValue.Value = cUsuario;

                        //4to. AGREGAMOS LOS VALORES A LOS PARAMETROS
                        oUsuario.CurrentValues.Add(oUsuarioDValue);


                        //5to. AGREGAMOS LOS PARAMETROS A LA COLECCION 
                        oParametrosCR.Add(oUsuario);

                        //nombre del parametro en CR (Crystal Reports)
                        oParametrosCR[0].Name = "cUsuario";

                        //nombre del TITULO DEL INFORME
                        cTitulo = "Año de la Atención Integral a la Primera Infancia";

                        //6to Instanciamos nuestro REPORTE
                        //Reportes.ListadoDoctores oListado = new Reportes.ListadoDoctores();
                        rptSolicitudDespachoGas orptSolicitudDespachoGas = new rptSolicitudDespachoGas();

                        //pasamos el nombre del TITULO del Listado
                        //SumaryInfo es un objeto que se utiliza para leer,crear y actualizar las propiedades del reporte
                        // oListado.SummaryInfo.ReportTitle = cTitulo;
                        orptSolicitudDespachoGas.SummaryInfo.ReportTitle = cTitulo;

                        //7mo. instanciamos nuestro el FORMULARIO donde esta nuestro ReportViewer
                        frmPrinter ofrmPrinter = new frmPrinter(dtGas, orptSolicitudDespachoGas, cTitulo);

                        //ParameterFieldInfo Obtiene o establece la colección de campos de parámetros.
                        ofrmPrinter.CrystalReportViewer1.ParameterFieldInfo = oParametrosCR;
                        ofrmPrinter.ShowDialog();
                    }
                }
                catch (Exception myEx)
                {
                    MessageBox.Show("Error : " + myEx.Message, "Mostrando Reporte", MessageBoxButtons.OK,
                                        MessageBoxIcon.Information);
                    clsExceptionLog.LogError(myEx, false);
                    return;
                }
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
                MessageBox.Show("Debe de introducir el numero de referencia de entrada...");
                this.txtCodigo.Focus();
            }
            else
            {
                // BUSCANDO EN LA TABLA COMBUSTIBLE_GAS
                try
                {
                    // Step 1 - Conexion
                    MySqlConnection MyConexion = new MySqlConnection(clsConexion.ConectionString);

                    // Step 2 - creating the command object
                    MySqlCommand MyCommand = MyConexion.CreateCommand();

                    // Step 3 - creating the commandtext
                    MyCommand.CommandText = "SELECT id, suplidor, nota, fecha FROM combustible_gas WHERE id = " + txtCodigo.Text + "";

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
                            cmbSuplidor.SelectedValue = MyReader["suplidor"].ToString();                            
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

                // BUSCANDO LA INFORMACION EN LA TABLA: MOVIMIENTOGAS PARA LUEGO LLENAR EL GRID
                if (txtCodigo.Text != "")
                {
                    try
                    {
                        // Establishing the MySQL Connection
                        MySqlConnection conn = new MySqlConnection(clsConexion.ConectionString);

                        // Open the connection to db
                        conn.Open();

                        // Creating the DataReader
                        MySqlDataAdapter myAdapter = new MySqlDataAdapter("SELECT departamento as Id, departamento_descripcion as Combustible," +
                            " cantidad as Cantidad FROM movimientogas WHERE id = " + txtCodigo.Text + "", conn);

                        // Creating the Dataset
                        DataSet myDs = new System.Data.DataSet();

                        // Filling the data adapter
                        myAdapter.Fill(myDs, "Gas");

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
            this.GenerarReporte();
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

        private void txtCantidad_TextChanged(object sender, EventArgs e)
        {

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

        private void cmbDepartamento_Leave(object sender, EventArgs e)
        {
            this.updatelbl();
        }
    }    

}
