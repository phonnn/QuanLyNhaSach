using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using QuanLyNhaSach.Processing;

namespace QuanLyNhaSach.Pages.Book
{
    public class AddModel : PageModel
    {
		private readonly IBook _Processing = (IBook)Injector.Injector.GetProcessing<BookProcessing>();
		public string notify = string.Empty;
		[BindProperty]
		public string Name { get; set; }

		[BindProperty]
		public string Btype { get; set; }

		[BindProperty]
		public string Author { get; set; }

		[BindProperty]
		public int Price { get; set; }
		public void OnGet()
        {

		}

		public async Task OnPost()
		{
            try
            {
				await _Processing.Add(Name, Btype, Author, Price);
				Response.Redirect("/Book/View");
			}
			catch (Exception ex)
            {
                notify = ex.Message;
            }
        }
	}
}




