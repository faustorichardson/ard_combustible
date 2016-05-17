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
    public partial class frmTipoCombustible : frmBase
    {
        string cModo;
        string Usuario = frmLogin.cUsuarioActual;

        public frmTipoCombustible()
        {
            InitializeComponent();
        }

        private void frmTipoCombustible_Load(object sender, EventArgs e)
        {
            this.Limpiar();
            cModo = "Inicio";
            this.Botones();
            this.txtCodigo.Focus();
        }

        private void Limpiar()
        {
            this.txtCodigo.Clear();
            this.txtTipo.Clear();
            this.txtMedida.Clear();
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
                    this.btnImprimir.Enabled = true;
                    this.btnEliminar.Enabled = false;
                    this.btnCancelar.Enabled = false;
                    this.btnSalir.Enabled = true;
                    this.txtCodigo.Enabled = true;
                    this.txtTipo.Enabled = false;
                    //
                    this.txtCodigo.Enabled = true;
                    this.txtMedida.Enabled = false;
                    this.txtTipo.Enabled = false;
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
                    this.txtCodigo.Enabled = false;
                    this.txtTipo.Enabled = true;
                    //
                    this.txtCodigo.Enabled = false;
                    this.txtMedida.Enabled = true;
                    this.txtTipo.Enabled = true;
                    break;

                case "Grabar":
                    this.btnNuevo.Enabled = true;
                    this.btnGrabar.Enabled = false;
                    this.btnEditar.Enabled = false;
                    this.btnBuscar.Enabled = true;
                    this.btnImprimir.Enabled = true;
                    this.btnEliminar.Enabled = false;
                    this.btnCancelar.Enabled = false;
                    this.btnSalir.Enabled = true;
                    this.txtCodigo.Enabled = true;
                    this.txtTipo.Enabled = false;
                    //
                    this.txtCodigo.Enabled = true;
                    this.txtMedida.Enabled = false;
                    this.txtTipo.Enabled = false;
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
                    this.txtCodigo.Enabled = false;
                    this.txtTipo.Enabled = true;
                    //
                    this.txtCodigo.Enabled = false;
                    this.txtMedida.Enabled = true;
                    this.txtTipo.Enabled = true;
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
                    this.txtCodigo.Enabled = true;
                    this.txtTipo.Enabled = false;
                    //
                    this.txtCodigo.Enabled = true;
                    this.txtMedida.Enabled = false;
                    this.txtTipo.Enabled = false;
                    break;

                case "Eliminar":
                    break;

                case "Cancelar":
                    this.btnNuevo.Enabled = true;
                    this.btnGrabar.Enabled = false;
                    this.btnEditar.Enabled = false;
                    this.btnBuscar.Enabled = true;
                    this.btnImprimir.Enabled = true;
                    this.btnEliminar.Enabled = false;
                    this.btnCancelar.Enabled = false;
                    this.btnSalir.Enabled = true;
                    //
                    this.txtCodigo.Enabled = true;
                    this.txtMedida.Enabled = false;
                    this.txtTipo.Enabled = false;
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
                MyCommand.CommandText = "SELECT count(*) FROM tipo_combustible";

                // Step 4 - Open connection
                MyConexion.Open();

                // Step 5 - Execute the SQL Statement y Asigno el valor resultante a la variable "codigo"
                int codigo;
                codigo = Convert.ToInt32(MyCommand.ExecuteScalar());
                codigo = codigo + 1;
                txtCodigo.Text = Convert.ToString(codigo);
                //txtTipo.Focus();

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
            this.txtTipo.Focus();
        }

        private void btnGrabar_Click(object sender, EventArgs e)
        {            
            
            if (this.txtTipo.Text == "")
            {
                MessageBox.Show("No se permiten campos vacios...");
                this.txtTipo.Focus();
            }
            else
            {
                // REGISTRO DE INFORMACIONES EN LA TABLA TIPO_COMBUSTIBLE
                if (cModo == "Nuevo")
                {
                    try
                    {
                        // Step 1 - Stablishing the connection
                        MySqlConnection MyConexion = new MySqlConnection(clsConexion.ConectionString);

                        // Step 2 - Crear el comando de ejecucion
                        MySqlCommand myCommand = MyConexion.CreateCommand();

                        // Step 3 - Comando a ejecutar                        
                        myCommand.CommandText = "INSERT INTO tipo_combustible(combustible, medida, status) values(@combustible, @medida, @status)";
                        myCommand.Parameters.AddWithValue("@combustible", txtTipo.Text);
                        myCommand.Parameters.AddWithValue("@medida", txtMedida.Text);
                        if (chkActivo.Checked == true)
                        {
                            myCommand.Parameters.AddWithValue("@status", "A");
                        }
                        else
                        {
                            myCommand.Parameters.AddWithValue("@status", "I");
                        }

                        // Step 4 - Opening the connection
                        MyConexion.Open();

                        // Step 5 - Executing the query
                        myCommand.ExecuteNonQuery();

                        // Step 6 - Closing the connection
                        MyConexion.Close();

                        MessageBox.Show("Informacion guardada satisfactoriamente...");

                        // Cerramos la conexion
                        MyConexion.Close();
                        myCommand.Dispose();
                    }
                    catch (Exception myEx)
                    {
                        MessageBox.Show(myEx.Message);
                        throw;
                    }


                    // REGISTRO DE TIPO COMBUSTIBLES EN LA TABLA EXISTENCIA
                    try
                    {
                        // Step 1 - Stablishing the connection
                        MySqlConnection MyConexion = new MySqlConnection(clsConexion.ConectionString);

                        // Step 2 - Crear el comando de ejecucion
                        MySqlCommand myCommand = MyConexion.CreateCommand();

                        // Step 3 - Comando a ejecutar                        
                        //Int32 cantidad = 0;
                        //Int32 codigo = Convert.ToInt32(txtCodigo.Text);
                        myCommand.CommandText = "INSERT INTO existencia(tipocombustible, cantidad) values(@tipocombustible, @cantidad)";
                        myCommand.Parameters.AddWithValue("@tipocombustible", txtCodigo.Text);
                        myCommand.Parameters.AddWithValue("@cantidad", 0);

                        // Step 4 - Opening the connection
                        MyConexion.Open();

                        // Step 5 - Executing the query
                        myCommand.ExecuteNonQuery();

                        // Step 6 - Closing the connection
                        MyConexion.Close();

                        //MessageBox.Show("Informacion guardada satisfactoriamente...");

                        // Cerramos la conexion
                        MyConexion.Close();
                        myCommand.Dispose();
                    }
                    catch (Exception myEx)
                    {
                        MessageBox.Show(myEx.Message);
                        throw;
                    }

                    this.Limpiar();
                    this.cModo = "Inicio";
                    this.Botones();
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
                        myCommand.CommandText = "UPDATE tipo_combustible SET combustible = @combustible, medida = @medida, status = @status" +
                            " WHERE id = " + txtCodigo.Text + "";
                        myCommand.Parameters.AddWithValue("@combustible", txtTipo.Text);
                        myCommand.Parameters.AddWithValue("@medida", txtMedida.Text);
                        if (chkActivo.Checked == true)
                        {
                            myCommand.Parameters.AddWithValue("@status", "A");
                        }
                        else
                        {
                            myCommand.Parameters.AddWithValue("@status", "I");
                        }

                        // Step 4 - Opening the connection
                        MyConexion.Open();

                        // Step 5 - Executing the query
                        myCommand.ExecuteNonQuery();

                        // Step 6 - Closing the connection
                        MyConexion.Close();

                        MessageBox.Show("Informacion actualizada satisfactoriamente...");

                        // Cerramos la conexion
                        MyConexion.Close();
                        myCommand.Dispose();
                    }
                    catch (Exception myEx)
                    {
                        MessageBox.Show(myEx.Message);
                        throw;
                    }

                    this.Limpiar();
                    this.cModo = "Inicio";
                    this.Botones();
                }

                // 
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
                MessageBox.Show("No se permiten busquedas con Codigo en blanco...");
                //this.Limpiar();
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
                    MyCommand.CommandText = "SELECT combustible, medida, status FROM tipo_combustible WHERE id = " + txtCodigo.Text + "";

                    // Step 4 - connection open
                    MyConexion.Open();

                    // Step 5 - Creating the DataReader                    
                    MySqlDataReader MyReader = MyCommand.ExecuteReader();

                    // Step 6 - Verifying if Reader has rows
                    if (MyReader.HasRows)
                    {
                        while (MyReader.Read())
                        {
                            txtTipo.Text = MyReader["combustible"].ToString();
                            txtMedida.Text = MyReader["medida"].ToString();
                            string varStatus = MyReader["status"].ToString();
                            if (varStatus == "A")
                            {
                                chkActivo.Checked = true;
                            }
                            else
                            {
                                chkActivo.Checked = false;
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
            }
        }

        private void btnImprimir_Click(object sender, EventArgs e)
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

            try
            {
                // Abro clsConexion
                myclsConexion.Open();
                // Creo comando
                myCommand = myclsConexion.CreateCommand();
                // Adhiero el comando a la clsConexion
                myCommand.Connection = myclsConexion;
                // Filtros de la busqueda
                // CREANDO EL QUERY DE CONSULTA
                //string fechadesde = fechaDesde.Value.ToString("yyyy-MM-dd");
                //string fechahasta = fechaHasta.Value.ToString("yyyy-MM-dd");
                //cWhere = cWhere + " AND fechacita >= "+"'"+ fechadesde +"'" +" AND fechacita <= "+"'"+ fechahasta +"'"+"";
                //cWhere = cWhere + " AND year = '" + txtYear.Text + "'";
                sbQuery.Clear();
                sbQuery.Append("SELECT tipo_combustible.id, tipo_combustible.combustible, tipo_combustible.medida");
                sbQuery.Append(" FROM tipo_combustible ");
                //sbQuery.Append(" INNER JOIN provincias ON provincias.idprovincia = clientes.provincia");
                sbQuery.Append(cWhere);

                // Paso los valores de sbQuery al CommandText
                myCommand.CommandText = sbQuery.ToString();

                // Creo el objeto Data Adapter y ejecuto el command en el
                myAdapter = new MySqlDataAdapter(myCommand);

                // Creo el objeto Data Table
                DataTable dtTiposCombustibles = new DataTable();

                // Lleno el data adapter
                myAdapter.Fill(dtTiposCombustibles);

                // Cierro el objeto clsConexion
                myclsConexion.Close();

                // Verifico cantidad de datos encontrados
                int nRegistro = dtTiposCombustibles.Rows.Count;
                if (nRegistro == 0)
                {
                    MessageBox.Show("No Hay Datos Para Mostrar, Favor Verificar", "Sistema de Gestion de Combustibles", MessageBoxButtons.OK, MessageBoxIcon.Information);
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
                    cTitulo = "LISTADO DE TIPOS DE COMBUSTIBLES";

                    //6to Instanciamos nuestro REPORTE
                    //Reportes.ListadoDoctores oListado = new Reportes.ListadoDoctores();
                    //REPORTES.rptClientes orptClientes = new REPORTES.rptClientes();                                        
                    rptTiposCombustibles orptTiposCombustibles = new rptTiposCombustibles();

                    //pasamos el nombre del TITULO del Listado
                    //SumaryInfo es un objeto que se utiliza para leer,crear y actualizar las propiedades del reporte
                    // oListado.SummaryInfo.ReportTitle = cTitulo;
                    orptTiposCombustibles.SummaryInfo.ReportTitle = cTitulo;                    

                    //7mo. instanciamos nuestro el FORMULARIO donde esta nuestro ReportViewer
                    frmPrinter ofrmPrinter = new frmPrinter(dtTiposCombustibles, orptTiposCombustibles, cTitulo);
                    //ParameterFieldInfo Obtiene o establece la colección de campos de parámetros.                                                            
                    ofrmPrinter.CrystalReportViewer1.ParameterFieldInfo = oParametrosCR;
                    ofrmPrinter.ShowDialog();
                }
            }
            catch (Exception myEx)
            {
                MessageBox.Show("Error : " + myEx.Message, "Mostrando Reporte", MessageBoxButtons.OK,
                                    MessageBoxIcon.Information);
                // clsExceptionLog.LogError(myEx, false);
                return;
            }
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {

        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            if (txtTipo.Text != "")
            {
                DialogResult Result =
                MessageBox.Show("Se perderan los cambios Realizados" + System.Environment.NewLine + "Desea Guardar los Cambios", "Sistema de Gestion de Combustibles", MessageBoxButtons.YesNo,
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
