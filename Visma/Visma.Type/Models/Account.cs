using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Visma.Types.Models
{
    public sealed class Account
    {
        private string _accountNumber;
        public string AccountNumber 
        {
            get { return _accountNumber; }
            set {
                try {
                    if (LuhnCheck(value) && value.Replace("-", "").All(char.IsDigit))
                        _accountNumber = value.Replace("-", "");
                    else
                        throw new Exception("account number is not correct");
                    } 
                catch (Exception e) { throw e; }
            }
        }

        public string Bank 
        {
            get {
                if (BankCode(AccountNumber.Substring(0, 1)) != null)
                    return BankCode(AccountNumber.Substring(0, 1));
                else if (BankCode(AccountNumber.Substring(0, 2)) != null)
                    return BankCode(AccountNumber.Substring(0, 2));
                else
                    return null;
            } 
        }
        public string FirstSixDigits => AccountNumber.Replace("-", "").Substring(0, 6);
        public string LastDigits => AccountNumber.Substring(AccountNumber.LastIndexOf('-') + 1);
        public string IBAN 
        {
            get 
            {
                string newLastDigits;
                string Abanks = LastDigits.PadLeft(8, '0');
                string Bbanks = LastDigits[0] + LastDigits.Substring(LastDigits.Length - 2).PadLeft(7, '0');
                switch (Bank)
                {
                    case "Nordea": newLastDigits = Abanks; break;
                    case "Handelsbanken(SHB)": newLastDigits = Abanks; break;
                    case "Skandinaviska Enskilda Banken(SEB)": newLastDigits = Abanks; break;
                    case "Danske Bank": newLastDigits = Abanks; break;
                    case "Tapiola Bank(Tapiola)": newLastDigits = Abanks; break;
                    case "DnB NOR Bank ASA(DnB NOR)": newLastDigits = Abanks; break;
                    case "Swedbank": newLastDigits = Abanks; break;
                    case "S-Bank": newLastDigits = Abanks; break;
                    case "savings banks(Sp) and local cooperative banks(Pop) and Aktia": newLastDigits = Bbanks; break;
                    case "cooperative banks(Op), OKO Bank and Okopankki":  newLastDigits = Bbanks; break;
                    case "Ålandsbanken ÅAB)":  newLastDigits = Abanks; break;
                    case "Sampo Bank(Sampo)": newLastDigits = Bbanks; break;
                    default: return null;
                }
                return FirstSixDigits + newLastDigits;
            }
        }

        private string BankCode(string code)
        {
            switch (int.Parse(code))
            {
                case 1: return "Nordea";
                case 2: return "Nordea";
                case 31: return "Handelsbanken(SHB)";
                case 33: return "Skandinaviska Enskilda Banken(SEB)";
                case 34: return "Danske Bank";
                case 36: return "Tapiola Bank(Tapiola)";
                case 37: return "DnB NOR Bank ASA(DnB NOR)";
                case 38: return "Swedbank";
                case 39: return "S-Bank";
                case 4: return "savings banks(Sp) and local cooperative banks(Pop) and Aktia";
                case 5: return "cooperative banks(Op), OKO Bank and Okopankki";
                case 6: return "Ålandsbanken ÅAB)";
                case 8: return "Sampo Bank(Sampo)";
                default: return null;
            }
        }

        public bool LuhnCheck(string accountNumber)
        {
            return LuhnCheck(accountNumber.Select(c => c - '0').ToArray());
        }

        private bool LuhnCheck(int[] digits)
        {
            return GetCheckValue(digits) == 0;
        }

        private int GetCheckValue(int[] digits)
        {
            return digits.Select((d, i) => i % 2 == digits.Length % 2 ? ((2 * d) % 10) + d / 5 : d).Sum() % 10;
        }
    }
}
