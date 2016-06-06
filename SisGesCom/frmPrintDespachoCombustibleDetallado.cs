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
    public partial class frmPrintDespachoCombustibleDetallado : frmBase
    {
        public frmPrintDespachoCombustibleDetallado()
        {
            InitializeComponent();
        }

        private void frmPrintDespachoCombustibleDetallado_Load(object sender, EventArgs e)
        {
            // LLENAR EL COMBO DE DEPENDENCIAS
            this.fillCmbCombo();

            // DESHABILITANDO COMBO
            this.cmbCombustible.Enabled = false;
        }

        private void fillCmbCombo()
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
                cmbCombustible.ValueMember = "id";
                cmbCombustible.DisplayMember = "deptobeneficiario";
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

        private void btnImprimir_Click(object sender, EventArgs e)
        {
            //Conexion a la base de datos
            MySqlConnection myConexion = new MySqlConnection(clsConexion.ConectionString);
            // Creando el command que ejecutare
            MySqlCommand myCommand = new MySqlCommand();
            // Creando el Data Adapter
            MySqlDataAdapter myAdapter = new MySqlDataAdapter();
            // Creando el String Builde
            StringBuilder sbQuery = new StringBuilder();
            // Otras variables del entorno
            string cWhere = " WHERE 1 = 1";
            string cUsuario =  frmLogin.cUsuarioActual;
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
                string fechadesde = dtDesde.Value.ToString("yyyy-MM-dd");
                string fechahasta = dtHasta.Value.ToString("yyyy-MM-dd");
                cWhere = cWhere + " AND combustible_salida.fecha >= " + "'" + fechadesde + "'" + " AND combustible_salida.fecha <= " + "'" + fechahasta + "'" + "";
                cWhere = cWhere + " AND movimientocombustible.tipo_movimiento = 'S'";

                // Filtros del tipo de Operaciones
                if (rbTerrestres.Checked == true)
                {
                    cWhere = cWhere + " AND movimientocombustible.operaciones = 'T'";
                }
                else if (rbMaritimas.Checked == true)
                {
                    cWhere = cWhere + " AND movimientocombustible.operaciones = 'M'";
                }
               
                // Filtros por departamentos
                if (chkDepartamentos.Checked == true)
                {
                    cWhere = cWhere + " AND combustible_salida.beneficiario_depto = " + this.cmbCombustible.SelectedValue + "";
                }

                // Filtros por anuladas o no
                if (chkAnuladas.Checked == true)
                {
                    cWhere = cWhere + " AND movimientocombustible.anulada = 1";
                }
                else
                {
                    cWhere = cWhere + " AND movimientocombustible.anulada = 0";
                }
                
                sbQuery.Clear();                                
                sbQuery.Append(" SELECT combustible_salida.id, movimientocombustible.descripcion_combustible, combustible_salida.nota,");
                sbQuery.Append(" movimientocombustible.cantidad as cantidad, combustible_salida.beneficiario,");
                sbQuery.Append(" combustible_salida.beneficiario_depto,	movimientocombustible.fecha,");
                sbQuery.Append(" tipo_combustible.combustible as tipo_combustible, departamento_autoriza.departamento as autorizadopor,");
                sbQuery.Append(" deptobeneficiario.deptobeneficiario as tipobeneficiario, tipo_combustible.medida");
                sbQuery.Append(" FROM combustible_salida");
                sbQuery.Append(" INNER JOIN movimientocombustible ON movimientocombustible.id = combustible_salida.id");
                sbQuery.Append(" INNER JOIN tipo_combustible ON tipo_combustible.id = movimientocombustible.tipo_combustible");
                sbQuery.Append(" INNER JOIN departamento_autoriza ON departamento_autoriza.id = combustible_salida.autorizadopor");
                sbQuery.Append(" INNER JOIN deptobeneficiario ON deptobeneficiario.id = combustible_salida.beneficiario_depto");
                sbQuery.Append(cWhere);
                //sbQuery.Append(" GROUP BY tipo_combustible");
                //sbQuery.Append(" GROUP BY tipo_combustible, tipobeneficiario");
                sbQuery.Append(" ORDER BY combustible_salida.id");

                // Paso los valores de sbQuery al CommandText
                myCommand.CommandText = sbQuery.ToString();
                // Creo el objeto Data Adapter y ejecuto el command en el
                myAdapter = new MySqlDataAdapter(myCommand);
                // Creo el objeto Data Table
                DataTable dtMovimientoCombustible = new DataTable();
                // Lleno el data adapter
                myAdapter.Fill(dtMovimientoCombustible);
                // Cierro el objeto conexion
                myConexion.Close();

                // Verifico cantidad de datos encontrados
                int nRegistro = dtMovimientoCombustible.Rows.Count;
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
                    ParameterField oFechaInicial = new ParameterField();
                    ParameterField oFechaFinal = new ParameterField();
                    //parametervaluetype especifica el TIPO de valor de parametro
                    //ParameterValueKind especifica el tipo de valor de parametro en la PARAMETERVALUETYPE de la Clase PARAMETERFIELD
                    oUsuario.ParameterValueType = ParameterValueKind.StringParameter;
                    oFechaInicial.ParameterValueType = ParameterValueKind.DateTimeParameter;
                    oFechaFinal.ParameterValueType = ParameterValueKind.DateTimeParameter;

                    //3ero.VALORES PARA LOS PARAMETROS
                    //ParameterDiscreteValue proporciona propiedades para la recuperacion y configuracion de 
                    //parametros de valores discretos
                    ParameterDiscreteValue oUsuarioDValue = new ParameterDiscreteValue();
                    oUsuarioDValue.Value = cUsuario;
                    ParameterDiscreteValue oFechaDValue = new ParameterDiscreteValue();
                    oFechaDValue.Value = fechadesde;
                    ParameterDiscreteValue oFechaFinDValue = new ParameterDiscreteValue();
                    oFechaFinDValue.Value = fechahasta;

                    //4to. AGREGAMOS LOS VALORES A LOS PARAMETROS
                    oUsuario.CurrentValues.Add(oUsuarioDValue);
                    oFechaInicial.CurrentValues.Add(oFechaDValue);
                    oFechaFinal.CurrentValues.Add(oFechaFinDValue);

                    //5to. AGREGAMOS LOS PARAMETROS A LA COLECCION 
                    oParametrosCR.Add(oUsuario);
                    oParametrosCR.Add(oFechaInicial);
                    oParametrosCR.Add(oFechaFinal);

                    //nombre del parametro en CR (Crystal Reports)
                    oParametrosCR[0].Name = "cUsuario";
                    oParametrosCR[1].Name = "cFechaInicial";
                    oParametrosCR[2].Name = "cFechaFinal";

                    //nombre del TITULO DEL INFORME
                    cTitulo = "Reporte de Despacho de Combustible Detallado";

                    //6to Instanciamos nuestro REPORTE
                    //Reportes.ListadoDoctores oListado = new Reportes.ListadoDoctores();
                    rptDespachoCombustibleDetallado orptDespachoCombustibleDetallado = new rptDespachoCombustibleDetallado();

                    //pasamos el nombre del TITULO del Listado
                    //SumaryInfo es un objeto que se utiliza para leer,crear y actualizar las propiedades del reporte
                    // oListado.SummaryInfo.ReportTitle = cTitulo;
                    orptDespachoCombustibleDetallado.SummaryInfo.ReportTitle = cTitulo;

                    //7mo. instanciamos nuestro el FORMULARIO donde esta nuestro ReportViewer
                    frmPrinter ofrmPrinter = new frmPrinter(dtMovimientoCombustible, orptDespachoCombustibleDetallado, cTitulo);

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

        private void btnSalir_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void chkDepartamentos_CheckedChanged(object sender, EventArgs e)
        {
            if (chkDepartamentos.Checked == true)
            {
                this.cmbCombustible.Enabled = true;
            }
            else
            {
                this.cmbCombustible.Enabled = false;
            }
        }
    }
}
