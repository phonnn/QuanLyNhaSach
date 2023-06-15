using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using QuanLyNhaSach.Processing;

namespace QuanLyNhaSach.Pages.Customer
{
    public class DeleteModel : PageModel
    {
        private readonly IBase<Entities.Customer> _Processing = (IBase<Entities.Customer>)Injector.Injector.GetProcessing<CustomerProcessing>();
        public string notify = string.Empty;
        public Entities.Customer customer = new Entities.Customer();

        [BindProperty(SupportsGet = true)]
        public string ID { get; set; }

        public async Task OnGet()
        {
            customer = await _Processing.SearchById(ID);
        }
        public async Task OnPost()
        {
            try
            {
                await _Processing.Delete(ID);
                Response.Redirect("/Customer/View");
            }
            catch (Exception ex)
            {
                notify = ex.Message;
            }
        }

    }
}
