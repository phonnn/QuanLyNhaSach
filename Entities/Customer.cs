﻿namespace QuanLyNhaSach.Entities
{
    public class Customer : Entity
    {
        public string Name { get; set; }
        public string Address { get; set; }
        public string Number { get; set; }
        public string Email { get; set; }
        public int Debt { get; set; }
    }
}