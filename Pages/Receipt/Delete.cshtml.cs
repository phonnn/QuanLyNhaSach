using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using QuanLyNhaSach.Processing;

namespace QuanLyNhaSach.Pages.Receipt
{
    public class DeleteModel : PageModel
    {
        private readonly IBase<Entities.Receipt> _Processing = (IBase<Entities.Receipt>)Injector.Injector.GetProcessing<ReceiptProcessing>();
        private static string _referer = string.Empty;
        public string notify = string.Empty;
        public Entities.Receipt receipt = new Entities.Receipt();

        [BindProperty(SupportsGet = true)]
        public string ID { get; set; }

        public async Task OnGet()
        {
            _referer = Request.Headers["Referer"].ToString();
            receipt = await _Processing.SearchById(ID);
        }
        public async Task OnPost()
        {
            try
            {
                await _Processing.Delete(ID);
                Response.Redirect(_referer);
            }
            catch (Exception ex)
            {
                notify = ex.Message;
            }
        }

    }
}
