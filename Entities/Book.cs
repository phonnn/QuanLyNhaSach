using System;
using System.Collections.Generic;

namespace QuanLyNhaSach.Entities
{
    public partial class Book : Base
    {
        public string Name { get; set; } = null!;

        public int Price { get; set; }

        public Guid? Type { get; set; }

        public Guid? Author { get; set; }
        public int Quantity { get; set; }

        public virtual Author? AuthorNavigation { get; set; }

        public virtual ICollection<ReceiptBook> ReceiptBooks { get; set; } = new List<ReceiptBook>();

        public virtual BookType? TypeNavigation { get; set; }
    }
}
