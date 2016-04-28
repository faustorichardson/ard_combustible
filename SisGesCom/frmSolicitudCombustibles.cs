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
    public partial class frmSolicitudCombustibles : frmBase
    {

        int i;
        DataTable dt = new DataTable();
        string cModo = "Inicio";

        public frmSolicitudCombustibles()
        {
            InitializeComponent();
        }

        private void frmSolicitudCombustibles_Load(object sender, EventArgs e)
        {
            // Creando el Datatable
            this.dtGenerating();                       

            // Llenando el combo de tipo de combustibles
            this.fillCmbComb();

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

        private void dtGenerating()
        {
            this.dt.Columns.Add("Id", typeof(int));
            this.dt.Columns.Add("Combustible", typeof(string));
            this.dt.Columns.Add("Cantidad", typeof(Int32));
        }

        private void LimpiaCampo()
        {
            this.txtCantidad.Clear();
            this.cmbCombustible.Focus();
        }

        private void Limpiar()
        {
            this.txtSolicitud.Clear();
            this.txtNota.Clear();
            this.txtCantidad.Clear();
            //this.dgview.Rows.Clear();
            //this.dgview.Refresh();
            this.dt.Clear();
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
                MyCommand.CommandText = "SELECT count(*) FROM secuencia_solicitudcombustible";

                // Step 4 - Open connection
                MyConexion.Open();

                // Step 5 - Execute the SQL Statement y Asigno el valor resultante a la variable "codigo"
                int codigo;
                codigo = Convert.ToInt32(MyCommand.ExecuteScalar());
                codigo = codigo + 1;
                txtSolicitud.Text = Convert.ToString(codigo);
                cmbCombustible.Focus();

                // Step 5 - Close the connection
                MyConexion.Close();
            }
            catch (MySqlException MyEx)
            {
                MessageBox.Show(MyEx.Message);
            }

        }

        private void fillCmbComb()
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
                    this.txtSolicitud.Enabled = true;
                    this.dtFecha.Enabled = false;
                    this.txtCantidad.Enabled = false;
                    this.txtNota.Enabled = false;
                    this.dgview.Enabled = false;
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
                    this.btnUpdate.Enabled = true;
                    //
                    this.txtSolicitud.Enabled = false;
                    this.dtFecha.Enabled = true;
                    this.txtCantidad.Enabled = true;
                    this.txtNota.Enabled = true;
                    this.dgview.Enabled = true;
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
                    this.btnUpdate.Enabled = false;
                    //
                    this.txtSolicitud.Enabled = true;
                    this.dtFecha.Enabled = false;
                    this.txtCantidad.Enabled = false;
                    this.txtNota.Enabled = false;
                    this.dgview.Enabled = false;
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
                    this.btnUpdate.Enabled = true;
                    //
                    this.txtSolicitud.Enabled = false;
                    this.dtFecha.Enabled = true;
                    this.txtCantidad.Enabled = true;
                    this.txtNota.Enabled = true;
                    this.dgview.Enabled = true;
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
                    this.btnUpdate.Enabled = false;
                    //
                    this.txtSolicitud.Enabled = true;
                    this.dtFecha.Enabled = false;
                    this.txtCantidad.Enabled = false;
                    this.txtNota.Enabled = false;
                    this.dgview.Enabled = false;
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

        private void cmbCombustible_Leave(object sender, EventArgs e)
        {
            updatelbl();
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


        private void btnGrabar_Click(object sender, EventArgs e)
        {

            if (txtSolicitud.Text == "")
            {
                MessageBox.Show("No se puede grabar sin un numero de solicitud ...");
                txtSolicitud.Focus();                
            } else if(dgview.Rows.Count < 1)
            {
                MessageBox.Show("No se puede grabar un registro sin productos agregados...");
                cmbCombustible.Focus();                
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
                    // verifico el codigo nuevamente
                    this.ProximoCodigo();

                    // Agrego la data a la tabla secuencia_solicitudcombustible
                    try
                    {
                        // Step 1 - Stablishing the connection
                        MySqlConnection MyConexion = new MySqlConnection(clsConexion.ConectionString);

                        // Step 2 - Crear el comando de ejecucion
                        MySqlCommand myCommand = MyConexion.CreateCommand();

                        // Step 3 - Comando a ejecutar                        
                        myCommand.CommandText = "INSERT INTO secuencia_solicitudcombustible(fecha, nota) values(@fecha, @nota)";
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

                    // Agrego la data a la tabla Solicitud
                    try
                    {
                        foreach (DataGridViewRow row in dgview.Rows)
                        {
                            MySqlConnection myConexion = new MySqlConnection(clsConexion.ConectionString);

                            {
                                using (MySqlCommand myCommand = new MySqlCommand("INSERT INTO solicitud(id, fecha, tipo_combustible, descripcion_combustible, cantidad)" +
                                    "VALUES(@id, @fecha, @tipo_combustible, @descripcion_combustible, @cantidad)", myConexion))
                                {
                                    myCommand.Parameters.AddWithValue("@id", txtSolicitud.Text);
                                    myCommand.Parameters.AddWithValue("@fecha", dtFecha.Value.ToString("yyyy-MM-dd"));
                                    myCommand.Parameters.AddWithValue("@tipo_combustible", row.Cells["Id"].Value);
                                    myCommand.Parameters.AddWithValue("@descripcion_combustible", row.Cells["Combustible"].Value);
                                    myCommand.Parameters.AddWithValue("@cantidad", row.Cells["Cantidad"].Value);
                                    // Abro Conexion
                                    myConexion.Open();
                                    // Ejecuto Valores
                                    myCommand.ExecuteNonQuery();
                                    // Cierro Conexion
                                    myConexion.Close();

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

                }
                else
                {
                    // Actualizo la data a la tabla secuencia_solicitudcombustible
                    try
                    {
                        // Step 1 - Stablishing the connection
                        MySqlConnection MyConexion = new MySqlConnection(clsConexion.ConectionString);

                        // Step 2 - Crear el comando de ejecucion
                        MySqlCommand myCommand = MyConexion.CreateCommand();

                        // Step 3 - Comando a ejecutar                        
                        myCommand.CommandText = "UPDATE secuencia_solicitudcombustible SET fecha = @fecha, nota = @nota" +
                            " WHERE id = " + txtSolicitud.Text + "";
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

                    

                }

                // Pregunto si deseo imprimir
                this.ImprimeSolicitud();

                // LIMPIO LOS CAMPOS Y CAMBIO EL MODO LUEGO DE HABER REGISTRADO O ACTUALIZADO EL RECORD
                this.cModo = "Inicio";
                this.Botones();
                this.Limpiar();
            }

            
        }

        private void ImprimeSolicitud()
        {
            DialogResult Result =
            MessageBox.Show("Imprima la Solicitud de Combustible" + System.Environment.NewLine + "Desea Imprimir la Solicitud de Combustible", "Sistema de Gestion de Combustibles", MessageBoxButtons.YesNo,
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
            if (txtSolicitud.Text == "")
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
                int cCodigo = Convert.ToInt32(txtSolicitud.Text);

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
                    cWhere = cWhere + " AND solicitud.id =" + cCodigo + "";
                    sbQuery.Clear();
                    sbQuery.Append("SELECT solicitud.id, solicitud.fecha, tipo_combustible.combustible as tipocombustible,");
                    sbQuery.Append(" solicitud.cantidad, secuencia_solicitudcombustible.nota as nota, tipo_combustible.medida as medida");
                    //sbQuery.Append(" licenciasmedicas.razonlicencia, dependencias.nomdepart, seccionaval.nomsec,");
                    //sbQuery.Append(" concat(rtrim(doctores.doctores_nombre),' ', ltrim(doctores.doctores_apellido)) as nombredoctor,");
                    //sbQuery.Append(" rangos.rangoabrev as rangodoctor, especialidades.especialidades_descripcion as doctorespecialidad,");
                    //sbQuery.Append(" licenciasmedicas.idlicencia ");
                    sbQuery.Append(" FROM solicitud");
                    sbQuery.Append(" INNER JOIN tipo_combustible ON tipo_combustible.id = solicitud.tipo_combustible");
                    sbQuery.Append(" INNER JOIN secuencia_solicitudcombustible ON secuencia_solicitudcombustible.id = solicitud.id");
                    //sbQuery.Append(" INNER JOIN seccionaval ON seccionaval.codsec = licenciasmedicas.seccionaval");
                    //sbQuery.Append(" INNER JOIN doctores ON doctores.doctores_cedula = licenciasmedicas.ceduladoctor");
                    //sbQuery.Append(" INNER JOIN rangos ON rangos.rango_id = doctores.doctores_rango");
                    //sbQuery.Append(" INNER JOIN especialidades ON especialidades.especialidades_id = doctores.doctores_especialidad");
                    sbQuery.Append(cWhere);

                    // Paso los valores de sbQuery al CommandText
                    myCommand.CommandText = sbQuery.ToString();
                    // Creo el objeto Data Adapter y ejecuto el command en el
                    myAdapter = new MySqlDataAdapter(myCommand);
                    // Creo el objeto Data Table
                    DataTable dtSolicitudCombustible = new DataTable();
                    // Lleno el data adapter
                    myAdapter.Fill(dtSolicitudCombustible);
                    // Cierro el objeto clsConexion
                    myclsConexion.Close();

                    // Verifico cantidad de datos encontrados
                    int nRegistro = dtSolicitudCombustible.Rows.Count;
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
                        cTitulo = "PEDIDO PARA MATERIALES GASTABLES";

                        //6to Instanciamos nuestro REPORTE
                        //Reportes.ListadoDoctores oListado = new Reportes.ListadoDoctores();
                        rptSolicitudCombustible orptSolicitudCombustible = new rptSolicitudCombustible();

                        //pasamos el nombre del TITULO del Listado
                        //SumaryInfo es un objeto que se utiliza para leer,crear y actualizar las propiedades del reporte
                        // oListado.SummaryInfo.ReportTitle = cTitulo;

                        orptSolicitudCombustible.SummaryInfo.ReportTitle = cTitulo;

                        //7mo. instanciamos nuestro el FORMULARIO donde esta nuestro ReportViewer
                        frmPrinter ofrmPrinter = new frmPrinter(dtSolicitudCombustible, orptSolicitudCombustible, cTitulo);

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
            if (txtSolicitud.Text == "")
            {
                MessageBox.Show("Debe de introducir el numero de una solicitud...");
                this.txtSolicitud.Focus();
            }
            else
            {
                // BUSCANDO EN LA TABLA SECUENCIA_SOLICITUDCOMBUSIBLE
                try
                {
                    // Step 1 - Conexion
                    MySqlConnection MyConexion = new MySqlConnection(clsConexion.ConectionString);

                    // Step 2 - creating the command object
                    MySqlCommand MyCommand = MyConexion.CreateCommand();

                    // Step 3 - creating the commandtext
                    MyCommand.CommandText = "SELECT id, fecha, nota FROM secuencia_solicitudcombustible WHERE id = " + txtSolicitud.Text + "";

                    // Step 4 - connection open
                    MyConexion.Open();

                    // Step 5 - Creating the DataReader                    
                    MySqlDataReader MyReader = MyCommand.ExecuteReader();

                    // Step 6 - Verifying if Reader has rows
                    if (MyReader.HasRows)
                    {
                        while (MyReader.Read())
                        {
                            dtFecha.Value = Convert.ToDateTime(MyReader["fecha"]);
                            txtNota.Text = MyReader["nota"].ToString();
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
                        this.txtSolicitud.Focus();                        
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

                // BUSCANDO LA INFORMACION EN LA TABLA: SOLICITUD. PARA LUEGO LLENAR EL GRID
                if (txtSolicitud.Text != "")
                {
                    try
                    {
                        // Establishing the MySQL Connection
                        MySqlConnection conn = new MySqlConnection(clsConexion.ConectionString);

                        // Open the connection to db
                        conn.Open();

                        // Creating the DataReader
                        MySqlDataAdapter myAdapter = new MySqlDataAdapter("SELECT tipo_combustible as Id, descripcion_combustible as Combustible,"+
                            " cantidad as Cantidad FROM solicitud WHERE id = "+ txtSolicitud.Text +"", conn);

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
            GenerarReporte();
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            if (txtSolicitud.Text != "")
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
                // Agrego la informacion al Grid
                dt.Rows.Add(cmbCombustible.SelectedValue, lblDescripcionCombustible.Text, Convert.ToInt32(txtCantidad.Text));
                dgview.DataSource = dt;
                this.LimpiaCampo();
            }
        }

        private void btnNuevo_Click(object sender, EventArgs e)
        {
            cModo = "Nuevo";
            this.Limpiar();
            this.Botones();
            this.ProximoCodigo();            
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

        private void dgview_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            i = e.RowIndex;
            DataGridViewRow row = dgview.Rows[i];
            this.cmbCombustible.SelectedValue = row.Cells[0].Value;
            this.lblDescripcionCombustible.Text = row.Cells[1].Value.ToString();
            this.txtCantidad.Text = row.Cells[2].Value.ToString();
        }

        private void txtSolicitud_KeyPress(object sender, KeyPressEventArgs e)
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

      
    }
}
