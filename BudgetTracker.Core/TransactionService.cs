using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BudgetTracker.Core.Enums;
using BudgetTracker.Core.Models;
using System.Transactions;

namespace BudgetTracker.Core
{
    public class TransactionService : ITransactionService
    {
        private readonly IStorageService storage;

        private List<TransactionModel> transactions;

        public TransactionService(IStorageService storage)
        {
            this.storage = storage;
            transactions = this.storage.LoadTransactions();
        }

        public void AddTransaction(TransactionModel transaction)
        {
            transactions.Add(transaction);
            storage.SaveTransactions(transactions);
        }

        public List<TransactionModel> GetAllTransactions()
        {
            // return a copy
            return transactions; 
        }

        public decimal GetBalance()
        {
            return transactions.Sum(t => t.Type == TransactionType.Income ? t.Amount : -t.Amount);
        }
    }
}

