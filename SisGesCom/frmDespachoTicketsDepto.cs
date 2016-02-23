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
    public partial class frmDespachoTicketsDepto : frmBase
    {
        string cModo = "Inicio";
        int i;
        DataTable dt = new DataTable();
        int idComb = 0;
        int cantComb = 0;
        Int32 MyCant = 0;

        public frmDespachoTicketsDepto()
        {
            InitializeComponent();
        }

        private void frmDespachoTicketsDepto_Load(object sender, EventArgs e)
        {
            // GENERO EL DATATABLE
            this.dtGenerating();

            // LLENANDO EL COMBO
            this.fillCmbCombo();

            // UPDATING THE LABEL
            this.updatelbl();

            // LIMPIAR
            this.Limpiar();

            // LIMPIANDO EL TEXTO
            this.LimpiaCampo();

            // BOTONES
            this.cModo = "Inicio";
            this.Botones();
        }

        private void Botones()
        {
            switch (cModo)
            {
                case "Inicio":
                    this.btnNuevo.Enabled = true;
                    this.btnGrabar.Enabled = false;
                    this.btnEditar.Enabled = false;
                    this.btnBuscar.Enabled = false;
                    this.btnImprimir.Enabled = false;
                    this.btnEliminar.Enabled = false;
                    this.btnCancelar.Enabled = false;
                    this.btnSalir.Enabled = true;
                    this.btnAdiciona.Enabled = false;
                    this.btnEliminar.Enabled = false;
                    //this.btnUpdate.Enabled = false;
                    //
                    this.txtCantidad.Enabled = false;
                    this.cmbCombustible.Enabled = false;
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
                    this.btnAdiciona.Enabled = true;
                    this.btnEliminar.Enabled = true;
                    //this.btnUpdate.Enabled = true;
                    //
                    this.txtCantidad.Enabled = true;
                    this.cmbCombustible.Enabled = true;
                    break;

                case "Grabar":
                    this.btnNuevo.Enabled = true;
                    this.btnGrabar.Enabled = false;
                    this.btnEditar.Enabled = false;
                    this.btnBuscar.Enabled = false;
                    this.btnImprimir.Enabled = false;
                    this.btnEliminar.Enabled = false;
                    this.btnCancelar.Enabled = false;
                    this.btnSalir.Enabled = true;
                    this.btnAdiciona.Enabled = false;
                    this.btnEliminar.Enabled = false;
                    //this.btnUpdate.Enabled = false;
                    //
                    this.txtCantidad.Enabled = false;
                    this.cmbCombustible.Enabled = false;
                    break;

                case "Editar":
                    this.btnNuevo.Enabled = false;
                    this.btnGrabar.Enabled = true;
                    this.btnEditar.Enabled = false;
                    this.btnBuscar.Enabled = false;
                    this.btnImprimir.Enabled = false;
                    this.btnEliminar.Enabled = false;
                    this.btnCancelar.Enabled = true;
                    this.btnSalir.Enabled = true;
                    this.btnAdiciona.Enabled = true;
                    this.btnEliminar.Enabled = true;
                    //this.btnUpdate.Enabled = true;
                    //
                    this.txtCantidad.Enabled = false;
                    this.cmbCombustible.Enabled = false;
                    break;

                case "Buscar":
                    this.btnNuevo.Enabled = true;
                    this.btnGrabar.Enabled = false;
                    this.btnEditar.Enabled = false;
                    this.btnBuscar.Enabled = false;
                    this.btnImprimir.Enabled = false;
                    this.btnEliminar.Enabled = false;
                    this.btnCancelar.Enabled = false;
                    this.btnSalir.Enabled = true;
                    this.btnAdiciona.Enabled = false;
                    this.btnEliminar.Enabled = false;
                    //this.btnUpdate.Enabled = false;
                    //
                    this.txtCantidad.Enabled = false;
                    this.cmbCombustible.Enabled = false;
                    break;

                case "Eliminar":
                    break;

                case "Cancelar":
                    this.btnNuevo.Enabled = true;
                    this.btnGrabar.Enabled = false;
                    this.btnEditar.Enabled = false;
                    this.btnBuscar.Enabled = false;
                    this.btnImprimir.Enabled = false;
                    this.btnEliminar.Enabled = false;
                    this.btnCancelar.Enabled = false;
                    this.btnSalir.Enabled = true;
                    break;

                default:
                    break;
            }
        }

        private void LimpiaCampo()
        {
            this.txtCantidad.Clear();            
            this.cmbCombustible.Focus();
        }

        private void Limpiar()
        {
            this.txtCantidad.Clear();
            this.dt.Clear();
            //this.dtGenerating();
            this.cantComb = 0;
            this.idComb = 0;
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
                MyCommand.CommandText = "SELECT id, departamento FROM dependencias WHERE id = " + cmbCombustible.SelectedValue + "";

                // Step 4 - connection open
                MyConexion.Open();

                // Step 5 - Creating the DataReader                    
                MySqlDataReader MyReader = MyCommand.ExecuteReader();

                // Step 6 - Verifying if Reader has rows
                if (MyReader.HasRows)
                {
                    while (MyReader.Read())
                    {
                        lblDescripcionCombustible.Text = MyReader["departamento"].ToString();
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

        private void dtGenerating()
        {
            try
            {
                // Creando el Datatable
                dt.Columns.Add("Id", typeof(int));
                dt.Columns.Add("Dependencia", typeof(string));
                dt.Columns.Add("Cantidad", typeof(Int32));
            }
            catch (Exception myEx)
            {
                MessageBox.Show(myEx.Message);
                throw;
            }
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
                MySqlCommand MyCommand = new MySqlCommand("SELECT id, departamento FROM dependencias ORDER BY departamento ASC", MyConexion);

                // Step 4
                MySqlDataReader MyReader;
                MyReader = MyCommand.ExecuteReader();

                // Step 5
                DataTable MyDataTable = new DataTable();
                MyDataTable.Columns.Add("id", typeof(int));
                MyDataTable.Columns.Add("departamento", typeof(string));
                MyDataTable.Load(MyReader);

                // Step 6
                cmbCombustible.ValueMember = "id";
                cmbCombustible.DisplayMember = "departamento";
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

        private void btnAdiciona_Click(object sender, EventArgs e)
        {
            if (txtCantidad.Text == "")
            {
                MessageBox.Show("No se puede agregar informacion sin cantidad");
                txtCantidad.Focus();
            }
            else
            {
                // Agrego la informacion al Grid
                dt.Rows.Add(cmbCombustible.SelectedValue, lblDescripcionCombustible.Text, Convert.ToInt32(txtCantidad.Text));
                dgview.DataSource = dt;
                this.LimpiaCampo();
            }
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

        private void btnNuevo_Click(object sender, EventArgs e)
        {            
            this.Limpiar();
            this.cModo = "Nuevo";
            this.Botones();            
        }

        private void cmbCombustible_Leave(object sender, EventArgs e)
        {
            this.updatelbl();
        }

        private void btnGrabar_Click(object sender, EventArgs e)
        {
            if (cModo == "Nuevo")
            {                

                // PASO 1 - Agrego la data a la tabla Movimiento Combustible
                try
                {
                    foreach (DataGridViewRow row in dgview.Rows)
                    {
                        MySqlConnection myConexion = new MySqlConnection(clsConexion.ConectionString);

                        {
                            this.verificaExistencia();
                            if (MyCant > Convert.ToInt32(row.Cells["cantidad"].Value))
                            {
                                using (MySqlCommand myCommand = new MySqlCommand("INSERT INTO movimientoticketsdepto(id, beneficiario, " +
                                "cantidad, fecha) VALUES(@id, @beneficiario, @cantidad, @fecha)", myConexion))
                                {
                                    myCommand.Parameters.AddWithValue("@id", row.Cells["Id"].Value);
                                    myCommand.Parameters.AddWithValue("@beneficiario", row.Cells["Dependencia"].Value);
                                    myCommand.Parameters.AddWithValue("@cantidad", row.Cells["Cantidad"].Value);
                                    myCommand.Parameters.AddWithValue("@fecha", dtFecha.Value.ToString("yyyy-MM-dd"));
                                    // Abro Conexion
                                    myConexion.Open();
                                    // Ejecuto Valores
                                    myCommand.ExecuteNonQuery();
                                    // Actualizo inventario
                                    this.idComb = Convert.ToInt32(row.Cells["Id"].Value);
                                    this.cantComb = Convert.ToInt32(row.Cells["Cantidad"].Value);
                                    this.updateExistencia();
                                    // Cierro Conexion
                                    myConexion.Close();

                                    //this.idComb = 0;
                                    //this.cantComb = 0;
                                }
                            }
                            else
                            {
                                MessageBox.Show("La cantidad es mayor que la existencia...");
                                break;
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

                // LIMPIO LOS CAMPOS Y CAMBIO EL MODO LUEGO DE HABER REGISTRADO O ACTUALIZADO EL RECORD
                this.cModo = "Inicio";
                this.Botones();
                this.Limpiar();
            }
            else
            {
                // PARA CUANDO VAYA A GRABAR
            }
        }

        private void verificaExistencia()
        {
            try
            {
                // Step 1 - Conexion
                MySqlConnection MyConexion = new MySqlConnection(clsConexion.ConectionString);

                // Step 2 
                MySqlCommand MyCommand = MyConexion.CreateCommand();

                // Step 3
                MyCommand.CommandText = "SELECT cantidad FROM existencia WHERE tipocombustible = 1000";

                // Step 4
                MyConexion.Open();

                // Step 5
                MyCant = Convert.ToInt32(MyCommand.ExecuteScalar());

                // Step 6
                //if (MyCant > cantComb)
                //{
                //    this.cantComb = MyCant - this.cantComb;
                //}
                //else
                //{
                //    MessageBox.Show("La cantidad a despachar es mayor que la existencia...");
                //}

                // Step 7
                MyConexion.Close();
            }
            catch (Exception myEx)
            {
                MessageBox.Show(myEx.Message);
                throw;
            }
        }

        private void updateExistencia()
        {
            // PASO 1 - Busco la cantidad actual
            try
            {
                // Step 1 - Conexion
                MySqlConnection MyConexion = new MySqlConnection(clsConexion.ConectionString);

                // Step 2 
                MySqlCommand MyCommand = MyConexion.CreateCommand();

                // Step 3
                MyCommand.CommandText = "SELECT cantidad FROM existencia WHERE tipocombustible = 1000";

                // Step 4
                MyConexion.Open();

                // Step 5
                Int32 MyCant = Convert.ToInt32(MyCommand.ExecuteScalar());

                // Step 6
                if (MyCant > cantComb)
                {
                    this.cantComb = MyCant - this.cantComb;
                }
                else
                {
                    MessageBox.Show("La cantidad a despachar es mayor que la existencia...");
                }

                // Step 7
                MyConexion.Close();
            }
            catch (Exception myEx)
            {
                MessageBox.Show(myEx.Message);
                throw;
            }

            // PASO 2 - Actualizo
            try
            {
                // Step 1 - Stablishing the connection
                MySqlConnection MyConexion = new MySqlConnection(clsConexion.ConectionString);

                // Step 2 - Crear el comando de ejecucion
                MySqlCommand myCommand = MyConexion.CreateCommand();

                // Step 3 - Comando a ejecutar
                myCommand.CommandText = "UPDATE existencia SET cantidad = @cantidad WHERE tipocombustible = @tipocombustible";
                myCommand.Parameters.AddWithValue("@tipocombustible", 1000);
                myCommand.Parameters.AddWithValue("@cantidad", cantComb);

                // Step 4 - Opening the connection
                MyConexion.Open();

                // Step 5 - Executing the query
                myCommand.ExecuteNonQuery();

                // Step 6 - Closing the connection
                MyConexion.Close();

                // MessageBox.Show("Informacion EXISTENCIA actualizada satisfactoriamente...");
            }
            catch (Exception myEx)
            {
                MessageBox.Show(myEx.Message);
                throw;
            }
        }


        private void btnEditar_Click(object sender, EventArgs e)
        {

        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {

        }

        private void btnImprimir_Click(object sender, EventArgs e)
        {
            
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            //if (txtCantidad.Text != "")
            //{
            //    DialogResult Result =
            //    MessageBox.Show("Se perderan Los cambios Realizados" + System.Environment.NewLine + "Desea Guardar los Cambios", "Sistema Gestion de Combustibles", MessageBoxButtons.YesNo,
            //            MessageBoxIcon.Question);
            //    switch (Result)
            //    {
            //        case DialogResult.Yes:
            //            cModo = "Actualiza";
            //            btnGrabar_Click(sender, e);
            //            break;
            //    }
            //}

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
            if (e.KeyChar >= '0' && e.KeyChar <= '9')
            {
                e.Handled = false;
            }
            else
            {
                e.Handled = true;
            }
        }
    }
}
