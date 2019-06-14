using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Microsoft.Speech.Recognition;
using System.Diagnostics;

namespace SpeechRecognition
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        static Label l;

        static void sre_SpeechRecognized(object sender, SpeechRecognizedEventArgs e)
        {
            if (e.Result.Confidence > 0.90) l.Text = e.Result.Text;

            switch (e.Result.Text)
            {
                case "выключить":
                    System.Diagnostics.Process.Start("cmd", "/c shutdown -s -f -t 00");
                    break;
                case "компьютер":
                    string myComputerPath = Environment.GetFolderPath(Environment.SpecialFolder.MyComputer);
                    System.Diagnostics.Process.Start("explorer", myComputerPath);
                    break;
                case "браузер":
                    System.Diagnostics.Process.Start("http://www.google.com");
                    break;
                case "ютуб":
                    System.Diagnostics.Process.Start("https://www.youtube.com");
                    break;
                case "радио":
                    System.Diagnostics.Process.Start(@"D:\исходник\Radio1\bin\Debug\Radio.exe");
                    break;
                default:
                    MessageBox.Show(" Не могу распознать  ");
                    break;
            }
        }    
        
        private void Form1_Shown(object sender, EventArgs e)
        {
            l = label1;
       
            System.Globalization.CultureInfo ci = new System.Globalization.CultureInfo("ru-ru");
            SpeechRecognitionEngine sre = new SpeechRecognitionEngine(ci);
            sre.SetInputToDefaultAudioDevice();
          
            sre.SpeechRecognized += new EventHandler<SpeechRecognizedEventArgs>(sre_SpeechRecognized);
          

            Choices numbers = new Choices();
            numbers.Add(new string[] {"радио", "выключить", "компьютер", "браузер", "ютуб" });

   
            GrammarBuilder gb = new GrammarBuilder();
            gb.Culture = ci;
            gb.Append(numbers);


            Grammar g = new Grammar(gb);
            sre.LoadGrammar(g);

            sre.RecognizeAsync(RecognizeMode.Multiple);
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
    }
}
