using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using QuanLyNhaSach.Processing;
using QuanLyNhaSach.Entities;
using System.Net;

namespace QuanLyNhaSach.Pages.Receipt
{

    public class SellModel : PageModel
    {
		private readonly ISellReceipt _Processing = (ISellReceipt)Injector.Injector.GetProcessing<SellProcessing>();
		private readonly IBase<Entities.Customer> _customer = (IBase<Entities.Customer>)Injector.Injector.GetProcessing<CustomerProcessing>();
		public List<ReceiptBook> receiptBooks;
		public Entities.Customer customer = null;
		private static string _referer = string.Empty;
		public string notify = string.Empty;

		[BindProperty]
		public List<PreReceipt> items { get; set; }
		[BindProperty(SupportsGet = true)]
		public string Customer { get; set; }
		public async Task OnGet()
		{
			_referer = Request.Headers["Referer"].ToString();
			items = new List<PreReceipt>();
			if (!string.IsNullOrEmpty(Customer))
			{
				try
				{
					customer = await _customer.SearchById(Customer);
				}
				catch (Exception ex)
				{
					notify = ex.Message;
				}
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

				await _Processing.SellAdd(Customer, bookIds, prices, amounts);
				Response.Redirect(_referer);
			}
			catch (Exception ex)
			{
				notify = ex.Message;
			}
		}
	}
}
