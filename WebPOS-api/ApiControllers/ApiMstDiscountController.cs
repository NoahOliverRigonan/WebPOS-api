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
    [RoutePrefix("api/discount")]
    public class ApiMstDiscountController : ApiController
    {
        private Data.posdbDataContext db = new Data.posdbDataContext();


        [HttpGet, Route("list")]
        public List<Entities.MstDiscount> listMstDiscount()
        {
            var discount = from d in db.MstDiscounts
                           select new Entities.MstDiscount
                            {
                                Id = d.Id,
                                Discount = d.Discount,
                                DiscountRate = d.DiscountRate,
                                IsVatExempt = d.IsVatExempt,
                                IsDateScheduled = d.IsDateScheduled,
                                DateStart = d.DateStart,
                                DateEnd = d.DateEnd,
                                IsTimeScheduled = d.IsTimeScheduled,
                                TimeStart = d.TimeStart,
                                TimeEnd = d.TimeEnd,
                                IsDayScheduled = d.IsDayScheduled,
                                DayMon = d.DayMon,
                                DayTue = d.DayTue,
                                DayWed = d.DayWed,
                                DayThu = d.DayThu,
                                DayFri = d.DayFri,
                                DaySat = d.DaySat,
                                DaySun = d.DaySun,
                                EntryUserId = d.EntryUserId,
                                EntryDateTime = d.EntryDateTime,
                                UpdateUserId = d.UpdateUserId,
                                UpdateDateTime = d.UpdateDateTime,
                                IsLocked = d.IsLocked
                            };
            return discount.ToList();
        }


        [HttpGet, Route("get/{id}")]
        public List<Entities.MstDiscount> listMstDiscount(String id)
        {
            var discount = from d in db.MstDiscounts
                           where d.Id == Convert.ToInt32(id)
                           select new Entities.MstDiscount
                           {
                               Id = d.Id,
                               Discount = d.Discount,
                               DiscountRate = d.DiscountRate,
                               IsVatExempt = d.IsVatExempt,
                               IsDateScheduled = d.IsDateScheduled,
                               DateStart = d.DateStart,
                               DateEnd = d.DateEnd,
                               IsTimeScheduled = d.IsTimeScheduled,
                               TimeStart = d.TimeStart,
                               TimeEnd = d.TimeEnd,
                               IsDayScheduled = d.IsDayScheduled,
                               DayMon = d.DayMon,
                               DayTue = d.DayTue,
                               DayWed = d.DayWed,
                               DayThu = d.DayThu,
                               DayFri = d.DayFri,
                               DaySat = d.DaySat,
                               DaySun = d.DaySun,
                               EntryUserId = d.EntryUserId,
                               EntryDateTime = d.EntryDateTime,
                               UpdateUserId = d.UpdateUserId,
                               UpdateDateTime = d.UpdateDateTime,
                               IsLocked = d.IsLocked
                           };
            return discount.ToList();
        }

        [HttpPost, Route("post")]
        public Int32 postItem(Entities.MstDiscount discount)
        {
            try
            {
                var userId = (from d in db.MstUsers where d.AspNetUserId == User.Identity.GetUserId() select d).FirstOrDefault().Id;
                Data.MstDiscount newDiscount = new Data.MstDiscount();
                newDiscount.Discount = "n/a";
                newDiscount.DiscountRate = 0;
                newDiscount.IsVatExempt = false;
                newDiscount.IsDateScheduled = false;
                newDiscount.DateStart = DateTime.Today;
                newDiscount.DateEnd = DateTime.Today;
                newDiscount.IsTimeScheduled = false;
                newDiscount.TimeStart = DateTime.Today;
                newDiscount.TimeEnd = DateTime.Today;
                newDiscount.IsDayScheduled = false;
                newDiscount.DayMon = false;
                newDiscount.DayTue = false;
                newDiscount.DayWed = false;
                newDiscount.DayThu = false;
                newDiscount.DayFri = false;
                newDiscount.DaySat = false;
                newDiscount.DaySun = false;
                newDiscount.EntryUserId = userId;
                newDiscount.EntryDateTime = DateTime.Today;
                newDiscount.UpdateUserId = userId;
                newDiscount.UpdateDateTime = DateTime.Today;
                newDiscount.IsLocked = false;

                db.MstDiscounts.InsertOnSubmit(newDiscount);
                db.SubmitChanges();

                return newDiscount.Id;
            }
            catch (Exception e)
            {
                Debug.WriteLine(e);
                return 0;
            }
        }



        [HttpPut, Route("put/{id}")]
        public HttpResponseMessage putDiscount(String id, Entities.MstDiscount discount)
        {
            try
            {
                var discounts = from d in db.MstDiscounts where d.Id == Convert.ToInt32(id) select d;
                if (discounts.Any())
                {
                    //var userId = (from d in db.MstUsers where d.AspNetUserId == User.Identity.GetUserId() select d).FirstOrDefault().Id;

                    var updateDiscount = discounts.FirstOrDefault();
                    updateDiscount.Discount = discount.Discount;
                    updateDiscount.DiscountRate = discount.DiscountRate;
                    updateDiscount.IsVatExempt = discount.IsVatExempt;
                    updateDiscount.DateStart = discount.DateStart;
                    updateDiscount.DateEnd = discount.DateEnd;
                    updateDiscount.IsTimeScheduled = discount.IsTimeScheduled;
                    updateDiscount.TimeStart = discount.TimeStart;
                    updateDiscount.TimeEnd = discount.TimeEnd;
                    updateDiscount.IsDayScheduled = discount.IsDayScheduled;
                    updateDiscount.DayMon = discount.DayMon;
                    updateDiscount.DayTue = discount.DayTue;
                    updateDiscount.DayWed = discount.DayWed;
                    updateDiscount.DayThu = discount.DayThu;
                    updateDiscount.DayFri = discount.DayFri;
                    updateDiscount.DaySat = discount.DaySat;
                    updateDiscount.DaySun = discount.DaySun;
                    updateDiscount.EntryUserId = discount.EntryUserId;
                    updateDiscount.EntryDateTime = DateTime.Now;
                    updateDiscount.UpdateUserId = discount.UpdateUserId;
                    updateDiscount.UpdateDateTime = DateTime.Now;
                    updateDiscount.IsLocked = discount.IsLocked;
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
        public HttpResponseMessage deleteDiscount(String id)
        {
            try
            {
                var discounts = from d in db.MstDiscounts where d.Id == Convert.ToInt32(id) select d;
                if (discounts.Any())
                {
                    db.MstDiscounts.DeleteOnSubmit(discounts.First());
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
