using FinalNewBankApp.Base;

namespace FinalNewBankApp;

internal class AccountHandler
{
    private readonly AccountRepository _repository;

    public AccountHandler(AccountRepository repository)
    {
        _repository = repository;
    }

    public void ShowMenu(AccountBase account)
    {
        var actions = new Dictionary<string, Action>
        {
            ["1"] = () => Deposit(account),
            ["2"] = () => Withdraw(account),
            ["3"] = () => ShowBalance(account),
            ["4"] = () => ShowTransactions(account),
        };

        while (true)
        {
            PrintMenu(account);

            string choice = Console.ReadLine()?.Trim() ?? "";

            if (choice == "0")
                return;

            if (actions.TryGetValue(choice, out var action))
            {
                action();
            }
            else
            {
                PrintError("Felaktigt val, försök igen.");
            }
        }
    }

    private void Deposit(AccountBase account)
    {
        if (!TryReadPositiveAmount("Ange belopp att sätta in: ", out decimal amount))
        {
            PrintError("Ogiltigt belopp.");
            return;
        }

        account.Deposit(amount, DateTime.Now);

        _repository.SaveChanges();   // مهم

        WriteLineColored($"{amount} Kr har satts in på kontot.", ConsoleColor.Green);
        WaitForKey();
    }

    private void Withdraw(AccountBase account)
    {
        if (!TryReadPositiveAmount("Ange belopp att ta ut: ", out decimal amount))
        {
            PrintError("Ogiltigt belopp.");
            return;
        }

        bool success = account.Withdraw(amount, DateTime.Now);

        if (success)
        {
            _repository.SaveChanges();   // مهم

            WriteLineColored($"{amount} Kr har tagits ut från kontot.", ConsoleColor.Green);
        }
        else
        {
            WriteLineColored("Not enough money to withdraw", ConsoleColor.Red);
        }

        WaitForKey();
    }

    private static void ShowBalance(AccountBase account)
    {
        WriteLineColored($"\nNuvarande saldo: {account.Balance()} Kr", ConsoleColor.DarkYellow);
        WaitForKey();
    }

    private static void ShowTransactions(AccountBase account)
    {
        Console.Clear();

        WriteLineColored("=== Transaktioner ===", ConsoleColor.Green);

        account.PrintTransaction();

        WriteLineColored($"\nSaldo: {account.Balance()} kr", ConsoleColor.Cyan);

        WaitForKey();
    }

    private static bool TryReadPositiveAmount(string prompt, out decimal amount)
    {
        WriteColored(prompt, ConsoleColor.Magenta);

        string input = Console.ReadLine()?.Trim() ?? "";

        return decimal.TryParse(input, out amount) && amount > 0;
    }

    private static void WriteLineColored(string text, ConsoleColor color)
    {
        Console.ForegroundColor = color;
        Console.WriteLine(text);
        Console.ResetColor();
    }

    private static void WriteColored(string text, ConsoleColor color)
    {
        Console.ForegroundColor = color;
        Console.Write(text);
        Console.ResetColor();
    }

    private static void PrintError(string message)
    {
        WriteLineColored(message, ConsoleColor.Red);
        WaitForKey();
    }

    private static void WaitForKey()
    {
        WriteLineColored("Tryck på valfri tangent för att fortsätta...", ConsoleColor.DarkCyan);
        Console.ReadKey();
    }

    private static void PrintMenu(AccountBase account)
    {
        Console.Clear();

        WriteLineColored("=== Konto ===", ConsoleColor.Green);
        WriteLineColored($"Namn: {account.AccountName}", ConsoleColor.Cyan);
        WriteLineColored($"Kontonummer: {account.AccountNumber}", ConsoleColor.Cyan);
        WriteLineColored($"Öppnat: {account.OpenDate:d}", ConsoleColor.DarkYellow);
        WriteLineColored($"Saldo: {account.Balance()} kr", ConsoleColor.DarkYellow);
        Console.WriteLine();

        WriteLineColored("1) Sätt in pengar", ConsoleColor.Magenta);
        WriteLineColored("2) Ta ut pengar", ConsoleColor.Magenta);
        WriteLineColored("3) Visa saldo", ConsoleColor.Magenta);
        WriteLineColored("4) Visa transaktioner", ConsoleColor.Magenta);
        WriteLineColored("0) Tillbaka", ConsoleColor.Magenta);
        Console.WriteLine();

        WriteColored("Välj ett alternativ: ", ConsoleColor.Magenta);
    }
}