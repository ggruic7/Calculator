using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Speech.Synthesis;

namespace Calculator
{
    public class speechOutput
    {
        public static SpeechSynthesizer sp = new SpeechSynthesizer();

        internal static void processOutput(string output)
        {
            output = output.Replace("*", " times ");
            output = output.Replace("/", " divided by ");
            output = output.Replace("^2", " squared ");
            output = output.Replace("^3", " cubed ");
            output = output.Replace("^", " to the power of ");
            output = output.Replace("(", " the quantity");
            output = output.Replace(")", "");
            output = output.Replace("-", " minus ");
            sp.Speak(output);
        }

        internal static void speak(string toSay)
        {
            sp.Speak(toSay);
        }
    }
}
