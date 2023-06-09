namespace QuanLyNhaSach.Entities
{
    public class Bill : Base
    {
        public Customer Customer { get; set; }
        public int Amount { get; set; }
    }
}