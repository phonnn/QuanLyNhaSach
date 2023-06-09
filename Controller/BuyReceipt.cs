using QuanLyNhaSach.Entities;

namespace QuanLyNhaSach.Controller
{
    public class BuyController : Controller<BuyReceipt>, IBuyReceipt
    {
        //private readonly IBase<User> _user = (IBase<User>)Injector.Injector.GetController<UserController>();
        private readonly IBase<Receipt> _receipt = (IBase<Receipt>)Injector.Injector.GetController<ReceiptController>();
        public async Task<BuyReceipt> Add(User user, List<string> bookIds, List<int> prices)
        {
            if (user == null || bookIds.Count == 0 || bookIds.Count != prices.Count
            )
            {
                throw new Exception("Invalid input");
            }

            IReceipt _tempReceipt = (IReceipt)_receipt;
            BuyReceipt newReceipt = (BuyReceipt) await _tempReceipt.Add(bookIds, prices);
            newReceipt.User = user;
            BuyReceipt result = await _model.AddAsync(newReceipt);
            return result;
        }
        public async Task Update(string receiptId, User user=null, List<string> bookIds=null, List<int> prices=null)
        {
            bookIds ??= new List<string>();
            prices ??= new List<int>();
            if (string.IsNullOrEmpty(receiptId) && user == null
                && (bookIds.Count == 0 || bookIds.Count != prices.Count)
            )
            {
                throw new Exception("Invalid input");
            }

            BuyReceipt foundReceipt = SearchById(receiptId);
            if (foundReceipt == null)
            {
                throw new Exception("Receipt not found");
            }

            if (user != null)
            {
                User oldUser = foundReceipt.User;
                foundReceipt.User = user;
                foundReceipt.UpdatedAt = DateTime.Now;
                await _model.UpdateAsync(receiptId, foundReceipt);
                ////////////
                //do sth update with both user
                ///
            }

            if (bookIds.Count != 0 && bookIds.Count == prices.Count)
            {
                IReceipt _tempReceipt = (IReceipt)_receipt;
                await _tempReceipt.Update(receiptId, bookIds, prices);
            }

        }
    }
}

