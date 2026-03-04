using FinalNewBankApp.Base;

namespace FinalNewBankApp.Accounts;

internal class StudentAccount : AccountBase
{
    protected StudentAccount() { }

    public StudentAccount(decimal startBalance, string accountName, string accountNumber, DateTime dateTime)
        : base(startBalance, accountName, accountNumber, dateTime)
    {
        InterestRate = 0.04m;
    }

    internal override decimal Balance()
    {
        var transactionSum = BankTransactions.Sum(x => x.Amount);
        return transactionSum + StartingBalance;
    }
}