﻿using Microsoft.Speech.Recognition;
using System;
using System.Windows.Forms;

namespace SpeechRecognition
{
    public partial class Form1 : Form
    {




        public Form1()
        {
            InitializeComponent();
        }

        private SpeechRecognitionEngine sre;


        static Label l;
        static Label l2;




        // proverka 2 

        static void sre_SpeechRecognized(object sender, SpeechRecognizedEventArgs e)
        {






            l2.Text = e.Result.Confidence.ToString();
            if (e.Result.Confidence > 0.33)
            {
                l.Text = e.Result.Text;
                // if (e.Result.Text == "выключить"&& e.Result.Confidence > 0.87)System.Diagnostics.Process.Start("cmd", "/c shutdown -s -f -t 00");
                if (e.Result.Text == "компьютер" && e.Result.Confidence > 0.80)
                {
                    string myComputerPath = Environment.GetFolderPath(Environment.SpecialFolder.MyComputer);
                    System.Diagnostics.Process.Start("explorer", myComputerPath);
                };
                if (e.Result.Text == "браузер" && e.Result.Confidence > 0.87) System.Diagnostics.Process.Start("http://www.google.com");
                if (e.Result.Text == "ютуб" && e.Result.Confidence > 0.87) System.Diagnostics.Process.Start("https://www.youtube.com");
                // if (e.Result.Text == "радио" && e.Result.Confidence > 0.87) System.Diagnostics.Process.Start(@"D:\исходник\Radio1\bin\Debug\Radio.exe");
                if (e.Result.Text == "радио" && e.Result.Confidence > 0.87) System.Diagnostics.Process.Start(@"D:\исходники\radio\radio\radio\Radio\bin\Debug\Radio.exe");
                if (e.Result.Text == "выход" && e.Result.Confidence > 0.87) { MessageBox.Show("пока господин"); Application.Exit(); };
                if (e.Result.Text == "пока" && e.Result.Confidence > 0.87) MessageBox.Show("надоела Вам");


            }


        }






        private void Form1_Shown(object sender, EventArgs e)
        {

            l = label1;
            l2 = label2;

            checkBox1.Enabled = true;

            System.Globalization.CultureInfo ci = new System.Globalization.CultureInfo("ru-ru");
            sre = new SpeechRecognitionEngine(ci);
            sre.SetInputToDefaultAudioDevice();
            sre.SpeechRecognized += new EventHandler<SpeechRecognizedEventArgs>(sre_SpeechRecognized);
            Choices numbers = new Choices();
            numbers.Add(new string[] { "радио", "выключить", "компьютер", "браузер", "ютуб", "пока", "алена" });
            GrammarBuilder gb = new GrammarBuilder();
            gb.Culture = ci;
            gb.Append(numbers);
            Grammar g = new Grammar(gb);
            sre.LoadGrammar(g);
            sre.RecognizeAsync(RecognizeMode.Multiple);







        }












        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {


            CheckBox checkBox = (CheckBox)sender;

            if (checkBox.Checked == true)
            {
                checkBox1.Text = "Выключить";
                sre.RecognizeAsync(RecognizeMode.Multiple);
            }
            else

            {
                checkBox1.Text = "Включить";
                MessageBox.Show("Выкл");
                sre.RecognizeAsyncStop();
            }


        }


    }
}
