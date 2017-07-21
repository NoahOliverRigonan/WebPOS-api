using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace WebPOS_api.ApiControllers
{
    [RoutePrefix("api/purchaseOrderLine")]
    public class ApiTrnPurchaseOrderLineController : ApiController
    {
        private Data.posdbDataContext db = new Data.posdbDataContext();


        [HttpGet, Route("get/{purchaseOrderLineId}")]
        public List<Entities.TrnPurchaseOrderLine> listPurchaseOrderLine(String purchaseOrderLineId)
        {
            var item = from d in db.MstItems select d;

            var unit = from d in db.MstUnits select d;

            var purchaseOrderLine = from d in db.TrnPurchaseOrderLines
                                    where d.PurchaseOrderId == Convert.ToInt32(purchaseOrderLineId)
                                    select new Entities.TrnPurchaseOrderLine
                                    {
                                        Id = d.Id,
                                        PurchaseOrderId = d.PurchaseOrderId,
                                        ItemId = d.ItemId,
                                        Item = item.Where(m => m.Id == d.ItemId).Select(m => m.ItemDescription).FirstOrDefault(),
                                        UnitId = d.UnitId,
                                        Unit = unit.Where(m => m.Id == d.UnitId).Select(m => m.Unit).FirstOrDefault(),
                                        Quantity = d.Quantity,
                                        Cost = d.Cost,
                                        Amount = (d.Quantity * d.Cost),
                                    };
            return purchaseOrderLine.ToList();
        }

        [HttpPost, Route("post")]
        public HttpResponseMessage postPurchaseOrderLine(Entities.TrnPurchaseOrderLine purchaseOrderLine)
        {
            try
            {
                Data.TrnPurchaseOrderLine newPurchaseOrderLine = new Data.TrnPurchaseOrderLine();
                newPurchaseOrderLine.PurchaseOrderId = purchaseOrderLine.PurchaseOrderId;
                newPurchaseOrderLine.ItemId = purchaseOrderLine.ItemId;
                newPurchaseOrderLine.UnitId = purchaseOrderLine.UnitId;
                newPurchaseOrderLine.Quantity = purchaseOrderLine.Quantity;
                newPurchaseOrderLine.Cost = purchaseOrderLine.Cost;
                newPurchaseOrderLine.Amount = purchaseOrderLine.Amount;
                db.TrnPurchaseOrderLines.InsertOnSubmit(newPurchaseOrderLine);
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
        public HttpResponseMessage putPurchaseOrderLine(String id, Entities.TrnPurchaseOrderLine newPurchaseOrderLine)
        {
            try
            {
                var purchaseOrderLine = from d in db.TrnPurchaseOrderLines where d.Id == Convert.ToInt32(id) select d;
                if (purchaseOrderLine.Any())
                {
                    //var userId = (from d in db.MstUsers where d.AspNetUserId == User.Identity.GetUserId() select d).FirstOrDefault().Id;

                    var updatePurchaseOrderLine = purchaseOrderLine.FirstOrDefault();
                    updatePurchaseOrderLine.PurchaseOrderId = newPurchaseOrderLine.PurchaseOrderId;
                    updatePurchaseOrderLine.ItemId = newPurchaseOrderLine.ItemId;
                    updatePurchaseOrderLine.UnitId = newPurchaseOrderLine.UnitId;
                    updatePurchaseOrderLine.Quantity = newPurchaseOrderLine.Quantity;
                    updatePurchaseOrderLine.Cost = newPurchaseOrderLine.Cost;
                    updatePurchaseOrderLine.Amount = newPurchaseOrderLine.Amount;
                    db.TrnPurchaseOrderLines.InsertOnSubmit(updatePurchaseOrderLine);
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
        public HttpResponseMessage deletePurchaseOrderLine(String id)
        {
            try
            {
                var activities = from d in db.TrnPurchaseOrderLines where d.Id == Convert.ToInt32(id) select d;
                if (activities.Any())
                {
                    db.TrnPurchaseOrderLines.DeleteOnSubmit(activities.First());
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
