using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace WebPOS_api.ApiControllers
{
    [RoutePrefix("api/customer")]
    public class ApiMstCustomerController : ApiController
    {
        private Data.posdbDataContext db = new Data.posdbDataContext();

        [HttpGet, Route("list")]
        public List<Entities.MstCustomer> listMstCustomer()
        {
            var customer = from d in db.MstCustomers
                           select new Entities.MstCustomer
                           {
                               Id = d.Id,
                               Customer = d.Customer,
                               Address = d.Address,
                               ContactPerson = d.ContactPerson,
                               ContactNumber = d.ContactNumber,
                               CreditLimit = d.CreditLimit,
                               TermId = d.TermId,
                               TIN = d.TIN,
                               WithReward = d.WithReward,
                               RewardNumber = d.RewardNumber,
                               RewardConversion = d.RewardConversion,
                               AccountId = d.AccountId,
                               EntryUserId = d.EntryUserId,
                               EntryDateTime = d.EntryDateTime,
                               UpdateUserId = d.UpdateUserId,
                               UpdateDateTime = d.UpdateDateTime,
                               IsLocked = d.IsLocked
                           };
            return customer.ToList();

        }
    }
}
