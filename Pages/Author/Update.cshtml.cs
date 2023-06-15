using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using QuanLyNhaSach.Processing;

namespace QuanLyNhaSach.Pages.Author
{
    public class UpdateModel : PageModel
    {
        private readonly IBase<Entities.Author> _Processing = (IBase<Entities.Author>)Injector.Injector.GetProcessing<AuthorProcessing>();
        public string notify = string.Empty;
        public Entities.Author author = new Entities.Author();

        [BindProperty(SupportsGet = true)]
        public string ID { get; set; }

        [BindProperty]
        public string Name { get; set; }

        public async Task OnGet()
        {
            author = await _Processing.SearchById(ID);
        }
        public async Task OnPost()
        {
            try
            {
                IAuthor _tempProcessing = (IAuthor)_Processing;
                author = await _tempProcessing.Update(ID, Name);
                Response.Redirect("/Author/View");
            }
            catch (Exception ex)
            {
                author = await _Processing.SearchById(ID);
                notify = ex.Message;
            }
        }
    }
}
