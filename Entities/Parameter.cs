using System;
using System.Collections.Generic;

namespace QuanLyNhaSach.Entities
{
    public partial class Parameter : Base
    {
        public string Name { get; set; } = null!;

        public string Type { get; set; } = null!;

        public string? Value { get; set; }

        public int? Status { get; set; }
    }
}