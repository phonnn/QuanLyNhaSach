using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using QuanLyNhaSach.Processing;

namespace QuanLyNhaSach.Pages.Book
{
    public class DeleteModel : PageModel
    {
        private readonly IBase<Entities.Book> _Processing = (IBase<Entities.Book>)Injector.Injector.GetProcessing<BookProcessing>();
        public string notify = string.Empty;
        public Entities.Book book = new Entities.Book();

        [BindProperty(SupportsGet = true)]
        public string ID { get; set; }

        public async Task OnGet()
        {
            book = await _Processing.SearchById(ID);
        }
        public async Task OnPost()
        {
            try
            {
                await _Processing.Delete(ID);
                Response.Redirect("/Book/View");
            }
            catch (Exception ex)
            {
                notify = ex.Message;
            }
        }

    }
}
