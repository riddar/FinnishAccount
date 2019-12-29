using Microsoft.VisualStudio.TestTools.UnitTesting;
using Visma.FinnishAccountNumbers.Validation;
using Visma.Types.Models;

namespace Visma.Tests
{
    [TestClass]
    public class UnitTestB
    {
        [DataTestMethod]
        [DataRow("423456-781")]
        public void CheckIfLuhn(string accountnumber)
        {
            Account account = new Account { AccountNumber = accountnumber };
            Luhn.LuhnCheck(account.IBAN);
        }

        [DataTestMethod]
        [DataRow("423456-781")]
        public void AccountNumbersAreFilledWithZerosFromLeftAfterSixDigits(string accountNumber)
        {
            Account account = new Account { AccountNumber = accountNumber };

            Assert.IsTrue(account.IBAN.Length == 14);
            Assert.IsTrue(int.Parse(account.LastDigits.Substring(0, 1)) != 0);
        }
    }
}
