using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace WebPOS_api.ApiControllers
{

    [RoutePrefix("api/table")]
    public class ApiMstTableController : ApiController
    {
        private Data.posdbDataContext db = new Data.posdbDataContext();

        [HttpGet, Route("list/{tableId}")]
        public List<Entities.MstTable> listMstTableId(String tableId)
        {
            var table = from d in db.MstTables
                        where d.TableGroupId == Convert.ToInt32(tableId)
                        select new Entities.MstTable
                        {
                            Id = d.Id,
                            TableCode = d.TableCode,
                            TableGroupId = d.TableGroupId,
                            TopLocation = d.TopLocation,
                            LeftLocation = d.LeftLocation
                        };

            return table.ToList();
        }
    }
}
