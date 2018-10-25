//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;
//using System.Data.SqlClient;
//using Newtonsoft.Json;
//using Newtonsoft.Json.Linq;
//using System.Globalization;
//using System.Reflection;
//using MySql.Data.MySqlClient;

//namespace LostOrderUtils
//{
//    public class UtilityHelper
//    {
//        

//        #region Get info
//        public static List<OrderAnswerInfo> GetBpmOrderList(string bpmConnection, string perconaConnection, 
//            DateTime startDate, DateTime dueDate)
//        {
//            List<OrderInfo> perconaOrderInfo;
//            List<OrderAnswerInfo> bpmOrderInfo;
//            List<OrderAnswerInfo> diffBpmGuids = new List<OrderAnswerInfo>();
//            for (; startDate <= dueDate; startDate = startDate.AddDays(1))
//            {
//                perconaOrderInfo = GetPerconaOrderInfo(perconaConnection, startDate, startDate.AddDays(1));
//                bpmOrderInfo = GetBpmOrderAnswerInfo(bpmConnection, startDate, startDate.AddDays(1));
//                foreach (OrderAnswerInfo bpmInfo in bpmOrderInfo)
//                {
//                    OrderInfo percona = perconaOrderInfo.Find(delegate (OrderInfo info)
//                    {
//                        return info.Id == bpmInfo.Id;
//                    });
//                    if (percona == null)
//                    {
//                        diffBpmGuids.Add(new OrderAnswerInfo
//                        {
//                            Id = bpmInfo.Id,
//                            Products = bpmInfo.Products,
//                            Transactions = bpmInfo.Transactions,
//                            ProductsDiff = false,
//                            TransactionDiff = false,
//                            Number = bpmInfo.Number,
//                            CardId = bpmInfo.CardId,
//                            TotalAmount = bpmInfo.TotalAmount
//                        });
//                    }
//                    else
//                    {
//                        if (percona.Products != bpmInfo.Products || percona.Transactions != bpmInfo.Transactions)
//                        {
//                            diffBpmGuids.Add(new OrderAnswerInfo
//                            {
//                                Id = bpmInfo.Id,
//                                Products = bpmInfo.Products,
//                                Transactions = bpmInfo.Transactions,
//                                ProductsDiff = percona.Products == bpmInfo.Products ? false : true,
//                                TransactionDiff = percona.Transactions == bpmInfo.Transactions ? false : true,
//                                Number = bpmInfo.Number,
//                                CardId = bpmInfo.CardId,
//                                TotalAmount = bpmInfo.TotalAmount
//                            });
//                        }
//                    }
//                }
//            }
//            return diffBpmGuids;
//        }

//        public static List<OrderAnswerInfo> GetPerconaPurchaseList(string bpmConnection, string perconaConnection,
//            DateTime startDate, DateTime dueDate)
//        {
//            List<OrderAnswerInfo> perconaOrderInfo;
//            List<OrderInfo> bpmOrderInfo;
//            List<OrderAnswerInfo> diffPerconaGuids = new List<OrderAnswerInfo>();
//            for (; startDate <= dueDate; startDate = startDate.AddDays(1))
//            {
//                perconaOrderInfo = GetPerconaOrderAnswerInfo(perconaConnection, startDate, startDate.AddDays(1));
//                bpmOrderInfo = GetBpmOrderInfo(bpmConnection, startDate, startDate.AddDays(1));
//                foreach(OrderAnswerInfo pecronaInfo in perconaOrderInfo)
//                {                    
//                    OrderInfo bpm = bpmOrderInfo.Find(delegate(OrderInfo info)
//                    {
//                        return info.Id == pecronaInfo.Id;
//                    });
//                    if (bpm == null)
//                    {
//                        diffPerconaGuids.Add(new OrderAnswerInfo
//                        {
//                            Id = pecronaInfo.Id,
//                            Products = pecronaInfo.Products,
//                            Transactions = pecronaInfo.Transactions,
//                            ProductsDiff = false,
//                            TransactionDiff = false,
//                            Number = pecronaInfo.Number,
//                            CardId = pecronaInfo.CardId,
//                            TotalAmount = pecronaInfo.TotalAmount
//                        });
//                    }
//                    else
//                    {
//                        if (bpm.Products != pecronaInfo.Products || bpm.Transactions != pecronaInfo.Transactions)
//                        {
//                            diffPerconaGuids.Add(new OrderAnswerInfo
//                            {
//                                Id = pecronaInfo.Id,
//                                Products = pecronaInfo.Products,
//                                Transactions = pecronaInfo.Transactions,
//                                ProductsDiff = bpm.Products == pecronaInfo.Products ? false : true,
//                                TransactionDiff = bpm.Transactions == pecronaInfo.Transactions ? false : true,
//                                Number = pecronaInfo.Number,
//                                CardId = pecronaInfo.CardId,
//                                TotalAmount = pecronaInfo.TotalAmount
//                            });
//                        }
//                    }
//                }
//            }            
//            return diffPerconaGuids;
//        }

//        public static List<OrderInfo> GetBpmOrderInfo(string connectionString, DateTime startDate, DateTime dueDate)
//        {
//            List<OrderInfo> response = new List<OrderInfo>();
//            string sqlQuery = String.Format(@"SELECT Id,
//    (SELECT COUNT(Id) FROM OrderProduct op WHERE op.OrderId = o.Id) as Products,
//    (SELECT COUNT(Id) FROM [Transaction] op WHERE op.OrderId = o.Id) as Transactions
//FROM [Order] o
//WHERE (Date BETWEEN  '{0}' AND '{1}')
//ORDER BY Date DESC", startDate.ToString("yyyy-MM-dd"), dueDate.ToString("yyyy-MM-dd"));

//            using (var dbConnection = new SqlConnection(connectionString))
//            {
//                try
//                {
//                    dbConnection.Open();
//                    using (SqlCommand command = new SqlCommand(sqlQuery, dbConnection))
//                    {
//                        using (SqlDataReader reader = command.ExecuteReader())
//                        {
//                            while (reader.Read())
//                            {
//                                response.Add(new OrderInfo
//                                {
//                                    Id = reader.GetGuid(0),
//                                    Products = reader.GetInt32(1),
//                                    Transactions = reader.GetInt32(2)
//                                });
//                            }
//                        }
//                    }
//                }
//                catch (Exception ex)
//                {
//                    throw ex;
//                }
//                finally
//                {
//                    dbConnection.Close();
//                }
//            }
//            return response;
//        }
        
//        public static List<OrderInfo> GetPerconaOrderInfo(string connectionString, DateTime startDate, DateTime dueDate)
//        {
//            List<OrderInfo> response = new List<OrderInfo>();
//            string sqlQuery = String.Format(@"SELECT UuidFromBin(Id),
//(SELECT Count(Id) FROM ProductInPurchase pip WHERE pip.PurchaseId = p.Id) as Products,
//(SELECT Count(Id) FROM `Transaction` t WHERE t.PurchaseId = p.Id) as Transactions 
//FROM Purchase p
//WHERE (p.Date BETWEEN '{0}' AND '{1} 23:59:59.999')
//ORDER BY ModifiedOn DESC", startDate.ToString("yyyy-MM-dd"), dueDate.ToString("yyyy-MM-dd"));

//            using (MySqlConnection dbConnection = new MySqlConnection(connectionString))
//            {
//                try
//                {
//                    dbConnection.Open();
//                    using (MySqlCommand command = new MySqlCommand(sqlQuery, dbConnection))
//                    {
//                        using (MySqlDataReader reader = command.ExecuteReader())
//                        {
//                            while (reader.Read())
//                            {
//                                response.Add(new OrderInfo
//                                {
//                                    Id = reader.GetGuid(0),
//                                    Products = reader.GetInt32(1),
//                                    Transactions = reader.GetInt32(2)
//                                });
//                            }
//                        }
//                    }
//                }
//                catch (Exception ex)
//                {
//                    throw ex;
//                }
//                finally
//                {
//                    dbConnection.Close();
//                }
//            }
//            return response;
//        }

