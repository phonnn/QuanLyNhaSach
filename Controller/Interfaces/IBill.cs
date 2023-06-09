using QuanLyNhaSach.Entities;

namespace QuanLyNhaSach.Controller
{
    public interface IBill
    {
		Task Add(Customer customer, int amount);
        Task Update(string id, Customer customer, int amount);
		List<Bill> Search(string customerId);
	}
}