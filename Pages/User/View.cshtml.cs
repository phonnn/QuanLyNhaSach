using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using QuanLyNhaSach.Processing;

namespace QuanLyNhaSach.Pages.User
{
    public class ViewModel : PageModel
    {
        private readonly IBase<Entities.User> _Processing = (IBase<Entities.User>)Injector.Injector.GetProcessing<UserProcessing>();
        public List<Entities.User> users = new List<Entities.User>();
        public async Task OnGet()
        {
            users = await _Processing.GetAllAsync();
        }
    }
}