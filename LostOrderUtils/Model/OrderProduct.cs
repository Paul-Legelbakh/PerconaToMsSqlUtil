using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace LostOrderUtils
{
    public class OrderProduct
    {
        public Guid Id { get; set; }

        public DateTime? CreatedOn { get; set; }

        public Guid CreatedById { get; set; }

        public DateTime? ModifiedOn { get; set; }

        public Guid ModifiedById { get; set; }

        public string Name { get; set; }

        public string Notes { get; set; }

        public Guid ProductId { get; set; }

        public string CustomProduct { get; set; }

        public DateTime? DeliveryDate { get; set; }

        public decimal? Quantity { get; set; }

        public string UnitId { get; set; }

        public decimal? PrimaryPrice { get; set; }

        public decimal? Price { get; set; }

        public decimal? PrimaryAmount { get; set; }

        public decimal? Amount { get; set; }

        public decimal? PrimaryDiscountAmount { get; set; }

        public decimal? DiscountAmount { get; set; }

        public decimal? DiscountPercent { get; set; }

        public Guid TaxId { get; set; }

        public decimal? PrimaryTaxAmount { get; set; }

        public decimal? TaxAmount { get; set; }

        public decimal? PrimaryTotalAmount { get; set; }

        public decimal? TotalAmount { get; set; }

        public decimal? DiscountTax { get; set; }

        public int? ProcessListeners { get; set; }

        public Guid OrderId { get; set; }

        public decimal? BaseQuantity { get; set; }

        public Guid PriceListId { get; set; }

        public decimal? GiftCardPayedAmount { get; set; }

        public Guid CampaignId { get; set; }

        public decimal CashDeskDiscountAmount { get; set; }

        public JObject json { get; set; }
    }
}
