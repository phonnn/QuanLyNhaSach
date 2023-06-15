using System;
using System.Collections.Generic;
namespace QuanLyNhaSach.Entities
{
    public partial class Bill : Base
    {
        public Guid? Customer { get; set; }

        public int Amount { get; set; }

        public virtual Customer? CustomerNavigation { get; set; }
    }
}