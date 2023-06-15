using QuanLyNhaSach.Entities;

namespace QuanLyNhaSach.Processing
{
    public interface IReceiptBook
    {
		Task Add(List<ReceiptBook> items);
        Task Update(Receipt receipt, List<ReceiptBook> newItems);
	    Task<bool> DeleteByReceipt(Receipt receipt);
    }
}