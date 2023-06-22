using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using QuanLyNhaSach.Processing;
using QuanLyNhaSach.Entities;
using System.Net;

namespace QuanLyNhaSach.Pages.Receipt
{
    public class SellUpdateModel : PageModel
    {
		private readonly ISellReceipt _Processing = (ISellReceipt)Injector.Injector.GetProcessing<SellProcessing>();
		private readonly IBase<Entities.Book> _book = (IBase<Entities.Book>)Injector.Injector.GetProcessing<BookProcessing>();
		private readonly IBase<ReceiptBook> _receiptbook = (IBase<ReceiptBook>)Injector.Injector.GetProcessing<RecieptBookProcessing>();
		public Entities.Receipt receipt = new Entities.Receipt();

		private static string _referer = string.Empty;
		public string notify = string.Empty;

		[BindProperty(SupportsGet = true)]
		public string ID { get; set; }
		[BindProperty]
		public List<PreReceipt> items { get; set; }
		[BindProperty]
		public string Customer { get; set; }
		public async Task OnGet()
		{
			_referer = Request.Headers["Referer"].ToString();
			receipt = await _Processing.SearchById(ID);
			IReceiptBook _tempProcessing = (IReceiptBook)_receiptbook;
			List<ReceiptBook> tempItems = await _tempProcessing.GetByReceipt(ID);
			items = new List<PreReceipt>();
			foreach (ReceiptBook item in tempItems)
            {
				Entities.Book book = await _book.SearchById(item.Book.ToString());
				PreReceipt _tempItem = new PreReceipt()
				{
					BookId = item.Book.ToString(),
					BookName = book.Name,
					Price = item.Price,
					Amount = item.Amount,
					Total = item.Total
				};

				items.Add(_tempItem);
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

				await _Processing.SellUpdate(ID, Customer, bookIds, prices, amounts);
				Response.Redirect(_referer);
			}
			catch (Exception ex)
			{
				notify = ex.Message;
			}
		}
	}
}
