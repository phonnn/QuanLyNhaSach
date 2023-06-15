namespace QuanLyNhaSach.Processing
{
    public interface IBase<T>
    {
        bool isExisted(string attribute, string value);
        Task<List<T>> GetAllAsync();
        Task<T> SearchById(string id);
        Task<bool> Delete(string id);
    }
}

