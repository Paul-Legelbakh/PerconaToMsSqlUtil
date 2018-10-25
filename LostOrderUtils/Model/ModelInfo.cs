using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LostOrderUtils
{
    public class OrderInfo
    {
        public Guid Id { get; set; }

        public int Products { get; set; }

        public int Transactions { get; set; }
    }
    public class OrderAnswerInfo
    {
        public Guid Id { get; set; }

        public string Number { get; set; }

        public decimal TotalAmount { get; set; }

        public Guid CardId { get; set; }

        public int Products { get; set; }

        public bool ProductsDiff { get; set; }

        public int Transactions { get; set; }

        public bool TransactionDiff { get; set; }
    }
}
