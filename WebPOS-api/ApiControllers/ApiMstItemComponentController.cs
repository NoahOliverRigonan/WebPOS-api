using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace WebPOS_api.ApiControllers
{
    [RoutePrefix("api/itemComponent")]
    public class ApiMstItemComponentController : ApiController
    {
        private Data.posdbDataContext db = new Data.posdbDataContext();

        [HttpGet, Route("list")]
        public List<Entities.MstItemComponent> listMstItemComponent()
        {
            var itemComponent = from d in db.MstItemComponents
                                select new Entities.MstItemComponent
                                {
                                    Id = d.Id,
                                    ItemId = d.ItemId,
                                    Item = d.MstItem.ItemCode,
                                    ComponentItemId = d.ComponentItemId,
                                    UnitId = d.UnitId,
                                    Quantity = d.Quantity,
                                    Cost = d.Cost,
                                    Amount  = d.Amount,
                                    IsPrinted = d.IsPrinted,
                                };
            return itemComponent.ToList();

        }
    }
}
