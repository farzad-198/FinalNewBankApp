using FinalNewBankApp.Accounts;
using FinalNewBankApp.Base;


namespace FinalNewBankApp;

internal static class AccountFactory
{
    public static AccountBase CreateAccount(
        string accountType,
        decimal startBalance,
        string accountName,
        string accountNumber,
        DateTime openDate)
    {
        return accountType switch
        {
            "1" => new BankAccount(startBalance, accountName, accountNumber, openDate),
            "2" => new IskAccount(startBalance, accountName, accountNumber, openDate),
            "3" => new UddevallaAccount(startBalance, accountName, accountNumber, openDate),
            "4" => new StudentAccount(startBalance, accountName, accountNumber, openDate),
            "5" => new SavingsAccount(startBalance, accountName, accountNumber, openDate),

            _ => throw new ArgumentException("Invalid account type")
        };
    }
}


