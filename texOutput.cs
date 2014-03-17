using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Calculator
{
    public class texOutput
    {
        internal static void displayOutput(string toOutput)
        {
            toOutput = "$$ " + toOutput + " $$";
            int position = toOutput.IndexOf('^');
            string temp = string.Empty;
            if (position > 0)
            {
                for (int i = position +1; i < toOutput.Length; i++)
                {
                    if (Char.IsDigit(toOutput[i]) || toOutput[i].Equals(' '))
                    {
                        temp += toOutput[i];
                    }
                    else
                        break;
                } 
            }
            if (temp.Length > 0)
            {
               toOutput = toOutput.Replace("^" + temp, "^{ " +temp +" } ");
            }
            
            MessageBox.Show(toOutput);
        
        }
    }
}
