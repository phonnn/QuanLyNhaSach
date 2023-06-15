using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using QuanLyNhaSach.Processing;

namespace QuanLyNhaSach.Pages.Bill
{
    public class DeleteModel : PageModel
    {
        private readonly IBase<Entities.Bill> _Processing = (IBase<Entities.Bill>)Injector.Injector.GetProcessing<BillProcessing>();
        public string notify = string.Empty;
        private static string _referer = string.Empty;
        public Entities.Bill bill = new Entities.Bill();

        [BindProperty(SupportsGet = true)]
        public string ID { get; set; }

        public async Task OnGet()
        {
            bill = await _Processing.SearchById(ID);
            _referer = Request.Headers["Referer"].ToString();
        }
        public async Task OnPost()
        {
            try
            {
                await _Processing.Delete(ID);
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
