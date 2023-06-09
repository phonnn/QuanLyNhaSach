using QuanLyNhaSach.Entities;

namespace QuanLyNhaSach.Controller
{
    public class BillController : Controller<Bill>, IBill
	{
		private readonly IBase<Customer> _customer = (IBase<Customer>)Injector.Injector.GetController<CustomerController>();
		public async Task Add(Customer customer, int amount)
        {
			if (customer == null || amount <= 0)
			{
				throw new Exception("Invalid input");
			}

			Bill newBill = new Bill()
            {
                Customer = customer,
				Amount = amount
            };

            await _model.AddAsync(newBill);
        }
        public async Task Update(string id, Customer customer=null, int amount=0)
        {
			bool updated = false;
			if (string.IsNullOrEmpty(id) && (customer == null || amount <= 0))
			{
				throw new Exception("Invalid input");
			}

			Bill foundBill = SearchById(id);
            if (foundBill == null)
            {
                throw new Exception("Bill not found");
            }

			if (customer != null)
            {
				Customer oldCustomer = foundBill.Customer;
				foundBill.Customer = customer;
				updated = true;
				////////////
				//do sth update with both customer
				///
			}

			if (amount > 0)
            {
				foundBill.Amount = amount;
				updated = true;
			}

			if (updated)
            {
				foundBill.UpdatedAt = DateTime.Now;
			}

			await _model.UpdateAsync(id, foundBill);
        }
		public List<Bill> Search(string customerId)
		{
			List<Bill> items = _items.FindAll(x => x.Customer.Id.ToString() == customerId);
			return items;
		}
	}
}

