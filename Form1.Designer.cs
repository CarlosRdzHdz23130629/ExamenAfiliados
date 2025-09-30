namespace Afiliados
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            btnReset = new Button();
            btnCargar = new Button();
            cmbMunicipio = new ComboBox();
            cmbEntidad = new ComboBox();
            dgvInformacion = new DataGridView();
            dtpInicio = new DateTimePicker();
            dtpFin = new DateTimePicker();
            chkFiltrarFecha = new CheckBox();
            label1 = new Label();
            label2 = new Label();
            label3 = new Label();
            ofdExcel = new OpenFileDialog();
            txtTotalAfiliados = new TextBox();
            ((System.ComponentModel.ISupportInitialize)dgvInformacion).BeginInit();
            SuspendLayout();
            // 
            // btnReset
            // 
            btnReset.Location = new Point(130, 12);
            btnReset.Name = "btnReset";
            btnReset.Size = new Size(112, 34);
            btnReset.TabIndex = 0;
            btnReset.Text = "Resetear";
            btnReset.UseVisualStyleBackColor = true;
            btnReset.Click += btnReset_Click;
            // 
            // btnCargar
            // 
            btnCargar.Location = new Point(12, 12);
            btnCargar.Name = "btnCargar";
            btnCargar.Size = new Size(112, 34);
            btnCargar.TabIndex = 1;
            btnCargar.Text = "Cargar";
            btnCargar.UseVisualStyleBackColor = true;
            btnCargar.Click += btnCargar_Click;
            // 
            // cmbMunicipio
            // 
            cmbMunicipio.FormattingEnabled = true;
            cmbMunicipio.Location = new Point(130, 128);
            cmbMunicipio.Name = "cmbMunicipio";
            cmbMunicipio.Size = new Size(182, 33);
            cmbMunicipio.TabIndex = 2;
            cmbMunicipio.SelectedIndexChanged += cmbMunicipio_SelectedIndexChanged;
            // 
            // cmbEntidad
            // 
            cmbEntidad.FormattingEnabled = true;
            cmbEntidad.Location = new Point(130, 74);
            cmbEntidad.Name = "cmbEntidad";
            cmbEntidad.Size = new Size(182, 33);
            cmbEntidad.TabIndex = 3;
            cmbEntidad.SelectedIndexChanged += cmbEntidad_SelectedIndexChanged;
            // 
            // dgvInformacion
            // 
            dgvInformacion.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvInformacion.Location = new Point(12, 182);
            dgvInformacion.Name = "dgvInformacion";
            dgvInformacion.RowHeadersWidth = 62;
            dgvInformacion.Size = new Size(1173, 225);
            dgvInformacion.TabIndex = 4;
            // 
            // dtpInicio
            // 
            dtpInicio.Location = new Point(35, 537);
            dtpInicio.Name = "dtpInicio";
            dtpInicio.Size = new Size(300, 31);
            dtpInicio.TabIndex = 5;
            dtpInicio.ValueChanged += dtpInicio_ValueChanged;
            // 
            // dtpFin
            // 
            dtpFin.Location = new Point(35, 602);
            dtpFin.Name = "dtpFin";
            dtpFin.Size = new Size(300, 31);
            dtpFin.TabIndex = 6;
            // 
            // chkFiltrarFecha
            // 
            chkFiltrarFecha.AutoSize = true;
            chkFiltrarFecha.Location = new Point(35, 489);
            chkFiltrarFecha.Name = "chkFiltrarFecha";
            chkFiltrarFecha.Size = new Size(162, 29);
            chkFiltrarFecha.TabIndex = 7;
            chkFiltrarFecha.Text = "Filtrar por fecha";
            chkFiltrarFecha.UseVisualStyleBackColor = true;
            chkFiltrarFecha.CheckedChanged += checkBox1_CheckedChanged;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(79, 442);
            label1.Name = "label1";
            label1.Size = new Size(164, 25);
            label1.TabIndex = 8;
            label1.Text = "Total de afiliados: 0";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(12, 77);
            label2.Name = "label2";
            label2.Size = new Size(66, 25);
            label2.TabIndex = 9;
            label2.Text = "Estado";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(12, 136);
            label3.Name = "label3";
            label3.Size = new Size(90, 25);
            label3.TabIndex = 10;
            label3.Text = "Municipio";
            // 
            // ofdExcel
            // 
            ofdExcel.FileName = "openFileDialog1";
            // 
            // txtTotalAfiliados
            // 
            txtTotalAfiliados.Location = new Point(249, 439);
            txtTotalAfiliados.Name = "txtTotalAfiliados";
            txtTotalAfiliados.Size = new Size(150, 31);
            txtTotalAfiliados.TabIndex = 11;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(10F, 25F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1197, 684);
            Controls.Add(txtTotalAfiliados);
            Controls.Add(label3);
            Controls.Add(label2);
            Controls.Add(label1);
            Controls.Add(chkFiltrarFecha);
            Controls.Add(dtpFin);
            Controls.Add(dtpInicio);
            Controls.Add(dgvInformacion);
            Controls.Add(cmbEntidad);
            Controls.Add(cmbMunicipio);
            Controls.Add(btnCargar);
            Controls.Add(btnReset);
            Name = "Form1";
            Text = "Form1";
            ((System.ComponentModel.ISupportInitialize)dgvInformacion).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Button btnReset;
        private Button btnCargar;
        private ComboBox cmbMunicipio;
        private ComboBox cmbEntidad;
        private DataGridView dgvInformacion;
        private DateTimePicker dtpInicio;
        private DateTimePicker dtpFin;
        private CheckBox chkFiltrarFecha;
        private Label label1;
        private Label label2;
        private Label label3;
        private OpenFileDialog ofdExcel;
        private TextBox txtTotalAfiliados;
    }
}
