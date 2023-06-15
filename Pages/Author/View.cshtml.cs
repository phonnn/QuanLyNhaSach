using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using QuanLyNhaSach.Processing;

namespace QuanLyNhaSach.Pages.Author
{
    public class ViewModel : PageModel
    {
        private readonly IBase<Entities.Author> _Processing = (IBase<Entities.Author>)Injector.Injector.GetProcessing<AuthorProcessing>();
        public List<Entities.Author> authors = new List<Entities.Author>();
        public async Task OnGet()
        {
            authors = await _Processing.GetAllAsync();
        }
    }
}