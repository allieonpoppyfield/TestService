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
        public async Task<IEnumerable<Conductance>> GetConductantList(long accountId, PeriodType periodType)
        {
            var _balances = await GetBalanceData();
            var _payments = await GetPaymentData();


            _balances = _balances.Where(x => x.AccountID == accountId).ToList();


            var joinedList = _balances.GroupJoin(_payments, f => f.PeriodsTupple,
                                                            s => s.PeriodsTupple, (f, s) =>
                    new
                    {
                        f.PeriodYear,
                        f.PeriodMonth,
                        f.PeriodQuarter,
                        f.Period,
                        f.InBalance,
                        f.Calculation,
                        PaidForPeriod = s.Sum(s => s.Sum)
                    }).ToList();

            IEnumerable<Conductance> result = new List<Conductance>();

            switch (periodType)
            {
                case PeriodType.Year:
                    result = joinedList.OrderByDescending(x => x.PeriodYear).ThenByDescending(x => x.PeriodMonth).GroupBy(x => x.PeriodYear).Select(x => new Conductance
                    {
                        PeriodName = $"Year: {x.Key}",
                        IncomingBalance = x.Sum(a => a.InBalance),
                        Accured = x.Sum(a => a.Calculation),
                        Paid = x.Sum(a => a.PaidForPeriod),
                        OutgoingBalance = x.Sum(a => a.InBalance) + x.Sum(a => a.Calculation) - x.Sum(a => a.PaidForPeriod)
                    }).ToList();
                    break;

                case PeriodType.Month:
                    result = joinedList.OrderByDescending(x => x.PeriodYear).ThenByDescending(x => x.PeriodMonth).GroupBy(x => new { x.PeriodYear, x.PeriodMonth }).Select(x => new Conductance
                    {
                        PeriodName = $"Year: {x.Key.PeriodYear} / Month: {x.Key.PeriodMonth}",
                        IncomingBalance = x.Sum(a => a.InBalance),
                        Accured = x.Sum(a => a.Calculation),
                        Paid = x.Sum(a => a.PaidForPeriod),
                        OutgoingBalance = x.Sum(a => a.InBalance) + x.Sum(a => a.Calculation) - x.Sum(a => a.PaidForPeriod)
                    }).ToList();
                    break;

                case PeriodType.Quarer:
                    result = joinedList.OrderByDescending(x => x.PeriodYear).ThenByDescending(x => x.PeriodMonth).GroupBy(x => new { x.PeriodYear, x.PeriodQuarter }).Select(x => new Conductance
                    {
                        PeriodName = $"Year: {x.Key.PeriodYear} / Quarter: {x.Key.PeriodQuarter}",
                        IncomingBalance = x.Sum(a => a.InBalance),
                        Accured = x.Sum(a => a.Calculation),
                        Paid = x.Sum(a => a.PaidForPeriod),
                        OutgoingBalance = x.Sum(a => a.InBalance) + x.Sum(a => a.Calculation) - x.Sum(a => a.PaidForPeriod)
                    }).ToList();
                    break;
            }

            return result;
        }






        private async Task<List<BalanceItem>> GetBalanceData()
        {
            var balanceFileParh = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "balance.json");
            using StreamReader r = new StreamReader(balanceFileParh);
            string json = await r.ReadToEndAsync();
            var resultList = JsonConvert.DeserializeObject<BalanceList>(json).Balances;
            resultList.ForEach(x => x.SetPeriods());
            return resultList;
        }


        private async Task<List<PaymentItem>> GetPaymentData()
        {
            var balanceFileParh = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "payment.json");
            using StreamReader r = new StreamReader(balanceFileParh);
            string json = await r.ReadToEndAsync();
            var resultList = JsonConvert.DeserializeObject<List<PaymentItem>>(json);
            resultList.ForEach(x => x.SetPeriods());
            return resultList;
        }


    }
}
