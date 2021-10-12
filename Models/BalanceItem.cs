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
        public long AccountID { get; set; }
        [JsonProperty("period")]
        public int Period { get; set; }
        [JsonProperty("in_balance")]
        public decimal InBalance { get; set; }
        [JsonProperty("calculation")]
        public decimal Calculation { get; set; }

        public int PeriodYear { get; private set; }
        public int PeriodMonth { get; private set; }
        public int PeriodQuarter { get; private set; }
        
        public (int year, int quarter, int mont) PeriodsTupple { get; private set; }

        public void SetPeriods()
        {
            if (int.TryParse(Period.ToString()[0..4], out int _year) && int.TryParse(Period.ToString()[4..], out int _month))
            {
                PeriodYear = _year;
                PeriodMonth = _month;
                PeriodQuarter = (_month + 2) / 3;

                PeriodsTupple = (PeriodYear, PeriodQuarter, PeriodMonth);
            }
            else throw new InvalidCastException("Неверный формат периода");
        }
    }

    public class BalanceList
    {
        [JsonProperty("balance")]
        public List<BalanceItem> Balances { get; set; }
    }
}
