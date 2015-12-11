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
            frmSolicitudCombustible ofrmSolicitudCombustible = new frmSolicitudCombustible();
            ofrmSolicitudCombustible.Show();
        }

        private void buttonItem11_Click(object sender, EventArgs e)
        {
            frmEntradaCombustible ofrmEntradaCombustible = new frmEntradaCombustible();
            ofrmEntradaCombustible.Show();
        }

        private void buttonItem14_Click(object sender, EventArgs e)
        {
            frmSalidaCombustible ofrmSalidaCombustible = new frmSalidaCombustible();
            ofrmSalidaCombustible.Show();
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
                sbQuery.Append("SELECT tipo_combustible.combustible as tipocombustible, existencia.cantidad ");                
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
                    cTitulo = "REPORTE DE EXISTENCIA DE COMBUSTIBLE";

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
    }
}
