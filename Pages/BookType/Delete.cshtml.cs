using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using QuanLyNhaSach.Controller;

namespace QuanLyNhaSach.Pages.BookType
{
    public class DeleteModel : PageModel
    {
        private readonly IBase<Entities.BookType> _controller = (IBase<Entities.BookType>)Injector.Injector.GetController<BookTypeController>();
        public string notify;
        public Entities.BookType type = new Entities.BookType();

        [BindProperty(SupportsGet = true)]
        public string ID { get; set; }

        public void OnGet()
        {
            type = _controller.SearchById(ID);
        }
        public async Task OnPost()
        {
            await _controller.Delete(ID);
            notify = "Xoá sản phẩm thành công";
            Response.Redirect("/BookType/View");
        }

    }
}
