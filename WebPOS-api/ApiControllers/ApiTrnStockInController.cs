using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Microsoft.AspNet.Identity;
using System.Text.RegularExpressions;

namespace WebPOS_api.ApiControllers
{
    [RoutePrefix("api/stockIn")]
    public class ApiTrnStockInController : ApiController
    {
        private Data.posdbDataContext db = new Data.posdbDataContext();

        [HttpGet, Route("list")]
        public List<Entities.TrnStockIn> listStockIn()
        {
            var supplier = from d in db.MstSuppliers select d;

            var stockIn = from d in db.TrnStockIns
                          select new Entities.TrnStockIn
                          {
                              Id = d.Id,
                              PeriodId = d.PeriodId,
                              StockInDate = d.StockInDate,
                              StockInNumber = d.StockInNumber,
                              SupplierId = d.SupplierId,
                              Supplier = supplier.Where(m => m.Id == d.SupplierId).Select(m => m.Supplier).FirstOrDefault(),
                              Remarks = d.Remarks,
                              IsReturn = d.IsReturn,
                              CollectionId = d.CollectionId,
                              PurchaseOrderId = d.PurchaseOrderId,
                              PreparedBy = d.PreparedBy,
                              CheckedBy = d.CheckedBy,
                              ApprovedBy = d.ApprovedBy,
                              IsLocked = d.IsLocked,
                              EntryUserId = d.EntryUserId,
                              EntryDateTime = d.EntryDateTime,
                              UpdateUserId = d.UpdateUserId,
                              UpdateDateTime = d.UpdateDateTime,
                          };
            return stockIn.ToList();
        }


        [HttpGet, Route("get/{id}")]
        public List<Entities.TrnStockIn> listStockInById(String id)
        {
            var user = from d in db.MstUsers select d;

            var supplier = from d in db.MstSuppliers select d;

            var purchaseOrder = from d in db.TrnPurchaseOrders select d;

            var period = from d in db.MstPeriods select d;

            //LIST FOR STOCK IN LINE
            var item = from d in db.MstItems select d;

            var unit = from d in db.MstUnits select d;

            var account = from d in db.MstAccounts select d;

            var stockIn = from d in db.TrnStockIns
                          where d.Id == Convert.ToInt32(id)
                          select new Entities.TrnStockIn
                          {
                              Id = d.Id,
                              PeriodId = d.PeriodId,
                              StockInDate = d.StockInDate,
                              StockInNumber = d.StockInNumber,
                              SupplierId = d.SupplierId,
                              Remarks = d.Remarks,
                              IsReturn = d.IsReturn,
                              CollectionId = d.CollectionId,
                              PurchaseOrderId = d.PurchaseOrderId,
                              PreparedBy = user.Select(m => m.Id).FirstOrDefault(),
                              Prepared = user.Where(m => m.Id == user.Select(n => n.Id).FirstOrDefault()).Select(m => m.UserName).FirstOrDefault(),
                              CheckedBy = d.CheckedBy,
                              ApprovedBy = d.ApprovedBy,
                              IsLocked = d.IsLocked,
                              EntryUserId = d.EntryUserId,
                              EntryDateTime = d.EntryDateTime,
                              UpdateUserId = d.UpdateUserId,
                              UpdateDateTime = d.UpdateDateTime,
                              listPreparedBy = user.Where(m => m.Id == user.Select(n => n.Id).FirstOrDefault()).Select(m => new Entities.MstUser
                              {
                                  Id = m.Id,
                                  UserName = m.UserName
                              }).ToList(),

                              listApprovedBy = user.Select(m => new Entities.MstUser
                              {
                                  Id = m.Id,
                                  UserName = m.UserName
                              }).ToList(),

                              listCheckedBy = user.Select(m => new Entities.MstUser
                              {
                                  Id = m.Id,
                                  UserName = m.UserName
                              }).ToList(),

                              listCategoryItem = item.Select(m => new Entities.MstItem
                              {
                                  Id = m.Id,
                                  Category = m.Category
                              }).ToList(),

                              listSupplier = supplier.Select(m => new Entities.MstSupplier
                              {
                                  Id = m.Id,
                                  Supplier = m.Supplier
                              }).ToList(),

                              listPurchaseOrder = purchaseOrder.Select(m => new Entities.TrnPurchaseOrder
                              {
                                  Id = m.Id,
                                  PurchaseOrderNumber = m.PurchaseOrderNumber,
                                  PurchaseOrderDate = m.PurchaseOrderDate,
                                  PurchaseOrderNumberAndDate = m.PurchaseOrderNumber + " " + " " + " " + " " + " | " + m.PurchaseOrderDate.Month + "/" + m.PurchaseOrderDate.Day + "/" + m.PurchaseOrderDate.Year,
                              }).ToList(),

                              listPeriod = period.Select(m => new Entities.MstPeriod
                              {
                                  Id = m.Id,
                                  Period = m.Period
                              }).ToList(),

                              listItem = item.Select(m => new Entities.MstItem
                              {
                                  Id = m.Id,
                                  ItemDescription = m.ItemDescription,
                              }).ToList(),

                              listUnit = unit.Select(m => new Entities.MstUnit
                              {
                                  Id = m.Id,
                                  Unit = m.Unit,
                              }).ToList(),

                              listAccount = account.Where(m => m.AccountType == "ASSET").Select(m => new Entities.MstAccount { 
                                  Id = m.Id,
                                  Account = m.Account,
                              }).ToList(),
                          };
            return stockIn.ToList();
        }

