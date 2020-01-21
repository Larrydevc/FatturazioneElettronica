using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FattElett
{
    public partial class Impostazione : Form
    {
        public Impostazione()
        {
            InitializeComponent();
        }

        string cartellaxml;
        string id_ddt;
        string eori;
        string emailcommer;
        string serversmtp;
        string portsmtp;
        string usersmtp;
        string passsmtp;
        string codpaese;
        string invioprogressivo;
        string ditta;
        string partitaiva;
        string codfis;
        string regfis;
        string nome;
        string cognome;
        string indirizzo;
        string ncivico;
        string cap;
        string città;
        string prov;
        string nazione;
        string ufficio;
        string nrea;
        string iban;
        string capsoc;
        string sociorea;
        string statoliqui;
        string telefono;
        string email;

        private void Button8_Click(object sender, EventArgs e)
        {
            List<Control> TBinflow = new List<Control>();
            foreach (Control control in flowLayoutPanel1.Controls)
            {
                if (control is TextBox)
                {
                    TBinflow.Add(control);
                }
            }

            foreach (TextBox control in TBinflow)
            {
                if (control is TextBox && control.ReadOnly == false)
                {
                    if (control.Text == "" || control.Text == " ")
                    {
                        if (control.Name == "TBddt")
                        {
                            control.Text = "6";
                        }
                        else { control.Text = "null"; }
                        
                    }
                }

                if (control is TextBox && control.ReadOnly == true)
                {
                    if (control.Text == "" || control.Text == " ")
                    {
                        control.Text = "1";
                    }
                }
            }

            IniFile ini = new IniFile();
            ini.Load("C://trilogis//FattElett.ini");
            ini.SetKeyValue("XML", "Percorso", TBcartellaxml.Text);
            ini.SetKeyValue("DDT", "ID", TBddt.Text);
            ini.SetKeyValue("FATT", "Telefono", TBtelefono.Text);
            ini.SetKeyValue("FATT", "Email", TBemail.Text);
            ini.SetKeyValue("FATT", "ProgressivoInvio", TBinvioprogressivo.Text);
            ini.SetKeyValue("FATT", "ProgressivoFatture", TBnfatt.Text);
            ini.SetKeyValue("FATT", "ProgressivoNotaCredito", TBnnc.Text);
            ini.SetKeyValue("FATT", "Ditta", TBditta.Text);
            ini.SetKeyValue("FATT", "PartitaIva", TBpartitaiva.Text);
            ini.SetKeyValue("FATT", "CodFis", TBcodfis.Text);
            try
            {
                ini.SetKeyValue("FATT", "RegFis", CBregfis.SelectedItem.ToString());
            }
            catch (Exception)
            {
                ini.SetKeyValue("FATT", "RegFis", "RF01");
            }
            ini.SetKeyValue("FATT", "Indirizzo", TBindirizzo.Text);
            ini.SetKeyValue("FATT", "CAP", TBcap.Text);
            ini.SetKeyValue("FATT", "Città", TBcittà.Text);
            ini.SetKeyValue("FATT", "CAP", TBcap.Text);
            ini.SetKeyValue("FATT", "Provincia", TBprovincia.Text);
            ini.SetKeyValue("FATT", "Nazione", TBnazione.Text);
            ini.SetKeyValue("FATT", "IBAN", TBiban.Text);
            ini.SetKeyValue("FATT", "CodiceEori", TBcodeori.Text);
            ini.SetKeyValue("REA", "Ufficio", TBufficio.Text);
            ini.SetKeyValue("REA", "NumeroREA", TBnrea.Text);
            ini.SetKeyValue("REA", "CapitaleSociale", TBcap.Text);
            ini.SetKeyValue("REA", "SocioREA", TBsociorea.Text);
            try
            {
                ini.SetKeyValue("REA", "StatoLiquidazione", CBstatoliqui.SelectedItem.ToString());
            }
            catch (Exception)
            {
                ini.SetKeyValue("REA", "StatoLiquidazione", "LN");
            }
            ini.Save("C://trilogis//FattElett.ini");
            MessageBox.Show("Impostazione salvata correttamente. Riavvio Fatt. Elett. in corso", "Fatturazione Elettronica");
            Application.Restart();
        }

        private void Button5_Click(object sender, EventArgs e)
        {
            Dispose();
        }

        private void LoadIni()
        {
            string iniexist = "C:/trilogis/FattElett.ini";
            IniFile ini = new IniFile();
            if (System.IO.File.Exists(iniexist))
            {
                ini.Load("C:/trilogis/FattElett.ini");
                TBcartellaxml.Text = ini.GetKeyValue("XML", "Percorso");
                TBddt.Text = ini.GetKeyValue("DDT", "ID");
                TBtelefono.Text = ini.GetKeyValue("FATT", "Telefono");
                TBemail.Text = ini.GetKeyValue("FATT", "Email");
                TBinvioprogressivo.Text = ini.GetKeyValue("FATT", "ProgressivoInvio");
                TBnfatt.Text = ini.GetKeyValue("FATT", "ProgressivoFatture");
                TBnnc.Text = ini.GetKeyValue("FATT", "ProgressivoNotaCredito");
                TBditta.Text = ini.GetKeyValue("FATT", "Ditta");
                TBpartitaiva.Text = ini.GetKeyValue("FATT", "PartitaIva");
                TBcodfis.Text = ini.GetKeyValue("FATT", "CodFis");
                CBregfis.SelectedItem = ini.GetKeyValue("FATT", "RegFis");
                TBindirizzo.Text = ini.GetKeyValue("FATT", "Indirizzo");
                TBcap.Text = ini.GetKeyValue("FATT", "CAP");
                TBcittà.Text = ini.GetKeyValue("FATT", "Città");
                TBprovincia.Text = ini.GetKeyValue("FATT", "Provincia");
                TBnazione.Text = ini.GetKeyValue("FATT", "Nazione");
                TBcodeori.Text = ini.GetKeyValue("FATT", "CodiceEORI");
                TBiban.Text = ini.GetKeyValue("FATT", "IBAN");
                TBufficio.Text = ini.GetKeyValue("REA", "Ufficio");
                TBnrea.Text = ini.GetKeyValue("REA", "NumeroREA");
                TBcapsoc.Text = ini.GetKeyValue("REA", "CapitaleSociale");
                TBsociorea.Text = ini.GetKeyValue("REA", "SocioREA");
                CBstatoliqui.SelectedItem = ini.GetKeyValue("REA", "StatoLiquidazione");
            }
        }

        private void PanelImpos_Paint(object sender, PaintEventArgs e)
        {
            LoadIni();
            if (TBcartellaxml.Text != "" && TBcartellaxml.Text != "null")
            {
                Directory.CreateDirectory(TBcartellaxml.Text + "\\Inviate");
                Directory.CreateDirectory(TBcartellaxml.Text + "\\Errori");
                Directory.CreateDirectory(TBcartellaxml.Text + "\\Passive");
            }
        }

        private void TBcartellaxml_MouseClick(object sender, MouseEventArgs e)
        {
            FolderBrowserDialog cercaspercorsodb = new FolderBrowserDialog();
            cercaspercorsodb.SelectedPath = Path.GetPathRoot(Environment.SystemDirectory);
            cercaspercorsodb.RootFolder = Environment.SpecialFolder.MyComputer;
            cercaspercorsodb.Description = "Scegliere il percorso di salvataggio fatture.";
            cercaspercorsodb.ShowNewFolderButton = false;
            if (cercaspercorsodb.ShowDialog() == DialogResult.OK) {
                TBcartellaxml.Text = cercaspercorsodb.SelectedPath;
            }
        }
    }
}

