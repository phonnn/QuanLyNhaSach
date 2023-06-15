using System;
using System.Collections.Generic;

namespace QuanLyNhaSach.Entities
{
    public partial class SellReceipt : Receipt
    {
        public Guid? Customer { get; set; }

        public virtual Customer? CustomerNavigation { get; set; }

        public virtual Receipt IdNavigation { get; set; } = null!;
    }
}