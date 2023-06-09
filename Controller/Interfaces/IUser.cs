namespace QuanLyNhaSach.Controller
{
    public interface IUser
    {
		Task Add(string name, string username, string password);
        Task Update(string id, string name, string username, string password, string status);
	}
}