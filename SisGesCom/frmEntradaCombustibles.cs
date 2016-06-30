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
    public partial class frmEntradaCombustibles : frmBase
    {

        string cModo = "Inicio";
        int i;
        DataTable dt = new DataTable();
        int idComb = 0;
        int cantComb = 0;

        public frmEntradaCombustibles()
        {
            InitializeComponent();
        }

        private void frmEntradaCombustibles_Load(object sender, EventArgs e)
        {
            // Creando el Datatable
            this.dtGenerating();

            // Llenando el combo de tipo de combustibles
            this.fillCmbComb();

            // Llenando el combo de suplidores
            this.fillCmbSuplidor();

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

        private void fillCmbSuplidor()
        {
            try
            {
                // Step 1 
                MySqlConnection MyConexion = new MySqlConnection(clsConexion.ConectionString);

                // Step 2
                MyConexion.Open();

                // Step 3
                MySqlCommand MyCommand = new MySqlCommand("SELECT id_suplidor, suplidor FROM suplidores ORDER BY suplidor ASC", MyConexion);

                // Step 4
                MySqlDataReader MyReader;
                MyReader = MyCommand.ExecuteReader();

                // Step 5
                DataTable MyDataTable = new DataTable();
                MyDataTable.Columns.Add("id_suplidor", typeof(int));
                MyDataTable.Columns.Add("suplidor", typeof(string));
                MyDataTable.Load(MyReader);

                // Step 6
                cmbSuplidor.ValueMember = "id_suplidor";
                cmbSuplidor.DisplayMember = "suplidor";
                cmbSuplidor.DataSource = MyDataTable;

                // Step 7
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
                dt.Columns.Add("Combustible", typeof(string));
                dt.Columns.Add("Cantidad", typeof(Int32));
            }
            catch (Exception myEx)
            {
                MessageBox.Show(myEx.Message);
                throw;
            }
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

        private void LimpiaCampo()
        {
            this.txtCantidad.Clear();
            this.cmbCombustible.Focus();
        }

        private void fillCmbComb()
        {
            try
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
            catch (Exception myEx)
            {
                MessageBox.Show(myEx.Message);
                throw;
            }
        }

        private void Limpiar()
        {
            this.txtSolicitud.Clear();
            this.txtNota.Clear();
            this.txtCantidad.Clear();
            this.txtEntrada.Clear();
            //this.dgview.Rows.Clear();
            //this.dgview.Refresh();
            //this.dgview.Rows.Clear();
            this.dt.Clear();
            //this.dtGenerating();
            this.cantComb = 0;
            this.idComb = 0;            
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
                MyCommand.CommandText = "SELECT count(*) FROM combustible_entrada";

                // Step 4 - Open connection
                MyConexion.Open();

                // Step 5 - Execute the SQL Statement y Asigno el valor resultante a la variable "codigo"
                int codigo;
                codigo = Convert.ToInt32(MyCommand.ExecuteScalar());
                codigo = codigo + 1;
                txtEntrada.Text = Convert.ToString(codigo);
                cmbCombustible.Focus();

                // Step 5 - Close the connection
                MyConexion.Close();
            }
            catch (MySqlException MyEx)
            {
                MessageBox.Show(MyEx.Message);
            }
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
                    this.btnAdiciona.Enabled = false;
                    //this.btnUpdate.Enabled = false;
                    //
                    this.txtSolicitud.Enabled = false;
                    this.dtFecha.Enabled = false;
                    this.txtCantidad.Enabled = false;
                    this.txtNota.Enabled = false;
                    this.dgview.Enabled = false;
                    this.cmbCombustible.Enabled = false;
                    this.txtEntrada.Enabled = true;
                    this.cmbSuplidor.Enabled = false;
                    break;

                case "Nuevo":
                    this.btnNuevo.Enabled = false;
                    this.btnGrabar.Enabled = true;
                    this.btnEditar.Enabled = false;
                    this.btnBuscar.Enabled = false;
                    this.btnImprimir.Enabled = false;
                    this.btnEliminar.Enabled = true;
                    this.btnCancelar.Enabled = true;
                    this.btnSalir.Enabled = true;
                    this.btnAdiciona.Enabled = true;
                    //this.btnUpdate.Enabled = true;
                    //
                    this.txtSolicitud.Enabled = true;
                    this.dtFecha.Enabled = true;
                    this.txtCantidad.Enabled = true;
                    this.txtNota.Enabled = true;
                    this.dgview.Enabled = true;
                    this.cmbCombustible.Enabled = true;
                    this.txtEntrada.Enabled = false;
                    this.cmbSuplidor.Enabled = true;
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
                    this.btnAdiciona.Enabled = false;
                    //this.btnUpdate.Enabled = false;
                    //
                    this.txtSolicitud.Enabled = true;
                    this.dtFecha.Enabled = false;
                    this.txtCantidad.Enabled = false;
                    this.txtNota.Enabled = false;
                    this.dgview.Enabled = false;
                    this.cmbCombustible.Enabled = false;
                    this.txtEntrada.Enabled = false;
                    this.cmbSuplidor.Enabled = false;
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
                    this.btnAdiciona.Enabled = true;
                    //this.btnUpdate.Enabled = true;
                    //
                    this.txtSolicitud.Enabled = false;
                    this.dtFecha.Enabled = true;
                    this.txtCantidad.Enabled = true;
                    this.txtNota.Enabled = true;
                    this.dgview.Enabled = true;
                    this.cmbCombustible.Enabled = true;
                    this.txtEntrada.Enabled = false;
                    this.cmbSuplidor.Enabled = true;
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
                    this.btnAdiciona.Enabled = false;
                    //this.btnUpdate.Enabled = false;
                    //
                    this.txtSolicitud.Enabled = true;
                    this.dtFecha.Enabled = false;
                    this.txtCantidad.Enabled = false;
                    this.txtNota.Enabled = false;
                    this.dgview.Enabled = false;
                    this.cmbCombustible.Enabled = false;
                    this.txtEntrada.Enabled = true;
                    this.cmbSuplidor.Enabled = false;
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

        private void btnAdiciona_Click(object sender, EventArgs e)
        {
            if (txtCantidad.Text == "")
            {
                MessageBox.Show("No se puede agregar informacion sin cantidad...");
                txtCantidad.Focus();
            }
            else
            {
                // Step 1 - Conexion
                MySqlConnection MyConexion = new MySqlConnection(clsConexion.ConectionString);

                // Step 2 - creating the command object
                MySqlCommand MyCommand = MyConexion.CreateCommand();

                // Step 3 - creating the commandtext
                MyCommand.CommandText = "SELECT id, tipo_combustible FROM solicitud " +
                    "WHERE id = " + txtSolicitud.Text + " AND tipo_combustible = " + cmbCombustible.SelectedValue + " AND anulada = 0";

                // Step 4 - connection open
                MyConexion.Open();

                // Step 5 - Creating the DataReader                    
                MySqlDataReader MyReader = MyCommand.ExecuteReader();

                // Step 6 - Verifying if Reader has rows
                if (MyReader.HasRows)
                {
                    // Agrego la informacion al Grid
                    dt.Rows.Add(cmbCombustible.SelectedValue, lblDescripcionCombustible.Text, Convert.ToInt32(txtCantidad.Text));
                    dgview.DataSource = dt;
                    this.LimpiaCampo();                    
                }
                else
                {
                    MessageBox.Show("No se encontraron registros de este tipo combustible en esta solicitud...");
                    //this.cModo = "Inicio";
                    //this.Botones();
                    this.LimpiaCampo();
                    this.cmbCombustible.Focus();
                }

                // Step 6 - Closing all
                MyReader.Close();
                MyCommand.Dispose();
                MyConexion.Close();
                
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

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            //DataGridViewRow row = dgview.Rows[i];
            //row.Cells[0].Value = cmbCombustible.SelectedValue;
            //row.Cells[1].Value = lblDescripcionCombustible.Text;
            //row.Cells[2].Value = txtCantidad.Text;
        }

        private void btnNuevo_Click(object sender, EventArgs e)
        {
            cModo = "Nuevo";
            this.Limpiar();
            this.Botones();
            this.ProximoCodigo();  
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
                MyCommand.CommandText = "SELECT cantidad FROM existencia WHERE tipocombustible = " + this.idComb + "";

                // Step 4
                MyConexion.Open();

                // Step 5
                Int32 MyCant = Convert.ToInt32(MyCommand.ExecuteScalar());

                // Step 6
                this.cantComb = this.cantComb + MyCant;

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
                myCommand.Parameters.AddWithValue("@tipocombustible", idComb);
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

        private void btnGrabar_Click(object sender, EventArgs e)
        {
            
            if (txtEntrada.Text == "")
            {
                MessageBox.Show("No se puede grabar sin un numero de solicitud ...");
                txtEntrada.Focus();
            }
            else if (dgview.Rows.Count < 1)
            {
                MessageBox.Show("No se puede grabar un registro sin productos agregados...");
                cmbCombustible.Focus();
            }
            else if (txtNota.Text == "")
            {
                MessageBox.Show("Debe de agregar una nota a esta solicitud...");
                txtNota.Focus();                
            }
            else if (txtSolicitud.Text == "")
            {
                MessageBox.Show("Debe de agregar el numero de solicitud para esta entrada...");
                txtSolicitud.Focus();
            }
            else
            {
                if (cModo == "Nuevo")
                {
                    // verifico el codigo nuevamente
                    this.ProximoCodigo();
            
                    // PASO 1 - Agrego la data a la tabla combustible_entrada
                    try
                    {
                        // Step 1 - Stablishing the connection
                        MySqlConnection MyConexion = new MySqlConnection(clsConexion.ConectionString);

                        // Step 2 - Crear el comando de ejecucion
                        MySqlCommand myCommand = MyConexion.CreateCommand();

                        // Step 3 - Comando a ejecutar                        
                        myCommand.CommandText = "INSERT INTO combustible_entrada(fecha, nota, id_solicitud, suplidor) values(@fecha, @nota, @solicitud, @suplidor)";
                        myCommand.Parameters.AddWithValue("@fecha", dtFecha.Value.ToString("yyyy-MM-dd"));
                        myCommand.Parameters.AddWithValue("@nota", txtNota.Text);
                        myCommand.Parameters.AddWithValue("@solicitud", txtSolicitud.Text);
                        myCommand.Parameters.AddWithValue("@suplidor", cmbSuplidor.SelectedValue);

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

                    // PASO 2 - Agrego la data a la tabla Movimiento Combustible
                    try
                    {
                        foreach (DataGridViewRow row in dgview.Rows)
                        {
                            MySqlConnection myConexion = new MySqlConnection(clsConexion.ConectionString);

                            {
                                using (MySqlCommand myCommand = new MySqlCommand("INSERT INTO movimientocombustible(id, fecha, "+
                                    "tipo_combustible, descripcion_combustible, cantidad, tipo_movimiento, operaciones) "+
                                    "VALUES(@id, @fecha, @tipo_combustible, @descripcion_combustible, @cantidad, @tipo_movimiento, @operaciones)", myConexion))
                                {
                                    myCommand.Parameters.AddWithValue("@id", txtEntrada.Text);
                                    myCommand.Parameters.AddWithValue("@fecha", dtFecha.Value.ToString("yyyy-MM-dd"));
                                    myCommand.Parameters.AddWithValue("@tipo_combustible", row.Cells["Id"].Value);
                                    myCommand.Parameters.AddWithValue("@descripcion_combustible", row.Cells["Combustible"].Value);
                                    myCommand.Parameters.AddWithValue("@cantidad", row.Cells["Cantidad"].Value);
                                    myCommand.Parameters.AddWithValue("@tipo_movimiento", "E");
                                    if (rbTerrestres.Checked == true)
                                    {
                                        myCommand.Parameters.AddWithValue("@operaciones", "T");
                                    }
                                    else if (rbMaritimas.Checked == true)
                                    {
                                        myCommand.Parameters.AddWithValue("@operaciones", "M");
                                    }
                                    else
                                    {
                                        myCommand.Parameters.AddWithValue("@operaciones", "G");
                                    }

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
                    // Actualizo la data a la tabla entrada de combustible
                    try
                    {
                        // Step 1 - Stablishing the connection
                        MySqlConnection MyConexion = new MySqlConnection(clsConexion.ConectionString);

                        // Step 2 - Crear el comando de ejecucion
                        MySqlCommand myCommand = MyConexion.CreateCommand();

                        // Step 3 - Comando a ejecutar                        
                        myCommand.CommandText = "UPDATE combustible_entrada SET fecha = @fecha, nota = @nota, id_solicitud = @solicitud," +
                            " suplidor = @suplidor WHERE id = " + txtSolicitud.Text + "";
                        myCommand.Parameters.AddWithValue("@fecha", dtFecha.Value.ToString("yyyy-MM-dd HH:mm:ss"));
                        myCommand.Parameters.AddWithValue("@nota", txtNota.Text);
                        myCommand.Parameters.AddWithValue("@solicitud", txtSolicitud.Text);
                        myCommand.Parameters.AddWithValue("@suplidor", cmbSuplidor.SelectedValue);

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

                    // Borro los datos de la tabla solicitud que tienen que ver con este ID
                    //try
                    //{
                    //    // Step 1 - Stablishing the connection
                    //    MySqlConnection MyConexion = new MySqlConnection(clsConexion.ConectionString);

                    //    // Step 2 - Crear el comando de ejecucion
                    //    MySqlCommand myCommand = MyConexion.CreateCommand();

                    //    // Step 3 - Comando a ejecutar                        
                    //    myCommand.CommandText = "DELETE FROM solicitud WHERE id = @id";
                    //    myCommand.Parameters.AddWithValue("@id", txtSolicitud.Text);

                    //    // Step 4 - Opening the connection
                    //    MyConexion.Open();

                    //    // Step 5 - Executing the query
                    //    myCommand.ExecuteNonQuery();

                    //    // Step 6 - Closing the connection
                    //    MyConexion.Close();
                    //}
                    //catch (Exception myEx)
                    //{
                    //    MessageBox.Show(myEx.Message);
                    //    throw;
                    //}

                    

                    // Agrego la data nuevamente a la tabla Solicitud
                    //try
                    //{
                    //    foreach (DataGridViewRow row in dgview.Rows)
                    //    {
                    //        MySqlConnection myConexion = new MySqlConnection(clsConexion.ConectionString);

                    //        {
                    //            using (MySqlCommand myCommand = new MySqlCommand("INSERT INTO solicitud(id, fecha, tipo_combustible, descripcion_combustible, cantidad)" +
                    //                "VALUES(@id, @fecha, @tipo_combustible, @descripcion_combustible, @cantidad)", myConexion))
                    //            {
                    //                myCommand.Parameters.AddWithValue("@id", txtSolicitud.Text);
                    //                myCommand.Parameters.AddWithValue("@fecha", dtFecha.Value.ToString("yyyy-MM-dd HH:mm:ss"));
                    //                myCommand.Parameters.AddWithValue("@tipo_combustible", row.Cells["Id"].Value);
                    //                myCommand.Parameters.AddWithValue("@descripcion_combustible", row.Cells["Combustible"].Value);
                    //                myCommand.Parameters.AddWithValue("@cantidad", row.Cells["Cantidad"].Value);
                    //                // Abro Conexion
                    //                myConexion.Open();
                    //                // Ejecuto Valores
                    //                myCommand.ExecuteNonQuery();
                    //                // Cierro Conexion
                    //                myConexion.Close();

                    //            }
                    //        }
                    //    }
                    //    //MessageBox.Show("Records inserted.");
                    //}
                    //catch (Exception myEx)
                    //{
                    //    MessageBox.Show(myEx.Message);
                    //    throw;
                    //}


                }

                // LIMPIO LOS CAMPOS Y CAMBIO EL MODO LUEGO DE HABER REGISTRADO O ACTUALIZADO EL RECORD
                this.cModo = "Inicio";
                this.Botones();
                this.Limpiar();
            }

            // Pregunto si deseo imprimir
            this.ImprimeSolicitud();

            
        }

        private void ImprimeSolicitud()
        {
            DialogResult Result =
            MessageBox.Show("Imprima la Solicitud de Combustible" + System.Environment.NewLine + "Desea Imprimir la Solicitud de Combustible", "Sistema de Gestion de Combustibles", MessageBoxButtons.YesNo,
            MessageBoxIcon.Question);
                switch (Result)
                {
                    case DialogResult.Yes:
                        GenerarReporte();
                        break;
                }
        }

        private void GenerarReporte()
        {
            if (txtEntrada.Text == "")
            {
                MessageBox.Show("No se permite imprimir certificacion sin su debida numeracion...");
                txtCantidad.Focus();
            }
            else
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
                int cCodigo = Convert.ToInt32(txtEntrada.Text);

                try
                {
                    // Abro clsConexion
                    myclsConexion.Open();
                    // Creo comando
                    myCommand = myclsConexion.CreateCommand();
                    // Adhiero el comando a la clsConexion
                    myCommand.Connection = myclsConexion;
                    // Filtros de la busqueda
                    //int cCodigoImprimir = Convert.ToInt32(txtIdLicencia.Text);
                    cWhere = cWhere + " AND combustible_entrada.id =" + cCodigo + "";
                    sbQuery.Clear();
                    sbQuery.Append("SELECT combustible_entrada.id, combustible_entrada.fecha, combustible_entrada.nota, suplidores.suplidor");
                    // sbQuery.Append(" solicitud.cantidad, secuencia_solicitudcombustible.nota as nota, tipo_combustible.medida as medida");
                    //sbQuery.Append(" licenciasmedicas.razonlicencia, dependencias.nomdepart, seccionaval.nomsec,");
                    //sbQuery.Append(" concat(rtrim(doctores.doctores_nombre),' ', ltrim(doctores.doctores_apellido)) as nombredoctor,");
                    //sbQuery.Append(" rangos.rangoabrev as rangodoctor, especialidades.especialidades_descripcion as doctorespecialidad,");
                    //sbQuery.Append(" licenciasmedicas.idlicencia ");
                    sbQuery.Append(" FROM combustible_entrada");
                    sbQuery.Append(" INNER JOIN suplidores ON suplidores.id_suplidor = combustible_entrada.suplidor");
                    // sbQuery.Append(" INNER JOIN secuencia_solicitudcombustible ON secuencia_solicitudcombustible.id = solicitud.id");
                    //sbQuery.Append(" INNER JOIN seccionaval ON seccionaval.codsec = licenciasmedicas.seccionaval");
                    //sbQuery.Append(" INNER JOIN doctores ON doctores.doctores_cedula = licenciasmedicas.ceduladoctor");
                    //sbQuery.Append(" INNER JOIN rangos ON rangos.rango_id = doctores.doctores_rango");
                    //sbQuery.Append(" INNER JOIN especialidades ON especialidades.especialidades_id = doctores.doctores_especialidad");
                    sbQuery.Append(cWhere);

                    // Paso los valores de sbQuery al CommandText
                    myCommand.CommandText = sbQuery.ToString();
                    // Creo el objeto Data Adapter y ejecuto el command en el
                    myAdapter = new MySqlDataAdapter(myCommand);
                    // Creo el objeto Data Table
                    DataTable dtMovimientoCombustible = new DataTable();
                    // Lleno el data adapter
                    myAdapter.Fill(dtMovimientoCombustible);
                    // Cierro el objeto clsConexion
                    myclsConexion.Close();

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
                        cTitulo = "CERTIFICACIÓN";

                        //6to Instanciamos nuestro REPORTE
                        //Reportes.ListadoDoctores oListado = new Reportes.ListadoDoctores();
                        rptCertificacionEntradaCombustible orptCertificacionEntradaCombustible = new rptCertificacionEntradaCombustible();                        

                        //pasamos el nombre del TITULO del Listado
                        //SumaryInfo es un objeto que se utiliza para leer,crear y actualizar las propiedades del reporte
                        // oListado.SummaryInfo.ReportTitle = cTitulo;
                        orptCertificacionEntradaCombustible.SummaryInfo.ReportTitle = cTitulo;

                        //7mo. instanciamos nuestro el FORMULARIO donde esta nuestro ReportViewer
                        frmPrinter ofrmPrinter = new frmPrinter(dtMovimientoCombustible, orptCertificacionEntradaCombustible, cTitulo);

                        //ParameterFieldInfo Obtiene o establece la colección de campos de parámetros.
                        ofrmPrinter.CrystalReportViewer1.ParameterFieldInfo = oParametrosCR;
                        ofrmPrinter.ShowDialog();
                    }
                }
                catch (Exception myEx)
                {
                    MessageBox.Show("Error : " + myEx.Message, "Mostrando Reporte", MessageBoxButtons.OK,
                                        MessageBoxIcon.Information);
                    clsExceptionLog.LogError(myEx, false);
                    return;
                }
            }
        }

        private void btnEditar_Click(object sender, EventArgs e)
        {
            this.cModo = "Editar";
            this.Botones();
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            if (txtEntrada.Text == "")
            {
                MessageBox.Show("Debe de introducir el numero de referencia de entrada...");
                this.txtEntrada.Focus();
            }
            else
            {
                // BUSCANDO EN LA TABLA COMBUSTIBLE_ENTRADA
                try
                {
                    // Step 1 - Conexion
                    MySqlConnection MyConexion = new MySqlConnection(clsConexion.ConectionString);

                    // Step 2 - creating the command object
                    MySqlCommand MyCommand = MyConexion.CreateCommand();

                    // Step 3 - creating the commandtext
                    MyCommand.CommandText = "SELECT id, fecha, nota, id_solicitud, suplidor FROM combustible_entrada WHERE id = " + txtEntrada.Text + "";

                    // Step 4 - connection open
                    MyConexion.Open();

                    // Step 5 - Creating the DataReader                    
                    MySqlDataReader MyReader = MyCommand.ExecuteReader();

                    // Step 6 - Verifying if Reader has rows
                    if (MyReader.HasRows)
                    {
                        while (MyReader.Read())
                        {
                            txtSolicitud.Text = MyReader["id_solicitud"].ToString();
                            dtFecha.Value = Convert.ToDateTime(MyReader["fecha"]);
                            txtNota.Text = MyReader["nota"].ToString();
                            cmbSuplidor.SelectedValue = MyReader["suplidor"].ToString();
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
                        this.txtSolicitud.Focus();
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

                // BUSCANDO LA INFORMACION EN LA TABLA: COMBUSTIBLE_ENTRADA. PARA LUEGO LLENAR EL GRID
                if (txtEntrada.Text != "")
                {
                    try
                    {
                        // Establishing the MySQL Connection
                        MySqlConnection conn = new MySqlConnection(clsConexion.ConectionString);

                        // Open the connection to db
                        conn.Open();

                        // Creating the DataReader
                        MySqlDataAdapter myAdapter = new MySqlDataAdapter("SELECT tipo_combustible as Id, descripcion_combustible as Combustible," +
                            " cantidad as Cantidad FROM movimientocombustible WHERE id = " + txtEntrada.Text + " AND tipo_movimiento = 'E'", conn);

                        // Creating the Dataset
                        DataSet myDs = new System.Data.DataSet();

                        // Filling the data adapter
                        myAdapter.Fill(myDs, "Solicitud");

                        // Fill the Gridview
                        dgview.DataSource = myDs.Tables[0];

                        //this.cModo = "Buscar";
                        //this.Botones();

                    }
                    catch (Exception myEx)
                    {
                        MessageBox.Show(myEx.Message);
                        throw;
                    }
                }

            }
        }

        private void btnImprimir_Click(object sender, EventArgs e)
        {
            this.GenerarReporte();
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            if (txtEntrada.Text != "" || txtNota.Text != "")
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

        private void txtSolicitud_KeyPress(object sender, KeyPressEventArgs e)
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

        private void txtEntrada_KeyPress(object sender, KeyPressEventArgs e)
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
