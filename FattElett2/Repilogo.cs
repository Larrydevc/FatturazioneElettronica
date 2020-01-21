using FattPrintF;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FattElett
{
    public partial class Repilogo : Form
    {
        public Repilogo()
        {
            InitializeComponent();
        }
        public bool StatoFattura { get; set; }
        private void Repilogo_Load(object sender, EventArgs e)
        {
            if (StatoFattura)
            {
                label1.Text = "FATTURA " + FattPrintF.Form1.FATTnfatt;
            }
            else
            {
                label1.Text = "N. DI CREDITO " + FattPrintF.Form1.FATTnnc;
            }
            double imponibile = Convert.ToDouble(FattPrintF.Form1.RepilogoTotaleImponibile);
            double imposta = Convert.ToDouble(FattPrintF.Form1.RepilogoTotaleImposta);
            double totaledoc2 = Convert.ToDouble(FattPrintF.Form1.RepilogoTotaleDocumento);
            labeltotaledoc.Text = "Totale imponibile: €" + imponibile.ToString("F");
            totaleimponibile.Text = "Totale imposta: €" + imposta.ToString("F");
            totaledoc.Text = "Totale Documento: €" + totaledoc2.ToString("F");
            labeldata.Text = "Data documento: " + DateTime.Today.ToShortDateString();
            labelragsoc.Text = RagSoc1.labelragsoc;
            labelcap.Text =  RagSoc1.labelcap;
            labelcitta.Text = FattPrintF.RagSoc1.labelcitta;
            labelindirizzo.Text = FattPrintF.RagSoc1.labelindirizzo;
            labelncivico.Text = FattPrintF.RagSoc1.labelncivico;
            labelsiglaprov.Text = FattPrintF.RagSoc1.labelsiglaprov;
            if (RagSoc1.coddest != "")
            {
                peccoddest.Text = "Codice SDI: " + RagSoc1.coddest.ToUpper();
            }
            else if (RagSoc1.coddest == "" && RagSoc1.peccoddest != "")
            {
                peccoddest.Text = "PEC: " + RagSoc1.peccoddest.ToUpper();
            }
            else
            {
                peccoddest.Text = "Invio Cassetto Fiscale";
            }

            int vertScrollWidth = SystemInformation.VerticalScrollBarWidth;
            tableLayoutPanel1.Padding = new Padding(0, 0, vertScrollWidth, 0);

            int contatore = 0;
            foreach (var item in FattPrintF.Form1.FattureRow)
            {
                if (item.desc == "SCONTO")
                {
                    tableLayoutPanel1.Controls.Add(new Label { Text = item.qty.ToString(), Anchor = AnchorStyles.Left, AutoSize = true }, 0, contatore);
                    tableLayoutPanel1.Controls.Add(new Label { Text = item.desc, Anchor = AnchorStyles.Left, AutoSize = true }, 1, contatore);
                    tableLayoutPanel1.Controls.Add(new Label { Text = "-  € " + (item.prezzo * (-1)).ToString("F"), Anchor = AnchorStyles.Right, AutoSize = true }, 2, contatore);
                    contatore++;
                }
                else
                {
                    tableLayoutPanel1.Controls.Add(new Label { Text = item.qty.ToString(), Anchor = AnchorStyles.Left, AutoSize = true }, 0, contatore);
                    tableLayoutPanel1.Controls.Add(new Label { Text = item.desc, Anchor = AnchorStyles.Left, AutoSize = true }, 1, contatore);
                    tableLayoutPanel1.Controls.Add(new Label { Text = "€ " + item.prezzo.ToString("F"), Anchor = AnchorStyles.Right, AutoSize = true }, 2, contatore);
                    contatore++;
                }
            }
            contatore = 0;
            Controls.Add(tableLayoutPanel1);

        }

        

        private void button2_Click(object sender, EventArgs e)
        {
            while (tableLayoutPanel1.Controls.Count > 0)
            {
                tableLayoutPanel1.Controls[0].Dispose();
            }
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void Button3_Click(object sender, EventArgs e)
        {
            AnnFatt ANNFATT = new AnnFatt();
            ANNFATT.Show();
        }
    }


}
