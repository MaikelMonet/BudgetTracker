using BudgetTracker.Core.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BudgetTracker.Core.Models
{
    public class TransactionModel
    {
        public DateTime Date { get; set; }

        public string Description { get; set; } = string.Empty;

        public decimal Amount { get; set; }

        public TransactionType Type { get; set; }
    }
}
