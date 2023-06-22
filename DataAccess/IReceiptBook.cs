using QuanLyNhaSach.Entities;
namespace QuanLyNhaSach.DataAccess
{
    public interface IReceiptBookModel
    {
        Task<List<ReceiptBook>> GetByReceipt(string receiptId);
    }
}