//        public static List<OrderAnswerInfo> GetPerconaOrderAnswerInfo(string connectionString, DateTime startDate, DateTime dueDate)
//        {
//            List<OrderAnswerInfo> response = new List<OrderAnswerInfo>();
//            string sqlQuery = String.Format(@"SELECT UuidFromBin(Id),
//(SELECT Count(Id) FROM ProductInPurchase pip WHERE pip.PurchaseId = p.Id) as Products,
//(SELECT Count(Id) FROM `Transaction` t WHERE t.PurchaseId = p.Id) as Transactions,
//Number, UuidFromBin(CardId), TotalAmount
//FROM Purchase p
//WHERE (p.Date BETWEEN '{0}' AND '{1}')
//ORDER BY ModifiedOn DESC", startDate.ToString("yyyy-MM-dd"), dueDate.ToString("yyyy-MM-dd"));

//            using (MySqlConnection dbConnection = new MySqlConnection(connectionString))
//            {
//                try
//                {
//                    dbConnection.Open();
//                    using (MySqlCommand command = new MySqlCommand(sqlQuery, dbConnection))
//                    {
//                        using (MySqlDataReader reader = command.ExecuteReader())
//                        {
//                            while (reader.Read())
//                            {
//                                response.Add(new OrderAnswerInfo
//                                {
//                                    Id = reader.GetGuid(0),
//                                    Products = reader.GetInt32(1),
//                                    Transactions = reader.GetInt32(2),
//                                    Number = reader.GetString(3),
//                                    CardId = reader.GetGuid(4),
//                                    TotalAmount = reader.GetDecimal(5)
//                                });
//                            }
//                        }
//                    }
//                }
//                catch (Exception ex)
//                {
//                    throw ex;
//                }
//                finally
//                {
//                    dbConnection.Close();
//                }
//            }
//            return response;
//        }

//        public static List<OrderAnswerInfo> GetBpmOrderAnswerInfo(string connectionString, DateTime startDate, DateTime dueDate)
//        {
//            List<OrderAnswerInfo> response = new List<OrderAnswerInfo>();
//            string sqlQuery = String.Format(@"SELECT Id,
//    (SELECT COUNT(Id) FROM OrderProduct op WHERE op.OrderId = o.Id) as Products,
//    (SELECT COUNT(Id) FROM [Transaction] op WHERE op.OrderId = o.Id) as Transactions,
//Number, CardId, Amount
//FROM [Order] o
//WHERE (Date BETWEEN  '{0}' AND '{1}  23:59:59.999')
//ORDER BY Date DESC", startDate.ToString("yyyy-MM-dd"), dueDate.ToString("yyyy-MM-dd"));

//            using (SqlConnection dbConnection = new SqlConnection(connectionString))
//            {
//                try
//                {
//                    dbConnection.Open();
//                    using (SqlCommand command = new SqlCommand(sqlQuery, dbConnection))
//                    {
//                        using (SqlDataReader reader = command.ExecuteReader())
//                        {
//                            while (reader.Read())
//                            {
//                                response.Add(new OrderAnswerInfo
//                                {
//                                    Id = reader.GetGuid(0),
//                                    Products = reader.GetInt32(1),
//                                    Transactions = reader.GetInt32(2),
//                                    Number = reader.GetString(3),
//                                    CardId = MyDate.GuidNullValidator(reader["CardId"].ToString()),
//                                    TotalAmount = reader.GetDecimal(5)
//                                });
//                            }
//                        }
//                    }
//                }
//                catch (Exception ex)
//                {
//                    throw ex;
//                }
//                finally
//                {
//                    dbConnection.Close();
//                }
//            }
//            return response;
//        }
//        #endregion

//        #region Get MsSQL Orders
//        #region Get Orders
//        public static List<Order> GetOrderList(string connectionString, List<OrderAnswerInfo> configList, Dictionary<string, bool> config)
//        {
//            List<Order> response = new List<Order>();
//            StringBuilder sqlQuery = new StringBuilder();

//            #region Form SQL
//            sqlQuery.Append(String.Format(@"SELECT
//                Id
//                ,CreatedOn
//                ,ModifiedOn
//                ,Number
//                ,AccountId
//                ,Date
//                ,StatusId
//                ,ContactId
//                ,CurrencyId
//                ,CurrencyRate
//                ,Amount
//                ,PaymentAmount
//                ,PrimaryAmount
//                ,PrimaryPaymentAmount
//                ,OrderTypeId
//                ,BonusAmmount
//                ,CardAccountId
//                ,CardId
//                ,CashierId
//                ,MoneyBonusAmount
//                ,CashDeskId
//                ,IsOffLinePurchase
//                ,IsPurchaseWithoutProcessing"));
//            foreach(KeyValuePair<string, bool> field in config)
//            {
//                if (field.Value)
//                {
//                    sqlQuery.Append(String.Format(@"         ,{0}", field.Key));
//                }
//            }
//            sqlQuery.Append(@"
//FROM [Order] o
//WHERE Id in (");
//            foreach(OrderAnswerInfo info in configList)
//            {
//                sqlQuery.Append(String.Format("'{0}', ", info.Id));
//            }
//            sqlQuery.Append(String.Format("'{0}')", Guid.Empty));
//            #endregion

//            using (SqlConnection dbConnection = new SqlConnection(connectionString))
//            {
//                try
//                {
//                    dbConnection.Open();
//                    using (SqlCommand command = new SqlCommand(sqlQuery.ToString(), dbConnection))
//                    {
//                        using (SqlDataReader reader = command.ExecuteReader())
//                        {
//                            int counter = 0;
//                            while (reader.Read())
//                            {
//                                response.Add(new Order
//                                {
//                                    Id = reader.GetGuid(0),
//                                    CreatedOn = String.IsNullOrEmpty(reader["CreatedOn"].ToString()) ? DateTime.MinValue : Convert.ToDateTime(reader["CreatedOn"]),
//                                    ModifiedOn = String.IsNullOrEmpty(reader["ModifiedOn"].ToString()) ? DateTime.MinValue : Convert.ToDateTime(reader["ModifiedOn"]),
//                                    Number = reader["Number"].ToString(),
//                                    AccountId = MyDate.GuidNullValidator(reader["AccountId"].ToString()),
//                                    Date = String.IsNullOrEmpty(reader["Date"].ToString()) ? DateTime.MinValue : Convert.ToDateTime(reader["Date"]),
//                                    StatusId = MyDate.GuidNullValidator(reader["StatusId"].ToString()),
//                                    ContactId = MyDate.GuidNullValidator(reader["ContactId"].ToString()),
//                                    CurrencyId = MyDate.GuidNullValidator(reader["CurrencyId"].ToString()),
//                                    CurrencyRate = Convert.ToDecimal(reader["CurrencyRate"]),
//                                    Amount = Convert.ToDecimal(reader["Amount"]),
//                                    PaymentAmount = Convert.ToDecimal(reader["PaymentAmount"]),
//                                    PrimaryAmount = Convert.ToDecimal(reader["PrimaryAmount"]),
//                                    PrimaryPaymentAmount = Convert.ToDecimal(reader["PrimaryPaymentAmount"]),
//                                    OrderTypeId = MyDate.GuidNullValidator(reader["OrderTypeId"].ToString()),
//                                    BonusAmmount = Convert.ToDecimal(reader["BonusAmmount"]),
//                                    CardAccountId = MyDate.GuidNullValidator(reader["CardAccountId"].ToString()),
//                                    CardId = MyDate.GuidNullValidator(reader["CardId"].ToString()),
//                                    CashierId = MyDate.GuidNullValidator(reader["CashierId"].ToString()),
//                                    MoneyBonusAmount = Convert.ToDecimal(reader["MoneyBonusAmount"]),
//                                    CashDeskId = MyDate.GuidNullValidator(reader["CashDeskId"].ToString()),
//                                    IsOffLinePurchase = Convert.ToInt32(reader["IsOffLinePurchase"]),
//                                    IsPurchaseWithoutProcessing = Convert.ToInt32(reader["IsPurchaseWithoutProcessing"]),
//                                    TISGuid = config["TISGuid"] ? reader["TISGuid"].ToString() : null,
//                                    ParentPurchaseId = config["ParentPurchaseId"] ? MyDate.GuidNullValidator(reader["ParentPurchaseId"].ToString()) : Guid.Empty,
//                                    ChargeBonusAmount = config["ChargeBonusAmount"] ? Convert.ToDecimal(reader["ChargeBonusAmount"]) : 0,
//                                    WriteOfFBonusAmount = config["WriteOfFBonusAmount"] ? Convert.ToDecimal(reader["WriteOfFBonusAmount"]) : 0,
//                                    BankCardTypeId = config["BankCardTypeId"] ? MyDate.GuidNullValidator(reader["BankCardTypeId"].ToString()) : Guid.Empty,
//                                    IsUpdate = (configList[counter].ProductsDiff || configList[counter].TransactionDiff),
//                                    Products = new List<OrderProduct>(),
//                                    Transactions = new List<Transaction>()
//                                });
//                                counter++;
//                            }
//                        }
//                    }
//                }
//                catch (Exception ex)
//                {
//                    throw ex;
//                }
//                finally
//                {
//                    dbConnection.Close();
//                }
//            }

