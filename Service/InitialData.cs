using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using TestService.Models;

namespace TestService.Service
{
    public class InitialData : IInitialData
    {
        public async Task<List<BalanceItem>> GetBalanceData()
        {
            var balanceFileParh = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "balance.json");
            using StreamReader r = new StreamReader(balanceFileParh);
            string json = await r.ReadToEndAsync();
            return JsonConvert.DeserializeObject<BalanceList>(json).Balances;
        }

        public async Task<List<PaymentItem>> GetPaymentData()
        {
            var balanceFileParh = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "payment.json");
            using StreamReader r = new StreamReader(balanceFileParh);
            string json = await r.ReadToEndAsync();
            return JsonConvert.DeserializeObject<List<PaymentItem>>(json);
        }


    }
}
