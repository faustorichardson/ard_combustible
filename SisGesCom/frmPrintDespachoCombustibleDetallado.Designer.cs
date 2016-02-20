namespace SisGesCom
{
    partial class frmPrintDespachoCombustibleDetallado
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmPrintDespachoCombustibleDetallado));
            this.btnSalir = new System.Windows.Forms.Button();
            this.btnImprimir = new System.Windows.Forms.Button();
            this.dtHasta = new System.Windows.Forms.DateTimePicker();
            this.label2 = new System.Windows.Forms.Label();
            this.dtDesde = new System.Windows.Forms.DateTimePicker();
            this.label1 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.rbMaritimas = new System.Windows.Forms.RadioButton();
            this.rbTerrestres = new System.Windows.Forms.RadioButton();
            this.rbTodas = new System.Windows.Forms.RadioButton();
            this.chkDepartamentos = new System.Windows.Forms.CheckBox();
            this.label3 = new System.Windows.Forms.Label();
            this.cmbCombustible = new System.Windows.Forms.ComboBox();
            this.chkAnuladas = new System.Windows.Forms.CheckBox();
            this.SuspendLayout();
            // 
            // lblTituloForm
            // 
            this.lblTituloForm.Size = new System.Drawing.Size(425, 22);
            this.lblTituloForm.Text = "Genera Reporte Despacho Combustible (Detallado)";
            // 
            // btnSalir
            // 
            this.btnSalir.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSalir.Image = ((System.Drawing.Image)(resources.GetObject("btnSalir.Image")));
            this.btnSalir.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnSalir.Location = new System.Drawing.Point(425, 114);
            this.btnSalir.Name = "btnSalir";
            this.btnSalir.Size = new System.Drawing.Size(92, 45);
            this.btnSalir.TabIndex = 27;
            this.btnSalir.Text = "Salir";
            this.btnSalir.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnSalir.UseVisualStyleBackColor = true;
            this.btnSalir.Click += new System.EventHandler(this.btnSalir_Click);
            // 
            // btnImprimir
            // 
            this.btnImprimir.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnImprimir.Image = ((System.Drawing.Image)(resources.GetObject("btnImprimir.Image")));
            this.btnImprimir.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnImprimir.Location = new System.Drawing.Point(425, 54);
            this.btnImprimir.Name = "btnImprimir";
            this.btnImprimir.Size = new System.Drawing.Size(92, 45);
            this.btnImprimir.TabIndex = 26;
            this.btnImprimir.Text = "Generar";
            this.btnImprimir.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnImprimir.UseVisualStyleBackColor = true;
            this.btnImprimir.Click += new System.EventHandler(this.btnImprimir_Click);
            // 
            // dtHasta
            // 
            this.dtHasta.Location = new System.Drawing.Point(117, 188);
            this.dtHasta.Name = "dtHasta";
            this.dtHasta.Size = new System.Drawing.Size(215, 20);
            this.dtHasta.TabIndex = 24;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(58, 192);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(52, 13);
            this.label2.TabIndex = 25;
            this.label2.Text = "HASTA:";
            // 
            // dtDesde
            // 
            this.dtDesde.Location = new System.Drawing.Point(117, 152);
            this.dtDesde.Name = "dtDesde";
            this.dtDesde.Size = new System.Drawing.Size(215, 20);
            this.dtDesde.TabIndex = 22;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(58, 156);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(53, 13);
            this.label1.TabIndex = 23;
            this.label1.Text = "DESDE:";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(10, 119);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(99, 13);
            this.label5.TabIndex = 43;
            this.label5.Text = "OPERACIONES:";
            // 
            // rbMaritimas
            // 
            this.rbMaritimas.AutoSize = true;
            this.rbMaritimas.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rbMaritimas.Location = new System.Drawing.Point(271, 117);
            this.rbMaritimas.Name = "rbMaritimas";
            this.rbMaritimas.Size = new System.Drawing.Size(78, 17);
            this.rbMaritimas.TabIndex = 42;
            this.rbMaritimas.TabStop = true;
            this.rbMaritimas.Text = "Maritimas";
            this.rbMaritimas.UseVisualStyleBackColor = true;
            // 
            // rbTerrestres
            // 
            this.rbTerrestres.AutoSize = true;
            this.rbTerrestres.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rbTerrestres.Location = new System.Drawing.Point(183, 117);
            this.rbTerrestres.Name = "rbTerrestres";
            this.rbTerrestres.Size = new System.Drawing.Size(82, 17);
            this.rbTerrestres.TabIndex = 41;
            this.rbTerrestres.TabStop = true;
            this.rbTerrestres.Text = "Terrestres";
            this.rbTerrestres.UseVisualStyleBackColor = true;
            // 
            // rbTodas
            // 
            this.rbTodas.AutoSize = true;
            this.rbTodas.Checked = true;
            this.rbTodas.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rbTodas.Location = new System.Drawing.Point(117, 117);
            this.rbTodas.Name = "rbTodas";
            this.rbTodas.Size = new System.Drawing.Size(60, 17);
            this.rbTodas.TabIndex = 40;
            this.rbTodas.TabStop = true;
            this.rbTodas.Text = "Todas";
            this.rbTodas.UseVisualStyleBackColor = true;
            // 
            // chkDepartamentos
            // 
            this.chkDepartamentos.AutoSize = true;
            this.chkDepartamentos.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkDepartamentos.Location = new System.Drawing.Point(115, 54);
            this.chkDepartamentos.Name = "chkDepartamentos";
            this.chkDepartamentos.Size = new System.Drawing.Size(163, 17);
            this.chkDepartamentos.TabIndex = 39;
            this.chkDepartamentos.Text = "Filtrar por Departamento";
            this.chkDepartamentos.UseVisualStyleBackColor = true;
            this.chkDepartamentos.CheckedChanged += new System.EventHandler(this.chkDepartamentos_CheckedChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(10, 83);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(99, 13);
            this.label3.TabIndex = 38;
            this.label3.Text = "DEPENDENCIA:";
            // 
            // cmbCombustible
            // 
            this.cmbCombustible.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbCombustible.FormattingEnabled = true;
            this.cmbCombustible.Location = new System.Drawing.Point(115, 80);
            this.cmbCombustible.Name = "cmbCombustible";
            this.cmbCombustible.Size = new System.Drawing.Size(283, 21);
            this.cmbCombustible.TabIndex = 37;
            // 
            // chkAnuladas
            // 
            this.chkAnuladas.AutoSize = true;
            this.chkAnuladas.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkAnuladas.Location = new System.Drawing.Point(284, 54);
            this.chkAnuladas.Name = "chkAnuladas";
            this.chkAnuladas.Size = new System.Drawing.Size(78, 17);
            this.chkAnuladas.TabIndex = 44;
            this.chkAnuladas.Text = "Anuladas";
            this.chkAnuladas.UseVisualStyleBackColor = true;
            // 
            // frmPrintDespachoCombustibleDetallado
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(530, 229);
            this.Controls.Add(this.chkAnuladas);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.rbMaritimas);
            this.Controls.Add(this.rbTerrestres);
            this.Controls.Add(this.rbTodas);
            this.Controls.Add(this.chkDepartamentos);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.cmbCombustible);
            this.Controls.Add(this.btnSalir);
            this.Controls.Add(this.btnImprimir);
            this.Controls.Add(this.dtHasta);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.dtDesde);
            this.Controls.Add(this.label1);
            this.Name = "frmPrintDespachoCombustibleDetallado";
            this.Text = "frmDespachoCombustible";
            this.Load += new System.EventHandler(this.frmPrintDespachoCombustibleDetallado_Load);
            this.Controls.SetChildIndex(this.lblTituloForm, 0);
            this.Controls.SetChildIndex(this.label1, 0);
            this.Controls.SetChildIndex(this.dtDesde, 0);
            this.Controls.SetChildIndex(this.label2, 0);
            this.Controls.SetChildIndex(this.dtHasta, 0);
            this.Controls.SetChildIndex(this.btnImprimir, 0);
            this.Controls.SetChildIndex(this.btnSalir, 0);
            this.Controls.SetChildIndex(this.cmbCombustible, 0);
            this.Controls.SetChildIndex(this.label3, 0);
            this.Controls.SetChildIndex(this.chkDepartamentos, 0);
            this.Controls.SetChildIndex(this.rbTodas, 0);
            this.Controls.SetChildIndex(this.rbTerrestres, 0);
            this.Controls.SetChildIndex(this.rbMaritimas, 0);
            this.Controls.SetChildIndex(this.label5, 0);
            this.Controls.SetChildIndex(this.chkAnuladas, 0);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnSalir;
        private System.Windows.Forms.Button btnImprimir;
        private System.Windows.Forms.DateTimePicker dtHasta;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.DateTimePicker dtDesde;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.RadioButton rbMaritimas;
        private System.Windows.Forms.RadioButton rbTerrestres;
        private System.Windows.Forms.RadioButton rbTodas;
        private System.Windows.Forms.CheckBox chkDepartamentos;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox cmbCombustible;
        private System.Windows.Forms.CheckBox chkAnuladas;
    }
}