//            return response;
//        }
//        #endregion        
        
//        #region Get OrderProducts
//        public static void GetOrderProductList(string connectionString, List<OrderAnswerInfo> configList, Dictionary<string, bool> config,
//            List<Order> orderList)
//        {
//            StringBuilder sqlQuery = new StringBuilder();

//            #region Form SQL
//            sqlQuery.Append(String.Format(@"SELECT
//                [Id]                                                            
//                ,[CreatedOn]
//                ,[ModifiedOn]
//                ,[OrderId]
//                ,[ProductId]
//                ,[Quantity]
//                ,[BaseQuantity]
//                ,[Price]
//                ,[Amount]
//                ,[PrimaryPrice]
//                ,[PrimaryAmount]
//                ,[Name]
//                ,[Notes]
//                ,[CustomProduct]
//                ,[PrimaryDiscountAmount]
//                ,[DiscountAmount]
//                ,[DiscountPercent]
//                ,[GiftCardPayedAmount]
//                ,[DiscountTax]
//                ,[TaxAmount]
//                ,[PrimaryTaxAmount]
//                ,[TotalAmount]
//                ,[PrimaryTotalAmount]
//                ,[ProcessListeners]
//                ,[CampaignId]"));
//            foreach (KeyValuePair<string, bool> field in config)
//            {
//                if (field.Value)
//                {
//                    sqlQuery.Append(String.Format(@"                ,{0}", field.Key));
//                }
//            }
//            sqlQuery.Append(@"
//FROM [OrderProduct] o
//WHERE OrderId in (");
//            foreach (OrderAnswerInfo info in configList)
//            {
//                sqlQuery.Append(String.Format("'{0}', ", info.Id));
//            }
//            sqlQuery.Append(String.Format("'{0}')", Guid.Empty));
//            #endregion

//            using (SqlConnection dbConnection = new SqlConnection(connectionString))
//            {
//                try
//                {
//                    dbConnection.Open();
//                    using (SqlCommand command = new SqlCommand(sqlQuery.ToString(), dbConnection))
//                    {
//                        using (SqlDataReader reader = command.ExecuteReader())
//                        {
//                            int counter = 0;
//                            while (reader.Read())
//                            {
//                                orderList[orderList.FindIndex(o => o.Id == new Guid(reader["OrderId"].ToString()))].Products.Add(new OrderProduct
//                                {
//                                    Id = new Guid(reader["Id"].ToString()),
//                                    CreatedOn = String.IsNullOrEmpty(reader["CreatedOn"].ToString()) ? DateTime.MinValue : Convert.ToDateTime(reader["CreatedOn"]),
//                                    ModifiedOn = String.IsNullOrEmpty(reader["ModifiedOn"].ToString()) ? DateTime.MinValue : Convert.ToDateTime(reader["ModifiedOn"]),
//                                    OrderId = MyDate.GuidNullValidator(reader["OrderId"].ToString()),
//                                    ProductId = MyDate.GuidNullValidator(reader["ProductId"].ToString()),
//                                    Quantity = Convert.ToDecimal(reader["Quantity"]),
//                                    BaseQuantity = Convert.ToDecimal(reader["BaseQuantity"]),
//                                    Price = Convert.ToDecimal(reader["Price"]),
//                                    Amount = Convert.ToDecimal(reader["Amount"]),
//                                    PrimaryPrice = Convert.ToDecimal(reader["PrimaryPrice"]),
//                                    PrimaryAmount = Convert.ToDecimal(reader["PrimaryAmount"]),
//                                    Name = reader["Name"].ToString(),
//                                    Notes = reader["Notes"].ToString(),
//                                    CustomProduct = reader["CustomProduct"].ToString(),
//                                    PrimaryDiscountAmount = Convert.ToDecimal(reader["PrimaryDiscountAmount"]),
//                                    DiscountAmount = Convert.ToDecimal(reader["DiscountAmount"]),
//                                    DiscountPercent = Convert.ToDecimal(reader["DiscountPercent"]),
//                                    GiftCardPayedAmount = Convert.ToDecimal(reader["GiftCardPayedAmount"]),
//                                    DiscountTax = Convert.ToDecimal(reader["DiscountTax"]),
//                                    TaxAmount = Convert.ToDecimal(reader["TaxAmount"]),
//                                    PrimaryTaxAmount = Convert.ToDecimal(reader["PrimaryTaxAmount"]),
//                                    TotalAmount = Convert.ToDecimal(reader["TotalAmount"]),
//                                    PrimaryTotalAmount = Convert.ToDecimal(reader["PrimaryTotalAmount"]),
//                                    ProcessListeners = Convert.ToInt32(reader["ProcessListeners"]),
//                                    CampaignId = MyDate.GuidNullValidator(reader["CampaignId"].ToString()),
//                                    CashDeskDiscountAmount = config["CashDeskDiscountAmount"] ? Convert.ToDecimal(reader["CashDeskDiscountAmount"]) : 0
//                                   });
//                                counter++;
//                            }
//                        }
//                    }
//                }
//                catch (Exception ex)
//                {
//                    throw ex;
//                }
//                finally
//                {
//                    dbConnection.Close();
//                }
//            }
//        }
//        #endregion

//        #region Get MsSQL Transactions
//        public static void GetMsTransactionsList(string connectionString, List<OrderAnswerInfo> configList, Dictionary<string, bool> config,
//            List<Order> orderList)
//        {
//            StringBuilder sqlQuery = new StringBuilder();

//            #region Form SQL
//            sqlQuery.Append(String.Format(@"SELECT
//             [Id]
//              ,[CreatedOn]
//              ,[ModifiedOn]
//              ,[TypeId]
//              ,[StatusId]
//              ,[Date]
//              ,[ActivationDate]
//              ,[CancellationDate]
//              ,[BonusTypeId]
//              ,[Amount]
//              ,[BaseBonusAmount]
//              ,[CardId]
//              ,[CardAccountId]
//              ,[OrderId]
//              ,[ProductId]
//              ,[CampaignId]"));
//            foreach (KeyValuePair<string, bool> field in config)
//            {
//                if (field.Value)
//                {
//                    sqlQuery.Append(String.Format(@"              ,{0}", field.Key));
//                }
//            }
//            sqlQuery.Append(@"
//FROM [Transaction] o
//WHERE OrderId in (");
//            foreach (OrderAnswerInfo info in configList)
//            {
//                sqlQuery.Append(String.Format("'{0}', ", info.Id));
//            }
//            sqlQuery.Append(String.Format("'{0}')", Guid.Empty));
//            #endregion

