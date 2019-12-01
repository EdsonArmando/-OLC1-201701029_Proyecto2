namespace _OLC1_Proyecto2_201701029
{
    partial class Form1
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private  System.ComponentModel.IContainer components = null;

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
        private  void InitializeComponent()
        {
            this.text1 = new System.Windows.Forms.RichTextBox();
            this.Analizar = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // text1
            // 
            this.text1.Location = new System.Drawing.Point(12, 12);
            this.text1.Name = "text1";
            this.text1.Size = new System.Drawing.Size(586, 370);
            this.text1.TabIndex = 0;
            this.text1.Text = "";
            // 
            // Analizar
            // 
            this.Analizar.Location = new System.Drawing.Point(628, 12);
            this.Analizar.Name = "Analizar";
            this.Analizar.Size = new System.Drawing.Size(75, 23);
            this.Analizar.TabIndex = 1;
            this.Analizar.Text = "Analizar";
            this.Analizar.UseVisualStyleBackColor = true;
            this.Analizar.Click += new System.EventHandler(this.Analizar_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(870, 616);
            this.Controls.Add(this.Analizar);
            this.Controls.Add(this.text1);
            this.Name = "Form1";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load_1);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.RichTextBox text1;
        private System.Windows.Forms.Button Analizar;
    }
}

