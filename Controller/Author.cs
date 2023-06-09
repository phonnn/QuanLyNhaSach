using QuanLyNhaSach.Entities;
using System;

namespace QuanLyNhaSach.Controller
{
    public class AuthorController : Controller<Author>, IAuthor
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
				throw new Exception("Author is existed");
			}

			Author newType = new Author()
            {
                Name = name
            };

            await _model.AddAsync(newType);
        }
        public async Task Update(string id, string name="")
        {
			if (string.IsNullOrEmpty(name))
			{
				return;
			}
		
            Author found = SearchById(id);
			if (found == null)
            {
                throw new Exception("Author not found");
            }

			if (found.Name != name)
			{
				bool existed = isExisted("Name", name);
				if (existed)
				{
					throw new Exception("Author is existed");
				}
			}

			found.UpdatedAt = DateTime.Now;
            found.Name = name;
            await _model.UpdateAsync(id, found);
        }
		public List<Author> Search(string name = "")
		{
			List<Author> items = new List<Author>();
			if (!string.IsNullOrEmpty(name))
			{
				List<Author> found = _items.FindAll(x => x.Name == name);
			}

			return items;
		}
	}
}

