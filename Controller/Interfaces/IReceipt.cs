using QuanLyNhaSach.Entities;

namespace QuanLyNhaSach.Controller
{
    public interface IReceipt
    {
        Task<Receipt> Add(List<string> bookIds, List<int> prices);
        Task Update(string receiptId, List<string> bookIds, List<int> prices);
    }
}