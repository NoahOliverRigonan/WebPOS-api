using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Microsoft.AspNet.Identity;

namespace WebPOS_api.ApiControllers
{
    [RoutePrefix("api/itemGroup")]
    public class ApiMstItemGroupController : ApiController
    {
        private Data.posdbDataContext db = new Data.posdbDataContext();

        [HttpGet, Route("list")]
        public List<Entities.MstItemGroup> listMstItemGroup()
        {
            var itemGroup = from d in db.MstItemGroups
                            select new Entities.MstItemGroup
                            {
                                Id = d.Id,
                                ItemGroup = d.ItemGroup,
                                ImagePath = d.ImagePath,
                                KitchenReport = d.KitchenReport,
                                EntryUserId = d.EntryUserId,
                                EntryDateTime = d.EntryDateTime,
                                UpdateUserId = d.UpdateUserId,
                                UpdateDateTime = d.UpdateDateTime,
                                IsLocked = d.IsLocked
                            };
            return itemGroup.ToList();
        }

        [HttpGet, Route("get/{itemGroupId}")]
        public List<Entities.MstItemGroup> listMstItemGroup(String itemGroupId)
        {
            var itemGroup = from d in db.MstItemGroups
                            where d.Id == Convert.ToInt32(itemGroupId)
                            select new Entities.MstItemGroup
                            {
                                Id = d.Id,
                                ItemGroup = d.ItemGroup,
                                ImagePath = d.ImagePath,
                                KitchenReport = d.ImagePath,
                                EntryUserId = d.EntryUserId,
                                EntryDateTime = d.EntryDateTime,
                                UpdateUserId = d.UpdateUserId,
                                UpdateDateTime = d.UpdateDateTime,
                                IsLocked = d.IsLocked
                            };
            return itemGroup.ToList();
        }
        //[HttpPost, Route("post")]
        //public HttpResponseMessage addMstItemGroup() { 
                
                
        //}
    }
}
