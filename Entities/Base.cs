using System.ComponentModel.DataAnnotations;

namespace QuanLyNhaSach.Entities
{
    public class Base
    {
        public Guid Id { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}