using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace WebPOS_api.ApiControllers
{
    [RoutePrefix("api/discountitem")]
    public class ApiMstDiscountItemController : ApiController
    {
        private Data.posdbDataContext db = new Data.posdbDataContext();

        [HttpPost, Route("post")]
        public HttpResponseMessage addDiscountItem(Entities.MstDiscountItem discountItem)
        {
            try
            {
                Data.MstDiscountItem newDiscountItems = new Data.MstDiscountItem();


                newDiscountItems.ItemId = discountItem.ItemId;
                newDiscountItems.DiscountId = discountItem.DiscountId;
                db.MstDiscountItems.InsertOnSubmit(newDiscountItems);
                db.SubmitChanges();

                return Request.CreateResponse(HttpStatusCode.OK);
            }
            catch (Exception e)
            {
                Debug.WriteLine(e);
                return Request.CreateResponse(HttpStatusCode.BadRequest);
            }
         
        }

        [HttpGet, Route("get/{id}")]
        public List<Entities.MstDiscountItem> listOfDiscountItem(String id)
        {
            var listItem = from d in db.MstItems
                           select d;

            var discountitem = from d in db.MstDiscountItems
                               where d.DiscountId == Convert.ToInt32(id)
                               select new Entities.MstDiscountItem
                               {
                                   Id = d.Id,
                                   ItemId = d.ItemId,
                                   DiscountId = d.DiscountId,
                                   ItemCode = d.MstItem.ItemCode,
                                   Item = d.MstItem.ItemDescription,
                          
                               };

            return discountitem.ToList();

        }

        [HttpDelete, Route("delete/{id}")]
        public HttpResponseMessage deleteDiscountedItem(String id)
        {
            try
            {
                var activities = from d in db.MstDiscountItems where d.Id == Convert.ToInt32(id) select d;
                if (activities.Any())
                {
                    db.MstDiscountItems.DeleteOnSubmit(activities.First());
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
