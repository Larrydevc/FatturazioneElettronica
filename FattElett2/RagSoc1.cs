using FattElett;
using FirebirdSql.Data.FirebirdClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FattPrintF
{

    public partial class RagSoc1 : Form
    {
        public static DialogResult result;
        public static string labelragsoc;
        public static string labelcap;
        public static string labelcitta;
        public static string labelindirizzo;
        public static string labelncivico;
        public static string labelsiglaprov;
        public static string peccoddest;
        public static string coddest;
        static string ShowPagamento;
        Control antcontrol;
        string eseiva = "";
        bool vcodfis;
        bool vpiva;
        public static bool togglepa;
        bool isfattura;
        public static bool togglefisica;
        string finemese = "";
        bool toggleMAYUS = false;
        bool toggleCHAR = false;
        public static Repilogo Repilogo2;
        public RagSoc1()
        {
            InitializeComponent();
        }

        private void RagSoc_Load(object sender, EventArgs e)
        {
            label1.Text = "FATTURA " + Form1.FATTnfatt;
            isfattura = true;
            label17.Visible = false;
            textBox17.Visible = false;
            toggleMAYUS = true;
            buttonMAYUS.BackColor = Color.Lime;
            toggleCHAR = false;
            CHAR.BackColor = Color.Red;
            indietro.Visible = false;
            avanti.Visible = true;
            panel2.Visible = true;
            panel3.Visible = false;
            textBox1.Text = Form1.ragionesociale;
            textBox2.Text = Form1.via;
            textBox3.Text = Form1.ncivico;
            textBox4.Text = Form1.cap;
            textBox5.Text = Form1.città;
            textBox6.Text = Form1.siglaprov;
            textBox7.Text = Form1.partivaiva;
            textBox8.Text = Form1.codicefiscale;
            textBox9.Text = Form1.coddest;
            textBox10.Text = Form1.pec;
            textBox11.Text = Form1.TipoDocumento;
            textBox12.Text = Form1.TipoDato;
            textBox13.Text = Form1.RiferimentoTesto;
            textBox14.Text = Form1.CIG;
            textBox15.Text = Form1.CUP;

            if (Form1.ShowPagamento == "1")
            {
                ShowPagamento = "1";
                pagamento_butt.BackColor = Color.LawnGreen;
            }
            else if (Form1.ShowPagamento == "0")
            {
                ShowPagamento = "0";
                pagamento_butt.BackColor = Color.Red;
            }

            richTextBox1.Text = "";
            antcontrol = null;
            this.ActiveControl = null;

            if (Form1.Finemese == "NO")
            {
                buttonDF.BackColor = Color.Lime;
                buttonFM.BackColor = Color.Red;
                finemese = "NO";
            }
            else if (Form1.Finemese == "SI")
            {
                buttonDF.BackColor = Color.Red;
                buttonFM.BackColor = Color.Lime;
                finemese = "SI";
            }

            if (Form1.EsigibilitaIVA == "I")
            {
                buttonI.BackColor = Color.Lime;
                buttonD.BackColor = Color.Red;
                buttonS.BackColor = Color.Red;
                eseiva = "I";
            }
            else if (Form1.EsigibilitaIVA == "D")
            {
                buttonI.BackColor = Color.Red;
                buttonD.BackColor = Color.Lime;
                buttonS.BackColor = Color.Red;
                eseiva = "D";
            }
            else if (Form1.EsigibilitaIVA == "S")
            {
                buttonI.BackColor = Color.Red;
                buttonD.BackColor = Color.Red;
                buttonS.BackColor = Color.Lime;
                eseiva = "S";
            }
            else
            {
                buttonI.BackColor = Color.Lime;
                buttonD.BackColor = Color.Red;
                buttonS.BackColor = Color.Red;
                eseiva = "I";
            }

            this.ActiveControl = textBox1;
            antcontrol = textBox1;
            vcodfis = CodiceFiscale.ControlloFormaleOK(textBox8.Text);
            if (vcodfis)
            {
                textBox8.BackColor = Color.LimeGreen;
            }
            else
            {
                vcodfis = VerificaPI(textBox8.Text);
                if (vcodfis)
                {
                    textBox8.BackColor = Color.LimeGreen;
                }
                else
                {
                    textBox8.BackColor = Color.Red;
                }
            }

            vpiva = VerificaPI(textBox7.Text);
            if (vpiva)
            {
                textBox7.BackColor = Color.LimeGreen;
            }
            else
            {
                textBox7.BackColor = Color.Red;
            }

            if (Form1.PA == 1)
            {
                if (textBox9.Text.Length == 6)
                {
                    textBox9.BackColor = Color.LimeGreen;
                }
                else
                {
                    textBox9.BackColor = Color.Red;
                }
            }
            else if (Form1.PA == 2)
            {
                textBox9.BackColor = Color.LimeGreen;
                textBox9.BackColor = Color.Red;
            }
            else if (Form1.PA == 0)
            {
                if (textBox9.Text.Length == 7)
                {
                    textBox9.BackColor = Color.LimeGreen;
                }
                else
                {
                    textBox9.BackColor = Color.Red;
                }
            }

            if (IsValidEmail(textBox10.Text))
            {
                textBox10.BackColor = Color.LimeGreen;
            }
            else
            {
                textBox10.BackColor = Color.Red;
            }
            if (Form1.PA == 1)
            {
                pa.BackColor = Color.Lime;
                privato.BackColor = Color.Red;
                togglepa = true;
            }else if (Form1.PA == 0)
            {
                pa.BackColor = Color.Red;
                privato.BackColor = Color.Lime;
                togglepa = false;
            }
        }

        private void bq_Click(object sender, EventArgs e)
        {
            if(toggleMAYUS == false && toggleCHAR == false)
            {
                this.ActiveControl = antcontrol;
                SendKeys.Send("q");
            }else if (toggleMAYUS == true)
            {
                this.ActiveControl = antcontrol;
                SendKeys.Send("Q");
            }
            else if (toggleCHAR == true)
            {
                this.ActiveControl = antcontrol;
                SendKeys.Send("\\");
            }
        }

        private void bspazio_Click(object sender, EventArgs e)
        {
            this.ActiveControl = antcontrol;
            SendKeys.Send(" ");
        }

        private void bw_Click(object sender, EventArgs e)
        {
            if (toggleMAYUS == false && toggleCHAR == false)
            {
                this.ActiveControl = antcontrol;
                SendKeys.Send("w");
            }
            else if (toggleMAYUS == true)
            {
                this.ActiveControl = antcontrol;
                SendKeys.Send("W");
            }
            else if (toggleCHAR == true)
            {
                this.ActiveControl = antcontrol;
                SendKeys.Send("|");
            }
        }

        private void be_Click(object sender, EventArgs e)
        {
            if (toggleMAYUS == false && toggleCHAR == false)
            {
                this.ActiveControl = antcontrol;
                SendKeys.Send("e");
            }
            else if (toggleMAYUS == true)
            {
                this.ActiveControl = antcontrol;
                SendKeys.Send("E");
            }
            else if (toggleCHAR == true)
            {
                this.ActiveControl = antcontrol;
                SendKeys.Send("\"");
            }
        }

        private void br_Click(object sender, EventArgs e)
        {
            if (toggleMAYUS == false && toggleCHAR == false)
            {
                this.ActiveControl = antcontrol;
                SendKeys.Send("r");
            }
            else if (toggleMAYUS == true)
            {
                this.ActiveControl = antcontrol;
                SendKeys.Send("R");
            }
            else if (toggleCHAR == true)
            {
                this.ActiveControl = antcontrol;
                SendKeys.Send("£");
            }
        }

        private void bt_Click(object sender, EventArgs e)
        {
            if (toggleMAYUS == false && toggleCHAR == false)
            {
                this.ActiveControl = antcontrol;
                SendKeys.Send("t");
            }
            else if (toggleMAYUS == true)
            {
                this.ActiveControl = antcontrol;
                SendKeys.Send("T");
            }
            else if (toggleCHAR == true)
            {
                this.ActiveControl = antcontrol;
                SendKeys.Send("€");
            }
        }

        private void by_Click(object sender, EventArgs e)
        {
            if (toggleMAYUS == false && toggleCHAR == false)
            {
                this.ActiveControl = antcontrol;
                SendKeys.Send("y");
            }
            else if (toggleMAYUS == true)
            {
                this.ActiveControl = antcontrol;
                SendKeys.Send("Y");
            }
            else if (toggleCHAR == true)
            {
                this.ActiveControl = antcontrol;
                SendKeys.Send("{(}");
            }
        }

        private void bu_Click(object sender, EventArgs e)
        {
            if (toggleMAYUS == false && toggleCHAR == false)
            {
                this.ActiveControl = antcontrol;
                SendKeys.Send("u");
            }
            else if (toggleMAYUS == true)
            {
                this.ActiveControl = antcontrol;
                SendKeys.Send("U");
            }
            else if (toggleCHAR == true)
            {
                this.ActiveControl = antcontrol;
                SendKeys.Send("{%}");
            }
        }

        private void bi_Click(object sender, EventArgs e)
        {
            if (toggleMAYUS == false && toggleCHAR == false)
            {
                this.ActiveControl = antcontrol;
                SendKeys.Send("i");
            }
            else if (toggleMAYUS == true)
            {
                this.ActiveControl = antcontrol;
                SendKeys.Send("I");
            }
            else if (toggleCHAR == true)
            {
                this.ActiveControl = antcontrol;
            }
        }

        private void bo_Click(object sender, EventArgs e)
        {
            if (toggleMAYUS == false && toggleCHAR == false)
            {
                this.ActiveControl = antcontrol;
                SendKeys.Send("o");
            }
            else if (toggleMAYUS == true)
            {
                this.ActiveControl = antcontrol;
                SendKeys.Send("O");
            }
            else if (toggleCHAR == true)
            {
                this.ActiveControl = antcontrol;
                SendKeys.Send("{)}");
            }
        }

        private void bp_Click(object sender, EventArgs e)
        {
            if (toggleMAYUS == false && toggleCHAR == false)
            {
                this.ActiveControl = antcontrol;
                SendKeys.Send("p");
            }
            else if (toggleMAYUS == true)
            {
                this.ActiveControl = antcontrol;
                SendKeys.Send("P");
            }
            else if (toggleCHAR == true)
            {
                this.ActiveControl = antcontrol;
                SendKeys.Send("{=}");
            }
        }

        private void ba_Click(object sender, EventArgs e)
        {
            if (toggleMAYUS == false && toggleCHAR == false)
            {
                this.ActiveControl = antcontrol;
                SendKeys.Send("a");
            }
            else if (toggleMAYUS == true)
            {
                this.ActiveControl = antcontrol;
                SendKeys.Send("A");
            }
            else if (toggleCHAR == true)
            {
                this.ActiveControl = antcontrol;
                SendKeys.Send("à");
            }
        }

        private void bs_Click(object sender, EventArgs e)
        {
            if (toggleMAYUS == false && toggleCHAR == false)
            {
                this.ActiveControl = antcontrol;
                SendKeys.Send("s");
            }
            else if (toggleMAYUS == true)
            {
                this.ActiveControl = antcontrol;
                SendKeys.Send("S");
            }
            else if (toggleCHAR == true)
            {
                this.ActiveControl = antcontrol;
                SendKeys.Send("[");
            }
        }

        private void bd_Click(object sender, EventArgs e)
        {
            if (toggleMAYUS == false && toggleCHAR == false)
            {
                this.ActiveControl = antcontrol;
                SendKeys.Send("d");
            }
            else if (toggleMAYUS == true)
            {
                this.ActiveControl = antcontrol;
                SendKeys.Send("D");
            }
            else if (toggleCHAR == true)
            {
                this.ActiveControl = antcontrol;
                SendKeys.Send("]");
            }
        }

        private void bf_Click(object sender, EventArgs e)
        {
            if (toggleMAYUS == false && toggleCHAR == false)
            {
                this.ActiveControl = antcontrol;
                SendKeys.Send("f");
            }
            else if (toggleMAYUS == true)
            {
                this.ActiveControl = antcontrol;
                SendKeys.Send("F");
            }
            else if (toggleCHAR == true)
            {
                this.ActiveControl = antcontrol;
                SendKeys.Send("#");
            }
        }

        private void bg_Click(object sender, EventArgs e)
        {
            if (toggleMAYUS == false && toggleCHAR == false)
            {
                this.ActiveControl = antcontrol;
                SendKeys.Send("g");
            }
            else if (toggleMAYUS == true)
            {
                this.ActiveControl = antcontrol;
                SendKeys.Send("G");
            }
            else if (toggleCHAR == true)
            {
                this.ActiveControl = antcontrol;
                SendKeys.Send("_");
            }
        }

        private void bh_Click(object sender, EventArgs e)
        {
            if (toggleMAYUS == false && toggleCHAR == false)
            {
                this.ActiveControl = antcontrol;
                SendKeys.Send("h");
            }
            else if (toggleMAYUS == true)
            {
                this.ActiveControl = antcontrol;
                SendKeys.Send("H");
            }
            else if (toggleCHAR == true)
            {
                this.ActiveControl = antcontrol;
                SendKeys.Send(":");
            }
        }

        private void bj_Click(object sender, EventArgs e)
        {
            if (toggleMAYUS == false && toggleCHAR == false)
            {
                this.ActiveControl = antcontrol;
                SendKeys.Send("j");
            }
            else if (toggleMAYUS == true)
            {
                this.ActiveControl = antcontrol;
                SendKeys.Send("J");
            }
            else if (toggleCHAR == true)
            {
                this.ActiveControl = antcontrol;
                SendKeys.Send(";");
            }
        }

        private void bk_Click(object sender, EventArgs e)
        {
            if (toggleMAYUS == false && toggleCHAR == false)
            {
                this.ActiveControl = antcontrol;
                SendKeys.Send("k");
            }
            else if (toggleMAYUS == true)
            {
                this.ActiveControl = antcontrol;
                SendKeys.Send("K");
            }
            else if (toggleCHAR == true)
            {
                this.ActiveControl = antcontrol;
                SendKeys.Send("<");
            }
        }

        private void bl_Click(object sender, EventArgs e)
        {
            if (toggleMAYUS == false && toggleCHAR == false)
            {
                this.ActiveControl = antcontrol;
                SendKeys.Send("l");
            }
            else if (toggleMAYUS == true)
            {
                this.ActiveControl = antcontrol;
                SendKeys.Send("L");
            }
            else if (toggleCHAR == true)
            {
                this.ActiveControl = antcontrol;
                SendKeys.Send(">");
            }
        }

        private void bz_Click(object sender, EventArgs e)
        {
            if (toggleMAYUS == false && toggleCHAR == false)
            {
                this.ActiveControl = antcontrol;
                SendKeys.Send("z");
            }
            else if (toggleMAYUS == true)
            {
                this.ActiveControl = antcontrol;
                SendKeys.Send("Z");
            }
            else if (toggleCHAR == true)
            {
                this.ActiveControl = antcontrol;
                SendKeys.Send("è");
            }
        }

        private void bx_Click(object sender, EventArgs e)
        {
            if (toggleMAYUS == false && toggleCHAR == false)
            {
                this.ActiveControl = antcontrol;
                SendKeys.Send("x");
            }
            else if (toggleMAYUS == true)
            {
                this.ActiveControl = antcontrol;
                SendKeys.Send("X");
            }
            else if (toggleCHAR == true)
            {
                this.ActiveControl = antcontrol;
                SendKeys.Send("ì");
            }
        }

        private void bc_Click(object sender, EventArgs e)
        {
            if (toggleMAYUS == false && toggleCHAR == false)
            {
                this.ActiveControl = antcontrol;
                SendKeys.Send("c");
            }
            else if (toggleMAYUS == true)
            {
                this.ActiveControl = antcontrol;
                SendKeys.Send("C");
            }
            else if (toggleCHAR == true)
            {
                this.ActiveControl = antcontrol;
                SendKeys.Send("ù");
            }
        }

        private void bv_Click(object sender, EventArgs e)
        {
            if (toggleMAYUS == false && toggleCHAR == false)
            {
                this.ActiveControl = antcontrol;
                SendKeys.Send("v");
            }
            else if (toggleMAYUS == true)
            {
                this.ActiveControl = antcontrol;
                SendKeys.Send("V");
            }
            else if (toggleCHAR == true)
            {
                this.ActiveControl = antcontrol;
                SendKeys.Send("ò");
            }
        }

        private void bb_Click(object sender, EventArgs e)
        {
            if (toggleMAYUS == false && toggleCHAR == false)
            {
                this.ActiveControl = antcontrol;
                SendKeys.Send("b");
            }
            else if (toggleMAYUS == true)
            {
                this.ActiveControl = antcontrol;
                SendKeys.Send("B");
            }
            else if (toggleCHAR == true)
            {
                this.ActiveControl = antcontrol;
            }
        }

        private void bn_Click(object sender, EventArgs e)
        {
            if (toggleMAYUS == false && toggleCHAR == false)
            {
                this.ActiveControl = antcontrol;
                SendKeys.Send("n");
            }
            else if (toggleMAYUS == true)
            {
                this.ActiveControl = antcontrol;
                SendKeys.Send("N");
            }
            else if (toggleCHAR == true)
            {
                this.ActiveControl = antcontrol;
            }
        }

        private void bm_Click(object sender, EventArgs e)
        {
            if (toggleMAYUS == false && toggleCHAR == false)
            {
                this.ActiveControl = antcontrol;
                SendKeys.Send("m");
            }
            else if (toggleMAYUS == true)
            {
                this.ActiveControl = antcontrol;
                SendKeys.Send("M");
            }
            else if (toggleCHAR == true)
            {
                this.ActiveControl = antcontrol;
            }
        }

        private void bcancella_Click(object sender, EventArgs e)
        {
            this.ActiveControl = antcontrol;
            SendKeys.Send("{BACKSPACE}");
        }

        private void bvirgola_Click(object sender, EventArgs e)
        {
            this.ActiveControl = antcontrol;
            SendKeys.Send(",");
        }

        private void bpunto_Click(object sender, EventArgs e)
        {
            this.ActiveControl = antcontrol;
            SendKeys.Send(".");
        }

        private void bchiocciola_Click(object sender, EventArgs e)
        {
            this.ActiveControl = antcontrol;
            SendKeys.Send("@");
        }

        private void textBox1_MouseClick(object sender, MouseEventArgs e)
        {
            antcontrol = textBox1;
        }

        private void textBox2_MouseClick(object sender, MouseEventArgs e)
        {
            antcontrol = textBox2;
        }

        private void textBox4_MouseClick(object sender, MouseEventArgs e)
        {
            antcontrol = textBox4;
        }

        private void textBox3_MouseClick(object sender, MouseEventArgs e)
        {
            antcontrol = textBox3;
        }

        private void textBox5_MouseClick(object sender, MouseEventArgs e)
        {
            antcontrol = textBox5;
        }

        private void textBox6_MouseClick(object sender, MouseEventArgs e)
        {
            antcontrol = textBox6;
        }

        private void textBox7_MouseClick(object sender, MouseEventArgs e)
        {
            antcontrol = textBox7;
        }

        private void textBox8_MouseClick(object sender, MouseEventArgs e)
        {
            antcontrol = textBox8;
        }

        private void textBox9_MouseClick(object sender, MouseEventArgs e)
        {
            antcontrol = textBox9;
        }

        private void textBox10_MouseClick(object sender, MouseEventArgs e)
        {
            antcontrol = textBox10;
        }

        private void textBox11_MouseClick(object sender, MouseEventArgs e)
        {
            antcontrol = textBox11;
        }

        private void textBox12_MouseClick(object sender, MouseEventArgs e)
        {
            antcontrol = textBox12;
        }

        private void textBox13_MouseClick(object sender, MouseEventArgs e)
        {
            antcontrol = textBox13;
        }

        private void textBox14_MouseClick(object sender, MouseEventArgs e)
        {
            antcontrol = textBox14;
        }

        private void textBox15_MouseClick(object sender, MouseEventArgs e)
        {
            antcontrol = textBox15;
        }

        private void textBox16_MouseClick(object sender, MouseEventArgs e)
        {
            antcontrol = textBox16;
        }

        private void RichTextBox1_Click(object sender, EventArgs e)
        {
            antcontrol = richTextBox1;
        }

        private void Newline_butt_Click(object sender, EventArgs e)
        {
            this.ActiveControl = richTextBox1;
            SendKeys.Send("{ENTER}");
        }

        private void button41_Click(object sender, EventArgs e)
        {
            AnnFatt ANNFATT = new AnnFatt();
            ANNFATT.Show();
        }

        public static DialogResult result3;

        private void button1_Click(object sender, EventArgs e)
        {
            if (textBox1.Text == "" || textBox2.Text == "" || textBox3.Text == "" || textBox4.Text == "" || textBox5.Text == "" || textBox6.Text == "")
            {
                //MessageBox.Show(vpiva.ToString() + " " + vcodfis.ToString());
                MessageBox.Show("Dati obbligatori mancanti", "Fatturazione Elettronica");
            }else if(textBox7.Text == "" && textBox8.Text == "")
            {
                MessageBox.Show("Partita IVA / Codice Fiscale mancanti", "Fatturazione Elettronica");
            }
            else
            {
                labelragsoc = textBox1.Text;
                labelindirizzo = textBox2.Text;
                labelncivico = textBox3.Text;
                labelcap = textBox4.Text;
                labelcitta = textBox5.Text;
                labelsiglaprov = textBox6.Text;

                if(textBox10.Text == "")
                {
                    peccoddest = "";
                }
                else
                {
                    peccoddest = textBox10.Text;
                }

                if (textBox9.Text == "" && Form1.PA == 0)
                {
                    coddest = "0000000";
                }
                else if (textBox9.Text == "" && Form1.PA == 1)
                {
                    coddest = "999999";
                }
                else if (textBox9.Text == "" && Form1.PA == 2)
                {
                    coddest = "";
                }
                else
                {
                    coddest = textBox9.Text.ToUpper();
                }

                Repilogo2 = new Repilogo();
                Repilogo2.StatoFattura = isfattura;
                result = Repilogo2.ShowDialog();
                if (result == DialogResult.OK)
                {
                    Form1.ragionesociale = textBox1.Text.Replace("'", "''");
                    Form1.via = textBox2.Text;
                    Form1.ncivico = textBox3.Text;
                    Form1.cap = textBox4.Text;
                    Form1.città = textBox5.Text.ToUpper();
                    Form1.siglaprov = textBox6.Text.ToUpper();
                    Form1.partivaiva = textBox7.Text;
                    Form1.codicefiscale = textBox8.Text.ToUpper();
                    Form1.coddest = textBox9.Text.ToUpper();
                    Form1.pec = textBox10.Text.ToUpper();
                    Form1.TipoDocumento = textBox11.Text;
                    Form1.TipoDato = textBox12.Text;
                    Form1.RiferimentoTesto = textBox13.Text;
                    Form1.CIG = textBox14.Text;
                    Form1.CUP = textBox15.Text;
                    Form1.EsigibilitaIVA = eseiva;
                    Form1.Finemese = finemese;
                    Form1.ShowPagamento = ShowPagamento;
                    Form1.isfattura = isfattura;
                    Form1.fatturecollegate = textBox17.Text;

                    this.DialogResult = DialogResult.OK;
                    AggCliente();
                    this.Close();
                }
                else
                {
                    Repilogo2.Dispose();
                }
            }
        }

        private void AggCliente()
        {
            try
            {
                string connectioncmd = "UPDATE CLIENTI SET CLIENTERAGIONESOCIALEUP = ('" + Form1.ragionesociale.ToUpper() + "'),CLIENTERAGIONESOCIALE = ('" + Form1.ragionesociale.ToUpper() + "'),CLIENTEVIA = ('" + Form1.via + "'),CLIENTECITTA = ('" + Form1.città + "'),CLIENTENUMEROCIVICO = ('" + Form1.ncivico + "'),CLIENTEPROVINCIA = ('" + Form1.siglaprov + "'),CLIENTECAP = ('" + Form1.cap + "'),CLIENTECODICEFISCALE = ('" + Form1.codicefiscale + "'),CLIENTEPARTITAIVA = ('" + Form1.partivaiva + "'),CLIENTECODICEDESTINATARIO = ('" + Form1.coddest + "'),CLIENTEPEC = ('" + Form1.pec + "'),CLIENTEPA = ('" + Form1.PA + "'),CLIENTETIPODOCUMENTO = ('" + Form1.TipoDocumento + "'),CLIENTETIPODATO = ('" + Form1.TipoDato + "'),CLIENTERIFTEST = ('" + Form1.RiferimentoTesto + "'),CLIENTECIG = ('" + Form1.CIG + "'),CLIENTECUP = ('" + Form1.CUP + "'),CLIENTEESEIVA = ('" + Form1.EsigibilitaIVA + "'),CLIENTEFM = ('" + Form1.Finemese + "'),CLIENTEPAGAMENTO = ('" + Form1.ShowPagamento + "') WHERE CLIENTEID = (" + Form1.codcliente + ")";
                FbCommand scontrino = new FbCommand(connectioncmd, Form1.connection);
                scontrino.ExecuteNonQuery();
            }
            catch (Exception)
            {
                MessageBox.Show("Errore aggiornamento dati cliente.","PentaStart");
            }

        }

        private void b1_Click(object sender, EventArgs e)
        {
            this.ActiveControl = antcontrol;
            SendKeys.Send("1");
        }

        private void b2_Click(object sender, EventArgs e)
        {
            this.ActiveControl = antcontrol;
            SendKeys.Send("2");
        }

        private void b3_Click(object sender, EventArgs e)
        {
            this.ActiveControl = antcontrol;
            SendKeys.Send("3");
        }

        private void b0_Click(object sender, EventArgs e)
        {
            this.ActiveControl = antcontrol;
            SendKeys.Send("0");
        }

        private void b4_Click(object sender, EventArgs e)
        {
            this.ActiveControl = antcontrol;
            SendKeys.Send("4");
        }

        private void b5_Click(object sender, EventArgs e)
        {
            this.ActiveControl = antcontrol;
            SendKeys.Send("5");
        }

        private void b6_Click(object sender, EventArgs e)
        {
            this.ActiveControl = antcontrol;
            SendKeys.Send("6");
        }

        private void b7_Click(object sender, EventArgs e)
        {
            this.ActiveControl = antcontrol;
            SendKeys.Send("7");
        }

        private void b8_Click(object sender, EventArgs e)
        {
            this.ActiveControl = antcontrol;
            SendKeys.Send("8");
        }

        private void b9_Click(object sender, EventArgs e)
        {
            this.ActiveControl = antcontrol;
            SendKeys.Send("9");
        }

        public static bool VerificaPI(string paramPI)
        {
            paramPI = paramPI.Trim();
            try
            {
                if (paramPI.Length == 11)
                {
                    int tot = 0;
                    int dispari = 0;

                    for (int i = 0; i < 10; i += 2)
                        dispari += int.Parse(paramPI.Substring(i, 1));

                    for (int i = 1; i < 10; i += 2)
                    {
                        tot = (int.Parse(paramPI.Substring(i, 1))) * 2;
                        tot = (tot / 10) + (tot % 10);
                        dispari += tot;
                    }

                    int controllo = int.Parse(paramPI.Substring(10, 1));

                    if (((dispari % 10) == 0 && (controllo == 0))
                       || ((10 - (dispari % 10)) == controllo))
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
                else
                {
                    return false;
                }
            }
            catch (Exception e)
            {
                return false;
            }
        }

        private void textBox7_TextChanged(object sender, EventArgs e)
        {
            if (togglefisica == false)
            {
                vpiva = VerificaPI(textBox7.Text);
                if (vpiva)
                {
                    textBox7.BackColor = Color.LimeGreen;
                }
                else
                {
                    textBox7.BackColor = Color.Red;
                }
            }
            else
            {
                textBox7.BackColor = Color.Red;
            }
        }

        private void textBox8_TextChanged(object sender, EventArgs e)
        {
            if(textBox8.TextLength == 11 && togglefisica == false)
            {
                vpiva = VerificaPI(textBox8.Text);
                if (vpiva)
                {
                    textBox8.BackColor = Color.LimeGreen;
                }
                else
                {
                    textBox8.BackColor = Color.Red;
                }
            }else
            {
                vcodfis = CodiceFiscale.ControlloFormaleOK(textBox8.Text);
                if (vcodfis)
                {
                    textBox8.BackColor = Color.LimeGreen;
                }
                else
                {
                    textBox8.BackColor = Color.Red;
                }
            }
        }

        private void bappostrofo_Click(object sender, EventArgs e)
        {
            this.ActiveControl = antcontrol;
            SendKeys.Send("\"");
        }

        private void bcommerciale_Click(object sender, EventArgs e)
        {
            this.ActiveControl = antcontrol;
            SendKeys.Send("&");
        }

        private void textBox9_TextChanged(object sender, EventArgs e)
        {
            if(togglefisica == false)
            {
                if (Form1.PA == 1)
                {
                    if (textBox9.Text.Length == 6)
                    {
                        textBox9.BackColor = Color.LimeGreen;
                    }
                    else
                    {
                        textBox9.BackColor = Color.Red;
                    }
                }
                else if (Form1.PA == 0)
                {
                    if (textBox9.Text.Length == 7)
                    {
                        textBox9.BackColor = Color.LimeGreen;
                    }
                    else
                    {
                        textBox9.BackColor = Color.Red;
                    }
                }
            }
            else
            {
                textBox9.BackColor = Color.Red;
            }

        }

        private void textBox10_TextChanged(object sender, EventArgs e)
        {
            if(togglefisica == false)
            {
                if (IsValidEmail(textBox10.Text))
                {
                    textBox10.BackColor = Color.LimeGreen;
                }
                else
                {
                    textBox10.BackColor = Color.Red;
                }
            }
            else
            {
                textBox10.BackColor = Color.Red;
            }

        }

        public static bool IsValidEmail(string email)
        {
            if (string.IsNullOrWhiteSpace(email))
                return false;

            try
            {
                // Normalize the domain
                email = Regex.Replace(email, @"(@)(.+)$", DomainMapper,
                                      RegexOptions.None, TimeSpan.FromMilliseconds(200));

                // Examines the domain part of the email and normalizes it.
                string DomainMapper(Match match)
                {
                    // Use IdnMapping class to convert Unicode domain names.
                    var idn = new IdnMapping();

                    // Pull out and process domain name (throws ArgumentException on invalid)
                    var domainName = idn.GetAscii(match.Groups[2].Value);

                    return match.Groups[1].Value + domainName;
                }
            }
            catch (RegexMatchTimeoutException e)
            {
                return false;
            }
            catch (ArgumentException e)
            {
                return false;
            }

            try
            {
                return Regex.IsMatch(email,
                    @"^(?("")("".+?(?<!\\)""@)|(([0-9a-z]((\.(?!\.))|[-!#\$%&'\*\+/=\?\^`\{\}\|~\w])*)(?<=[0-9a-z])@))" +
                    @"(?(\[)(\[(\d{1,3}\.){3}\d{1,3}\])|(([0-9a-z][-0-9a-z]*[0-9a-z]*\.)+[a-z0-9][\-a-z0-9]{0,22}[a-z0-9]))$",
                    RegexOptions.IgnoreCase, TimeSpan.FromMilliseconds(250));
            }
            catch (RegexMatchTimeoutException)
            {
                return false;
            }
        }

        private void Butt_pfisica_Click(object sender, EventArgs e)
        {
            antcontrol = null;
            pa.BackColor = Color.Red;
            privato.BackColor = Color.Red;
            Butt_pfisica.BackColor = Color.LawnGreen;
            togglefisica = true;
            togglepa = false;
            Form1.PA = 2;

            if (IsValidEmail(textBox10.Text))
            {
                textBox10.BackColor = Color.Red;
            }
            else
            {
                textBox10.BackColor = Color.Red;
            }

            vpiva = VerificaPI(textBox7.Text);
            if (vpiva)
            {
                textBox7.BackColor = Color.Red;
            }
            else
            {
                textBox7.BackColor = Color.Red;
            }
            vcodfis = CodiceFiscale.ControlloFormaleOK(textBox8.Text);
            if (vcodfis)
            {
                textBox8.BackColor = Color.LimeGreen;
            }
            else
            {
                vcodfis = VerificaPI(textBox8.Text);
                if (vcodfis)
                {
                    textBox8.BackColor = Color.Red;
                }
                else
                {
                    textBox8.BackColor = Color.Red;
                }
            }
            textBox9.BackColor = Color.Red;
        }

        private void privato_Click(object sender, EventArgs e)
        {
            antcontrol = null;
            pa.BackColor = Color.Red;
            privato.BackColor = Color.LawnGreen;
            Butt_pfisica.BackColor = Color.Red;
            togglepa = false;
            togglefisica = false;
            Form1.PA = 0;

            if (IsValidEmail(textBox10.Text))
            {
                textBox10.BackColor = Color.LimeGreen;
            }
            else
            {
                textBox10.BackColor = Color.Red;
            }

            vpiva = VerificaPI(textBox7.Text);
            if (vpiva)
            {
                textBox7.BackColor = Color.LimeGreen;
            }
            else
            {
                textBox7.BackColor = Color.Red;
            }
            vcodfis = CodiceFiscale.ControlloFormaleOK(textBox8.Text);
            if (vcodfis)
            {
                textBox8.BackColor = Color.LimeGreen;
            }
            else
            {
                vcodfis = VerificaPI(textBox8.Text);
                if (vcodfis)
                {
                    textBox8.BackColor = Color.LimeGreen;
                }
                else
                {
                    textBox8.BackColor = Color.Red;
                }
            }

            if (textBox9.Text.Length == 7)
            {
                textBox9.BackColor = Color.LimeGreen;
            }
            else
            {
                textBox9.BackColor = Color.Red;
            }
        }

        private void pa_Click(object sender, EventArgs e)
        {
            antcontrol = null;
            pa.BackColor = Color.LawnGreen;
            privato.BackColor = Color.Red;
            Butt_pfisica.BackColor = Color.Red;
            togglepa = true;
            togglefisica = false;
            Form1.PA = 1;

            if (IsValidEmail(textBox10.Text))
            {
                textBox10.BackColor = Color.LimeGreen;
            }
            else
            {
                textBox10.BackColor = Color.Red;
            }

            vpiva = VerificaPI(textBox7.Text);
            if (vpiva)
            {
                textBox7.BackColor = Color.LimeGreen;
            }
            else
            {
                textBox7.BackColor = Color.Red;
            }
            vcodfis = CodiceFiscale.ControlloFormaleOK(textBox8.Text);
            if (vcodfis)
            {
                textBox8.BackColor = Color.LimeGreen;
            }
            else
            {
                vcodfis = VerificaPI(textBox8.Text);
                if (vcodfis)
                {
                    textBox8.BackColor = Color.LimeGreen;
                }
                else
                {
                    textBox8.BackColor = Color.Red;
                }
            }

            if (textBox9.Text.Length == 6)
            {
                textBox9.BackColor = Color.LimeGreen;
            }
            else
            {
                textBox9.BackColor = Color.Red;
            }
        }

        private void indietro_Click(object sender, EventArgs e)
        {
            antcontrol = null;
            this.ActiveControl = null;
            if (panel3.Visible == true)
            {
                panel2.Visible = true;
                panel3.Visible = false;
                indietro.Visible = false;
                avanti.Visible = true;
            }
            else if (panel4.Visible == true)
            {
                panel2.Visible = false;
                panel3.Visible = true;
                panel4.Visible = false;
                indietro.Visible = true;
                avanti.Visible = true;
            }
        }

        private void avanti_Click(object sender, EventArgs e)
        {
            antcontrol = null;
            this.ActiveControl = null;
            if (panel2.Visible == true)
            {
                panel2.Visible = false;
                panel3.Visible = true;
                panel4.Visible = false;
                indietro.Visible = true;
            }else if(panel3.Visible == true)
            {
                panel3.Visible = false;
                panel4.Visible = true;
                panel2.Visible = false;
                indietro.Visible = true;
                avanti.Visible = false;
            }
        }

        private void buttonMAYUS_Click(object sender, EventArgs e)
        {
            if (toggleMAYUS == false)
            {
                toggleMAYUS = true;
                buttonMAYUS.BackColor = Color.Lime;
                bq.Text = bq.Text.ToUpper();
                bw.Text = bw.Text.ToUpper();
                be.Text = be.Text.ToUpper();
                br.Text = br.Text.ToUpper();
                bt.Text = bt.Text.ToUpper();
                by.Text = by.Text.ToUpper();
                bu.Text = bu.Text.ToUpper();
                bi.Text = bi.Text.ToUpper();
                bo.Text = bo.Text.ToUpper();
                bp.Text = bp.Text.ToUpper();
                ba.Text = ba.Text.ToUpper();
                bs.Text = bs.Text.ToUpper();
                bd.Text = bd.Text.ToUpper();
                bf.Text = bf.Text.ToUpper();
                bg.Text = bg.Text.ToUpper();
                bh.Text = bh.Text.ToUpper();
                bj.Text = bj.Text.ToUpper();
                bk.Text = bk.Text.ToUpper();
                bl.Text = bl.Text.ToUpper();
                bz.Text = bz.Text.ToUpper();
                bx.Text = bx.Text.ToUpper();
                bc.Text = bc.Text.ToUpper();
                bv.Text = bv.Text.ToUpper();
                bb.Text = bb.Text.ToUpper();
                bn.Text = bn.Text.ToUpper();
                bm.Text = bm.Text.ToUpper();
            }
            else
            {
                toggleMAYUS = false;
                buttonMAYUS.BackColor = Color.Red;
                bq.Text = bq.Text.ToLower();
                bw.Text = bw.Text.ToLower();
                be.Text = be.Text.ToLower();
                br.Text = br.Text.ToLower();
                bt.Text = bt.Text.ToLower();
                by.Text = by.Text.ToLower();
                bu.Text = bu.Text.ToLower();
                bi.Text = bi.Text.ToLower();
                bo.Text = bo.Text.ToLower();
                bp.Text = bp.Text.ToLower();
                ba.Text = ba.Text.ToLower();
                bs.Text = bs.Text.ToLower();
                bd.Text = bd.Text.ToLower();
                bf.Text = bf.Text.ToLower();
                bg.Text = bg.Text.ToLower();
                bh.Text = bh.Text.ToLower();
                bj.Text = bj.Text.ToLower();
                bk.Text = bk.Text.ToLower();
                bl.Text = bl.Text.ToLower();
                bz.Text = bz.Text.ToLower();
                bx.Text = bx.Text.ToLower();
                bc.Text = bc.Text.ToLower();
                bv.Text = bv.Text.ToLower();
                bb.Text = bb.Text.ToLower();
                bn.Text = bn.Text.ToLower();
                bm.Text = bm.Text.ToLower();
            }
        }

        private void CHAR_Click(object sender, EventArgs e)
        {
            if (toggleCHAR == false)
            {
                toggleCHAR = true;
                toggleMAYUS = false;
                buttonMAYUS.BackColor = Color.Red;
                CHAR.BackColor = Color.Lime;
                bq.Text = bq.Text = "\\";
                bw.Text = bw.Text = "|";
                be.Text = be.Text = "\"";
                br.Text = br.Text = "£";
                bt.Text = bt.Text = "€";
                by.Text = by.Text = "(";
                bu.Text = bu.Text = "%";
                bi.Text = bi.Text = "";
                bo.Text = bo.Text = ")";
                bp.Text = bp.Text = "=";
                ba.Text = ba.Text = "à";
                bs.Text = bs.Text = "[";
                bd.Text = bd.Text = "]";
                bf.Text = bf.Text = "#";
                bg.Text = bg.Text = "_";
                bh.Text = bh.Text = ":";
                bj.Text = bj.Text = ";";
                bk.Text = bk.Text = "<";
                bl.Text = bl.Text = ">";
                bz.Text = bz.Text = "è";
                bx.Text = bx.Text = "ì";
                bc.Text = bc.Text = "ò";
                bv.Text = bv.Text = "";
                bb.Text = bb.Text = "";
                bn.Text = bn.Text = "";
                bm.Text = bm.Text = "";
            }
            else
            {
                toggleCHAR = false;
                CHAR.BackColor = Color.Red;
                bq.Text = bq.Text = "q";
                bw.Text = bw.Text = "w";
                be.Text = be.Text = "e";
                br.Text = br.Text = "r";
                bt.Text = bt.Text = "t";
                by.Text = by.Text = "y";
                bu.Text = bu.Text = "u";
                bi.Text = bi.Text = "i";
                bo.Text = bo.Text = "o";
                bp.Text = bp.Text = "p";
                ba.Text = ba.Text = "a";
                bs.Text = bs.Text = "s";
                bd.Text = bd.Text = "d";
                bf.Text = bf.Text = "f";
                bg.Text = bg.Text = "g";
                bh.Text = bh.Text = "h";
                bj.Text = bj.Text = "j";
                bk.Text = bk.Text = "k";
                bl.Text = bl.Text = "l";
                bz.Text = bz.Text = "z";
                bx.Text = bx.Text = "x";
                bc.Text = bc.Text = "c";
                bv.Text = bv.Text = "v";
                bb.Text = bb.Text = "b";
                bn.Text = bn.Text = "n";
                bm.Text = bm.Text = "m";
            }
        }

        private void btrattino_Click(object sender, EventArgs e)
        {
                this.ActiveControl = antcontrol;
                SendKeys.Send("-");
        }

        private void bcommerciale_Click_1(object sender, EventArgs e)
        {
            this.ActiveControl = antcontrol;
            SendKeys.Send("*");
        }

        private void buttonDF_Click(object sender, EventArgs e)
        {
            antcontrol = null;
            buttonDF.BackColor = Color.Lime;
            buttonFM.BackColor = Color.Red;
            finemese = "NO";
        }

        private void buttonFM_Click(object sender, EventArgs e)
        {
            antcontrol = null;
            buttonDF.BackColor = Color.Red;
            buttonFM.BackColor = Color.Lime;
            finemese = "SI";
        }

        private void buttonS_Click(object sender, EventArgs e)
        {
            antcontrol = null;
            buttonI.BackColor = Color.Red;
            buttonD.BackColor = Color.Red;
            buttonS.BackColor = Color.Lime;
            eseiva = "S";
        }

        private void buttonD_Click(object sender, EventArgs e)
        {
            antcontrol = null;
            buttonI.BackColor = Color.Red;
            buttonD.BackColor = Color.Lime;
            buttonS.BackColor = Color.Red;
            eseiva = "D";
        }

        private void buttonI_Click(object sender, EventArgs e)
        {
            antcontrol = null;
            buttonI.BackColor = Color.Lime;
            buttonD.BackColor = Color.Red;
            buttonS.BackColor = Color.Red;
            eseiva = "I";
        }

        private void Pagamento_butt_Click(object sender, EventArgs e)
        {
            antcontrol = null;
            if(ShowPagamento == "0")
            {
                ShowPagamento = "1";
                pagamento_butt.BackColor = Color.LawnGreen;
            }
            else if(ShowPagamento == "1")
            {
                ShowPagamento = "0";
                pagamento_butt.BackColor = Color.Red;
            }
        }

        private void Label1_Click(object sender, EventArgs e)
        {
            if (!isfattura)
            {
                label1.Text = "FATTURA " + Form1.FATTnfatt;
                isfattura = true;
                label17.Visible = false;
                textBox17.Visible = false;
            }
            else
            {
                label1.Text = "N. DI CREDITO " + Form1.FATTnnc;
                isfattura = false;
                label17.Visible = true;
                textBox17.Visible = true;
            }
        }

        private void TextBox17_MouseClick(object sender, MouseEventArgs e)
        {
            antcontrol = textBox17;
        }
    }
}
