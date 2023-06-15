using System;
using System.Collections.Generic;

namespace QuanLyNhaSach.Entities
{
    public partial class Author : Base
    {
        public string? Name { get; set; }

        public virtual ICollection<Book> Books { get; set; } = new List<Book>();
    }
}