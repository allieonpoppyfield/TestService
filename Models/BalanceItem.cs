using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TestService.Models
{
    public class BalanceItem
    {
        [JsonProperty("account_id")]
        public long AcoountID { get; set; }
        [JsonProperty("period")]
        public int Period { get; set; }
        [JsonProperty("in_balance")]
        public decimal InBalance { get; set; }
        [JsonProperty("calculation")]
        public decimal Calculation { get; set; }
        public string GetPeriodYear() => Period.ToString()[0..3];
        public string GetPeriodMonth() => Period.ToString()[4..];
    }

    public class BalanceList
    {
        [JsonProperty("balance")]
        public List<BalanceItem> Balances { get; set; }
    }
}
