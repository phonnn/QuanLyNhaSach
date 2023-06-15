using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using QuanLyNhaSach.Processing;

namespace QuanLyNhaSach.Pages.Author
{
    public class DeleteModel : PageModel
    {
        private readonly IBase<Entities.Author> _Processing = (IBase<Entities.Author>)Injector.Injector.GetProcessing<AuthorProcessing>();
        public string notify = string.Empty;
        public Entities.Author author = new Entities.Author();

        [BindProperty(SupportsGet = true)]
        public string ID { get; set; }

        public async Task OnGet()
        {
            author = await _Processing.SearchById(ID);
        }
        public async Task OnPost()
        {
            try
            {
                await _Processing.Delete(ID);
                Response.Redirect("/Author/View");
            } catch (Exception ex)
            {
                notify = ex.Message;
            }
        }

    }
}
