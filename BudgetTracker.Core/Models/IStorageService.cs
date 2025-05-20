using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BudgetTracker.Core.Models
{
    public interface IStorageService
    {
        List<TransactionModel> LoadTransactions();

        void SaveTransactions(List<TransactionModel> transactions);
    }
}
