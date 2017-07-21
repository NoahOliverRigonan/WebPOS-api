using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebPOS_api.Entities
{
    public class MstDiscountItem
    {
        public Int32 Id { get; set; }
        public Int32 DiscountId { get; set; }
        public Int32 ItemId { get; set; }
        public String ItemCode { get; set; }
        public String Item { get; set; }
    }
}