//            using (SqlConnection dbConnection = new SqlConnection(connectionString))
//            {
//                try
//                {
//                    dbConnection.Open();
//                    using (SqlCommand command = new SqlCommand(sqlQuery.ToString(), dbConnection))
//                    {
//                        using (SqlDataReader reader = command.ExecuteReader())
//                        {
//                            int counter = 0;
//                            while (reader.Read())
//                            {
//                                orderList[orderList.FindIndex(o => o.Id == new Guid(reader["OrderId"].ToString()))].Transactions.Add(new Transaction
//                                {
//                                    Id = new Guid(reader["Id"].ToString()),
//                                    CreatedOn = String.IsNullOrEmpty(reader["CreatedOn"].ToString()) ? DateTime.MinValue : Convert.ToDateTime(reader["CreatedOn"]),
//                                    ModifiedOn = String.IsNullOrEmpty(reader["ModifiedOn"].ToString()) ? DateTime.MinValue : Convert.ToDateTime(reader["ModifiedOn"]),
//                                    OrderId = MyDate.GuidNullValidator(reader["OrderId"].ToString()),
//                                    TypeId = MyDate.GuidNullValidator(reader["TypeId"].ToString()),
//                                    StatusId = MyDate.GuidNullValidator(reader["StatusId"].ToString()),
//                                    Date = String.IsNullOrEmpty(reader["Date"].ToString()) ? DateTime.MinValue : Convert.ToDateTime(reader["Date"]),
//                                    ActivationDate = String.IsNullOrEmpty(reader["ActivationDate"].ToString()) ? DateTime.MinValue : Convert.ToDateTime(reader["ActivationDate"]),
//                                    CancellationDate = String.IsNullOrEmpty(reader["CancellationDate"].ToString()) ? DateTime.MinValue : Convert.ToDateTime(reader["CancellationDate"]),
//                                    BonusTypeId = MyDate.GuidNullValidator(reader["BonusTypeId"].ToString()),
//                                    Amount = Convert.ToDecimal(reader["Amount"]),
//                                    BaseBonusAmount = Convert.ToDecimal(reader["Amount"]),
//                                    CardId = MyDate.GuidNullValidator(reader["CardId"].ToString()),
//                                    CardAccountId = MyDate.GuidNullValidator(reader["CardAccountId"].ToString()),
//                                    ProductId = MyDate.GuidNullValidator(reader["ProductId"].ToString()),
//                                    CampaignId = MyDate.GuidNullValidator(reader["CampaignId"].ToString()),
//                                    Name = config["Name"] ? reader["Name"].ToString() : "",
//                                    BonusChargeId = config["BonusChargeId"] ? MyDate.GuidNullValidator(reader["BonusChargeId"].ToString()) : Guid.Empty
//                                });
//                                counter++;
//                            }
//                        }
//                    }
//                }
//                catch (Exception ex)
//                {
//                    throw ex;
//                }
//                finally
//                {
//                    dbConnection.Close();
//                }
//            }
//        }
//        #endregion
//        #endregion
        
//        #region Get MySQL Purchase
//        #region Get Purchase
//        public static List<Order> GetPurchaseList(string connectionString, List<OrderAnswerInfo> configList, Dictionary<string, bool> config)
//        {
//            List<Order> response = new List<Order>();
//            StringBuilder sqlQuery = new StringBuilder();

//            #region Form SQL
//            sqlQuery.Append(String.Format(@"SELECT
//                 UuidFromBin(Id) as Id,
//                 CreatedOn,
//                 ModifiedOn,
//                 Number,
//                 Date,
//                 UuidFromBin(TypeId) as OrderTypeId,
//                 UuidFromBin(PointOfSaleId) as AccountId, 
//                 UuidFromBin(StatusId) as StatusId,
 
                 
//                 UuidFromBin(CardId) as CardId,
//                 UuidFromBin(OwnerId) as OwnerId,
//                 UuidFromBin(CurrencyId) as CurrencyId,
//                 UuidFromBin(CardAccountId) as CardAccountId,
//                 UuidFromBin(CashierId) as CashierId,
//                 UuidFromBin(CashDeskId) as CashDeskId,
//                 UuidFromBin(PaymentTypeId) as PaymentTypeId,
//                 UuidFromBin(BankCardTypeId) as BankCardTypeId,
 
 
//                 Rate as CurrencyRate,
//                 IsOffLinePurchase,
//                 IsPurchaseWithoutProcessing,
//                 UuidFromBin(TISGuid) as TISGuid,
//                 UuidFromBin(ParentPurchaseId) as ParentPurchaseId,
 
//                 TotalAmount/100 as Amount,
//                 BaseTotalAmount/100 as PrimaryAmount,
//                 CashPaidAmount/100 as PaymentAmount,
//                 BaseCashPaidAmount/100 as PrimaryPaymentAmount,
//                 BonusesPaidAmount/100 as BonusAmmount"));
//            sqlQuery.Append(@"
//FROM Purchase o
//WHERE Id in (");
//            foreach (OrderAnswerInfo info in configList)
//            {
//                sqlQuery.Append(String.Format("UuidToBin('{0}'), ", info.Id));
//            }
//            sqlQuery.Append(String.Format("UuidToBin('{0}'))", Guid.Empty));
//            #endregion

//            using (MySqlConnection dbConnection = new MySqlConnection(connectionString))
//            {
//                try
//                {
//                    dbConnection.Open();
//                    using (MySqlCommand command = new MySqlCommand(sqlQuery.ToString(), dbConnection))
//                    {
//                        using (MySqlDataReader reader = command.ExecuteReader())
//                        {
//                            int counter = 0;
//                            while (reader.Read())
//                            {
//                                response.Add(new Order
//                                {
//                                    Id = reader.GetGuid(0),
//                                    CreatedOn = String.IsNullOrEmpty(reader["CreatedOn"].ToString()) ? DateTime.MinValue : Convert.ToDateTime(reader["CreatedOn"]),
//                                    ModifiedOn = String.IsNullOrEmpty(reader["ModifiedOn"].ToString()) ? DateTime.MinValue : Convert.ToDateTime(reader["ModifiedOn"]),
//                                    Number = reader["Number"].ToString(),
//                                    Date = String.IsNullOrEmpty(reader["Date"].ToString()) ? DateTime.MinValue : Convert.ToDateTime(reader["Date"]),
//                                    OrderTypeId = MyDate.GuidNullValidator(reader["OrderTypeId"].ToString()),
//                                    AccountId = MyDate.GuidNullValidator(reader["AccountId"].ToString()),
//                                    StatusId = MyDate.GuidNullValidator(reader["StatusId"].ToString()),
//                                    CardId = MyDate.GuidNullValidator(reader["CardId"].ToString()),
//                                    OwnerId = MyDate.GuidNullValidator(reader["OwnerId"].ToString()),
//                                    CurrencyId = MyDate.GuidNullValidator(reader["CurrencyId"].ToString()),
//                                    CardAccountId = MyDate.GuidNullValidator(reader["CardAccountId"].ToString()),
//                                    CashierId = MyDate.GuidNullValidator(reader["CashierId"].ToString()),
//                                    CashDeskId = MyDate.GuidNullValidator(reader["CashDeskId"].ToString()),
//                                    PaymentTypeId = MyDate.GuidNullValidator(reader["PaymentTypeId"].ToString()),
//                                    BankCardTypeId = MyDate.GuidNullValidator(reader["BankCardTypeId"].ToString()),
//                                    CurrencyRate = Convert.ToDecimal(reader["CurrencyRate"]),
//                                    IsOffLinePurchase = Convert.ToInt32(reader["IsOffLinePurchase"]),
//                                    IsPurchaseWithoutProcessing = Convert.ToInt32(reader["IsPurchaseWithoutProcessing"]),
//                                    TISGuid = reader["TISGuid"].ToString(),
//                                    Amount = Convert.ToDecimal(reader["Amount"]),
//                                    PaymentAmount = Convert.ToDecimal(reader["PaymentAmount"]),
//                                    PrimaryAmount = Convert.ToDecimal(reader["PrimaryAmount"]),
//                                    PrimaryPaymentAmount = Convert.ToDecimal(reader["PrimaryPaymentAmount"]),
//                                    BonusAmmount = Convert.ToDecimal(reader["BonusAmmount"]),
//                                    //MoneyBonusAmount = Convert.ToDecimal(reader["MoneyBonusAmount"]),
//                                    //TISGuid = config["TISGuid"] ? reader["TISGuid"].ToString() : null,
//                                    //ParentPurchaseId = config["ParentPurchaseId"] ? MyDate.GuidNullValidator(reader["ParentPurchaseId"].ToString()) : Guid.Empty,
//                                    ParentPurchaseId = MyDate.GuidNullValidator(reader["ParentPurchaseId"].ToString()),
//                                    //ChargeBonusAmount = config["ChargeBonusAmount"] ? Convert.ToDecimal(reader["ChargeBonusAmount"]) : 0,
//                                    //WriteOfFBonusAmount = config["WriteOfFBonusAmount"] ? Convert.ToDecimal(reader["WriteOfFBonusAmount"]) : 0,
//                                    //BankCardTypeId = config["BankCardTypeId"] ? MyDate.GuidNullValidator(reader["BankCardTypeId"].ToString()) : Guid.Empty,
//                                    IsUpdate = (configList[counter].ProductsDiff || configList[counter].TransactionDiff),
//                                    Products = new List<OrderProduct>(),
//                                    Transactions = new List<Transaction>()
//                                });
//                                counter++;
//                            }
//                        }
//                    }
//                }
//                catch (Exception ex)
//                {
//                    throw ex;
//                }
//                finally
//                {
//                    dbConnection.Close();
//                }
//            }

