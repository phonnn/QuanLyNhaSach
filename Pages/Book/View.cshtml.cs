using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using QuanLyNhaSach.Processing;

namespace QuanLyNhaSach.Pages.Book
{
    public class ViewModel : PageModel
    {
        private readonly IBase<Entities.Book> _Processing = (IBase<Entities.Book>)Injector.Injector.GetProcessing<BookProcessing>();
        public List<Entities.Book> books = new List<Entities.Book>();
        public async Task OnGet()
        {
            books = await _Processing.GetAllAsync();
        }
    }
}