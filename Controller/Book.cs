using QuanLyNhaSach.Entities;

namespace QuanLyNhaSach.Controller
{
    public class BookController : Controller<Book>, IBook
    {
        private readonly IBase<BookType> _booktype = (IBase<BookType>)Injector.Injector.GetController<BookTypeController>();
        private readonly IBase<Author> _author = (IBase<Author>)Injector.Injector.GetController<AuthorController>();

        public async Task Add(string name, string typeId, string authorId, int price)
        {
			if (string.IsNullOrEmpty(name)
                || string.IsNullOrEmpty(typeId)
                || string.IsNullOrEmpty(authorId)
                || price <= 0
			)
			{
				throw new Exception("Invalid input");
			}

			BookType booktype = _booktype.SearchById(typeId);
            if (booktype == null)
            {
                throw new Exception("Book type not found");
            }

            Author author = _author.SearchById(authorId);
            if (booktype == null)
            {
                throw new Exception("Author not found");
            }

            if (price <= 0)
            {
                throw new Exception("Invalid price");
            }

            Book newBook = new Book()
            {
                Name = name,
                Type = booktype,
                Author = author,
                Price = price
            };

            await _model.AddAsync(newBook);
        }
        public async Task Update(string id, string name="", string typeId="", string authorId="", int price=0)
        {
			if (string.IsNullOrEmpty(name)
				&& string.IsNullOrEmpty(typeId)
				&& string.IsNullOrEmpty(authorId)
				&& price == 0
				)
			{
				throw new Exception("Invalid input");
			}

			Book found = SearchById(id);
			if (found == null)
            {
                throw new Exception("Book not found");
            }

			found.UpdatedAt = DateTime.Now;
			if (!string.IsNullOrEmpty(name))
            {
				found.Name = name;
			}
            			
            if (!string.IsNullOrEmpty(typeId))
            {
				BookType booktype = _booktype.SearchById(typeId);
				if (booktype == null)
				{
					throw new Exception("Book type not found");
				}

				found.Type = booktype;
			}

            if (!string.IsNullOrEmpty(authorId))
            {
                Author author = _author.SearchById(authorId);
                if (author == null)
                {
                    throw new Exception("Author not found");
                }

				found.Author = author;
            }

            if (price > 0)
            {
				found.Price = price;
            }

            await _model.UpdateAsync(id, found);
        }
        public List<Book> Search(string name = "", string typeId = "", string authorId = "")
		{
			List<Book> items = new List<Book>();
            items.AddRange(_items);
            
            if (!string.IsNullOrEmpty(name))
            {
				List<Book> temp = _items.FindAll(x => x.Name == name);
				IEnumerable<Book> both = items.AsQueryable().Intersect(temp);
                if (both.Any())
                {
					items = both.ToList();
				}
			}

			if (!string.IsNullOrEmpty(typeId))
			{
				List<Book> temp = _items.FindAll(x => x.Type.Id.ToString() == typeId);
				IEnumerable<Book> both = items.AsQueryable().Intersect(temp);
				if (both.Any())
				{
					items = both.ToList();
				}
			}

			if (!string.IsNullOrEmpty(typeId))
			{
				List<Book> temp = _items.FindAll(x => x.Author.Id.ToString() == authorId);
                IEnumerable<Book> both = items.AsQueryable().Intersect(temp);
                if (both.Any())
                {
					items = both.ToList();
				}
			}

            return items;
        }
        public bool CheckAll(List<string> bookIds)
        {
            if (bookIds.Count == 0)
            {
                return true;
            }

            foreach(string bookId in bookIds)
            {
                bool check = isExisted("Id", bookId);
                if (!check)
                {
                    return false;
                }
            }

            return true;
        }
    }
}