//            return response;
//        }
//        #endregion

//        #region Get Product in Purchase
//        public static void GetProductInPurchaseList(string connectionString, List<OrderAnswerInfo> configList, Dictionary<string, bool> config,
//            List<Order> orderList)
//        {
//            StringBuilder sqlQuery = new StringBuilder();
            
//            #region Form SQL
//            sqlQuery.Append(String.Format(@"SELECT
//				UuidFromBin(Id) as Id,
//				CreatedOn,
//				ModifiedOn,
//				UuidFromBin(PurchaseId) as OrderId,
//				UuidFromBin(ProductId) as ProductId,
//				Quantity,
//				Price/100 as Price,
//				Amount/100 as Amount,
//				BaseAmount/100 as PrimaryAmount,
//				CustomProduct,
//				DiscountAmount/100 as PrimaryDiscountAmount,
//				DiscountAmount/100 as DiscountAmount,
//                CashDeskDiscountAmount/100 as CashDeskDiscountAmount,
//                DiscountPercent,
//				GiftCardPaidAmount/100 as GiftCardPayedAmount,
//				(Amount - GiftCardPaidAmount - DiscountAmount)/100 as TotalAmount,
//				(Amount - GiftCardPaidAmount - DiscountAmount)/100 as PrimaryTotalAmount,
//				UuidFromBin(CampaignId) as CampaignId"));
//            sqlQuery.Append(@"
//FROM ProductInPurchase o
//WHERE PurchaseId in (");
//            foreach (OrderAnswerInfo info in configList)
//            {
//                sqlQuery.Append(String.Format("UuidToBin('{0}'), ", info.Id));
//            }
//            sqlQuery.Append(String.Format("UuidToBin('{0}'))", Guid.Empty));
//            #endregion

//            using (MySqlConnection dbConnection = new MySqlConnection(connectionString))
//            {
//                try
//                {
//                    dbConnection.Open();
//                    using (MySqlCommand command = new MySqlCommand(sqlQuery.ToString(), dbConnection))
//                    {
//                        using (MySqlDataReader reader = command.ExecuteReader())
//                        {
//                            int counter = 0;
//                            while (reader.Read())
//                            {
//                                orderList[orderList.FindIndex(o => o.Id == new Guid(reader["OrderId"].ToString()))].Products.Add(new OrderProduct
//                                {
//                                    Id = new Guid(reader["Id"].ToString()),
//                                    CreatedOn = String.IsNullOrEmpty(reader["CreatedOn"].ToString()) ? DateTime.MinValue : Convert.ToDateTime(reader["CreatedOn"]),
//                                    ModifiedOn = String.IsNullOrEmpty(reader["ModifiedOn"].ToString()) ? DateTime.MinValue : Convert.ToDateTime(reader["ModifiedOn"]),
//                                    OrderId = MyDate.GuidNullValidator(reader["OrderId"].ToString()),
//                                    ProductId = MyDate.GuidNullValidator(reader["ProductId"].ToString()),
//                                    Quantity = Convert.ToDecimal(reader["Quantity"]),
//                                    BaseQuantity = Convert.ToDecimal(reader["Quantity"]),
//                                    Price = Convert.ToDecimal(reader["Price"]),
//                                    Amount = Convert.ToDecimal(reader["Amount"]),
//                                    PrimaryPrice = Convert.ToDecimal(reader["Price"]),
//                                    PrimaryAmount = Convert.ToDecimal(reader["PrimaryAmount"]),
//                                    CustomProduct = reader["CustomProduct"].ToString(),
//                                    PrimaryDiscountAmount = Convert.ToDecimal(reader["PrimaryDiscountAmount"]),
//                                    DiscountAmount = Convert.ToDecimal(reader["DiscountAmount"]),
//                                    CashDeskDiscountAmount = Convert.ToDecimal(reader["CashDeskDiscountAmount"]),
//                                    DiscountPercent = Convert.ToDecimal(reader["DiscountPercent"]),
//                                    GiftCardPayedAmount = Convert.ToDecimal(reader["GiftCardPayedAmount"]),
//                                    TotalAmount = Convert.ToDecimal(reader["TotalAmount"]),
//                                    PrimaryTotalAmount = Convert.ToDecimal(reader["PrimaryTotalAmount"]),
//                                    CampaignId = MyDate.GuidNullValidator(reader["CampaignId"].ToString())
//                                });
//                                counter++;
//                            }
//                        }
//                    }
//                }
//                catch (Exception ex)
//                {
//                    throw ex;
//                }
//                finally
//                {
//                    dbConnection.Close();
//                }
//            }
//        }
//        #endregion

//        #region Get Percona Transactions
//        public static void GetMyTransactionsList(string connectionString, List<OrderAnswerInfo> configList, Dictionary<string, bool> config,
//            List<Order> orderList)
//        {
//            StringBuilder sqlQuery = new StringBuilder();

//            #region Form SQL
//            sqlQuery.Append(String.Format(@"SELECT
//             UuidFromBin(Id) as Id
//              ,CreatedOn
//              ,ModifiedOn
//              ,UuidFromBin(TypeId) as TypeId
//              ,UuidFromBin(StatusId) as StatusId
//              ,Date
//              ,ActivationDate
//              ,CancellationDate
//              ,UuidFromBin(BonusTypeId) as BonusTypeId
//              ,Amount
//              ,BaseBonusAmount
//              ,UuidFromBin(CardId) as CardId
//              ,UuidFromBin(CardAccountId) as CardAccountId
//              ,UuidFromBin(PurchaseId) as OrderId
//              ,UuidFromBin(ProductId) as ProductId
//              ,UuidFromBin(CampaignId) as CampaignId"));
//            sqlQuery.Append(@"
//FROM `Transaction` o
//WHERE PurchaseId in (");
//            foreach (OrderAnswerInfo info in configList)
//            {
//                sqlQuery.Append(String.Format("UuidToBin('{0}'), ", info.Id));
//            }
//            sqlQuery.Append(String.Format("UuidToBin('{0}'))", Guid.Empty));
//            #endregion

