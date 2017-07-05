using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebPOS_api.Entities
{
    public class MstUser
    {
        public Int32 Id { get; set; }
        public String UserName { get; set; }
        public String Password { get; set; }
        public String FullName { get; set; }
        public String UserCardNumber { get; set; }
        public Int32 EntryUserId { get; set; }
        public DateTime EntryDateTime { get; set; }
        public Int32 UpdateUserId { get; set; }
        public DateTime UpdateDateTime { get; set; }
        public Boolean IsLocked { get; set; }
        public string AspNetUserId { get; set; }
        public List<Entities.MstUser> listMstUser { get; set; }
        public List<Entities.SysForm> listSysForm { get; set; }

        public List<Entities.MstUserForm> listMstUserForm { get; set; }
    }
}