using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using QuanLyNhaSach.Processing;

namespace QuanLyNhaSach.Pages.Receipt
{
    public class ViewModel : PageModel
    {
        private readonly IBase<Entities.Receipt> _Processing = (IBase<Entities.Receipt>)Injector.Injector.GetProcessing<ReceiptProcessing>();

        public List<Entities.Receipt> receipts = new List<Entities.Receipt>();
        public async Task OnGet()
        {
            receipts = await _Processing.GetAllAsync();
        }
    }
}