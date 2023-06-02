using Microsoft.EntityFrameworkCore;
using QuanLyNhaSach.Entities;
using System.Reflection;

namespace QuanLyNhaSach.DataAccess
{
    public class Model<TEntity>: IModel<TEntity> where TEntity : Entity
    {
        protected readonly DataContext _dbContext;
        protected readonly DbSet<TEntity> _dbSet;
        public Model()
        {
            _dbContext = (DataContext)Injector.Injector.GetDb();
            _dbSet = _dbContext.Set<TEntity>();
        }
        public async Task<List<TEntity>> GetListAsync()
        {
            return await _dbSet.ToListAsync();
        }
        public async Task<TEntity> GetByIdAsync(string id) 
        {
            return await _dbSet.FirstOrDefaultAsync(item => item.Id.ToString() == id);
        }
        public async Task<TEntity> AddAsync(TEntity entity)
        {
            _dbSet.Add(entity);
            await _dbContext.SaveChangesAsync();
            return entity;
        }
        public async Task UpdateAsync(string id, TEntity newEntity)
        {
            var result = _dbSet.SingleOrDefault(item => item.Id.ToString() == id);
            if (result != null)
            {
                string[] omit = { "Id", "CreatedAt" };
                var modelEntity = result.GetType();
                var newObjectEntity = newEntity.GetType();
                PropertyInfo[] properties = modelEntity.GetProperties();
                foreach (PropertyInfo info in properties)
                {
                    if (!omit.Contains(info.Name))
                    {
                        var newItemField = newObjectEntity.GetProperty(info.Name);
                        info.SetValue(result, newItemField.GetValue(newEntity, null));
                    }
                }

                _dbContext.SaveChanges();
            }
            return;
        }
        public async Task DeleteAsync(TEntity entity)
        {
            _dbSet.Remove(entity);
            await _dbContext.SaveChangesAsync();
            return;
        }
    }
}
