using System;
using Visma.Types.Models;
using System.Linq;

namespace Visma.FinnishAccountNumbers
{
    class Program
    {
        static void Main(string[] args)
        {
            Account account;
            do
            {
                account = new Account();
                try
                {    
                    Console.Write("account number: ");
                    account.AccountNumber = Console.ReadLine();
                }
                catch(Exception e) { Console.WriteLine(e); }


            } while (string.IsNullOrEmpty(account.AccountNumber));

            Console.WriteLine($"Account: {account.AccountNumber}");
            Console.WriteLine($"Bank: {account.Bank}");
            Console.WriteLine($"{account.IBAN}");
            Console.ReadKey();
        }  

        
    }
}
