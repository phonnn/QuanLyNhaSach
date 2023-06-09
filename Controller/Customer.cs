using QuanLyNhaSach.Entities;

namespace QuanLyNhaSach.Controller
{
    public class CustomerController : Controller<Customer>, ICustomer
	{
		public async Task Add(string name, string address, string email, string number)
        {
			if (string.IsNullOrEmpty(name)
				|| string.IsNullOrEmpty(address)
				|| string.IsNullOrEmpty(email)
				|| string.IsNullOrEmpty(number)
			)
			{
				throw new Exception("Invalid input");
			}

			bool existed = isExisted("Email", email);
			if (existed)
			{
				throw new Exception("Email is existed");
			}

			existed = isExisted("Number", number);
			if (existed)
			{
				throw new Exception("Number is existed");
			}

			Customer newCustomer = new Customer()
            {
                Name = name,
                Address = address,
                Email = email,
                Number = number,
                Debt = 0
            };

            await _model.AddAsync(newCustomer);
        }
        public async Task Update(string id, string name="", string address="", string email="", string number="", string debt="")
        {
			if (string.IsNullOrEmpty(name)
				&& string.IsNullOrEmpty(address)
				&& string.IsNullOrEmpty(email)
				&& string.IsNullOrEmpty(number)
				&& string.IsNullOrEmpty(debt)
			)
			{
				throw new Exception("Invalid input");
			}

			Customer found = SearchById(id);
            if (found == null)
            {
                throw new Exception("Customer not found");
            }

			found.UpdatedAt = DateTime.Now;
			if (!string.IsNullOrEmpty(name))
			{
				found.Name = name;
			}

			if (!string.IsNullOrEmpty(address))
			{
				found.Address = address;
			}

			if (!string.IsNullOrEmpty(email))
			{
				if (found.Email != email)
				{
					bool existed = isExisted("Email", email);
					if (existed)
					{
						throw new Exception("Email is existed");
					}
				}

				found.Email = email;
			}

			if (!string.IsNullOrEmpty(number))
			{
				if (found.Email != number)
				{
					bool existed = isExisted("Number", number);
					if (existed)
					{
						throw new Exception("Number is existed");
					}
				}

				found.Number = number;
			}

			if (!string.IsNullOrEmpty(debt))
			{
				found.Debt = int.Parse(debt);
			}

			await _model.UpdateAsync(id, found);
        }
        public List<Customer> Search(string name="", string address="", string email="", string number="")
		{
			List<Customer> items = new List<Customer>();
            items.AddRange(_items);
            
            if (!string.IsNullOrEmpty(name))
            {
				List<Customer> temp = _items.FindAll(x => x.Name == name);
				IEnumerable<Customer> both = items.AsQueryable().Intersect(temp);
                if (both.Any())
                {
					items = both.ToList();
				}
			}

			if (!string.IsNullOrEmpty(address))
			{
				List<Customer> temp = _items.FindAll(x => x.Address == address);
				IEnumerable<Customer> both = items.AsQueryable().Intersect(temp);
				if (both.Any())
				{
					items = both.ToList();
				}
			}

			if (!string.IsNullOrEmpty(email))
			{
				List<Customer> temp = _items.FindAll(x => x.Email == email);
                IEnumerable<Customer> both = items.AsQueryable().Intersect(temp);
                if (both.Any())
                {
					items = both.ToList();
				}
			}
			
			if (!string.IsNullOrEmpty(number))
			{
				List<Customer> temp = _items.FindAll(x => x.Number == number);
                IEnumerable<Customer> both = items.AsQueryable().Intersect(temp);
                if (both.Any())
                {
					items = both.ToList();
				}
			}

            return items;
        }
    }
}

