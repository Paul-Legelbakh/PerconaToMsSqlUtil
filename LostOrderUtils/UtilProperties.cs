using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LostOrderUtils
{
    public class UtilProperties
    {
        public string MsServer { get; set; }

        public string MsDbName { get; set; }

        public string MsLogin { get; set; }

        public string MsPassword { get; set; }

        public string MyServer { get; set; }

        public string MyDbName { get; set; }

        public string MyLogin { get; set; }

        public string MyPassword { get; set; }

        public DateTime startDate { get; set; }

        public DateTime dueDate { get; set; }
        public int packageSize { get; set; }

        public Dictionary<string, bool> OrderModel { get; set; }

        public Dictionary<string, bool> OrderProductModel { get; set; }

        public Dictionary<string, bool> TransactionModel { get; set; }

        public bool PushOrder { get; set; }

        public bool PushOrderProduct { get; set; }

        public bool PushTransaction { get; set; }
    }
}
