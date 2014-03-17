using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using SpeechLib;
using System.Speech.Recognition;
using System.Speech.Synthesis;
using System.Threading;


namespace Calculator
{
    public partial class Calc : Form
    {
        public Calc()
        {
            InitializeComponent();
        }

        static bool speechAcvite, texActive;
        static ManualResetEvent completed = null;

        void Form1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)8)
            {
                if (lblTextDisplay.Text.Length >0)
                lblTextDisplay.Text = lblTextDisplay.Text.Substring(0, lblTextDisplay.Text.Length - 1);
            }
            else if (e.KeyChar.ToString() == "=")
            {
                string answer = MainAgent.Calculate(lblTextDisplay.Text,speechAcvite, texActive);
                lblTextDisplay.Text += "=" + answer;
            }
            else if (!char.IsLetter(e.KeyChar) || !char.IsWhiteSpace(e.KeyChar))
            {
                lblTextDisplay.Text += e.KeyChar.ToString();
            }
            this.Focus();
        }

        void sre_SpeechRecognized(object sender, SpeechRecognizedEventArgs e)
        {
            processInput(e.Result.Text);
        }

        private void Calc_Load(object sender, EventArgs e)
        {
            texActive = true;
            completed = new ManualResetEvent(false);
            SpeechRecognitionEngine sre = new SpeechRecognitionEngine(new System.Globalization.CultureInfo("en-US"));
            sre.SetInputToDefaultAudioDevice();
            Choices numbers = new Choices();
            for (int i = 0; i < 10; i++)
            {
                numbers.Add(i.ToString());
            }
            numbers.Add(new string[]{"=","-","+","times","multiplied by", "divided by","to the power of" ,"Clear","negative" ,"quit", "the quantity", "end quantity", "answer", "squared","cubed"});
            GrammarBuilder gb = new GrammarBuilder();
            gb.Append(numbers);
            Grammar g = new Grammar(gb);
            sre.LoadGrammar(g);
            sre.SpeechRecognized += new EventHandler<SpeechRecognizedEventArgs>(sre_SpeechRecognized);
            sre.RecognizeAsync(RecognizeMode.Multiple);
            Application.Idle += new EventHandler(OnLoaded);
            
        }

        public void OnLoaded(object sender, EventArgs args)
        {
            Application.Idle -= new EventHandler(OnLoaded);
            MainAgent.prompt("new");
            btnEquals.Focus();
        }

        private void button_Click(object sender, EventArgs e)
        {
            Button b = (Button)sender;
            processInput(b.Text);
            btnEquals.Focus();
        }

        public void displaySingle(string input)
        {
            lblTextDisplay.Text = input;
        }

        private void processInput(string input)
        {
            switch (input)
            {
                case "=":
                    string answer = MainAgent.Calculate(lblTextDisplay.Text,speechAcvite, texActive);
                    lblTextDisplay.Text += " " + input+ " " + answer;
                    break;
                case "Clear":
                    lblTextDisplay.Text = String.Empty;
                    break;
                case "times":
                    lblTextDisplay.Text += "*";
                    break;
                case "divided by":
                    lblTextDisplay.Text += "/";
                    break;
                case "multiplied by":
                    lblTextDisplay.Text += "*";
                    break;
                case "to the power of":
                    lblTextDisplay.Text += "^";
                    break;
                case "negative":
                    lblTextDisplay.Text += "-";
                    break;
                case "the quantity":
                    lblTextDisplay.Text += "(";
                    break;
                case "end quantity":
                    lblTextDisplay.Text += ")";
                    break;
                case "answer":
                    lblTextDisplay.Text = lblTextDisplay.Text.Substring(lblTextDisplay.Text.LastIndexOf("=") + 1);
                    break;
                case "sqaured":
                    lblTextDisplay.Text += "^2";
                    break;
                case "cubed":
                    lblTextDisplay.Text += "^3";
                    break;
                case "quit":
                    if(DialogResult.Yes == MessageBox.Show("Are you sure you want to quit?", "Exit?", MessageBoxButtons.YesNo))
                    Environment.Exit(0);
                    break;
                default:
                    lblTextDisplay.Text += input;
                    break;
            }
        }

        private void exitMenu_Click(object sender, EventArgs e)
        {
            Environment.Exit(0);
        }

        private void Radio_CheckedChanged(object sender, EventArgs e)
        {
            string result = string.Empty;
            foreach (Control control in this.groupBox1.Controls)
            {
                RadioButton radio = control as RadioButton;
                if (radio.Checked)
                    result = radio.Text;
            }

            switch (result)
            {
                case "Tex":
                    speechAcvite = false;
                    texActive = true;
                    break;
                case "Speech":
                    speechAcvite = true;
                    texActive = false;
                    break;
                case "Both":
                    speechAcvite = true;
                    texActive = true;
                    break;
            }
        }


     

        
    }
}