//            using (MySqlConnection dbConnection = new MySqlConnection(connectionString))
//            {
//                try
//                {
//                    dbConnection.Open();
//                    using (MySqlCommand command = new MySqlCommand(sqlQuery.ToString(), dbConnection))
//                    {
//                        using (MySqlDataReader reader = command.ExecuteReader())
//                        {
//                            int counter = 0;
//                            while (reader.Read())
//                            {
//                                orderList[orderList.FindIndex(o => o.Id == new Guid(reader["OrderId"].ToString()))].Transactions.Add(new Transaction
//                                {
//                                    Id = new Guid(reader["Id"].ToString()),
//                                    CreatedOn = String.IsNullOrEmpty(reader["CreatedOn"].ToString()) ? DateTime.MinValue : Convert.ToDateTime(reader["CreatedOn"]),
//                                    ModifiedOn = String.IsNullOrEmpty(reader["ModifiedOn"].ToString()) ? DateTime.MinValue : Convert.ToDateTime(reader["ModifiedOn"]),
//                                    OrderId = MyDate.GuidNullValidator(reader["OrderId"].ToString()),
//                                    TypeId = MyDate.GuidNullValidator(reader["TypeId"].ToString()),
//                                    StatusId = MyDate.GuidNullValidator(reader["StatusId"].ToString()),
//                                    Date = String.IsNullOrEmpty(reader["Date"].ToString()) ? DateTime.MinValue : Convert.ToDateTime(reader["Date"]),
//                                    ActivationDate = String.IsNullOrEmpty(reader["ActivationDate"].ToString()) ? DateTime.MinValue : Convert.ToDateTime(reader["ActivationDate"]),
//                                    CancellationDate = String.IsNullOrEmpty(reader["CancellationDate"].ToString()) ? DateTime.MinValue : Convert.ToDateTime(reader["CancellationDate"]),
//                                    BonusTypeId = MyDate.GuidNullValidator(reader["BonusTypeId"].ToString()),
//                                    Amount = Convert.ToDecimal(reader["Amount"]),
//                                    BaseBonusAmount = Convert.ToDecimal(reader["Amount"]),
//                                    CardId = MyDate.GuidNullValidator(reader["CardId"].ToString()),
//                                    CardAccountId = MyDate.GuidNullValidator(reader["CardAccountId"].ToString()),
//                                    ProductId = MyDate.GuidNullValidator(reader["ProductId"].ToString()),
//                                    CampaignId = MyDate.GuidNullValidator(reader["CampaignId"].ToString()),
//                                    //Name = config["Name"] ? reader["Name"].ToString() : "",
//                                    //BonusChargeId = config["BonusChargeId"] ? MyDate.GuidNullValidator(reader["BonusChargeId"].ToString()) : Guid.Empty
//                                });
//                                counter++;
//                            }
//                        }
//                    }
//                }
//                catch (Exception ex)
//                {
//                    throw ex;
//                }
//                finally
//                {
//                    dbConnection.Close();
//                }
//            }
//        }
//        #endregion
//        #endregion

//        #region Insert Data
//        public static void PushOrderToPercona(string connectionString, List<Order> bpmOrder, Dictionary<string, bool> orderModel,
//            Dictionary<string, bool> orderProductModel, Dictionary<string, bool> transactionModel)
//        {

//        }

//        public static void PushOrderToMsSQL(string connectionString, List<Order> perconaPurchase, Dictionary<string, bool> orderModel,
//            Dictionary<string, bool> orderProductModel, Dictionary<string, bool> transactionModel)
//        {
//            StringBuilder insertSql = new StringBuilder();
//            int counter = 0;
//            int count = perconaPurchase.Count; //2001
//            int diff = count - counter >= 100 ? 100 : count - counter; //100

//            while (counter < perconaPurchase.Count)
//            {
//                insertSql.Clear();
//                for(int i = counter; i < counter + diff; i++)
//                {
//                    Order order = perconaPurchase[i];
                    
//                    #region Form Order SQL
//                    insertSql.Append(@"INSERT INTO [Order](
//                                    Id,
//                                    CreatedOn,
//                                    ModifiedOn,
//                                    Number,
//                                    Date,
//                                    OrderTypeId,
//                                    AccountId, 
//                                    StatusId,
                                    
//                                    ContactId,
//                                    CardId,
//                                    OwnerId,
//                                    CurrencyId,
//                                    CardAccountId,
//                                    CashierId,
//                                    CashDeskId,
//                                    PaymentTypeId,
//                                    BankCardTypeId,


//                                    CurrencyRate,
//                                    IsOffLinePurchase,
//                                    IsPurchaseWithoutProcessing,
//                                    TISGuid,

//                                    Amount,
//                                    PrimaryAmount,
//                                    PaymentAmount,
//                                    PrimaryPaymentAmount,
//                                    BonusAmmount");
//                    insertSql.Append(@"                                ) VALUES (" +
//                                  @"'" + order.Id + @"'" +
//                                  @"," + order.CreatedOn.ToStringOrNull("yyyy-MM-dd HH:mm:ss") +
//                                  @"," + order.ModifiedOn.ToStringOrNull("yyyy-MM-dd HH:mm:ss") +
//                                  @",'" + order.Number + @"'" +
//                                  @"," + order.Date.ToStringOrNull("yyyy-MM-dd HH:mm:ss") +
//                                  @"," + order.OrderTypeId.ToStringOrNull() + 
//                                  @"," + order.AccountId.ToStringOrNull() +
//                                  @"," + "'40DE86EE-274D-4098-9B92-9EBDCF83D4FC'" +
//                                  @"," + order.ContactId.ToStringOrNull() + 
//                                  @"," + order.CardId.ToStringOrNull() + 
//                                  @"," + order.OwnerId.ToStringOrNull() +
//                                  @"," + order.CurrencyId.ToStringOrNull() + 
//                                  @"," + order.CardAccountId.ToStringOrNull() +
//                                  @"," + order.CashierId.ToStringOrNull() + 
//                                  @"," + order.CashDeskId.ToStringOrNull() + 
//                                  @"," + order.PaymentTypeId.ToStringOrNull() +
//                                  @"," + order.BankCardTypeId.ToStringOrNull() +
//                                  @"," + order.CurrencyRate.ToString("F", CultureInfo.InvariantCulture) +
//                                  @",'" + order.IsOffLinePurchase + @"'" +
//                                  @",'" + order.IsPurchaseWithoutProcessing + @"'" +
//                                  @",'" + order.TISGuid + @"'" +
//                                  //@"," + order.ParentPurchaseId.ToStringOrNull() + @"'" +
//                                  @"," + order.Amount.ToString("F", CultureInfo.InvariantCulture) +
//                                  @"," + order.PrimaryAmount.ToString("F", CultureInfo.InvariantCulture) +
//                                  @"," + order.PaymentAmount.ToString("F", CultureInfo.InvariantCulture) +
//                                  @"," + order.PrimaryPaymentAmount.ToString("F", CultureInfo.InvariantCulture) +
//                                  @"," + order.BonusAmmount.ToString("F", CultureInfo.InvariantCulture));
//                    insertSql.Append(")\n");
//                    #endregion

//                    #region OrderProduct sql format
//                    foreach (OrderProduct orderProduct in order.Products)
//                    {

//                        insertSql.Append(@"INSERT INTO [OrderProduct](
//    [Id]                                                            
//    ,[CreatedOn]
//    ,[ModifiedOn]
//    ,[OrderId]
//    ,[ProductId]
//    ,[Quantity]
//    ,[BaseQuantity]
//    ,[Price]
//    ,[Amount]
//    ,[PrimaryPrice]
//    ,[PrimaryAmount]
//    ,[CustomProduct]
//    ,[PrimaryDiscountAmount]
//    ,[DiscountAmount]
//    ,[DiscountPercent]
//    ,[GiftCardPayedAmount]
//    ,[PrimaryTotalAmount]
//    ,[TotalAmount]
//    ,[CashDeskDiscountAmount]
//    ,[CampaignId]");
//                        insertSql.Append(@") values (" +
//        @"'" + orderProduct.Id + @"'" +
//        @"," + orderProduct.CreatedOn.ToStringOrNull("yyyy-MM-dd HH:mm:ss") +
//        @"," + orderProduct.ModifiedOn.ToStringOrNull("yyyy-MM-dd HH:mm:ss") +
//        @"," + orderProduct.OrderId.ToStringOrNull() + 
//        @"," + orderProduct.ProductId.ToStringOrNull() + 
//        @"," + orderProduct.Quantity.ToString("F", CultureInfo.InvariantCulture) +
//        @"," + orderProduct.BaseQuantity.ToString("F", CultureInfo.InvariantCulture) +
//        @"," + orderProduct.Price.ToString("F", CultureInfo.InvariantCulture) +
//        @"," + orderProduct.Amount.ToString("F", CultureInfo.InvariantCulture) +
//        @"," + orderProduct.PrimaryPrice.ToString("F", CultureInfo.InvariantCulture) +
//        @"," + orderProduct.PrimaryAmount.ToString("F", CultureInfo.InvariantCulture) +
//        @",'" + orderProduct.CustomProduct + @"'" +
//        @"," + orderProduct.PrimaryDiscountAmount.ToString("F", CultureInfo.InvariantCulture) +
//        @"," + orderProduct.DiscountAmount.ToString("F", CultureInfo.InvariantCulture) +
//        @"," + orderProduct.DiscountPercent.ToString("F", CultureInfo.InvariantCulture) +
//        @"," + orderProduct.GiftCardPayedAmount.ToString("F", CultureInfo.InvariantCulture) +
//        @"," + orderProduct.TotalAmount.ToString("F", CultureInfo.InvariantCulture) +
//        @"," + orderProduct.PrimaryTotalAmount.ToString("F", CultureInfo.InvariantCulture) +
//        @"," + orderProduct.CashDeskDiscountAmount.ToString("F", CultureInfo.InvariantCulture) +
//        @"," + orderProduct.CampaignId.ToStringOrNull());
//                        insertSql.Append(")\n");
//                    }
//                    #endregion

