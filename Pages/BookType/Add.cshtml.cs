using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using QuanLyNhaSach.Controller;

namespace QuanLyNhaSach.Pages.BookType
{
    public class AddModel : PageModel
    {
		private readonly IBookType _controller = (IBookType)Injector.Injector.GetController<BookTypeController>();
		public string notify;

		[BindProperty]
		public string Name { get; set; }
		public void OnGet()
        {
			notify = String.Empty;
		}

		public async Task OnPost()
		{
            try
            {
                await _controller.Add(Name);
                notify = "Đã thêm loại sách";
                Response.Redirect("/BookType/View");
            }
            catch (Exception ex)
            {
                notify = ex.Message;
            }
        }
	}
}




