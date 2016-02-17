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

        public string cUsuarioActual = frmLogin.cUsuarioActual;
        
        public frmMenu()
        {
            InitializeComponent();
        }

        private void buttonItem7_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void buttonItem15_Click(object sender, EventArgs e)
        {
            frmTipoCombustible ofrmTipoCombustible = new frmTipoCombustible();
            ofrmTipoCombustible.Show();
        }

        private void buttonItem16_Click(object sender, EventArgs e)
        {
            frmDeptoAutoriza ofrmDeptoAutoriza = new frmDeptoAutoriza();
            ofrmDeptoAutoriza.Show();
        }

        private void buttonItem3_Click(object sender, EventArgs e)
        {
            frmTipoBeneficiario ofrmTipoBeneficiario = new frmTipoBeneficiario();
            ofrmTipoBeneficiario.Show();
        }

        private void buttonItem1_Click(object sender, EventArgs e)
        {
            frmSolicitudCombustibles ofrmSolicitudCombustible = new frmSolicitudCombustibles();
            ofrmSolicitudCombustible.Show();
        }

        private void buttonItem11_Click(object sender, EventArgs e)
        {
            frmEntradaCombustibles ofrmEntradaCombustible = new frmEntradaCombustibles();
            ofrmEntradaCombustible.Show();
        }

        private void buttonItem14_Click(object sender, EventArgs e)
        {
            frmDespachoCombustibles ofrmDespachoCombustible = new frmDespachoCombustibles();
            ofrmDespachoCombustible.Show();            
        }

        private void buttonItem10_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void buttonItem9_Click(object sender, EventArgs e)
        {
            frmAbout ofrmAbout = new frmAbout();
            ofrmAbout.Show();
        }

        private void buttonItem2_Click(object sender, EventArgs e)
        {
            frmInformaciones ofrmInformaciones = new frmInformaciones();
            ofrmInformaciones.Show();
        }

        private void buttonItem6_Click(object sender, EventArgs e)
        {
            frmPrintListadoSolicitudCombustible ofrmPrintListadoSolicitudCombustible = new frmPrintListadoSolicitudCombustible();
            ofrmPrintListadoSolicitudCombustible.Show();
        }

        private void buttonItem25_Click(object sender, EventArgs e)
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
                sbQuery.Append("SELECT tipo_combustible.combustible as tipocombustible, existencia.cantidad, tipo_combustible.medida ");                
                sbQuery.Append(" FROM existencia ");
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

        private void buttonItem17_Click(object sender, EventArgs e)
        {
            frmBeneficiariosTickets ofrmBeneficiarioTickets = new frmBeneficiariosTickets();
            ofrmBeneficiarioTickets.Show();
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
            ofrmRegistroTickets.Show();
        }

        private void buttonItem13_Click(object sender, EventArgs e)
        {
            frmPrintEntradaCombustible ofrmPrintEntradaCombustible = new frmPrintEntradaCombustible();
            ofrmPrintEntradaCombustible.Show();
        }

        private void buttonItem5_Click(object sender, EventArgs e)
        {
            frmPrintDespachoCombustible ofrmPrintDespachoCombustible = new frmPrintDespachoCombustible();
            ofrmPrintDespachoCombustible.Show();
        }

        private void buttonItem4_Click(object sender, EventArgs e)
        {
            frmPrintDespachoCombustibleDetallado ofrmPrintDespachoCombustibleDetallado = new frmPrintDespachoCombustibleDetallado();
            ofrmPrintDespachoCombustibleDetallado.Show();
        }

        private void buttonItem12_Click(object sender, EventArgs e)
        {
            frmPrintResumenCombustibleSolicitado ofrmPrintResumenCombustibleSolicitado = new frmPrintResumenCombustibleSolicitado();
            ofrmPrintResumenCombustibleSolicitado.Show();
        }

        private void buttonItem18_Click(object sender, EventArgs e)
        {
            frmDespachoTickets ofrmDespachoTickets = new frmDespachoTickets();
            ofrmDespachoTickets.Show();
        }

        private void buttonItem26_Click(object sender, EventArgs e)
        {
            frmPrintTicketsEntregados ofrmPrintTicketsEntregados = new frmPrintTicketsEntregados();
            ofrmPrintTicketsEntregados.Show();
        }

        private void buttonItem27_Click(object sender, EventArgs e)
        {
            frmPrintTicketsRecibidos ofrmPrintTicketsRecibidos = new frmPrintTicketsRecibidos();
            ofrmPrintTicketsRecibidos.Show();
        }

        private void buttonItem28_Click(object sender, EventArgs e)
        {
            frmPrintTicketsResumenDespacho ofrmPrintTicketsResumenDespacho = new frmPrintTicketsResumenDespacho();
            ofrmPrintTicketsResumenDespacho.Show();
        }

        private void buttonItem22_Click(object sender, EventArgs e)
        {
            frmBeneficiarioGas ofrmBeneficiarioGas = new frmBeneficiarioGas();
            ofrmBeneficiarioGas.Show();
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
            ofrmDespachoGas.Show();
        }

        private void buttonItem2_Click_1(object sender, EventArgs e)
        {
            frmPrintDespachoGas ofrmPrintDespachoGas = new frmPrintDespachoGas();
            ofrmPrintDespachoGas.Show();
        }

        private void buttonItem29_Click(object sender, EventArgs e)
        {
            frmDespachoTicketsDepto ofrmDespachoTicketsDepto = new frmDespachoTicketsDepto();
            ofrmDespachoTicketsDepto.Show();
        }

        private void buttonItem31_Click(object sender, EventArgs e)
        {
            
        }

        private void buttonItem30_Click(object sender, EventArgs e)
        {
            frmPrintDespachoCombUnidNaval ofrmPrintDespachoCombUnidNaval = new frmPrintDespachoCombUnidNaval();
            ofrmPrintDespachoCombUnidNaval.Show();
        }

        private void buttonItem32_Click(object sender, EventArgs e)
        {
            frmAnularSolicitud ofrmAnularSolicitudCombustible = new frmAnularSolicitud();
            ofrmAnularSolicitudCombustible.ShowDialog();
        }
    }
}
