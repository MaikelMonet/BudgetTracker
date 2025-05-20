using BudgetTracker.Core.Enums;
using BudgetTracker.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

namespace BudgetTracker.App
{
    public static class UIMethods
    {
        public static void AddTransaction(ITransactionService service)
        {
            Console.Write("Enter description: ");
            string description = Console.ReadLine() ?? "";

            Console.Write("Enter amount: ");

            decimal amount;

            if (decimal.TryParse(Console.ReadLine(), out decimal parsedAmount))
            {
                amount = parsedAmount;
            }
            else
            {
                amount = 0;
            }

            Console.Write("Type (1 = Income, 2 = Expense): ");

            TransactionType type;

            if (Console.ReadLine() == "1")
            {
                type = TransactionType.Income;
            }
            else
            {
                type = TransactionType.Expense;
            }

            var transaction = new TransactionModel
            {
                Date = DateTime.Now,
                Description = description,
                Amount = amount,
                Type = type
            };

            service.AddTransaction(transaction);
            Console.WriteLine("Transaction added. Press Enter to continue.");
            Console.ReadLine();
        }

        public static void ViewTransactions(ITransactionService service)
        {
            Console.WriteLine("\n--- Transactions ---");

            foreach (TransactionModel transaction in service.GetAllTransactions())
            {
                Console.WriteLine($"{transaction.Date:g} | {transaction.Type} | {transaction.Description} | {transaction.Amount:C}");
            }

            Console.WriteLine("\nPress Enter to return to menu.");
            Console.ReadLine();
        }

        public static void ShowBalance(ITransactionService service)
        {
            var balance = service.GetBalance();
            Console.WriteLine($"\nCurrent Balance: {balance:C}");
            Console.WriteLine("Press Enter to return to menu.");
            Console.ReadLine();
        }

        public static void ViewTransactionsByDate(ITransactionService service)
        {
            Console.WriteLine("\nEnter start date (yyyy-mm-dd): ");

            if (!DateTime.TryParse(Console.ReadLine(), out DateTime starDate))
            {
                Console.WriteLine("Invalid start date. Press Enter to return");
                Console.ReadLine();
                return;
            }

            Console.WriteLine("\nEnter end date (yyyy-mm-dd): ");

            if (!DateTime.TryParse(Console.ReadLine(), out DateTime endDate))
            {
                Console.WriteLine("Invalid start date. Press Enter to return");
                Console.ReadLine();
                return;
            }

            var result = service.GetAllTransactions().Where(x => x.Date >= starDate && x.Date <= endDate).ToList();
            
            if (result.Count == 0)
            {
                Console.WriteLine("There were no transactions withing those dates. Press enter to continue");
                Console.ReadLine();
            }
            else
            {
                Console.WriteLine($"Here are the transactions from {starDate:g} to {endDate:g}: ");

                foreach (TransactionModel transaction in result)
                {
                    Console.WriteLine($"{transaction.Date:g} | {transaction.Type} | {transaction.Description} | {transaction.Amount:C}");
                }

                Console.WriteLine("\nPress Enter to return to menu.");
                Console.ReadLine();
            }
        }

        public static void ViewTransactionsByCategory(ITransactionService service)
        {
            Console.WriteLine("\nWhat category would you like to filter by?");

            Console.Write("Type (1 = Income, 2 = Expense): ");

            if (!int.TryParse(Console.ReadLine(),out int choice))
            {
                Console.WriteLine("Invalid choice! Press enter to continue");
                Console.ReadLine();
                return;
            }
            else if(choice == 1)
            {
                var result = service.GetAllTransactions().Where(t => t.Type == TransactionType.Income).ToList();

                if (result.Count > 0)
                {
                    foreach (TransactionModel transaction in result)
                    {
                        Console.WriteLine($"{transaction.Date:g} | {transaction.Type} | {transaction.Description} | {transaction.Amount:C}");
                    }

                    Console.WriteLine("\nPress Enter to return to menu.");
                    Console.ReadLine(); 
                }
                else
                {
                    Console.WriteLine("There were no transactions for that Category. Press enter to continue");
                    Console.ReadLine();
                }
            }
            else
            {
                var result = service.GetAllTransactions().Where(t => t.Type == TransactionType.Expense).ToList();

                if (result.Count > 0)
                {
                    foreach (TransactionModel transaction in result)
                    {
                        Console.WriteLine($"{transaction.Date:g} | {transaction.Type} | {transaction.Description} | {transaction.Amount:C}");
                    }

                    Console.WriteLine("\nPress Enter to return to menu.");
                    Console.ReadLine(); 
                }
                else
                {
                    Console.WriteLine("There were no transactions for that Category. Press enter to continue");
                    Console.ReadLine();
                }
            }
        }

        public static void ViewMonthlySummary(ITransactionService service)
        {
            List<TransactionModel> transactions = service.GetAllTransactions();

            var monthlyGroups = transactions
                                 .GroupBy(t => new { t.Date.Year, t.Date.Month })
                                 .OrderByDescending(g => g.Key.Year)
                                 .ThenByDescending(g => g.Key.Month);

            Console.WriteLine("\n=== Monthly Summary ===");

            foreach (var group in monthlyGroups)
            {
                decimal income = group.Where(t => t.Type == TransactionType.Income).Sum(t => t.Amount);
                decimal expenses = group.Where(t => t.Type == TransactionType.Expense).Sum(t => t.Amount);
                decimal balance = income - expenses;

                string monthName = new DateTime(group.Key.Year, group.Key.Month, 1).ToString("MMMM yyyy");

                Console.WriteLine($"\n{monthName}");
                Console.WriteLine($"Income: {income:C}");
                Console.WriteLine($"Expenses: {expenses:C}");
                Console.WriteLine($"Balance: {balance:C}");
            }


            Console.WriteLine("\nPress Enter to return to the menu");
            Console.ReadLine();

        }
    }
}
