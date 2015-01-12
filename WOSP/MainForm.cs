/*
 * Created by SharpDevelop.
 * User: Szymański
 * Date: 2015-01-09
 * Time: 03:06
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Globalization;
using System.Drawing.Printing;
using System.Windows.Forms;
using System.IO;
using System.Text.RegularExpressions;
using NPOI.HSSF.UserModel;
using NPOI.SS.UserModel;
using WOSP.Properties;

namespace WOSP
{
	/// <summary>
	/// Description of MainForm.
	/// </summary>
	public partial class MainForm : Form
	{
		#region variables
		
		ISheet volSheet;
		ISheet orgSheet;
		HSSFWorkbook wbook1;
		HSSFWorkbook wbook2;
		PrinterSettings printerSettings = new PrinterSettings();
		int xCalibration = 0;
		int yCalibration = 0;
		double sumaPieniedzy = 0.00;
		double volSum = 0.00;
		double orgSum = 0.00;
		string volBookName = @"wolont_2015.xls";
		string orgBookName = @"orgs.xls";
		double highscore = 0.00;
		int highscoreId = 0;
		string highscoreName = "Brak";
		int wydane = 0;
		
		#endregion
		
		public MainForm()
		{
			//
			// The InitializeComponent() call is required for Windows Forms designer support.
			//
			InitializeComponent();
			if(!System.Diagnostics.Debugger.IsAttached)
			{
				checkBox1.Checked = true;
				checkBox1.Visible = false;
			}
			//
			// TODO: Add constructor code after the InitializeComponent() call.
			//
			wbook1  = new HSSFWorkbook(new FileStream(volBookName, FileMode.Open, FileAccess.ReadWrite));
			volSheet = wbook1.GetSheetAt(0);
			
			wbook2 = new HSSFWorkbook(new FileStream(orgBookName, FileMode.OpenOrCreate, FileAccess.ReadWrite));
			orgSheet = wbook2.GetSheetAt(0);
			
			if(Settings.Default.PrinterSettings == null)
			{
				printDialog1.ShowDialog();
				printerSettings = printDialog1.PrinterSettings;
				toolStripStatusLabel1.Text = "Drukarka: " + printerSettings.PrinterName;
				Settings.Default.PrinterSettings = printerSettings;
				Settings.Default.Save();
			}
			else
			{
				printerSettings = Settings.Default.PrinterSettings;
				toolStripStatusLabel1.Text = "Drukarka: " + printerSettings.PrinterName;
			}
			xCalibration = Settings.Default.XCalib;
			yCalibration = Settings.Default.YCalib;
			Settings.Default.PropertyChanged += new PropertyChangedEventHandler(this.Default_PropertyChanged);
			volSum = 0.00;
			orgSum = 0.00;
			for(int i = 0; i <= volSheet.LastRowNum; i++)
			{
				if(volSheet.GetRow(i).GetCell(15) != null)
				{
					if(volSheet.GetRow(i).GetCell(15).CellType == CellType.Numeric) 
					{
						volSum += volSheet.GetRow(i).GetCell(15).NumericCellValue;
						wydane++;
						if(volSheet.GetRow(i).GetCell(15).NumericCellValue > highscore)
						{
							highscore = volSheet.GetRow(i).GetCell(15).NumericCellValue;
							highscoreId = i;
							highscoreName = new CultureInfo("pl-PL", false).TextInfo.ToTitleCase( (volSheet.GetRow(i).GetCell(1).StringCellValue + " " + volSheet.GetRow(i).GetCell(2).StringCellValue).ToLower());
						}
					}
				}
			}
			for(int i = 0; i <= orgSheet.LastRowNum; i++)
			{
				if(orgSheet.GetRow(i).GetCell(1) != null)
				{
					if(orgSheet.GetRow(i).GetCell(1).CellType == CellType.Numeric) orgSum += orgSheet.GetRow(i).GetCell(1).NumericCellValue;
				}
			}
			sumaPieniedzy = volSum + orgSum;
			toolStripStatusLabel4.Text = volSum.ToString("C2");
			toolStripStatusLabel5.Text = orgSum.ToString("C2");
			toolStripStatusLabel6.Text = sumaPieniedzy.ToString("C2");
			toolStripStatusLabel7.Text = highscore.ToString("C2");
			toolStripStatusLabel8.Text = highscoreName;
			toolStripStatusLabel9.Text = "Wyd: " + wydane.ToString();
		}
		
		private void Default_PropertyChanged(object sender, PropertyChangedEventArgs e)
    	{
      	if (e.PropertyName == "xCalibration")
        	this.xCalibration = Settings.Default.XCalib;
      	if (e.PropertyName == "yCalibration")
	        this.yCalibration = Settings.Default.XCalib;
      	this.toolStripStatusLabel2.Text = "x = " + this.xCalibration.ToString() + "; y = " + this.yCalibration.ToString();
      	Settings.Default.Save();
		}
		
		void ZamknijToolStripMenuItemClick(object sender, EventArgs e)
		{
			Application.Exit();
		}
		void IdBoxTextChanged(object sender, EventArgs e)
		{
			int selectStart = idBox.SelectionStart;
			int selectLength = idBox.SelectionLength;
			idBox.Text = Regex.Replace(idBox.Text, "\\D", "");
			idBox.Select(selectStart, selectLength);
			if(idBox.Text.Length > 0)
			{
				if(int.Parse(idBox.Text) <= volSheet.LastRowNum)
				{
					IRow wolRow = volSheet.GetRow(int.Parse(idBox.Text));
					nameBox.Text = new CultureInfo("pl-PL", false).TextInfo.ToTitleCase( (wolRow.GetCell(1).StringCellValue.Trim() + " " + wolRow.GetCell(2).StringCellValue.Trim()).ToLower());
				}
			}
			else nameBox.Text = "";
		}
		void IdBoxKeyPress(object sender, KeyPressEventArgs e)
		{
			if(e.KeyChar == (char)Keys.Enter)
			{
				if(nameBox.Text.Length <= 0) nameBox.Focus();
				else countBox.Focus();
			}
			else if(e.KeyChar == (char)Keys.Escape)
			{
				nameBox.Clear();
				countBox.Clear();
				idBox.Clear();
				idBox.Focus();
			}
		}
		
		void UstawieniaDrukarkiToolStripMenuItemClick(object sender, EventArgs e)
		{
			printDialog1.ShowDialog();
			printerSettings = printDialog1.PrinterSettings;
			toolStripStatusLabel1.Text = "Drukarka: " + printerSettings.PrinterName;
			Settings.Default.PrinterSettings = printerSettings;
			Settings.Default.Save();
		}
		void NameBoxKeyPress(object sender, KeyPressEventArgs e)
		{
			if(e.KeyChar == (char)Keys.Enter)
			{
				if(countBox.Text.Length <= 0) countBox.Focus();
				else
				{
					if(nameBox.Text.Length <= 0) nameBox.Focus();
					else
					{
						printIt();
					}
				}
			}
			else if(e.KeyChar == (char)Keys.Escape)
			{
				nameBox.Clear();
				countBox.Clear();
				idBox.Clear();
				idBox.Focus();
			}
		}
		
		void printIt()
		{
			if(idBox.Text.Length > 0)
			{
				int id = Int32.Parse(idBox.Text);
				if(id <= 0 || id > volSheet.LastRowNum)idBox.Text = "";
			}
			try
      		{
        		if (!this.checkBox1.Checked) return;
 		        PrintDocument printDocument = new PrintDocument();
 		        printDocument.PrinterSettings = printerSettings;
        		printDocument.PrintPage += new PrintPageEventHandler(this.pd_PrintPage);
        		printDocument.Print();
      		}
			finally
			{
				if(idBox.Text.Length > 0)
				{
					int id = Int32.Parse(idBox.Text);
					if(volSheet.GetRow(id).GetCell(15) == null) volSheet.GetRow(id).CreateCell(15);
					volSheet.GetRow(id).GetCell(15).SetCellValue(double.Parse(countBox.Text));
					using (FileStream fs = new FileStream(volBookName, FileMode.Open, FileAccess.Write))
            		{
                		wbook1.Write(fs);
            		}
					volSum += double.Parse(countBox.Text);
					wydane++;
					if(double.Parse(countBox.Text) > highscore)
					{
						highscore = double.Parse(countBox.Text);
						highscoreId = id;
						highscoreName = nameBox.Text;
						MessageBox.Show(nameBox.Text + ": " + countBox.Text);
					}
				}
				else
				{
					HSSFRow row = (HSSFRow)orgSheet.CreateRow(orgSheet.LastRowNum + 1);
					row.CreateCell(0).SetCellValue(nameBox.Text);
					row.CreateCell(1).SetCellValue(double.Parse(countBox.Text));
					using (FileStream fs = new FileStream(orgBookName, FileMode.Open, FileAccess.Write))
            		{
                		wbook2.Write(fs);
            		}
					orgSum += double.Parse(countBox.Text);
				}
				toolStripStatusLabel4.Text = volSum.ToString("C2");
				toolStripStatusLabel5.Text = orgSum.ToString("C2");
				sumaPieniedzy = volSum + orgSum;
				toolStripStatusLabel6.Text = sumaPieniedzy.ToString("C2");
				toolStripStatusLabel7.Text = highscore.ToString("C2");
				toolStripStatusLabel8.Text = highscoreName;
				toolStripStatusLabel9.Text = "Wyd: " + wydane.ToString();
			}
			nameBox.Clear();
			countBox.Clear();
			idBox.Clear();
			idBox.Focus();
		}
		
		public void pd_PrintPage(object sender, PrintPageEventArgs ev)
    	{
	    	ev.Graphics.PageUnit = GraphicsUnit.Millimeter;
	    	float fontsize = 40f;
	    	Font font = new Font("Arial", fontsize);
	    	SizeF stringsize = ev.Graphics.MeasureString(this.nameBox.Text, font);
	    	while(stringsize.Height > 14f || stringsize.Width > 151f)
	    	{
	    		font = new Font("Arial", --fontsize);
	    		stringsize = ev.Graphics.MeasureString(this.nameBox.Text, font);
	    	}
      		SolidBrush solidBrush = new SolidBrush(Color.Black);
      		StringFormat format = new StringFormat();
      		format.Alignment = StringAlignment.Center;
      		format.LineAlignment = StringAlignment.Center;
      		RectangleF layoutRectangle1 = new RectangleF((float) (36 + this.xCalibration), (float) (136 + this.yCalibration), 151f, 14f);
      		RectangleF layoutRectangle2 = new RectangleF((float) (36 + this.xCalibration), (float) (160 + this.yCalibration), 151f, 14f);
      		ev.Graphics.DrawString(this.nameBox.Text, font, (Brush) solidBrush, layoutRectangle1, format);
      		font = new Font("Arial", 40f);
      		ev.Graphics.DrawString(this.countBox.Text + " zł", font, (Brush) solidBrush, layoutRectangle2, format);
    	}
		
		
		void NameBoxEnter(object sender, EventArgs e)
		{
			nameBox.SelectAll();
		}
		void CountBoxKeyPress(object sender, KeyPressEventArgs e)
		{
			if(e.KeyChar == (char)Keys.Enter)
			{
				if(nameBox.Text.Length <= 0) nameBox.Focus();
				else if(countBox.Text.Length <= 0) countBox.Focus();
				else
				{
					printIt();
				}
			}
			else if(e.KeyChar == (char)Keys.Escape)
			{
				nameBox.Clear();
				countBox.Clear();
				idBox.Clear();
				idBox.Focus();
			}
		}
		void CountBoxTextChanged(object sender, EventArgs e)
		{
			int selectStart = countBox.SelectionStart;
			int selectLength = countBox.SelectionLength;
			countBox.Text = Regex.Match(countBox.Text, @"[0-9]+(,|\.)?[0-9]{0,2}").ToString();
			countBox.Text = Regex.Replace(countBox.Text, @"[,\.]", ",");
			countBox.Select(selectStart, selectLength);
		}
		
		void UstawieniaKalibracjiToolStripMenuItemClick(object sender, EventArgs e)
		{
			Kalibracja calib = new Kalibracja();
			calib.ShowDialog();
			xCalibration = Settings.Default.XCalib;
			yCalibration = Settings.Default.YCalib;
			this.toolStripStatusLabel2.Text = "x = " + this.xCalibration.ToString() + "; y = " + this.yCalibration.ToString();
		}
		
		void Button2Click(object sender, EventArgs e)
		{
			if(nameBox.Text.Length > 0 && countBox.Text.Length > 0)
			{
				if(countBox.Text.Length <= 0) countBox.Focus();
				else
				{
					if(nameBox.Text.Length <= 0) nameBox.Focus();
					else
					{
						printIt();
					}
				}
			}
		}
		
		void MainFormKeyPress(object sender, KeyPressEventArgs e)
		{
			if(e.KeyChar == (char)Keys.Escape)
			{
				nameBox.Clear();
				countBox.Clear();
				idBox.Clear();
				idBox.Focus();
			}
		}
		
	}
}
