namespace QuanLyNhaSach.DataAccess
{
    public interface IModel<T>
    {
        Task<List<T>> GetListAsync();
        Task<T> GetByIdAsync(string id);
        Task<T> AddAsync(T entity);
        Task BatchAddAsync(List<T> entities);
        Task UpdateAsync(string id, T newEntity);
        Task BatchUpdateAsync(List<T> entities);
        Task DeleteAsync(T entity);
        Task BatchDeleteAsync(List<T> entities);
    }
}
