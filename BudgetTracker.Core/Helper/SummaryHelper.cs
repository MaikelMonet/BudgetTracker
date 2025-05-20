using BudgetTracker.Core.Enums;
using BudgetTracker.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BudgetTracker.Core.Helper
{
    public static class SummaryHelper
    {
        public record MonthlySummary(int Year, int Month, decimal Income, decimal Expenses, decimal Balance);

        public static List<MonthlySummary> GetMonthlySummary(List<TransactionModel> transactions)
        {
            return transactions
                .GroupBy(t => new { t.Date.Year, t.Date.Month })
                .Select(g =>
                {
                    decimal income = g.Where(t => t.Type == TransactionType.Income).Sum(t => t.Amount);
                    decimal expenses = g.Where(t => t.Type == TransactionType.Expense).Sum(t => t.Amount);
                    return new MonthlySummary(
                        g.Key.Year,
                        g.Key.Month,
                        income,
                        expenses,
                        income - expenses
                    );
                })
                .OrderByDescending(s => s.Year)
                .ThenByDescending(s => s.Month)
                .ToList();
        }
    }

}
