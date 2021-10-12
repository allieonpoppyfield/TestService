using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TestService.Models;

namespace TestService.Service
{
    public interface IInitialData
    {
        public Task<List<BalanceItem>> GetBalanceData();
        public Task<List<PaymentItem>> GetPaymentData();
    }
}
