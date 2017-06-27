using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace WebPOS_api.ApiControllers
{
    [RoutePrefix("api/itemGroupItem")]
    public class ApiMstItemGroupItemController : ApiController
    {
        private Data.posdbDataContext db = new Data.posdbDataContext();

        [HttpGet, Route("list")]
        public List<Entities.MstItemGroupItem> listMstItemGroupItem()
        {
            var itemGroupItem = from d in db.MstItemGroupItems
                                select new Entities.MstItemGroupItem
                                {
                                    Id = d.Id,
                                    ItemId = d.ItemId,
                                    Item = d.MstItem.Alias,
                                    ItemGroupId = d.ItemGroupId,
                                    ItemGroup = d.MstItemGroup.ItemGroup,
                                };
            return itemGroupItem.ToList();
        }

        [HttpGet, Route("get/{itemGroupItemId}")]
        public List<Entities.MstItemGroupItem> listMstItemGroupItem(String itemGroupItemId)
        {
            var itemGroupItem = from d in db.MstItemGroupItems
                                where d.ItemGroupId == Convert.ToInt32(itemGroupItemId)
                                select new Entities.MstItemGroupItem
                                {
                                    Id = d.Id,
                                    ItemId = d.ItemId,
                                    Item = d.MstItem.Alias,
                                    ItemGroupId = d.ItemGroupId,
                                    ItemGroup = d.MstItemGroup.ItemGroup,
                                };
            return itemGroupItem.ToList();
        }

    }
}
