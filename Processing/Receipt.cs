using QuanLyNhaSach.DataAccess;
using QuanLyNhaSach.Entities;

namespace QuanLyNhaSach.Processing
{
    public class ReceiptProcessing : Processing<Receipt>, IReceipt
    {
        protected readonly IBase<Book> _book = (IBase<Book>)Injector.Injector.GetProcessing<BookProcessing>();
        protected readonly IReceiptBook _receiptbook = (IReceiptBook)Injector.Injector.GetProcessing<RecieptBookProcessing>();

        public async Task<List<Receipt>> GetAll()
        {
            return await _model.GetListAsync();
        }
        public async Task Add(Receipt receipt, List<string> bookIds, List<int> prices, List<int> amounts)
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
                if (receipt.SellReceipt != null)
                {
                    if (book.Quantity < amounts[i])
                    {
                        throw new Exception($"Insufficient amount book {book.Name}");
                    }
                }

                ReceiptBook newItem = new ReceiptBook()
                {
                    Receipt = receipt.Id,
                    Book = book.Id,
                    Price = prices[i],
                    Amount = amounts[i],
                    Total = prices[i] * amounts[i]
                };

                items.Add(newItem);
                if (receipt.SellReceipt != null)
                {
                    await _tempBook.SetQuantity(book.Id.ToString(), -newItem.Amount);
                } else
                {
                    await _tempBook.SetQuantity(book.Id.ToString(), newItem.Amount);
                }
            }

            await _receiptbook.Add(items);
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

			foreach (ReceiptBook item in receipt.ReceiptBooks)
			{
				await _tempBook.SetQuantity(item.Book.ToString(), -item.Amount);
			}

			List<ReceiptBook> items = new List<ReceiptBook>();
            for (int i = 0; i < bookIds.Count; i++)
            {
                Book book = await _book.SearchById(bookIds[i]);
                if (receipt.SellReceipt != null)
                {
                    if (book.Quantity < amounts[i])
                    {
                        throw new Exception($"Insufficient amount book {book.Name}");
                    }
                }

                ReceiptBook newItem = new ReceiptBook()
                {
                    Receipt = receipt.Id,
                    Book = book.Id,
                    Price = prices[i],
                    Amount = amounts[i],
                    Total = prices[i] * amounts[i]
                };

                items.Add(newItem);
                if (receipt.SellReceipt != null)
                {
                    await _tempBook.SetQuantity(book.Id.ToString(), -newItem.Amount);
                }
                else
                {
                    await _tempBook.SetQuantity(book.Id.ToString(), newItem.Amount);
                }
            }

			await _receiptbook.Update(receipt, items);
        }

        public override async Task<bool> Delete(string id)
        {
            bool deleted = false;
            Receipt foundReceipt = await _model.GetByIdAsync(id);
            if (foundReceipt == null)
            {
                throw new Exception("Receipt not found");
            }

            bool result = await _receiptbook.DeleteByReceipt(foundReceipt);
            if (result)
            {
                if (foundReceipt.SellReceipt != null)
                {
                    int total = foundReceipt.ReceiptBooks.Sum(item => item.Total);
                    ICustomer _tempCustomer = (ICustomer)Injector.Injector.GetProcessing<CustomerProcessing>();
                    await _tempCustomer.SetDebt(foundReceipt.SellReceipt.Customer.ToString(), -total);
                }

				IBook _tempBook = (IBook)_book;
				foreach (ReceiptBook item in foundReceipt.ReceiptBooks)
				{
					await _tempBook.SetQuantity(item.Book.ToString(), item.Amount);
				}

				await _model.DeleteAsync(foundReceipt);
                deleted = true;
            }

            return deleted;
        }
    }
}

