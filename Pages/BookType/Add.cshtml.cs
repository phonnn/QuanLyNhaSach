using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using QuanLyNhaSach.Processing;

namespace QuanLyNhaSach.Pages.BookType
{
    public class AddModel : PageModel
    {
		private readonly IBookType _Processing = (IBookType)Injector.Injector.GetProcessing<BookTypeProcessing>();
		public string notify = string.Empty;
        private static string _referer = string.Empty;

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
                    Response.Redirect("/BookType/View");
                }
            }
            catch (Exception ex)
            {
                notify = ex.Message;
            }
        }
	}
}




