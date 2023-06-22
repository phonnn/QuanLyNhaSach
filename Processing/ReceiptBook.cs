using QuanLyNhaSach.Entities;
using QuanLyNhaSach.DataAccess;
using Microsoft.AspNetCore.Authorization;

namespace QuanLyNhaSach.Processing
{
    public class RecieptBookProcessing : Processing<ReceiptBook>, IReceiptBook
    {
        protected IModel<ReceiptBook> _model = (IModel<ReceiptBook>)Injector.Injector.SetModel<ReceiptBook>(new ReceiptBookModel());
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
            List<ReceiptBook> items = (List<ReceiptBook>)receipt.ReceiptBooks;
            if (items.Count != 0)
            {
                return true;
            }

            await _model.BatchDeleteAsync(items);
            return true;
        }

        public async Task<List<ReceiptBook>> GetByReceipt(string receiptId)
        {
            IReceiptBookModel _tempModel = (IReceiptBookModel)_model;          
            List<ReceiptBook> items = await _tempModel.GetByReceipt(receiptId);
            return items;
        }
    }
}

