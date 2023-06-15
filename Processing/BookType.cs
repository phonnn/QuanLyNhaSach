using QuanLyNhaSach.Entities;

namespace QuanLyNhaSach.Processing
{
    public class BookTypeProcessing : Processing<BookType>, IBookType
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

			BookType foundType = await SearchById(id);
            if (foundType == null)
            {
                throw new Exception("Book type not found");
            }

			if (foundType.Name != name)
			{
				bool existed = isExisted("Name", name);
				if (existed)
				{
					throw new Exception("Book type is existed");
				}
			}

			foundType.UpdatedAt = DateTime.Now;
			foundType.Name = name;
            await _model.UpdateAsync(id, foundType);
			return foundType;
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

