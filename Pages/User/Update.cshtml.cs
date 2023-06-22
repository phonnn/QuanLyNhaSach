using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using QuanLyNhaSach.Processing;

namespace QuanLyNhaSach.Pages.User
{
    public class UpdateModel : PageModel
    {
        private readonly IBase<Entities.User> _Processing = (IBase<Entities.User>)Injector.Injector.GetProcessing<UserProcessing>();
        public string notify = string.Empty;
        public Entities.User user = new Entities.User();

        [BindProperty(SupportsGet = true)]
        public string ID { get; set; }

        [BindProperty]
        public string Name { get; set; }

        [BindProperty]
        public string Password { get; set; }

        [BindProperty]
        public string Status { get; set; }
		[BindProperty]
		public List<string> Roles { get; set; }
		public async Task OnGet()
        {
            user = await _Processing.SearchById(ID);
        }
        public async Task OnPost()
        {
            try
            {
                IUser _tempProcessing = (IUser)_Processing;
                await _tempProcessing.Update(ID, Name, Password, Status, Roles);
				user = await _Processing.SearchById(ID);
				Response.Redirect("/User/View");
            }
            catch (Exception ex)
            {
                user = await _Processing.SearchById(ID);
                notify = ex.Message;
            }
        }
    }
}
