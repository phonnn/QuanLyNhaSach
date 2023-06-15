using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using QuanLyNhaSach.Processing;

namespace QuanLyNhaSach.Pages.User
{
    public class DeleteModel : PageModel
    {
        private readonly IBase<Entities.User> _Processing = (IBase<Entities.User>)Injector.Injector.GetProcessing<UserProcessing>();
        public string notify = string.Empty;
        public Entities.User user = new Entities.User();

        [BindProperty(SupportsGet = true)]
        public string ID { get; set; }

        public async Task OnGet()
        {
            user = await _Processing.SearchById(ID);
        }
        public async Task OnPost()
        {
            try
            {
                await _Processing.Delete(ID);
                Response.Redirect("/User/View");
            }
            catch (Exception ex)
            {
                notify = ex.Message;
            }
        }

    }
}
