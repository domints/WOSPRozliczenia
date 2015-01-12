/*
 * Created by SharpDevelop.
 * User: Szymański
 * Date: 2015-01-09
 * Time: 03:06
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
namespace WOSP
{
	partial class MainForm
	{
		/// <summary>
		/// Designer variable used to keep track of non-visual components.
		/// </summary>
		private System.ComponentModel.IContainer components = null;
		private System.Windows.Forms.MenuStrip menuStrip1;
		private System.Windows.Forms.ToolStripMenuItem plikToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem zamknijToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem ustawieniaDrukarkiToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem ustawieniaKalibracjiToolStripMenuItem;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.TextBox idBox;
		private System.Windows.Forms.TextBox nameBox;
		private System.Windows.Forms.TextBox countBox;
		private System.Windows.Forms.PrintDialog printDialog1;
		private System.Windows.Forms.StatusStrip statusStrip1;
		private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel1;
		private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel2;
		private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel4;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.CheckBox checkBox1;
		private System.Windows.Forms.Button button2;
		
		/// <summary>
		/// Disposes resources used by the form.
		/// </summary>
		/// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		protected override void Dispose(bool disposing)
		{
			if (disposing) {
				if (components != null) {
					components.Dispose();
				}
			}
			base.Dispose(disposing);
		}
		
		/// <summary>
		/// This method is required for Windows Forms designer support.
		/// Do not change the method contents inside the source code editor. The Forms designer might
		/// not be able to load this method if it was changed manually.
		/// </summary>
		private void InitializeComponent()
		{
			this.menuStrip1 = new System.Windows.Forms.MenuStrip();
			this.plikToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.zamknijToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.ustawieniaDrukarkiToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.ustawieniaKalibracjiToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.label1 = new System.Windows.Forms.Label();
			this.label2 = new System.Windows.Forms.Label();
			this.label3 = new System.Windows.Forms.Label();
			this.idBox = new System.Windows.Forms.TextBox();
			this.nameBox = new System.Windows.Forms.TextBox();
			this.countBox = new System.Windows.Forms.TextBox();
			this.printDialog1 = new System.Windows.Forms.PrintDialog();
			this.statusStrip1 = new System.Windows.Forms.StatusStrip();
			this.toolStripStatusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
			this.toolStripStatusLabel2 = new System.Windows.Forms.ToolStripStatusLabel();
			this.toolStripStatusLabel4 = new System.Windows.Forms.ToolStripStatusLabel();
			this.toolStripStatusLabel5 = new System.Windows.Forms.ToolStripStatusLabel();
			this.toolStripStatusLabel6 = new System.Windows.Forms.ToolStripStatusLabel();
			this.toolStripStatusLabel3 = new System.Windows.Forms.ToolStripStatusLabel();
			this.toolStripStatusLabel8 = new System.Windows.Forms.ToolStripStatusLabel();
			this.toolStripStatusLabel7 = new System.Windows.Forms.ToolStripStatusLabel();
			this.toolStripStatusLabel9 = new System.Windows.Forms.ToolStripStatusLabel();
			this.label4 = new System.Windows.Forms.Label();
			this.checkBox1 = new System.Windows.Forms.CheckBox();
			this.button2 = new System.Windows.Forms.Button();
			this.menuStrip1.SuspendLayout();
			this.statusStrip1.SuspendLayout();
			this.SuspendLayout();
			// 
			// menuStrip1
			// 
			this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
									this.plikToolStripMenuItem,
									this.ustawieniaDrukarkiToolStripMenuItem,
									this.ustawieniaKalibracjiToolStripMenuItem});
			this.menuStrip1.Location = new System.Drawing.Point(0, 0);
			this.menuStrip1.Name = "menuStrip1";
			this.menuStrip1.Size = new System.Drawing.Size(808, 24);
			this.menuStrip1.TabIndex = 0;
			this.menuStrip1.Text = "menuStrip1";
			// 
			// plikToolStripMenuItem
			// 
			this.plikToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
									this.zamknijToolStripMenuItem});
			this.plikToolStripMenuItem.Name = "plikToolStripMenuItem";
			this.plikToolStripMenuItem.Size = new System.Drawing.Size(34, 20);
			this.plikToolStripMenuItem.Text = "Plik";
			// 
			// zamknijToolStripMenuItem
			// 
			this.zamknijToolStripMenuItem.Name = "zamknijToolStripMenuItem";
			this.zamknijToolStripMenuItem.Size = new System.Drawing.Size(121, 22);
			this.zamknijToolStripMenuItem.Text = "Zamknij";
			this.zamknijToolStripMenuItem.Click += new System.EventHandler(this.ZamknijToolStripMenuItemClick);
			// 
			// ustawieniaDrukarkiToolStripMenuItem
			// 
			this.ustawieniaDrukarkiToolStripMenuItem.Name = "ustawieniaDrukarkiToolStripMenuItem";
			this.ustawieniaDrukarkiToolStripMenuItem.Size = new System.Drawing.Size(112, 20);
			this.ustawieniaDrukarkiToolStripMenuItem.Text = "Ustawienia drukarki";
			this.ustawieniaDrukarkiToolStripMenuItem.Click += new System.EventHandler(this.UstawieniaDrukarkiToolStripMenuItemClick);
			// 
			// ustawieniaKalibracjiToolStripMenuItem
			// 
			this.ustawieniaKalibracjiToolStripMenuItem.Name = "ustawieniaKalibracjiToolStripMenuItem";
			this.ustawieniaKalibracjiToolStripMenuItem.Size = new System.Drawing.Size(115, 20);
			this.ustawieniaKalibracjiToolStripMenuItem.Text = "Ustawienia kalibracji";
			this.ustawieniaKalibracjiToolStripMenuItem.Click += new System.EventHandler(this.UstawieniaKalibracjiToolStripMenuItemClick);
			// 
			// label1
			// 
			this.label1.Location = new System.Drawing.Point(12, 43);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(100, 23);
			this.label1.TabIndex = 1;
			this.label1.Text = "Nr identyfikatora:";
			// 
			// label2
			// 
			this.label2.Location = new System.Drawing.Point(12, 80);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(100, 23);
			this.label2.TabIndex = 2;
			this.label2.Text = "Imię i nazwisko:";
			// 
			// label3
			// 
			this.label3.Location = new System.Drawing.Point(12, 117);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(100, 23);
			this.label3.TabIndex = 3;
			this.label3.Text = "Kwota:";
			// 
			// idBox
			// 
			this.idBox.Location = new System.Drawing.Point(118, 40);
			this.idBox.Name = "idBox";
			this.idBox.Size = new System.Drawing.Size(301, 20);
			this.idBox.TabIndex = 4;
			this.idBox.TextChanged += new System.EventHandler(this.IdBoxTextChanged);
			this.idBox.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.IdBoxKeyPress);
			// 
			// nameBox
			// 
			this.nameBox.Location = new System.Drawing.Point(118, 77);
			this.nameBox.Name = "nameBox";
			this.nameBox.Size = new System.Drawing.Size(301, 20);
			this.nameBox.TabIndex = 5;
			this.nameBox.Enter += new System.EventHandler(this.NameBoxEnter);
			this.nameBox.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.NameBoxKeyPress);
			// 
			// countBox
			// 
			this.countBox.Location = new System.Drawing.Point(118, 114);
			this.countBox.Name = "countBox";
			this.countBox.Size = new System.Drawing.Size(278, 20);
			this.countBox.TabIndex = 6;
			this.countBox.TextChanged += new System.EventHandler(this.CountBoxTextChanged);
			this.countBox.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.CountBoxKeyPress);
			// 
			// printDialog1
			// 
			this.printDialog1.UseEXDialog = true;
			// 
			// statusStrip1
			// 
			this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
									this.toolStripStatusLabel1,
									this.toolStripStatusLabel2,
									this.toolStripStatusLabel4,
									this.toolStripStatusLabel5,
									this.toolStripStatusLabel6,
									this.toolStripStatusLabel3,
									this.toolStripStatusLabel8,
									this.toolStripStatusLabel7,
									this.toolStripStatusLabel9});
			this.statusStrip1.Location = new System.Drawing.Point(0, 276);
			this.statusStrip1.Name = "statusStrip1";
			this.statusStrip1.Size = new System.Drawing.Size(808, 22);
			this.statusStrip1.TabIndex = 8;
			this.statusStrip1.Text = "statusStrip1";
			// 
			// toolStripStatusLabel1
			// 
			this.toolStripStatusLabel1.Name = "toolStripStatusLabel1";
			this.toolStripStatusLabel1.Size = new System.Drawing.Size(78, 17);
			this.toolStripStatusLabel1.Text = "Drukarka: Brak";
			// 
			// toolStripStatusLabel2
			// 
			this.toolStripStatusLabel2.Name = "toolStripStatusLabel2";
			this.toolStripStatusLabel2.Size = new System.Drawing.Size(70, 17);
			this.toolStripStatusLabel2.Text = "x = 0; y = 0;";
			// 
			// toolStripStatusLabel4
			// 
			this.toolStripStatusLabel4.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
			this.toolStripStatusLabel4.Name = "toolStripStatusLabel4";
			this.toolStripStatusLabel4.Size = new System.Drawing.Size(29, 17);
			this.toolStripStatusLabel4.Text = "0,00";
			// 
			// toolStripStatusLabel5
			// 
			this.toolStripStatusLabel5.Name = "toolStripStatusLabel5";
			this.toolStripStatusLabel5.Size = new System.Drawing.Size(29, 17);
			this.toolStripStatusLabel5.Text = "0,00";
			// 
			// toolStripStatusLabel6
			// 
			this.toolStripStatusLabel6.Name = "toolStripStatusLabel6";
			this.toolStripStatusLabel6.Size = new System.Drawing.Size(29, 17);
			this.toolStripStatusLabel6.Text = "0,00";
			// 
			// toolStripStatusLabel3
			// 
			this.toolStripStatusLabel3.Name = "toolStripStatusLabel3";
			this.toolStripStatusLabel3.Size = new System.Drawing.Size(45, 17);
			this.toolStripStatusLabel3.Text = "Rekord:";
			// 
			// toolStripStatusLabel8
			// 
			this.toolStripStatusLabel8.Name = "toolStripStatusLabel8";
			this.toolStripStatusLabel8.Size = new System.Drawing.Size(28, 17);
			this.toolStripStatusLabel8.Text = "Brak";
			// 
			// toolStripStatusLabel7
			// 
			this.toolStripStatusLabel7.Name = "toolStripStatusLabel7";
			this.toolStripStatusLabel7.Size = new System.Drawing.Size(29, 17);
			this.toolStripStatusLabel7.Text = "0,00";
			// 
			// toolStripStatusLabel9
			// 
			this.toolStripStatusLabel9.Name = "toolStripStatusLabel9";
			this.toolStripStatusLabel9.Size = new System.Drawing.Size(60, 17);
			this.toolStripStatusLabel9.Text = "Wydane: 0";
			// 
			// label4
			// 
			this.label4.Location = new System.Drawing.Point(402, 117);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(17, 23);
			this.label4.TabIndex = 9;
			this.label4.Text = "zł";
			// 
			// checkBox1
			// 
			this.checkBox1.Location = new System.Drawing.Point(12, 175);
			this.checkBox1.Name = "checkBox1";
			this.checkBox1.Size = new System.Drawing.Size(104, 24);
			this.checkBox1.TabIndex = 10;
			this.checkBox1.Text = "Drukowanie";
			this.checkBox1.UseVisualStyleBackColor = true;
			// 
			// button2
			// 
			this.button2.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
			this.button2.Location = new System.Drawing.Point(12, 205);
			this.button2.Name = "button2";
			this.button2.Size = new System.Drawing.Size(488, 57);
			this.button2.TabIndex = 11;
			this.button2.Text = "Drukuj";
			this.button2.UseVisualStyleBackColor = true;
			this.button2.Click += new System.EventHandler(this.Button2Click);
			// 
			// MainForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(808, 298);
			this.Controls.Add(this.button2);
			this.Controls.Add(this.checkBox1);
			this.Controls.Add(this.label4);
			this.Controls.Add(this.statusStrip1);
			this.Controls.Add(this.countBox);
			this.Controls.Add(this.nameBox);
			this.Controls.Add(this.idBox);
			this.Controls.Add(this.label3);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.menuStrip1);
			this.MainMenuStrip = this.menuStrip1;
			this.Name = "MainForm";
			this.Text = "WOSP";
			this.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.MainFormKeyPress);
			this.menuStrip1.ResumeLayout(false);
			this.menuStrip1.PerformLayout();
			this.statusStrip1.ResumeLayout(false);
			this.statusStrip1.PerformLayout();
			this.ResumeLayout(false);
			this.PerformLayout();
		}
		private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel9;
		private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel8;
		private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel7;
		private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel3;
		private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel6;
		private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel5;
	}
}
