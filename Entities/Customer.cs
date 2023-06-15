using System;
using System.Collections.Generic;

namespace QuanLyNhaSach.Entities
{
    public partial class Customer : Base
    {
        public string Name { get; set; } = null!;

        public string? Address { get; set; }

        public string? Number { get; set; }

        public string? Email { get; set; }

        public int? Debt { get; set; }

        public virtual ICollection<Bill> Bills { get; set; } = new List<Bill>();

        public virtual ICollection<SellReceipt> SellReceipts { get; set; } = new List<SellReceipt>();
    }
}