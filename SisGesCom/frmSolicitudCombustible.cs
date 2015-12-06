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
    public partial class frmSolicitudCombustible : frmBase
    {

        string cModo = "Inicio";
        bool status;

        public frmSolicitudCombustible()
        {
            InitializeComponent();
        }

        private void frmSolicitudCombustible_Load(object sender, EventArgs e)
        {
            this.Limpiar();
            this.BuscarInformaciones();
            this.fillCmbTipoCombustible();
            this.cModo = "Inicio";
            this.Botones();
        }

        private void fillCmbTipoCombustible()
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
                    this.btnEliminar.Enabled = false;
                    this.btnImprimir.Enabled = false;
                    this.btnCancelar.Enabled = false;
                    this.btnSalir.Enabled = true;
                    //
                    this.dtFechaSolicitud.Enabled = false;
                    this.txtSolicitud.Enabled = true;
                    this.cmbTipoCombustible.Enabled = false;
                    this.txtCantidad.Enabled = false;
                    this.txtNota.Enabled = false;
                    //
                    this.txtSolicitud.Focus();
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
                    this.dtFechaSolicitud.Enabled = true;
                    this.txtSolicitud.Enabled = false;
                    this.cmbTipoCombustible.Enabled = true;
                    this.txtCantidad.Enabled = true;
                    this.txtNota.Enabled = true;
                    //
                    this.cmbTipoCombustible.Focus();
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
                    this.dtFechaSolicitud.Enabled = false;
                    this.txtSolicitud.Enabled = true;
                    this.cmbTipoCombustible.Enabled = false;
                    this.txtCantidad.Enabled = false;
                    this.txtNota.Enabled = false;
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
                    this.dtFechaSolicitud.Enabled = true;
                    this.txtSolicitud.Enabled = false;
                    this.cmbTipoCombustible.Enabled = true;
                    this.txtCantidad.Enabled = true;
                    this.txtNota.Enabled = true;
                    //
                    this.cmbTipoCombustible.Focus();
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
                    //
                    this.dtFechaSolicitud.Enabled = false;
                    this.txtSolicitud.Enabled = true;
                    this.cmbTipoCombustible.Enabled = false;
                    this.txtCantidad.Enabled = false;
                    this.txtNota.Enabled = false;
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
                    this.dtFechaSolicitud.Enabled = false;
                    this.txtSolicitud.Enabled = true;
                    this.cmbTipoCombustible.Enabled = false;
                    this.txtCantidad.Enabled = false;
                    this.txtNota.Enabled = false;
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
                MyCommand.CommandText = "SELECT cdtegral, enccomb FROM info WHERE id = 1";

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
                        lblCdteGral.Text = MyReader["cdtegral"].ToString();
                        lblEncComb.Text = MyReader["enccomb"].ToString();

                        //MessageBox.Show(lblCdteGral.Text);
                        //MessageBox.Show(lblEncComb.Text);
                    }
                }

            }
            catch (Exception myEx)
            {
                MessageBox.Show(myEx.Message);
                throw;
            }

        }

        private void Limpiar()
        {
            this.dtFechaSolicitud.Refresh();
            this.txtSolicitud.Clear();
            this.cmbTipoCombustible.Refresh();
            this.txtCantidad.Clear();
            this.txtNota.Clear();
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
                MyCommand.CommandText = "SELECT count(*) FROM solicitud";

                // Step 4 - Open connection
                MyConexion.Open();

                // Step 5 - Execute the SQL Statement y Asigno el valor resultante a la variable "codigo"
                int codigo;
                codigo = Convert.ToInt32(MyCommand.ExecuteScalar());
                codigo = codigo + 1;
                txtSolicitud.Text = Convert.ToString(codigo);

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
            cModo = "Nuevo";
            this.Limpiar();
            this.Botones();
            this.ProximoCodigo();
        }

        private void btnGrabar_Click(object sender, EventArgs e)
        {
            if (txtSolicitud.Text == "")
            {
                MessageBox.Show("No se permite guardar un registro sin numero solicitud...");
                txtSolicitud.Focus();
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
                        myCommand.CommandText = "INSERT INTO solicitud(fecha, tipo_combustible, cantidad, nota, cdtegral, enccomb, status) " +
                            "values(@fecha, @tipocombustible, @cantidad, @nota, @cdtegral, @enccomb, @status)";
                        myCommand.Parameters.AddWithValue("@fecha", dtFechaSolicitud.Value.ToString("yyyy-MM-dd HH:mm:ss"));
                        myCommand.Parameters.AddWithValue("@tipocombustible", cmbTipoCombustible.SelectedValue);
                        myCommand.Parameters.AddWithValue("@cantidad", txtCantidad.Text);
                        myCommand.Parameters.AddWithValue("@nota", txtNota.Text);
                        myCommand.Parameters.AddWithValue("@cdtegral", lblCdteGral.Text);
                        myCommand.Parameters.AddWithValue("@enccomb", lblEncComb.Text);
                        myCommand.Parameters.AddWithValue("@status", 1);

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
                        myCommand.CommandText = "UPDATE solicitud SET fecha = @fecha, tipo_combustible = @tipocombustible, " +
                            "cantidad = @cantidad, nota = @nota " +
                            "WHERE id = " + txtSolicitud.Text + "";
                        myCommand.Parameters.AddWithValue("@fecha", dtFechaSolicitud.Value.ToString("yyyy-MM-dd HH:mm:ss"));
                        myCommand.Parameters.AddWithValue("@tipocombustible", cmbTipoCombustible.SelectedValue);
                        myCommand.Parameters.AddWithValue("@cantidad", txtCantidad.Text);
                        myCommand.Parameters.AddWithValue("@nota", txtNota.Text);
                        //myCommand.Parameters.AddWithValue("@cdtegral", lblCdteGral.Text);
                        //myCommand.Parameters.AddWithValue("@enccomb", lblEncComb.Text);
                        //myCommand.Parameters.AddWithValue("@status", 1);

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


                imprimeSolicitud();

                this.cModo = "Inicio";
                this.Botones();
                this.Limpiar();
            }
        }

        private void imprimeSolicitud()
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
            else if (txtNota.Text == "")
            {
                MessageBox.Show("No se permite generar una solicitud sin su debida nota...");
                txtNota.Focus();
            }
            else if (txtCantidad.Text == "")
            {
                MessageBox.Show("No se permite generar una solicitud sin su debida cantidad...");
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
                    sbQuery.Append(" solicitud.cantidad, solicitud.nota");
                    //sbQuery.Append(" licenciasmedicas.razonlicencia, dependencias.nomdepart, seccionaval.nomsec,");
                    //sbQuery.Append(" concat(rtrim(doctores.doctores_nombre),' ', ltrim(doctores.doctores_apellido)) as nombredoctor,");
                    //sbQuery.Append(" rangos.rangoabrev as rangodoctor, especialidades.especialidades_descripcion as doctorespecialidad,");
                    //sbQuery.Append(" licenciasmedicas.idlicencia ");
                    sbQuery.Append(" FROM solicitud");
                    sbQuery.Append(" INNER JOIN tipo_combustible ON tipo_combustible.id = solicitud.tipo_combustible");
                    //sbQuery.Append(" INNER JOIN dependencias ON dependencias.coddepart = licenciasmedicas.dependencia");
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


        private void verificaEstatus()
        {
            try
            {
                // Step 1 - Conexion
                MySqlConnection MyConexion = new MySqlConnection(clsConexion.ConectionString);

                // Step 2 - creating the command object
                MySqlCommand myCommand = MyConexion.CreateCommand();

                // Step 3 - Comando a ejecutar                        
                myCommand.CommandText = "SELECT status FROM solicitud WHERE id = " + txtSolicitud.Text + "";

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
                        status = Convert.ToBoolean(MyReader["status"]);
                    }
                }
                else
                {
                    status = false;
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

        private void btnEditar_Click(object sender, EventArgs e)
        {
            // Funcion que verifica el estatus de una solicitud
            verificaEstatus();

            if (status == true)
            {
                this.cModo = "Editar";
                this.Botones();
            }
            else
            {
                MessageBox.Show("No se puede modificar esta solicitud, ya ha sido procesada...");
                this.cModo = "Inicio";
                this.Botones();
            }
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            if (txtSolicitud.Text == "")
            {
                MessageBox.Show("Debe de indicar un numero de pedido de combustible...");
                txtSolicitud.Focus();
            }
            else
            {
                // Step 1 - Conexion
                MySqlConnection MyConexion = new MySqlConnection(clsConexion.ConectionString);

                // Step 2 - creating the command object
                MySqlCommand MyCommand = MyConexion.CreateCommand();

                // Step 3 - creating the commandtext
                MyCommand.CommandText = "SELECT id, fecha, tipo_combustible, cantidad, nota, enccomb, cdtegral " +
                    "FROM solicitud WHERE id = " + txtSolicitud.Text + "";

                // Step 4 - connection open
                MyConexion.Open();

                // Step 5 - Creating the DataReader                    
                MySqlDataReader MyReader = MyCommand.ExecuteReader();

                // Step 6 - Verifying if Reader has rows
                if (MyReader.HasRows)
                {
                    while (MyReader.Read())
                    {
                        dtFechaSolicitud.Value = Convert.ToDateTime(MyReader["fecha"].ToString());
                        cmbTipoCombustible.SelectedValue = MyReader["tipo_combustible"].ToString();
                        txtCantidad.Text = MyReader["cantidad"].ToString();
                        txtNota.Text = MyReader["nota"].ToString();
                        lblCdteGral.Text = MyReader["cdtegral"].ToString();
                        lblEncComb.Text = MyReader["enccomb"].ToString();                        
                    }

                    this.cModo = "Buscar";
                    this.Botones();
                }
                else
                {
                    MessageBox.Show("No se encontraron registros...");
                    this.Limpiar();
                    this.cModo = "Inicio";
                    this.Botones();                    
                    //this.txtYear.Focus();
                }

                // Step 6 - Closing all
                MyReader.Close();
                MyCommand.Dispose();
                MyConexion.Close();
            }
        }

        private void btnImprimir_Click(object sender, EventArgs e)
        {
            GenerarReporte();
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {

        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            if (txtSolicitud.Text != "")
            {
                DialogResult Result =
                MessageBox.Show("Se perderan Los cambios Realizados" + System.Environment.NewLine + "Desea Guardar los Cambios", "Sistema Gestion de Combustibles", MessageBoxButtons.YesNo,
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
            if (!(char.IsNumber(e.KeyChar)) && (e.KeyChar != (char)Keys.Back))
            {
                MessageBox.Show("Solo se permiten numeros...", "Sistema de Gestion de Combustibles", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                e.Handled = true;
                return;
            }
        }

        private void txtSolicitud_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!(char.IsNumber(e.KeyChar)) && (e.KeyChar != (char)Keys.Back))
            {
                MessageBox.Show("Solo se permiten numeros...", "Sistema de Gestion de Combustibles", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                e.Handled = true;
                return;
            }
        }        


    }
}
