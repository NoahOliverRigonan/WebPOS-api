﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebPOS_api.Entities
{
    public class MstSupplier
    {
        public Int32 Id { get; set; }
        public String Supplier { get; set; }
        public String Address { get; set; }
        public String TelephoneNumber { get; set; }
        public String CellphoneNumber { get; set; }
        public String FaxNumber { get; set; }
        public Int32 TermId { get; set; }
        public String TIN { get; set; }
        public Int32 AccountId { get; set; }
        public Int32 EntryUserId { get; set; }
        public DateTime EntryDateTime { get; set; }
        public Int32 UpdateUserId { get; set; }
        public DateTime UpdateDateTime { get; set; }
        public Boolean IsLocked { get; set; }
        public List<Entities.MstTerm> listTerm { get; set; }
        public List<Entities.MstAccount> listAccount { get; set; }
    }
}