using FinalNewBankApp.Base;

namespace FinalNewBankApp.Accounts;

internal class SavingsAccount : AccountBase
{
    protected SavingsAccount() { }

    public SavingsAccount(decimal startBalance, string accountName, string accountNumber, DateTime dateTime)
        : base(startBalance, accountName, accountNumber, dateTime)
    {
        InterestRate = 0.05m;
    }

    internal override decimal Balance()
    {
        var transactionSum = BankTransactions.Sum(x => x.Amount);
        return transactionSum + StartingBalance;
    }
}