using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using QuanLyNhaSach.Processing;
using QuanLyNhaSach.Entities;
using System.Net;

namespace QuanLyNhaSach.Pages.Receipt
{
    public class BuyModel : PageModel
    {
		private readonly IBuyReceipt _Processing = (IBuyReceipt)Injector.Injector.GetProcessing<BuyProcessing>();
		private readonly IBase<Entities.User> _user = (IBase<Entities.User>)Injector.Injector.GetProcessing<UserProcessing>();
		public List<ReceiptBook> receiptBooks;
		public Entities.User user = null;
		private static string _referer = string.Empty;
		public string notify = string.Empty;

		[BindProperty]
		public List<PreReceipt> items { get; set; }
		[BindProperty(SupportsGet = true)]
		public string User { get; set; }
		public async Task OnGet()
		{
			_referer = Request.Headers["Referer"].ToString();
			items = new List<PreReceipt>();
			if (!string.IsNullOrEmpty(User))
			{
				try
				{
					user = await _user.SearchById(User);
				}
				catch (Exception ex)
				{
					notify = ex.Message;
				}
			}
		}

		public async Task OnPost()
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
				} else
				{
					amounts[foundIndex] += item.Amount;
				}
			}

			await _Processing.BuyAdd(User, bookIds, prices, amounts);
			Response.Redirect(_referer);
		}
	}
}
