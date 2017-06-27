using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace WebPOS_api.ApiControllers
{
    //[Authorize]
    [RoutePrefix("api/tableGroup")]
    public class ApiMstTableGroupController : ApiController
    {
        private Data.posdbDataContext db = new Data.posdbDataContext();

        [HttpGet, Route("list")]
        public List<Entities.MstTableGroup> listMstTableGroupId()
        {
            var tableGroup = from d in db.MstTableGroups
                             select new Entities.MstTableGroup
                             {
                                 Id = d.Id,
                                 EntryUserId = d.EntryUserId,
                                 EntryDateTime = d.EntryDateTime,
                                 TableGroup = d.TableGroup,
                                 UpdateDateTime = d.UpdateDateTime,
                                 UpdateUserId = d.UpdateUserId,
                                 IsLocked = d.IsLocked
                             };
            return tableGroup.ToList();
        }

        [HttpGet, Route("get")]
        public List<Entities.MstTableGroup> listMstTableGroupWalkInDelivery()
        {
            var tableGroup = from d in db.MstTableGroups
                             where d.TableGroup != "Dine-in"
                             select new Entities.MstTableGroup 
                             {
                                 Id = d.Id,
                                 EntryUserId = d.EntryUserId,
                                 EntryDateTime = d.EntryDateTime,
                                 TableGroup = d.TableGroup,
                                 UpdateDateTime = d.UpdateDateTime,
                                 UpdateUserId = d.UpdateUserId,
                                 IsLocked = d.IsLocked
                             };
            return tableGroup.ToList();
        }
    }
}
