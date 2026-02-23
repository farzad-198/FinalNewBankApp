namespace FinalNewBankApp;

internal class BankTransaction
{
    internal decimal Amount { get; set; }
    internal DateTime TransactionalDate { get; set; }
    public string Transaction { get; set; } = string.Empty;
}
