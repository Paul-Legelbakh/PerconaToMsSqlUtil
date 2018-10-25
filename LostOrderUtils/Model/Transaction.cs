using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace LostOrderUtils
{
    public class Transaction
    {
        public Guid Id { get; set; }

        public DateTime? CreatedOn { get; set; }

        public DateTime? ModifiedOn { get; set; }

        public string Name { get; set; }

        public Guid TypeId { get; set; }

        public Guid StatusId { get; set; }              

        public DateTime? Date { get; set; }              

        public DateTime? ActivationDate { get; set; }

        public DateTime? CancellationDate { get; set; }

        public Guid BonusTypeId { get; set; }

        public decimal Amount { get; set; }

        public decimal BaseBonusAmount { get; set; }

        public Guid CardId { get; set; }

        public Guid CardAccountId { get; set; }

        public Guid OrderId { get; set; }

        public Guid ProductId { get; set; }

        public Guid CampaignId { get; set; }

        public Guid BonusChargeId { get; set; }

        public JObject json { get; set; }
    }
}
