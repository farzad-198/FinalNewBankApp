using FinalNewBankApp.Base;

namespace FinalNewBankApp.Accounts;

internal class BankAccount : AccountBase
{
    // سازنده برای Entity Framework
    protected BankAccount() { }

    public BankAccount(decimal startBalance, string accountName, string accountNumber, DateTime dateTime)
        : base(startBalance, accountName, accountNumber, dateTime)
    {
        InterestRate = 0.01m;
    }

    internal override decimal Balance()
    {
        var transactionSum = BankTransactions.Sum(x => x.Amount);
        return transactionSum + StartingBalance;
    }
}