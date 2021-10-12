using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TestService.Models
{
    public class PaymentItem
    {
        [JsonProperty("account_id")]
        public long AccountID { get; set; }
        [JsonProperty("date")]
        public DateTime Date { get; set; }
        [JsonProperty("sum")]
        public decimal Sum { get; set; }
        [JsonProperty("payment_guid")]
        public Guid PaymentGuid { get; set; }

        public void SetPeriods()
        {
            this.PeriodsTupple = (Date.Year, (Date.Month + 2) / 3, Date.Month);
        }

        public (int year, int quarter, int month) PeriodsTupple { get; private set; }



    }

    public class PaymentList
    {
        public List<PaymentItem> Payments { get; set; }
    }
}
