using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using QuanLyNhaSach.Processing;

namespace QuanLyNhaSach.Pages.Bill
{
    public class ViewModel : PageModel
    {
        private readonly IBase<Entities.Bill> _Processing = (IBase<Entities.Bill>)Injector.Injector.GetProcessing<BillProcessing>();
        public List<Entities.Bill> bills = new List<Entities.Bill>();
        public async Task OnGet()
        {
            bills = await _Processing.GetAllAsync();
        }
    }
}