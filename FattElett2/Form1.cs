using AdvancedClient;
using FattElett;
using FatturaEL.v13;
using FirebirdSql.Data.FirebirdClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Serialization;

namespace FattPrintF
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        static List<int> lottilist;
        static Dictionary<int, DateTime> listddt;
        static string progressivoinvio;
        static string FATTcartellaxml;
        static string FATTidddt;
        static string FATTemailcommer;
        static string FATTserversmtp;
        static string FATTportsmtp;
        static string FATTusersmtp;
        static string FATTpasssmtp;
        static string FATTcodpaese;
        static string FATTinvioprogressivo;
        static string FATTditta;
        static string FATTpartitaiva;
        static string FATTcodfis;
        static string FATTregfis;
        static string FATTnome;
        static string FATTcognome;
        static string FATTindirizzo;
        static string FATTncivico;
        static string FATTcap;
        static string FATTcittà;
        static string FATTprov;
        static string FATTnazione;
        static string FATTufficio;
        static string FATTnrea;
        static string FATTcapsoc;
        static string FATTsociorea;
        static string FATTstatoliqui;
        static string FATTtelefono;
        static string FATTeori;
        static string FATTiban;
        static string FATTemail;
        static string userdb;
        static string passdb;
        public static bool isfattura = true;
        public static int nfatt_old = 0;
        public static string fatturecollegate;
        public static string ragionesociale;
        public static string via;
        public static string ncivico;
        public static string cap;
        public static string città;
        public static string codicefiscale;
        public static string partivaiva;
        public static string siglaprov;
        public static string FATTnfatt;
        public static string FATTnnc;
        public static string ncredito;
        public static string codcliente;
        public static string email;
        public static string coddest;
        public static string pec;
        public static string TipoDocumento;
        public static string TipoDato;
        public static string RiferimentoTesto;
        public static string CIG;
        public static string CUP;
        public static string Finemese;
        public static string EsigibilitaIVA;
        public static string ShowPagamento;
        static int lottiriga;
        public static FbConnection connection;
        public static FbConnection connection2;
        public static FbConnection connection3;
        public static bool clientevuoto = false;
        public static RagSoc1 RAGSOC1 = new RagSoc1();
        static AnnFatt ANNFATT = new AnnFatt();
        public static DialogResult result;
        public static DialogResult result2;
        public static string[,] articolidesc;
        public static int TipoPagamento = 0;
        public static int PA = 0;
        public static bool servizio = false;

        public static decimal RepilogoTotaleDocumento = 0.00M;
        public static decimal RepilogoTotaleImponibile = 0.00M;
        public static decimal RepilogoTotaleImposta = 0.00M;

        public static decimal TotaleImponibile = 0.00M;
        public static decimal TotaleDocumento = 0.00M;
        public static decimal TotaleImposta = 0.00M;
        public static decimal TotaleSconti = 0.00M;

        private void Form1_Load(object sender, EventArgs e)
        {
            ExistIni();
            LoadIni();
            bool connopen = false;
            while (connopen == false)
            {
                try
                {
                    LoadDatabase();
                    connopen = true;
                }
                catch (Exception ex)
                {
                    connopen = false;
                    MessageBox.Show("Errore: " + ex.ToString());
                }
            }
            LoadConf();
            AggDatabase();
            servizio = true;
            FbRemoteEvent revent = new FbRemoteEvent(connection);
            revent.AddEvents(new string[] { "new_fatt" });
            revent.RemoteEventCounts += new FbRemoteEventEventHandler(EventCounts);
            revent.QueueEvents();
            notifyIcon1.ShowBalloonTip(2000, "PentaStart - Fatturazione Elettronica", "Servizio Attivo", ToolTipIcon.Info);
            notifyIcon1.ContextMenuStrip = contextMenuStrip1;
        }

        private void ExistIni()
        {
            string iniexist = "C:/trilogis/FattElett.ini";
            IniFile ini = new IniFile();
            if (!System.IO.File.Exists(iniexist))
            {
                File.Create("C:/trilogis/FattElett.ini").Dispose();
                System.IO.Directory.CreateDirectory("C:/FattureXML");
                ini.Load("C:/trilogis/FattElett.ini");
                ini.AddSection("XML");
                ini.AddSection("XML").AddKey("Percorso");
                ini.SetKeyValue("XML", "Percorso", "C://FattureXML");
                ini.AddSection("DDT");
                ini.AddSection("DDT").AddKey("ID");
                ini.SetKeyValue("DDT", "ID", "6");
                ini.AddSection("FATT");
                ini.AddSection("FATT").AddKey("Paese");
                ini.SetKeyValue("FATT", "Paese", "IT");
                ini.AddSection("FATT").AddKey("ProgressivoInvio");
                ini.SetKeyValue("FATT", "ProgressivoInvio", "1");
                ini.AddSection("FATT").AddKey("ProgressivoFatture");
                ini.SetKeyValue("FATT", "ProgressivoFatture", "1");
                ini.AddSection("FATT").AddKey("ProgressivoNotaCredito");
                ini.SetKeyValue("FATT", "ProgressivoNotaCredito", "1");
                ini.AddSection("FATT").AddKey("Ditta");
                ini.SetKeyValue("FATT", "Ditta", "null");
                ini.AddSection("FATT").AddKey("PartitaIva");
                ini.SetKeyValue("FATT", "PartitaIva", "null");
                ini.AddSection("FATT").AddKey("CodFis");
                ini.SetKeyValue("FATT", "CodFis", "null");
                ini.AddSection("FATT").AddKey("RegFis");
                ini.SetKeyValue("FATT", "RegFis", "RF01");
                ini.AddSection("FATT").AddKey("Indirizzo");
                ini.SetKeyValue("FATT", "Indirizzo", "null");
                ini.AddSection("FATT").AddKey("CAP");
                ini.SetKeyValue("FATT", "CAP", "null");
                ini.AddSection("FATT").AddKey("Città");
                ini.SetKeyValue("FATT", "Città", "null");
                ini.AddSection("FATT").AddKey("Provincia");
                ini.SetKeyValue("FATT", "Provincia", "null");
                ini.AddSection("FATT").AddKey("Nazione");
                ini.SetKeyValue("FATT", "Nazione", "IT");
                ini.AddSection("FATT").AddKey("CodiceEORI");
                ini.SetKeyValue("FATT", "CodiceEORI", "null");
                ini.AddSection("FATT").AddKey("IBAN");
                ini.SetKeyValue("FATT", "IBAN", "null");
                ini.AddSection("FATT").AddKey("Telefono");
                ini.SetKeyValue("FATT", "Telefono", "null");
                ini.AddSection("FATT").AddKey("Email");
                ini.SetKeyValue("FATT", "Email", "null");
                ini.AddSection("REA");
                ini.AddSection("REA").AddKey("Ufficio");
                ini.SetKeyValue("REA", "Ufficio", "null");
                ini.AddSection("REA").AddKey("NumeroREA");
                ini.SetKeyValue("REA", "NumeroREA", "null");
                ini.AddSection("REA").AddKey("CapitaleSociale");
                ini.SetKeyValue("REA", "CapitaleSociale", "null");
                ini.AddSection("REA").AddKey("SocioREA");
                ini.SetKeyValue("REA", "SocioREA", "null");
                ini.AddSection("REA").AddKey("StatoLiquidazione");
                ini.SetKeyValue("REA", "StatoLiquidazione", "LN");
                ini.Save("C:/trilogis/FattElett.ini");
            }
        }

        private void AggDatabase()
        {
            int i = 0;
            FbCommand ctr_gen_fatt = new FbCommand("select COUNT(rdb$generator_name) as GENQ from rdb$generators where (rdb$generator_name = 'GEN_ULTIMAFATT')", connection);
            if (Convert.ToInt16(ctr_gen_fatt.ExecuteScalar()) == 0)
            {
                FbCommand sql = new FbCommand("CREATE GENERATOR GEN_ULTIMAFATT", connection);
                sql.ExecuteNonQuery();
                sql = new FbCommand("SET GENERATOR GEN_ULTIMAFATT TO 1", connection);
                sql.ExecuteNonQuery();
                i++;
            }

            FbCommand table_lastfatt = new FbCommand("select COUNT(RDB$RELATION_NAME) from RDB$RELATION_FIELDS where (RDB$RELATION_NAME = 'ULTIMAFATT')", connection);
            if (Convert.ToInt16(table_lastfatt.ExecuteScalar()) == 0)
            {
                FbCommand sql2 = new FbCommand("CREATE TABLE ULTIMAFATT (ID INTEGER,CLIENTEID INTEGER,LOTTOID INTEGER)", connection);
                sql2.ExecuteNonQuery();
                i++;
            }

            FbCommand trigg_genfatt = new FbCommand("SELECT COUNT(RDB$RELATION_NAME) FROM RDB$TRIGGERS WHERE RDB$SYSTEM_FLAG = 0 AND RDB$TRIGGER_NAME='TR_ULTIMAFATT';", connection);
            if (Convert.ToInt16(trigg_genfatt.ExecuteScalar()) == 0)
            {
                FbCommand sql3 = new FbCommand("CREATE TRIGGER TR_ULTIMAFATT FOR ULTIMAFATT ACTIVE BEFORE INSERT POSITION 0 AS BEGIN NEW.ID = GEN_ID(GEN_ULTIMAFATT, 1); END", connection);
                sql3.ExecuteNonQuery();
                i++;
            }

            FbCommand trigg_insertfatt = new FbCommand("SELECT COUNT(RDB$RELATION_NAME) FROM RDB$TRIGGERS WHERE RDB$SYSTEM_FLAG = 0 AND RDB$TRIGGER_NAME='NEW_FATT';", connection);
            if (Convert.ToInt16(trigg_insertfatt.ExecuteScalar()) == 0)
            {
                FbCommand sql4 = new FbCommand("CREATE TRIGGER NEW_FATT FOR DOCUMENTILOTTI ACTIVE AFTER INSERT POSITION 0 AS BEGIN IF(NEW.TIPODOCUMENTOID = '5') THEN BEGIN POST_EVENT 'new_fatt';INSERT INTO ULTIMAFATT(CLIENTEID, LOTTOID) VALUES(NEW.DOCUMENTOLOTTOCLIENTEID, NEW.LOTTOID);END END; ", connection);
                sql4.ExecuteNonQuery();
                i++;
            }

            FbCommand cliente_column1 = new FbCommand("SELECT COUNT(RDB$FIELD_NAME) FROM RDB$RELATION_FIELDS WHERE RDB$RELATION_NAME = 'CLIENTI' AND RDB$FIELD_NAME = 'CLIENTECODICEDESTINATARIO';", connection);
            if (Convert.ToInt16(cliente_column1.ExecuteScalar()) == 0)
            {
                FbCommand sql4 = new FbCommand("ALTER TABLE CLIENTI ADD CLIENTECODICEDESTINATARIO VARCHAR(7);", connection);
                sql4.ExecuteNonQuery();
                i++;
            }

            FbCommand cliente_column2 = new FbCommand("SELECT COUNT(RDB$FIELD_NAME) FROM RDB$RELATION_FIELDS WHERE RDB$RELATION_NAME = 'CLIENTI' AND RDB$FIELD_NAME = 'CLIENTEPEC';", connection);
            if (Convert.ToInt16(cliente_column2.ExecuteScalar()) == 0)
            {
                FbCommand sql6 = new FbCommand("ALTER TABLE CLIENTI ADD CLIENTEPEC VARCHAR(100);", connection);
                sql6.ExecuteNonQuery();
                i++;
            }

            FbCommand cliente_column3 = new FbCommand("SELECT COUNT(RDB$FIELD_NAME) FROM RDB$RELATION_FIELDS WHERE RDB$RELATION_NAME = 'CLIENTI' AND RDB$FIELD_NAME = 'CLIENTEPA';", connection);
            if (Convert.ToInt16(cliente_column3.ExecuteScalar()) == 0)
            {
                FbCommand sql7 = new FbCommand("ALTER TABLE CLIENTI ADD CLIENTEPA INTEGER;", connection);
                sql7.ExecuteNonQuery();
                FbCommand sql8 = new FbCommand("UPDATE CLIENTI SET CLIENTEPA = '0';", connection);
                sql8.ExecuteNonQuery();
                i++;
            }

            FbCommand cliente_column4 = new FbCommand("SELECT COUNT(RDB$FIELD_NAME) FROM RDB$RELATION_FIELDS WHERE RDB$RELATION_NAME = 'CLIENTI' AND RDB$FIELD_NAME = 'CLIENTETIPODOCUMENTO';", connection);
            if (Convert.ToInt16(cliente_column4.ExecuteScalar()) == 0)
            {
                FbCommand sql9 = new FbCommand("ALTER TABLE CLIENTI ADD CLIENTETIPODOCUMENTO VARCHAR(100);", connection);
                sql9.ExecuteNonQuery();
                i++;
            }

            FbCommand cliente_column5 = new FbCommand("SELECT COUNT(RDB$FIELD_NAME) FROM RDB$RELATION_FIELDS WHERE RDB$RELATION_NAME = 'CLIENTI' AND RDB$FIELD_NAME = 'CLIENTETIPODOCUMENTO';", connection);
            if (Convert.ToInt16(cliente_column5.ExecuteScalar()) == 0)
            {
                FbCommand sql10 = new FbCommand("ALTER TABLE CLIENTI ADD CLIENTETIPODOCUMENTO VARCHAR(100);", connection);
                sql10.ExecuteNonQuery();
                MessageBox.Show("Database aggiornato.", "PentaStart");
            }

            FbCommand cliente_column6 = new FbCommand("SELECT COUNT(RDB$FIELD_NAME) FROM RDB$RELATION_FIELDS WHERE RDB$RELATION_NAME = 'CLIENTI' AND RDB$FIELD_NAME = 'CLIENTETIPODATO';", connection);
            if (Convert.ToInt16(cliente_column6.ExecuteScalar()) == 0)
            {
                FbCommand sql11 = new FbCommand("ALTER TABLE CLIENTI ADD CLIENTETIPODATO VARCHAR(100);", connection);
                sql11.ExecuteNonQuery();
                i++;
            }

            FbCommand cliente_column7 = new FbCommand("SELECT COUNT(RDB$FIELD_NAME) FROM RDB$RELATION_FIELDS WHERE RDB$RELATION_NAME = 'CLIENTI' AND RDB$FIELD_NAME = 'CLIENTERIFTEST';", connection);
            if (Convert.ToInt16(cliente_column7.ExecuteScalar()) == 0)
            {
                FbCommand sql12 = new FbCommand("ALTER TABLE CLIENTI ADD CLIENTERIFTEST VARCHAR(100);", connection);
                sql12.ExecuteNonQuery();
                MessageBox.Show("Database aggiornato.", "PentaStart");
            }

            FbCommand cliente_column8 = new FbCommand("SELECT COUNT(RDB$FIELD_NAME) FROM RDB$RELATION_FIELDS WHERE RDB$RELATION_NAME = 'CLIENTI' AND RDB$FIELD_NAME = 'CLIENTECIG';", connection);
            if (Convert.ToInt16(cliente_column8.ExecuteScalar()) == 0)
            {
                FbCommand sql13 = new FbCommand("ALTER TABLE CLIENTI ADD CLIENTECIG VARCHAR(100);", connection);
                sql13.ExecuteNonQuery();
                i++;
            }

            FbCommand cliente_column9 = new FbCommand("SELECT COUNT(RDB$FIELD_NAME) FROM RDB$RELATION_FIELDS WHERE RDB$RELATION_NAME = 'CLIENTI' AND RDB$FIELD_NAME = 'CLIENTECUP';", connection);
            if (Convert.ToInt16(cliente_column9.ExecuteScalar()) == 0)
            {
                FbCommand sql14 = new FbCommand("ALTER TABLE CLIENTI ADD CLIENTECUP VARCHAR(100);", connection);
                sql14.ExecuteNonQuery();
                i++;
            }

            FbCommand cliente_column10 = new FbCommand("SELECT COUNT(RDB$FIELD_NAME) FROM RDB$RELATION_FIELDS WHERE RDB$RELATION_NAME = 'CLIENTI' AND RDB$FIELD_NAME = 'CLIENTESCADENZA';", connection);
            if (Convert.ToInt16(cliente_column10.ExecuteScalar()) == 0)
            {
                FbCommand sql15 = new FbCommand("ALTER TABLE CLIENTI ADD CLIENTESCADENZA VARCHAR(100) DEFAULT '0';", connection);
                sql15.ExecuteNonQuery();
                FbCommand sql16 = new FbCommand("UPDATE CLIENTI SET CLIENTESCADENZA = '0';", connection);
                sql16.ExecuteNonQuery();
                i++;
            }

            FbCommand cliente_column11 = new FbCommand("SELECT COUNT(RDB$FIELD_NAME) FROM RDB$RELATION_FIELDS WHERE RDB$RELATION_NAME = 'CLIENTI' AND RDB$FIELD_NAME = 'CLIENTEESEIVA';", connection);
            if (Convert.ToInt16(cliente_column11.ExecuteScalar()) == 0)
            {
                FbCommand sql17 = new FbCommand("ALTER TABLE CLIENTI ADD CLIENTEESEIVA VARCHAR(2) DEFAULT 'I';", connection);
                sql17.ExecuteNonQuery();
                FbCommand sql18 = new FbCommand("UPDATE CLIENTI SET CLIENTEESEIVA = 'I';", connection);
                sql18.ExecuteNonQuery();
                i++;
            }

            FbCommand cliente_column12 = new FbCommand("SELECT COUNT(RDB$FIELD_NAME) FROM RDB$RELATION_FIELDS WHERE RDB$RELATION_NAME = 'CLIENTI' AND RDB$FIELD_NAME = 'CLIENTEFM';", connection);
            if (Convert.ToInt16(cliente_column12.ExecuteScalar()) == 0)
            {
                FbCommand sql19 = new FbCommand("ALTER TABLE CLIENTI ADD CLIENTEFM VARCHAR(2) DEFAULT 'NO';", connection);
                sql19.ExecuteNonQuery();
                FbCommand sql20 = new FbCommand("UPDATE CLIENTI SET CLIENTEFM = 'NO';", connection);
                sql20.ExecuteNonQuery();
                i++;
            }

            FbCommand cliente_column13 = new FbCommand("SELECT COUNT(RDB$FIELD_NAME) FROM RDB$RELATION_FIELDS WHERE RDB$RELATION_NAME = 'CLIENTI' AND RDB$FIELD_NAME = 'CLIENTEPAGAMENTO';", connection);
            if (Convert.ToInt16(cliente_column13.ExecuteScalar()) == 0)
            {
                FbCommand sql21 = new FbCommand("ALTER TABLE CLIENTI ADD CLIENTEPAGAMENTO VARCHAR(2) DEFAULT '1';", connection);
                sql21.ExecuteNonQuery();
                FbCommand sql22 = new FbCommand("UPDATE CLIENTI SET CLIENTEPAGAMENTO = '0';", connection);
                sql22.ExecuteNonQuery();
                i++;
            }

            FbCommand pag1 = new FbCommand("UPDATE TIPIPAGAMENTI SET TIPOPAGAMENTODESCRIZIONE = 'Contanti',TIPOPAGAMENTOATTIVO = 'True',TIPOPAGAMENTOABILITATO = 'True',TIPOPAGAMENTOIMMEDIATO = 'True' WHERE TIPOPAGAMENTOID = 1 ;", connection);
            pag1.ExecuteNonQuery();

            FbCommand pag2 = new FbCommand("UPDATE TIPIPAGAMENTI SET TIPOPAGAMENTODESCRIZIONE = 'Pagam. Elettronico',TIPOPAGAMENTOATTIVO = 'True',TIPOPAGAMENTOABILITATO = 'True',TIPOPAGAMENTOIMMEDIATO = 'False' WHERE TIPOPAGAMENTOID = 2 ;", connection);
            pag2.ExecuteNonQuery();

            FbCommand pag3 = new FbCommand("UPDATE TIPIPAGAMENTI SET TIPOPAGAMENTODESCRIZIONE = 'Bonifico',TIPOPAGAMENTOATTIVO = 'True',TIPOPAGAMENTOABILITATO = 'True',TIPOPAGAMENTOIMMEDIATO = 'False' WHERE TIPOPAGAMENTOID = 3 ;", connection);
            pag3.ExecuteNonQuery();

            FbCommand pag4 = new FbCommand("UPDATE TIPIPAGAMENTI SET TIPOPAGAMENTODESCRIZIONE = 'Non Riscosso',TIPOPAGAMENTOATTIVO = 'True',TIPOPAGAMENTOABILITATO = 'True',TIPOPAGAMENTOIMMEDIATO = 'False' WHERE TIPOPAGAMENTOID = 4 ;", connection);
            pag4.ExecuteNonQuery();

            FbCommand pag6 = new FbCommand("UPDATE TIPIPAGAMENTI SET TIPOPAGAMENTOABILITATO = 'False' WHERE TIPOPAGAMENTOID > 5 AND TIPOPAGAMENTOID != 899;", connection);
            pag6.ExecuteNonQuery();

            if (i < 0)
                AutoClosingMessageBox.Show("Database aggiornato. Qty Query:" + i + ".", "PentaStart", 5000);
        }

        private void EventCounts(object sender, FbRemoteEventEventArgs args)
        {
            LoadIni();
            ProcArt();
            LoadCliente();
            LoadProgressivo();
            GetTipoPagamento();
            if (result == DialogResult.OK)
            {
                CreaXML();
                AggProgressivo();
                ControlloNotaCredito();
            }
            else
            {
                CancFatt();
            }
        }

        private void ControlloNotaCredito()
        {
            if (!isfattura)
            {
                CancFatt();
            }
        }

        void AvviaServizio()
        {
            servizio = true;
            LoadIni();
            LoadDatabase();
            LoadConf();
            FbRemoteEvent revent = new FbRemoteEvent(connection);
            revent.AddEvents(new string[] { "new_fatt" });
            revent.RemoteEventCounts += new FbRemoteEventEventHandler(EventCounts);
            revent.QueueEvents();
            notifyIcon1.ShowBalloonTip(2000, "PentaStart - Fatturazione Elettronica", "Servizio Attivo", ToolTipIcon.Info);
        }

        void ChiudiServizio()
        {
            servizio = false;
            connection.Close();
            connection2.Close();
            notifyIcon1.ShowBalloonTip(2000, "PentaStart - Fatturazione Elettronica", "Servizio disattivato", ToolTipIcon.Info);
        }

        static void LoadProgressivo()
        {
            string iniexist = "C://trilogis//FattElett.ini";
            IniFile ini = new IniFile();
            if (System.IO.File.Exists(iniexist))
            {
                ini.Load("C://trilogis//FattElett.ini");
                progressivoinvio = ini.GetKeyValue("FATT", "ProgressivoInvio").PadLeft(5, '0'); ;
                ini.Save("C://trilogis//FattElett.ini");
            }
        }

        void AggProgressivo()
        {
            string iniexist = "C://trilogis//FattElett.ini";
            IniFile ini = new IniFile();
            int proxprog = Convert.ToInt32(FATTinvioprogressivo);
            proxprog++;
            if (System.IO.File.Exists(iniexist))
            {
                ini.Load("C://trilogis//FattElett.ini");
                ini.SetKeyValue("FATT", "ProgressivoInvio", proxprog.ToString());

                if (isfattura)
                {
                    int proxfatt = Convert.ToInt32(FATTnfatt);
                    proxfatt++;
                    ini.SetKeyValue("FATT", "ProgressivoFatture", proxfatt.ToString());
                    AutoClosingMessageBox.Show("Fattura N." + FATTnfatt + " - File XML N." + FATTinvioprogressivo + " generato.", "Fatturazione elettronica - PentaStart", 5000);
                    notifyIcon1.ShowBalloonTip(2000, "Fattura N." + FATTnfatt, "File XML N." + FATTinvioprogressivo + " generato.", ToolTipIcon.Info);
                }
                else
                {
                    int proxnc = Convert.ToInt32(FATTnnc);
                    proxnc++;
                    ini.SetKeyValue("FATT", "ProgressivoNotaCredito", proxnc.ToString());
                    AutoClosingMessageBox.Show("Nota di Credito N." + FATTnnc + " - File XML N." + FATTinvioprogressivo + " generato.", "Fatturazione elettronica - PentaStart", 5000);
                    notifyIcon1.ShowBalloonTip(2000, "Nota di Credito N." + FATTnnc, "File XML N." + FATTinvioprogressivo + " generato.", ToolTipIcon.Info);
                }
                ini.Save("C://trilogis//FattElett.ini");
            }
        }

        static void CancFatt()
        {
            FbCommand scontrino = new FbCommand("SELECT COUNT (DISTINCT LOTTORIGAID) FROM LOTTIRIGHE WHERE LOTTOID =(SELECT LOTTOID FROM ULTIMAFATT WHERE ID = (SELECT MAX(ID) FROM ULTIMAFATT))", connection);
            int totale = Convert.ToInt32(scontrino.ExecuteScalar());
            int lottirighecount1 = totale;

            FbCommand artread1 = new FbCommand("SELECT DISTINCT LOTTORIGAID FROM LOTTIRIGHE WHERE LOTTOID =(SELECT LOTTOID FROM ULTIMAFATT WHERE ID = (SELECT MAX(ID) FROM ULTIMAFATT))", connection);
            FbDataReader reader1 = artread1.ExecuteReader();
            string[] lottirighe = new string[totale];
            int pos = 0;
            while (lottirighecount1 > 0)
            {
                if (reader1.Read())
                {
                    lottirighe[pos] = (reader1.GetString(0));
                }
                lottirighecount1--;
                pos++;
            }
            reader1.Close();
            foreach (string s in lottirighe)
            {
                using (FbCommand command = new FbCommand("DELETE FROM DOCUMENTIRIGHE WHERE LOTTORIGAID= '" + s + "'", connection))
                {
                    command.ExecuteNonQuery();
                }
                using (FbCommand command = new FbCommand("DELETE FROM PAGAMENTIRIGHE WHERE LOTTORIGAID= '" + s + "'", connection))
                {
                    command.ExecuteNonQuery();
                }
                using (FbCommand command = new FbCommand("DELETE FROM LOTTIRIGHELAVORAZIONI WHERE LOTTORIGAID= '" + s + "'", connection))
                {
                    command.ExecuteNonQuery();
                }
                using (FbCommand command = new FbCommand("DELETE FROM LOTTIRIGHE WHERE LOTTORIGAID= '" + s + "'", connection))
                {
                    command.ExecuteNonQuery();
                }
            }

            using (FbCommand command = new FbCommand("DELETE FROM DOCUMENTILOTTI WHERE LOTTOID=(SELECT LOTTOID FROM ULTIMAFATT WHERE ID = (SELECT MAX(ID) FROM ULTIMAFATT))", connection))
            {
                command.ExecuteNonQuery();
            }
            using (FbCommand command = new FbCommand("DELETE FROM PAGAMENTILOTTI WHERE LOTTOID=(SELECT LOTTOID FROM ULTIMAFATT WHERE ID = (SELECT MAX(ID) FROM ULTIMAFATT))", connection))
            {
                command.ExecuteNonQuery();
            }
            using (FbCommand command = new FbCommand("DELETE FROM LOTTI WHERE LOTTOID=(SELECT LOTTOID FROM ULTIMAFATT WHERE ID = (SELECT MAX(ID) FROM ULTIMAFATT))", connection))
            {
                command.ExecuteNonQuery();
            }
            string ConnectionString3 = "User ID=" + userdb + ";Password=" + passdb + ";" + "Database=c:/trilogis/trilogisremoteconf.fb20; " + "DataSource=localhost;Charset=NONE;";
            connection3 = new FbConnection(ConnectionString3);
            connection3.Open();
            using (FbCommand command = new FbCommand("UPDATE CONFIGURAZIONE SET VALORE =" + FATTnfatt + " WHERE PAGINAID=4 AND SUBPAGINAID=3 AND LIVELLOID=1 AND POSIZIONE=1", connection3))
            {
                command.ExecuteNonQuery();
            }

        }

        public static List<ElementsFatture> FattureRow;
        public static int TotaleSconto = 0;

        static void ProcArt()
        {
            FbCommand maxdata = new FbCommand("SELECT MAX(DISTINCT DOCUMENTORIGADATA) FROM DOCUMENTIRIGHE WHERE TIPODOCUMENTOID=5", connection);
            DateTime maxdata2 = Convert.ToDateTime(maxdata.ExecuteScalar());
            //MessageBox.Show(maxdata2.ToString());

            FbCommand maxora = new FbCommand("SELECT MAX(DISTINCT DOCUMENTORIGAORA) FROM DOCUMENTIRIGHE WHERE DOCUMENTORIGADATA ='" + maxdata2.ToString("yyyy-MM-dd") + "' AND TIPODOCUMENTOID=5", connection);
            string maxora2 = maxora.ExecuteScalar().ToString();

            FbCommand artread1 = new FbCommand("SELECT DISTINCT LOTTORIGAID FROM DOCUMENTIRIGHE WHERE DOCUMENTORIGADATA ='" + maxdata2.ToString("yyyy-MM-dd") + "' AND DOCUMENTORIGAORA='" + maxora2.ToString() + "' AND TIPODOCUMENTOID=5", connection);
            FbDataReader reader1 = artread1.ExecuteReader();
            List<string> lottirighe = new List<string>();
            while (reader1.Read())
            {
                lottirighe.Add(reader1.GetString(0));
            }
            reader1.Close();

            TotaleSconti = 0;

            FattureRow = new List<ElementsFatture>();
            List<Articolo> articolidesc = new List<Articolo>();
            List<Articolo> alreadyread = new List<Articolo>();
            foreach (string lottoriga in lottirighe)
            {
                FbCommand lineadesc = new FbCommand("SELECT LOTTORIGADESCRIZIONE FROM LOTTIRIGHE WHERE LOTTORIGAID='" + lottoriga + "'", connection);
                //articolidesc[pos, 0] = lineadesc.ExecuteScalar().ToString();

                FbCommand lineaprez = new FbCommand("SELECT LOTTORIGALAVORAZIONEPREZZO FROM LOTTIRIGHELAVORAZIONI WHERE LOTTORIGAID='" + lottoriga + "'", connection);
                //articolidesc[pos, 1] = lineaprez.ExecuteScalar().ToString();

                FbCommand lineascont = new FbCommand("SELECT MODIFLOTTORIGAVALORE FROM MODIFICATORILOTTIRIGHE WHERE LOTTORIGAID='" + lottoriga + "'", connection);
                //articolidesc[pos, 1] = lineaprez.ExecuteScalar().ToString();

                try
                {
                    TotaleSconti = TotaleSconti + Convert.ToDecimal(lineascont.ExecuteScalar().ToString());
                }
                catch (Exception)
                {
                }

                articolidesc.Add(new Articolo { desc = lineadesc.ExecuteScalar().ToString(), prezzo = Convert.ToDecimal(lineaprez.ExecuteScalar()) });
                //MessageBox.Show(totaleparziale.ToString());
            }

            

            int contatore = 0;
            RepilogoTotaleDocumento = 0M;
            RepilogoTotaleImponibile = 0M;
            RepilogoTotaleImposta = 0M;
            foreach (var item in articolidesc)
            {
                if (alreadyread.Any(x => x.desc == item.desc && x.prezzo == item.prezzo))
                {
                    contatore = 0;
                }
                else
                {
                    alreadyread.Add(new Articolo { desc = item.desc, prezzo = item.prezzo });
                    foreach (var item2 in articolidesc)
                    {
                        if (item2.desc == item.desc && item2.prezzo == item.prezzo)
                        {
                            contatore++;
                        }
                    }
                    FattureRow.Add(new ElementsFatture { qty = contatore, desc = item.desc, prezzo = contatore * item.prezzo });
                    RepilogoTotaleDocumento = RepilogoTotaleDocumento + (contatore * item.prezzo);
                    RepilogoTotaleImponibile = RepilogoTotaleImponibile + ((contatore * item.prezzo) / Convert.ToDecimal(1.22));
                    RepilogoTotaleImposta = RepilogoTotaleImposta + (RepilogoTotaleDocumento - RepilogoTotaleImponibile);
                    contatore = 0;
                }
            }
            contatore = 0;

            FbCommand listlottoid = new FbCommand("SELECT LOTTOID FROM DOCUMENTILOTTI WHERE DOCUMENTOLOTTODATA ='" + maxdata2.ToString("yyyy-MM-dd") + "' AND DOCUMENTOLOTTOORA='" + maxora2.ToString() + "' AND TIPODOCUMENTOID='5'", connection);
            FbDataReader readerlottoid = listlottoid.ExecuteReader();
            lottilist = new List<int>();
            while (readerlottoid.Read())
            {
                lottilist.Add(readerlottoid.GetInt32(0));
            }
            readerlottoid.Close();

            listddt = new Dictionary<int, DateTime>();
            foreach (int lotto in lottilist)
            {
                FbCommand fblistddt = new FbCommand("SELECT DOCUMENTOLOTTONUMERO,DOCUMENTOLOTTODATA FROM DOCUMENTILOTTI WHERE LOTTOID='" + lotto.ToString() + "' AND TIPODOCUMENTOID='" + FATTidddt.ToString() + "'", connection);
                FbDataReader readerddt = fblistddt.ExecuteReader();
                while (readerddt.Read())
                {
                    listddt.Add(readerddt.GetInt32(0), readerddt.GetDateTime(1));
                }
                readerddt.Close();

                FbCommand modiflotti = new FbCommand("SELECT MODIFLOTTOVALORE FROM MODIFICATORILOTTI WHERE LOTTOID='" + lotto.ToString() + "'", connection);
                FbDataReader modiflottireader = modiflotti.ExecuteReader();
                while (modiflottireader.Read())
                {
                   TotaleSconti = TotaleSconti + modiflottireader.GetInt32(0);
                }
                modiflottireader.Close();
            }

            if (TotaleSconti != 0)
            {
                if (TotaleSconti < 0)
                {
                    RepilogoTotaleDocumento = RepilogoTotaleDocumento + TotaleSconti;
                    RepilogoTotaleImponibile = RepilogoTotaleImponibile + (TotaleSconti / Convert.ToDecimal(1.22));
                    RepilogoTotaleImposta = RepilogoTotaleDocumento - RepilogoTotaleImponibile;
                    FattureRow.Add(new ElementsFatture { qty = 1, desc = "SCONTO", prezzo = TotaleSconti });
                }
                else if (TotaleSconti >0)
                {
                    RepilogoTotaleDocumento = RepilogoTotaleDocumento + TotaleSconti;
                    RepilogoTotaleImponibile = RepilogoTotaleImponibile + (TotaleSconti / Convert.ToDecimal(1.22));
                    RepilogoTotaleImposta = RepilogoTotaleDocumento - RepilogoTotaleImponibile;
                    FattureRow.Add(new ElementsFatture { qty = 1, desc = "VARIAZIONE", prezzo = TotaleSconti });
                }
                TotaleSconti = 0;
            }
        }

        static void GetTipoPagamento()
        {
            using (FbCommand command = new FbCommand("SELECT TIPOPAGAMENTOID FROM PAGAMENTILOTTI WHERE LOTTOID =" + lottilist[0].ToString(), connection))
            {
                TipoPagamento = Convert.ToInt32(command.ExecuteScalar());
            }
        }

        static void LoadIni()
        {
            string iniexist = "C://trilogis//FattElett.ini";
            IniFile ini = new IniFile();
            if (System.IO.File.Exists(iniexist))
            {
                ini.Load("C://trilogis//FattElett.ini");
                FATTcartellaxml = ini.GetKeyValue("XML", "Percorso");
                if(FATTcartellaxml == "") FATTcartellaxml = "null";
                ini.SetKeyValue("XML", "Percorso", FATTcartellaxml);
                FATTidddt = ini.GetKeyValue("DDT", "ID");
                if (FATTidddt == "") FATTidddt = "6";
                ini.SetKeyValue("DDT", "ID", FATTidddt);
                FATTcodpaese = ini.GetKeyValue("FATT", "Paese");
                if (FATTcodpaese == "") FATTcodpaese = "IT";
                ini.SetKeyValue("FATT", "Paese", FATTcodpaese);
                FATTinvioprogressivo = ini.GetKeyValue("FATT", "ProgressivoInvio");
                if (FATTinvioprogressivo == "") FATTinvioprogressivo = "1";
                ini.SetKeyValue("FATT", "ProgressivoInvio", FATTinvioprogressivo);
                FATTnfatt = ini.GetKeyValue("FATT", "ProgressivoFatture");
                if (FATTnfatt == "") FATTnfatt = "1";
                if (Convert.ToInt32(FATTnfatt) == 1 && nfatt_old > 1)
                {
                    FATTnfatt = nfatt_old.ToString();
                }
                ini.SetKeyValue("FATT", "ProgressivoFatture", FATTnfatt);
                FATTnnc = ini.GetKeyValue("FATT", "ProgressivoNotaCredito");
                if (FATTnnc == "") FATTnnc = "1";
                ini.SetKeyValue("FATT", "ProgressivoNotaCredito", FATTnnc);
                FATTditta = ini.GetKeyValue("FATT", "Ditta");
                if (FATTditta == "") FATTditta = "null";
                ini.SetKeyValue("FATT", "Ditta", FATTditta);
                FATTpartitaiva = ini.GetKeyValue("FATT", "PartitaIva");
                if (FATTpartitaiva == "") FATTpartitaiva = "null";
                ini.SetKeyValue("FATT", "PartitaIva", FATTpartitaiva);
                FATTcodfis = ini.GetKeyValue("FATT", "CodFis");
                if (FATTcodfis == "") FATTcodfis = "null";
                ini.SetKeyValue("FATT", "CodFis", FATTcodfis);
                FATTregfis = ini.GetKeyValue("FATT", "RegFis");
                if (FATTregfis == "") FATTregfis = "RF01";
                ini.SetKeyValue("FATT", "RegFis", FATTregfis);
                FATTindirizzo = ini.GetKeyValue("FATT", "Indirizzo");
                if (FATTindirizzo == "") FATTindirizzo = "null";
                ini.SetKeyValue("FATT", "Indirizzo", FATTindirizzo);
                FATTncivico = ini.GetKeyValue("FATT", "NumeroCivico");
                if (FATTncivico == "") FATTncivico = "null";
                ini.SetKeyValue("FATT", "NumeroCivico", FATTncivico);
                FATTcap = ini.GetKeyValue("FATT", "CAP");
                if (FATTcap == "") FATTcap = "null";
                ini.SetKeyValue("FATT", "CAP", FATTcap);
                FATTcittà = ini.GetKeyValue("FATT", "Città");
                if (FATTcittà == "") FATTcittà = "null";
                ini.SetKeyValue("FATT", "Città", FATTcittà);
                FATTprov = ini.GetKeyValue("FATT", "Provincia");
                if (FATTprov == "") FATTprov = "null";
                ini.SetKeyValue("FATT", "Provincia", FATTprov);
                FATTtelefono = ini.GetKeyValue("FATT", "Telefono");
                if (FATTtelefono == "") FATTtelefono = "null";
                ini.SetKeyValue("FATT", "Telefono", FATTtelefono);
                FATTemail = ini.GetKeyValue("FATT", "Email");
                if (FATTemail == "") FATTemail = "null";
                ini.SetKeyValue("FATT", "Email", FATTemail);
                FATTnazione = ini.GetKeyValue("FATT", "Nazione");
                if (FATTnazione == "") FATTnazione = "IT";
                ini.SetKeyValue("FATT", "Nazione", FATTnazione);
                FATTeori = ini.GetKeyValue("FATT", "CodiceEORI");
                if (FATTeori == "") FATTeori = "null";
                ini.SetKeyValue("FATT", "CodiceEORI", FATTeori);
                FATTiban = ini.GetKeyValue("FATT", "IBAN");
                if (FATTiban == "") FATTiban = "null";
                ini.SetKeyValue("FATT", "IBAN", FATTiban);
                FATTufficio = ini.GetKeyValue("REA", "Ufficio");
                if (FATTufficio == "") FATTufficio = "null";
                ini.SetKeyValue("REA", "Ufficio", FATTufficio);
                FATTnrea = ini.GetKeyValue("REA", "NumeroREA");
                if (FATTnrea == "") FATTnrea = "null";
                ini.SetKeyValue("REA", "NumeroREA", FATTnrea);
                FATTcapsoc = ini.GetKeyValue("REA", "CapitaleSociale");
                if (FATTcapsoc == "") FATTcapsoc = "null";
                ini.SetKeyValue("REA", "CapitaleSociale", FATTcapsoc);
                FATTsociorea = ini.GetKeyValue("REA", "SocioREA");
                if (FATTsociorea == "") FATTsociorea = "null";
                ini.SetKeyValue("REA", "SocioREA", FATTsociorea);
                FATTstatoliqui = ini.GetKeyValue("REA", "StatoLiquidazione");
                if (FATTstatoliqui == "") FATTstatoliqui = "null";
                ini.SetKeyValue("REA", "StatoLiquidazione", FATTstatoliqui);
            }
            else
            {
                MessageBox.Show("Manca configurazione Fatturazione Elettronica.", "Fatturazione Elettronica");
            }
            ini.Save("C://trilogis//FattElett.ini");
            ini.Load("c://trilogis//pentastart.ini");
            userdb = ini.GetKeyValue("DB", "User");
            passdb = ini.GetKeyValue("DB", "Password");
            try
            {
                nfatt_old = Convert.ToInt32(ini.GetKeyValue("FATTEL", "numerofatt"));
            }
            catch (Exception)
            {
                nfatt_old = 0;
            }
            ini.Save("c://trilogis//pentastart.ini");
        }

        static void CreaXML()
        {
            bool esito = false;

            FatturaEL.v13.FatturaElettronica nodoPrincipale = new FatturaEL.v13.FatturaElettronica();
            FatturaElettronicaHeader overviewHeader = new FatturaElettronicaHeader();
            DatiTrasmissione datiTrasmissione = new DatiTrasmissione();
            IdTrasmittente idTrasmittente_111 = new IdTrasmittente();

            idTrasmittente_111.IdPaese = "IT";
            if (FATTcodfis == "" && FATTpartitaiva != "")
            {
                idTrasmittente_111.IdCodice = FATTpartitaiva;
            }
            else if (FATTcodfis != "")
            {
                idTrasmittente_111.IdCodice = FATTcodfis;
            }
            datiTrasmissione.IdTrasmittente = idTrasmittente_111;

            datiTrasmissione.ProgressivoInvio = progressivoinvio;
            if (PA == 0)
            {
                datiTrasmissione.FormatoTrasmissione = "FPR12";
            }
            else if (PA == 1)
            {
                datiTrasmissione.FormatoTrasmissione = "FPA12";
            }
            else
            {
                datiTrasmissione.FormatoTrasmissione = "FPR12";
            }

            if (coddest == "" && pec != "")
            {
                datiTrasmissione.CodiceDestinatario = "0000000";
                datiTrasmissione.PECDestinatario = pec;
            }

            else if (coddest != "")
            {
                datiTrasmissione.CodiceDestinatario = coddest.ToUpper();
            }
            else
            {
                if (PA == 0 || PA == 2)
                {
                    datiTrasmissione.CodiceDestinatario = "0000000";
                }
                else if (PA == 1)
                {
                    datiTrasmissione.CodiceDestinatario = "999999";
                }
                else
                {
                    datiTrasmissione.CodiceDestinatario = "0000000";
                }
            }

            overviewHeader.DatiTrasmissione = datiTrasmissione;

            CedentePrestatore cedentePrestatore = new CedentePrestatore();
            DatiAnagrafici datiAnagrafici_121 = new DatiAnagrafici();
            IdFiscaleIVA idFiscaleIVA_121 = new IdFiscaleIVA();

            idFiscaleIVA_121.IdPaese = "IT";
            idFiscaleIVA_121.IdCodice = FATTpartitaiva;

            datiAnagrafici_121.IdFiscaleIVA = idFiscaleIVA_121;

            datiAnagrafici_121.CodiceFiscale = FATTcodfis;

            Anagrafica anagrafica_121 = new Anagrafica();
            anagrafica_121.Denominazione = FATTditta;
            if (FATTeori != "" && FATTeori != "null")
            {
                anagrafica_121.CodEORI = FATTeori;
            }

            datiAnagrafici_121.Anagrafica = anagrafica_121;

            datiAnagrafici_121.RegimeFiscale = FATTregfis;
            cedentePrestatore.DatiAnagrafici = datiAnagrafici_121;

            Sede sede_122 = new Sede();
            sede_122.Indirizzo = FATTindirizzo;
            sede_122.CAP = FATTcap;
            sede_122.Comune = FATTcittà;
            sede_122.Provincia = FATTprov.ToUpper();
            sede_122.Nazione = FATTnazione.ToUpper();
            cedentePrestatore.Sede = sede_122;

            overviewHeader.CedentePrestatore = cedentePrestatore;


            CessionarioCommittente cessionarioCommittente = new CessionarioCommittente();

            DatiAnagrafici datiAnagrafici_141 = new DatiAnagrafici();
            if (RagSoc1.togglefisica == false)
            {
                IdFiscaleIVA idFiscaleIVA_141 = new IdFiscaleIVA();
                idFiscaleIVA_141.IdPaese = "IT";

                if (partivaiva != "")
                {
                    idFiscaleIVA_141.IdCodice = partivaiva;
                }
                else
                {
                    datiAnagrafici_141.CodiceFiscale = codicefiscale;
                }

                datiAnagrafici_141.IdFiscaleIVA = idFiscaleIVA_141;
            }
            else if (RagSoc1.togglepa == true)
            {
                if (partivaiva != "")
                {
                    IdFiscaleIVA idFiscaleIVA_141 = new IdFiscaleIVA();
                    idFiscaleIVA_141.IdPaese = "IT";

                    if (partivaiva != "")
                    {
                        idFiscaleIVA_141.IdCodice = partivaiva;
                    }
                    datiAnagrafici_141.IdFiscaleIVA = idFiscaleIVA_141;
                }
                else if(codicefiscale != "")
                {
                    datiAnagrafici_141.CodiceFiscale = codicefiscale;
                }
            }
            else
            {

            }

            Anagrafica anagrafica_141 = new Anagrafica();
            anagrafica_141.Denominazione = ragionesociale.Replace("''", "'");
            datiAnagrafici_141.Anagrafica = anagrafica_141;
            cessionarioCommittente.DatiAnagrafici = datiAnagrafici_141;

            Sede sede_142 = new Sede();
            sede_142.Indirizzo = via;
            if (ncivico != "" || ncivico != " ")
            {
                sede_142.NumeroCivico = ncivico;
            }
            sede_142.CAP = cap;
            sede_142.Comune = città;
            sede_142.Provincia = siglaprov.ToUpper();
            sede_142.Nazione = "IT";
            cessionarioCommittente.Sede = sede_142;
            overviewHeader.CessionarioCommittente = cessionarioCommittente;

            FatturaElettronicaBody overviewBody = new FatturaElettronicaBody();



            DatiBeniServizi datiBeniServizi = new DatiBeniServizi();

            List<DettaglioLinee> dettaglioLineeList = new List<DettaglioLinee>();

            string datafattura = DateTime.Today.ToString("dd/MM/yy");
            string numerofattura = FATTnfatt;

            int contatore = 0;
            foreach (var item in FattureRow)
            {
                contatore++;
                DettaglioLinee dettaglioLinee = new DettaglioLinee();
                dettaglioLinee.NumeroLinea = contatore.ToString();
                dettaglioLinee.Descrizione = item.desc.ToString();
                dettaglioLinee.Quantita = item.qty.ToString("0.00").Replace(",", ".");
                dettaglioLinee.UnitaMisura = "Pz";
                //double prezzo = (Convert.ToDouble(articolidesc[f, 1]) / 1.22);
                //double iva = (Convert.ToDouble(articolidesc[f, 1]) - (Convert.ToDouble(articolidesc[f, 1]) / 1.22));
                //TotaleDocumento = TotaleDocumento + Convert.ToDouble(articolidesc[f, 1]);
                ////MessageBox.Show(prezzo.ToString("0.0000"));
                ////MessageBox.Show(iva.ToString("0.0000"));
                //string prezzounit = Math.Round(prezzo, 2).ToString("0.00");
                //string ivaunit = Math.Round(iva, 2).ToString("0.00");
                //TotaleImponibile = TotaleImponibile + prezzo;
                //TotaleImposta = TotaleImposta + iva;

                TotaleImponibile = TotaleImponibile + (item.prezzo / Convert.ToDecimal(1.22));

                string prezzounit = ((item.prezzo / item.qty) / Convert.ToDecimal(1.22)).ToString("0.00000000");
                string prezzotot = (item.prezzo / Convert.ToDecimal(1.22)).ToString("0.00000000");
                TotaleDocumento = TotaleDocumento + (item.prezzo);

                dettaglioLinee.PrezzoUnitario = prezzounit.Replace(',', '.');
                dettaglioLinee.PrezzoTotale = prezzotot.Replace(',', '.');
                dettaglioLinee.AliquotaIVA = "22.00";
                if (TipoDato != "" && RiferimentoTesto != "")
                {
                    AltriDatiGestionali altriDatiGestionali = new AltriDatiGestionali();
                    altriDatiGestionali.TipoDato = TipoDato;
                    altriDatiGestionali.RiferimentoTesto = RiferimentoTesto;
                    dettaglioLinee.AltriDatiGestionali = altriDatiGestionali;
                }
                dettaglioLineeList.Add(dettaglioLinee);
            }
            TotaleImposta = TotaleDocumento - TotaleImponibile;

            datiBeniServizi.DettaglioLinee = dettaglioLineeList;


            DatiGenerali datiGenerali = new DatiGenerali();

            DatiGeneraliDocumento datiGeneraliDocumento = new DatiGeneraliDocumento();
            if (isfattura)
            {
                datiGeneraliDocumento.TipoDocumento = "TD01";
                datiGeneraliDocumento.Numero = FATTnfatt;
            }
            else
            {
                datiGeneraliDocumento.TipoDocumento = "TD04";
                datiGeneraliDocumento.Numero = "NC-" + FATTnnc;
            }
            datiGeneraliDocumento.Divisa = "EUR";
            datiGeneraliDocumento.Data = DateTime.Today.ToString("yyyy-MM-dd");
            
            if (RAGSOC1.richTextBox1.Text != "" && RAGSOC1.richTextBox1.Text != " ")
            {
                datiGeneraliDocumento.Causale = RAGSOC1.richTextBox1.Text;
            }
            datiGeneraliDocumento.ImportoTotaleDocumento = TotaleDocumento.ToString("0.00").Replace(',', '.');
            datiGenerali.DatiGeneraliDocumento = datiGeneraliDocumento;

            if (TipoDocumento != "" || CUP != "" || CIG != "")
            {
                DatiOrdineAcquisto datiOrdineAcquisto = new DatiOrdineAcquisto();
                List<DatiOrdineAcquisto> datiOrdineAcquistoList = new List<DatiOrdineAcquisto>();
                if (TipoDocumento != "")
                {
                    datiOrdineAcquisto.IdDocumento = TipoDocumento;
                }

                if (CUP != "")
                {
                    datiOrdineAcquisto.CodiceCUP = CUP;
                }

                if (CIG != "")
                {
                    datiOrdineAcquisto.CodiceCIG = CIG;
                }
                datiOrdineAcquistoList.Add(datiOrdineAcquisto);
                datiGenerali.DatiOrdineAcquisto = datiOrdineAcquistoList;
            }

            if (isfattura == false && fatturecollegate != "")
            {
                DatiFattureCollegate datiFattureCollegate = new DatiFattureCollegate();
                List<DatiFattureCollegate> datiFattureCollegateList = new List<DatiFattureCollegate>();

                datiFattureCollegate.IdDocumento = fatturecollegate;

                datiFattureCollegateList.Add(datiFattureCollegate);
                datiGenerali.DatiFattureCollegate = datiFattureCollegateList;
            }

            if (listddt.Count > 0)
            {
                List<DatiDDT> datiDDTList = new List<DatiDDT>();
                foreach (var ddt in listddt)
                {
                    DatiDDT datiDDT = new DatiDDT();
                    datiDDT.NumeroDDT = ddt.Key.ToString();
                    datiDDT.DataDDT = ddt.Value.ToString("yyyy-MM-dd");
                    datiDDTList.Add(datiDDT);
                }
                datiGenerali.DatiDDT = datiDDTList;
            }
            listddt.Clear();

            overviewBody.DatiGenerali = datiGenerali;

            DatiRiepilogo datiRiepilogo = new DatiRiepilogo();
            List<DatiRiepilogo> datiRiepilogoList = new List<DatiRiepilogo>();
            datiRiepilogo.AliquotaIVA = "22.00";
            datiRiepilogo.ImponibileImporto = TotaleImponibile.ToString("0.00").Replace(',', '.');
            datiRiepilogo.Imposta = TotaleImposta.ToString("0.00").Replace(',', '.');
            datiRiepilogo.EsigibilitaIVA = EsigibilitaIVA;
            datiRiepilogoList.Add(datiRiepilogo);
            datiBeniServizi.DatiRiepilogo = datiRiepilogoList;

            overviewBody.DatiBeniServizi = datiBeniServizi;
            if (TipoPagamento == 2)
            {
                DatiPagamento datiPagamento = new DatiPagamento();
                datiPagamento.CondizioniPagamento = "TP02";
                DettaglioPagamento dettaglioPagamento = new DettaglioPagamento();
                dettaglioPagamento.ModalitaPagamento = "MP05";
                DateTime datascadenza = DateTime.Today;
                DateTime finemese2 = new DateTime(datascadenza.Year, datascadenza.Month, 1).AddMonths(1).AddDays(-1);
                dettaglioPagamento.ImportoPagamento = TotaleDocumento.ToString("0.00").Replace(',', '.');
                if (FATTiban != "" || FATTiban != "null")
                {
                    dettaglioPagamento.IBAN = FATTiban;
                }
                datiPagamento.DettaglioPagamento = dettaglioPagamento;
                overviewBody.DatiPagamento = datiPagamento;
            }

            nodoPrincipale.FatturaElettronicaHeader = overviewHeader;
            nodoPrincipale.FatturaElettronicaBody = overviewBody;

            XmlRootAttribute XmlRoot = new XmlRootAttribute();
            XmlRoot.Namespace = "http://www.fatturapa.gov.it/sdi/fatturapa/v1.2";
            XmlAttributes myxmlAttribute = new XmlAttributes();
            myxmlAttribute.XmlRoot = XmlRoot;
            XmlAttributeOverrides xmlAttributeOverrides = new XmlAttributeOverrides();
            xmlAttributeOverrides.Add(typeof(FatturaEL.v13.FatturaElettronica), myxmlAttribute);

            XmlAttributes emptyNsAttribute = new XmlAttributes();
            XmlElementAttribute xElement1 = new XmlElementAttribute();
            xElement1.Namespace = "";
            emptyNsAttribute.XmlElements.Add(xElement1);
            xmlAttributeOverrides.Add(typeof(FatturaEL.v13.FatturaElettronica), "FatturaElettronicaHeader", emptyNsAttribute);
            xmlAttributeOverrides.Add(typeof(FatturaEL.v13.FatturaElettronica), "FatturaElettronicaBody", emptyNsAttribute);
            TotaleSconti = 0;
            TotaleDocumento = 0;
            TotaleImponibile = 0;
            TotaleImposta = 0;

            if (PA == 0)
            {
                nodoPrincipale.versione = "FPR12";
            }
            else if (PA == 1)
            {
                nodoPrincipale.versione = "FPA12";
            }
            else
            {
                nodoPrincipale.versione = "FPR12";
            }

            XmlSerializer ser = new XmlSerializer(nodoPrincipale.GetType(), xmlAttributeOverrides);
            ser = new XmlSerializer(nodoPrincipale.GetType(), new XmlRootAttribute("pX"));

            XmlSerializerNamespaces ns = new XmlSerializerNamespaces();
            ns.Add("ds", "http://www.w3.org/2000/09/xmldsig#");
            ns.Add("p", "http://ivaservizi.agenziaentrate.gov.it/docs/xsd/fatture/v1.2");
            ns.Add("xsi", "http://www.w3.org/2001/XMLSchema-instance");

            string path = "";
            string path2 = "";
            if (FATTcodfis == "" && FATTpartitaiva != "")
            {
                path = @FATTcartellaxml + "//IT" + FATTpartitaiva + "_" + progressivoinvio.PadLeft(5, '0') + ".xml";
                path2 = @FATTcartellaxml + "-Backup//IT" + FATTpartitaiva + "_" + progressivoinvio.PadLeft(5, '0') + ".xml";
            }
            else if (FATTcodfis != "")
            {
                path = @FATTcartellaxml + "//IT" + FATTcodfis + "_" + progressivoinvio.PadLeft(5, '0') + ".xml";
                path2 = @FATTcartellaxml + "-Backup//IT" + FATTpartitaiva + "_" + progressivoinvio.PadLeft(5, '0') + ".xml";
            }
            FileStream file;
            try
            {
                file = System.IO.File.Create(path);
            }
            catch (Exception)
            {
                if (Directory.Exists(path) == false)
                {
                    Directory.CreateDirectory(@FATTcartellaxml);
                }
                file = System.IO.File.Create(path);
            }

            ser.Serialize(new StreamWriter(file, new System.Text.UTF8Encoding()), nodoPrincipale, ns);
            file.Close();

            String delimiterToBeReplaced = "pX";
            String newDelimiter = "p:FatturaElettronica";
            String contents = System.IO.File.ReadAllText(path);
            contents = contents.Replace(delimiterToBeReplaced, newDelimiter);
            File.WriteAllText(path, contents);

            try
            {
                file = System.IO.File.Create(path2);
            }
            catch (Exception)
            {
                if (Directory.Exists(path) == false)
                {
                    Directory.CreateDirectory(@FATTcartellaxml + "-Backup");
                }
                file = System.IO.File.Create(path2);
            }
            ser.Serialize(new StreamWriter(file, new System.Text.UTF8Encoding()), nodoPrincipale, ns);
            file.Close();

            String delimiterToBeReplaced2 = "pX";
            String newDelimiter2 = "p:FatturaElettronica";
            String contents2 = System.IO.File.ReadAllText(path2);
            contents2 = contents2.Replace(delimiterToBeReplaced2, newDelimiter2);
            File.WriteAllText(path2, contents2);
        }

        public static FbConnection LoadDatabase()
        {
            string ConnectionString = "User ID=" + userdb + ";Password=" + passdb + ";" + "Database=c:/trilogis/trilogis.fb20; " + "DataSource=localhost;Charset=NONE;";
            connection = new FbConnection(ConnectionString);
            connection.Open();
            return connection;
        }

        public static FbConnection LoadConf()
        {
            string ConnectionString2 = "User ID=" + userdb + ";Password=" + passdb + ";" + "Database=c:/trilogis/trilogislocalconf.fb20; " + "DataSource=localhost;Charset=NONE;";
            connection2 = new FbConnection(ConnectionString2);
            connection2.Open();
            return connection2;
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

        private static void LoadCliente()
        {
            AdvancedClient.AdvancedClient infocliente = new AdvancedClient.AdvancedClient();
            infocliente.RefId = "SELECT CLIENTEID FROM ULTIMAFATT WHERE ID = (SELECT MAX(ID) FROM ULTIMAFATT)";
            codcliente = infocliente.ID(connection);
            ragionesociale = infocliente.RagSoc(connection);
            via = infocliente.Indir(connection);
            ncivico = infocliente.NCivico(connection);
            cap = infocliente.CAP(connection);
            città = infocliente.Citta(connection);
            siglaprov = infocliente.Prov(connection);
            partivaiva = infocliente.PartitaIva(connection);
            codicefiscale = infocliente.CodiceFiscale(connection);
            coddest = infocliente.CodiceDestinatario(connection);
            pec = infocliente.PEC(connection);
            PA = infocliente.PA(connection);
            TipoDocumento = infocliente.TipoDocumento(connection);
            TipoDato = infocliente.TipoDato(connection);
            RiferimentoTesto = infocliente.RiferimentoTesto(connection);
            CIG = infocliente.CIG(connection);
            CUP = infocliente.CUP(connection);
            Finemese = infocliente.Finemese(connection);
            EsigibilitaIVA = infocliente.EsigibilitaIVA(connection);
            ShowPagamento = infocliente.ShowPagamento(connection);
            isfattura = true;
            result = RAGSOC1.ShowDialog();
        }

        private void avviaServizioToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (servizio != true)
            {
                AvviaServizio();
            }
        }

        private void notifyIcon1_MouseClick(object sender, MouseEventArgs e)
        {
            if (servizio != true)
            {
                contextMenuStrip1.Items[0].Enabled = true;
                contextMenuStrip1.Items[1].Enabled = false;
            }
            else if (servizio == true)
            {
                contextMenuStrip1.Items[0].Enabled = false;
                contextMenuStrip1.Items[1].Enabled = true;
            }
        }

        private void apriCartellaXMLToolStripMenuItem_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start("explorer.exe", @FATTcartellaxml);
        }

        private void chiudiToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void chiudiServizioToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (servizio != false)
            {
                ChiudiServizio();
            }
        }

        private void ShowToolTip(object sender, string message)
        {
            new ToolTip().Show(message, this, Cursor.Position.X - this.Location.X, Cursor.Position.Y - this.Location.Y, 1000);
        }

        private void ImpostazioneToolStripMenuItem_Click(object sender, EventArgs e)
        { 
            Impostazione impostazione2 = new Impostazione();
            impostazione2.ShowDialog();
        }
    }
}