using QuanLyNhaSach.Entities;

namespace QuanLyNhaSach.Processing
{
    public interface IUserRole
    {
		Task Add(User user, List<string> roleIds);
		Task Update(User user, List<string> roleIds);
		Task DeleteByUser(User user);
	}
}