using Microsoft.AspNetCore.Authorization;
using QuanLyNhaSach.Entities;

namespace QuanLyNhaSach.Processing
{
	public class BillProcessing : Processing<Bill>, IBill
	{
		private readonly IBase<Customer> _customer = (IBase<Customer>)Injector.Injector.GetProcessing<CustomerProcessing>();
		public async Task Add(string customerId, int amount)
        {
			if (string.IsNullOrEmpty(customerId) || amount <= 0)
			{
				throw new Exception("Invalid input");
			}

			Customer customer = await _customer.SearchById(customerId);
			if (customer == null)
            {
				throw new Exception("Customer not found");
			}
			
			if (amount > customer.Debt)
            {
				throw new Exception($"Amount must be less than {customer.Debt}");
			}

			Bill newBill = new Bill()
            {
                Customer = customer.Id,
				Amount = amount
            };

			await _model.AddAsync(newBill);
			ICustomer _tempCustomer = (ICustomer)_customer;
			await _tempCustomer.SetDebt(customerId, -amount);
		}
		public async Task<Bill> Update(string id, string customerId="", int amount=0)
        {
			bool updated = false;
			if (string.IsNullOrEmpty(id) && (string.IsNullOrEmpty(customerId) == null || amount <= 0))
			{
				throw new Exception("Invalid input");
			}

			Bill foundBill = await SearchById(id);
            if (foundBill == null)
            {
                throw new Exception("Bill not found");
            }

			Customer oldCustomer = foundBill.CustomerNavigation;
			int oldAmount = foundBill.Amount;
			if (amount > 0)
			{
				foundBill.Amount = amount;
				updated = true;
			}

			Customer newCustomer = null;
			if (customerId != "" && customerId != foundBill.Customer.ToString())
            {
				newCustomer = await _customer.SearchById(customerId);
				if (newCustomer == null)
                {
					throw new Exception("Customer not found");
				}

				foundBill.Customer = newCustomer.Id;
				updated = true;
			}

			if (updated)
            {
				foundBill.UpdatedAt = DateTime.Now;
			}

			ICustomer _tempCustomer = (ICustomer)_customer;
			if (newCustomer != null)
            {
				await _tempCustomer.SetDebt(newCustomer.Id.ToString(), -foundBill.Amount);
				await _tempCustomer.SetDebt(oldCustomer.Id.ToString(), oldAmount);
			} else
            {
				await _tempCustomer.SetDebt(oldCustomer.Id.ToString(), oldAmount - foundBill.Amount);
			}

			await _model.UpdateAsync(id, foundBill);
			return foundBill;
        }
		public List<Bill> Search(string customerId)
		{
			List<Bill> items = _items.FindAll(x => x.Customer.ToString() == customerId);
			return items;
		}
		public override async Task<bool> Delete(string id)
		{
			bool deleted = false;
			Bill foundBill = await _model.GetByIdAsync(id);
			if (foundBill != null)
			{
				ICustomer _tempCustomer = (ICustomer)_customer;
				await _tempCustomer.SetDebt(foundBill.Customer.ToString(), foundBill.Amount);

				await _model.DeleteAsync(foundBill);
				_items = await _model.GetListAsync();
				deleted = true;
			}

			return deleted;
		}
	}
}

