using FattElett;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FattPrintF
{
    public partial class AnnFatt : Form
    {
        public AnnFatt()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                Repilogo j = new Repilogo();
                while (j.tableLayoutPanel1.Controls.Count > 0)
                {
                    j.tableLayoutPanel1.Controls[0].Dispose();
                }
                RagSoc1.result = DialogResult.Cancel;
                Form1.result2 = DialogResult.Cancel;
                Form1.result = DialogResult.Cancel;
                this.Close();
                Form1.RAGSOC1.Close();
                RagSoc1.Repilogo2.Close();
            }
            catch (Exception ex)
            {

            }
            
        }

        private void AnnFatt_Paint(object sender, PaintEventArgs e)
        {
            int thickness = 2;
            int halfThickness = thickness / 2;
            using (Pen p = new Pen(Color.Black, thickness))
            {
                e.Graphics.DrawRectangle(p, new Rectangle(halfThickness,
                                                          halfThickness,
                                                          this.ClientSize.Width - thickness,
                                                          this.ClientSize.Height - thickness));
            }
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {
            int thickness = 2;
            int halfThickness = thickness / 2;
            using (Pen p = new Pen(Color.Black, thickness))
            {
                e.Graphics.DrawRectangle(p, new Rectangle(halfThickness,
                                                          halfThickness,
                                                          panel1.ClientSize.Width - thickness,
                                                          panel1.ClientSize.Height - thickness));
            }
        }

        private void AnnFatt_Load(object sender, EventArgs e)
        {
        }
    }
}
