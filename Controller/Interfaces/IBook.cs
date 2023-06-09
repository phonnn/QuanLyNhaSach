using QuanLyNhaSach.Entities;

namespace QuanLyNhaSach.Controller
{
    public interface IBook
    {
		Task Add(string name, string typeId, string authorId, int price);
        Task Update(string id, string name, string typeId, string authorId, int price);
		List<Book> Search(string name, string typeId, string authorId);
		bool CheckAll(List<string> bookIds);
	}
}