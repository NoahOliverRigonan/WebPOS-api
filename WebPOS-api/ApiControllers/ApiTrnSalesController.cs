using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace WebPOS_api.ApiControllers
{
    [RoutePrefix("api/sales")]
    public class ApiTrnSalesController : ApiController
    {
        private Data.posdbDataContext db = new Data.posdbDataContext();

        [HttpGet, Route("list/{salesId}/{userId}/{tableId}")]
        public List<Entities.TrnSales> listTrnSalesId(String salesId, String userId, String tableId)
        {
            var sale = from d in db.TrnSales
                       where d.TableId == Convert.ToInt32(tableId)
                       && d.Id == Convert.ToInt32(salesId) &&
                       d.EntryUserId == Convert.ToInt32(userId)
                       select new Entities.TrnSales
                       {
                           Id = d.Id,
                           PeriodId = d.PeriodId,
                           SalesDate = d.SalesDate,
                           SalesNumber = d.SalesNumber,
                           ManualInvoiceNumber = d.ManualInvoiceNumber,
                           Amount = d.Amount,
                           TableId = d.TableId,
                           TableCode = d.MstTable.TableCode,
                           CustomerId = d.CustomerId,
                           AccountId = d.AccountId,
                           AccountName = d.MstUser.UserName,
                           TermId = d.TermId,
                           DiscountId = d.DiscountId,
                           SeniorCitizenId = d.SeniorCitizenId,
                           SeniorCitizenName = d.SeniorCitizenName,
                           SeniorCitizenAge = d.SeniorCitizenAge,
                           Remarks = d.Remarks,
                           SalesAgent = d.SalesAgent,
                           TerminalId = d.TerminalId,
                           PreparedBy = d.PreparedBy,
                           CheckedBy = d.CheckedBy,
                           ApprovedBy = d.ApprovedBy,
                           IsLocked = d.IsLocked,
                           IsCancelled = d.IsCancelled,
                           PaidAmount = d.PaidAmount,
                           CreditAmount = d.CreditAmount,
                           DebitAmount = d.DebitAmount,
                           BalanceAmount = d.BalanceAmount,
                           EntryUserId = d.EntryUserId,
                           EntryDateTime = d.EntryDateTime,
                           UpdateUserId = d.UpdateUserId,
                           UpdateDateTime = d.UpdateDateTime,
                           Pax = d.Pax
                       };
            return sale.ToList();
        }
    }
}
