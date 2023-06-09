using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using QuanLyNhaSach.Controller;

namespace QuanLyNhaSach.Pages.BookType
{
    public class ViewModel : PageModel
    {
        private readonly IBase<Entities.BookType> _controller = (IBase<Entities.BookType>)Injector.Injector.GetController<BookTypeController>();
        public List<Entities.BookType> types;

        [BindProperty]
        public string Key { get; set; }

        public async Task OnGet()
        {
            types = await _controller.GetAll();
            foreach (var type in types)
            {
                Console.WriteLine(type);
            }
        }
    }
}