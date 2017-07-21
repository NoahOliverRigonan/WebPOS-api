using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace WebPOS_api.ApiControllers
{
    [RoutePrefix("api/stockInLine")]
    public class ApiTrnStockInLineController : ApiController
    {
        private Data.posdbDataContext db = new Data.posdbDataContext();

        [HttpGet, Route("get/{stockInLineId}")]
        public List<Entities.TrnStockInLine> listStockInLine(String stockInLineId)
        {
            var stockInLine = from d in db.TrnStockInLines
                                    where d.StockInId == Convert.ToInt32(stockInLineId)
                                    select new Entities.TrnStockInLine
                                    {
                                        Id = d.Id,
                                        StockInId = d.StockInId,
                                        ItemId = d.ItemId,
                                        Item = d.MstItem.ItemDescription,
                                        UnitId = d.UnitId,
                                        Unit = d.MstUnit.Unit,
                                        Quantity = d.Quantity,
                                        Cost = d.Cost,
                                        Amount = d.Amount,
                                        ExpiryDate = d.ExpiryDate,
                                        LotNumber = d.LotNumber,
                                        AssetAccountId = d.AssetAccountId,
                                        AssetAccount = d.MstAccount.Account,
                                    };
            return stockInLine.ToList();
        }

        [HttpPost, Route("post")]
        public HttpResponseMessage postStockInLine(Entities.TrnStockInLine stockInLine)
        {
            try
            {
                Data.TrnStockInLine newStockInLine = new Data.TrnStockInLine();
                newStockInLine.StockInId = stockInLine.StockInId;
                newStockInLine.ItemId = stockInLine.ItemId;
                newStockInLine.UnitId = stockInLine.UnitId;
                newStockInLine.Quantity = stockInLine.Quantity;
                newStockInLine.Cost = stockInLine.Cost;
                newStockInLine.Amount = stockInLine.Amount;
                newStockInLine.ExpiryDate = stockInLine.ExpiryDate;
                newStockInLine.LotNumber = stockInLine.LotNumber;
                newStockInLine.AssetAccountId = stockInLine.AssetAccountId;
                db.TrnStockInLines.InsertOnSubmit(newStockInLine);
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
        public HttpResponseMessage putStockInLine(String id, Entities.TrnStockInLine newStockInLine)
        {
            try
            {
                var stockInLine = from d in db.TrnStockInLines where d.Id == Convert.ToInt32(id) select d;
                if (stockInLine.Any())
                {
                    //var userId = (from d in db.MstUsers where d.AspNetUserId == User.Identity.GetUserId() select d).FirstOrDefault().Id;

                    var updateStockInLine = stockInLine.FirstOrDefault();
                    updateStockInLine.StockInId = newStockInLine.StockInId;
                    updateStockInLine.ItemId = newStockInLine.ItemId;
                    updateStockInLine.UnitId = newStockInLine.UnitId;
                    updateStockInLine.Quantity = newStockInLine.Quantity;
                    updateStockInLine.Cost = newStockInLine.Cost;
                    updateStockInLine.Amount = newStockInLine.Amount;
                    newStockInLine.ExpiryDate = newStockInLine.ExpiryDate;
                    newStockInLine.LotNumber = newStockInLine.LotNumber;
                    newStockInLine.AssetAccountId = newStockInLine.AssetAccountId;
                    db.TrnStockInLines.InsertOnSubmit(updateStockInLine);
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
        public HttpResponseMessage deleteStockInLine(String id)
        {
            try
            {
                var activities = from d in db.TrnStockInLines where d.Id == Convert.ToInt32(id) select d;
                if (activities.Any())
                {
                    db.TrnStockInLines.DeleteOnSubmit(activities.First());
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
