using System.ComponentModel.DataAnnotations;

namespace QuanLyNhaSach.Entities
{
    public class Base
    {
        [Key]
        public Guid Id { get; set; } = Guid.NewGuid();
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}