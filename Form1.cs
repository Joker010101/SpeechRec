using Microsoft.Speech.Recognition;
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
        static CheckBox Chek;





        static void KeyVoice()
        {

            System.Globalization.CultureInfo ci = new System.Globalization.CultureInfo("ru-ru");
            SpeechRecognitionEngine sre2 = new SpeechRecognitionEngine(ci);
            sre2.SetInputToDefaultAudioDevice();
            sre2.SpeechRecognized += new EventHandler<SpeechRecognizedEventArgs>(sre_key);
            Choices numbers = new Choices();
            numbers.Add(new string[] { "кузя" });
            GrammarBuilder gb = new GrammarBuilder();
            gb.Culture = ci;
            gb.Append(numbers);
            Grammar g = new Grammar(gb);
            sre2.LoadGrammar(g);
            sre2.RecognizeAsync(RecognizeMode.Multiple);



        }

        static void sre_key(object sender, SpeechRecognizedEventArgs e)
        {
            l2.Text = e.Result.Confidence.ToString();
            if (e.Result.Confidence > 0.33)
            {

                if (e.Result.Text == "кузя" && e.Result.Confidence > 0.87) { Chek.Checked = true; };

            }


        }

        static void sre_SpeechRecognized(object sender, SpeechRecognizedEventArgs e)
        {

           
            if (e.Result.Confidence > 0.45)
            {
                l2.Text = e.Result.Confidence.ToString();
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
                if (e.Result.Text == "выход" && e.Result.Confidence > 0.87) { Application.Exit(); };
                if (e.Result.Text == "пока" && e.Result.Confidence > 0.87) {  Chek.Checked = false; };

            }


        }






        private void Form1_Shown(object sender, EventArgs e)
        {

            l = label1;
            l2 = label2;

            Chek = checkBox1;

            checkBox1.Enabled = true;

            System.Globalization.CultureInfo ci = new System.Globalization.CultureInfo("ru-ru");
            sre = new SpeechRecognitionEngine(ci);
            sre.SetInputToDefaultAudioDevice();
            sre.SpeechRecognized += new EventHandler<SpeechRecognizedEventArgs>(sre_SpeechRecognized);
            Choices numbers = new Choices();
            numbers.Add(new string[] { "радио", "выключить", "компьютер", "браузер", "ютуб", "пока", "выход" });
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
                MessageBox.Show("Выкл голосовой Ассистен");
                sre.RecognizeAsyncStop();
                KeyVoice();

            }


        }


    }
}
