using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using QuanLyNhaSach.Processing;

namespace QuanLyNhaSach.Pages.Parameter
{
    public class UpdateModel : PageModel
    {
        private readonly IBase<Entities.Parameter> _Processing = (IBase<Entities.Parameter>)Injector.Injector.GetProcessing<ParameterProcessing>();
        public string notify = string.Empty;
        public Entities.Parameter parameter = new Entities.Parameter();

        [BindProperty(SupportsGet = true)]
        public string ID { get; set; }

        [BindProperty]
        public string Value { get; set; }

        public async Task OnGet()
        {
			parameter = await _Processing.SearchById(ID);
        }
        public async Task OnPost()
        {
            try
            {
				IParameter _tempProcessing = (IParameter)_Processing;
				parameter = await _tempProcessing.Update(ID, Value);
                Response.Redirect("/Parameter/View");
            }
            catch (Exception ex)
            {
				parameter = await _Processing.SearchById(ID);
                notify = ex.Message;
            }
        }
    }
}
