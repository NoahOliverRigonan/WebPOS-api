using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Microsoft.AspNet.Identity;
using System.Diagnostics;

namespace WebPOS_api.ApiControllers
{
    [RoutePrefix("api/supplier")]
    public class ApiMstSupplierController : ApiController
    {
        private Data.posdbDataContext db = new Data.posdbDataContext();

        [HttpGet, Route("list")]
        public List<Entities.MstSupplier> listMstSuppler()
        {
            var supplier = from d in db.MstSuppliers
                           select new Entities.MstSupplier
                           {
                               Id = d.Id,
                               Supplier = d.Supplier,
                               Address = d.Address,
                               TelephoneNumber = d.TelephoneNumber,
                               CellphoneNumber = d.CellphoneNumber,
                               FaxNumber = d.FaxNumber,
                               TermId = d.TermId,
                               TIN = d.TIN,
                               AccountId = d.AccountId,
                               EntryUserId = d.EntryUserId,
                               EntryDateTime = d.EntryDateTime,
                               UpdateUserId = d.UpdateUserId,
                               UpdateDateTime = d.UpdateDateTime,
                               IsLocked = d.IsLocked,
                           };
            return supplier.ToList();
        }

        [HttpGet, Route("list/{id}")]
        public List<Entities.MstSupplier> listMstSuppler(String id)
        {
            var term = from d in db.MstTerms
                       select d;

            var account = from d in db.MstAccounts
                          select d;

            var supplier = from d in db.MstSuppliers
                           where d.Id == Convert.ToInt32(id)
                           select new Entities.MstSupplier
                           {
                               Id = d.Id,
                               Supplier = d.Supplier,
                               Address = d.Address,
                               TelephoneNumber = d.TelephoneNumber,
                               CellphoneNumber = d.CellphoneNumber,
                               FaxNumber = d.FaxNumber,
                               TermId = d.TermId,
                               TIN = d.TIN,
                               AccountId = d.AccountId,
                               EntryUserId = d.EntryUserId,
                               EntryDateTime = d.EntryDateTime,
                               UpdateUserId = d.UpdateUserId,
                               UpdateDateTime = d.UpdateDateTime,
                               IsLocked = d.IsLocked,

                               listAccount = account.Where(m => m.AccountType == "LIABILITY").Select(m => new Entities.MstAccount
                               {
                                   Id = m.Id,
                                   Account = m.Account
                               }).ToList(),

                               listTerm = term.Select(m => new Entities.MstTerm { 
                                    Id = m.Id,
                                    Term = m.Term
                               }).ToList(),


                           };
            return supplier.ToList();
        }

        [HttpPost, Route("post")]
        public Int32 postSupplier(Entities.MstSupplier supplier)
        {
            try
            {
                var userId = (from d in db.MstUsers where d.AspNetUserId == User.Identity.GetUserId() select d).FirstOrDefault().Id;

                var accounts = from d in db.MstAccounts
                               select d;

                var term = from d in db.MstTerms
                           select d;
                //var tax = from d in db.MstTaxes
                //          select d;

                //var unit = from d in db.MstUnits
                //           select d;

                //var supplier = from d in db.MstSuppliers
                //               select d;


                Data.MstSupplier newSupplier = new Data.MstSupplier();
                newSupplier.Supplier = "n/a";
                newSupplier.Address = "n/a";
                newSupplier.TelephoneNumber = "n/a";
                newSupplier.CellphoneNumber = "n/a";
                newSupplier.FaxNumber = "n/a";
                newSupplier.TermId = term.Select(m => m.Id).FirstOrDefault();
                newSupplier.TIN = "n/a";
                newSupplier.AccountId = accounts.Select(m => m.Id).FirstOrDefault();
                newSupplier.EntryUserId = userId;
                newSupplier.EntryDateTime = DateTime.Today;
                newSupplier.UpdateUserId = userId;
                newSupplier.UpdateDateTime = DateTime.Today;
                newSupplier.IsLocked = false;
                db.MstSuppliers.InsertOnSubmit(newSupplier);
                db.SubmitChanges();

                return newSupplier.Id;
            }
            catch (Exception e)
            {
                Debug.WriteLine(e);
                return 0;
            }
        }


        [HttpPut, Route("put/{id}")]
        public HttpResponseMessage putSupplier(String id, Entities.MstSupplier supplier)
        {
            try
            {
                var suppliers = from d in db.MstSuppliers where d.Id == Convert.ToInt32(id) select d;
                if (suppliers.Any())
                {
                    var userId = (from d in db.MstUsers where d.AspNetUserId == User.Identity.GetUserId() select d).FirstOrDefault().Id;

                    var updateSupplier = suppliers.FirstOrDefault();
                    updateSupplier.Supplier = supplier.Supplier;
                    updateSupplier.Address = supplier.Address;
                    updateSupplier.TelephoneNumber = supplier.TelephoneNumber;
                    updateSupplier.CellphoneNumber = supplier.CellphoneNumber;
                    updateSupplier.FaxNumber = supplier.FaxNumber;
                    updateSupplier.TermId = supplier.TermId;
                    updateSupplier.TIN = supplier.TIN;
                    updateSupplier.AccountId = supplier.AccountId;
                    updateSupplier.EntryUserId = supplier.EntryUserId;
                    updateSupplier.EntryDateTime = DateTime.Now;
                    updateSupplier.UpdateUserId = supplier.UpdateUserId;
                    updateSupplier.UpdateDateTime = DateTime.Now;
                    updateSupplier.IsLocked = supplier.IsLocked;
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

        //DELETE SUPPLIER
        [HttpDelete, Route("delete/{id}")]
        public HttpResponseMessage deleteSupplier(String id)
        {
            try
            {
                var activities = from d in db.MstSuppliers where d.Id == Convert.ToInt32(id) select d;
                if (activities.Any())
                {
                    db.MstSuppliers.DeleteOnSubmit(activities.First());
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
