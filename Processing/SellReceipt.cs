using QuanLyNhaSach.Entities;
using QuanLyNhaSach.DataAccess;

namespace QuanLyNhaSach.Processing
{
    public class SellProcessing : ReceiptProcessing<SellReceipt>, ISellReceipt
    {
        private readonly IBase<Customer> _customer = (IBase<Customer>)Injector.Injector.GetProcessing<CustomerProcessing>();
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

            SellReceipt newReceipt = new SellReceipt()
            {
                Customer = customer.Id
		    };

            SellReceipt result = await _model.AddAsync(newReceipt);
            await Add(newReceipt.Id, bookIds, prices, amounts);

            ICustomer _tempCustomer = (ICustomer)_customer;
            await _tempCustomer.SetDebt(customerId);
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

            SellReceipt foundReceipt = await SearchById(receiptId);
            if (foundReceipt == null)
            {
                throw new Exception("Receipt not found");
            }

            Customer oldCustomer = foundReceipt.CustomerNavigation;
            Customer newCustomer = null;
            if (customerId != "")
            {
                newCustomer = await _customer.SearchById(customerId);
                if (newCustomer == null)
                {
                    throw new Exception("Customer not found");
                }

                foundReceipt.Customer = newCustomer.Id;
                foundReceipt.UpdatedAt = DateTime.Now;
                await _model.UpdateAsync(receiptId, foundReceipt);
            }
           
            if (bookIds.Count != 0 && bookIds.Count == prices.Count)
            {
                await Update(receiptId, bookIds, prices, amounts);
            }

            ICustomer _tempCustomer = (ICustomer)_customer;
            await _tempCustomer.SetDebt(oldCustomer.Id.ToString());

            if (newCustomer != null)
            {
                await _tempCustomer.SetDebt(newCustomer.Id.ToString());
            }
        }
        public List<SellReceipt> SearchByCustomer(string customerId)
        {
			List<SellReceipt> items = _items.FindAll(x => x.Customer.ToString() == customerId);
			return items;
		}
	}
}

