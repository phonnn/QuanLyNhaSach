using QuanLyNhaSach.Entities;

namespace QuanLyNhaSach.Processing
{
    public class UserProcessing : Processing<User>, IUser
	{
		public async Task Add(string name, string username, string password)
        {
			if (string.IsNullOrEmpty(name)
				|| string.IsNullOrEmpty(username)
				|| string.IsNullOrEmpty(password)
			)
			{
				throw new Exception("Invalid input");
			}
			
			bool existed = isExisted("Username", username);
			if (existed)
			{
				throw new Exception("Username is existed");
			}

			User newUser = new User()
            {
                Name = name,
				Username = username,
				Password = password,
				Status = 1
            };

            await _model.AddAsync(newUser);
        }
        public async Task<User> Update(string id, string name="", string password="", string status="")
        {
			if (string.IsNullOrEmpty(name)
				&& string.IsNullOrEmpty(password)
				&& string.IsNullOrEmpty(status)
			)
			{
				throw new Exception("Invalid input");
			}

			User found = await SearchById(id);
            if (found == null)
            {
                throw new Exception("Customer not found");
            }

			found.UpdatedAt = DateTime.Now;
			if (!string.IsNullOrEmpty(name))
			{
				found.Name = name;
			}

			if (!string.IsNullOrEmpty(password))
			{
				found.Password = password;
			}

			if (!string.IsNullOrEmpty(status))
			{
				int _status = int.Parse(status);
				if (_status != 0 && _status != 1) 
				{
					throw new Exception("Invalid status");
				}

				found.Status = _status;
			}

			await _model.UpdateAsync(id, found);
			return found;
        }

		public async Task<User> Authorize(string username, string password)
        {
			await GetAllAsync();
			User user = _items.Find(x => x.Username == username && x.Password == password);
			if (user == null)
            {
				throw new Exception("Invalid username or password");
			}

			return user;
		}
	}
}

