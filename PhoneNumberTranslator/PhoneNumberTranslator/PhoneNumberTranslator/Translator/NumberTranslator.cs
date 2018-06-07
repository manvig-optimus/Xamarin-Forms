using System;
using System.Linq;
using System.Text;

namespace PhoneNumberTranslator.Translator
{
    public static class NumberTranslator
    {
        public static string TranslateToNumber(string number)
        {
            if (String.IsNullOrEmpty(number))
            {
                return null;
            }

            number = number.ToUpperInvariant();

            var newNumber = new StringBuilder();

            foreach (var c in number)
            {
                if ("-0123456789".Contains(c))
                {
                    newNumber.Append(c);
                }
                else
                {
                    var translatedCharacter = Translate(c);

                    if (translatedCharacter == null)
                    {
                        return null;
                    }
                    else
                    {
                        newNumber.Append(translatedCharacter);
                    }
                }
            }

            return newNumber.ToString();
        }

        private static readonly string[] digits = { "ABC", "DEF", "GHI", "JKL", "MNO", "PQRS", "TUV", "WXYZ" };

        private static int? Translate(char c)
        {
            for (int i = 0; i < digits.Length; i++)
            {
                if (digits[i].Contains(c))
                {
                    return 2 + i;
                }
            }
            return null;
        }
    }
}
