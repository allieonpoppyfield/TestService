using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TestService.Models
{
    public class PaymentItem
    {
        public long AccountID { get; set; }
        public DateTime Date { get; set; }
        public decimal Sum { get; set; }
        public Guid PaymentGuid { get; set; }
    }

    public class PaymentList
    {
        public List<PaymentItem> Payments { get; set; }
    }
}
