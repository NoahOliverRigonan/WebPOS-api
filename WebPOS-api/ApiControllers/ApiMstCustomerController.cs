using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Microsoft.AspNet.Identity;

namespace WebPOS_api.ApiControllers
{
    [RoutePrefix("api/customer")]
    public class ApiMstCustomerController : ApiController
    {
        private Data.posdbDataContext db = new Data.posdbDataContext();

        [HttpGet, Route("list")]
        public List<Entities.MstCustomer> listCustomer()
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

        [HttpGet, Route("get/{id}")]
        public List<Entities.MstCustomer> listCustomerById(String id)
        {
            var term = from d in db.MstTerms select d;

            var account = from d in db.MstAccounts select d;

            var itemPrice = from d in db.MstItemPrices select d;

            var customer = from d in db.MstCustomers
                           where d.Id == Convert.ToInt32(id)
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
                               IsLocked = d.IsLocked,
                               listTerm = term.Select(m => new Entities.MstTerm
                               {
                                   Id = m.Id,
                                   Term = m.Term,
                               }).ToList(),

                               listAccount = account.Where(m => m.AccountType == "ASSET").Select(m => new Entities.MstAccount
                               {
                                   Id = m.Id,
                                   Account = m.Account,
                               }).ToList(),

                               listDefaultPrice = itemPrice.Select(m => new Entities.MstItemPrice
                               {
                                   Id = m.Id,
                                   PriceDescription = m.PriceDescription,
                               }).ToList(),
                           };
            return customer.ToList();
        }

        [HttpPost, Route("post")]
        public Int32 postCustomer(Entities.MstCustomer customer)
        {
            try
            {
                var term = from d in db.MstTerms select d;

                var account = from d in db.MstAccounts select d;

                var userId = (from d in db.MstUsers where d.AspNetUserId == User.Identity.GetUserId() select d).FirstOrDefault().Id;

                Data.MstCustomer newCustomer = new Data.MstCustomer();
                newCustomer.Customer = "n/a";
                newCustomer.Address = "n/a";
                newCustomer.ContactPerson = "n/a";
                newCustomer.ContactNumber = "n/a";
                newCustomer.CreditLimit = 0;
                newCustomer.TermId = term.Select(m => m.Id).FirstOrDefault();
                newCustomer.TIN = "n/a";
                newCustomer.WithReward = false;
                newCustomer.RewardNumber = "n/a";
                newCustomer.RewardConversion = 0;
                newCustomer.AccountId = account.Select(m => m.Id).FirstOrDefault();
                newCustomer.EntryUserId = userId;
                newCustomer.EntryDateTime = DateTime.Today;
                newCustomer.UpdateUserId = userId;
                newCustomer.UpdateDateTime = DateTime.Today;
                newCustomer.IsLocked = false;
                db.MstCustomers.InsertOnSubmit(newCustomer);
                db.SubmitChanges();

                return newCustomer.Id;
            }
            catch (Exception e)
            {
                Debug.WriteLine(e);
                return 0;
            }
        }



        [HttpPut, Route("put/{id}")]
        public HttpResponseMessage putCustomer(String id, Entities.MstCustomer customer)
        {
            try
            {
                var customers = from d in db.MstCustomers where d.Id == Convert.ToInt32(id) select d;
                if (customers.Any())
                {
                    //var userId = (from d in db.MstUsers where d.AspNetUserId == User.Identity.GetUserId() select d).FirstOrDefault().Id;

                    var updateCustomers = customers.FirstOrDefault();
                    updateCustomers.Customer = customer.Customer;
                    updateCustomers.Address = customer.Address;
                    updateCustomers.ContactPerson = customer.ContactPerson;
                    updateCustomers.ContactNumber = customer.ContactNumber;
                    updateCustomers.CreditLimit = customer.CreditLimit;
                    updateCustomers.TermId = customer.TermId;
                    updateCustomers.TIN = customer.TIN;
                    updateCustomers.WithReward = customer.WithReward;
                    updateCustomers.RewardNumber = customer.RewardNumber;
                    updateCustomers.RewardConversion = customer.RewardConversion;
                    updateCustomers.AccountId = customer.AccountId;
                    updateCustomers.EntryUserId = customer.EntryUserId;
                    updateCustomers.EntryDateTime = DateTime.Now;
                    updateCustomers.UpdateUserId = customer.UpdateUserId;
                    updateCustomers.UpdateDateTime = DateTime.Now;
                    updateCustomers.IsLocked = true;
                    db.SubmitChanges();

                    return Request.CreateResponse(HttpStatusCode.OK);
                }
                else
                {
                    return Request.CreateResponse(HttpStatusCode.NotFound);
                }
            }
            catch (Exception e)
            {
                Debug.WriteLine(e);
                return Request.CreateResponse(HttpStatusCode.BadRequest);
            }
        }

        //DELETE ITEM
        [HttpDelete, Route("delete/{id}")]
        public HttpResponseMessage deleteCustomer(String id)
        {
            try
            {
                var activities = from d in db.MstCustomers where d.Id == Convert.ToInt32(id) select d;
                if (activities.Any())
                {
                    db.MstCustomers.DeleteOnSubmit(activities.First());
                    db.SubmitChanges();

                    return Request.CreateResponse(HttpStatusCode.OK);
                }
                else
                {
                    return Request.CreateResponse(HttpStatusCode.NotFound);
                }
            }
            catch (Exception e)
            {
                Debug.WriteLine(e);
                return Request.CreateResponse(HttpStatusCode.BadRequest);
            }
        }
    }
}
