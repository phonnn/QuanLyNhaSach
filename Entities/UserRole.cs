using System;
using System.Collections.Generic;

namespace QuanLyNhaSach.Entities
{
    public partial class UserRole : Base
    {
        public Guid? User { get; set; }

        public Guid? Role { get; set; }

        public virtual Role? RoleNavigation { get; set; }

        public virtual User? UserNavigation { get; set; }
    }
}