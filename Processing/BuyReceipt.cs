using QuanLyNhaSach.Entities;
using QuanLyNhaSach.DataAccess;
using Microsoft.AspNetCore.Authorization;

namespace QuanLyNhaSach.Processing
{
    public class BuyProcessing : ReceiptProcessing, IBuyReceipt
    {
        private readonly IBase<User> _user = (IBase<User>)Injector.Injector.GetProcessing<UserProcessing>();
        protected IModel<BuyReceipt> _receipt = (IModel<BuyReceipt>)Injector.Injector.GetModel<BuyReceipt>();

        public async Task<BuyReceipt> BuyAdd(string userId, List<string> bookIds, List<int> prices, List<int> amounts)
        {
            if (string.IsNullOrEmpty(userId) || bookIds.Count == 0 
                || bookIds.Count != prices.Count || bookIds.Count != amounts.Count
			)
            {
                throw new Exception("Invalid input");
            }

			User user = await _user.SearchById(userId);
			if (user == null)
			{
				throw new Exception("User not found");
			}

            BuyReceipt newReceipt = new BuyReceipt()
            {
                User = user.Id
		    };

            BuyReceipt result = await _receipt.AddAsync(newReceipt);
            await Add(newReceipt, bookIds, prices, amounts);
            return result;
        }
        public async Task BuyUpdate(string receiptId, string userId = "", List<string> bookIds=null, List<int> prices=null, List<int> amounts=null)
        {
            bookIds ??= new List<string>();
            prices ??= new List<int>();
            if (string.IsNullOrEmpty(receiptId) && string.IsNullOrEmpty(userId)
				&& (bookIds.Count == 0 || bookIds.Count != prices.Count || bookIds.Count != amounts.Count)
            )
            {
                throw new Exception("Invalid input");
            }

            Receipt foundReceipt = await SearchById(receiptId);
            if (foundReceipt == null)
            {
                throw new Exception("Receipt not found");
            }

            if (foundReceipt.BuyReceipt == null)
            {
                throw new Exception("Receipt not found");
            }

            if (userId != "" && userId != foundReceipt.BuyReceipt.User.ToString())
            {
                User user = await _user.SearchById(userId);
                if (user == null)
                {
                    throw new Exception("User not found");
                }

                foundReceipt.BuyReceipt.User = user.Id;
                foundReceipt.UpdatedAt = DateTime.Now;
                await _model.UpdateAsync(receiptId, foundReceipt);
            }

            if (bookIds.Count != 0 && bookIds.Count == prices.Count)
            {
                await Update(receiptId, bookIds, prices, amounts);
            }
        }
        public List<Receipt> SearchByUser(string userId)
        {
            List<Receipt> items = _items.FindAll(x => x.BuyReceipt.User.ToString() == userId);
            return items;
		}

        public async Task<BuyReceipt> SearchById(string id)
        {
            BuyReceipt item = await _receipt.GetByIdAsync(id);
            return item;
		}
	}
}

