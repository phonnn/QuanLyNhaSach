using System;
using System.Collections.Generic;

namespace QuanLyNhaSach.Entities
{
    public partial class ReceiptBook : Base
    {
        public Guid? Receipt { get; set; }

        public Guid? Book { get; set; }

        public int Amount { get; set; }

        public int Price { get; set; }

        public int Total { get; set; }

        public virtual Book? BookNavigation { get; set; }

        public virtual Receipt? ReceiptNavigation { get; set; }
    }
}