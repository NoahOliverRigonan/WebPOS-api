using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebPOS_api.Entities
{
    public class TrnPurchaseOrder
    {
        public Int32 Id { get; set; }
        public Int32 PeriodId { get; set; }
        public DateTime PurchaseOrderDate { get; set; }
        public String PurchaseOrderNumber { get; set; }
        public Decimal Amount { get; set; }
        public Int32 SupplierId { get; set; }
        public String Supplier { get; set; }
        public String Remarks { get; set; }
        public Int32 PreparedBy { get; set; }
        public Int32 CheckedBy { get; set; }
        public Int32 ApprovedBy { get; set; }
        public Boolean IsLocked { get; set; }
        public Int32 EntryUserId { get; set; }
        public DateTime EntryDateTime { get; set; }
        public Int32 UpdateUserId { get; set; }
        public DateTime UpdateDateTime { get; set; }
        public String PurchaseOrderNumberAndDate { get; set; }
        public List<Entities.MstUser> listPreparedBy { get; set; }
        public List<Entities.MstUser> listCheckedBy { get; set; }
        public List<Entities.MstUser> listApprovedBy { get; set; }
        public List<Entities.MstPeriod> listPeriod { get; set; }
        public List<Entities.MstSupplier> listSupplier { get; set; }

    }
}