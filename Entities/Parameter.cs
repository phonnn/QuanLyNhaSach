﻿namespace QuanLyNhaSach.Entities
{
    public class Parameter : Entity
    {
        public string Name { get; set; }
        public string Type { get; set; }
        public string Value { get; set; }
        public int Status { get; set; }
    }
}