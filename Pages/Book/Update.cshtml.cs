using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using QuanLyNhaSach.Processing;

namespace QuanLyNhaSach.Pages.Book
{
    public class UpdateModel : PageModel
    {
        private readonly IBase<Entities.Book> _Processing = (IBase<Entities.Book>)Injector.Injector.GetProcessing<BookProcessing>();
        public string notify = string.Empty;
        public Entities.Book book = new Entities.Book();

        [BindProperty(SupportsGet = true)]
        public string ID { get; set; }

        [BindProperty]
        public string Name { get; set; }

        [BindProperty]
        public string Btype { get; set; }

        [BindProperty]
        public string Author { get; set; }

        [BindProperty]
        public int Price { get; set; }

        public async Task OnGet()
        {
            book = await _Processing.SearchById(ID);
        }
        public async Task OnPost()
        {
            try
            {
                IBook _tempProcessing = (IBook)_Processing;
                book = await _tempProcessing.Update(ID, Name, Btype, Author, Price);
                Response.Redirect("/Book/View");
            }
            catch (Exception ex)
            {
                book = await _Processing.SearchById(ID);
                notify = ex.Message;
            }
        }
    }
}
