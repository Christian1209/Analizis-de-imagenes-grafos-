namespace UsoDeImagenes
{
    partial class Form1
    {
        /// <summary>
        /// Variable del diseñador necesaria.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Limpiar los recursos que se estén usando.
        /// </summary>
        /// <param name="disposing">true si los recursos administrados se deben desechar; false en caso contrario.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Código generado por el Diseñador de Windows Forms

        /// <summary>
        /// Método necesario para admitir el Diseñador. No se puede modificar
        /// el contenido de este método con el editor de código.
        /// </summary>
        private void InitializeComponent()
        {
            this.btnSelection = new System.Windows.Forms.Button();
            this.pbxImage = new System.Windows.Forms.PictureBox();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.btnDibujaPixel = new System.Windows.Forms.Button();
            this.listBoxCirculos = new System.Windows.Forms.ListBox();
            this.label1 = new System.Windows.Forms.Label();
            this.btnKruskal = new System.Windows.Forms.Button();
            this.btnDeprededar = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.btnAumentar = new System.Windows.Forms.Button();
            this.btnReducir = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.pbxImage)).BeginInit();
            this.SuspendLayout();
            // 
            // btnSelection
            // 
            this.btnSelection.Location = new System.Drawing.Point(1040, 57);
            this.btnSelection.Name = "btnSelection";
            this.btnSelection.Size = new System.Drawing.Size(107, 81);
            this.btnSelection.TabIndex = 0;
            this.btnSelection.Text = "Seleccionar Imagen";
            this.btnSelection.UseVisualStyleBackColor = true;
            this.btnSelection.Click += new System.EventHandler(this.btnSelection_Click);
            // 
            // pbxImage
            // 
            this.pbxImage.Location = new System.Drawing.Point(12, 23);
            this.pbxImage.Name = "pbxImage";
            this.pbxImage.Size = new System.Drawing.Size(991, 711);
            this.pbxImage.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pbxImage.TabIndex = 1;
            this.pbxImage.TabStop = false;
            this.pbxImage.MouseClick += new System.Windows.Forms.MouseEventHandler(this.pbxImage_MouseClick);
            this.pbxImage.MouseMove += new System.Windows.Forms.MouseEventHandler(this.pbxImage_MouseMove);
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            // 
            // btnDibujaPixel
            // 
            this.btnDibujaPixel.Location = new System.Drawing.Point(1040, 162);
            this.btnDibujaPixel.Name = "btnDibujaPixel";
            this.btnDibujaPixel.Size = new System.Drawing.Size(107, 95);
            this.btnDibujaPixel.TabIndex = 13;
            this.btnDibujaPixel.Text = "Encuentra Circulos";
            this.btnDibujaPixel.UseVisualStyleBackColor = true;
            this.btnDibujaPixel.Click += new System.EventHandler(this.btnDibujaPixel_Click);
            // 
            // listBoxCirculos
            // 
            this.listBoxCirculos.FormattingEnabled = true;
            this.listBoxCirculos.ItemHeight = 16;
            this.listBoxCirculos.Location = new System.Drawing.Point(1352, 23);
            this.listBoxCirculos.Name = "listBoxCirculos";
            this.listBoxCirculos.Size = new System.Drawing.Size(532, 548);
            this.listBoxCirculos.TabIndex = 14;
            this.listBoxCirculos.SelectedIndexChanged += new System.EventHandler(this.listBoxCirculos_SelectedIndexChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.label1.Location = new System.Drawing.Point(993, 363);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(0, 20);
            this.label1.TabIndex = 16;
            // 
            // btnKruskal
            // 
            this.btnKruskal.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(128)))));
            this.btnKruskal.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnKruskal.Location = new System.Drawing.Point(1040, 635);
            this.btnKruskal.Name = "btnKruskal";
            this.btnKruskal.Size = new System.Drawing.Size(167, 99);
            this.btnKruskal.TabIndex = 17;
            this.btnKruskal.Text = "Comenzar Simulacion";
            this.btnKruskal.UseVisualStyleBackColor = false;
            this.btnKruskal.Click += new System.EventHandler(this.btnKruskal_Click);
            // 
            // btnDeprededar
            // 
            this.btnDeprededar.BackColor = System.Drawing.Color.LightGreen;
            this.btnDeprededar.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnDeprededar.Location = new System.Drawing.Point(1205, 443);
            this.btnDeprededar.Name = "btnDeprededar";
            this.btnDeprededar.Size = new System.Drawing.Size(132, 63);
            this.btnDeprededar.TabIndex = 20;
            this.btnDeprededar.Text = "Añadir depredador";
            this.btnDeprededar.UseVisualStyleBackColor = false;
            this.btnDeprededar.Click += new System.EventHandler(this.btnDeprededar_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(1043, 443);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(104, 54);
            this.label2.TabIndex = 21;
            this.label2.Text = "Presiona para \r\nañadir\r\n depredadores";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(1043, 552);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(104, 54);
            this.label3.TabIndex = 22;
            this.label3.Text = "Presiona para \r\ndejar de \r\nañadir agentes\r\n";
            // 
            // button1
            // 
            this.button1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
            this.button1.Cursor = System.Windows.Forms.Cursors.Default;
            this.button1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button1.Location = new System.Drawing.Point(1205, 543);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(132, 63);
            this.button1.TabIndex = 23;
            this.button1.Text = "Dejar de añadir\r\nDepredadores\r\n";
            this.button1.UseVisualStyleBackColor = false;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(1124, 297);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(83, 36);
            this.label4.TabIndex = 24;
            this.label4.Text = "Tamaño de\r\nRadar\r\n";
            // 
            // btnAumentar
            // 
            this.btnAumentar.Location = new System.Drawing.Point(1173, 351);
            this.btnAumentar.Name = "btnAumentar";
            this.btnAumentar.Size = new System.Drawing.Size(100, 32);
            this.btnAumentar.TabIndex = 26;
            this.btnAumentar.Text = "Aumentar";
            this.btnAumentar.UseVisualStyleBackColor = true;
            this.btnAumentar.Click += new System.EventHandler(this.btnAumentar_Click);
            // 
            // btnReducir
            // 
            this.btnReducir.Location = new System.Drawing.Point(1047, 351);
            this.btnReducir.Name = "btnReducir";
            this.btnReducir.Size = new System.Drawing.Size(100, 32);
            this.btnReducir.TabIndex = 26;
            this.btnReducir.Text = "Reducir";
            this.btnReducir.UseVisualStyleBackColor = true;
            this.btnReducir.Click += new System.EventHandler(this.btnReducir_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1896, 962);
            this.Controls.Add(this.btnReducir);
            this.Controls.Add(this.btnAumentar);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.btnDeprededar);
            this.Controls.Add(this.btnKruskal);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.listBoxCirculos);
            this.Controls.Add(this.btnDibujaPixel);
            this.Controls.Add(this.pbxImage);
            this.Controls.Add(this.btnSelection);
            this.Name = "Form1";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pbxImage)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnSelection;
        private System.Windows.Forms.PictureBox pbxImage;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.Button btnDibujaPixel;
        private System.Windows.Forms.ListBox listBoxCirculos;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnKruskal;
        private System.Windows.Forms.Button btnDeprededar;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button btnAumentar;
        private System.Windows.Forms.Button btnReducir;
    }
}

