using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Text.RegularExpressions;
namespace Multisian
{
    public partial class frm_Main : Form
    {
        private List<System.Windows.Forms.Button> buttons = new List<Button>();
        private SeriesFile sf;
        public frm_Main()
        {
            InitializeComponent();
            this.SuspendLayout();

            this.ResumeLayout(false);
            this.PerformLayout();
        }

        private void btn_Open_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog1 = new OpenFileDialog();

            // Set filter options and filter index.
            openFileDialog1.Filter = "CSV or Text Files (.csv, .txt)|*.csv;*.txt|All Files (*.*)|*.*";
            openFileDialog1.FilterIndex = 1;
            
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                sf = new SeriesFile(openFileDialog1.FileName);
                PlaceButtons(sf.cols);
                textBox1.Text = openFileDialog1.FileName;
            }

        }

        private void btn_AddText(object sender, EventArgs e)
        {
            Button btn = sender as Button;
            textBox2.Text += btn.Text + ", ";
        }
        private void PlaceButtons(int p)
        {
            int XStart = 40;
            int YStart = 75;
            int width = 107;
            int height = 28;

            for (int i = 0; i < p; i++)
            {
                Button btn = new Button();
                buttons.Add(btn);
                this.SuspendLayout();
                this.Controls.Add(btn);
                btn.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));

                btn.Location = new System.Drawing.Point(XStart + (width + 10) * (i % 4), YStart + (height + 10) * (i / 4));
                btn.Name = "b1";
                btn.Size = new System.Drawing.Size(width, height);
                btn.TabIndex = 2;
                btn.Text = "X" + Convert.ToString(i + 1);
                btn.UseVisualStyleBackColor = true;
                btn.Click += new System.EventHandler(this.btn_AddText);

                this.ResumeLayout(false);
                this.PerformLayout();

            }
            this.SuspendLayout();
            textBox2 = new TextBox();
            textBox2.Location = new System.Drawing.Point(XStart, YStart + (height + 10) * ((p - 1) / 4 + 1));
            textBox2.Size = new System.Drawing.Size(341, 20);
            textBox2.Name = "textBox2";
            textBox2.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));

            this.Controls.Add(textBox2);

            Button btn_run = new Button();
            btn_run.Location = new System.Drawing.Point(391, 74 + (height + 10) * ((p - 1) / 4 + 1));
            btn_run.Size = new System.Drawing.Size(width, height);
            btn_run.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            btn_run.Text = "Run";
            btn_run.UseVisualStyleBackColor = true;
            btn_run.Click += new System.EventHandler(this.btn_run_Event);

            this.Controls.Add(btn_run);
            
            this.ResumeLayout(true);
        }

        private void btn_run_Event(object sender, EventArgs e)
        {
            char[] d =  { ' ' };
            textBox2.Text.TrimStart(d);

            string invalidChars = @"[xX][0-9]+"; 
            Regex rgx = new Regex(invalidChars, RegexOptions.IgnoreCase);
            rgx.Match(textBox2.Text.TrimStart(d));
            String[] matches =  
                Regex.Matches(textBox2.Text.TrimStart(d), @"[xX][0-9]+")
                .Cast<Match>()
                .Select(m => m.Value)
                .ToArray();

            double[] results = GIFEE.GeneralizedFalseNearestNeighbors(matches, sf.len, sf.cols, sf.filePath);
            foreach (double result in results)
            {
                chart1.Series[0].Points.Add(result);
            }
        }

    }
}
