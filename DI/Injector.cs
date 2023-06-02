using Microsoft.EntityFrameworkCore;
using QuanLyNhaSach.DataAccess;
using QuanLyNhaSach.Entities;

namespace QuanLyNhaSach.Injector
{
    public class Injector
    {
        private static readonly Dictionary<String, Object>
                   RegisteredObjects = new Dictionary<String, Object>();
        public static Object GetDb()
        {
            string ConnectionString = "Server=DESKTOP-9J7E6C9\\SQLDEV2019;Database=QuanLyNhaSach;Trusted_Connection=True;MultipleActiveResultSets=true;TrustServerCertificate=True";
            if (!RegisteredObjects.ContainsKey("database"))
            {
                var optionsBuilder = new DbContextOptionsBuilder<DataContext>();
                optionsBuilder.UseSqlServer(ConnectionString);
                DataContext dbContext = new DataContext(optionsBuilder.Options);
                RegisteredObjects.Add("database", dbContext);
            }

            return RegisteredObjects["database"];
        }

        public static Object GetModel<T>() where T : Entity
        {
            string modelName = typeof(T).Name;
            if (!RegisteredObjects.ContainsKey(modelName))
            {
                IModel<T> model = new Model<T>();
                RegisteredObjects.Add(modelName, model);
            }

            return RegisteredObjects[modelName];
        }
    }
}
