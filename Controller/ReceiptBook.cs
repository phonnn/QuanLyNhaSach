using QuanLyNhaSach.Entities;

namespace QuanLyNhaSach.Controller
{
    public class RecieptBookController : Controller<ReceiptBook>, IReceiptBook
    {
        public async Task Add(List<ReceiptBook> items)
        {
            if (items.Count == 0)
            {
                throw new Exception("Invalid input");
            }

            await _model.BatchAddAsync(items);
        }
        public async Task Update(string receiptId, List<ReceiptBook> newItems)
        {
            if (string.IsNullOrEmpty(receiptId) || newItems.Count == 0)
            {
                throw new Exception("Invalid input");
            }

            bool deleted = await DeleteByReceipt(receiptId);
            if (deleted)
            {
                await _model.BatchAddAsync(newItems);
            }
        }
        public List<ReceiptBook> Search(string receiptId)
        {
            if (string.IsNullOrEmpty(receiptId))
            {
                throw new Exception("Invalid input");
            }

            List<ReceiptBook> items = _items.FindAll(x => x.Receipt.Id.ToString() == receiptId);
            return items;
        }

        public async Task<bool> DeleteByReceipt(string receiptId)
        {
            bool deleted = false;
            List<ReceiptBook> items = Search(receiptId);
            if (items.Count != 0)
            {
                await _model.BatchDeleteAsync(items);
                deleted = true;
            }

            return deleted;
        }

    }
}

