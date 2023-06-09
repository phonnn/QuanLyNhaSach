namespace QuanLyNhaSach.Entities
{
    public class ReceiptBook : Base
    {
        public Receipt Receipt { get; set; }
        public Book Book { get; set; }
        public int Price { get; set; }
    }
}