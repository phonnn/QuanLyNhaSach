using QuanLyNhaSach.Entities;

namespace QuanLyNhaSach.Processing
{
    public interface IBuyReceipt
    {
        Task<BuyReceipt> BuyAdd(string userId, List<string> bookIds, List<int> prices, List<int> amounts);
        Task BuyUpdate(string receiptId, string userId, List<string> bookIds, List<int> prices, List<int> amounts);
		List<BuyReceipt> SearchByUser(string userId);
	}
}