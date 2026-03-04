using FinalNewBankApp.Base;

namespace FinalNewBankApp;

public class BankTransaction
{
    public int Id { get; set; }
    internal decimal Amount { get; set; }
    internal DateTime TransactionalDate { get; set; }
    public string Transaction { get; set; } = string.Empty;
    public Guid AccountBaseId { get; set; }

    public AccountBase Account { get; set; }
}
