using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebPOS_api.Entities
{
    public class TrnCollection
    {
        public Int32 Id { get; set; }
        public Int32 PeriodId { get; set; }
        public DateTime CollectionDate { get; set; }
        public String CollectionNumber { get; set; }
        public Int32 TerminalId { get; set; }
        public String Terminal { get; set; }
        public String ManualORNumber { get; set; }
        public Int32 CustomerId { get; set; }
        public String Customer { get; set; }
        public String Remarks { get; set; }
        public Int32? SalesId { get; set; }
        public String SalesNumber { get; set; }
        public Decimal SalesBalanceAmount { get; set; }
        public Decimal Amount { get; set; }
        public Decimal TenderAmount { get; set; }
        public Decimal ChangeAmount { get; set; }
        public Int32 PreparedBy { get; set; }
        public Int32 CheckedBy { get; set; }
        public Int32 ApprovedBy { get; set; }
        public Boolean IsCancelled { get; set; }
        public Boolean IsLocked { get; set; }
        public Int32 EntryUserId { get; set; }
        public DateTime EntryDateTime { get; set; }
        public Int32 UpdateUserId { get; set; }
        public DateTime UpdateDateTime { get; set; }
        public List<Entities.MstUser> listPreparedBy { get; set; }
        public List<Entities.MstUser> listCheckedBy { get; set; }
        public List<Entities.MstUser> listApprovedBy { get; set; }
        public List<Entities.MstPeriod> listPeriod { get; set; }
        public List<Entities.TrnSales> listSales { get; set; }
        public List<Entities.MstCustomer> listCustomer { get; set; }
        public List<Entities.MstTerminal> listTerminal { get; set; }
        public List<Entities.MstPayType> listPayType { get; set; }
        public List<Entities.TrnStockIn> listStockIn{ get; set; }
        public List<Entities.MstAccount> listAccount { get; set; }
    }
}