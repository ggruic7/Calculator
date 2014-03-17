using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NCalc;
using System.Speech.Synthesis;
using System.Text.RegularExpressions;

namespace Calculator
{
    public class MainAgent
    {

        internal static string Calculate(string inputEquation, bool speechActive, bool texActive)
        {
            string speechString = inputEquation;
            for (int i = 0; i < inputEquation.Length; i++)
            {
                if (inputEquation[i].Equals('(') && i!=0)
                {
                    if (Char.IsDigit(inputEquation[i - 1]))
                    {
                        inputEquation = inputEquation.Insert(i, "*");
                    }
                }

                if (inputEquation[i].Equals('^'))
                {
                    int j;
                    string tempBefore = String.Empty;
                    string tempAfter = String.Empty;
                    for (j = i-1; j >= 0; j--)
                    {
                        if (Char.IsDigit(inputEquation[j]) || inputEquation[j].Equals(')') || inputEquation[j].Equals('(') || (tempBefore.Contains(')') && !tempBefore.Contains('(')) || inputEquation[j].Equals(' '))
                        {
                            tempBefore = inputEquation[j] + tempBefore;
                        }
                        else
                            break;
                    }
                    for (j = i + 1; j < inputEquation.Length; j++)
                    {
                        if (Char.IsDigit(inputEquation[j]) || inputEquation[j].Equals(' '))
                        {
                            tempAfter += inputEquation[j];
                        }
                        else
                            break;
                    }
                    i = j;
                    inputEquation = inputEquation.Replace(tempBefore + "^" + tempAfter, String.Format("Pow({0},{1})", tempBefore, tempAfter));
                }
            }
            
            Expression e = new Expression(inputEquation);
            string x = e.Evaluate().ToString();
            if (speechActive)
                speechOutput.processOutput(speechString + " = " + x);
            if (texActive)
                texOutput.displayOutput(speechString + " = " + x);
            return x;
        }

        internal static void prompt(string type)
        {
            SpeechSynthesizer sp = new SpeechSynthesizer();

            switch (type)
            {
                case "new":
                    speechOutput.speak("Please say or enter an equation followed by an equals sign to compute the equation");
                    break;
            }
        }
    }
}
