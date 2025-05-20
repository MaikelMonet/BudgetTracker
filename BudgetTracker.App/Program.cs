using BudgetTracker.Core;
using BudgetTracker.Core.Enums;
using BudgetTracker.Core.Models;
using System.Transactions;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace BudgetTracker.App
{
    public class Program
    {
        private static void Main(string[] args)
        {
            const string filePath = "transactions.json";

            IStorageService storage = new JsonStorageService(filePath);

            ITransactionService service = new TransactionService(storage);

            while (true)
            {
                Console.Clear();
                Console.WriteLine("==== Budget Tracker ====");
                Console.WriteLine("1. Add Transaction");
                Console.WriteLine("2. View All Transactions");
                Console.WriteLine("3. View Balance");
                Console.WriteLine("4. View All Transactions by date");
                Console.WriteLine("5. View All Transactions by Category");
                Console.WriteLine("6. View Monthly Summary");
                Console.WriteLine("7. Exit ");
                Console.Write("Choose an option: ");

                var choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        UIMethods.AddTransaction(service);
                        break;
                    case "2":
                        UIMethods.ViewTransactions(service);
                        break;
                    case "3":
                        UIMethods.ShowBalance(service);
                        break;
                    case "4":
                        UIMethods.ViewTransactionsByDate(service);
                        break;
                    case "5":
                        UIMethods.ViewTransactionsByCategory(service);
                        break;
                    case "6":
                        UIMethods.ViewMonthlySummary(service);
                        break;
                    case "7":
                        return;
                    default:
                        Console.WriteLine("Invalid choice. Press Enter to try again.");
                        Console.ReadLine();
                        break;
                }
            }

            
        }
    }
}