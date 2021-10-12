using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TestService.Models
{
    public class Conductance
    {
        public string PeriodName { get; set; }
        public decimal IncomingBalance { get; set; }
        public decimal Accured { get; set; }
        public decimal Paid { get; set; }
        public decimal OutgoingBalance { get; set; }
    }
}
