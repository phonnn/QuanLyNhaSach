using QuanLyNhaSach.Entities;

namespace QuanLyNhaSach.Processing
{
    public class RecieptBookProcessing : Processing<ReceiptBook>, IReceiptBook
    {
        public async Task Add(List<ReceiptBook> items)
        {
            if (items.Count == 0)
            {
                throw new Exception("Invalid input");
            }

            await _model.BatchAddAsync(items);
        }
  
        public async Task Update(Receipt receipt, List<ReceiptBook> newItems)
        {
            if (receipt == null || newItems.Count == 0)
            {
                throw new Exception("Invalid input");
            }

            bool deleted = await DeleteByReceipt(receipt);
            if (deleted)
            {
                await _model.BatchAddAsync(newItems);
            }
        }

        public async Task<bool> DeleteByReceipt(Receipt receipt)
        {
            bool deleted = false;
            List<ReceiptBook> items = (List<ReceiptBook>)receipt.ReceiptBooks;
            if (items.Count != 0)
            {
                await _model.BatchDeleteAsync(items);
                deleted = true;
            }

            return deleted;
        }

    }
}

