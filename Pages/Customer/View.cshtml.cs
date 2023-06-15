using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using QuanLyNhaSach.Processing;

namespace QuanLyNhaSach.Pages.Customer
{
    public class ViewModel : PageModel
    {
        private readonly IBase<Entities.Customer> _Processing = (IBase<Entities.Customer>)Injector.Injector.GetProcessing<CustomerProcessing>();
        public List<Entities.Customer> customers = new List<Entities.Customer>();
        public async Task OnGet()
        {
            customers = await _Processing.GetAllAsync();
        }
    }
}