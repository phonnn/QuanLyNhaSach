using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using QuanLyNhaSach.Processing;

namespace QuanLyNhaSach.Pages.Parameter
{
    public class ViewModel : PageModel
    {
        private readonly IBase<Entities.Parameter> _Processing = (IBase<Entities.Parameter>)Injector.Injector.GetProcessing<ParameterProcessing>();
        public List<Entities.Parameter> parameters = new List<Entities.Parameter>();
        public async Task OnGet()
        {
			parameters = await _Processing.GetAllAsync();
        }
    }
}