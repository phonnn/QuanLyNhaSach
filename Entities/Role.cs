﻿using System;
using System.Collections.Generic;

namespace QuanLyNhaSach.Entities
{
    public partial class Role : Base
    {
        public string Name { get; set; } = null!;

        public virtual ICollection<UserRole> UserRoles { get; set; } = new List<UserRole>();
    }
}