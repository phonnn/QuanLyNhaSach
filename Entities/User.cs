namespace QuanLyNhaSach.Entities
{
    public class User : Base
    {
        public string Username { get; set; }
        public string Password { get; set; }
        public string Name { get; set; }
        public int Status { get; set; }
    }
}