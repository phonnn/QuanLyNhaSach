namespace QuanLyNhaSach.Entities
{
    public class Book : Base
    {
        public string Name { get; set; }
        public int Price { get; set; }
        public BookType Type { get; set; }
        public Author Author { get; set; }
    }
}