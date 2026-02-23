using FinalNewBankApp.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace FinalNewBankApp
{
    internal class AccountRepository
    {
        private readonly List<AccountBase> _accounts = new();
        private readonly Random _random = new();

        public void Add(AccountBase account)
        {
            _accounts.Add(account);
        }

        public void Remove(AccountBase account)
        {
            _accounts.Remove(account);
        }

        public List<AccountBase> GetAll()
        {
            return _accounts.ToList();
        }

        public bool HasAny()
        {
            return _accounts.Count > 0;
        }

        public int Count => _accounts.Count;

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
            if (_accounts.Count == 0)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Inga konton finns.");
                Console.ResetColor();
                return;
            }

            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("=== Alla konton ===");
            Console.ResetColor();

            int index = 1;
            foreach (var account in _accounts)
            {
                Console.ForegroundColor = ConsoleColor.DarkYellow;
                Console.WriteLine(
                    $"{index,-3} " +
                    $"AccountName: {account.AccountName,-20} " +
                    $"AccountNumber: {account.AccountNumber,-12} " +
                    $"Saldo: {account.Balance(),-5} Kr " +
                    $"Date: {account.OpenDate}");
                Console.ResetColor();
                index++;
            }
        }
    }
}
