using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinanceTrackerWithAdo
{
    public class Expense
    {
        public decimal Amount { get; set; }
        public string Category { get; set; }

        public override string ToString()
        {
            return $"Amount: {Amount:C}, Category: {Category}";
        }
    }
}
