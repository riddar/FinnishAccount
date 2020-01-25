using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Visma.Types.Models
{
    public sealed class Account
    {

        public string Bank;
        public string FirstSixDigits;
        public string LastDigits;
        public string IBAN;

        private string _accountNumber;
        public string AccountNumber 
        {
            get { return _accountNumber; }
            set {
                if (value.Replace("-", "").All(char.IsDigit) && value.Length > 6 && value.Length < 14)
                {
                    _accountNumber = value;
                    FirstSixDigits = value.Substring(0, 6);

                    if (string.IsNullOrEmpty(value.Substring(0, 1)))
                        Bank = BankCode(value.Substring(0, 1));
                    else if (string.IsNullOrEmpty(value.Substring(0, 1)))
                        Bank = BankCode(value.Substring(0, 2));
                    else throw new Exception("account number is not correct");

                    var temp = value.Substring(value.LastIndexOf('-') + 1);
                    string Abanks = temp.PadLeft(8, '0');
                    string Bbanks = temp[0] + temp.Substring(temp.Length - 2).PadLeft(7, '0');
                    switch (Bank)
                    {
                        case "Nordea": LastDigits = Abanks; break;
                        case "Handelsbanken(SHB)": LastDigits = Abanks; break;
                        case "Skandinaviska Enskilda Banken(SEB)": LastDigits = Abanks; break;
                        case "Danske Bank": LastDigits = Abanks; break;
                        case "Tapiola Bank(Tapiola)": LastDigits = Abanks; break;
                        case "DnB NOR Bank ASA(DnB NOR)": LastDigits = Abanks; break;
                        case "Swedbank": LastDigits = Abanks; break;
                        case "S-Bank": LastDigits = Abanks; break;
                        case "savings banks(Sp) and local cooperative banks(Pop) and Aktia": LastDigits = Bbanks; break;
                        case "cooperative banks(Op), OKO Bank and Okopankki": LastDigits = Bbanks; break;
                        case "Ålandsbanken ÅAB)": LastDigits = Abanks; break;
                        case "Sampo Bank(Sampo)": LastDigits = Bbanks; break;
                    }
                  
                    if (LuhnCheck(IBAN))                                
                        IBAN = FirstSixDigits + LastDigits;   
                    else throw new Exception("account number is not correct");
                }        
                else throw new Exception("account number is not correct");
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
