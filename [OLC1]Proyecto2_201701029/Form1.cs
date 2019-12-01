using _OLC1_Proyecto2_201701029.Analizadores;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace _OLC1_Proyecto2_201701029
{
    public partial class Form1 : Form
    {
        public static RichTextBox salidaConsola = new RichTextBox();
        
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load_1(object sender, EventArgs e)
        {
            salidaConsola.Size= new System.Drawing.Size(846, 300);
            salidaConsola.ScrollToCaret();
            // Creating and setting the  
            // properties of the label 
            Label lb = new Label();
            lb.Location = new Point(251, 70);
            lb.Text = "Enter Text";

            // Adding this label in the form 
            this.Controls.Add(lb);

            // Creating and setting the 
            // properties of RichTextBox 
            salidaConsola.Location = new System.Drawing.Point(12, 392);
           
            salidaConsola.ForeColor = Color.Red;
            

            // Adding this RichTextBox in the form 
            this.Controls.Add(salidaConsola);
        }

        private void Analizar_Click(object sender, EventArgs e)
        {
            salidaConsola.ResetText();
            Sintactico nuevo = new Sintactico();
            string textAnalizar = text1.Text;

            nuevo.Analizar(textAnalizar);
        }
    }
}
