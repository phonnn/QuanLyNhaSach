using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using QuanLyNhaSach.Processing;

namespace QuanLyNhaSach.Pages.BookType
{
    public class UpdateModel : PageModel
    {
        private readonly IBase<Entities.BookType> _Processing = (IBase<Entities.BookType>)Injector.Injector.GetProcessing<BookTypeProcessing>();
        private static string _referer = string.Empty;
        public string notify = string.Empty;
        public Entities.BookType type = new Entities.BookType();

        [BindProperty(SupportsGet = true)]
        public string ID { get; set; }

        [BindProperty]
        public string Name { get; set; }

        public async Task OnGet()
        {
            _referer = Request.Headers["Referer"].ToString();
            type = await _Processing.SearchById(ID);
        }
        public async Task OnPost()
        {
            try
            {
                IBookType _tempProcessing = (IBookType)_Processing;
                type = await _tempProcessing.Update(ID, Name);
                Response.Redirect("/BookType/View");
            }
            catch (Exception ex)
            {
                type = await _Processing.SearchById(ID);
                notify = ex.Message;
            }
        }
    }
}
