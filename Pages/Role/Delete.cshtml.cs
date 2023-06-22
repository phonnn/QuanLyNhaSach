using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using QuanLyNhaSach.Processing;

namespace QuanLyNhaSach.Pages.Role
{
    public class DeleteModel : PageModel
    {
        private readonly IBase<Entities.Role> _Processing = (IBase<Entities.Role>)Injector.Injector.GetProcessing<RoleProcessing>();
        public string notify = string.Empty;
        public Entities.Role role = new Entities.Role();

        [BindProperty(SupportsGet = true)]
        public string ID { get; set; }

        public async Task OnGet()
        {
            role = await _Processing.SearchById(ID);
        }
        public async Task OnPost()
        {
            try
            {
                await _Processing.Delete(ID);
                Response.Redirect("/Role/View");
            } catch (Exception ex)
            {
                notify = ex.Message;
            }
        }

    }
}
