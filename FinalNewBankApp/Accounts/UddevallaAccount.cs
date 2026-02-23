using FinalNewBankApp.Base;


namespace FinalNewBankApp.Accounts
{
    internal class UddevallaAccount : AccountBase
    {
        public UddevallaAccount(decimal startBalance, string accountName, string accountNumber, DateTime dateTime)
            : base(startBalance, accountName, accountNumber, dateTime)
        {
            InterestRate = 0.05m;
        }

        internal override decimal Balance()
        {
            var transactionSum = bankTransactions.Sum(x => x.Amount);
            return transactionSum + StartingBalance;
        }

    }
}
