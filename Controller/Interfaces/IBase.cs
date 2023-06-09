namespace QuanLyNhaSach.Controller
{
    public interface IBase<T>
    {
        bool isExisted(string attribute, string value);
        Task<List<T>> GetAll();
        T SearchById(string id);
        Task<bool> Delete(string id);
    }
}

