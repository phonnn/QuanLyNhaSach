using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using QuanLyNhaSach.Processing;
using QuanLyNhaSach.Entities;
using System.Net;

namespace QuanLyNhaSach.Pages.Receipt
{
    public class BuyUpdateModel : PageModel
    {
		private readonly IBuyReceipt _Processing = (IBuyReceipt)Injector.Injector.GetProcessing<BuyProcessing>();
		private readonly IBase<Entities.Book> _book = (IBase<Entities.Book>)Injector.Injector.GetProcessing<BookProcessing>();
		public Entities.Receipt receipt = new Entities.Receipt();

		private static string _referer = string.Empty;
		public string notify = string.Empty;

		[BindProperty(SupportsGet = true)]
		public string ID { get; set; }
		[BindProperty]
		public List<PreReceipt> items { get; set; }
		[BindProperty]
		public string User { get; set; }
		public async Task OnGet()
		{
			_referer = Request.Headers["Referer"].ToString();
			receipt = await _Processing.SearchById(ID);
			items = new List<PreReceipt>();
			foreach (ReceiptBook item in receipt.ReceiptBooks)
            {
				Entities.Book book = await _book.SearchById(item.Book.ToString());
				items.Add(new PreReceipt()
				{
					BookId = item.Book.ToString(),
					BookName = book.Name,
					Price = item.Price,
					Amount = item.Amount,
					Total = item.Total
				});
            }
		}

		public async Task OnPost()
		{
			try
			{
				List<string> bookIds = new List<string>();
				List<int> prices = new List<int>();
				List<int> amounts = new List<int>();
				List<string> visited = new List<string>();
				foreach (PreReceipt item in items)
				{
					int foundIndex = visited.IndexOf($"{item.BookId}:{item.Price}");
					if (foundIndex == -1)
					{
						bookIds.Add(item.BookId);
						prices.Add(item.Price);
						amounts.Add(item.Amount);
						visited.Add($"{item.BookId}:{item.Price}");
					}
					else
					{
						amounts[foundIndex] += item.Amount;
					}
				}

				await _Processing.BuyUpdate(ID, User, bookIds, prices, amounts);
				Response.Redirect(_referer);
			} catch (Exception ex)
			{
				receipt = await _Processing.SearchById(ID);
				notify = ex.Message;
			}
		}
	}
}
