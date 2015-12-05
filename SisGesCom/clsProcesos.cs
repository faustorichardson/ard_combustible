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
//using VFPToolkit;
using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.ReportSource;
using CrystalDecisions.Shared;
using CrystalDecisions.Windows.Forms;
//using ReaSanto.LogicaNegocio;
using System.Xml;
using System.Data.Odbc;


namespace SisGesCom
{
    class clsProcesos
    {
        // Turbi: Simula un InputBox  de fox y VB
        public DialogResult InputBox(string title, string promptText, ref string value)
        {
            Form form = new Form();
            Label label = new Label();
            TextBox textBox = new TextBox();
            Button buttonOk = new Button();
            Button buttonCancel = new Button();

            form.Text = title;
            label.Text = promptText;
            textBox.Text = value;

            buttonOk.Text = "OK";
            buttonCancel.Text = "Cancel";
            buttonOk.DialogResult = DialogResult.OK;
            buttonCancel.DialogResult = DialogResult.Cancel;

            label.SetBounds(9, 20, 372, 13);
            textBox.SetBounds(12, 36, 372, 20);
            buttonOk.SetBounds(228, 72, 75, 23);
            buttonCancel.SetBounds(309, 72, 75, 23);

            label.AutoSize = true;
            textBox.Anchor = textBox.Anchor | AnchorStyles.Right;
            buttonOk.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            buttonCancel.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;

            form.ClientSize = new Size(396, 107);
            form.Controls.AddRange(new Control[] { label, textBox, buttonOk, buttonCancel });
            form.ClientSize = new Size(Math.Max(300, label.Right + 10), form.ClientSize.Height);
            form.FormBorderStyle = FormBorderStyle.FixedDialog;
            form.StartPosition = FormStartPosition.CenterScreen;
            form.MinimizeBox = false;
            form.MaximizeBox = false;
            form.AcceptButton = buttonOk;
            form.CancelButton = buttonCancel;

            DialogResult dialogResult = form.ShowDialog();
            value = textBox.Text;
            return dialogResult;
        }

        // Declaramos nuestro metodo que hara la limpieza de los textbox
        public void LimpiarTextBox(Form ofrm)
        {
            // hace un chequeo por todos los textbox del formulario
            foreach (Control oControls in ofrm.Controls)
            {
                if (oControls is TextBox)
                {
                    oControls.Text = ""; // eliminar el texto
                }
            }
        }
        public void ReadOnly(Form ofrm)
        {
            foreach (Control oControls in ofrm.Controls)
            {
                if (oControls is GroupBox)
                {
                    oControls.Enabled = false;

                }
            }

        }
        // Turbi, Limpiar controles
        public void LimpiarControles(Control c)
        {
            foreach (Control Ctrl in c.Controls)
            {
                try
                {
                    //Console.WriteLine(Ctrl.GetType().ToString());
                    //MessageBox.Show ( (Ctrl.GetType().ToString())) ;
                    switch (Ctrl.GetType().ToString())
                    {
                        case "System.Windows.Forms.CheckBox":
                            ((CheckBox)Ctrl).Checked = false;
                            break;

                        case "System.Windows.Forms.TextBox":
                            ((TextBox)Ctrl).Text = "";
                            break;

                        case "System.Windows.Forms.RichTextBox":
                            ((RichTextBox)Ctrl).Text = "";
                            break;

                        case "System.Windows.Forms.ComboBox":
                            ((ComboBox)Ctrl).SelectedIndex = -1;
                            ((ComboBox)Ctrl).SelectedIndex = -1;
                            break;

                        case "System.Windows.Forms.MaskedTextBox":

                            ((MaskedTextBox)Ctrl).Text = "";
                            break;

                        case "Infragistics.Win.UltraWinMaskedEdit.UltraMaskedEdit":
                            //   ((Infragistics.Win.UltraMaskedEdit.UltraMaskedEdit)Ctrl).Text = "";
                            break;

                        case "Infragistics.Win.UltraWinEditors.UltraDateTimeEditor":
                            DateTime dt = DateTime.Now;
                            string shortDate = dt.ToShortDateString();
                            //        ((UltraDateTimeEditor)Ctrl).Text = shortDate;
                            break;

                        case " Infragistics.Win.UltraWinGrid.UltraCombo":
                            //         ((UltraCombo)Ctrl).Text = "";
                            break;

                        case "Infragistics.Win.UltraWinEditors.UltraCurrencyEditor":
                            //         ((UltraCurrencyEditor)Ctrl).Value = 0.0m;
                            break;

                        default:
                            if (Ctrl.Controls.Count > 0)
                                LimpiarControles(Ctrl);
                            break;
                    }
                }
                catch (Exception ex)
                {

                    MessageBox.Show("Error : " + ex.Message, "Resetear Controles", MessageBoxButtons.OK,
                        MessageBoxIcon.Information);
                    clsExceptionLog.LogError(ex, false);
                    return;
                }
                finally
                {


                }
            }
        }

