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
using CrystalDecisions.VSDesigner;
using CrystalDecisions.Web;
using CrystalDecisions.Windows;

namespace SisGesCom
{
    public partial class frmPrinter : frmBase
    {

        DataTable _Datos;
        CrystalDecisions.CrystalReports.Engine.ReportDocument _Reporte;
        string _Titulo = "";
        DataTable _Encabezado;

        public frmPrinter(DataTable dtDatos, CrystalDecisions.CrystalReports.Engine.ReportDocument oReporte, string cTitulo)
        {
            _Datos = dtDatos;
            _Reporte = oReporte;
            _Titulo = cTitulo;

            InitializeComponent();
        }

        public frmPrinter(DataTable dtDatos, DataTable dtEncabezado, CrystalDecisions.CrystalReports.Engine.ReportDocument oReporte, string cTitulo)
        {
            _Datos = dtDatos;
            _Reporte = oReporte;
            _Titulo = cTitulo;
            _Encabezado = dtEncabezado;

            InitializeComponent();
        }

        private void frmPrinter_Load(object sender, EventArgs e)
        {
            try
            {
                int nRegistros = _Datos.Rows.Count;
                //if (nRegistros == 0)
                //{
                //    MessageBox.Show("No hay información para mostrar, favor verificar", "Mostrando Reporte", MessageBoxButtons.OK,
                //    MessageBoxIcon.Information);
                //    return;
                //}                 
                this.lblTituloForm.Text = _Titulo;
                // CrystalReportViewer1.ShowGroupTreeButton  = false;
                //CrystalReportViewer1.DisplayToolbar = false;                 

                // Asigno titulo del Reporte 
                /*
                SummaryInfo oSummaryInfo = new SummaryInfo();
                oSummaryInfo = _Reporte.SummaryInfo();
                oSummaryInfo.ReportTitle = _Titulo;
                oSummaryInfo.ReportAuthor = "Luis Alberto Turbi"; */

                CrystalReportViewer1.ToolPanelView = CrystalDecisions.Windows.Forms.ToolPanelViewType.None;
                CrystalReportViewer1.ReuseParameterValuesOnRefresh = true;

                // Asigno los datos
                _Reporte.Database.Tables[0].SetDataSource(_Datos);
                CrystalReportViewer1.ReuseParameterValuesOnRefresh = true;
                CrystalReportViewer1.ReportSource = _Reporte;
                // CrystalReportViewer1.Refresh();

                /*
                 // Metodo con la ruta del archivo fisico
                if (VFPToolkit.files.File(VFPToolkit.files.FullPath(_Reporte)))
                {
                    CrReport.Load(@VFPToolkit.files.FullPath(_Reporte));
                    CrReport.SetDataSource(_Datos);
                    CrystalReportViewer1.ReportSource = CrReport;
                }
                else
                {
                    MessageBox.Show("Archivo No Existe: " + VFPToolkit.files.FullPath(@_Reporte), "Mostrando Reporte", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }*/

            }
            catch (Exception ex)
            {
                MessageBox.Show("Excepcion: " + ex.Message, "Mostrando Reporte", MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
                clsExceptionLog.LogError(ex, false);
                Cursor.Current = Cursors.Default;
                this.Cursor = Cursors.Default;
                this.UseWaitCursor = false;
                Application.UseWaitCursor = false;
                return;
            }
        }

        private void cmdPrimeraPag_Click(object sender, EventArgs e)
        {
            CrystalReportViewer1.ShowFirstPage();
        }

        private void cmdAnterior_Click(object sender, EventArgs e)
        {
            CrystalReportViewer1.ShowPreviousPage();
        }

        private void cmdSiguiente_Click(object sender, EventArgs e)
        {
            CrystalReportViewer1.ShowNextPage();
        }

        private void cmdUltimaPag_Click(object sender, EventArgs e)
        {
            CrystalReportViewer1.ShowLastPage();
        }

        private void cmdExportarPdf_Click(object sender, EventArgs e)
        {
            CrystalReportViewer1.ExportReport();
        }

        private void cmdImprimir_Click(object sender, EventArgs e)
        {
            CrystalReportViewer1.PrintReport();
        }

        private void cmdZoom_Click(object sender, EventArgs e)
        {
            CrystalReportViewer1.Zoom(Convert.ToInt32(txtUpDown.Value));
        }

        private void frmPrinter_KeyUp(object sender, KeyEventArgs e)
        {
            Funciones_F(sender, e);
        }

        #region Funciones_F
        private void Funciones_F(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {

                case Keys.F2:
                    /*  if (lblPrimeraPag.Enabled == true)
                      {
                          cmdPrimeraPag_Click(sender, e);
                      }*/
                    break;

                case Keys.F3:
                    /* if (lblAnterior.Enabled == true)
                    {
                        cmdAnterior_Click(sender, e);
                    }
                      */
                    break;

                case Keys.F4:
                    /*  if (lblSiguiente.Enabled == true)
                      {
                          cmdSiguiente_Click(sender, e);
                      }*/
                    break;

                case Keys.F5:
                    /*     if (lblUltimaPag.Enabled == true)
                         {
                             cmdUltimaPag_Click(sender, e);
                         }  */
                    break;

                case Keys.F6:
                    /*   if (lblImprimir.Enabled == true)
                       {
                           cmdImprimir_Click(sender, e);
                       } */
                    break;

                case Keys.F7:
                    /*   if (lblExportarPdf.Enabled == true)
                       {
                           cmdExportarPdf_Click(sender, e);
                       } */
                    break;

                case Keys.F8:
                    /*    if (lblSalir.Enabled == true)
                        {
                            cmdSalir_Click(sender, e);
                        } */
                    break;

            }
        }
        #endregion

        private void CrystalReportViewer1_Load(object sender, EventArgs e)
        {
            txtUpDown.Value = 100;
        }

        private void CrystalReportViewer1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)(Keys.Escape))
            {
                e.Handled = true;
                this.Close();
            }
        }

        private void txtIrPag_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (char.IsNumber(e.KeyChar))
            {
                CrystalReportViewer1.ShowNthPage(Convert.ToInt32(txtIrPag.Text));
            }
            else
            {
                MessageBox.Show("Solo se Admite Numeros En este Capo");
                e.Handled = true;
            }
        }

        private void txtBuscarText_KeyPress(object sender, KeyPressEventArgs e)
        {
            CrystalReportViewer1.SearchForText(txtBuscarText.Text);
        }

        private void frmPrinter_FormClosing(object sender, FormClosingEventArgs e)
        {
            this.CrystalReportViewer1 = null;
        }

        private void cmdSalir_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void chkGrupos_CheckedChanged(object sender, EventArgs e)
        {

        }


    }
}
