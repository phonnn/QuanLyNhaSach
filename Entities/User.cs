using System;
using System.Collections.Generic;

namespace QuanLyNhaSach.Entities
{
    public partial class User : Base
    {
        public string Username { get; set; } = null!;

        public string Password { get; set; } = null!;

        public string? Name { get; set; }

        public int? Status { get; set; }

        public virtual ICollection<BuyReceipt> BuyReceipts { get; set; } = new List<BuyReceipt>();

        public virtual ICollection<UserRole> UserRoles { get; set; } = new List<UserRole>();
    }
}