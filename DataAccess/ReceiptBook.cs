using Microsoft.EntityFrameworkCore;
using QuanLyNhaSach.Entities;
using System.Reflection;
using EFCore.BulkExtensions;

namespace QuanLyNhaSach.DataAccess
{
    public class ReceiptBookModel: Model<ReceiptBook>, IReceiptBookModel
    {
        public async Task<List<ReceiptBook>> GetByReceipt(string receiptId)
        {
            List<ReceiptBook> items = await _dbSet.Where(item => item.Receipt.ToString() == receiptId).ToListAsync();
            return items;
        }
    }
}
