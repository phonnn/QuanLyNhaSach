using QuanLyNhaSach.Entities;
using QuanLyNhaSach.DataAccess;

namespace QuanLyNhaSach.Processing
{
    public class SellProcessing : ReceiptProcessing, ISellReceipt
    {
        private readonly IBase<Customer> _customer = (IBase<Customer>)Injector.Injector.GetProcessing<CustomerProcessing>();
		private readonly IModel<SellReceipt> _receipt = (IModel<SellReceipt>)Injector.Injector.GetModel<SellReceipt>();
        private readonly IParameter _parameter = (IParameter)Injector.Injector.GetProcessing<ParameterProcessing>();

        public async Task<SellReceipt> SellAdd(string customerId, List<string> bookIds, List<int> prices, List<int> amounts)
        {
            if (string.IsNullOrEmpty(customerId) || bookIds.Count == 0 
                || bookIds.Count != prices.Count || bookIds.Count != amounts.Count
			)
            {
                throw new Exception("Invalid input");
            }

			Customer customer = await _customer.SearchById(customerId);
			if (customer == null)
			{
				throw new Exception("Customer not found");
			}

            Parameter maxDebt = await _parameter.Search("DuNoToiDa");
            if (customer.Debt > int.Parse(maxDebt.Value))
            {
                throw new Exception("Credit limit reached");
            }

            SellReceipt newReceipt = new SellReceipt()
            {
                Customer = customer.Id
		    };

            Parameter minQuantity = await _parameter.Search("TonKhoToiThieu");
            for (int i = 0; i < bookIds.Count; i++)
            {
                Book book = await _book.SearchById(bookIds[i]);
                if (book.Quantity < amounts[i])
                {
                    throw new Exception($"Insufficient amount book {book.Name}");
                }

                if (book.Quantity - amounts[i] < int.Parse(minQuantity.Value))
                {
                    throw new Exception($"{book.Name} out of stock");
                }
            }

            SellReceipt result = await _receipt.AddAsync(newReceipt);
			await Add(newReceipt, bookIds, prices, amounts);

			int total = 0;
            for (int i = 0; i < bookIds.Count; i++)
            {
                total += (prices[i] * amounts[i]);
            }

            ICustomer _tempCustomer = (ICustomer)_customer;
            await _tempCustomer.SetDebt(customerId, total);
            return result;
        }
        public async Task SellUpdate(string receiptId, string customerId="", List<string> bookIds=null, List<int> prices=null, List<int> amounts=null)
        {
            bookIds ??= new List<string>();
            prices ??= new List<int>();
            if (string.IsNullOrEmpty(receiptId) && string.IsNullOrEmpty(customerId)
				&& (bookIds.Count == 0 || bookIds.Count != prices.Count || bookIds.Count != amounts.Count)
            )
            {
                throw new Exception("Invalid input");
            }

            Receipt foundReceipt = await SearchById(receiptId);
            if (foundReceipt == null)
            {
                throw new Exception("Receipt not found");
            }

            if (foundReceipt.SellReceipt == null)
            {
                throw new Exception("Receipt not found");
            }

            int oldTotal = 0;
            int newTotal = 0;
            if (bookIds.Count != 0 && bookIds.Count == prices.Count)
            {
                oldTotal = foundReceipt.ReceiptBooks.Sum(item => item.Total);
                for (int i = 0; i < bookIds.Count; i++)
                {
                    if (!string.IsNullOrEmpty(bookIds[i]))
                    {
                        Book book = await _book.SearchById(bookIds[i]);
                        if (book.Quantity < amounts[i])
                        {
                            throw new Exception($"Insufficient amount book {book.Name}");
                        }

                        newTotal += (prices[i] * amounts[i]);
                    }
                }

                await Update(receiptId, bookIds, prices, amounts);
            }

            Customer oldCustomer = foundReceipt.SellReceipt.CustomerNavigation;
            Customer newCustomer = null;
            foundReceipt = await SearchById(receiptId);
            if (customerId != "" && customerId != oldCustomer.Id.ToString())
            {
                newCustomer = await _customer.SearchById(customerId);
                if (newCustomer == null)
                {
                    throw new Exception("Customer not found");
                }

                foundReceipt.SellReceipt.Customer = newCustomer.Id;
                foundReceipt.UpdatedAt = DateTime.Now;
                await _receipt.UpdateAsync(receiptId, foundReceipt.SellReceipt);
            }

            ICustomer _tempCustomer = (ICustomer)_customer;
            if (newCustomer != null)
            {
                await _tempCustomer.SetDebt(newCustomer.Id.ToString(), newTotal);
                await _tempCustomer.SetDebt(oldCustomer.Id.ToString(), -oldTotal);
            } else
            {
                await _tempCustomer.SetDebt(oldCustomer.Id.ToString(), newTotal - oldTotal);
            }
        }
        public List<Receipt> SearchByCustomer(string customerId)
        {
			List<Receipt> items = _items.FindAll(x => x.SellReceipt.Customer.ToString() == customerId);
			return items;
		}
        public async Task<SellReceipt> SearchById(string id)
        {
            SellReceipt item = await _receipt.GetByIdAsync(id);
            return item;
        }
    }
}

