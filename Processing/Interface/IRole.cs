using QuanLyNhaSach.Entities;

namespace QuanLyNhaSach.Processing
{
    public interface IRole
    {
		Task Add(string name);
		Task<Role> Update(string id, string name);
		List<Role> Search(string name);
	}
}