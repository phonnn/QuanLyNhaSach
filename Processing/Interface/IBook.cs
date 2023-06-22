using QuanLyNhaSach.Entities;

namespace QuanLyNhaSach.Processing
{
    public interface IBook
    {
		Task Add(string name, string typeId, string authorId, int price);
        Task<Book> Update(string id, string name, string typeId, string authorId, int price);
		List<Book> Search(string name, string typeId, string authorId);
		bool CheckAll(List<string> bookIds);
		Task SetQuantity(string id, int amount);
	}
}