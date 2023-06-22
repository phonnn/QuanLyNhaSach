using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using QuanLyNhaSach.Processing;

namespace QuanLyNhaSach.Pages.User
{
    public class AddModel : PageModel
    {
		private readonly IUser _Processing = (IUser)Injector.Injector.GetProcessing<UserProcessing>();
		public string notify = string.Empty;

		[BindProperty]
		public string Name { get; set; }
		[BindProperty]
		public string Username { get; set; }
		[BindProperty]
		public string Password { get; set; }
		[BindProperty]
		public List<string> Roles { get; set; }
        public void OnGet()
        {

		}

		public async Task OnPost()
		{
            try
            {
                await _Processing.Add(Name, Username, Password, Roles);
				Response.Redirect("/User/View");
			}
            catch (Exception ex)
            {
                notify = ex.Message;
            }
        }
	}
}




