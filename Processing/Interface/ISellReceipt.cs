using QuanLyNhaSach.Entities;

namespace QuanLyNhaSach.Processing
{
    public interface ISellReceipt
    {
        Task<SellReceipt> SellAdd(string customerId, List<string> bookIds, List<int> prices, List<int> amounts);
        Task SellUpdate(string receiptId, string customerId, List<string> bookIds, List<int> prices, List<int> amounts);
		List<SellReceipt> SearchByCustomer(string customerId);
	}
}