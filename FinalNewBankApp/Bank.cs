using FinalNewBankApp;
using FinalNewBankApp.Accounts;
using FinalNewBankApp.Base;

namespace FinalNewBankApp;

internal class Bank
{
    private readonly AccountRepository _accountRepository = new();
    private readonly AccountHandler _accountHandler = new();
    internal void ShowBankMenu()
    {
        while (true)
        {

            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("=== Bankmeny ===");
            Console.ResetColor();
            Console.ForegroundColor = ConsoleColor.DarkBlue;
            Console.WriteLine("1. Skapa konto");
            Console.WriteLine("2. Ta bort konto");
            Console.WriteLine("3. Visa alla konton");
            Console.WriteLine("4. Hantera konto");
            Console.WriteLine("5. Interest test (DEBUG)");
            Console.WriteLine("0. Avsluta");
            Console.ResetColor();
            Console.ForegroundColor = ConsoleColor.Magenta;
            Console.WriteLine("Välj ett alternativ: ");
            Console.ResetColor();
            var input = Console.ReadLine();
            switch (input)
            {
                case "1":
                    CreateAccount();

                    break;

                case "2":
                    DeleteAccount();
                    break;

                case "3":
                    _accountRepository.ShowAll();
                    WaitForKey();
                    break;

                case "4":
                    ManageAccount();
                    break;
                case "5":
                    RunSeedTest();
                    Console.ForegroundColor = ConsoleColor.DarkCyan;
                    Console.WriteLine("\nTryck på valfri tangent för att fortsätta...");
                    Console.ResetColor();
                    Console.ReadKey();
                    break;
                case "0":
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Avsluta...");
                    Console.ResetColor();
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
    private void CreateAccount()
    {
        Console.ForegroundColor = ConsoleColor.Green;
        Console.WriteLine("=== Skapa konto ===");
        Console.ResetColor();

        Console.ForegroundColor = ConsoleColor.DarkBlue;
        Console.WriteLine("Välj kontotyp:");
        Console.WriteLine("1. Bankkonto");
        Console.WriteLine("2. ISK-konto");
        Console.WriteLine("3. Uddevalla-konto");
        Console.ResetColor();

        Console.ForegroundColor = ConsoleColor.Magenta;
        Console.Write("Val Kontotyp: ");
        Console.ResetColor();
        string accountTypeChoice = Console.ReadLine();
        if (accountTypeChoice != "1" && accountTypeChoice != "2" && accountTypeChoice != "3")
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("Felaktigt val av kontotyp.");
            Console.ResetColor();
            Console.ForegroundColor = ConsoleColor.DarkCyan;
            Console.WriteLine("Tryck på valfri tangent för att fortsätta...");
            Console.ResetColor();
            Console.ReadKey();
            return;

        }
        Console.ForegroundColor = ConsoleColor.Magenta;
        Console.Write("Ange kontonamn: ");
        Console.ResetColor();
        string accountName = Console.ReadLine();

        string accountNumber = _accountRepository.GenerateUniqueAccountNumber();
        decimal initialBalance = ReadInitialBalance();

        AccountBase? account = CreateAccountByType(accountTypeChoice, accountName, accountNumber, initialBalance);
        if (account != null)
        {
            _accountRepository.Add(account);
            PrintAccountCreated(account, accountTypeChoice);
        }
        else
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("Felaktigt val av kontotyp.");
            Console.ResetColor();
        }

        Console.ForegroundColor = ConsoleColor.DarkCyan;
        Console.WriteLine("Tryck på valfri tangent för att fortsätta...");
        Console.ResetColor();
        Console.ReadKey();
    }


    private decimal ReadInitialBalance()
    {
        Console.ForegroundColor = ConsoleColor.Yellow;
        Console.WriteLine("Vill du sätta in pengar nu i kontot? (j/N):");
        Console.ResetColor();

        if (Console.ReadLine()?.Trim().ToLowerInvariant() != "j")
        {
            Console.ForegroundColor = ConsoleColor.DarkGray;
            Console.WriteLine("Du valde inget startbelopp.");
            Console.ResetColor();
            return 0m;
        }

        Console.ForegroundColor = ConsoleColor.Yellow;
        Console.Write("Skriv Startbelopp: ");
        Console.ResetColor();
        return decimal.TryParse(Console.ReadLine(), out decimal amount) && amount >= 0 ? amount : 0m;
    }

    private static AccountBase? CreateAccountByType(string? accountType, string accountName, string accountNumber, decimal initialBalance)
    {
        var openDate = DateTime.Now;
        return accountType switch
        {
            "1" => new BankAccount(initialBalance, accountName, accountNumber, openDate),
            "2" => new IskAccount(initialBalance, accountName, accountNumber, openDate),
            "3" => new UddevallaAccount(initialBalance, accountName, accountNumber, openDate),
            _ => null
        };
    }

    private static void PrintAccountCreated(AccountBase account, string? accountType)
    {
        string kontotyp = accountType switch
        {
            "1" => "Bankkonto",
            "2" => "ISK-konto",
            "3" => "Uddevalla-konto",
            _ => "Konto"
        };

        Console.ForegroundColor = ConsoleColor.Green;
        Console.WriteLine($"=== Ditt {kontotyp} har skapats ===");
        Console.ResetColor();
        Console.ForegroundColor = ConsoleColor.DarkYellow;
        Console.WriteLine($"AccountName: {account.AccountName}   AccountNumber: {account.AccountNumber}   Saldo: {account.Balance()} Kr   Date: {account.OpenDate}");
        Console.ResetColor();
    }
    private void DeleteAccount()
    {
        if (!_accountRepository.HasAny())
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("Inga konton finns att ta bort.");
            Console.ResetColor();
            WaitForKey();
            return;
        }

        _accountRepository.ShowAll();
        Console.ForegroundColor = ConsoleColor.Magenta;
        Console.WriteLine("\nVälj konto (nummer i listan)(eller 0 för att avbryta):");
        Console.ResetColor();

        var accounts = _accountRepository.GetAll();
        if (int.TryParse(Console.ReadLine(), out int selection))
        {
            if (selection == 0)
            {
                return;
            }

            if (selection > 0 && selection <= accounts.Count)
            {
                var accountToDelete = accounts[selection - 1];
                _accountRepository.Remove(accountToDelete);
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine($"Konto {accountToDelete.AccountNumber} har tagits bort.");
                Console.ResetColor();
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Ogiltigt val.");
                Console.ResetColor();
            }
        }
        else
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("Ogiltigt val.");
            Console.ResetColor();
        }
        WaitForKey();
    }
    private void ManageAccount()
    {
        if (!_accountRepository.HasAny())
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("Inga konton finns.");
            Console.ResetColor();
            WaitForKey();
            return;
        }

        _accountRepository.ShowAll();
        Console.ForegroundColor = ConsoleColor.Magenta;
        Console.WriteLine("\nVälj konto (nummer i listan)(eller 0 för att avbryta):");
        Console.ResetColor();

        var accounts = _accountRepository.GetAll();
        if (int.TryParse(Console.ReadLine(), out int selection))
        {
            if (selection == 0)
            {
                return;
            }

            if (selection > 0 && selection <= accounts.Count)
            {
                var selectedAccount = accounts[selection - 1];
                _accountHandler.ShowMenu(selectedAccount);
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Ogiltigt val.");
                Console.ResetColor();
                WaitForKey();
            }
        }
        else
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("Ogiltigt val.");
            Console.ResetColor();
            WaitForKey();
        }
    }

    private static void WaitForKey()
    {
        Console.ForegroundColor = ConsoleColor.DarkCyan;
        Console.WriteLine("Tryck på valfri tangent för att fortsätta...");
        Console.ResetColor();
        Console.ReadKey();
    }
    private void RunSeedTest()
    {
        InterestTestSeed.Run();
    }


}
