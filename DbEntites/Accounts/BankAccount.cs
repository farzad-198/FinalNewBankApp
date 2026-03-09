using FinalNewBankApp.Base;

namespace FinalNewBankApp.Accounts;

public class BankAccount : AccountBase
{
   
    protected BankAccount() { }

    public BankAccount(decimal startBalance, string accountName, string accountNumber, DateTime dateTime)
        : base(startBalance, accountName, accountNumber, dateTime)
    {
        InterestRate = 0.01m;
    }

    public override decimal Balance()
    {
        var transactionSum = BankTransactions.Sum(x => x.Amount);
        return transactionSum + StartingBalance;
    }
}