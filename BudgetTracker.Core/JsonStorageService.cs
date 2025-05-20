using BudgetTracker.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace BudgetTracker.Core
{
    public class JsonStorageService : IStorageService
    {
        private readonly string filePatch;

        public JsonStorageService(string filePatch)
        {
            this.filePatch = filePatch;
        }

        public List<TransactionModel> LoadTransactions()
        {
            if (!File.Exists(filePatch))
            {
                return new List<TransactionModel>();
            }

            string json = File.ReadAllText(filePatch);

            return JsonSerializer.Deserialize<List<TransactionModel>>(json) ?? new List<TransactionModel>();
        }

        public void SaveTransactions(List<TransactionModel> transactions)
        {
            //Json option
            var option = new JsonSerializerOptions{ WriteIndented = true };

            //Convert List to Json
            string json = JsonSerializer.Serialize(transactions, option);

            //Save the List
            File.WriteAllText(filePatch, json);
        }
    }
}
