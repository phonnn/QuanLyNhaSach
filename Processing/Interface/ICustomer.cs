using QuanLyNhaSach.Entities;

namespace QuanLyNhaSach.Processing
{
    public interface ICustomer
    {
		Task Add(string name, string address, string email, string number);
        Task<Customer> Update(string id, string name, string address, string email, string number);
		List<Customer> Search(string name, string address, string email, string number);
		Task SetDebt (string id);
	}
}