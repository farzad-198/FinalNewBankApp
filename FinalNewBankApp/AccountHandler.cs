
using FinalNewBankApp.Base;

namespace FinalNewBankApp;

internal class AccountHandler
{
    public void ShowMenu(AccountBase account)
    {
        while (true)
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("=== Hantera konto ===");
            Console.ResetColor();

            Console.ForegroundColor = ConsoleColor.DarkYellow;
            Console.WriteLine($"Kontonamn: {account.AccountName}");
            Console.WriteLine($"Kontonummer: {account.AccountNumber}");
            Console.WriteLine($"Date: {account.OpenDate}");
            Console.ResetColor();

            Console.ForegroundColor = ConsoleColor.DarkBlue;
            Console.WriteLine();
            Console.WriteLine("1. Sätt in pengar");
            Console.WriteLine("2. Ta ut pengar");
            Console.WriteLine("3. Visa saldo");
            Console.WriteLine("4. Visa transaktioner");
            Console.WriteLine("0. Tillbaka till huvudmenyn");
            Console.ResetColor();

            Console.ForegroundColor = ConsoleColor.Magenta;
            Console.Write("Välj ett alternativ: ");
            Console.ResetColor();

            string menuChoice = Console.ReadLine();

            switch (menuChoice)
            {
                case "1":
                    Deposit(account);
                    break;

                case "2":
                    Withdraw(account);
                    break;

                case "3":
                    ShowBalance(account);
                    break;

                case "4":
                    ShowTransactions(account);
                    break;

                case "0":
                    return;

                default:
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Felaktigt val, försök igen.");
                    Console.ResetColor();
                    Console.ForegroundColor = ConsoleColor.DarkCyan;
                    Console.WriteLine("Tryck på valfri tangent för att fortsätta...");
                    Console.ResetColor();
                    Console.ReadKey();
                    break;
            }
        }
    }

    private void Deposit(AccountBase account)
    {
        Console.ForegroundColor = ConsoleColor.Magenta;
        Console.Write("Ange belopp att sätta in: ");
        Console.ResetColor();
        if (decimal.TryParse(Console.ReadLine(), out decimal amount) && amount > 0)
        {
            account.Deposit(amount, DateTime.Now);
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine($"{amount} Kr har satts in på kontot.");
            Console.ResetColor();
        }
        else
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("Ogiltigt belopp.");
            Console.ResetColor();
        }
        WaitForKey();
    }

    private void Withdraw(AccountBase account)
    {
        Console.ForegroundColor = ConsoleColor.Magenta;
        Console.Write("Ange belopp att ta ut: ");
        Console.ResetColor();
        if (decimal.TryParse(Console.ReadLine(), out decimal amount) && amount > 0)
        {
            bool success = account.Withdraw(amount, DateTime.Now);
            if (success)
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine($"{amount} Kr har tagits ut från kontot.");
                Console.ResetColor();
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Not enough money to withdraw");
                Console.ResetColor();
            }
        }
        else
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("Ogiltigt belopp.");
            Console.ResetColor();
        }
        WaitForKey();
    }

    private void ShowBalance(AccountBase account)
    {
        Console.ForegroundColor = ConsoleColor.DarkYellow;
        Console.WriteLine($"\nNuvarande saldo: {account.Balance()} Kr");
        Console.ResetColor();
        WaitForKey();
    }

    private void ShowTransactions(AccountBase account)
    {
        Console.Clear();
        Console.ForegroundColor = ConsoleColor.Green;
        Console.WriteLine("=== Transaktioner ===");
        Console.ResetColor();

        Console.ForegroundColor = ConsoleColor.DarkYellow;
        account.PrintTransaction();
        Console.ResetColor();

        Console.ForegroundColor = ConsoleColor.Cyan;
        Console.WriteLine($"\nSaldo: {account.Balance()} kr");
        Console.ResetColor();

        WaitForKey();
    }

    private static void WaitForKey()
    {
        Console.ForegroundColor = ConsoleColor.DarkCyan;
        Console.WriteLine("Tryck på valfri tangent för att fortsätta...");
        Console.ResetColor();
        Console.ReadKey();
    }
}
