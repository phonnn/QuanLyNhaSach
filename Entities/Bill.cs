namespace QuanLyNhaSach.Entities
{
    public class Bill : Entity
    {
        public Customer Customer { get; set; }
        public int amount { get; set; }
    }
}