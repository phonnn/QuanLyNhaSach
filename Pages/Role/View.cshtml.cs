using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using QuanLyNhaSach.Processing;

namespace QuanLyNhaSach.Pages.Role
{
    public class ViewModel : PageModel
    {
        private readonly IBase<Entities.Role> _Processing = (IBase<Entities.Role>)Injector.Injector.GetProcessing<RoleProcessing>();
        public List<Entities.Role> roles = new List<Entities.Role>();
        public string notify = string.Empty;

        [BindProperty]
        public string Role { get; set; }

        public async Task OnGet()
        {
            roles = await _Processing.GetAllAsync();
        }

        public async Task OnPost()
        {
            try
            {
                IRole _tempProcessing = (IRole)_Processing;
                await _tempProcessing.Add(Role);
                Response.Redirect("/Role/View");
            }
            catch (Exception ex)
            {
                notify = ex.Message;
            }
        }
    }
}