//                    #region Transaction
//                    foreach (Transaction transaction in order.Transactions)
//                    {
//                        insertSql.Append(@"INSERT INTO [Transaction](
//    [Id]
//    ,[CreatedOn]
//    ,[ModifiedOn]
//    ,[TypeId]
//    ,[StatusId]
//    ,[Date]
//    ,[ActivationDate]
//    ,[CancellationDate]
//    ,[BonusTypeId]
//    ,[Amount]
//    ,[BaseBonusAmount]
//    ,[CardId]
//    ,[CardAccountId]
//    ,[OrderId]
//    ,[ProductId]
//    ,[CampaignId]
//");
//                        insertSql.Append(@") values (" +
//        @"'" + transaction.Id + @"'" +
//        @"," + transaction.CreatedOn.ToStringOrNull("yyyy-MM-dd HH:mm:ss") +
//        @"," + transaction.ModifiedOn.ToStringOrNull("yyyy-MM-dd HH:mm:ss") +
//        @"," + transaction.TypeId.ToStringOrNull() +
//        @"," + transaction.StatusId.ToStringOrNull() +
//        @"," + transaction.Date.ToStringOrNull("yyyy-MM-dd HH:mm:ss") +
//        @"," + transaction.ActivationDate.ToStringOrNull("yyyy-MM-dd HH:mm:ss") +
//        @"," + transaction.CancellationDate.ToStringOrNull("yyyy-MM-dd HH:mm:ss") + 
//        @"," + transaction.BonusTypeId.ToStringOrNull() +
//        @"," + transaction.Amount.ToString("F", CultureInfo.InvariantCulture) +
//        @"," + transaction.BaseBonusAmount.ToString("F", CultureInfo.InvariantCulture) +
//        @"," + transaction.CardId.ToStringOrNull() +
//        @"," + transaction.CardAccountId.ToStringOrNull() + 
//        @"," + transaction.OrderId.ToStringOrNull() +
//        @"," + transaction.ProductId.ToStringOrNull() +
//        @"," + transaction.CampaignId.ToStringOrNull());
//                        insertSql.Append(")\n");
//                    }
//                    #endregion
//                }
//                using (SqlConnection connection = new SqlConnection(connectionString))
//                {
//                    connection.Open();
//                    SqlCommand command = new SqlCommand();
//                    command.Connection = connection;
//                    command.CommandText = insertSql.ToString();
//                    command.CommandType = System.Data.CommandType.Text;
//                    try
//                    {
//                        command.ExecuteNonQuery();
//                    }
//                    catch (Exception ex)
//                    {
//                        throw ex;
//                    }
//                    finally
//                    {
//                        connection.Close();
//                    }
//                }

//                diff = count - counter >= 100 ? 100 : count - counter;
//                counter += diff;
//            }
//        }
//        #endregion

//        #region Insert order
//        public static void InsertOrder(string connectionString, Order order, Dictionary<string, bool> config)
//        {
//            JToken insertValue = "";
//            string insertSql = @"INSERT INTO [Order](
//                                    Id
//                                    ,CreatedOn
//                                    ,ModifiedOn
//                                    ,Number
//                                    ,AccountId
//                                    ,Date
//                                    ,StatusId
//                                    ,ContactId
//                                    ,CurrencyId
//                                    ,CurrencyRate
//                                    ,Amount
//                                    ,PaymentAmount
//                                    ,PrimaryAmount
//                                    ,PrimaryPaymentAmount
//                                    ,OrderTypeId
//                                    ,BonusAmmount
//                                    ,CardAccountId
//                                    ,CardId
//                                    ,CashierId
//                                    ,MoneyBonusAmount
//                                    ,CashDeskId
//                                    ,IsOffLinePurchase
//                                    ,IsPurchaseWithoutProcessing";

//            foreach(KeyValuePair<string, bool> field in config)
//            {
//                if (field.Value)
//                {
//                    insertValue = order.json.GetValue(field.Key);
//                    if (insertValue != null && String.IsNullOrEmpty(insertValue.ToString()))
//                    {
//                        insertSql += String.Format(",{0}", field.Key);
//                    }
//                }
//            }

//            insertSql += @"                                ) VALUES (" +
//                                     @"'" + order.Id + @"'" +
//                                     @"," + order.CreatedOn.ToStringOrNull("yyyy-MM-dd HH:mm:ss") +
//                                     @"," + order.ModifiedOn.ToStringOrNull("yyyy-MM-dd HH:mm:ss") +
//                                     @",'" + order.Number + @"'" +
//                                     @"," + order.AccountId.ToStringOrNull() +
//                                     @"," + order.Date.ToStringOrNull("yyyy-MM-dd HH:mm:ss") +
//                                     @"," + order.StatusId.ToStringOrNull() +
//                                     @"," + order.ContactId.ToStringOrNull() +
//                                     @"," + order.CurrencyId.ToStringOrNull() +
//                                     @",'" + order.CurrencyRate + @"'" +
//                                     @"," + order.Amount.ToString("F", CultureInfo.InvariantCulture) +
//                                     @"," + order.PaymentAmount.ToString("F", CultureInfo.InvariantCulture) +
//                                     @"," + order.PrimaryAmount.ToString("F", CultureInfo.InvariantCulture) +
//                                     @"," + order.PrimaryPaymentAmount.ToString("F", CultureInfo.InvariantCulture) +
//                                     @"," + order.OrderTypeId.ToStringOrNull() +
//                                     @"," + order.BonusAmmount.ToString("F", CultureInfo.InvariantCulture) +
//                                     @"," + order.CardAccountId.ToStringOrNull() +
//                                     @"," + order.CardId.ToStringOrNull() +
//                                     @"," + order.CashierId.ToStringOrNull() +
//                                     @"," + order.MoneyBonusAmount.ToString("F", CultureInfo.InvariantCulture) +
//                                     @"," + order.CashDeskId.ToStringOrNull() +
//                                     @",'" + order.IsOffLinePurchase + @"'" +
//                                     @",'" + order.IsPurchaseWithoutProcessing + @"'";

//            foreach (KeyValuePair<string, bool> field in config)
//            {
//                if (field.Value && insertValue != null && String.IsNullOrEmpty(insertValue.ToString()))
//                {
//                    insertSql += String.Format(", '{0}'", insertValue.ToString());
//                }
//            }
//            insertSql += ")";

//            using (SqlConnection connection = new SqlConnection(connectionString))
//            {
//                connection.Open();
//                SqlCommand command = new SqlCommand();
//                command.Connection = connection;
//                command.CommandText = insertSql;
//                command.CommandType = System.Data.CommandType.Text;
//                try
//                {
//                    command.ExecuteNonQuery();
//                }
//                catch (Exception ex)
//                {
//                    throw ex;
//                }
//                finally
//                {
//                    connection.Close();
//                }
//            }
//        }
//        #endregion

