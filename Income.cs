using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinanceTrackerWithAdo
{
    public class Income
    {
        public decimal Amount { get; set; }
        public string Source { get; set; }

        public override string ToString()
        {
            return $"Amount: {Amount:C}, Source: {Source}";

        }
    }
}
