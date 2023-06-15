using QuanLyNhaSach.Entities;
using QuanLyNhaSach.DataAccess;

namespace QuanLyNhaSach.Processing
{
    public class Processing<Model> : IBase<Model> where Model : Base, new()
    {
        protected IModel<Model> _model = (IModel<Model>)Injector.Injector.GetModel<Model>();
        protected List<Model> _items = new List<Model>();
		public bool isExisted(string attribute, string value)
		{
            value = value.ToLower();
			Model found = _items.Find(x => x.GetType().GetProperty(attribute).GetValue(x, null).ToString() == value);
			if (found != null)
            {
				return true;
            }

			return false;
		}
		public async Task<List<Model>> GetAllAsync()
        {
            _items = await _model.GetListAsync();
            return _items;
        }
		public async Task<Model> SearchById(string id)
		{
            Model item = await _model.GetByIdAsync(id);
			return item;
		}
		public async virtual Task<bool> Delete(string id)
        {
            bool deleted = false;
            Model item = await _model.GetByIdAsync(id);
            if (item != null)
            {
                await _model.DeleteAsync(item);
                _items = await _model.GetListAsync();
                deleted = true;
            }

            return deleted;
        }
    }
}