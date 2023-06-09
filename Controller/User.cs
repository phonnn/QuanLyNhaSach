using QuanLyNhaSach.Entities;

namespace QuanLyNhaSach.Controller
{
    public class UserController : Controller<User>, IUser
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
				Password = password
            };

            await _model.AddAsync(newUser);
        }
        public async Task Update(string id, string name="", string username="", string password="", string status="")
        {
			if (string.IsNullOrEmpty(name)
				&& string.IsNullOrEmpty(username)
				&& string.IsNullOrEmpty(password)
				&& string.IsNullOrEmpty(status)
			)
			{
				throw new Exception("Invalid input");
			}

			User found = SearchById(id);
            if (found == null)
            {
                throw new Exception("Customer not found");
            }

			found.UpdatedAt = DateTime.Now;
			if (!string.IsNullOrEmpty(name))
			{
				found.Name = name;
			}

			if (!string.IsNullOrEmpty(username))
			{
				if (found.Username != username)
				{
					bool existed = isExisted("Username", username);
					if (existed)
					{
						throw new Exception("Author is existed");
					}
				}

				found.Username = username;
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
					throw new Exception("invalid status");
				}

				found.Status = _status;
			}

			await _model.UpdateAsync(id, found);
        }
    }
}

