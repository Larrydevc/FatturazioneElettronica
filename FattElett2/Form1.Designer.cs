namespace FattPrintF
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.notifyIcon1 = new System.Windows.Forms.NotifyIcon(this.components);
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.avviaServizioToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.chiudiServizioToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.apriCartellaXMLToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.impostazioneToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.chiudiToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.contextMenuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // notifyIcon1
            // 
            this.notifyIcon1.BalloonTipIcon = System.Windows.Forms.ToolTipIcon.Info;
            this.notifyIcon1.Icon = ((System.Drawing.Icon)(resources.GetObject("notifyIcon1.Icon")));
            this.notifyIcon1.Text = "Fatt. Elett. PENTA";
            this.notifyIcon1.Visible = true;
            this.notifyIcon1.MouseClick += new System.Windows.Forms.MouseEventHandler(this.notifyIcon1_MouseClick);
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.avviaServizioToolStripMenuItem,
            this.chiudiServizioToolStripMenuItem,
            this.apriCartellaXMLToolStripMenuItem,
            this.impostazioneToolStripMenuItem,
            this.chiudiToolStripMenuItem});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(167, 114);
            // 
            // avviaServizioToolStripMenuItem
            // 
            this.avviaServizioToolStripMenuItem.Name = "avviaServizioToolStripMenuItem";
            this.avviaServizioToolStripMenuItem.Size = new System.Drawing.Size(166, 22);
            this.avviaServizioToolStripMenuItem.Text = "Avvia Servizio";
            this.avviaServizioToolStripMenuItem.Click += new System.EventHandler(this.avviaServizioToolStripMenuItem_Click);
            // 
            // chiudiServizioToolStripMenuItem
            // 
            this.chiudiServizioToolStripMenuItem.Name = "chiudiServizioToolStripMenuItem";
            this.chiudiServizioToolStripMenuItem.Size = new System.Drawing.Size(166, 22);
            this.chiudiServizioToolStripMenuItem.Text = "Chiudi Servizio";
            this.chiudiServizioToolStripMenuItem.Click += new System.EventHandler(this.chiudiServizioToolStripMenuItem_Click);
            // 
            // apriCartellaXMLToolStripMenuItem
            // 
            this.apriCartellaXMLToolStripMenuItem.Name = "apriCartellaXMLToolStripMenuItem";
            this.apriCartellaXMLToolStripMenuItem.Size = new System.Drawing.Size(166, 22);
            this.apriCartellaXMLToolStripMenuItem.Text = "Apri Cartella XML";
            this.apriCartellaXMLToolStripMenuItem.Click += new System.EventHandler(this.apriCartellaXMLToolStripMenuItem_Click);
            // 
            // impostazioneToolStripMenuItem
            // 
            this.impostazioneToolStripMenuItem.Name = "impostazioneToolStripMenuItem";
            this.impostazioneToolStripMenuItem.Size = new System.Drawing.Size(166, 22);
            this.impostazioneToolStripMenuItem.Text = "Impostazione";
            this.impostazioneToolStripMenuItem.Click += new System.EventHandler(this.ImpostazioneToolStripMenuItem_Click);
            // 
            // chiudiToolStripMenuItem
            // 
            this.chiudiToolStripMenuItem.Name = "chiudiToolStripMenuItem";
            this.chiudiToolStripMenuItem.Size = new System.Drawing.Size(166, 22);
            this.chiudiToolStripMenuItem.Text = "Chiudi";
            this.chiudiToolStripMenuItem.Click += new System.EventHandler(this.chiudiToolStripMenuItem_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(272, 22);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(2);
            this.Name = "Form1";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Fatt. Elett.";
            this.WindowState = System.Windows.Forms.FormWindowState.Minimized;
            this.Load += new System.EventHandler(this.Form1_Load);
            this.contextMenuStrip1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.NotifyIcon notifyIcon1;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem avviaServizioToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem chiudiServizioToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem apriCartellaXMLToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem chiudiToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem impostazioneToolStripMenuItem;
    }
}

