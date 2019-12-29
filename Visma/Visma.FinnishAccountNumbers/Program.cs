using System;
using Visma.FinnishAccountNumbers.Validation;
using Visma.Types.Models;

namespace Visma.FinnishAccountNumbers
{
    class Program
    {
        static void Main(string[] args)
        {
            bool AccountValid = false;
            Account account = new Account();

            do
            {
                Console.Write("account number: ");
                string accountNumber = Console.ReadLine();

                if(accountNumber.Length != 14 && Luhn.LuhnCheck(accountNumber))
                    Console.WriteLine($"{accountNumber} is wrong please try again!");
                else
                {
                    if (!Luhn.isDigitsOnly(accountNumber.Replace("-", "")))
                        Console.WriteLine($"{accountNumber} is wrong please try again!");
                    else
                    {
                        account.AccountNumber = accountNumber;
                            
                        if (!Luhn.LuhnCheck(account.IBAN))
                            Console.WriteLine($"{accountNumber} is wrong please try again!");
                        else
                            AccountValid = true;
                    }
                }  
            } while (!AccountValid);

            Console.WriteLine($"Account: {account.AccountNumber}");
            Console.WriteLine($"Bank: {account.Bank}");
            Console.WriteLine($"{account.IBAN}");
            Console.ReadKey();
        }  

        
    }
}
