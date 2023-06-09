using QuanLyNhaSach.Entities;

namespace QuanLyNhaSach.Controller
{
    public class BookTypeController : Controller<BookType>, IBookType
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
                throw new Exception("Book type is existed");
            }

            BookType newType = new BookType()
            {
                Name = name
            };

            await _model.AddAsync(newType);
        }
        public async Task<BookType> Update(string id, string name) 
        {
			if (string.IsNullOrEmpty(name))
			{
				throw new Exception("Invalid input");
			}

			BookType found = SearchById(id);
            if (found == null)
            {
                throw new Exception("Book type not found");
            }

			if (found.Name != name)
			{
				bool existed = isExisted("Name", name);
				if (existed)
				{
					throw new Exception("Book type is existed");
				}
			}

			found.UpdatedAt = DateTime.Now;
			found.Name = name;
            await _model.UpdateAsync(id, found);
			return found;
        }
		public List<BookType> Search(string name = "")
		{
			List<BookType> items = new List<BookType>();
			if (!string.IsNullOrEmpty(name))
			{
				List<BookType> found = _items.FindAll(x => x.Name == name);
			}

			return items;
		}
	}
}

