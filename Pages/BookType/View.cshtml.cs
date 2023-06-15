using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using QuanLyNhaSach.Processing;

namespace QuanLyNhaSach.Pages.BookType
{
    public class ViewModel : PageModel
    {
        private readonly IBase<Entities.BookType> _Processing = (IBase<Entities.BookType>)Injector.Injector.GetProcessing<BookTypeProcessing>();
        public List<Entities.BookType> types = new List<Entities.BookType>();
        public async Task OnGet()
        {
            types = await _Processing.GetAllAsync();
        }
    }
}