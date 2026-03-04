using FinalNewBankApp.Base;
using FinalNewBankApp.Models;
using Microsoft.EntityFrameworkCore;

namespace FinalNewBankApp
{
    internal class AccountRepository
    {
        private readonly BankDbContext _context;
        private readonly Random _random = new();

        public AccountRepository()
        {
            _context = new BankDbContext();
        }

        public int Count => _context.Accounts.Count();

        public bool HasAny()
        {
            return _context.Accounts.Any();
        }

        public List<AccountBase> GetAll()
        {
            return _context.Accounts.ToList();
        }

        public void Add(AccountBase account)
        {
            _context.Accounts.Add(account);
            _context.SaveChanges();
        }

        public void Remove(AccountBase account)
        {
            _context.Accounts.Remove(account);
            _context.SaveChanges();
        }

        public AccountBase? GetAccountById(Guid id)
        {
            return _context.Accounts.Find(id);
        }

        public string GenerateUniqueAccountNumber()
        {
            string accountNumber;

            do
            {
                accountNumber = _random.Next(100000000, 999999999).ToString();
            }
            while (_context.Accounts.Any(a => a.AccountNumber == accountNumber));

            return accountNumber;
        }
        public void ShowAll()
        {
            var accounts = _context.Accounts.ToList();

            if (!accounts.Any())
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

            foreach (var account in accounts)
            {
                Console.ForegroundColor = ConsoleColor.DarkYellow;

                Console.WriteLine(
                    $"{index,-3} " +
                    $"AccountName: {account.AccountName,-20} " +
                    $"AccountNumber: {account.AccountNumber,-12} " +
                    $"Saldo: {account.Balance(),-10} Kr " +
                    $"Date: {account.OpenDate}"
                );

                Console.ResetColor();
                index++;
            }
        }
    }
}