//        #region Insert order product
//        public static void InsertOrderProduct(string connectionString, OrderProduct orderProduct, Dictionary<string, bool> config)
//        {
//            JToken insertValue = "";
//            string insertSql = @"INSERT INTO [OrderProduct](
//    [Id]                                                            
//    ,[CreatedOn]
//    ,[ModifiedOn]
//    ,[OrderId]
//    ,[ProductId]
//    ,[Quantity]
//    ,[BaseQuantity]
//    ,[Price]
//    ,[Amount]
//    ,[PrimaryPrice]
//    ,[PrimaryAmount]
//    ,[Name]
//    ,[Notes]
//    ,[CustomProduct]
//    ,[PrimaryDiscountAmount]
//    ,[DiscountAmount]
//    ,[DiscountPercent]
//    ,[GiftCardPayedAmount]
//    ,[DiscountTax]
//    ,[TaxAmount]
//    ,[PrimaryTaxAmount]
//    ,[TotalAmount]
//    ,[PrimaryTotalAmount]
//    ,[ProcessListeners]
//    ,[CampaignId]";


//            foreach (KeyValuePair<string, bool> field in config)
//            {
//                if (field.Value)
//                {
//                    insertValue = orderProduct.json.GetValue(field.Key);
//                    if (insertValue != null && String.IsNullOrEmpty(insertValue.ToString()))
//                    {
//                        insertSql += String.Format(",{0}", field.Key);
//                    }
//                }
//            }
//            insertSql += @") values (" +
//    @"'" + orderProduct.Id + @"'" +
//    @"," + orderProduct.CreatedOn.ToStringOrNull("yyyy-MM-dd HH:mm:ss") +
//    @"," + orderProduct.ModifiedOn.ToStringOrNull("yyyy-MM-dd HH:mm:ss") +
//    @"," + orderProduct.OrderId.ToStringOrNull() +
//    @"," + orderProduct.ProductId.ToStringOrNull() +
//    @"," + orderProduct.Quantity.ToString("F", CultureInfo.InvariantCulture) +
//    @"," + orderProduct.BaseQuantity.ToString("F", CultureInfo.InvariantCulture) +
//    @"," + orderProduct.Price.ToString("F", CultureInfo.InvariantCulture) +
//    @"," + orderProduct.Amount.ToString("F", CultureInfo.InvariantCulture) +
//    @"," + orderProduct.PrimaryPrice.ToString("F", CultureInfo.InvariantCulture) +
//    @"," + orderProduct.PrimaryAmount.ToString("F", CultureInfo.InvariantCulture) +
//    @",'" + orderProduct.Name + @"'" +
//    @",'" + orderProduct.Notes + @"'" +
//    @",'" + orderProduct.CustomProduct + @"'" +
//    @"," + orderProduct.PrimaryDiscountAmount.ToString("F", CultureInfo.InvariantCulture) +
//    @"," + orderProduct.DiscountAmount.ToString("F", CultureInfo.InvariantCulture) +
//    @"," + orderProduct.DiscountPercent.ToString("F", CultureInfo.InvariantCulture) +
//    @"," + orderProduct.GiftCardPayedAmount.ToString("F", CultureInfo.InvariantCulture) +
//    @"," + orderProduct.DiscountTax.ToString("F", CultureInfo.InvariantCulture) +
//    @"," + orderProduct.TaxAmount.ToString("F", CultureInfo.InvariantCulture) +
//    @"," + orderProduct.PrimaryTaxAmount.ToString("F", CultureInfo.InvariantCulture) +
//    @"," + orderProduct.TotalAmount.ToString("F", CultureInfo.InvariantCulture) +
//    @"," + orderProduct.PrimaryTotalAmount.ToString("F", CultureInfo.InvariantCulture) +
//    @",'" + orderProduct.ProcessListeners + @"'" +
//    @"," + orderProduct.CampaignId.ToStringOrNull();

//            foreach (KeyValuePair<string, bool> field in config)
//            {
//                if (field.Value && insertValue != null && String.IsNullOrEmpty(insertValue.ToString()))
//                {
//                    insertSql += String.Format(", '{0}'", insertValue.ToString());
//                }
//            }
//            insertSql += ")";

//            using (SqlConnection connection = new SqlConnection(connectionString))
//            {
//                connection.Open();
//                SqlCommand command = new SqlCommand();
//                command.Connection = connection;
//                command.CommandText = insertSql.ToString();
//                command.CommandType = System.Data.CommandType.Text;
//                try
//                {
//                    command.ExecuteNonQuery();
//                }
//                catch (Exception ex)
//                {
//                    DeleteOrder(connectionString, orderProduct.OrderId);
//                    throw ex;
//                }
//                finally
//                {
//                    connection.Close();
//                }
//            }
//        }
//        #endregion

//        #region Insert transaction
//        public static void InsertTransaction(string connectionString, Transaction transaction, Dictionary<string, bool> config)
//        {
//        }
//        #endregion

//        #region Crutch Fields Helper
//        public static readonly Dictionary<string, bool> OrderModel = new Dictionary<string, bool>
//        {
//            { "TISGuid", false },
//            { "ParentPurchaseId", false },
//            { "ChargeBonusAmount", false },
//            { "WriteOfFBonusAmount", false },
//            { "BankCardTypeId", false }
//        };

//        public static readonly Dictionary<string, bool> OrderProductModel = new Dictionary<string, bool>
//        {
//            { "CashDeskDiscountAmount", false }
//        };

//        public static readonly Dictionary<string, bool> TransactionModel = new Dictionary<string, bool>
//        {
//            { "Name", false },
//            { "BonusChargeId", false }
//        };
//        #endregion

//        #region Delete Order
//        public static void DeleteOrder(string connectionString, Guid Id)
//        {
//            string insertSql = "";
//            insertSql += String.Format("Delete from OrderProduct where OrderId = '{0}'\n", Id.ToString());
//            insertSql += String.Format("Delete from [Transaction] where OrderId = '{0}'\n", Id.ToString());
//            insertSql += String.Format("Delete from [Order] where Id = '{0}'\n", Id.ToString());
//            using (SqlConnection connection = new SqlConnection(connectionString))
//            {
//                connection.Open();
//                SqlCommand command = new SqlCommand();
//                command.Connection = connection;
//                command.CommandText = insertSql.ToString();
//                command.CommandType = System.Data.CommandType.Text;
//                try
//                {
//                    command.ExecuteNonQuery();
//                }
//                catch (Exception ex)
//                {
//                    Console.WriteLine(ex.Message + " " + ex.Source);
//                }
//                finally
//                {
//                    connection.Close();
//                }
//            }

//        }
//        #endregion

        
//    }


//    #region Helper
//    public static class MyDate
//    {
//        public static Guid GuidNullValidator(string s)
//        {
//            if (String.IsNullOrEmpty(s))
//            {
//                return Guid.Empty;
//            }
//            return new Guid(s);
//        }

//        public static string ToStringOrNull(this DateTime? dtN, string format)
//        {
//            if (dtN == null)
//                return "null";
//            else
//            {
//                var dt = DateTime.Now;
//                DateTime.TryParse(dtN.ToString(), out dt);
//                return "'" + dt.ToString(format) + "'";
//            }
//        }

//        public static string ToString(this decimal? dtN, string format, CultureInfo cultureInfo)
//        {
//            if (dtN == null)
//                return null;
//            else
//            {
//                decimal dt = (decimal)dtN;
//                //return dt.ToString("F", CultureInfo.InvariantCulture);
//                return dt.ToString(format, cultureInfo);
//            }
//        }

//        public static string ToStringOrNull(this Guid dtN)
//        {
//            if (dtN == Guid.Empty)
//                return "null";
//            else
//            {
//                Guid dt = dtN;
//                //return dt.ToString("F", CultureInfo.InvariantCulture);
//                return "'" + dt.ToString() + "'";
//            }
//        }
//    }
//    #endregion

//}
