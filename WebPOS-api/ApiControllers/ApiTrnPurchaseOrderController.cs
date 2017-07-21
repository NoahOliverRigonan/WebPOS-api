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
    [RoutePrefix("api/purchaseOrder")]
    public class ApiTrnPurchaseOrderController : ApiController
    {
        private Data.posdbDataContext db = new Data.posdbDataContext();

        [HttpGet, Route("list")]
        public List<Entities.TrnPurchaseOrder> listPurchaseOrder()
        {
            var supplier = from d in db.MstSuppliers select d;

            var purchaseOrder = from d in db.TrnPurchaseOrders
                                select new Entities.TrnPurchaseOrder
                                {
                                    Id = d.Id,
                                    PeriodId = d.PeriodId,
                                    PurchaseOrderDate = d.PurchaseOrderDate,
                                    PurchaseOrderNumber = d.PurchaseOrderNumber,
                                    Amount = d.Amount,
                                    SupplierId = d.SupplierId,
                                    Supplier = supplier.Where(m => m.Id == d.SupplierId).Select(m => m.Supplier).FirstOrDefault(),
                                    Remarks = d.Remarks,
                                    PreparedBy = d.PreparedBy,
                                    CheckedBy = d.CheckedBy,
                                    ApprovedBy = d.ApprovedBy,
                                    IsLocked = d.IsLocked,
                                    EntryUserId = d.EntryUserId,
                                    EntryDateTime = d.EntryDateTime,
                                    UpdateUserId = d.UpdateUserId,
                                    UpdateDateTime = d.UpdateDateTime,
                                };
            return purchaseOrder.ToList();
        }

        [HttpGet, Route("get/{id}")]
        public List<Entities.TrnPurchaseOrder> listPurchaseOrderById(String id)
        {
            var supplier = from d in db.MstSuppliers select d;

            var user = from d in db.MstUsers select d;

            var period = from d in db.MstPeriods select d;

            var item = from d in db.MstItems select d;

            var unit = from d in db.MstUnits select d;

            var purchaseOrder = from d in db.TrnPurchaseOrders
                                where d.Id == Convert.ToInt32(id)
                                select new Entities.TrnPurchaseOrder
                                {
                                    Id = d.Id,
                                    PeriodId = d.PeriodId,
                                    PurchaseOrderDate = d.PurchaseOrderDate,
                                    PurchaseOrderNumber = d.PurchaseOrderNumber,
                                    Amount = d.Amount,
                                    SupplierId = d.SupplierId,
                                    Remarks = d.Remarks,
                                    PreparedBy = d.PreparedBy,
                                    CheckedBy = d.CheckedBy,
                                    ApprovedBy = d.ApprovedBy,
                                    IsLocked = d.IsLocked,
                                    EntryUserId = d.EntryUserId,
                                    EntryDateTime = d.EntryDateTime,
                                    UpdateUserId = d.UpdateUserId,
                                    UpdateDateTime = d.UpdateDateTime,
                                    listPreparedBy = user.Select(m => new Entities.MstUser
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

                                    listSupplier = supplier.Select(m => new Entities.MstSupplier
                                    {
                                        Id = m.Id,
                                        Supplier = m.Supplier,
                                        Address = m.Address,
                                        SupplierAndAddress = m.Supplier + " " + " " + " " + " " + "||" + m.Address
                                    }).ToList(),

                                    listPeriod = period.Select(m => new Entities.MstPeriod
                                    {
                                        Id = m.Id,
                                        Period = m.Period
                                    }).ToList(),

                                    listItem = item.Select(m => new Entities.MstItem
                                    {
                                        Id = m.Id,
                                        ItemDescription = m.ItemDescription
                                    }).ToList(),

                                    listUnit = unit.Select(m => new Entities.MstUnit { 
                                        Id = m.Id,
                                        Unit = m.Unit
                                    }).ToList(),
                                };
            return purchaseOrder.ToList();
        }

        [HttpPost, Route("post")]
        public Int32 postPurchaseOrder(Entities.TrnPurchaseOrder add)
        {

            try
            {

                var period = from d in db.MstPeriods select d.Id;
                var supplier = from d in db.MstSuppliers select d;
                var collection = from d in db.TrnCollections select d;
                var purcherOrder = from d in db.TrnPurchaseOrders select d;
                var user = from d in db.MstUsers select d;
                var stockIn = from d in db.TrnStockIns.OrderByDescending(m => m.StockInNumber) select d;
                var userId = (from d in db.MstUsers where d.AspNetUserId == User.Identity.GetUserId() select d).FirstOrDefault().Id;


                Data.TrnPurchaseOrder addPurchaseOrder = new Data.TrnPurchaseOrder();
                addPurchaseOrder.PeriodId = period.FirstOrDefault();
                addPurchaseOrder.PurchaseOrderDate = DateTime.Today;
                addPurchaseOrder.PurchaseOrderNumber = purcherOrder.Select(m => m.PurchaseOrderNumber).FirstOrDefault();
                addPurchaseOrder.Amount = 0;
                addPurchaseOrder.SupplierId = supplier.Select(m => m.Id).FirstOrDefault();
                addPurchaseOrder.Remarks = "n/a";
                addPurchaseOrder.PreparedBy = user.Select(m => m.Id).FirstOrDefault();
                addPurchaseOrder.CheckedBy = user.Select(m => m.Id).FirstOrDefault();
                addPurchaseOrder.ApprovedBy = user.Select(m => m.Id).FirstOrDefault();
                addPurchaseOrder.IsLocked = false;
                addPurchaseOrder.EntryUserId = userId;
                addPurchaseOrder.EntryDateTime = DateTime.Today;
                addPurchaseOrder.UpdateUserId = userId;
                addPurchaseOrder.UpdateDateTime = DateTime.Today;
                db.TrnPurchaseOrders.InsertOnSubmit(addPurchaseOrder);
                db.SubmitChanges();

                return addPurchaseOrder.Id;
            }
            catch (Exception e)
            {
                Debug.WriteLine(e);
                return 0;
            }
        }


        [HttpPut, Route("put/{id}")]
        public HttpResponseMessage putUser(String id, Entities.TrnPurchaseOrder purchaseOrder)
        {
            try
            {
                var purchaseOrders = from d in db.TrnPurchaseOrders where d.Id == Convert.ToInt32(id) select d;
                if (purchaseOrders.Any())
                {
                    //var userId = (from d in db.MstUsers where d.AspNetUserId == User.Identity.GetUserId() select d).FirstOrDefault().Id;

                    var updatePurchaseOrder = purchaseOrders.FirstOrDefault();
                    updatePurchaseOrder.PeriodId = purchaseOrder.PeriodId;
                    updatePurchaseOrder.PurchaseOrderDate = Convert.ToDateTime(purchaseOrder.PurchaseOrderDate);
                    updatePurchaseOrder.PurchaseOrderNumber = purchaseOrder.PurchaseOrderNumber;
                    updatePurchaseOrder.Amount = purchaseOrder.Amount;
                    updatePurchaseOrder.SupplierId = purchaseOrder.SupplierId;
                    updatePurchaseOrder.Remarks = purchaseOrder.Remarks;
                    updatePurchaseOrder.PreparedBy = purchaseOrder.PreparedBy;
                    updatePurchaseOrder.CheckedBy = purchaseOrder.CheckedBy;
                    updatePurchaseOrder.ApprovedBy = purchaseOrder.ApprovedBy;
                    updatePurchaseOrder.IsLocked = purchaseOrder.IsLocked;
                    updatePurchaseOrder.EntryUserId = purchaseOrder.EntryUserId;
                    updatePurchaseOrder.EntryDateTime = DateTime.Now;
                    updatePurchaseOrder.UpdateUserId = purchaseOrder.UpdateUserId;
                    updatePurchaseOrder.UpdateDateTime = DateTime.Now;
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
        public HttpResponseMessage deletePurchaseOrder(String id)
        {
            try
            {
                var activities = from d in db.TrnPurchaseOrders where d.Id == Convert.ToInt32(id) select d;
                if (activities.Any())
                {
                    db.TrnPurchaseOrders.DeleteOnSubmit(activities.First());
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