        // Declaramos nuestro metodo para habilitar los controles
        public void AbilitarDesabilitarControles(Form ofrm, bool lTrueFalse)
        {
            // hace un chequeo por todos los controles del formulario
            foreach (Control oControls in ofrm.Controls)
            {
                if (oControls is Button)
                {

                }
                else
                {
                    oControls.Enabled = lTrueFalse;
                }
            }
        }

        //metodo para cargar los datos de la bd
        public static DataTable DatosGeneral(string tabla)
        {
            try
            {
                DataTable dt = new DataTable();
                clsProcesos con = new clsProcesos();
                string constring = con.conectar();
                MySql.Data.MySqlClient.MySqlConnection objCon = new MySqlConnection();
                objCon.ConnectionString = constring;
                objCon.Open();

                string consulta = "SELECT * FROM " + tabla + " "; //consulta a la tabla paises
                MySqlCommand comando = new MySqlCommand(consulta, objCon);

                MySqlDataAdapter adap = new MySqlDataAdapter(comando);

                //Turbi
                //dt.Locale = System.Globalization.CultureInfo.InvariantCulture;

                adap.Fill(dt);
                objCon.Close();
                return dt;
            }
            catch (MySql.Data.MySqlClient.MySqlException ex)
            {
                MessageBox.Show("Error : " + ex.Message, "Bancos", MessageBoxButtons.OK,
                    MessageBoxIcon.Information);
                clsExceptionLog.LogError(ex, false);
                return null;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error : " + ex.Message, "Bancos", MessageBoxButtons.OK,
                    MessageBoxIcon.Information);
                clsExceptionLog.LogError(ex, false);
                return null;
            }
            finally
            {

            }
        }

        //Turbi: metodo para cargar los datos de la bd sobrecargado
        public static DataTable DatosGeneral(string tabla, string cFiltro, string cOrderBy)
        {

            try
            {
                if (cFiltro.Trim() == "")
                {
                    cFiltro = " Where 1 = 1 ";
                }

                if (cOrderBy.Trim() == "")
                {
                    cOrderBy = " Order by 1 ";
                }

                DataTable dt = new DataTable();
                clsProcesos con = new clsProcesos();
                string constring = con.conectarConta();
                if (constring == null) { constring = SisGesCom.clsConexion.ConectionString; }

                MySqlConnection objCon = new MySqlConnection();
                objCon.ConnectionString = constring;
                objCon.Open();

                string consulta = "SELECT * FROM " + tabla + " " + cFiltro + " " + cOrderBy;
                //VFPToolkit.strings.StrToFile(consulta, "Consulta.sql", true);
                MySqlCommand comando = new MySqlCommand(consulta, objCon);
                MySqlDataAdapter adap = new MySqlDataAdapter(comando);

                //Turbi
                //dt.Locale = System.Globalization.CultureInfo.InvariantCulture;

                adap.Fill(dt);
                objCon.Close();
                return dt;
            }
            catch (MySql.Data.MySqlClient.MySqlException ex)
            {
                MessageBox.Show("Error : " + ex.Message, "Activos Fijos", MessageBoxButtons.OK,
                    MessageBoxIcon.Information);
                clsExceptionLog.LogError(ex, false);
                return null;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error : " + ex.Message, "frmActivosFijos", MessageBoxButtons.OK,
                    MessageBoxIcon.Information);
                clsExceptionLog.LogError(ex, false);
                return null;
            }

