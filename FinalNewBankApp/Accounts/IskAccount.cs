using FinalNewBankApp.Base;

namespace FinalNewBankApp.Accounts;

internal class IskAccount : AccountBase
{
   protected IskAccount()
    {
        
    }
    public IskAccount(decimal startBalance, string accountName, string accountNumber, DateTime dateTime)
        : base(startBalance, accountName, accountNumber, dateTime)
    {
        InterestRate = 0.02m;
    }

    internal override decimal Balance()
    {
        var transactionSum = BankTransactions.Sum(x => x.Amount);
        return transactionSum + StartingBalance;
    }
}