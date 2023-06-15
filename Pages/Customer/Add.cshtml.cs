using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using QuanLyNhaSach.Processing;

namespace QuanLyNhaSach.Pages.Customer
{
    public class AddModel : PageModel
    {
		private readonly ICustomer _Processing = (ICustomer)Injector.Injector.GetProcessing<CustomerProcessing>();
		private static string _referer = string.Empty;
		public string notify = string.Empty;

		[BindProperty]
		public string Name { get; set; }
		[BindProperty]
		public string Address { get; set; }
		[BindProperty]
		public string Number { get; set; }
		[BindProperty]
		public string Email { get; set; }
		public void OnGet()
        {
			_referer = Request.Headers["Referer"].ToString();
		}

		public async Task OnPost()
		{
			try
            {
				await _Processing.Add(Name, Address, Email, Number);
				if (_referer != string.Empty)
				{
					Response.Redirect(_referer);
				}
				else
				{
					Response.Redirect("/Customer/View");
				}
			}
			catch (Exception ex)
			{
				notify = ex.Message;
			}
		}
	}
}




