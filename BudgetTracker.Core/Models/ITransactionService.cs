using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BudgetTracker.Core.Models
{
    public interface ITransactionService
    {
        void AddTransaction(TransactionModel transaction);

        List<TransactionModel> GetAllTransactions();

        decimal GetBalance();
    }
}
