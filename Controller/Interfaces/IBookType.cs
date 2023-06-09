using QuanLyNhaSach.Entities;

namespace QuanLyNhaSach.Controller
{
    public interface IBookType
    {
		Task Add(string name);
        Task<BookType> Update(string id, string name);
		List<BookType> Search(string name);
	}
}