using QuanLyNhaSach.Entities;

namespace QuanLyNhaSach.Controller
{
    public interface IBuyReceipt
    {
        Task<BuyReceipt> Add(User user, List<string> bookIds, List<int> prices);
        Task Update(string receiptId, User user, List<string> bookIds, List<int> prices);
    }
}