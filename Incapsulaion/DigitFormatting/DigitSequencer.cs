
using System.Text;


namespace DigitFormatting
{
    internal class DigitSequencer
    {
        public string GetSequence(int M)
        {
            StringBuilder result = new StringBuilder();
            int digitNo = 1;
            int devisor = 10;
            if (M < 0) return string.Empty;
            int quotient = M;
            int remainder;
            while (quotient > 0)
            {
                //devisor = devisor*10;
                remainder = quotient % (devisor );
                quotient = quotient / devisor;
                result.Append(remainder);
                if ((digitNo++ % 3) == 0) result.Append(",");

            }
            
            return GetInverted(result.ToString());
        }

        string GetInverted(string input)
        {
            StringBuilder result = new StringBuilder();
            for (int i = input.Length - 1; i >= 0; i--)
                result.Append(input[i]);
            return result.ToString();
        }
    }
}
