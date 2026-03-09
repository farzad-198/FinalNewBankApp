using FinalNewBankApp.Accounts;
using System;
using System.Collections.Generic;
using System.Text;


namespace FinalNewBankApp
{
    public static class InterestTestSeed
    {
        public static void Run()
        {
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("=== Interest Test Seed ===");
            Console.ResetColor();

            var account = new UddevallaAccount(
                startBalance: 0m,
                accountName: "Test Account",
                accountNumber: "111111111",
                dateTime: new DateTime(2025, 1, 1)
            );

            decimal[] deposits =
            {
        1000m, 500m, 750m, 300m, 1200m,
        400m, 900m, 650m, 800m, 1100m
    };

            int[] months = { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };

            for (int i = 0; i < deposits.Length; i++)
            {
                var date = new DateTime(2025, months[i], 1);
                account.Deposit(deposits[i], date);

                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine(
                    $"Test {i + 1,2}: Deposit {deposits[i],6} kr on {date:yyyy-MM-dd}"
                );
                Console.ResetColor();
            }

            Console.ForegroundColor = ConsoleColor.DarkGray;
            Console.WriteLine("--------------------------------------------");
            Console.ResetColor();

            decimal interest2025 = account.CalculateYearlyInterest(2025);

            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine($"Interest for 2025: {interest2025:F2} kr");
            Console.ResetColor();
        }

    }
}
