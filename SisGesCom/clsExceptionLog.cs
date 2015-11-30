/*********************************************************
 *  Authoer: Luis Alberto Turbi Mella
 *  Fecha: 01-07-2013
 *  Notas: Rutina generica para manejo y registro de errores
 *  *******************************************************/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MySql.Data;
using MySql.Data.MySqlClient;
using MySql.Data.Common;
using MySql.Data.Types;
using System.Diagnostics;
using System.Xml;
using System.IO;
using System.Windows.Forms;
using System.Data;
//using VFPToolkit;


namespace SisGesCom
{
    public class clsExceptionLog
    {
        public static MySqlConnection Cn;
        public static MySqlDataReader Dr;
        public static MySqlCommand Cm;
        public static int secuencia;
        public static int cia;
        public static int numero;
        public static DateTime fecha;
        public static string message;
        public static string msgoleodbc = "";
        public static string source;
        public static string programa;
        public static string app;
        public static string apphelp = "";
        public static string hora = "";
        public static int linea = 0;
        public static string area = "";
        public static int clsConexion = 0;
        public static string parametro = "";
        public static string cUsuario = frmLogin.cUsuarioActual;
        public static string userwin = "";
        public static string pc = "";

        /// <summary>
        /// Log file name
        /// </summary>



        private static string logFile = "EventLog.xml";
        public static string LogFile
        {
            get
            { return logFile; }
            set
            { logFile = value; }
        }

        /// <summary>
        /// Application name
        /// </summary>
        private static string applicationName = "Sistema SysHospitalARD v1.0";
        public static string ApplicationName
        {
            get
            { return applicationName; }
            set
            { applicationName = value; }
        }

        public static void LogError(Exception ex, bool lShowError)
        {
            // If the log file doesn't exist, create it
            if (!File.Exists(LogFile))
            {
                CreateLogFile();
            }

            // Open the log file
            XmlDocument XmlDoc = new XmlDocument();
            XmlDoc.Load(LogFile);
            // Create a new Exception element
            XmlElement ExceptionEntry = XmlDoc.CreateElement("Exception");
            // Create a new DateTime element
            XmlElement DateTimeChild = XmlDoc.CreateElement("DateTime");
            DateTimeChild.InnerText = DateTime.Now.ToString();
            ExceptionEntry.AppendChild(DateTimeChild);
            // Create an Application element
            XmlElement ApplicationChild = XmlDoc.CreateElement("Application");
            ApplicationChild.InnerText = ApplicationName;
            ExceptionEntry.AppendChild(ApplicationChild);
            // Create a new Message child element
            XmlElement MessageChild = XmlDoc.CreateElement("Message");
            MessageChild.InnerText = ex.Message;
            ExceptionEntry.AppendChild(MessageChild);
            // Create a new Source child element
            XmlElement SourceChild = XmlDoc.CreateElement("Source");
            SourceChild.InnerText = ex.Source;
            ExceptionEntry.AppendChild(SourceChild);
            // Create a new TargetSite child element
            XmlElement TargetSiteChild = XmlDoc.CreateElement("TargetSite");
            TargetSiteChild.InnerText = ex.TargetSite.ToString();
            ExceptionEntry.AppendChild(TargetSiteChild);
            // Create a new Stacktrace child element
            XmlElement StackTraceChild = XmlDoc.CreateElement("StackTrace");
            StackTraceChild.InnerText = ex.StackTrace;
            ExceptionEntry.AppendChild(StackTraceChild);
            // Add the entire ExceptionEntry to the XML document
            XmlDoc.DocumentElement.AppendChild(ExceptionEntry);
            // Write out the updated XML file
            XmlTextWriter xtw = new XmlTextWriter(LogFile, null);
            xtw.Formatting = Formatting.Indented;
            XmlDoc.WriteContentTo(xtw);
            xtw.Close();

            // lleno la clase con los datos del error
            int nLineaNo = 0;   // Temporal
            int nErrorNo = 0;   // Temporal
            Llenar(nErrorNo, DateTime.Now, ex.Message, ex.Source, Convert.ToString(ex.TargetSite), ApplicationName,
                 "", DateTime.Now.ToShortTimeString(), nLineaNo, cUsuario, Environment.UserName, Environment.MachineName);
            // ex.HelpLink.ToString()
            // Inserto el error en la base de datos
            AgregarError(ex);

            // Despiega el error al usuario si llega el parametro verdadero
            if (lShowError == true)
            {
                ShowError(ex);
            }
        }

