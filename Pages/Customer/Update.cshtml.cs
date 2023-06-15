using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using QuanLyNhaSach.Processing;

namespace QuanLyNhaSach.Pages.Customer
{
    public class UpdateModel : PageModel
    {
        private readonly IBase<Entities.Customer> _Processing = (IBase<Entities.Customer>)Injector.Injector.GetProcessing<CustomerProcessing>();
        public string notify = string.Empty;
        public Entities.Customer customer = new Entities.Customer();

        [BindProperty(SupportsGet = true)]
        public string ID { get; set; }

        [BindProperty]
        public string Name { get; set; }

        [BindProperty]
        public string Address { get; set; }

        [BindProperty]
        public string Number { get; set; }

        [BindProperty]
        public string Email { get; set; }

        public async Task OnGet()
        {
            customer = await _Processing.SearchById(ID);
        }
        public async Task OnPost()
        {
            try
            {
                ICustomer _tempProcessing = (ICustomer)_Processing;
                customer = await _tempProcessing.Update(ID, Name, Address, Email, Number);
                Response.Redirect("/Customer/View");
            }
            catch (Exception ex)
            {
                customer = await _Processing.SearchById(ID);
                notify = ex.Message;
            }
        }
    }
}
