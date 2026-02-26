using FinalNewBankApp.Base;
using System;
using System.Collections.Generic;
using System.Linq;

namespace FinalNewBankApp
{
    internal class AccountRepository
    {
        private readonly List<AccountBase> _accounts = new();
        private readonly Random _random = new();

        public int Count => _accounts.Count;
        public bool HasAny() => _accounts.Any();
        public List<AccountBase> GetAll() => _accounts.ToList();

        public void Add(AccountBase account) => _accounts.Add(account);
        public void Remove(AccountBase account) => _accounts.Remove(account);

        public string GenerateUniqueAccountNumber()
        {
            string accountNumber;
            do
            {
                accountNumber = _random.Next(100000000, 999999999).ToString();
            }
            while (_accounts.Any(a => a.AccountNumber == accountNumber));

            return accountNumber;
        }

        public void ShowAll()
        {
            if (!_accounts.Any())
            {
                WriteLineColored("Inga konton finns.", ConsoleColor.Red);
                return;
            }

            WriteLineColored("=== Alla konton ===", ConsoleColor.Green);

            var lines = _accounts.Select((account, index) =>
                $"{index + 1,-3} " +
                $"AccountName: {account.AccountName,-20} " +
                $"AccountNumber: {account.AccountNumber,-12} " +
                $"Saldo: {account.Balance(),-5} Kr " +
                $"Date: {account.OpenDate}"
            );

            WriteLinesColored(lines, ConsoleColor.DarkYellow);
        }

        private static void WriteLineColored(string text, ConsoleColor color)
        {
            Console.ForegroundColor = color;
            Console.WriteLine(text);
            Console.ResetColor();
        }

        private static void WriteLinesColored(IEnumerable<string> lines, ConsoleColor color)
        {
            Console.ForegroundColor = color;
            foreach (var line in lines)
                Console.WriteLine(line);
            Console.ResetColor();
        }
    }
}