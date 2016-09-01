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
    public partial class frmMenu : Form
    {
        public string UsuarioActual;        
        //public string cUsuarioActual = frmLogin.cUsuarioActual;        
        int nivel;
        
        public frmMenu(string cUsuarioActual)
        {
            UsuarioActual = frmLogin.cUsuarioActual;
            InitializeComponent();
        }


        private void frmMenu_Load(object sender, EventArgs e)
        {
            //MessageBox.Show(UsuarioActual);
            verificapermisos(); 
        }

        private void verificapermisos()
        {
            try
            {
                // Step 1 - Conexion
                MySqlConnection MyConexion = new MySqlConnection(clsConexion.ConectionString);

                // Step 2 - creating the command object
                MySqlCommand MyCommand = MyConexion.CreateCommand();

                // Step 3 - creating the commandtext
                MyCommand.CommandText = "SELECT usuario, nivelpermiso " +
                    "FROM usuarios WHERE usuario = '" + UsuarioActual + "'";

                // Step 4 - connection open
                MyConexion.Open();

                // Step 5 - Creating the DataReader                    
                MySqlDataReader MyReader = MyCommand.ExecuteReader();

                // Step 6 - Verifying if Reader has rows
                if (MyReader.HasRows)
                {
                    while (MyReader.Read())
                    {                                            
                        nivel = Convert.ToInt32(MyReader["nivelpermiso"]);
                    }
                }
                else
                {
                    MessageBox.Show("No tiene permisos asignados...");
                    nivel = 0;
                }

                // Step 6 - Closing all
                MyReader.Close();
                MyCommand.Dispose();
                MyConexion.Close();

                // Llama la funcion que habilita permisos
                this.aplicapermisos();

            }
            catch (Exception MyEx)
            {
                MessageBox.Show(MyEx.Message);
             //   nivel = 0;
            }

            
        }

        private void aplicapermisos()
        {
            switch (nivel)
            {
                case 1:
                    
                    // Mantenimientos
                    this.menu_mantenimientos.Enabled = true;
                    this.mantenimientos_anulaciones.Enabled = true;
                    this.mantenimientos_combustible.Enabled = true;
                    this.mantenimientos_gas.Enabled = true;
                    this.mantenimientos_tickets.Enabled = true;
                    this.mantenimientos_usuarios.Enabled = true;
                    
                    // Procesos
                    this.menu_procesos.Enabled = true;
                    this.procesos_gescombustible.Enabled = true;
                    this.procesos_gesgas.Enabled = true;
                    this.procesos_gestickets.Enabled = true;
                    
                    // Reportes
                    this.menu_reportes.Enabled = true;
                    this.menu_estadisticas.Enabled = true;
                    this.reportes_listados.Enabled = true;
                    //
                    break;

                case 2:

                    // Mantenimientos
                    this.menu_mantenimientos.Enabled = true;
                    this.mantenimientos_anulaciones.Enabled = true;
                    this.mantenimientos_combustible.Enabled = false;
                    this.mantenimientos_gas.Enabled = false;
                    this.mantenimientos_tickets.Enabled = false;
                    this.mantenimientos_usuarios.Enabled = false;

                    // Procesos
                    this.menu_procesos.Enabled = true;
                    this.btnEntradaCmb.Enabled = false;
                    this.btnRegistroEntradaTicket.Enabled = false;
                    this.btnSolicitudCmb.Enabled = true;
                    this.procesos_gescombustible.Enabled = true;
                    this.procesos_gesgas.Enabled = true;
                    this.procesos_gestickets.Enabled = true;

                    // Reportes
                    this.menu_reportes.Enabled = true;
                    //
                    break;

                case 3:

                    // Mantenimientos
                    this.menu_mantenimientos.Enabled = false;
                    this.mantenimientos_anulaciones.Enabled = false;
                    this.mantenimientos_combustible.Enabled = false;
                    this.mantenimientos_gas.Enabled = false;
                    this.mantenimientos_tickets.Enabled = false;
                    this.mantenimientos_usuarios.Enabled = false;

                    // Procesos
                    this.menu_procesos.Enabled = true;
                    this.procesos_gescombustible.Enabled = true;
                    this.btnEntradaCmb.Enabled = false;
                    this.btnRegistroEntradaTicket.Enabled = false;
                    //
                    this.procesos_gesgas.Enabled = false;
                    //
                    this.procesos_gestickets.Enabled = false;
                    this.btnRegistroEntradaTicket.Enabled = false;

                    // Reportes
                    this.menu_reportes.Enabled = false;
                    //this.reportes_estadisticas.Enabled = false;
                    //this.reportes_listados.Enabled = false;
                    //
                    break;



                default:
                    this.menu_mantenimientos.Enabled = false;
                    this.menu_procesos.Enabled = false;
                    this.menu_reportes.Enabled = false;
                    break;
            }

        }


        private void buttonItem7_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void buttonItem15_Click(object sender, EventArgs e)
        {
            frmTipoCombustible ofrmTipoCombustible = new frmTipoCombustible();
            ofrmTipoCombustible.ShowDialog();
        }

        private void buttonItem16_Click(object sender, EventArgs e)
        {
            frmDeptoAutoriza ofrmDeptoAutoriza = new frmDeptoAutoriza();
            ofrmDeptoAutoriza.ShowDialog();
        }

        private void buttonItem3_Click(object sender, EventArgs e)
        {
            frmTipoBeneficiario ofrmTipoBeneficiario = new frmTipoBeneficiario();
            ofrmTipoBeneficiario.ShowDialog();
        }

        private void buttonItem1_Click(object sender, EventArgs e)
        {
            frmSolicitudCombustibles ofrmSolicitudCombustible = new frmSolicitudCombustibles();
            ofrmSolicitudCombustible.ShowDialog();
        }

        private void buttonItem11_Click(object sender, EventArgs e)
        {
            frmEntradaCombustibles ofrmEntradaCombustible = new frmEntradaCombustibles();
            ofrmEntradaCombustible.ShowDialog();
        }

        private void buttonItem14_Click(object sender, EventArgs e)
        {
            frmDespachoCombustibles ofrmDespachoCombustible = new frmDespachoCombustibles();
            ofrmDespachoCombustible.ShowDialog();       
        }

        private void buttonItem10_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void buttonItem9_Click(object sender, EventArgs e)
        {
            frmAbout ofrmAbout = new frmAbout();
            ofrmAbout.ShowDialog();
        }

        private void buttonItem2_Click(object sender, EventArgs e)
        {
            frmInformaciones ofrmInformaciones = new frmInformaciones();
            ofrmInformaciones.ShowDialog();
        }

        private void buttonItem6_Click(object sender, EventArgs e)
        {
            frmPrintListadoSolicitudCombustible ofrmPrintListadoSolicitudCombustible = new frmPrintListadoSolicitudCombustible();
            ofrmPrintListadoSolicitudCombustible.ShowDialog();
        }

        private void buttonItem25_Click(object sender, EventArgs e)
        {

            
        }

        private void buttonItem17_Click(object sender, EventArgs e)
        {
            frmBeneficiariosTickets ofrmBeneficiarioTickets = new frmBeneficiariosTickets();
            ofrmBeneficiarioTickets.ShowDialog();
        }

        private void ribbonTabItem2_Click(object sender, EventArgs e)
        {

        }

        private void buttonItem24_Click(object sender, EventArgs e)
        {
            // CODIGO QUE GENERA EL LISTADO DE LOS MILITARES BENEFICIADOS CON TICKETS DE COMBUSTIBLE

            //Conexion a la base de datos
            MySqlConnection myConexion = new MySqlConnection(clsConexion.ConectionString);
            // Creando el command que ejecutare
            MySqlCommand myCommand = new MySqlCommand();
            // Creando el Data Adapter
            MySqlDataAdapter myAdapter = new MySqlDataAdapter();
            // Creando el String Builder
            StringBuilder sbQuery = new StringBuilder();
            // Otras variables del entorno
            //string cWhere = " WHERE 1 = 1";
            string cUsuario = "";
            string cTitulo = "";

            try
            {
                // Abro conexion
                myConexion.Open();
                // Creo comando
                myCommand = myConexion.CreateCommand();
                // Adhiero el comando a la conexion
                myCommand.Connection = myConexion;
                // Filtros de la busqueda               
                //string fechadesde = dtDesde.Value.ToString("yyyy-MM-dd");
                //string fechahasta = dtHasta.Value.ToString("yyyy-MM-dd");
                //cWhere = cWhere + " AND fecha >= " + "'" + fechadesde + "'" + " AND fecha <= " + "'" + fechahasta + "'" + "";
                sbQuery.Clear();
                sbQuery.Append("SELECT tickets.rango as id, tickets.cedula, tickets.nombre, tickets.apellido, rangos.rango_descripcion as rango,");
                sbQuery.Append(" rangos.rangoabrev, rangos.orden");
                sbQuery.Append(" FROM tickets ");
                sbQuery.Append(" INNER JOIN rangos ON rangos.rango_id = tickets.rango");
                sbQuery.Append(" ORDER BY rangos.orden, tickets.apellido ASC");
                //sbQuery.Append(cWhere);
                
                // Paso los valores de sbQuery al CommandText
                myCommand.CommandText = sbQuery.ToString();
                // Creo el objeto Data Adapter y ejecuto el command en el
                myAdapter = new MySqlDataAdapter(myCommand);
                // Creo el objeto Data Table
                DataTable dtTickets = new DataTable();
                // Lleno el data adapter
                myAdapter.Fill(dtTickets);
                // Cierro el objeto conexion
                myConexion.Close();

                // Verifico cantidad de datos encontrados
                int nRegistro = dtTickets.Rows.Count;
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
                    cTitulo = "LISTADO DE MILITARES RECIBEN TICKETS COMBUSTIBLE";

                    //6to Instanciamos nuestro REPORTE
                    //Reportes.ListadoDoctores oListado = new Reportes.ListadoDoctores();
                    rptListadoBeneficiariosTickets orptListadoBeneficiariosTickets = new rptListadoBeneficiariosTickets();

                    //pasamos el nombre del TITULO del Listado
                    //SumaryInfo es un objeto que se utiliza para leer,crear y actualizar las propiedades del reporte
                    // oListado.SummaryInfo.ReportTitle = cTitulo;
                    orptListadoBeneficiariosTickets.SummaryInfo.ReportTitle = cTitulo;

                    //7mo. instanciamos nuestro el FORMULARIO donde esta nuestro ReportViewer
                    frmPrinter ofrmPrinter = new frmPrinter(dtTickets, orptListadoBeneficiariosTickets, cTitulo);

                    //ParameterFieldInfo Obtiene o establece la colección de campos de parámetros.
                    ofrmPrinter.CrystalReportViewer1.ParameterFieldInfo = oParametrosCR;
                    ofrmPrinter.ShowDialog();
                }
            }
            catch (Exception myEx)
            {
                MessageBox.Show("Error : " + myEx.Message, "Mostrando Reporte", MessageBoxButtons.OK,
                                   MessageBoxIcon.Information);
                //ExceptionLog.LogError(myEx, false);
                return;
            }
        }

        private void buttonItem19_Click(object sender, EventArgs e)
        {
            frmRegistroTickets ofrmRegistroTickets = new frmRegistroTickets();
            ofrmRegistroTickets.ShowDialog();
        }

        private void buttonItem13_Click(object sender, EventArgs e)
        {
            frmPrintEntradaCombustible ofrmPrintEntradaCombustible = new frmPrintEntradaCombustible();
            ofrmPrintEntradaCombustible.ShowDialog();
        }

        private void buttonItem5_Click(object sender, EventArgs e)
        {
            frmPrintDespachoCombustible ofrmPrintDespachoCombustible = new frmPrintDespachoCombustible();
            ofrmPrintDespachoCombustible.ShowDialog();
        }

        private void buttonItem4_Click(object sender, EventArgs e)
        {
            frmPrintDespachoCombustibleDetallado ofrmPrintDespachoCombustibleDetallado = new frmPrintDespachoCombustibleDetallado();
            ofrmPrintDespachoCombustibleDetallado.ShowDialog();
        }

        private void buttonItem12_Click(object sender, EventArgs e)
        {
            frmPrintResumenCombustibleSolicitado ofrmPrintResumenCombustibleSolicitado = new frmPrintResumenCombustibleSolicitado();
            ofrmPrintResumenCombustibleSolicitado.ShowDialog();
        }

        private void buttonItem18_Click(object sender, EventArgs e)
        {
            frmDespachoTickets ofrmDespachoTickets = new frmDespachoTickets();
            ofrmDespachoTickets.ShowDialog();
        }

        private void buttonItem26_Click(object sender, EventArgs e)
        {
            frmPrintTicketsEntregados ofrmPrintTicketsEntregados = new frmPrintTicketsEntregados();
            ofrmPrintTicketsEntregados.ShowDialog();
        }

        private void buttonItem27_Click(object sender, EventArgs e)
        {
            frmPrintTicketsRecibidos ofrmPrintTicketsRecibidos = new frmPrintTicketsRecibidos();
            ofrmPrintTicketsRecibidos.ShowDialog();
        }

        private void buttonItem28_Click(object sender, EventArgs e)
        {
            frmPrintTicketsResumenDespacho ofrmPrintTicketsResumenDespacho = new frmPrintTicketsResumenDespacho();
            ofrmPrintTicketsResumenDespacho.ShowDialog();
        }

        private void buttonItem22_Click(object sender, EventArgs e)
        {
            frmBeneficiarioGas ofrmBeneficiarioGas = new frmBeneficiarioGas();
            ofrmBeneficiarioGas.ShowDialog();
        }

        private void buttonItem23_Click(object sender, EventArgs e)
        {
            //
            // GENERA LISTADO DE LOS DEPARTAMENTOS BENEFICIARIOS DE GAS
            //
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
                sbQuery.Append("SELECT deptobeneficiariogas.id, deptobeneficiariogas.departamento, deptobeneficiariogas.tipo,");
                sbQuery.Append(" deptobeneficiariogas.tarjeta, tipo_deptogas.tipo as tipodescripcion, tipo_deptogas.id");
                sbQuery.Append(" FROM deptobeneficiariogas ");
                sbQuery.Append(" INNER JOIN tipo_deptogas ON tipo_deptogas.id = deptobeneficiariogas.tipo");
                sbQuery.Append(cWhere);
                sbQuery.Append(" ORDER BY tipo_deptogas.id");

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
                    cTitulo = "LISTADO DE DEPENDENCIAS BENEFICIARIOS DE GAS";

                    //6to Instanciamos nuestro REPORTE
                    //Reportes.ListadoDoctores oListado = new Reportes.ListadoDoctores();
                    //REPORTES.rptClientes orptClientes = new REPORTES.rptClientes();                                        
                    rptListadoDeptoBeneficiariosGas orptListadoDeptoBeneficiarioGas = new rptListadoDeptoBeneficiariosGas();

                    //pasamos el nombre del TITULO del Listado
                    //SumaryInfo es un objeto que se utiliza para leer,crear y actualizar las propiedades del reporte
                    // oListado.SummaryInfo.ReportTitle = cTitulo;
                    orptListadoDeptoBeneficiarioGas.SummaryInfo.ReportTitle = cTitulo;

                    //7mo. instanciamos nuestro el FORMULARIO donde esta nuestro ReportViewer
                    frmPrinter ofrmPrinter = new frmPrinter(dtGas, orptListadoDeptoBeneficiarioGas, cTitulo);
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

        private void buttonItem20_Click(object sender, EventArgs e)
        {
            frmDespachoGas ofrmDespachoGas = new frmDespachoGas();
            ofrmDespachoGas.ShowDialog();
        }

        private void buttonItem2_Click_1(object sender, EventArgs e)
        {
            frmPrintDespachoGas ofrmPrintDespachoGas = new frmPrintDespachoGas();
            ofrmPrintDespachoGas.ShowDialog();
        }

        private void buttonItem29_Click(object sender, EventArgs e)
        {
            frmDespachoTicketsDepto ofrmDespachoTicketsDepto = new frmDespachoTicketsDepto();
            ofrmDespachoTicketsDepto.ShowDialog();
        }

        private void buttonItem31_Click(object sender, EventArgs e)
        {
            frmPrintDespachoCombUnidNaval ofrDespachoCombUnidNaval = new frmPrintDespachoCombUnidNaval();
            ofrDespachoCombUnidNaval.ShowDialog();
        }

        private void buttonItem30_Click(object sender, EventArgs e)
        {
            frmPrintDespachoCombUnidNaval ofrmPrintDespachoCombUnidNaval = new frmPrintDespachoCombUnidNaval();
            ofrmPrintDespachoCombUnidNaval.ShowDialog();
        }

        private void buttonItem32_Click(object sender, EventArgs e)
        {
            frmAnularSolicitud ofrmAnularSolicitudCombustible = new frmAnularSolicitud();
            ofrmAnularSolicitudCombustible.ShowDialog();
        }

        private void buttonItem33_Click(object sender, EventArgs e)
        {
            frmAnularEntradaCombustible ofrmAnularEntradaCombustible = new frmAnularEntradaCombustible();
            ofrmAnularEntradaCombustible.ShowDialog();
        }

        private void buttonItem34_Click(object sender, EventArgs e)
        {
            frmAnularDespachoCombustible ofrmAnularDespachoCombustible = new frmAnularDespachoCombustible();
            ofrmAnularDespachoCombustible.ShowDialog();
        }

        private void ribbonTabItem5_Click(object sender, EventArgs e)
        {

        }

        private void buttonItem35_Click(object sender, EventArgs e)
        {
            frmAnularDespachoGas ofrmAnularDepachoGas = new frmAnularDespachoGas();
            ofrmAnularDepachoGas.ShowDialog();
        }

        private void buttonItem36_Click(object sender, EventArgs e)
        {
            frmTicketsEntregadosDepto ofrmTicketsEntregadosDepto = new frmTicketsEntregadosDepto();
            ofrmTicketsEntregadosDepto.ShowDialog();
        }

        private void buttonItem37_Click(object sender, EventArgs e)
        {
            frmAnularDespachoTicketsDepto ofrmAnularDespachoTicketsDepto = new frmAnularDespachoTicketsDepto();
            ofrmAnularDespachoTicketsDepto.ShowDialog();
        }

        private void ribbonControl1_Click(object sender, EventArgs e)
        {

        }

        private void buttonItem39_Click(object sender, EventArgs e)
        {
            frmAgregarUsuario ofrmAgregarUsuario = new frmAgregarUsuario();
            ofrmAgregarUsuario.ShowDialog();
        }

        private void btnDespachoEmbarcacion_Click(object sender, EventArgs e)
        {
            frmDespachoEmbarcacion ofrmDespachoEmbarcacion = new frmDespachoEmbarcacion();
            ofrmDespachoEmbarcacion.ShowDialog();
        }

        private void buttonItem11_Click_1(object sender, EventArgs e)
        {
            // CODIGO QUE GENERA EL REPORTE DE LA CANTIDAD DE COMBUSTIBLE EN EXISTENCIA

            //Conexion a la base de datos
            MySqlConnection myConexion = new MySqlConnection(clsConexion.ConectionString);
            // Creando el command que ejecutare
            MySqlCommand myCommand = new MySqlCommand();
            // Creando el Data Adapter
            MySqlDataAdapter myAdapter = new MySqlDataAdapter();
            // Creando el String Builder
            StringBuilder sbQuery = new StringBuilder();
            // Otras variables del entorno
            string cWhere = " WHERE 1 = 1";
            cWhere = cWhere + " AND tipo_combustible.status = " + "'A'";
            string cUsuario = "";
            string cTitulo = "";

            try
            {
                // Abro conexion
                myConexion.Open();
                // Creo comando
                myCommand = myConexion.CreateCommand();
                // Adhiero el comando a la conexion
                myCommand.Connection = myConexion;
                // Filtros de la busqueda               
                //string fechadesde = dtDesde.Value.ToString("yyyy-MM-dd");
                //string fechahasta = dtHasta.Value.ToString("yyyy-MM-dd");
                //cWhere = cWhere + " AND fecha >= " + "'" + fechadesde + "'" + " AND fecha <= " + "'" + fechahasta + "'" + "";
                sbQuery.Clear();
                sbQuery.Append("SELECT tipo_combustible.combustible as tipocombustible, existencia.cantidad, tipo_combustible.medida, ");
                sbQuery.Append(" tipo_combustible.status FROM existencia ");
                sbQuery.Append(" INNER JOIN tipo_combustible ON tipo_combustible.id = existencia.tipocombustible");
                sbQuery.Append(cWhere);

                // Paso los valores de sbQuery al CommandText
                myCommand.CommandText = sbQuery.ToString();
                // Creo el objeto Data Adapter y ejecuto el command en el
                myAdapter = new MySqlDataAdapter(myCommand);
                // Creo el objeto Data Table
                DataTable dtExistencia = new DataTable();
                // Lleno el data adapter
                myAdapter.Fill(dtExistencia);
                // Cierro el objeto conexion
                myConexion.Close();

                // Verifico cantidad de datos encontrados
                int nRegistro = dtExistencia.Rows.Count;
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
                    cTitulo = "REPORTE DE EXISTENCIA DE COMBUSTIBLES";

                    //6to Instanciamos nuestro REPORTE
                    //Reportes.ListadoDoctores oListado = new Reportes.ListadoDoctores();
                    rptExistencia orptExistencia = new rptExistencia();

                    //pasamos el nombre del TITULO del Listado
                    //SumaryInfo es un objeto que se utiliza para leer,crear y actualizar las propiedades del reporte
                    // oListado.SummaryInfo.ReportTitle = cTitulo;
                    orptExistencia.SummaryInfo.ReportTitle = cTitulo;

                    //7mo. instanciamos nuestro el FORMULARIO donde esta nuestro ReportViewer
                    frmPrinter ofrmPrinter = new frmPrinter(dtExistencia, orptExistencia, cTitulo);

                    //ParameterFieldInfo Obtiene o establece la colección de campos de parámetros.
                    ofrmPrinter.CrystalReportViewer1.ParameterFieldInfo = oParametrosCR;
                    ofrmPrinter.ShowDialog();
                }
            }
            catch (Exception myEx)
            {
                MessageBox.Show("Error : " + myEx.Message, "Mostrando Reporte", MessageBoxButtons.OK,
                                   MessageBoxIcon.Information);
                //ExceptionLog.LogError(myEx, false);
                return;
            }
        }

        private void buttonItem42_Click(object sender, EventArgs e)
        {
            frmPrintDespachoCombUnidNaval ofrDespachoCombUnidNaval = new frmPrintDespachoCombUnidNaval();
            ofrDespachoCombUnidNaval.ShowDialog();
        }

        private void buttonItem43_Click(object sender, EventArgs e)
        {
            frmPrintDespachoCombustible ofrmPrintDespachoCombustible = new frmPrintDespachoCombustible();
            ofrmPrintDespachoCombustible.ShowDialog();
        }

        private void buttonItem44_Click(object sender, EventArgs e)
        {
            frmPrintResumenCombustibleSolicitado ofrmPrintResumenCombustibleSolicitado = new frmPrintResumenCombustibleSolicitado();
            ofrmPrintResumenCombustibleSolicitado.ShowDialog();
        }

        private void buttonItem45_Click(object sender, EventArgs e)
        {
            frmPrintTicketsResumenDespacho ofrmPrintTicketsResumenDespacho = new frmPrintTicketsResumenDespacho();
            ofrmPrintTicketsResumenDespacho.ShowDialog();
        }

        private void buttonItem21_Click(object sender, EventArgs e)
        {

        }

        private void buttonItem46_Click(object sender, EventArgs e)
        {
            // CODIGO QUE GENERA EL REPORTE DE LA CANTIDAD DE COMBUSTIBLE EN EXISTENCIA

            //Conexion a la base de datos
            MySqlConnection myConexion = new MySqlConnection(clsConexion.ConectionString);
            // Creando el command que ejecutare
            MySqlCommand myCommand = new MySqlCommand();
            // Creando el Data Adapter
            MySqlDataAdapter myAdapter = new MySqlDataAdapter();
            // Creando el String Builder
            StringBuilder sbQuery = new StringBuilder();
            // Otras variables del entorno
            string cWhere = " WHERE 1 = 1";
            cWhere = cWhere + " AND tipo_combustible.status = " + "'A'";
            string cUsuario = "";
            string cTitulo = "";

            try
            {
                // Abro conexion
                myConexion.Open();
                // Creo comando
                myCommand = myConexion.CreateCommand();
                // Adhiero el comando a la conexion
                myCommand.Connection = myConexion;
                // Filtros de la busqueda               
                //string fechadesde = dtDesde.Value.ToString("yyyy-MM-dd");
                //string fechahasta = dtHasta.Value.ToString("yyyy-MM-dd");
                //cWhere = cWhere + " AND fecha >= " + "'" + fechadesde + "'" + " AND fecha <= " + "'" + fechahasta + "'" + "";
                sbQuery.Clear();
                sbQuery.Append("SELECT tipo_combustible.combustible as tipocombustible, existencia.cantidad, tipo_combustible.medida, ");
                sbQuery.Append(" tipo_combustible.status FROM existencia ");
                sbQuery.Append(" INNER JOIN tipo_combustible ON tipo_combustible.id = existencia.tipocombustible");
                sbQuery.Append(cWhere);

                // Paso los valores de sbQuery al CommandText
                myCommand.CommandText = sbQuery.ToString();
                // Creo el objeto Data Adapter y ejecuto el command en el
                myAdapter = new MySqlDataAdapter(myCommand);
                // Creo el objeto Data Table
                DataTable dtExistencia = new DataTable();
                // Lleno el data adapter
                myAdapter.Fill(dtExistencia);
                // Cierro el objeto conexion
                myConexion.Close();

                // Verifico cantidad de datos encontrados
                int nRegistro = dtExistencia.Rows.Count;
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
                    cTitulo = "REPORTE DE EXISTENCIA DE COMBUSTIBLES";

                    //6to Instanciamos nuestro REPORTE
                    //Reportes.ListadoDoctores oListado = new Reportes.ListadoDoctores();
                    rptExistencia orptExistencia = new rptExistencia();

                    //pasamos el nombre del TITULO del Listado
                    //SumaryInfo es un objeto que se utiliza para leer,crear y actualizar las propiedades del reporte
                    // oListado.SummaryInfo.ReportTitle = cTitulo;
                    orptExistencia.SummaryInfo.ReportTitle = cTitulo;

                    //7mo. instanciamos nuestro el FORMULARIO donde esta nuestro ReportViewer
                    frmPrinter ofrmPrinter = new frmPrinter(dtExistencia, orptExistencia, cTitulo);

                    //ParameterFieldInfo Obtiene o establece la colección de campos de parámetros.
                    ofrmPrinter.CrystalReportViewer1.ParameterFieldInfo = oParametrosCR;
                    ofrmPrinter.ShowDialog();
                }
            }
            catch (Exception myEx)
            {
                MessageBox.Show("Error : " + myEx.Message, "Mostrando Reporte", MessageBoxButtons.OK,
                                   MessageBoxIcon.Information);
                //ExceptionLog.LogError(myEx, false);
                return;
            }
        }

        private void buttonItem1_Click_1(object sender, EventArgs e)
        {
            frmSuplidor ofrmSuplidor = new frmSuplidor();
            ofrmSuplidor.ShowDialog();
        }

        private void buttonItem47_Click(object sender, EventArgs e)
        {
            frmPrintDespachoGasEst ofrmPrintDespachoGasEst = new frmPrintDespachoGasEst();
            ofrmPrintDespachoGasEst.ShowDialog();
        }

        private void buttonItem5_Click_1(object sender, EventArgs e)
        {
            frmEstacionCombustible ofrmEstacionesCombustible = new frmEstacionCombustible();
            ofrmEstacionesCombustible.ShowDialog();
        }

        private void buttonItem11_Click_2(object sender, EventArgs e)
        {
            frmDespachoEstaciones ofrmDespachoEstaciones = new frmDespachoEstaciones();
            ofrmDespachoEstaciones.ShowDialog();
        }

        
    }
}
