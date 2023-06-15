using QuanLyNhaSach.Entities;

namespace QuanLyNhaSach.Processing
{
    public interface IAuthor
    {
		Task Add(string name);
		Task<Author> Update(string id, string name);
		List<Author> Search(string name);
	}
}