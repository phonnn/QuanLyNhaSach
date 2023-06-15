using System;
using System.Collections.Generic;

namespace QuanLyNhaSach.Entities
{
    public class PreReceipt
    {
        public string BookId { get; set; }
        public string BookName { get; set; }
        public int Price { get; set; }
        public int Amount { get; set; }
        public int Total { get; set; }
    }
    public partial class Receipt : Base
    {
        public virtual BuyReceipt? BuyReceipt { get; set; }

        public virtual ICollection<ReceiptBook> ReceiptBooks { get; set; } = new List<ReceiptBook>();

        public virtual SellReceipt? SellReceipt { get; set; }
    }
}