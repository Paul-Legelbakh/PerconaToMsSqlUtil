using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace LostOrderUtils
{
    public class Order
    {
        public bool IsUpdate { get; set; }

        public int? IsOffLinePurchase { get; set; }

        public int? IsPurchaseWithoutProcessing { get; set; }
        
        public Guid Id { get; set; }

        public DateTime? CreatedOn { get; set; }

        public Guid CreatedById { get; set; }

        public DateTime? ModifiedOn { get; set; }

        public Guid ModifiedById { get; set; }

        public string Number { get; set; }

        public Guid AccountId { get; set; }

        public DateTime? Date { get; set; }

        public Guid OwnerId { get; set; }

        public Guid StatusId { get; set; }

        public Guid PaymentStatusId { get; set; }

        public Guid DeliveryStatusId { get; set; }

        public Guid ContactId { get; set; }

        public DateTime? DueDate { get; set; }

        public DateTime? ActualDate { get; set; }

        public Guid CurrencyId { get; set; }

        public decimal? CurrencyRate { get; set; }

        public decimal? Amount { get; set; }

        public decimal? PaymentAmount { get; set; }

        public decimal? PrimaryAmount { get; set; }

        public decimal? PrimaryPaymentAmount { get; set; }
        
        public Guid SourceOrderId { get; set; }

        public Guid PaymentTypeId { get; set; }

        public string Notes { get; set; }

        public Guid OpportunityId { get; set; }

        public Guid OrderTypeId { get; set; }

        public decimal? BonusAmmount { get; set; }

        public Guid CardAccountId { get; set; }

        public Guid CardId { get; set; }

        public Guid CashierId { get; set; }

        public decimal MoneyBonusAmount { get; set; }

        public Guid CashDeskId { get; set; }

        public Guid ParentPurchaseId { get; set; }

        public decimal ChargeBonusAmount { get; set; }

        public decimal WriteOfFBonusAmount { get; set; }

        public Guid BankCardTypeId { get; set; }

        public string TISGuid { get; set; }
        
        public List<OrderProduct> Products { get; set; }

        public List<Transaction> Transactions { get; set; }

        public JObject json { get; set; }

    }
}
