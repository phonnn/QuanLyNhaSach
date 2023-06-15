using System;
using System.Collections.Generic;

namespace QuanLyNhaSach.Entities
{
    public partial class BookType : Base
    {
        public string Name { get; set; } = null!;

        public virtual ICollection<Book> Books { get; set; } = new List<Book>();
    }
}