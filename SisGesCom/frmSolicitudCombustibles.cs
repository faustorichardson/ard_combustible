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
            dt.Columns.Add("Id", typeof(int));
            dt.Columns.Add("Combustible", typeof(string));
            dt.Columns.Add("Cantidad", typeof(int));                       

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
            this.dgview.Rows.Clear();
            this.dgview.Refresh();
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
                    break;

                case "Buscar":
                    this.btnNuevo.Enabled = true;
                    this.btnGrabar.Enabled = false;
                    this.btnEditar.Enabled = true;
                    this.btnBuscar.Enabled = true;
                    this.btnImprimir.Enabled = false;
                    this.btnEliminar.Enabled = false;
                    this.btnCancelar.Enabled = false;
                    this.btnSalir.Enabled = true;
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
            else
            {
                if (cModo == "Nuevo")
                {
                    // Agrego la data a la tabla secuencia_solicitudcombustible
                    try
                    {
                        // Step 1 - Stablishing the connection
                        MySqlConnection MyConexion = new MySqlConnection(clsConexion.ConectionString);

                        // Step 2 - Crear el comando de ejecucion
                        MySqlCommand myCommand = MyConexion.CreateCommand();

                        // Step 3 - Comando a ejecutar                        
                        myCommand.CommandText = "INSERT INTO secuencia_solicitudcombustible(fecha, nota) values(@fecha, @nota)";
                        myCommand.Parameters.AddWithValue("@fecha", dtFecha.Value.ToString("yyyy-MM-dd HH:mm:ss"));
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
                                    myCommand.Parameters.AddWithValue("@fecha", dtFecha.Value.ToString("yyyy-MM-dd HH:mm:ss"));
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

                }

                // LIMPIO LOS CAMPOS Y CAMBIO EL MODO LUEGO DE HABER REGISTRADO O ACTUALIZADO EL RECORD
                this.cModo = "Inicio";
                this.Botones();
                this.Limpiar();
            }

            
        }

        private void btnEditar_Click(object sender, EventArgs e)
        {
            this.cModo = "Editar";
            this.Botones();
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {

        }

        private void btnImprimir_Click(object sender, EventArgs e)
        {

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
            dt.Rows.Add(cmbCombustible.SelectedValue, lblDescripcionCombustible.Text, txtCantidad.Text);
            dgview.DataSource = dt;
            this.LimpiaCampo();
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

      
    }
}
