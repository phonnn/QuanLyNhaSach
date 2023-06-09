using QuanLyNhaSach.Entities;

namespace QuanLyNhaSach.Controller
{
    public interface IAuthor
    {
		Task Add(string name);
		Task Update(string id, string name);
		List<Author> Search(string name);
	}
}