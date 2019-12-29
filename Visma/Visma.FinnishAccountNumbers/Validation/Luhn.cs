using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Visma.FinnishAccountNumbers.Validation
{
    public static class Luhn
    {
        public static bool LuhnCheck(this string accountNumber)
        {
            return LuhnCheck(accountNumber.Select(c => c - '0').ToArray());
        }

        private static bool LuhnCheck(this int[] digits)
        {
            return GetCheckValue(digits) == 0;
        }

        private static int GetCheckValue(int[] digits)
        {
            return digits.Select((d, i) => i % 2 == digits.Length % 2 ? ((2 * d) % 10) + d / 5 : d).Sum() % 10;
        }

        public static bool isDigitsOnly(string str)
        {
            foreach (char c in str)
            {
                if (c < '0' || c > '9')
                    return false;
            }

            return true;
        }
    }
}
