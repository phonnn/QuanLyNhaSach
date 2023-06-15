using QuanLyNhaSach.Entities;

namespace QuanLyNhaSach.Processing
{
    public interface IReceipt
    {
        Task Add(Guid receiptId, List<string> bookIds, List<int> prices, List<int> amounts);
        Task Update(string receiptId, List<string> bookIds, List<int> prices, List<int> amounts);
    }
}