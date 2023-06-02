namespace QuanLyNhaSach.DataAccess
{
    public interface IModel<T>
    {
        Task<List<T>> GetListAsync();
        Task<T> GetByIdAsync(string id);
        Task<T> AddAsync(T entity);
        Task UpdateAsync(string id, T newEntity);
        Task DeleteAsync(T entity);
    }
}
