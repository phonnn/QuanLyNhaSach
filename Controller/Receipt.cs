using QuanLyNhaSach.Entities;

namespace QuanLyNhaSach.Controller
{
    public class ReceiptController : Controller<Receipt>, IReceipt
    {
        protected readonly IBase<Book> _book = (IBase<Book>)Injector.Injector.GetController<BookController>();
        protected readonly IReceiptBook _receiptbook = (IReceiptBook)Injector.Injector.GetController<RecieptBookController>();
        public async Task<Receipt> Add(List<string> bookIds, List<int> prices)
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

            Receipt receipt = new Receipt();
            receipt = await _model.AddAsync(receipt);

            List<ReceiptBook> items = new List<ReceiptBook>();
            for (int i = 0; i < bookIds.Count; i++)
            {
                Book book = _book.SearchById(bookIds[i]);
                ReceiptBook newItem = new ReceiptBook()
                {
                    Receipt = receipt,
                    Book = book,
                    Price = prices[i]
                };
            }

            await _receiptbook.Add(items);
            return receipt;
        }
        
        public async Task Update(string receiptId, List<string> bookIds, List<int> prices)
        {
            Receipt receipt = SearchById(receiptId);
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

            List<ReceiptBook> items = new List<ReceiptBook>();
            for (int i = 0; i < bookIds.Count; i++)
            {
                Book book = _book.SearchById(bookIds[i]);
                ReceiptBook newItem = new ReceiptBook()
                {
                    Receipt = receipt,
                    Book = book,
                    Price = prices[i]
                };
            }

            await _receiptbook.Update(receiptId, items);
        }

        public override async Task<bool> Delete(string id)
        {
            bool deleted = false;
            Receipt foundReceipt = SearchById(id);
            if (foundReceipt == null)
            {
                throw new Exception("Receipt not found");
            }

            bool result = await _receiptbook.DeleteByReceipt(id);
            if (result)
            {
                await _model.DeleteAsync(foundReceipt);
                deleted = true;
            }

            return deleted;
        }
    }
}

