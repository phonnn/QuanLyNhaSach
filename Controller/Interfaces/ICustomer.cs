using QuanLyNhaSach.Entities;

namespace QuanLyNhaSach.Controller
{
    public interface ICustomer
    {
		Task Add(string name, string address, string email, string number);
        Task Update(string id, string name, string address, string email, string number, string debt);
		List<Customer> Search(string name, string address, string email, string number);
	}
}