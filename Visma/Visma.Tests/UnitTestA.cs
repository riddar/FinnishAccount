using Microsoft.VisualStudio.TestTools.UnitTesting;
using Visma.FinnishAccountNumbers.Validation;
using Visma.Types.Models;

namespace Visma.Tests
{
    [TestClass]
    public class UnitTestA
    {
        [DataTestMethod]
        [DataRow("123456-785")]
        public void CheckIfLuhn(string accountNumber)
        {
            Account account = new Account { AccountNumber = accountNumber };
            Luhn.LuhnCheck(account.IBAN);  
        }

        [TestMethod]
        public void AccountNumberIBANIs14DigitsLong()
        {
            Account account = new Account { AccountNumber = "123456-785" };
            Assert.IsTrue(account.IBAN.Length == 14);
        }

        [DataTestMethod]
        [DataRow("123456-785", "Nordea")]
        public void AccountNumberHasSameBank(string accountNumber, string expectedBank)
        {
            Account account = new Account { AccountNumber = accountNumber };

            Assert.AreEqual(expectedBank, account.Bank);
        }

        [DataTestMethod]
        [DataRow("123456-785")]
        public void AccountNumbersAreFilledWithZerosFromLeftAfterSixDigits(string accountNumber)
        {
            Account account = new Account { AccountNumber = accountNumber };

            Assert.IsTrue(account.IBAN[7] == '0');
        }
    }
}
