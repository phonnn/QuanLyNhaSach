using Microsoft.AspNet.Identity;
using Microsoft.AspNetCore.Authorization;
using QuanLyNhaSach.Entities;
using System.Security.Claims;

namespace QuanLyNhaSach.Processing
{
	public class UserProcessing : Processing<User>, IUser
	{
		private readonly IUserRole _userRole = (IUserRole)Injector.Injector.GetProcessing<UserRoleProcessing>();
		public async Task Add(string name, string username, string password, List<string> roles)
        {
			if (string.IsNullOrEmpty(name)
				|| string.IsNullOrEmpty(username)
				|| string.IsNullOrEmpty(password)
				|| roles.Count == 0
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
			await _userRole.Add(newUser, roles);
        }
        public async Task<User> Update(string id, string name="", string password="", string status="", List<string> roles=null)
        {
			if (string.IsNullOrEmpty(name)
				&& string.IsNullOrEmpty(password)
				&& string.IsNullOrEmpty(status)
				&& roles == null
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
			await _userRole.Update(found, roles);
			return found;
        }

		public async Task<User> validateUser(string username, string password)
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