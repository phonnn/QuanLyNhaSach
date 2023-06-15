using QuanLyNhaSach.Entities;

namespace QuanLyNhaSach.Processing
{
    public interface IBill
    {
		Task Add(string customerId, int amount);
        Task<Bill> Update(string id, string customerId, int amount);
		List<Bill> Search(string customerId);
	}
}