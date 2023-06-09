using Microsoft.EntityFrameworkCore;
using QuanLyNhaSach.Entities;

namespace QuanLyNhaSach.DataAccess
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options) { }
        public DbSet<Author> Author { get; set; }
        public DbSet<Bill> Bill { get; set; }
        public DbSet<Book> Book { get; set; }
        public DbSet<BookType> BookType { get; set; }
        public DbSet<BuyReceipt> BuyReceipt { get; set; }
        public DbSet<Customer> Customer { get; set; }
        public DbSet<Parameter> Parameter { get; set; }
        public DbSet<Receipt> Receipt { get; set; }
        public DbSet<ReceiptBook> ReceiptBook { get; set; }
        public DbSet<SellReceipt> SellReceipt { get; set; }
        public DbSet<User> User { get; set; }
    }
}