        /// <summary>
        /// Creates a new log file
        /// </summary>
        public static void CreateLogFile()
        {
            // Create an XmlTextWriter, specifying the name of the new XML log file
            XmlTextWriter xtw = new XmlTextWriter(LogFile, null);

            // Write the XML declaration at the top of the file
            xtw.WriteStartDocument();

            // Add a comment to the file indicating the date/time created
            xtw.WriteComment("Log file created: " + DateTime.Now);

            // Add an empty <EventLog> element
            xtw.WriteStartElement("EventLog");
            xtw.WriteEndElement();

            // Close the stream
            xtw.Close();
        }

        // Desplega el error al usuario final
        public static void ShowError(Exception ex)
        {
            MessageBox.Show("Mensaje de Error: " + ex.Message + "\n\n" +
                "Fuente / Source: " + ex.Source + "\n\n" +
                "TargetSite: " + ex.TargetSite + "\n\n" +
                "Pila de Llamadas: \n" + ex.StackTrace,
                "Sistema SysHospitalARD v1.0", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private static void Llenar(int nNumero, DateTime dFecha, string cMessage, string cSource,
       string cPrograma, string cApp, string cApphelp, string cHora, int nLinea, string cUsuario,
      string cUserwin, string cPC)
        {

            numero = nNumero;
            fecha = dFecha;
            message = cMessage;
            source = cSource;
            programa = cPrograma;
            app = cApp;
            apphelp = cApphelp;
            hora = cHora;
            linea = nLinea;
            //cUsuario = cUsuario;
            userwin = cUserwin;
            pc = cPC;

        }

        // Inserta el error en la base de datos
        public static void AgregarError(Exception e)
        {
            try
            {
                Cn = new MySqlConnection();
                Cn.ConnectionString = SisGesCom.clsConexion.ConectionString;
                Cm = new MySqlCommand();
                Cm.Connection = Cn;
                Cm.CommandText = "AgregarError";
                Cm.CommandType = CommandType.StoredProcedure;
                Cm.Parameters.Add(new MySqlParameter("@nNumero", MySqlDbType.Int32));
                Cm.Parameters["@nNumero"].Value = numero;
                Cm.Parameters.Add(new MySqlParameter("@dFecha", MySqlDbType.DateTime));
                Cm.Parameters["@dFecha"].Value = fecha;
                Cm.Parameters.Add(new MySqlParameter("@cMessage", MySqlDbType.VarChar));
                Cm.Parameters["@cMessage"].Value = message;
                Cm.Parameters.Add(new MySqlParameter("@cSource", MySqlDbType.VarChar));
                Cm.Parameters["@cSource"].Value = source;
                Cm.Parameters.Add(new MySqlParameter("@cPrograma", MySqlDbType.Text));
                Cm.Parameters["@cPrograma"].Value = programa;
                Cm.Parameters.Add(new MySqlParameter("@cApp", MySqlDbType.VarChar));
                Cm.Parameters["@cApp"].Value = app;
                Cm.Parameters.Add(new MySqlParameter("@cApphelp", MySqlDbType.VarChar));
                Cm.Parameters["@cApphelp"].Value = apphelp;
                Cm.Parameters.Add(new MySqlParameter("@cHora", MySqlDbType.VarChar));
                Cm.Parameters["@cHora"].Value = hora;
                Cm.Parameters.Add(new MySqlParameter("@nLinea", MySqlDbType.Int32));
                Cm.Parameters["@nLinea"].Value = linea;
                Cm.Parameters.Add(new MySqlParameter("@cUsuario", MySqlDbType.VarChar));
                Cm.Parameters["@cUsuario"].Value = cUsuario;
                Cm.Parameters.Add(new MySqlParameter("@cUserwin", MySqlDbType.VarChar));
                Cm.Parameters["@cUserwin"].Value = userwin;
                Cm.Parameters.Add(new MySqlParameter("@cPC", MySqlDbType.VarChar));
                Cm.Parameters["@cPC"].Value = pc;
                try
                {
                    Cn.Open();
                }
                catch (MySqlException oError)
                {
                    // lRetorno = false;
                    // MessageBox.Show("Error en consulta SQL");
                    //     foreach (MySqlError err in oError.Errors)
                    {
                        MessageBox.Show("Error en comando de Actualizacion SQL: " + oError.Message, "Sistema SysHospitalARD v1.0",
                                MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    // Registro el error sin desplegarlo
                    clsExceptionLog.LogError(oError, false);
                }
                Cm.ExecuteNonQuery();
            }
            catch (MySqlException oError)
            {
                // MessageBox.Show("Error en consulta SQL");
                //    foreach (SqlError err in oError.Errors)
                {
                    MessageBox.Show("Error en comando de Insercion SQL: " + oError.Message, ApplicationName,
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            Cn.Close();
        }

    }
}
