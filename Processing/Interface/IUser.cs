using QuanLyNhaSach.Entities;

namespace QuanLyNhaSach.Processing
{
    public interface IUser
    {
		Task Add(string name, string username, string password, List<string> roles);
        Task<User> Update(string id, string name, string password, string status, List<string> roles);
        Task<User> validateUser(string username, string password);
    }
}