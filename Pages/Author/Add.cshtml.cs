using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using QuanLyNhaSach.Processing;

namespace QuanLyNhaSach.Pages.Author
{
    public class AddModel : PageModel
    {
		private readonly IAuthor _Processing = (IAuthor)Injector.Injector.GetProcessing<AuthorProcessing>();
        private static string _referer = string.Empty;
        public string notify = string.Empty;

		[BindProperty]
		public string Name { get; set; }
		public void OnGet()
        {
            _referer = Request.Headers["Referer"].ToString();
		}

		public async Task OnPost()
		{
            try
            {
                await _Processing.Add(Name);
                if (_referer != string.Empty)
                {
                    Response.Redirect(_referer);
                }
                else
                {
                    Response.Redirect("/Author/View");
                }
            }
            catch (Exception ex)
            {
                notify = ex.Message;
            }
        }
	}
}




