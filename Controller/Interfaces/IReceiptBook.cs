using QuanLyNhaSach.Entities;

namespace QuanLyNhaSach.Controller
{
    public interface IReceiptBook
    {
		Task Add(List<ReceiptBook> items);
        Task Update(string receiptId, List<ReceiptBook> newItems);
		List<ReceiptBook> Search(string receiptId);
	    Task<bool> DeleteByReceipt(string receiptId);
    }
}