            finally
            {

            }
        }
        public static DataTable DatosGeneral(string tabla, string cOrderBy)
        {

            try
            {

                if (cOrderBy.Trim() == "")
                {
                    cOrderBy = "Order by 1 ";
                }

                DataTable dt = new DataTable();
                clsProcesos con = new clsProcesos();
                string constring = con.conectarConta();
                if (constring == null) { constring = SisGesCom.clsConexion.ConectionString; }

                MySqlConnection objCon = new MySqlConnection();
                objCon.ConnectionString = constring;
                objCon.Open();

                string consulta = "SELECT * FROM " + tabla + " " + cOrderBy;
                MySqlCommand comando = new MySqlCommand(consulta, objCon);
                MySqlDataAdapter adap = new MySqlDataAdapter(comando);

                //Turbi
                //dt.Locale = System.Globalization.CultureInfo.InvariantCulture;

                adap.Fill(dt);
                objCon.Close();  // Turbi
                return dt;
            }
            catch (MySql.Data.MySqlClient.MySqlException ex)
            {
                MessageBox.Show("Error : " + ex.Message, "Bancos", MessageBoxButtons.OK,
                    MessageBoxIcon.Information);
                clsExceptionLog.LogError(ex, false);
                return null;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error : " + ex.Message, "Bancos", MessageBoxButtons.OK,
                    MessageBoxIcon.Information);
                clsExceptionLog.LogError(ex, false);
                return null;
            }

            finally
            {

            }
        }

        //metodo para cargar los datos de la bd
        public static DataTable DatosGeneralEscalar(string tabla, string cExpresion)
        {
            try
            {
                DataTable dt = new DataTable();
                clsProcesos con = new clsProcesos();
                string constring = con.conectar();
                MySql.Data.MySqlClient.MySqlConnection objCon = new MySqlConnection();
                objCon.ConnectionString = constring;
                objCon.Open();

                string consulta = "SELECT " + cExpresion + " As Retorno FROM " + tabla + " "; //consulta a la tabla paises
                MySqlCommand comando = new MySqlCommand(consulta, objCon);

                MySqlDataAdapter adap = new MySqlDataAdapter(comando);

                //Turbi
                //dt.Locale = System.Globalization.CultureInfo.InvariantCulture;

                adap.Fill(dt);
                objCon.Close();
                return dt;
            }
            catch (MySql.Data.MySqlClient.MySqlException ex)
            {
                MessageBox.Show("Error : " + ex.Message, "Bancos", MessageBoxButtons.OK,
                    MessageBoxIcon.Information);
                clsExceptionLog.LogError(ex, false);
                return null;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error : " + ex.Message, "Bancos", MessageBoxButtons.OK,
                    MessageBoxIcon.Information);
                clsExceptionLog.LogError(ex, false);
                return null;
            }
            finally
            {

            }
        }

        //retornas los permisos del usuario
        public static DataTable Permisos(string cusuario, string copcion)
        {

            try
            {


                DataTable dt = new DataTable();
                clsProcesos con = new clsProcesos();
                string constring = con.conectarConta();
                if (constring == null) { constring = SisGesCom.clsConexion.ConectionString; }

                MySqlConnection objCon = new MySqlConnection();
                objCon.ConnectionString = constring;
                objCon.Open();

                string consulta = "SELECT * FROM PermisoUsuarios where usuario=+'" + cusuario.Trim() + "' and menu='" + copcion.Trim() + "' ";
                MySqlCommand comando = new MySqlCommand(consulta, objCon);
                MySqlDataAdapter adap = new MySqlDataAdapter(comando);

                //Turbi
                //dt.Locale = System.Globalization.CultureInfo.InvariantCulture;

                adap.Fill(dt);
                return dt;
            }
            catch (MySql.Data.MySqlClient.MySqlException ex)
            {
                MessageBox.Show("Error : " + ex.Message, "Bancos", MessageBoxButtons.OK,
                    MessageBoxIcon.Information);
                clsExceptionLog.LogError(ex, false);
                return null;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error : " + ex.Message, "Bancos", MessageBoxButtons.OK,
                    MessageBoxIcon.Information);
                clsExceptionLog.LogError(ex, false);
                return null;
            }

            finally
            {

            }
        }

        public static DataTable ValidaDatos(string Tabla, string cFiltro)
        {
            try
            {
                if (cFiltro.Trim() == "")
                {
                    cFiltro = " Where 1 = 1 ";
                }

                //if (cOrderBy.Trim() == "")
                //{
                //    cOrderBy = " Order by 1 ";
                //}

                DataTable dt = new DataTable();
                clsProcesos con = new clsProcesos();
                string constring = con.conectarConta();
                if (constring == null) { constring = SisGesCom.clsConexion.ConectionString; }

                MySqlConnection objCon = new MySqlConnection();
                objCon.ConnectionString = constring;
                objCon.Open();

                string consulta = "SELECT * FROM " + Tabla + " " + cFiltro;
                MySqlCommand comando = new MySqlCommand(consulta, objCon);
                MySqlDataAdapter adap = new MySqlDataAdapter(comando);

                //Turbi
                //dt.Locale = System.Globalization.CultureInfo.InvariantCulture;

                adap.Fill(dt);
                return dt;
            }
            catch (MySql.Data.MySqlClient.MySqlException ex)
            {
                MessageBox.Show("Error : " + ex.Message, "Bancos", MessageBoxButtons.OK,
                    MessageBoxIcon.Information);
                clsExceptionLog.LogError(ex, false);
                return null;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error : " + ex.Message, "Bancos", MessageBoxButtons.OK,
                    MessageBoxIcon.Information);
                clsExceptionLog.LogError(ex, false);
                return null;
            }

            finally
            {

            }

        }

        // Turbi: SobreCarga metodo para cargar la coleccion de datos para el autocomplete
        public static AutoCompleteStringCollection AutocompleteGeneral(string campo, string cTabla)
        {
            if (cTabla.Trim() == "")
            {
                return AutocompleteGeneral(campo);
            }

            string tabla = cTabla;
            DataTable dt = DatosGeneral(tabla);
            AutoCompleteStringCollection coleccion = new AutoCompleteStringCollection();
            //recorrer y cargar los items para el autocompletado
            foreach (DataRow row in dt.Rows)
            {
                coleccion.Add(Convert.ToString(row[campo]));
            }
            return coleccion;
        }

        //metodo para cargar la coleccion de datos para el autocomplete
        public static AutoCompleteStringCollection AutocompleteGeneral(string campo)
        {
            string tabla = "catalogo";
            DataTable dt = DatosGeneral(tabla);
            AutoCompleteStringCollection coleccion = new AutoCompleteStringCollection();
            //recorrer y cargar los items para el autocompletado
            foreach (DataRow row in dt.Rows)
            {
                coleccion.Add(Convert.ToString(row[campo]));
            }
            return coleccion;
        }

        //metodo para cargar los datos de la bd
        public static DataTable Datos()
        {
            try
            {
                DataTable dt = new DataTable();
                clsProcesos con = new clsProcesos();
                string constring = con.conectarConta();
                MySqlConnection objCon = new MySqlConnection();
                objCon.ConnectionString = constring;
                objCon.Open();

                string consulta = "SELECT * FROM items"; //consulta a la tabla paises
                MySqlCommand comando = new MySqlCommand(consulta, objCon);

                MySqlDataAdapter adap = new MySqlDataAdapter(comando);

                adap.Fill(dt);
                return dt;

            }
            catch (MySql.Data.MySqlClient.MySqlException ex)
            {
                MessageBox.Show("Error : " + ex.Message, "Bancos", MessageBoxButtons.OK,
                    MessageBoxIcon.Information);
                clsExceptionLog.LogError(ex, false);
                return null;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error : " + ex.Message, "Bancos", MessageBoxButtons.OK,
                    MessageBoxIcon.Information);
                clsExceptionLog.LogError(ex, false);
                return null;
            }
            finally
            {

            }
        }

        //metodo para cargar la coleccion de datos para el autocomplete
        public static AutoCompleteStringCollection Autocomplete()
        {
            DataTable dt = Datos();

            AutoCompleteStringCollection coleccion = new AutoCompleteStringCollection();
            //recorrer y cargar los items para el autocompletado
            foreach (DataRow row in dt.Rows)
            {
                coleccion.Add(Convert.ToString(row["descripcio"]));
            }

            return coleccion;
        }

        //metodo para cargar la coleccion de datos para el autocomplete
        public static AutoCompleteStringCollection AutocompleteCodigo()
        {
            DataTable dt = Datos();

            AutoCompleteStringCollection coleccion = new AutoCompleteStringCollection();
            //recorrer y cargar los items para el autocompletado
            foreach (DataRow row in dt.Rows)
            {
                coleccion.Add(Convert.ToString(row["codigo"]));
            }

            return coleccion;
        }

        //AUTOCOMPLE PARA CLIENTES

        //metodo para cargar los datos de la bd
        public static DataTable DatosClientes()
        {
            DataTable dt = new DataTable();

            clsProcesos con = new clsProcesos();
            string constring = con.conectar();
            MySqlConnection objCon = new MySqlConnection();
            objCon.ConnectionString = constring;
            objCon.Open();
            string consulta = "SELECT * FROM empresas"; //consulta a la tabla paises
            MySqlCommand comando = new MySqlCommand(consulta, objCon);

            MySqlDataAdapter adap = new MySqlDataAdapter(comando);

            adap.Fill(dt);
            return dt;
        }

        //metodo para cargar la coleccion de datos para el autocomplete
        public static AutoCompleteStringCollection AutocompleteClientes()
        {
            DataTable dt = DatosClientes();

            AutoCompleteStringCollection coleccion = new AutoCompleteStringCollection();
            //recorrer y cargar los items para el autocompletado
            foreach (DataRow row in dt.Rows)
            {
                coleccion.Add(Convert.ToString(row["name_empre"]));
            }

            return coleccion;
        }

        //metodo para cargar la coleccion de datos para el autocomplete
        public static AutoCompleteStringCollection AutocompleteClientesCodigo()
        {
            DataTable dt = DatosClientes();

            AutoCompleteStringCollection coleccion = new AutoCompleteStringCollection();
            //recorrer y cargar los items para el autocompletado
            foreach (DataRow row in dt.Rows)
            {
                coleccion.Add(Convert.ToString(row["cod_empre"]));
            }

            return coleccion;
        }

        //Metodo que Retorna la Empresa por Default
        public static string DatosEmpresa()
        {
            string empresa;

            try
            {
                //realiza a Procesos
                clsProcesos conF = new clsProcesos();
                string constringF = conF.conectarConta();
                MySqlConnection objConF = new MySqlConnection();
                objConF.ConnectionString = constringF;
                objConF.Open();

                //Consulta datos de la empresa
                string sqlF;
                sqlF = "select nom_empre from empre_mo ";
                MySqlCommand mycmPR = new MySqlCommand();
                mycmPR.Connection = objConF;
                mycmPR.CommandText = sqlF;
                MySqlDataReader myreaderPR = mycmPR.ExecuteReader();

                myreaderPR.Read();
                empresa = myreaderPR.GetString(0).ToString();

                return empresa;
            }
            catch (MySql.Data.MySqlClient.MySqlException ex)
            {
                MessageBox.Show("Error : " + ex.Message, "Bancos", MessageBoxButtons.OK,
                    MessageBoxIcon.Information);
                clsExceptionLog.LogError(ex, false);
                return null;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error : " + ex.Message, "Bancos", MessageBoxButtons.OK,
                    MessageBoxIcon.Information);
                clsExceptionLog.LogError(ex, false);
                return null;
            }
            finally
            {

            }

        }


        //Turbi: metodo para cargar los datos de la bd sobrecargado
        public static DataTable DatosEmpresa(string tabla)
        {

            try
            {
                DataTable dt = new DataTable();
                clsProcesos con = new clsProcesos();
                string constring = con.conectarConta();
                MySqlConnection objCon = new MySqlConnection();
                objCon.ConnectionString = constring;
                objCon.Open();

                string consulta = "SELECT * FROM " + tabla + " "; //consulta a la tabla paises
                MySqlCommand comando = new MySqlCommand(consulta, objCon);

                MySqlDataAdapter adap = new MySqlDataAdapter(comando);

                //Turbi
                //dt.Locale = System.Globalization.CultureInfo.InvariantCulture;

                adap.Fill(dt);
                return dt;
            }
            catch (MySql.Data.MySqlClient.MySqlException ex)
            {
                MessageBox.Show("Error : " + ex.Message, "Bancos", MessageBoxButtons.OK,
                    MessageBoxIcon.Information);
                clsExceptionLog.LogError(ex, false);
                return null;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error : " + ex.Message, "Bancos", MessageBoxButtons.OK,
                    MessageBoxIcon.Information);
                clsExceptionLog.LogError(ex, false);
                return null;
            }
            finally
            {

            }
        }

        public static decimal CalculaEdad(DateTime dFechaNac)
        {
            decimal nEdad = 0;

            //Se calcula la Edad Actual A partir de la fecha actual Sustrayendo la fecha de nacimiento
            //esto devuelve un TimeSpan por tanto tomaremos los Dias y lo dividimos en 365 días
            nEdad = (DateTime.Now.Subtract(dFechaNac).Days / 365);
            return nEdad;
        }

        public static DataTable EntradaFijas()
        {
            try
            {
                DataTable dt = new DataTable();

                clsProcesos con = new clsProcesos();
                string constring = con.conectarConta();
                MySqlConnection objCon = new MySqlConnection();
                objCon.ConnectionString = constring;
                objCon.Open();
                string consulta = "SELECT * FROM EntradaFija";
                MySqlCommand comando = new MySqlCommand(consulta, objCon);

                MySqlDataAdapter adap = new MySqlDataAdapter(comando);

                adap.Fill(dt);
                return dt;

            }
            catch (MySql.Data.MySqlClient.MySqlException ex)
            {
                MessageBox.Show("Error : " + ex.Message, "Bancos", MessageBoxButtons.OK,
                    MessageBoxIcon.Information);
                clsExceptionLog.LogError(ex, false);
                return null;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error : " + ex.Message, "Bancos", MessageBoxButtons.OK,
                    MessageBoxIcon.Information);
                clsExceptionLog.LogError(ex, false);
                return null;
            }
            finally
            {

            }
        }

        //Funcion que busca los datos del Mayor General Detallado y Resumido
        public static DataTable MayorGeneral(string sqlstring)
        {
            try
            {

                DataTable dt = new DataTable();

                clsProcesos con = new clsProcesos();
                string constring = con.conectar();
                MySqlConnection objCon = new MySqlConnection();



                objCon.ConnectionString = constring;
                objCon.Open();
                //string consulta = sqlstring;
                //MySqlCommand comando = new MySqlCommand(consulta, objCon);

                //MySqlDataAdapter adap = new MySqlDataAdapter(comando);
                //adap.SelectCommand.CommandType = CommandType.StoredProcedure;

                //string cCadena = @"Server=localhost;Database=GsiSoft;Uid=root; Pwd=*010405;";

                MySqlDataAdapter oDataAdapter = new MySqlDataAdapter();
                oDataAdapter.SelectCommand = new MySqlCommand();
                oDataAdapter.SelectCommand.CommandText = sqlstring;
                oDataAdapter.SelectCommand.Connection = objCon;
                DataSet dsDatos = new DataSet();
                // oDataAdapter.Fill(dsDatos);
                //return dsDatos;

                oDataAdapter.Fill(dt);
                return dt;

            }
            catch (MySql.Data.MySqlClient.MySqlException ex)
            {
                MessageBox.Show("Error : " + ex.Message, "Bancos", MessageBoxButtons.OK,
                    MessageBoxIcon.Information);
                // ExceptionLog.LogError(ex, false);
                return null;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error : " + ex.Message, "Bancos", MessageBoxButtons.OK,
                    MessageBoxIcon.Information);
                clsExceptionLog.LogError(ex, false);
                return null;
            }
            finally
            {

            }


        }


        public static string NumeroALetras(string num)
        {
            string res, dec = "";
            Int64 entero;
            int decimales;
            double nro;

            try
            {
                nro = Convert.ToDouble(num);
            }
            catch
            {
                return "";
            }

            entero = Convert.ToInt64(Math.Truncate(nro));
            decimales = Convert.ToInt32(Math.Round((nro - entero) * 100, 2));

            //  if (decimales > 0)
            //  {
            dec = " PESOS CON " + decimales.ToString().Trim().PadLeft(2, '0') + "/100";
            //}

            res = NumeroALetras(Convert.ToDouble(entero)) + dec;
            return res;
        }

        // Luis turbi: Converte un mumero a letra
        private static string NumeroALetras(double value)
        {
            string Num2Text = "";
            value = Math.Truncate(value);

            if (value == 0) Num2Text = "CERO";
            else if (value == 1) Num2Text = "UNO";
            else if (value == 2) Num2Text = "DOS";
            else if (value == 3) Num2Text = "TRES";
            else if (value == 4) Num2Text = "CUATRO";
            else if (value == 5) Num2Text = "CINCO";
            else if (value == 6) Num2Text = "SEIS";
            else if (value == 7) Num2Text = "SIETE";
            else if (value == 8) Num2Text = "OCHO";
            else if (value == 9) Num2Text = "NUEVE";
            else if (value == 10) Num2Text = "DIEZ";
            else if (value == 11) Num2Text = "ONCE";
            else if (value == 12) Num2Text = "DOCE";
            else if (value == 13) Num2Text = "TRECE";
            else if (value == 14) Num2Text = "CATORCE";
            else if (value == 15) Num2Text = "QUINCE";
            else if (value < 20) Num2Text = "DIECI" + NumeroALetras(value - 10);
            else if (value == 20) Num2Text = "VEINTE";
            else if (value < 30) Num2Text = "VEINTI" + NumeroALetras(value - 20);
            else if (value == 30) Num2Text = "TREINTA";
            else if (value == 40) Num2Text = "CUARENTA";
            else if (value == 50) Num2Text = "CINCUENTA";
            else if (value == 60) Num2Text = "SESENTA";
            else if (value == 70) Num2Text = "SETENTA";
            else if (value == 80) Num2Text = "OCHENTA";
            else if (value == 90) Num2Text = "NOVENTA";

            else if (value < 100) Num2Text = NumeroALetras(Math.Truncate(value / 10) * 10) + " Y " + NumeroALetras(value % 10);
            else if (value == 100) Num2Text = "CIEN";
            else if (value < 200) Num2Text = "CIENTO " + NumeroALetras(value - 100);
            else if ((value == 200) || (value == 300) || (value == 400) || (value == 600) || (value == 800)) Num2Text = NumeroALetras(Math.Truncate(value / 100)) + "CIENTOS";

            else if (value == 500) Num2Text = "QUINIENTOS";
            else if (value == 700) Num2Text = "SETECIENTOS";
            else if (value == 900) Num2Text = "NOVECIENTOS";
            else if (value < 1000) Num2Text = NumeroALetras(Math.Truncate(value / 100) * 100) + " " + NumeroALetras(value % 100);
            else if (value == 1000) Num2Text = "MIL";
            else if (value < 2000) Num2Text = "MIL " + NumeroALetras(value % 1000);
            else if (value < 1000000)
            {
                Num2Text = NumeroALetras(Math.Truncate(value / 1000)) + " MIL";
                if ((value % 1000) > 0) Num2Text = Num2Text + " " + NumeroALetras(value % 1000);
            }

            else if (value == 1000000) Num2Text = "UN MILLON";
            else if (value < 2000000) Num2Text = "UN MILLON " + NumeroALetras(value % 1000000);
            else if (value < 1000000000000)
            {
                Num2Text = NumeroALetras(Math.Truncate(value / 1000000)) + " MILLONES ";
                if ((value - Math.Truncate(value / 1000000) * 1000000) > 0) Num2Text = Num2Text + " " + NumeroALetras(value - Math.Truncate(value / 1000000) * 1000000);
            }
            else if (value == 1000000000000) Num2Text = "UN BILLON";
            else if (value < 2000000000000) Num2Text = "UN BILLON " + NumeroALetras(value - Math.Truncate(value / 1000000000000) * 1000000000000);
            else
            {
                Num2Text = NumeroALetras(Math.Truncate(value / 1000000000000)) + " BILLONES";
                if ((value - Math.Truncate(value / 1000000000000) * 1000000000000) > 0) Num2Text = Num2Text + " " + NumeroALetras(value - Math.Truncate(value / 1000000000000) * 1000000000000);
            }

            return Num2Text;
        }

        public string conectarConta()
        {
            return clsConexion.ConectionString;

        }

        public string cadconta { get; set; }

        public string conectar()
        {

            return clsConexion.ConectionString;

            /*
            string srv = "";
            string pas = "";
            string baseDato = "";
            string CObdc = "";
            string user = "";

            //XmlTextReader myreader = new XmlTextReader("c:/gsisoft/Configuraciones.xml");
            //XmlNodeType type;
            //while (myreader.Read())
            //{

            //    type = myreader.NodeType;

            //    if (type == XmlNodeType.Element)
            //    {

            //        if (myreader.Name == "Servidor")
            //        {


            //            myreader.Read();

            //            srv = myreader.Value.ToString().Trim();

            //        }

            //        if (myreader.Name == "user")
            //        {


            //            myreader.Read();

            //            user = myreader.Value.ToString().Trim();

            //        }

            //        if (myreader.Name == "Id")
            //        {


            //            myreader.Read();

            //            pas = myreader.Value.ToString().Trim();

            //        }

            //        if (myreader.Name == "Data")
            //        {


            //            myreader.Read();

            //            baseDato = myreader.Value.ToString().Trim();

            //        }

            //        if (myreader.Name == "MySql")
            //        {


            //            myreader.Read();

            //            CObdc = myreader.Value.ToString().Trim();

            //        }

            //    }
            //}



            int coempre = 1;

            DataTable dtDsolicitud = Procesos.DatosGeneral("empre_mo", " Where  trim(cod_empre) = " + coempre, " ");
            if (dtDsolicitud.Rows.Count > 0)
            {
                foreach (DataRow registro in dtDsolicitud.Rows)
                {

                    pas = registro["pas"].ToString().Trim();
                    user = registro["user"].ToString().Trim();
                    srv = registro["servidor"].ToString().Trim();
                    CObdc = registro["MySql"].ToString().Trim();
                    baseDato = registro["basededato"].ToString().Trim();

                }

            }


            return cadconta = "Driver=" + CObdc + ";Server=" + srv + ";Database=" + baseDato + ";User='" + user + "';Password=" + pas + ";Option=3;";


            */

        }

        public void Valida_Keypress(object sender, KeyPressEventArgs e)
        {

            if (char.IsLetter(e.KeyChar))
            {
                MessageBox.Show("No se Permiten Letras", "Sistema de Gestion de Combustibles", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                e.Handled = false;

            }
            //else if (char.IsSeparator(e.KeyChar))
            //{
            //    MessageBox.Show("No se Permiten Espacios", "Sistema SysHospitalARD v1.0", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            //    e.Handled = false;
            //}

            else if (char.IsControl(e.KeyChar))
            {
                e.Handled = false;
            }
            else if (char.IsNumber(e.KeyChar))
            {
                e.Handled = false;
            }
            else if (char.IsPunctuation(e.KeyChar))
            {
                MessageBox.Show("No se Permiten Caracteres Especiales", "Sistema de Gestion de Combustible", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                e.Handled = false;
            }

            else
            {
                e.Handled = true;
            }

            if //(!(char.IsLetter(e.KeyChar)) && (!(char.IsPunctuation(e.KeyChar)) && (!(char.IsSeparator(e.KeyChar)) &&
                (!(char.IsNumber(e.KeyChar)) && (!(char.IsControl(e.KeyChar)) &&
                (e.KeyChar != (char)Keys.Back)))
            {

                e.Handled = true;

            }


        }

        public void NumerosyPunto(string miTextBox, object sender, KeyPressEventArgs e)
        {
            if (miTextBox.Contains('.'))
            {
                if (!char.IsDigit(e.KeyChar))
                {
                    e.Handled = true;
                }
                if (e.KeyChar == '\b')
                {
                    e.Handled = false;
                }
            }
            else
            {
                if (!char.IsDigit(e.KeyChar))
                {
                    e.Handled = true;
                }
                if (e.KeyChar == '.' || e.KeyChar == '\b')
                {
                    e.Handled = false;

                }
            }
        }

        public bool ValidaRangoFecha(DateTime dFechaInicial, DateTime dFechaFinal)
        {
            bool lReterno = false;
            if (dFechaInicial > dFechaFinal)
            {
                MessageBox.Show("Fecha Final no debe ser Menor que Fecha Inicial");

                lReterno = true;

            }
            return lReterno;
            //if (dtpFechaHasta.Value < dtpFechadesde.Value)
            //{
            //    MessageBox.Show("Fecha Inicial No puede ser Mayor que Fecha Final");
            //}
        }

        static class Globales
        {


            public static string Empresa = clsProcesos.DatosEmpresa();

        }
    }
}
