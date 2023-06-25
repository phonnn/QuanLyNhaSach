using QuanLyNhaSach.Entities;

namespace QuanLyNhaSach.Processing
{
    public interface IParameter
	{
        Task<Parameter> Update(string id, string value, string status);
		Task<Parameter> Search(string name);
	}
}