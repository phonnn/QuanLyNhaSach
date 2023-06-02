namespace QuanLyNhaSach.Entities
{
    public class ReceiptBook : Entity
    {
        public Book Book { get; set; }
        public Receipt Receipt { get; set; }
    }
}