using Microsoft.AspNetCore.Authorization;
using QuanLyNhaSach.Entities;
using System;

namespace QuanLyNhaSach.Processing
{
	public class RoleProcessing : Processing<Role>, IRole
    {
        public async Task Add(string name)
        {
			if (string.IsNullOrEmpty(name))
			{
				throw new Exception("Invalid input");
			}

			bool existed = isExisted("Name", name);
			if (existed)
			{
				throw new Exception("Role is existed");
			}

			Role newType = new Role()
            {
                Name = name
            };

            await _model.AddAsync(newType);
        }
        public async Task<Role> Update(string id, string name="")
        {
			if (string.IsNullOrEmpty(name))
			{
				throw new Exception("Invalid input");
			}

			Role found = await SearchById(id);
			if (found == null)
            {
                throw new Exception("Role not found");
            }

			if (found.Name != name)
			{
				bool existed = isExisted("Name", name);
				if (existed)
				{
					throw new Exception("Role is existed");
				}
			}

			found.UpdatedAt = DateTime.Now;
            found.Name = name;
            await _model.UpdateAsync(id, found);
			return found;
        }
		public List<Role> Search(string name = "")
		{
			List<Role> items = new List<Role>();
			if (!string.IsNullOrEmpty(name))
			{
				List<Role> found = _items.FindAll(x => x.Name == name);
			}

			return items;
		}
	}
}

