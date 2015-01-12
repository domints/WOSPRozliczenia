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
			
			if(!System.Diagnostics.Debugger.IsAttached) //Jeśli podpięty debugger nie pokazuje checkboxa i ustawia zawsze drukowanie
			{
				checkBox1.Checked = true;
				checkBox1.Visible = false;
			}
			#region inicjalizacja baz
			wbook1  = new HSSFWorkbook(new FileStream(volBookName, FileMode.Open, FileAccess.ReadWrite)); //Otwieranie pliku XLS z wolontariuszami
			volSheet = wbook1.GetSheetAt(0); //wybieranie pierwszego arkusza - na nim są dane
			
			wbook2 = new HSSFWorkbook(new FileStream(orgBookName, FileMode.OpenOrCreate, FileAccess.ReadWrite)); //Otwieranie pliku XLS z akcjami
			orgSheet = wbook2.GetSheetAt(0); //wybieranie pierwszego arkusza - na nim są dane
			#endregion
			#region wczytanie ustawień
			//
			// TODO: Sprawdzić, czy wszystko działa na innych systemach niż XP i ewentualnie naprawić, albo z tym żyć.
			//		 Mi nie działało chyba tak jak powinno ;)
			//			
			if(Settings.Default.PrinterSettings == null) //Jeśli w ustawieniach  nie ma zapisanej drukarki, pokaż okno wyboru. Chyba niekoniecznie działa.
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
			xCalibration = Settings.Default.XCalib; //Wczytywanie danych kalibracji
			yCalibration = Settings.Default.YCalib;
			Settings.Default.PropertyChanged += new PropertyChangedEventHandler(this.Default_PropertyChanged);
			#endregion
			#region sumowanie kwoty i wyszukiwanie rekordzisty
			volSum = 0.00;
			orgSum = 0.00;
			for(int i = 0; i <= volSheet.LastRowNum; i++) //Ładowanie już wpisanych wolontariuszy i obliczanie kwoty zebranej
			{
				if(volSheet.GetRow(i).GetCell(15) != null) //wykonywanie tylko gdy komórka z kwotą istnieje
				{
					if(volSheet.GetRow(i).GetCell(15).CellType == CellType.Numeric) //wykonywanie tylko, gdy komórka jest typu liczbowego (walutowego itd.)
					{
						volSum += volSheet.GetRow(i).GetCell(15).NumericCellValue; //dodawanie aktualnie przeliczanego wolontariusza
						wydane++;
						if(volSheet.GetRow(i).GetCell(15).NumericCellValue > highscore) //zapis rekordu do zmiennych
						{
							highscore = volSheet.GetRow(i).GetCell(15).NumericCellValue; //pobieranie wartości jako liczba
							highscoreId = i;
							/*
							 * Całe to cultureinfo itd. po to, że w bazie ludzie są różnie zapisani,
							 * a to zapewnia mnie, że między imieniem i nazwiskiem będzie jedna spacja,
							 * przed i za nie będzie spacji i zarówno imię i nazwisko będą zaczynać się
							 * wielką literą tak jak powinny.
							 * To samo jest w funkcji wywoływanej przy zmianie tekstu w okienku idBox
							 */
							highscoreName = new CultureInfo("pl-PL", false).TextInfo.ToTitleCase( (volSheet.GetRow(i).GetCell(1).StringCellValue.Trim() + " " + volSheet.GetRow(i).GetCell(2).StringCellValue.Trim()).ToLower()); 
						}
					}
				}
			}
			for(int i = 0; i <= orgSheet.LastRowNum; i++) //obrabianie danych jak wyżej, tyle że dla akcji, bez rekordu 
			{
				if(orgSheet.GetRow(i).GetCell(1) != null)
				{
					if(orgSheet.GetRow(i).GetCell(1).CellType == CellType.Numeric) orgSum += orgSheet.GetRow(i).GetCell(1).NumericCellValue;
				}
			}
			#endregion
			#region zapis toolstripa
			sumaPieniedzy = volSum + orgSum;
			toolStripStatusLabel4.Text = volSum.ToString("C2"); //suma zebrana przez wolontariuszy. Format "C2" to format walutowy, 0.00
			toolStripStatusLabel5.Text = orgSum.ToString("C2"); //suma sebrana na akcjach
			toolStripStatusLabel6.Text = sumaPieniedzy.ToString("C2"); //suma zebrana łącznie
			toolStripStatusLabel7.Text = highscore.ToString("C2"); //kwota zebrana przez rekordzistę
			toolStripStatusLabel8.Text = highscoreName; //imię i nazwisko rekordzisty
			toolStripStatusLabel9.Text = "Wyd: " + wydane.ToString();
			#endregion
		}
		
		/// <summary>
		/// Teoretycznie gdy inne okno / wątek zmieni ustawienia powinno je wczytać, w oryginale działało, teraz chyba nie...
		/// </summary>
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
			#region Czyszczenie okna ze znaków nieodpowiednich przy pomocy wyrażeń regularnych
			int selectStart = idBox.SelectionStart;
			int selectLength = idBox.SelectionLength;
			idBox.Text = Regex.Replace(idBox.Text, "\\D", ""); //wyrażenie \D zaznacza wszystko co nie jest cyfrą, odpowiednik [^0-9]
			idBox.Select(selectStart, selectLength);
			#endregion
			if(idBox.Text.Length > 0) //jeśli zostało cokolwiek w okienku na ID
			{
				if(int.Parse(idBox.Text) <= volSheet.LastRowNum) //Jeśli ID zawiera się w zakresie w bazie danych
				{
					IRow wolRow = volSheet.GetRow(int.Parse(idBox.Text));
					nameBox.Text = new CultureInfo("pl-PL", false).TextInfo.ToTitleCase( (wolRow.GetCell(1).StringCellValue.Trim() + " " + wolRow.GetCell(2).StringCellValue.Trim()).ToLower()); //to samo co przy rekordzie
				}
			}
			else nameBox.Text = "";
		}
		
		void IdBoxKeyPress(object sender, KeyPressEventArgs e)
		{
			if(e.KeyChar == (char)Keys.Enter) // Jeśli ENTER, przejdź dalej
			{
				if(nameBox.Text.Length <= 0) nameBox.Focus(); //Jeśli nazwa jest pusta przejdź do jej wpisywania
				else countBox.Focus(); //przejdź do wpisywania kwoty
			}
			else if(e.KeyChar == (char)Keys.Escape) // Jeśli ESC, wyczyść wszystkie pola i przejdź do ID
			{
				#region czyszczenie okienek
				nameBox.Clear();
				countBox.Clear();
				idBox.Clear();
				idBox.Focus();
				#endregion
			}
		}
		
		/// <summary>
		/// Przechodzi do okna wyboru drukarki, wybiera ją, zapisuje i ustawia tekst na pasku statusu
		/// </summary>
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
			if(e.KeyChar == (char)Keys.Enter) //Jeśli ENTER przejdź dalej
			{
				if(countBox.Text.Length <= 0) countBox.Focus(); // Jeśli kwota pusta przejdź do kwoty
				else
				{
					if(nameBox.Text.Length <= 0) nameBox.Focus(); //Jeśli nazwa pusta przejdź do nazwy
					else
					{
						printIt(); //wydrukuj
					}
				}
			}
			else if(e.KeyChar == (char)Keys.Escape) //Jeśli ESC wykasuj wszystko
			{
				#region czyszczenie okienek
				nameBox.Clear();
				countBox.Clear();
				idBox.Clear();
				idBox.Focus();
				#endregion
			}
		}
		
		/// <summary>
		/// Drukowanie rozliczenia na podstawie okienek
		/// </summary>
		void printIt()
		{
			if(idBox.Text.Length > 0) //Jeżeli coś jest w okienku z ID to ustaw zmienną id
			{
				int id = Int32.Parse(idBox.Text);
				if(id <= 0 || id > volSheet.LastRowNum)idBox.Text = ""; //Jeżeli ID jest poza zakresem, wyczyść okienko z ID
			}
			try //Próba wydrukowania
      		{
        		if (!this.checkBox1.Checked) return;
 		        PrintDocument printDocument = new PrintDocument();
 		        printDocument.PrinterSettings = printerSettings;
        		printDocument.PrintPage += new PrintPageEventHandler(this.pd_PrintPage);
        		printDocument.Print();
      		}
			finally
			{
				if(idBox.Text.Length > 0) //Jeżeli teraz zostało coś w ID to jest to poprawny wolontariusz
				{
					int id = Int32.Parse(idBox.Text);
					if(volSheet.GetRow(id).GetCell(15) == null) volSheet.GetRow(id).CreateCell(15); //Jeżeli przy nim nie było kwoty, utwórz komórkę
					volSheet.GetRow(id).GetCell(15).SetCellValue(double.Parse(countBox.Text)); //Do odpowiedniej komórki wpisz wartość
					using (FileStream fs = new FileStream(volBookName, FileMode.Open, FileAccess.Write)) //Zapisz wszystko spowrotem do pliku
            		{
                		wbook1.Write(fs);
            		}
					volSum += double.Parse(countBox.Text); //Dodaj do sumy zebranej przez wolontariuszy
					wydane++; //Dodaj ilość wydrukowanych rozliczeń
					if(double.Parse(countBox.Text) > highscore) //Jeśli mamy nowy rekord pozapisuj zmienne i wyrzuć okienko
					{
						highscore = double.Parse(countBox.Text);
						highscoreId = id;
						highscoreName = nameBox.Text;
						MessageBox.Show(nameBox.Text + ": " + countBox.Text);
					}
				}
				else //Jeśli nie, trzeba zapisać do akcji 
				{
					//Tutaj zawsze tworzę nowy wiersz
					HSSFRow row = (HSSFRow)orgSheet.CreateRow(orgSheet.LastRowNum + 1);
					row.CreateCell(0).SetCellValue(nameBox.Text);
					row.CreateCell(1).SetCellValue(double.Parse(countBox.Text));
					using (FileStream fs = new FileStream(orgBookName, FileMode.Open, FileAccess.Write))
            		{
                		wbook2.Write(fs); //zapisz do pliku
            		}
					orgSum += double.Parse(countBox.Text); //dodanie do sumy z akcji
				}
				#region aktualizacja toolstripa
				toolStripStatusLabel4.Text = volSum.ToString("C2");
				toolStripStatusLabel5.Text = orgSum.ToString("C2");
				sumaPieniedzy = volSum + orgSum;
				toolStripStatusLabel6.Text = sumaPieniedzy.ToString("C2");
				toolStripStatusLabel7.Text = highscore.ToString("C2");
				toolStripStatusLabel8.Text = highscoreName;
				toolStripStatusLabel9.Text = "Wyd: " + wydane.ToString();
				#endregion
			}
			#region czyszczenie okienek
			nameBox.Clear();
			countBox.Clear();
			idBox.Clear();
			idBox.Focus();
			#endregion
		}
		
		//Eventhandler od funkcji Print, generuje i drukuje rozliczenie
		public void pd_PrintPage(object sender, PrintPageEventArgs ev)
    	{
	    	ev.Graphics.PageUnit = GraphicsUnit.Millimeter;
	    	#region Ustalanie odpowiedniego rozmiaru czcionki
	    	float fontsize = 40f; //początkowy wymiar
	    	Font font = new Font("Arial", fontsize);
	    	SizeF stringsize = ev.Graphics.MeasureString(this.nameBox.Text, font); //pomiar wielkości tekstu
	    	while(stringsize.Height > 14f || stringsize.Width > 151f) //dopóki tekst nie mieści się w boxie
	    	{
	    		font = new Font("Arial", --fontsize); //odejmujemy fontsize zanim go poda jako argument funkcji
	    		stringsize = ev.Graphics.MeasureString(this.nameBox.Text, font); //pomiar wielkości tekstu
	    	}
	    	#endregion
      		SolidBrush solidBrush = new SolidBrush(Color.Black);
      		StringFormat format = new StringFormat();
      		format.Alignment = StringAlignment.Center;
      		format.LineAlignment = StringAlignment.Center;
      		//prostokąty w których musi zawierać się imię+nazwisko i kwota
      		RectangleF layoutRectangle1 = new RectangleF((float) (36 + this.xCalibration), (float) (136 + this.yCalibration), 151f, 14f);
      		RectangleF layoutRectangle2 = new RectangleF((float) (36 + this.xCalibration), (float) (160 + this.yCalibration), 151f, 14f);
      		
      		//Narysuj imię i nazwisko
      		ev.Graphics.DrawString(this.nameBox.Text, font, (Brush) solidBrush, layoutRectangle1, format);
      		// Ustaw czcionkę od kwoty i narysuj
      		font = new Font("Arial", 40f);
      		ev.Graphics.DrawString(this.countBox.Text + " zł", font, (Brush) solidBrush, layoutRectangle2, format);
      		
      		//Na samym końcu ev.Graphics jest drukowane.
      		//Jeśli chciałbyś dodać jakiś obrazek czy coś,
      		//z powodzeniem można użyć funkcji klasy Graphics
    	}
		
		//przy przejściu tabem zaznacza to okienko
		void NameBoxEnter(object sender, EventArgs e)
		{
			nameBox.SelectAll();
		}
		
		void CountBoxKeyPress(object sender, KeyPressEventArgs e)
		{
			if(e.KeyChar == (char)Keys.Enter) // Jeśli ENTER
			{
				if(nameBox.Text.Length <= 0) nameBox.Focus(); //Jeśli nazwa pusta, przejdź tam
				else if(countBox.Text.Length <= 0) countBox.Focus(); // Jeśli kwota pusta idź tam
				else
				{
					printIt(); //Jeśli oba są zapisane, drukuj
				}
			}
			else if(e.KeyChar == (char)Keys.Escape) //Jeśli ESC
			{
				#region czyszczenie okienek
				nameBox.Clear();
				countBox.Clear();
				idBox.Clear();
				idBox.Focus();
				#endregion
			}
		}
		
		/// <summary>
		/// Czyszczenie kwoty ze znaków innych niż 0-9, oraz znaki , i .
		/// Ostatecznie zamiana "," i "." na ",", które są obowiązujące w Polsce.
		/// Dobrze byłoby zrobić detekcję tego, co akurat jest na danym kompie obowiązujące.
		/// </summary>
		void CountBoxTextChanged(object sender, EventArgs e)
		{
			int selectStart = countBox.SelectionStart;
			int selectLength = countBox.SelectionLength;
			countBox.Text = Regex.Match(countBox.Text, @"[0-9]+(,|\.)?[0-9]{0,2}").ToString();
			countBox.Text = Regex.Replace(countBox.Text, @"[,\.]", ",");
			countBox.Select(selectStart, selectLength);
		}
		
		//Wyświetlanie okienka kalibracji jako Dialog, odczyt ustawień i zapis toolStripa
		void UstawieniaKalibracjiToolStripMenuItemClick(object sender, EventArgs e)
		{
			Kalibracja calib = new Kalibracja();
			calib.ShowDialog();
			xCalibration = Settings.Default.XCalib;
			yCalibration = Settings.Default.YCalib;
			this.toolStripStatusLabel2.Text = "x = " + this.xCalibration.ToString() + "; y = " + this.yCalibration.ToString();
		}
		
		//Sprawdzanie czy dane do wydruku wpisane i ewentualny wydruk
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
		
		//Teoretycznie przyciśnięcie ESC gdy sfocusowane jest tylko okno apki powinno wyczyścić okienka
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
