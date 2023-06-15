using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using QuanLyNhaSach.Processing;

namespace QuanLyNhaSach.Pages.Bill
{
    public class UpdateModel : PageModel
    {
        private readonly IBase<Entities.Bill> _Processing = (IBase<Entities.Bill>)Injector.Injector.GetProcessing<BillProcessing>();
        public string notify = string.Empty;
        private static string _referer = string.Empty;
        public Entities.Bill bill = new Entities.Bill();

        [BindProperty(SupportsGet = true)]
        public string ID { get; set; }

        [BindProperty]
        public int Amount { get; set; }

        [BindProperty]
        public string Customer { get; set; }

        public async void OnGet()
        {
            _referer = Request.Headers["Referer"].ToString();
            bill = await _Processing.SearchById(ID);
        }
        public async Task OnPost()
        {
            try
            {
                IBill _tempProcessing = (IBill)_Processing;
                bill = await _tempProcessing.Update(ID, Customer, Amount);
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
                bill = await _Processing.SearchById(ID);
                notify = ex.Message;
            }
        }
    }
}
