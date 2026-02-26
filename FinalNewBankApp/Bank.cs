using FinalNewBankApp;
using FinalNewBankApp.Accounts;
using FinalNewBankApp.Base;
using System;
using System.Collections.Generic;

namespace FinalNewBankApp;

internal class Bank
{
    private readonly AccountRepository _accountRepository = new();
    private readonly AccountHandler _accountHandler = new();

    internal void ShowBankMenu()
    {
        var actions = new Dictionary<string, Action>
        {
            ["1"] = CreateAccount,
            ["2"] = DeleteAccount,
            ["3"] = () => { _accountRepository.ShowAll(); WaitForKey(); },
            ["4"] = ManageAccount,
            ["5"] = () => { RunSeedTest(); WaitForKey(); },
            ["0"] = () => { WriteLineColored("Avsluta...", ConsoleColor.Red); }
        };

        while (true)
        {
            PrintMainMenu();

            string choice = Console.ReadLine()?.Trim() ?? "";

            if (choice == "0")
            {
                actions["0"]();
                return;
            }

            if (actions.TryGetValue(choice, out var action))
            {
                action();
            }
            else
            {
                WriteLineColored("Felaktigt val, försök igen.", ConsoleColor.Red);
                WaitForKey();
            }
        }
    }

    private static void PrintMainMenu()
    {
        Console.Clear();

        WriteLineColored("=== Bankmeny ===", ConsoleColor.Green);

        Console.ForegroundColor = ConsoleColor.DarkBlue;
        Console.WriteLine("1. Skapa konto");
        Console.WriteLine("2. Ta bort konto");
        Console.WriteLine("3. Visa alla konton");
        Console.WriteLine("4. Hantera konto");
        Console.WriteLine("5. Interest test (DEBUG)");
        Console.WriteLine("0. Avsluta");
        Console.ResetColor();

        WriteColored("Välj ett alternativ: ", ConsoleColor.Magenta);
    }

    private void CreateAccount()
    {
        WriteLineColored("=== Skapa konto ===", ConsoleColor.Green);

        Console.ForegroundColor = ConsoleColor.DarkBlue;
        Console.WriteLine("Välj kontotyp:");
        Console.WriteLine("1. Bankkonto");
        Console.WriteLine("2. ISK-konto");
        Console.WriteLine("3. Uddevalla-konto");
        Console.ResetColor();

        WriteColored("Val Kontotyp: ", ConsoleColor.Magenta);
        string accountTypeChoice = Console.ReadLine()?.Trim() ?? "";

        if (accountTypeChoice is not ("1" or "2" or "3"))
        {
            WriteLineColored("Felaktigt val av kontotyp.", ConsoleColor.Red);
            WaitForKey();
            return;
        }

        WriteColored("Ange kontonamn: ", ConsoleColor.Magenta);
        string accountName = Console.ReadLine() ?? "";

        string accountNumber = _accountRepository.GenerateUniqueAccountNumber();
        decimal initialBalance = ReadInitialBalance();

        var account = CreateAccountByType(accountTypeChoice, accountName, accountNumber, initialBalance);

        _accountRepository.Add(account);
        PrintAccountCreated(account, accountTypeChoice);

        WaitForKey();
    }

    private decimal ReadInitialBalance()
    {
        WriteLineColored("Vill du sätta in pengar nu i kontot? (j/N):", ConsoleColor.Yellow);

        if (Console.ReadLine()?.Trim().ToLowerInvariant() != "j")
        {
            WriteLineColored("Du valde inget startbelopp.", ConsoleColor.DarkGray);
            return 0m;
        }

        WriteColored("Skriv Startbelopp: ", ConsoleColor.Yellow);

        string input = Console.ReadLine()?.Trim() ?? "";
        return decimal.TryParse(input, out decimal amount) && amount >= 0 ? amount : 0m;
    }

    private static AccountBase CreateAccountByType(string accountType, string accountName, string accountNumber, decimal initialBalance)
    {
        var openDate = DateTime.Now;

        return accountType switch
        {
            "1" => new BankAccount(initialBalance, accountName, accountNumber, openDate),
            "2" => new IskAccount(initialBalance, accountName, accountNumber, openDate),
            "3" => new UddevallaAccount(initialBalance, accountName, accountNumber, openDate),
            _ => throw new ArgumentException("Invalid account type")
        };
    }

    private static void PrintAccountCreated(AccountBase account, string accountType)
    {
        string kontotyp = accountType switch
        {
            "1" => "Bankkonto",
            "2" => "ISK-konto",
            "3" => "Uddevalla-konto",
            _ => "Konto"
        };

        WriteLineColored($"=== Ditt {kontotyp} har skapats ===", ConsoleColor.Green);

        Console.ForegroundColor = ConsoleColor.DarkYellow;
        Console.WriteLine(
            $"AccountName: {account.AccountName}   " +
            $"AccountNumber: {account.AccountNumber}   " +
            $"Saldo: {account.Balance()} Kr   " +
            $"Date: {account.OpenDate}");
        Console.ResetColor();
    }

    private void DeleteAccount()
    {
        if (!_accountRepository.HasAny())
        {
            WriteLineColored("Inga konton finns att ta bort.", ConsoleColor.Red);
            WaitForKey();
            return;
        }

        var account = SelectAccountFromList("\nVälj konto (nummer i listan)(eller 0 för att avbryta):");
        if (account is null) return;

        _accountRepository.Remove(account);
        WriteLineColored($"Konto {account.AccountNumber} har tagits bort.", ConsoleColor.Green);
        WaitForKey();
    }

    private void ManageAccount()
    {
        if (!_accountRepository.HasAny())
        {
            WriteLineColored("Inga konton finns.", ConsoleColor.Red);
            WaitForKey();
            return;
        }

        var account = SelectAccountFromList("\nVälj konto (nummer i listan)(eller 0 för att avbryta):");
        if (account is null) return;

        _accountHandler.ShowMenu(account);
    }

    private AccountBase? SelectAccountFromList(string prompt)
    {
        _accountRepository.ShowAll();

        WriteLineColored(prompt, ConsoleColor.Magenta);

        var accounts = _accountRepository.GetAll();

        if (!int.TryParse(Console.ReadLine(), out int selection))
        {
            WriteLineColored("Ogiltigt val.", ConsoleColor.Red);
            WaitForKey();
            return null;
        }

        if (selection == 0)
            return null;

        if (selection < 1 || selection > accounts.Count)
        {
            WriteLineColored("Ogiltigt val.", ConsoleColor.Red);
            WaitForKey();
            return null;
        }

        return accounts[selection - 1];
    }

    private static void WaitForKey()
    {
        WriteLineColored("Tryck på valfri tangent för att fortsätta...", ConsoleColor.DarkCyan);
        Console.ReadKey();
    }

    private void RunSeedTest()
    {
        InterestTestSeed.Run();
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
}