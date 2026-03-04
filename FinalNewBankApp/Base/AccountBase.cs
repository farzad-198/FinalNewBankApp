namespace FinalNewBankApp.Base;

public abstract class AccountBase
{
    public Guid Id { get; set; } = Guid.NewGuid();

    public DateTime OpenDate { get; set; }

    protected decimal StartingBalance { get; set; } = 0;

    public string AccountName { get; set; } = string.Empty;

    public string AccountNumber { get; set; } = string.Empty;

    public decimal InterestRate { get; set; } = 0;

    
    public List<BankTransaction> BankTransactions { get; set; } = new();

  
    protected AccountBase()
    {
    }

    
    protected AccountBase(decimal startBalance, string accountName, string accountNumber, DateTime openDate)
    {
        StartingBalance = startBalance;
        AccountName = accountName;
        AccountNumber = accountNumber;
        OpenDate = openDate;
    }

    internal abstract decimal Balance();

    internal virtual void Deposit(decimal amount, DateTime date)
    {
        if (amount <= 0)
            throw new ArgumentException("Amount must be greater than zero", nameof(amount));

        var transaction = new BankTransaction
        {
            Amount = amount,
            TransactionalDate = date,
            Transaction = "Deposit"
        };

        BankTransactions.Add(transaction);
    }

    internal virtual bool Withdraw(decimal amount, DateTime date)
    {
        if (amount <= 0)
            throw new ArgumentException("Amount must be greater than zero", nameof(amount));

        var balance = Balance();

        if (balance < amount)
            return false;

        var transaction = new BankTransaction
        {
            Amount = -amount,
            TransactionalDate = date,
            Transaction = "Withdraw"
        };

        BankTransactions.Add(transaction);

        return true;
    }

    internal virtual void PrintTransaction()
    {
        if (BankTransactions.Count == 0)
        {
            Console.ForegroundColor = ConsoleColor.DarkGray;
            Console.WriteLine("Inga transaktioner ännu.");
            Console.ResetColor();
            return;
        }

        foreach (var transaction in BankTransactions)
        {
            Console.ForegroundColor = ConsoleColor.DarkYellow;

            Console.WriteLine(
                $"Transaction: {transaction.Transaction,-15} " +
                $"Date: {transaction.TransactionalDate,-20:yyyy-MM-dd HH:mm:ss} " +
                $"Amount: {transaction.Amount,10} kr");

            Console.ResetColor();
        }
    }

    internal decimal CalculateYearlyInterest(int year)
    {
        decimal balance = StartingBalance;
        decimal totalInterest = 0m;

        DateTime start = new DateTime(year, 1, 1);
        DateTime end = new DateTime(year, 12, 31);

        for (DateTime day = start; day <= end; day = day.AddDays(1))
        {
            foreach (var transaction in BankTransactions)
            {
                if (transaction.TransactionalDate.Date == day.Date)
                {
                    balance += transaction.Amount;
                }
            }

            totalInterest += balance * (InterestRate / 365m);
        }

        return totalInterest;
    }
}