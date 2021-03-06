﻿/*
 * Utworzone przez SharpDevelop.
 * Użytkownik: Szymen
 * Data: 2015-01-10
 * Godzina: 23:17
 * 
 * Do zmiany tego szablonu użyj Narzędzia | Opcje | Kodowanie | Edycja Nagłówków Standardowych.
 */
using System;
using System.ComponentModel;
using System.Drawing;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using WOSP.Properties;

namespace WOSP
{
	/// <summary>
	/// Description of Kalibracja.
	/// </summary>
	public partial class Kalibracja : Form
	{
		
		//
		// TODO: Przycisk Testuj nic nie drukuje, a powinien to, co z głównej formatki, tylko z testowymi danymi, żeby ustawić kalibrację, ale nie miałem na to czasu
		//

		public Kalibracja()
		{
			//
			// The InitializeComponent() call is required for Windows Forms designer support.
			//
			InitializeComponent();
			
			//odczyt ustawień
			textBox1.Text = Settings.Default.XCalib.ToString();
			textBox2.Text = Settings.Default.YCalib.ToString();
			//
			// TODO: Add constructor code after the InitializeComponent() call.
			//
		}
		
		void Button2Click(object sender, EventArgs e)
		{
			this.Close();
		}
		
		void Button3Click(object sender, EventArgs e)
		{
			//Zapis do ustawień. Powinno wywołać event w MainForm, ale nie wywołuje 
			Settings.Default.XCalib = int.Parse(textBox1.Text);
			Settings.Default.YCalib = int.Parse(textBox2.Text);
			Settings.Default.Save();
		}
		
		void TextBox1TextChanged(object sender, EventArgs e)
		{
			//Czyszczenie z innego niż 0-9 oraz "-"
			textBox1.Text = Regex.Replace(textBox1.Text, @"[^0-9-]", "");
		}
		
		void TextBox2TextChanged(object sender, EventArgs e)
		{
			//Czyszczenie z innego niż 0-9 oraz "-"
			textBox2.Text = Regex.Replace(textBox2.Text, @"[^0-9-]", "");
		}
	}
}
