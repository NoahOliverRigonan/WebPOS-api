using System;
using System.Collections.Generic;
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

        [HttpGet, Route("list")]
        public List<Entities.MstItem> listMstItemTable()
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

        // add item
        //[HttpPost, Route("post")]
        //public HttpResponseMessage postItem(Entities.MstItem item)
        //{
        //    try
        //    {

        //        // get last activity number
        //        var lastActivityNumber = from d in db.IS_TrnActivities.OrderByDescending(d => d.Id) select d;
        //        var activityNumberValue = "0000000001";
        //        if (lastActivityNumber.Any())
        //        {
        //            var activityNumber = Convert.ToInt32(lastActivityNumber.FirstOrDefault().ActivityNumber) + 0000000001;
        //            activityNumberValue = fillLeadingZeroes(activityNumber, 10);
        //        }
        //        var userId = (from d in db.MstUsers where d.UserId == User.Identity.GetUserId() select d.Id).FirstOrDefault();
        //        Data.IS_TrnActivity newActivity = new Data.IS_TrnActivity();
        //        newActivity.ActivityNumber = activityNumberValue;
        //        newActivity.ActivityDate = Convert.ToDateTime(activity.ActivityDate);
        //        newActivity.StaffUserId = userId;
        //        newActivity.CustomerId = activity.CustomerId;
        //        newActivity.ProductId = activity.ProductId;
        //        newActivity.ParticularCategory = activity.ParticularCategory;
        //        newActivity.Particulars = activity.Particulars;
        //        newActivity.NumberOfHours = activity.NumberOfHours;
        //        newActivity.ActivityAmount = activity.ActivityAmount;
        //        newActivity.ActivityStatus = activity.ActivityStatus;
        //        newActivity.LeadId = activity.LeadId;
        //        newActivity.QuotationId = activity.QuotationId;
        //        newActivity.DeliveryId = activity.DeliveryId;
        //        newActivity.SupportId = activity.SupportId;
        //        db.IS_TrnActivities.InsertOnSubmit(newActivity);
        //        db.SubmitChanges();

        //        return Request.CreateResponse(HttpStatusCode.OK);
        //    }
        //    catch (Exception e)
        //    {
        //        Debug.WriteLine(e);
        //        return Request.CreateResponse(HttpStatusCode.BadRequest);
        //    }
        //}
    }
}
