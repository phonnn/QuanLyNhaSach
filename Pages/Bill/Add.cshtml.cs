using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using QuanLyNhaSach.Processing;

namespace QuanLyNhaSach.Pages.Bill
{
    public class AddModel : PageModel
    {
		private readonly IBill _Processing = (IBill)Injector.Injector.GetProcessing<BillProcessing>();
        private readonly IBase<Entities.Customer> _customer = (IBase<Entities.Customer>)Injector.Injector.GetProcessing<CustomerProcessing>();
        public string notify = string.Empty;
        private static string _referer = string.Empty;
        public Entities.Customer customer = null;

        [BindProperty(SupportsGet = true)]
        public string Customer { get; set; }
        [BindProperty]
		public int Amount { get; set; }
		public async Task OnGet()
        {
            _referer = Request.Headers["Referer"].ToString();
            if (!string.IsNullOrEmpty(Customer))
            {
                try
                {
                    customer = await _customer.SearchById(Customer);
                }
                catch (Exception ex)
                {
                    notify = ex.Message;
                }
            }
        }

        public async Task OnPost()
		{
            try
            {
                await _Processing.Add(Customer, Amount);
                if (_referer != string.Empty)
                {
                    Response.Redirect(_referer);
                }
                else
                {
                    Response.Redirect("/Bill/View");
                }
            }
            catch (Exception ex)
            {
                notify = ex.Message;
            }
        }
	}
}