        [HttpPost, Route("post")]
        public Int32 postTrnStockIn(Entities.TrnStockIn add)
        {

            try
            {

                var period = from d in db.MstPeriods where d.Id > 0 select d.Id;
                var supplier = from d in db.MstSuppliers select d;
                var collection = from d in db.TrnCollections select d;
                var purcherOrder = from d in db.TrnPurchaseOrders select d;
                var user = from d in db.MstUsers select d;
                var stockIn = from d in db.TrnStockIns.OrderByDescending(m => m.StockInNumber) select d;
                var userId = (from d in db.MstUsers where d.AspNetUserId == User.Identity.GetUserId() select d).FirstOrDefault().Id;

                Data.TrnStockIn addStock = new Data.TrnStockIn();
                addStock.PeriodId = period.FirstOrDefault();
                addStock.StockInDate = DateTime.Today;
                addStock.StockInNumber = stockIn.Select(m => m.StockInNumber).FirstOrDefault();
                addStock.SupplierId = supplier.Select(m => m.Id).FirstOrDefault();
                addStock.Remarks = "n/a";
                addStock.IsReturn = false;
                addStock.CollectionId = collection.Select(m => m.Id).FirstOrDefault();
                addStock.PurchaseOrderId = purcherOrder.Select(m => m.Id).FirstOrDefault();
                addStock.PreparedBy = user.Select(m => m.Id).FirstOrDefault();
                addStock.CheckedBy = user.Select(m => m.Id).FirstOrDefault();
                addStock.ApprovedBy = user.Select(m => m.Id).FirstOrDefault();
                addStock.IsLocked = 0;
                addStock.EntryUserId = userId;
                addStock.EntryDateTime = DateTime.Today;
                addStock.UpdateUserId = userId;
                addStock.UpdateDateTime = DateTime.Today;
                db.TrnStockIns.InsertOnSubmit(addStock);
                db.SubmitChanges();

                return addStock.Id;
            }
            catch (Exception e)
            {
                Debug.WriteLine(e);
                return 0;
            }
        }


        [HttpPut, Route("put/{id}")]
        public HttpResponseMessage putUser(String id, Entities.TrnStockIn stockIn)
        {
            try
            {
                var stockIns = from d in db.TrnStockIns where d.Id == Convert.ToInt32(id) select d;
                if (stockIns.Any())
                {
                    //var userId = (from d in db.MstUsers where d.AspNetUserId == User.Identity.GetUserId() select d).FirstOrDefault().Id;

                    var updateTrnStockIn = stockIns.FirstOrDefault();
                    updateTrnStockIn.PeriodId = stockIn.PeriodId;
                    updateTrnStockIn.StockInDate = Convert.ToDateTime(stockIn.StockInDate);
                    updateTrnStockIn.StockInNumber = stockIn.StockInNumber;
                    updateTrnStockIn.SupplierId = stockIn.SupplierId;
                    updateTrnStockIn.Remarks = stockIn.Remarks;
                    updateTrnStockIn.IsReturn = stockIn.IsReturn;
                    updateTrnStockIn.CollectionId = stockIn.CollectionId;
                    updateTrnStockIn.PurchaseOrderId = stockIn.PurchaseOrderId;
                    updateTrnStockIn.PreparedBy = stockIn.PreparedBy;
                    updateTrnStockIn.CheckedBy = stockIn.CheckedBy;
                    updateTrnStockIn.ApprovedBy = stockIn.ApprovedBy;
                    updateTrnStockIn.IsLocked = stockIn.IsLocked;
                    updateTrnStockIn.EntryUserId = stockIn.EntryUserId;
                    updateTrnStockIn.EntryDateTime = DateTime.Now;
                    updateTrnStockIn.UpdateUserId = stockIn.UpdateUserId;
                    updateTrnStockIn.UpdateDateTime = DateTime.Now;
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
        public HttpResponseMessage deleteTrnStockIn(String id)
        {
            try
            {
                var activities = from d in db.TrnStockIns where d.Id == Convert.ToInt32(id) select d;
                if (activities.Any())
                {
                    db.TrnStockIns.DeleteOnSubmit(activities.First());
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
