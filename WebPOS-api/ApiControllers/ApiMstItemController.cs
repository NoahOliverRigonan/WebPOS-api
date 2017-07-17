using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace WebPOS_api.ApiControllers
{
    [RoutePrefix("api/item")]
    public class ApiMstItemController : ApiController
    {
        private Data.posdbDataContext db = new Data.posdbDataContext();

        //LIST OF ITEm
        [HttpGet, Route("list")]
        public List<Entities.MstItem> listMstItem()
        {
            var item = from d in db.MstItems
                       select new Entities.MstItem
                       {
                           Id = d.Id,
                           ItemCode = d.ItemCode,
                           BarCode = d.BarCode,
                           ItemDescription = d.ItemDescription,
                           Alias = d.Alias,
                           GenericName = d.GenericName,
                           Category = d.Category,
                           SalesAccountId = d.SalesAccountId,
                           AssetAccountId = d.AssetAccountId,
                           CostAccountId = d.CostAccountId,
                           InTaxId = d.InTaxId,
                           OutTaxId = d.OutTaxId,
                           UnitId = d.UnitId,
                           Unit = d.MstUnit.Unit,
                           DefaultSupplierId = d.DefaultSupplierId,
                           Cost = d.Cost,
                           MarkUp = d.MarkUp,
                           Price = d.Price,
                           ImagePath = d.ImagePath,
                           ReorderQuantity = d.ReorderQuantity,
                           OnhandQuantity = d.OnhandQuantity,
                           IsInventory = d.IsInventory,
                           ExpiryDate = d.ExpiryDate,
                           LotNumber = d.LotNumber,
                           Remarks = d.Remarks,
                           EntryUserId = d.EntryUserId,
                           EntryDateTime = d.EntryDateTime,
                           UpdateUserId = d.UpdateUserId,
                           UpdateDateTime = d.UpdateDateTime,
                           IsLocked = d.IsLocked,
                           DefaultKitchenReport = d.DefaultKitchenReport,
                           IsPackage = d.IsPackage
                       };
            return item.ToList();
        }

        // LIST OF UNIT
        [HttpGet, Route("list/{id}")]
        public List<Entities.MstItem> listMstItemById(String id)
        {
            var tax = from d in db.MstTaxes
                      select d;

            var salesAccount = from d in db.MstAccounts
                               select d;

            var units = from d in db.MstUnits
                        select d;

            var supplier = from d in db.MstSuppliers
                           select d;

            var item = from d in db.MstItems
                       where d.Id == Convert.ToInt32(id)
                       //d.UnitId == units.Select(m => m.Id).FirstOrDefault() &&
                       //d.SalesAccountId == salesAccount.Select(m => m.Id).FirstOrDefault() &&
                       //d.AssetAccountId == salesAccount.Select(m => m.Id).FirstOrDefault() &&
                       //d.CostAccountId == salesAccount.Select(m => m.Id).FirstOrDefault()
                       select new Entities.MstItem
                       {
                           Id = d.Id,
                           ItemCode = d.ItemCode,
                           BarCode = d.BarCode,
                           ItemDescription = d.ItemDescription,
                           Alias = d.Alias,
                           GenericName = d.GenericName,
                           Category = d.Category,
                           SalesAccountId = d.SalesAccountId,
                           AssetAccountId = d.AssetAccountId,
                           CostAccountId = d.CostAccountId,
                           InTaxId = d.InTaxId,
                           OutTaxId = d.OutTaxId,
                           UnitId = d.UnitId,
                           Unit = d.MstUnit.Unit,
                           DefaultSupplierId = d.DefaultSupplierId,
                           Cost = d.Cost,
                           MarkUp = d.MarkUp,
                           Price = d.Price,
                           ImagePath = d.ImagePath,
                           ReorderQuantity = d.ReorderQuantity,
                           OnhandQuantity = d.OnhandQuantity,
                           IsInventory = d.IsInventory,
                           ExpiryDate = d.ExpiryDate,
                           LotNumber = d.LotNumber,
                           Remarks = d.Remarks,
                           EntryUserId = d.EntryUserId,
                           EntryDateTime = d.EntryDateTime,
                           UpdateUserId = d.UpdateUserId,
                           UpdateDateTime = d.UpdateDateTime,
                           IsLocked = d.IsLocked,
                           DefaultKitchenReport = d.DefaultKitchenReport,
                           IsPackage = d.IsPackage,

                           listPurchaseVatTax = tax.Select(m => new Entities.MstTax
                           {
                               Id = m.Id,
                               Tax = m.Tax
                           }).ToList(),

                           listSalesVatTax = tax.Select(m => new Entities.MstTax
                           {
                               Id = m.Id,
                               Tax = m.Tax
                           }).ToList(),

                           listSalesAccount = salesAccount.Where(m => m.AccountType == "SALES").Select(m => new Entities.MstAccount
                           {
                               Id = m.Id,
                               Account = m.Account
                           }).ToList(),

                           listAssetAccount = salesAccount.Where(m => m.AccountType == "ASSET").Select(m => new Entities.MstAccount
                           {
                               Id = m.Id,
                               Account = m.Account
                           }).ToList(),

                           listCostAccount = salesAccount.Where(m => m.AccountType == "EXPENSES").Select(m => new Entities.MstAccount
                           {
                               Id = m.Id,
                               Account = m.Account
                           }).ToList(),


                           listSupplier = supplier.Select(m => new Entities.MstSupplier
                           {
                               Id = m.Id,
                               Supplier = m.Supplier
                           }).ToList(),

                           listUnit = units.Select(m => new Entities.MstUnit
                           {
                               Id = m.Id,
                               Unit = m.Unit
                           }).ToList(),

                       };

            return item.ToList();
        }

        // add item
        [HttpPost, Route("post")]
        public Int32 postItem(Entities.MstItem item)
        {
            try
            {
                var userId = (from d in db.MstUsers where d.AspNetUserId == User.Identity.GetUserId() select d).FirstOrDefault().Id;

                var accounts = from d in db.MstAccounts
                               select d;

                var tax = from d in db.MstTaxes
                          select d;

                var unit = from d in db.MstUnits
                           select d;

                var supplier = from d in db.MstSuppliers
                               select d;


                Data.MstItem newItem = new Data.MstItem();
                newItem.ItemCode = "n/a";
                newItem.BarCode = "n/a";
                newItem.ItemDescription = "n/a";
                newItem.Alias = "n/a";
                newItem.GenericName = "n/a";
                newItem.Category = "n/a";
                newItem.SalesAccountId = accounts.Where(d => d.AccountType == "SALES").Select(d => d.Id).FirstOrDefault();
                newItem.AssetAccountId = accounts.Where(d => d.AccountType == "ASSET").Select(d => d.Id).FirstOrDefault();
                newItem.CostAccountId = accounts.Where(d => d.AccountType == "EXPENSES").Select(d => d.Id).FirstOrDefault();
                newItem.InTaxId = tax.Select(d => d.Id).FirstOrDefault();
                newItem.OutTaxId = tax.Select(d => d.Id).FirstOrDefault();
                newItem.UnitId = unit.Select(d => d.Id).FirstOrDefault();
                newItem.DefaultSupplierId = supplier.Select(d => d.Id).FirstOrDefault();
                newItem.Cost = 0;
                newItem.MarkUp = 0;
                newItem.Price = 0;
                newItem.ImagePath = "n/a";
                newItem.ReorderQuantity = 0;
                newItem.OnhandQuantity = 0;
                newItem.IsInventory = false;
                newItem.ExpiryDate = DateTime.Today;
                newItem.LotNumber = "n/a";
                newItem.Remarks = "n/a";
                newItem.EntryUserId = userId;
                newItem.EntryDateTime = DateTime.Today;
                newItem.UpdateUserId = userId;
                newItem.UpdateDateTime = DateTime.Today;
                newItem.IsLocked = false;
                newItem.DefaultKitchenReport = "n/a";
                newItem.IsPackage = false;
                db.MstItems.InsertOnSubmit(newItem);
                db.SubmitChanges();

                return newItem.Id;
            }
            catch (Exception e)
            {
                Debug.WriteLine(e);
                return 0;
            }
        }

        //UPDATE ITEM
        [HttpPut, Route("put/{id}")]
        public HttpResponseMessage putItem(String id, Entities.MstItem item)
        {
            try
            {
                var items = from d in db.MstItems where d.Id == Convert.ToInt32(id) select d;
                if (items.Any())
                {
                    var userId = (from d in db.MstUsers where d.AspNetUserId == User.Identity.GetUserId() select d).FirstOrDefault().Id;

                    var updateItems = items.FirstOrDefault();
                    updateItems.ItemCode = item.ItemCode;
                    updateItems.BarCode = item.BarCode;
                    updateItems.ItemDescription = item.ItemDescription;
                    updateItems.Alias = item.Alias;
                    updateItems.GenericName = item.GenericName;
                    updateItems.Category = item.Category;
                    updateItems.SalesAccountId = item.SalesAccountId;
                    updateItems.AssetAccountId = item.AssetAccountId;
                    updateItems.CostAccountId = item.CostAccountId;
                    updateItems.InTaxId = item.InTaxId;
                    updateItems.OutTaxId = item.OutTaxId;
                    updateItems.UnitId = item.UnitId;
                    updateItems.DefaultSupplierId = item.DefaultSupplierId;
                    updateItems.Cost = item.Cost;
                    updateItems.MarkUp = item.MarkUp;
                    updateItems.Price = item.Price;
                    updateItems.ImagePath = item.ImagePath;
                    updateItems.ReorderQuantity = item.ReorderQuantity;
                    updateItems.OnhandQuantity = item.OnhandQuantity;
                    updateItems.IsInventory = item.IsInventory;
                    updateItems.ExpiryDate = item.ExpiryDate;
                    updateItems.LotNumber = item.LotNumber;
                    updateItems.Remarks = item.Remarks;
                    updateItems.EntryUserId = item.EntryUserId;
                    updateItems.EntryDateTime = DateTime.Now;
                    updateItems.UpdateUserId = item.UpdateUserId;
                    updateItems.UpdateDateTime = DateTime.Now;
                    updateItems.IsLocked = item.IsLocked;
                    updateItems.DefaultKitchenReport = item.DefaultKitchenReport;
                    updateItems.IsPackage = item.IsPackage;
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
        public HttpResponseMessage deleteItem(String id)
        {
            try
            {
                var activities = from d in db.MstItems where d.Id == Convert.ToInt32(id) select d;
                if (activities.Any())
                {
                        db.MstItems.DeleteOnSubmit(activities.First());
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
