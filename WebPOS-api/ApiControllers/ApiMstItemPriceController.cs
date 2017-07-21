using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace WebPOS_api.ApiControllers
{
    [RoutePrefix("api/itemPrice")]
    public class ApiMstItemPriceController : ApiController
    {
        private Data.posdbDataContext db = new Data.posdbDataContext();
        [HttpGet, Route("get/{itemPriceId}")]

        public List<Entities.MstItemPrice> listStockInLine(String itemPriceId)
        {
            var itemPrice = from d in db.MstItemPrices
                              where d.ItemId == Convert.ToInt32(itemPriceId)
                              select new Entities.MstItemPrice
                              {
                                  Id = d.Id,
                                  ItemId = d.ItemId,
                                  PriceDescription = d.PriceDescription,
                                  Price = d.Price,
                                  TriggerQuantity = d.TriggerQuantity,
                              };
            return itemPrice.ToList();
        }

        [HttpPost, Route("post")]
        public HttpResponseMessage postItemPrice(Entities.MstItemPrice itemPrice)
        {
            try
            {
                Data.MstItemPrice newItemPrice = new Data.MstItemPrice();
                newItemPrice.ItemId = itemPrice.ItemId;
                newItemPrice.PriceDescription = itemPrice.PriceDescription;
                newItemPrice.Price = itemPrice.Price;
                newItemPrice.TriggerQuantity = itemPrice.TriggerQuantity;
                db.MstItemPrices.InsertOnSubmit(newItemPrice);
                db.SubmitChanges();

                return Request.CreateResponse(HttpStatusCode.OK);
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
                return Request.CreateResponse(HttpStatusCode.InternalServerError, "Something's wrong from the server.");
            }

        }

        [HttpPut, Route("put/{id}")]
        public HttpResponseMessage putItemPrice(String id, Entities.MstItemPrice newItemPrice)
        {
            try
            {
                var itemPrice = from d in db.MstItemPrices where d.Id == Convert.ToInt32(id) select d;
                if (itemPrice.Any())
                {

                    var updateItemPrice = itemPrice.FirstOrDefault();
                    updateItemPrice.ItemId = newItemPrice.ItemId;
                    updateItemPrice.PriceDescription = newItemPrice.PriceDescription;
                    updateItemPrice.Price = newItemPrice.Price;
                    updateItemPrice.TriggerQuantity = newItemPrice.TriggerQuantity;
                    db.MstItemPrices.InsertOnSubmit(updateItemPrice);
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


        [HttpDelete, Route("delete/{id}")]
        public HttpResponseMessage deleteItemPrice(String id)
        {
            try
            {
                var activities = from d in db.MstItemPrices where d.Id == Convert.ToInt32(id) select d;
                if (activities.Any())
                {
                    db.MstItemPrices.DeleteOnSubmit(activities.First());
                    db.SubmitChanges();

                    return Request.CreateResponse(HttpStatusCode.OK);
                }
                else
                {
                    return Request.CreateResponse(HttpStatusCode.BadRequest);
                }
            }
            catch
            {
                return Request.CreateResponse(HttpStatusCode.NotFound, "Something went Wrong");
            }
        }
    }
}
