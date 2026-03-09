using FinalNewBankApp.Base;

namespace FinalNewBankApp.Accounts;

public class StudentAccount : AccountBase
{
    protected StudentAccount() { }

    public StudentAccount(decimal startBalance, string accountName, string accountNumber, DateTime dateTime)
        : base(startBalance, accountName, accountNumber, dateTime)
    {
        InterestRate = 0.04m;
    }

    public override decimal Balance()
    {
        var transactionSum = BankTransactions.Sum(x => x.Amount);
        return transactionSum + StartingBalance;
    }
}