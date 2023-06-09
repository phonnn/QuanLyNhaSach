using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using QuanLyNhaSach.Controller;

namespace QuanLyNhaSach.Pages.BookType
{
    public class UpdateModel : PageModel
    {
        private readonly IBase<Entities.BookType> _controller = (IBase<Entities.BookType>)Injector.Injector.GetController<BookTypeController>();
        public string notify;
        public Entities.BookType type;

        [BindProperty(SupportsGet = true)]
        public string ID { get; set; }

        [BindProperty]
        public string Name { get; set; }

        public void OnGet()
        {
            type = _controller.SearchById(ID);
        }
        public async Task OnPost()
        {
            try
            {
                IBookType _tempController = (IBookType)_controller;
                type = await _tempController.Update(ID, Name);
                notify = "Sửa loại hàng thành công";
                Response.Redirect("/BookType/View");
            }
            catch (Exception ex)
            {
                type = _controller.SearchById(ID);
                notify = ex.Message;
            }
        }
    }
}
