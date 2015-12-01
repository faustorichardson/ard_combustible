namespace SisGesCom
{
    partial class frmPrinter
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmPrinter));
            this.CrystalReportViewer1 = new CrystalDecisions.Windows.Forms.CrystalReportViewer();
            this.txtUpDown = new System.Windows.Forms.NumericUpDown();
            this.cmdZoom = new System.Windows.Forms.Button();
            this.txtBuscarText = new System.Windows.Forms.TextBox();
            this.cmdImprimir = new System.Windows.Forms.Button();
            this.cmdExportarPdf = new System.Windows.Forms.Button();
            this.cmdUltimaPag = new System.Windows.Forms.Button();
            this.txtIrPag = new System.Windows.Forms.TextBox();
            this.cmdSiguiente = new System.Windows.Forms.Button();
            this.cmdPrimeraPag = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.label3 = new System.Windows.Forms.Label();
            this.cmdAnterior = new System.Windows.Forms.Button();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            ((System.ComponentModel.ISupportInitialize)(this.txtUpDown)).BeginInit();
            this.groupBox3.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // CrystalReportViewer1
            // 
            this.CrystalReportViewer1.ActiveViewIndex = -1;
            this.CrystalReportViewer1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.CrystalReportViewer1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.CrystalReportViewer1.Cursor = System.Windows.Forms.Cursors.Default;
            this.CrystalReportViewer1.Location = new System.Drawing.Point(4, 55);
            this.CrystalReportViewer1.Name = "CrystalReportViewer1";
            this.CrystalReportViewer1.Size = new System.Drawing.Size(997, 497);
            this.CrystalReportViewer1.TabIndex = 56;
            // 
            // txtUpDown
            // 
            this.txtUpDown.Location = new System.Drawing.Point(904, 20);
            this.txtUpDown.Name = "txtUpDown";
            this.txtUpDown.Size = new System.Drawing.Size(41, 20);
            this.txtUpDown.TabIndex = 54;
            // 
            // cmdZoom
            // 
            this.cmdZoom.Image = ((System.Drawing.Image)(resources.GetObject("cmdZoom.Image")));
            this.cmdZoom.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.cmdZoom.Location = new System.Drawing.Point(945, 17);
            this.cmdZoom.Name = "cmdZoom";
            this.cmdZoom.Size = new System.Drawing.Size(56, 26);
            this.cmdZoom.TabIndex = 52;
            this.cmdZoom.Text = "Zoom";
            this.cmdZoom.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.cmdZoom.UseVisualStyleBackColor = true;
            // 
            // txtBuscarText
            // 
            this.txtBuscarText.Location = new System.Drawing.Point(169, 12);
            this.txtBuscarText.Name = "txtBuscarText";
            this.txtBuscarText.Size = new System.Drawing.Size(168, 20);
            this.txtBuscarText.TabIndex = 12;
            // 
            // cmdImprimir
            // 
            this.cmdImprimir.Image = ((System.Drawing.Image)(resources.GetObject("cmdImprimir.Image")));
            this.cmdImprimir.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.cmdImprimir.Location = new System.Drawing.Point(827, 17);
            this.cmdImprimir.Name = "cmdImprimir";
            this.cmdImprimir.Size = new System.Drawing.Size(75, 27);
            this.cmdImprimir.TabIndex = 55;
            this.cmdImprimir.Text = "Imprimir";
            this.cmdImprimir.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.cmdImprimir.UseVisualStyleBackColor = true;
            // 
            // cmdExportarPdf
            // 
            this.cmdExportarPdf.Image = ((System.Drawing.Image)(resources.GetObject("cmdExportarPdf.Image")));
            this.cmdExportarPdf.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.cmdExportarPdf.Location = new System.Drawing.Point(723, 17);
            this.cmdExportarPdf.Name = "cmdExportarPdf";
            this.cmdExportarPdf.Size = new System.Drawing.Size(103, 27);
            this.cmdExportarPdf.TabIndex = 51;
            this.cmdExportarPdf.Text = "   Guardar como";
            this.cmdExportarPdf.UseVisualStyleBackColor = true;
            // 
            // cmdUltimaPag
            // 
            this.cmdUltimaPag.Image = ((System.Drawing.Image)(resources.GetObject("cmdUltimaPag.Image")));
            this.cmdUltimaPag.Location = new System.Drawing.Point(89, 11);
            this.cmdUltimaPag.Name = "cmdUltimaPag";
            this.cmdUltimaPag.Size = new System.Drawing.Size(30, 20);
            this.cmdUltimaPag.TabIndex = 16;
            this.cmdUltimaPag.UseVisualStyleBackColor = true;
            // 
            // txtIrPag
            // 
            this.txtIrPag.Location = new System.Drawing.Point(71, 12);
            this.txtIrPag.Name = "txtIrPag";
            this.txtIrPag.Size = new System.Drawing.Size(27, 20);
            this.txtIrPag.TabIndex = 12;
            // 
            // cmdSiguiente
            // 
            this.cmdSiguiente.Image = ((System.Drawing.Image)(resources.GetObject("cmdSiguiente.Image")));
            this.cmdSiguiente.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.cmdSiguiente.Location = new System.Drawing.Point(59, 11);
            this.cmdSiguiente.Name = "cmdSiguiente";
            this.cmdSiguiente.Size = new System.Drawing.Size(30, 20);
            this.cmdSiguiente.TabIndex = 3;
            this.cmdSiguiente.UseVisualStyleBackColor = true;
            // 
            // cmdPrimeraPag
            // 
            this.cmdPrimeraPag.Image = ((System.Drawing.Image)(resources.GetObject("cmdPrimeraPag.Image")));
            this.cmdPrimeraPag.Location = new System.Drawing.Point(2, 11);
            this.cmdPrimeraPag.Name = "cmdPrimeraPag";
            this.cmdPrimeraPag.Size = new System.Drawing.Size(30, 20);
            this.cmdPrimeraPag.TabIndex = 15;
            this.cmdPrimeraPag.UseVisualStyleBackColor = true;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(99, 16);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(70, 13);
            this.label2.TabIndex = 13;
            this.label2.Text = "Buscar Texto";
            // 
            // groupBox3
            // 
            this.groupBox3.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.groupBox3.Controls.Add(this.txtBuscarText);
            this.groupBox3.Controls.Add(this.label2);
            this.groupBox3.Controls.Add(this.txtIrPag);
            this.groupBox3.Controls.Add(this.label3);
            this.groupBox3.Location = new System.Drawing.Point(358, 12);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(355, 37);
            this.groupBox3.TabIndex = 53;
            this.groupBox3.TabStop = false;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(3, 16);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(68, 13);
            this.label3.TabIndex = 0;
            this.label3.Text = "Ir A Pag. No.";
            // 
            // cmdAnterior
            // 
            this.cmdAnterior.Image = ((System.Drawing.Image)(resources.GetObject("cmdAnterior.Image")));
            this.cmdAnterior.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.cmdAnterior.Location = new System.Drawing.Point(31, 11);
            this.cmdAnterior.Name = "cmdAnterior";
            this.cmdAnterior.Size = new System.Drawing.Size(30, 20);
            this.cmdAnterior.TabIndex = 2;
            this.cmdAnterior.UseVisualStyleBackColor = true;
            // 
            // groupBox2
            // 
            this.groupBox2.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.groupBox2.Controls.Add(this.cmdUltimaPag);
            this.groupBox2.Controls.Add(this.cmdSiguiente);
            this.groupBox2.Controls.Add(this.cmdPrimeraPag);
            this.groupBox2.Controls.Add(this.cmdAnterior);
            this.groupBox2.ForeColor = System.Drawing.Color.Black;
            this.groupBox2.ImeMode = System.Windows.Forms.ImeMode.On;
            this.groupBox2.Location = new System.Drawing.Point(229, 12);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(122, 37);
            this.groupBox2.TabIndex = 50;
            this.groupBox2.TabStop = false;
            // 
            // frmPrinter
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1004, 564);
            this.Controls.Add(this.CrystalReportViewer1);
            this.Controls.Add(this.txtUpDown);
            this.Controls.Add(this.cmdZoom);
            this.Controls.Add(this.cmdImprimir);
            this.Controls.Add(this.cmdExportarPdf);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox2);
            this.Name = "frmPrinter";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "frmPrinter";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Controls.SetChildIndex(this.lblTituloForm, 0);
            this.Controls.SetChildIndex(this.groupBox2, 0);
            this.Controls.SetChildIndex(this.groupBox3, 0);
            this.Controls.SetChildIndex(this.cmdExportarPdf, 0);
            this.Controls.SetChildIndex(this.cmdImprimir, 0);
            this.Controls.SetChildIndex(this.cmdZoom, 0);
            this.Controls.SetChildIndex(this.txtUpDown, 0);
            this.Controls.SetChildIndex(this.CrystalReportViewer1, 0);
            ((System.ComponentModel.ISupportInitialize)(this.txtUpDown)).EndInit();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        public CrystalDecisions.Windows.Forms.CrystalReportViewer CrystalReportViewer1;
        private System.Windows.Forms.NumericUpDown txtUpDown;
        private System.Windows.Forms.Button cmdZoom;
        private System.Windows.Forms.TextBox txtBuscarText;
        private System.Windows.Forms.Button cmdImprimir;
        private System.Windows.Forms.Button cmdExportarPdf;
        private System.Windows.Forms.Button cmdUltimaPag;
        private System.Windows.Forms.TextBox txtIrPag;
        private System.Windows.Forms.Button cmdSiguiente;
        private System.Windows.Forms.Button cmdPrimeraPag;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button cmdAnterior;
        private System.Windows.Forms.GroupBox groupBox2;
    }
}