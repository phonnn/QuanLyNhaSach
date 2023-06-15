using QuanLyNhaSach.DataAccess;
using QuanLyNhaSach.Entities;

namespace QuanLyNhaSach.Processing
{
    public class ReceiptProcessing<TReceipt> : Processing<TReceipt> where TReceipt : Receipt, new()
    {
        protected readonly IBase<Book> _book = (IBase<Book>)Injector.Injector.GetProcessing<BookProcessing>();
        protected readonly IReceiptBook _receiptbook = (IReceiptBook)Injector.Injector.GetProcessing<RecieptBookProcessing>();
        protected IModel<Receipt> _receipt = (IModel<Receipt>)Injector.Injector.GetModel<Receipt>();

        public async Task Add(Guid receiptId, List<string> bookIds, List<int> prices, List<int> amounts)
        {
            IBook _tempBook = (IBook)_book;
            bool check = _tempBook.CheckAll(bookIds);
            if (!check)
            {
                throw new Exception("Some books not found");
            }

            check = prices.TrueForAll(price => price > 0);
            if (!check)
            {
                throw new Exception("Invalid price");
            }

            check = amounts.TrueForAll(amount => amount > 0);
            if (!check)
            {
                throw new Exception("Invalid amount");
            }

            List<ReceiptBook> items = new List<ReceiptBook>();
            for (int i = 0; i < bookIds.Count; i++)
            {
                Book book = await _book.SearchById(bookIds[i]);
                ReceiptBook newItem = new ReceiptBook()
                {
                    Receipt = receiptId,
                    Book = book.Id,
                    Price = prices[i],
                    Amount = amounts[i],
                    Total = prices[i] * amounts[i]
                };

                items.Add(newItem);
            }

            await _receiptbook.Add(items);
            Receipt test = await _receipt.GetByIdAsync(receiptId.ToString());
            return;
        }

        public async Task Update(string receiptId, List<string> bookIds, List<int> prices, List<int> amounts)
        {
            Receipt receipt = await SearchById(receiptId);
            if (receipt == null)
            {
                throw new Exception("Receipt not found");
            }

            IBook _tempBook = (IBook)_book;
            bool check = _tempBook.CheckAll(bookIds);
            if (!check)
            {
                throw new Exception("Some books not found");
            }

            check = prices.TrueForAll(price => price > 0);
            if (!check)
            {
                throw new Exception("Invalid price");
            }

            check = amounts.TrueForAll(amount => amount > 0);
            if (!check)
            {
                throw new Exception("Invalid amount");
            }

            List<ReceiptBook> items = new List<ReceiptBook>();
            for (int i = 0; i < bookIds.Count; i++)
            {
                Book book = await _book.SearchById(bookIds[i]);
                ReceiptBook newItem = new ReceiptBook()
                {
                    Receipt = receipt.Id,
                    Book = book.Id,
                    Price = prices[i],
                    Amount = amounts[i],
                    Total = prices[i] * amounts[i]
                };
            }

            await _receiptbook.Update(receipt, items);
        }

        public override async Task<bool> Delete(string id)
        {
            bool deleted = false;
            Receipt foundReceipt = await _receipt.GetByIdAsync(id);
            if (foundReceipt == null)
            {
                throw new Exception("Receipt not found");
            }

            bool result = await _receiptbook.DeleteByReceipt(foundReceipt);
            if (result)
            {
                await _receipt.DeleteAsync(foundReceipt);
                deleted = true;
            }

            ICustomer _tempCustomer = (ICustomer)Injector.Injector.GetProcessing<CustomerProcessing>();
            if (foundReceipt.SellReceipt != null)
            {
                await _tempCustomer.SetDebt(foundReceipt.SellReceipt.Customer.ToString());
            }

            return deleted;
        }
    }
}

