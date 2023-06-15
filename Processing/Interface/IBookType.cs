using QuanLyNhaSach.Entities;

namespace QuanLyNhaSach.Processing
{
    public interface IBookType
    {
		Task Add(string name);
        Task<BookType> Update(string id, string name);
		List<BookType> Search(string name);
	}
}