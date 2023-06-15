using QuanLyNhaSach.Entities;

namespace QuanLyNhaSach.Processing
{
    public interface IUser
    {
		Task Add(string name, string username, string password);
        Task<User> Update(string id, string name, string password, string status);
	}
}