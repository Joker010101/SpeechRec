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
                    MessageBox.Show("Excellent !!");
                    break;
                case "компьютер":
                    MessageBox.Show("Very Good  !!");
                    break;
                case "браузер":
                    System.Diagnostics.Process Proc = new System.Diagnostics.Process();
                    Proc.StartInfo.FileName = "google.exe";
                    Proc.Start();
                    break;
                case "ютуб":
                    MessageBox.Show("Very Good  !!");
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
            numbers.Add(new string[] { "выключить", "компьютер", "браузер", "ютуб" });

   
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
