using Microsoft.AspNetCore.Authorization;
using QuanLyNhaSach.Entities;

namespace QuanLyNhaSach.Processing
{
    public class BookProcessing : Processing<Book>, IBook
    {
        private readonly IBase<BookType> _booktype = (IBase<BookType>)Injector.Injector.GetProcessing<BookTypeProcessing>();
        private readonly IBase<Author> _author = (IBase<Author>)Injector.Injector.GetProcessing<AuthorProcessing>();

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

			BookType booktype = await _booktype.SearchById(typeId);
            if (booktype == null)
            {
                throw new Exception("Book type not found");
            }

            Author author = await _author.SearchById(authorId);
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
                Type = booktype.Id,
                Author = author.Id,
                Price = price
            };

            await _model.AddAsync(newBook);
        }
        public async Task<Book> Update(string id, string name="", string typeId="", string authorId="", int price=0)
        {
			if (string.IsNullOrEmpty(name)
				&& string.IsNullOrEmpty(typeId)
				&& string.IsNullOrEmpty(authorId)
				&& price == 0
				)
			{
				throw new Exception("Invalid input");
			}

			Book foundBook = await SearchById(id);
			if (foundBook == null)
            {
                throw new Exception("Book not found");
            }

            foundBook.UpdatedAt = DateTime.Now;
			if (!string.IsNullOrEmpty(name))
            {
                foundBook.Name = name;
			}
            			
            if (!string.IsNullOrEmpty(typeId))
            {
				BookType booktype = await _booktype.SearchById(typeId);
				if (booktype == null)
				{
					throw new Exception("Book type not found");
				}

                foundBook.Type = booktype.Id;
			}

            if (!string.IsNullOrEmpty(authorId))
            {
                Author author = await _author.SearchById(authorId);
                if (author == null)
                {
                    throw new Exception("Author not found");
                }

                foundBook.Author = author.Id;
            }

            if (price > 0)
            {
                foundBook.Price = price;
            }

            await _model.UpdateAsync(id, foundBook);
            return foundBook;
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
				List<Book> temp = _items.FindAll(x => x.Type.ToString() == typeId);
				IEnumerable<Book> both = items.AsQueryable().Intersect(temp);
				if (both.Any())
				{
					items = both.ToList();
				}
			}

			if (!string.IsNullOrEmpty(typeId))
			{
				List<Book> temp = _items.FindAll(x => x.Author.ToString() == authorId);
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

		public async Task SetQuantity(string id, int amount)
		{
			Book foundBook = await SearchById(id);
			if (foundBook == null)
			{
				throw new Exception("Book not found");
			}

			if (amount == 0)
			{
				return;
			}

			foundBook.UpdatedAt = DateTime.Now;
			foundBook.Quantity += amount;
			await _model.UpdateAsync(id, foundBook);
		}
	}
}

