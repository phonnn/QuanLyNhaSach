using System;
using System.Collections.Generic;

namespace QuanLyNhaSach.Entities
{
    public partial class BuyReceipt : Receipt
    {
        public Guid? User { get; set; }

        public virtual Receipt IdNavigation { get; set; } = null!;

        public virtual User? UserNavigation { get; set; }
    }
}